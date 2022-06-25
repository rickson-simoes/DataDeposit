using System;
using Dapper;
using DataDeposit.Models;
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
        var categories = connection.Query<Category>("SELECT * FROM CATEGORY");

        foreach(var category in categories)
        {
          Console.WriteLine($"{category.Id} - {category.Title}");
        }
        
      }
    }
  }
}

