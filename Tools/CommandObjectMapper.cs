using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace XactERPAssessment;


//Maps an object property to a command parameter. Example: Maps objToMap.test to @test 
public static class CommandObjectMapper
{
    public static void Map<T>(SqliteCommand command, T objToMap)
    {
        var commandParams = Regex.Matches(command.CommandText,@"@\b\S+?\b")
                                    .Select(m=>m.ToString().Substring(1))
                                    .ToHashSet<string>();

        var objectProps = objToMap.GetType().GetProperties();
        foreach (var prop in objectProps)
        {
            if (commandParams.Contains(prop.Name))
            {
                command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(objToMap));
                commandParams.Remove(prop.Name);
            }
        }

        if (commandParams.Count>0)
        {
            throw new Exception($"Not all command parameters were mapped. Missing @{commandParams.Aggregate((s1,s2)=>s1+", "+s2)}");
        }
    }
}