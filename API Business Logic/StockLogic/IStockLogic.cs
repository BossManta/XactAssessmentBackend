using XactERPAssessment.Models;
using Microsoft.AspNetCore.Mvc;

//These are all actions related to debtors.
public interface IStockLogic
{
    public IEnumerable<StockModel> Get(string DBConnectionsString);
    public IEnumerable<StockModel> Search(string DBConnectionsString, string id);
    public ActionResult Edit(string DBConnectionsString, StockModel changedStock);
    public ActionResult Insert(string DBConnectionsString, StockModel newStock);
    public ActionResult AddStock(string DBConnectionString, StockCount stock);
    public IEnumerable<StockDetailsModel> Details(string DBConnectionString, long stockId);
}