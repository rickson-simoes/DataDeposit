using System;
using System.Collections.Generic;
using System.Data;
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
        //CreateManyCategories(connection);

        //ExecuteProcedure(connection);

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

    static void CreateManyCategories(SqlConnection connection)
    {
      var category = new List<Category>()
      {
        new Category() {
          Id = Guid.NewGuid(),
          Title = "Amazon AWS",
          Url = "Amazon",
          Summary = "AWS Cloud",
          Order = 8,
          Description = "Categoria destinada a serviços do AWS",
          Featured = false
        },
        new Category() {
          Id = Guid.NewGuid(),
          Title = "Unity Games",
          Url = "Unity",
          Summary = "Desenvolvimento de Jogos",
          Order = 9,
          Description = "Categoria destinada a criação de jogos",
          Featured = true
        }
      };

      var query = @"INSERT INTO [Category] values
      (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

      connection.Execute(query, category);
    }

    static void ExecuteProcedure(SqlConnection connection)
    {
      var procedure = "EXEC [spDeleteStudent] @StudentId";

      var parameters = new { StudentId = "5C5B934B-ADAE-46EB-B834-467BAF50CCAA" };

      var row = connection.Execute(
        procedure, 
        parameters);

      Console.WriteLine($"Affected Rows: {row}");
    }
  }
}

