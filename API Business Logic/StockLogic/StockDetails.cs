using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.StockMasterTools;

public partial class StockLogic: IStockLogic
{

    private StockDetailsModel PopulateStockDetailsModel(SqliteDataReader reader)
    {
        return new StockDetailsModel()
        {
            InvoiceNo = reader.GetInt64(0),
            Date = DateOnly.Parse(reader.GetString(1)),
            DebtorAccountCode = reader.GetInt64(2),
            DebtorName = reader.GetString(3),
            PurchaseQty = reader.GetInt64(4),
            Total = reader.GetDouble(5)
        };
    }

    public IEnumerable<StockDetailsModel> Details(string DBConnectionsString, long stockCode)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT A.invoice_no, B.date, B.account_code, (SELECT name FROM debtors_master WHERE account_code=B.account_code), A.qty_sold, A.total 
                                    FROM invoice_detail AS A LEFT JOIN invoice_header AS B 
                                    ON A.invoice_no=B.invoice_no WHERE stock_code=@StockCode;";
            command.Parameters.AddWithValue("@StockCode",stockCode);

            List<StockDetailsModel> output = new List<StockDetailsModel>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateStockDetailsModel(reader));
                }
            }
            return output;
        }
    }
}