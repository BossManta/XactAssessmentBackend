using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.DebtorsMasterTools;

public partial class DebtorLogic: IDebtorLogic
{
    private DebtorInvoiceModel PopulateDebtorInvoiceModel(SqliteDataReader reader)
    {
        return new DebtorInvoiceModel()
        {
            InvoiceNo = reader.GetInt64(0),
            Date = DateOnly.Parse(reader.GetString(1)),
            ItemCount = reader.GetInt64(2),
            Total = reader.GetDouble(3)
        };
    }

    public IEnumerable<DebtorInvoiceModel> GetInvoices(string DBConnectionsString, long accountCode)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT invoice_no, date,
                                    (SELECT COUNT(*) FROM invoice_detail WHERE invoice_detail.invoice_no=invoice_header.invoice_no), total_cost
                                    FROM invoice_header WHERE account_code=@AccountCode;";
            command.Parameters.AddWithValue("@AccountCode",accountCode);

            List<DebtorInvoiceModel> output = new List<DebtorInvoiceModel>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateDebtorInvoiceModel(reader));
                }
            }
            return output;
        }
    }
}