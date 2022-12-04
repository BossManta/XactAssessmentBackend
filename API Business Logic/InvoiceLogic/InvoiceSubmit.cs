using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class InvoiceLogic: IInvoiceLogic
{
    public InvoiceDisplayModel Submit(string DBConnectionsString, InvoiceMinimalModel invoiceMinimal)
    {
        InvoiceDisplayModel invoiceData = Preview(DBConnectionsString, invoiceMinimal);

        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            SqliteTransaction transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            

            //Insert invoice general information into database
            command.CommandText = @"INSERT INTO invoice_header (account_code, date, total_sell_amount_excl_vat, vat, total_cost)
                                    VALUES (@AccountCode, @Date, @TotalSellAmountExclVat, @Vat, @TotalCost)";
            CommandObjectMapper.Map<InvoiceGeneralModel>(command, invoiceData.GeneralInfo);
            command.ExecuteNonQuery();

            //Get auto incremented invoice number
            command.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'invoice_header'";
            long invoiceNumber = (long)command.ExecuteScalar();
            invoiceData.GeneralInfo.InvoiceNo = invoiceNumber;


            //Update debtor values
            

            //Loop through all items on invoice
            for (int i=0; i<invoiceData.ItemInfo.Length; i++)
            {
                InvoiceItemModel currentItem = invoiceData.ItemInfo[i];
                currentItem.InvoiceNo = invoiceNumber;

                command.CommandText = @"INSERT INTO invoice_detail VALUES
                                        (@InvoiceNo, @ItemNo, @StockCode, @QtySold, @UnitCost, @CombinedCost, @Disc, @Total)";
                command.Parameters.Clear();
                CommandObjectMapper.Map<InvoiceItemModel>(command, currentItem);
                command.ExecuteNonQuery();
                

                //Update Stock values
                command.CommandText = @"UPDATE stock_master 
                                        SET total_sales_excl_vat = total_sales_excl_vat + @CombinedCost,
                                            qty_sold = qty_sold + @QtySold,
                                            stock_on_hand = stock_on_hand - @QtySold
                                        WHERE stock_code = @StockCode";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CombinedCost",currentItem.CombinedCost);
                command.Parameters.AddWithValue("@QtySold",currentItem.QtySold);
                command.Parameters.AddWithValue("@StockCode",currentItem.StockCode);
                command.ExecuteNonQuery();
            }
            
            transaction.Commit();

        }

        return invoiceData;
    }
}