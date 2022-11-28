using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using static XactERPAssessment.DebtorsMasterTools;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebtorsMasterController : ControllerBase
{
    private readonly ILogger<DebtorsMasterController> _logger;
    private readonly string DBConnectionsString;

    public DebtorsMasterController(ILogger<DebtorsMasterController> logger)
    {
        _logger = logger;
        DBConnectionsString = DbConfig.ConnectionString;
    }


    //----------------ENDPOINTS-----------------------
    [HttpGet]
    public IEnumerable<DebtorsMaster> Get()
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master;";

            List<DebtorsMaster> output = new List<DebtorsMaster>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateNewDebtorMasterFromReader(reader));
                }
            }
            return output;
        }
    }



    [HttpGet("search/{id}")]
    public IEnumerable<DebtorsMaster> Get(string id)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master Where account_code LIKE @SEARCH ;";
            command.Parameters.AddWithValue("@SEARCH", $"%{id}%");

            List<DebtorsMaster> output = new List<DebtorsMaster>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateNewDebtorMasterFromReader(reader));
                }
            }
            return output;
        }
    }

    
    [HttpPost("insert")]
    public ActionResult Post(DebtorsMaster newDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO debtors_master VALUES (@AccountCode, @Address1, @Address2, @Address3, @Balance, @SalesYearToDate, @CostYearToDate);";
                DebtorAddAllValuesToCommand(command, newDebtor);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e){
            if (e.SqliteErrorCode == 19)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Debtor already exists.");
            }
            
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }


    [HttpPut("edit")]
    public ActionResult Put(DebtorsMaster newDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE debtors_master SET 
                                        address1 = @Address1,
                                        address2 = @Address2,
                                        address3 = @Address3,
                                        balance = @Balance,
                                        sales_year_to_date = @SalesYearToDate,
                                        cost_year_to_date = @CostYearToDate
                                        WHERE account_code = @AccountCode;";

                DebtorAddAllValuesToCommand(command, newDebtor);

                command.ExecuteNonQuery();
            }
        }

        catch(SqliteException e){
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}
