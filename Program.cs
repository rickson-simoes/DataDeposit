using System;
using Microsoft.Data.SqlClient;

namespace DataDeposit
{
  internal class Program
  {
    static void Main(string[] args)
    {
      const string ConnectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=a1b2c3d4#@!;Trusted_Connection=False;TrustServerCertificate=True;";

      using (var connection = new SqlConnection(ConnectionString)) 
      {
        Console.WriteLine("Conectado");
        connection.Open();
         Console.WriteLine("");

        using(var command = new SqlCommand())
        {
          command.Connection = connection;
          command.CommandType = System.Data.CommandType.Text;

          command.CommandText = "SELECT ID, TITLE FROM CATEGORY";

          var reader = command.ExecuteReader();
          while(reader.Read())
          {
            Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
          }
        }
      }
    }
  }
}
