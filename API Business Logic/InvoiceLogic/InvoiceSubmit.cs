using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class InvoiceLogic: IInvoiceLogic
{
    //Takes in stripped down invoice and generates detailed invoice.
    //Stripped down invoice only provides debtor account code and list of stock codes.
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
                                    VALUES (@AccountCode, @Date, @TotalSellAmountExclVat, @Vat, @Total)";
            CommandObjectMapper.Map<InvoiceGeneralModel>(command, invoiceData.GeneralInfo);
            command.ExecuteNonQuery();

            //Get auto incremented invoice number
            command.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'invoice_header'";
            long invoiceNumber = (long)command.ExecuteScalar();
            invoiceData.GeneralInfo.InvoiceNo = invoiceNumber;


            //Update debtor values
            command.CommandText = @"UPDATE debtors_master
                                    SET balance = balance - @Total,
                                        cost_year_to_date = cost_year_to_date + @TotalCost,
                                        sales_year_to_date = sales_year_to_date + @TotalSellAmountExclVat
                                    WHERE account_code = @AccountCode;";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Total", invoiceData.GeneralInfo.Total);
            command.Parameters.AddWithValue("@TotalCost", invoiceData.ItemInfo.Sum(a=>a.UnitCost*a.QtySold));
            command.Parameters.AddWithValue("@TotalSellAmountExclVat", invoiceData.GeneralInfo.TotalSellAmountExclVat);
            command.Parameters.AddWithValue("@AccountCode", invoiceData.GeneralInfo.AccountCode);
            command.ExecuteNonQuery();

            //Loop through all items on invoice
            for (int i=0; i<invoiceData.ItemInfo.Length; i++)
            {
                InvoiceItemModel currentItem = invoiceData.ItemInfo[i];
                currentItem.InvoiceNo = invoiceNumber;

                command.CommandText = @"INSERT INTO invoice_detail VALUES
                                        (@InvoiceNo, @ItemNo, @StockCode, @QtySold, @UnitCost, @UnitSell, @CombinedSell, @Disc, @Total)";
                command.Parameters.Clear();
                CommandObjectMapper.Map<InvoiceItemModel>(command, currentItem);
                command.ExecuteNonQuery();
                

                //Update Stock values
                command.CommandText = @"UPDATE stock_master 
                                        SET total_sales_excl_vat = total_sales_excl_vat + @CombinedSell,
                                            qty_sold = qty_sold + @QtySold,
                                            stock_on_hand = stock_on_hand - @QtySold
                                        WHERE stock_code = @StockCode";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CombinedSell",currentItem.CombinedSell);
                command.Parameters.AddWithValue("@QtySold",currentItem.QtySold);
                command.Parameters.AddWithValue("@StockCode",currentItem.StockCode);
                command.ExecuteNonQuery();
            }
            
            transaction.Commit();

        }

        return invoiceData;
    }
}