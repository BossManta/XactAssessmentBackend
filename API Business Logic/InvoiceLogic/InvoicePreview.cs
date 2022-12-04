using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;
using static XactERPAssessment.DebtorsMasterTools;

public partial class InvoiceLogic: IInvoiceLogic
{
    public InvoiceDisplayModel Preview(string DBConnectionsString, InvoiceMinimalModel invoiceMinimal)
    {
        int totalUniqueItems = invoiceMinimal.StockCountArray.Length;
        InvoiceItemModel[] itemInfo = new InvoiceItemModel[totalUniqueItems];

        SqliteCommand command;
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            //Gather and generate all invoice item info.
            for (int i=0; i<totalUniqueItems; i++)
            {
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM stock_master WHERE stock_code == @StockCode";
                command.Parameters.AddWithValue("@StockCode",invoiceMinimal.StockCountArray[i].StockCode);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        StockModel stockInfo = StockMasterTools.PopulateNewStockMasterFromReader(reader);

                        itemInfo[i] = new InvoiceItemModel();
                        itemInfo[i].ItemNo = i+1;
                        itemInfo[i].StockCode = stockInfo.StockCode;
                        itemInfo[i].QtySold = invoiceMinimal.StockCountArray[i].Count;
                        itemInfo[i].UnitCost = stockInfo.SellingPrice;
                        itemInfo[i].CombinedCost = stockInfo.SellingPrice*invoiceMinimal.StockCountArray[i].Count;
                        itemInfo[i].Disc = stockInfo.StockDescription;
                        itemInfo[i].Total = stockInfo.SellingPrice*invoiceMinimal.StockCountArray[i].Count;
                    }
                }
            }


            //Fetch invoice debtor information
            DebtorModel debtorInfo;

            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master WHERE account_code = @AccountCode LIMIT 1;";
            command.Parameters.AddWithValue("@AccountCode", invoiceMinimal.AccountCode);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    debtorInfo = PopulateNewDebtorMasterFromReader(reader);
                }
                else
                {
                    throw new Exception("Could not load debtor info");
                }
            }


            //Gather and generate all general invoice info
            InvoiceGeneralModel generalInfo = new InvoiceGeneralModel();      
            generalInfo.AccountCode = invoiceMinimal.AccountCode;
            generalInfo.Date = DateOnly.FromDateTime(DateTime.Now);
            generalInfo.TotalSellAmountExclVat = itemInfo.Sum((item) => item.Total);
            generalInfo.Vat = generalInfo.TotalSellAmountExclVat*0.15; //Add Vat
            generalInfo.TotalCost = generalInfo.Vat + generalInfo.TotalSellAmountExclVat;
        
            return new InvoiceDisplayModel{
                GeneralInfo = generalInfo,
                ItemInfo = itemInfo,
                DebtorInfo = debtorInfo
            };
     
        }
    }
}