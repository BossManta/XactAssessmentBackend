using Microsoft.Data.Sqlite;
using XactERPAssessment;

public partial class InvoiceLogic: IInvoiceLogic
{
    public InvoiceFull Preview(string DBConnectionsString, InvoiceFoundation foundation)
    {
        int totalUniqueItems = foundation.StockCountArray.Length;
        InvoiceDetail[] invoiceDetails = new InvoiceDetail[totalUniqueItems];

        SqliteCommand command;
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            for (int i=0; i<totalUniqueItems; i++)
            {
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM stock_master WHERE stock_code == @StockCode";
                command.Parameters.AddWithValue("@StockCode",foundation.StockCountArray[i].StockCode);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        StockMaster stockInfo = StockMasterTools.PopulateNewStockMasterFromReader(reader);

                        //Propulate Invoice Detail data for each stock item on invoice
                        invoiceDetails[i] = new InvoiceDetail();
                        invoiceDetails[i].ItemNo = i+1;
                        invoiceDetails[i].StockCode = stockInfo.StockCode;
                        invoiceDetails[i].QtySold = foundation.StockCountArray[i].Count;
                        invoiceDetails[i].UnitCost = stockInfo.Cost;
                        invoiceDetails[i].UnitSell = stockInfo.SellingPrice;
                        invoiceDetails[i].Disc = stockInfo.StockDescription;
                        invoiceDetails[i].Total = stockInfo.SellingPrice*foundation.StockCountArray[i].Count;
                    }
                }
            }

            //Populate Invoice Header data
            InvoiceHeader invoiceHeader = new InvoiceHeader();      
            invoiceHeader.AccountCode = foundation.AccountCode;
            invoiceHeader.Date = DateOnly.FromDateTime(DateTime.Now);
            invoiceHeader.TotalSellAmountExclVat = invoiceDetails.Sum((invoiceDetail) => invoiceDetail.Total);
            invoiceHeader.Vat = invoiceHeader.TotalSellAmountExclVat*0.15; //Add Vat
            invoiceHeader.TotalCost = invoiceHeader.Vat + invoiceHeader.TotalSellAmountExclVat;
        
            return new InvoiceFull{
                header = invoiceHeader,
                details = invoiceDetails
            };
     
        }
    }
}