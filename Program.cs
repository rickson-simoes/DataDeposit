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
        ListCategories(connection);
        //CreateCategories(connection);
      }
    }

    static void ListCategories(SqlConnection connection)
    {
      var categories = connection.Query<Category>("SELECT * FROM CATEGORY");

      foreach (var cat in categories)
      {
        Console.WriteLine($"{cat.Id} - {cat.Title}");
      }
    }    
    
    static void CreateCategories(SqlConnection connection)
    {
      var category = new Category();

      category.Id = Guid.NewGuid();
      category.Title = "Amazon AWS";
      category.Url = "Amazon";
      category.Summary = "AWS Cloud";
      category.Order = 8;
      category.Description = "Categoria destinada a serviços do AWS";
      category.Featured = false;

      var insertSQL = @"INSERT INTO Category 
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

      var rows = connection.Execute(insertSQL, category);

      Console.WriteLine($"Rows Affected: {rows}");
    }
  }
}

