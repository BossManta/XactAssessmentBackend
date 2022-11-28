using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Net;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebtorsMasterController : ControllerBase
{
    private readonly ILogger<DebtorsMasterController> _logger;

    public DebtorsMasterController(ILogger<DebtorsMasterController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<DebtorsMaster> Get()
    {
        using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master;";

            List<DebtorsMaster> output = new List<DebtorsMaster>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(new DebtorsMaster
                    {
                        AccountCode = reader.GetInt64(0),
                        Address1 = reader.GetString(1),
                        Address2 = reader.GetString(2),
                        Address3 = reader.GetString(3),
                        Balance = reader.GetFloat(4),
                        SalesYearToDate = DateOnly.Parse(reader.GetString(5)),
                        CostYearToDate = DateOnly.Parse(reader.GetString(6))
                    });
                }
            }
            return output;
        }
    }


    [HttpGet("search/{id}")]
    public IEnumerable<DebtorsMaster> Get(string id)
    {
        using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
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
                    output.Add(new DebtorsMaster
                    {
                        AccountCode = reader.GetInt64(0),
                        Address1 = reader.GetString(1),
                        Address2 = reader.GetString(2),
                        Address3 = reader.GetString(3),
                        Balance = reader.GetFloat(4),
                        SalesYearToDate = DateOnly.Parse(reader.GetString(5)),
                        CostYearToDate = DateOnly.Parse(reader.GetString(6))
                    });
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
            using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO debtors_master VALUES (@AccountCode, @Address1, @Address2, @Address3, @Balance, @SalesYearToDate, @CostYearToDate);";
                command.Parameters.AddWithValue("@AccountCode", newDebtor.AccountCode);
                command.Parameters.AddWithValue("@Address1", newDebtor.Address1);
                command.Parameters.AddWithValue("@Address2", newDebtor.Address2);
                command.Parameters.AddWithValue("@Address3", newDebtor.Address3);
                command.Parameters.AddWithValue("@Balance", newDebtor.Balance);
                command.Parameters.AddWithValue("@SalesYearToDate", newDebtor.SalesYearToDate);
                command.Parameters.AddWithValue("@CostYearToDate", newDebtor.CostYearToDate);

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
            using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
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

                command.Parameters.AddWithValue("@AccountCode", newDebtor.AccountCode);
                command.Parameters.AddWithValue("@Address1", newDebtor.Address1);
                command.Parameters.AddWithValue("@Address2", newDebtor.Address2);
                command.Parameters.AddWithValue("@Address3", newDebtor.Address3);
                command.Parameters.AddWithValue("@Balance", newDebtor.Balance);
                command.Parameters.AddWithValue("@SalesYearToDate", newDebtor.SalesYearToDate);
                command.Parameters.AddWithValue("@CostYearToDate", newDebtor.CostYearToDate);

                command.ExecuteNonQuery();
            }
        }

        catch(SqliteException e){
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}
