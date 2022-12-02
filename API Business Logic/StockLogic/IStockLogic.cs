using XactERPAssessment;
using Microsoft.AspNetCore.Mvc;

public interface IStockLogic
{
    public IEnumerable<StockMaster> Get(string DBConnectionsString);
    public IEnumerable<StockMaster> Search(string DBConnectionsString, string id);
    public ActionResult Edit(string DBConnectionsString, StockMaster changedStock);
    public ActionResult Insert(string DBConnectionsString, StockMaster newStock);
}