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
        //CreateCategories(connection);
        UpdateCategories(connection, new Guid("2568524C-FFF5-4F19-B83B-A2908E3E0BC4"), "AWS Cloud Services");
        //ListCategories(connection);
      }
    }

    static void ListCategories(SqlConnection connection)
    {
      var categories = connection.Query<Category>("SELECT [Id], [Title] FROM CATEGORY");

      foreach (var cat in categories)
      {
        Console.WriteLine($"{cat.Id} - {cat.Title}");
      }
    }    
    
    static void CreateCategories(SqlConnection connection)
    {
      var category = new Category()
      {
        Id = Guid.NewGuid(),
        Title = "Amazon AWS",
        Url = "Amazon",
        Summary = "AWS Cloud",
        Order = 8,
        Description = "Categoria destinada a serviços do AWS",
        Featured = false
      };

      var insertSQL = @"INSERT INTO Category 
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

      var rows = connection.Execute(insertSQL, category);

      Console.WriteLine($"Rows Affected: {rows}");
    }

    static void UpdateCategories(SqlConnection connection, Guid Id, string Title)
    {
      var category = new Category()
      {
        Id = Id,
        Title = Title
      };

      var updateCategory = @"UPDATE [Category] SET [Title]=@Title WHERE [Id]=@Id";

      var rows = connection.Execute(updateCategory, category);
      Console.WriteLine($"Rows Affected: {rows}");
    }
  }
}

