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
        Guid Id = new Guid("87C967C7-1656-420B-AF02-6B50A4946DDE");

        //CreateCategories(connection);
        //UpdateCategories(connection, Id, "AWS Cloud Services");
        //DeleteCategory(connection, Id);

        ListCategories(connection);
      }
    }

    static void ListCategories(SqlConnection connection)
    {
      var listCategories = "SELECT [Id], [Title] FROM CATEGORY";

      var categories = connection.Query<Category>(listCategories);

      foreach (var category in categories)
      {
        Console.WriteLine($"{category.Id} - {category.Title}");
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

      var updateCategory = "UPDATE [Category] SET [Title]=@Title WHERE [Id]=@Id";

      var rows = connection.Execute(updateCategory, category);
      Console.WriteLine($"Rows Affected: {rows}");
    }

    static void DeleteCategory(SqlConnection connection, Guid Id)
    {
      var category = new Category
      {
        Id = Id
      };

      var deleteCategory = "DELETE FROM [Category] Where Id = @Id";

      var rows = connection.Execute(deleteCategory, category);
      Console.WriteLine($"Rows Affected: {rows}");
    }
  }
}

