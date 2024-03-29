# CleanCodeTraining

## Salaries Payment System

SPS (Salaries Payment System) is a SaaS system that companies use to manage and pay their employees' salaries.  
Each company has 1 or more approvers that need to approve the monthly payment of all employees' salaries in their company.
For that, each company has a `MonthlyApproval` object in the db (nevermind which kind of DB - we mock it anyway) that has a list of `Decision` objects.

![alt text](MonthlyPayment1.jpg)  

Note: `Decision.Approved = false` means a rejection.

Your job is to implement the following logic inside the `ApprovalService.AddDecision(companyId, month, year, approval, approver, requiredApprovers)` method, in a TDD style:  

Everytime an approver approves or rejects:  
1 - A `Decision` object should be added to the relevant `MonthlyApproval` in the DB, and it's done by calling: `IMonthlyApprovalRepository.Update(companyId, month, year, approved, approver)`

2 - Inspect the `MonthlyApproval` object that comes back from  `IMonthlyApprovalRepository.Update`:
- if at least one approver rejected - update the bookkeeper by calling `IBookkeeperUpdater.Update(companyId)`
- if all approvers approved - pay the salaries by calling `ISalaryPayer.PayAll(companyId)`
- If there are no rejections so far, and yet not all company's approvers have sent their decisions - DO NOTHING! (the `requiredApprovers` parameter of `ApprovalService.AddDecision` indicates how many required approvers a company has)


### Advanced:  

`IMonthlyApprovalRepository.Update` works only if the relevant `MonthlyApproval` already exists in the DB, therefore, before you call it, you first need to understand if it exists and if not - create it instead, as follows:

3 - Try to fetch the relevant `MonthlyApproval` from the DB by calling `IMonthlyApprovalRepository.Get(companyId, month)`

4 - If not found - create a new one with the approver decision by calling  `IMonthlyApprovalRepository.CreateNew(companyId, month, year, approval, approver)`

5 - If you failed to insert it (method return false) because it already exists (someone else tried to insert it concurrently) - you can perform section 1 above

6 - if the company has only one approver you can call `IMonthlyApprovalRepository.CreateNew(companyId, month, year, approval, approver)` and then:
- if the only approver approved - pay the salaries by calling `ISalaryPayer.PayAll(companyId)`
- if not all approved - update the bookkeeper by calling `IBookkeeperUpdater.Update(companyId)`
