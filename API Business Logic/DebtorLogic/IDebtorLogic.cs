using Microsoft.AspNetCore.Mvc;
using XactERPAssessment;

public interface IDebtorLogic
{
    public IEnumerable<DebtorsMaster> Get(string DBConnectionsString);
    public IEnumerable<DebtorsMaster> Search(string DBConnectionsString, string id);
    public ActionResult Edit(string DBConnectionsString, DebtorsMaster changedDebtor);
    public ActionResult Insert(string DBConnectionsString, DebtorsMaster newDebtor);
}