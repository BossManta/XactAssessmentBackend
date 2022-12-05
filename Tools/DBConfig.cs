namespace XactERPAssessment;


//Tool to get database connection string from app.config file.
public static class DbConfig
{
    public static string ConnectionString { 
        get {
            string? tmpConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
            if (tmpConnectionString == null)
            {
                throw new SystemException("Could not load DB connection string");
            }
            return tmpConnectionString;
        }
    }
}