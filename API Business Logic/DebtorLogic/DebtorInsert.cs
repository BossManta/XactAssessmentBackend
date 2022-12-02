using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;

public partial class DebtorLogic: IDebtorLogic
{
    public ActionResult Insert(string DBConnectionsString, DebtorsMaster newDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO debtors_master VALUES (@AccountCode, @Address1, @Address2, @Address3, @Balance, @SalesYearToDate, @CostYearToDate);";
                CommandObjectMapper.Map<DebtorsMaster>(command, newDebtor);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e)
        {

            if (e.SqliteErrorCode == 19)
            {
                return new ObjectResult("Debtor already exists.")
                {
                    StatusCode=StatusCodes.Status400BadRequest
                };
            }
            
            return new ObjectResult(e.Message)
            {
                StatusCode=StatusCodes.Status500InternalServerError
            };
        }

        return new OkObjectResult("All Good");
    }
}