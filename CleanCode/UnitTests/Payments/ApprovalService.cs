namespace UnitTests.Payments;

public class ApprovalService
{
    private readonly IMonthlyApprovalRepository _monthlyApprovalRepository;
    private readonly IBookkeeperUpdater _bookkeeperUpdater;
    private readonly ISalaryPayer _salaryPayer;

    public ApprovalService(IMonthlyApprovalRepository monthlyApprovalRepository, IBookkeeperUpdater bookkeeperUpdater,
        ISalaryPayer salaryPayer)
    {
        _monthlyApprovalRepository = monthlyApprovalRepository;
        _bookkeeperUpdater = bookkeeperUpdater;
        _salaryPayer = salaryPayer;
    }

    public void AddDecision(string companyId, int month, int year, bool approval, string approver, int requiredApprovers)
    {
        var monthlyApproval = _monthlyApprovalRepository.Update(companyId, month, year, approval, approver);
        if (monthlyApproval.Decisions.Count(decision => !decision.Approved) == 1 && !approval)
        {
            _bookkeeperUpdater.Update(companyId);
        }
        if (monthlyApproval.Decisions.Count == requiredApprovers && monthlyApproval.Decisions.All(decision => decision.Approved))
        {
            _salaryPayer.PayAll(companyId);
        }
    }
}