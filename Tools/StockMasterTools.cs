using Microsoft.Data.Sqlite;

namespace XactERPAssessment;

public static class StockMasterTools
{
    public static StockMaster PopulateNewStockMasterFromReader(SqliteDataReader reader)
    {
        return new StockMaster
        {
            StockCode = reader.GetInt64(0),
            StockDescription = reader.GetString(1),
            Cost = reader.GetDouble(2),
            SellingPrice = reader.GetDouble(3),
            TotalPurchasesExclVat = reader.GetDouble(4),
            TotalSalesExclVat = reader.GetDouble(5),
            QtyPurchased = reader.GetInt64(6),
            QtySold = reader.GetInt64(7),
            StockOnHand = reader.GetInt64(8)
        };
    }

}