using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class DebtorLogic: IDebtorLogic
{
    //Replaces all values for a debtor based on new passed in debtor
    public ActionResult Edit(string DBConnectionsString, DebtorModel changedDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE debtors_master SET 
                                        name = @Name,
                                        address1 = @Address1,
                                        address2 = @Address2,
                                        address3 = @Address3,
                                        balance = @Balance,
                                        sales_year_to_date = @SalesYearToDate,
                                        cost_year_to_date = @CostYearToDate
                                        WHERE account_code = @AccountCode;";

                CommandObjectMapper.Map<DebtorModel>(command, changedDebtor);

                command.ExecuteNonQuery();
            }
        }

        catch(SqliteException e){

            return new ObjectResult(e.Message)
            {
                StatusCode=StatusCodes.Status500InternalServerError
            };
        }

        return new OkObjectResult("All Good");
    }
}