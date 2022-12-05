using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;

public partial class DebtorLogic: IDebtorLogic
{

    private InvoiceItemModel PopulateInvoiceItemModel(SqliteDataReader reader)
    {
        return new InvoiceItemModel()
        {
            InvoiceNo = reader.GetInt64(0),
            ItemNo = reader.GetInt64(1),
            StockCode = reader.GetInt64(2),
            QtySold = reader.GetInt64(3),
            UnitCost = reader.GetDouble(4),
            UnitSell = reader.GetDouble(5),
            CombinedSell = reader.GetDouble(6),
            Disc = reader.GetString(7),
            Total = reader.GetDouble(8)
        };
    }

    //Returns a list of items from a given invoice number. This is used to show items purchased by debtor.
    public IEnumerable<InvoiceItemModel> GetInvoiceItems(string DBConnectionsString, long invoiceNo)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM invoice_detail WHERE invoice_no=@InvoiceNo;";
            command.Parameters.AddWithValue("@InvoiceNo",invoiceNo);

            List<InvoiceItemModel> output = new List<InvoiceItemModel>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateInvoiceItemModel(reader));
                }
            }
            return output;
        }
    }
}