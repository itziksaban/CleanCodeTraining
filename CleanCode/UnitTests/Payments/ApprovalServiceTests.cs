using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Payments
{
    [TestClass]
    public class ApprovalServiceTests
    {
        private const string CompanyId = "companyId";
        private const string Approver = "approver";
        private Mock<IMonthlyApprovalRepository> _monthlyApprovalRepository;
        private ApprovalService _approvalService;
        private Mock<IBookkeeperUpdater> _bookKeeperUpdater;
        private Mock<ISalaryPayer> _salaryPayer;
        private MonthlyApproval _monthlyApproval;

        public ApprovalServiceTests()
        {
            _salaryPayer = new Mock<ISalaryPayer>();
            _bookKeeperUpdater = new Mock<IBookkeeperUpdater>();
            _monthlyApprovalRepository = new Mock<IMonthlyApprovalRepository>();
            _approvalService = new ApprovalService(_monthlyApprovalRepository.Object, _bookKeeperUpdater.Object, _salaryPayer.Object);
            _monthlyApproval = new MonthlyApproval
            {
                Decisions = new()
                {
                    new()
                    {
                        Approved = true
                    },
                    new()
                    {
                        Approved = true
                    }
                }
            };
            _monthlyApprovalRepository.Setup(repository => repository.Update(CompanyId, 12, 2022, It.IsAny<bool>(), Approver))
                .Returns(_monthlyApproval);
        }

        [TestMethod]
        public void ShouldSaveInDb()
        {
            _approvalService.AddDecision(CompanyId, 12, 2022, false, Approver, 1);

            _monthlyApprovalRepository.Verify(repository => repository.Update(CompanyId, 12, 2022, false, Approver));
        }

        [TestMethod]
        public void ShouldUpdateTheBookKeeperIfOneApproverRejected()
        {
            _monthlyApproval.Decisions[0].Approved = false;
            _approvalService.AddDecision(CompanyId, 12, 2022, false, Approver, 1);

            _bookKeeperUpdater.Verify(updater => updater.Update(CompanyId));
        }

        [TestMethod]
        public void ShouldPayWhenAllApproversApproved()
        {
            _approvalService.AddDecision(CompanyId, 12, 2022, true, Approver, 2);

            _salaryPayer.Verify(payer => payer.PayAll(CompanyId));
        }


        [TestMethod]
        public void ShouldNotPayWhenNotAllApproversApproved()
        {
            _approvalService.AddDecision(CompanyId, 12, 2022, true, Approver, 3);

            _salaryPayer.Verify(payer => payer.PayAll(CompanyId), Times.Never);
        }

        [TestMethod]
        public void ShouldNotUpdateTheBookKeeperIfNoApproverRejected()
        {
            _approvalService.AddDecision(CompanyId, 12, 2022, true, Approver, 3);

            _bookKeeperUpdater.Verify(updater => updater.Update(CompanyId), Times.Never);
        }

        [TestMethod]
        public void ShouldNotUpdateTheBookKeeperMoreThanOnceIfThereAreAlreadyMoreThanOneRejections()
        {
            //if there are more than one rejections in the returning back 'MonthlyApproval` - it means that someone already rejected before the current approver, thus, an update to the bookkeeper was sent already.
            _monthlyApproval.Decisions[0].Approved = false;
            _monthlyApproval.Decisions[1].Approved = false;

            _approvalService.AddDecision(CompanyId, 12, 2022, false, Approver, 2);

            _bookKeeperUpdater.Verify(updater => updater.Update(CompanyId), Times.Never);
        }

        [TestMethod]
        public void ShouldNotUpdateTheBookKeeperMoreThanOnceIfThereIsExactlyOneRejectionButItIsNotTheCurrentApprover()
        {
            //if there is exactly one rejection in the returning back 'MonthlyApproval` and only if its the current approver - this means we never updated the bookkeeper before, therefore - update it, otherwise - don't.
            _monthlyApproval.Decisions[0].Approved = false;

            _approvalService.AddDecision(CompanyId, 12, 2022, true, Approver, 2);

            _bookKeeperUpdater.Verify(updater => updater.Update(CompanyId), Times.Never);
        }
    }
}
