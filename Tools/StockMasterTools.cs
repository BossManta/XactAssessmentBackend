using Microsoft.Data.Sqlite;

namespace XactERPAssessment;

public static class StockMasterTools
{
    //----------------------Reusable Tools---------------------
    public static void StockAddAllValuesToCommand(SqliteCommand command, StockMaster stock)
    {
        command.Parameters.AddWithValue("@StockCode", stock.StockCode);
        command.Parameters.AddWithValue("@StockDescription", stock.StockDescription);
        command.Parameters.AddWithValue("@Cost", stock.Cost);
        command.Parameters.AddWithValue("@SellingPrice", stock.SellingPrice);
        command.Parameters.AddWithValue("@TotalPurchasesExclVat", stock.TotalPurchasesExclVat);
        command.Parameters.AddWithValue("@TotalSalesExclVat", stock.TotalSalesExclVat);
        command.Parameters.AddWithValue("@QtyPurchased", stock.QtyPurchased);
        command.Parameters.AddWithValue("@QtySold", stock.QtySold);
        command.Parameters.AddWithValue("@StockOnHand", stock.StockOnHand);
    }

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