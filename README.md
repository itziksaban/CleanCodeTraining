# CleanCodeTraining

## Salaries Payment System

SPS (Salaries Payment System) is a SaaS system that companies use to manage and pay their employees' salaries.  
Each company has 1 or more approvers that need to approve the monthly payment of employees' salaries.
For that we have a MonthlyApprovement object in our db (nevermind which kind of DB) that has a list of Decisions. Each Decision object has 2 fields: 
- Approved (boolean)
- Approver (string)

![alt text](Untitled Diagram (3).jpg)

Every time an ApprovalRequest is coming in, we apply the following logic:
1 - Add the Decision object to the relevant MonthlyApprovement by using IMonthlyApprovementRepository.Update(companyId, month, approved, approver)

2 - Inspect the MonthlyApprovement object that comes back from  IMonthlyApprovementRepository.Update:
- if all approvers approved - pay the salaries by calling ISalaryPayer.PayAll()
- if at least one approver rejected - update the bookkeeper by calling IBookkeeperUpdater.Update()
- If not all company's approvers have sent their approvals/rejections - DO NOTHING!

IMonthlyApprovementRepository.Update works only if the relevant MonthlyApprovement already exists in the DB, therefore, you first need to understand if it exists and if not - create it, as follows:

3 - Try to fetch the relevant MonthlyApprovement from the DB by calling IMonthlyApprovementRepository.Get(companyId, month)

4 - If not found - insert it with the current approval by calling  IMonthlyApprovementRepository.Insert(companyId, month, approval, approver)

5 - If you failed to insert it because it already exists (someone else try to inserted it concurrently) - you can perform section 1 above

6 - if the company has only one owner you can call IMonthlyApprovementRepository.Insert( companyId, month, approval, approver) and then:
- if the only approver approved - pay the salaries by calling ISalaryPayer.PayAll()
- if not all approved - update the bookkeeper by calling IBookkeeperUpdater.Update()
