﻿using System;
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

        //ExecuteDeleteProcedure(connection);

        //ExecuteListCoursesByCategoryIdProcedure(connection);

        //ListCategories(connection);

        //ExecuteScalar(connection);

        //ReadViewCourse(connection);

        OneToOne(connection);
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

    static void ExecuteDeleteProcedure(SqlConnection connection)
    {
      var procedure = "[spDeleteStudent]";

      var parameters = new { StudentId = "2362EA27-E741-49A3-BCF8-9317EBBA7420" };

      var row = connection.Execute(
        procedure, 
        parameters,
        commandType: CommandType.StoredProcedure);

      Console.WriteLine($"Affected Rows: {row}");
    }

    static void ExecuteListCoursesByCategoryIdProcedure(SqlConnection connection)
    {
      var procedureName = "[spGetCoursesByCategory]";

      var parameters = new { CategoryId = "09CE0B7B-CFCA-497B-92C0-3290AD9D5142" }; //backend

      var courses = connection.Query(procedureName, parameters, commandType: CommandType.StoredProcedure); //dinamico

      foreach(var item in courses)
      {
        Console.WriteLine(item.Title);
      }
    }

    static void ExecuteScalar(SqlConnection connection)
    {
      var category = new Category()
      {
        Title = "Teste Category",
        Url = "Teste",
        Summary = "Category Teste Summary",
        Order = 10,
        Description = "Categoria destinada a testes",
        Featured = false
      };

      var insertSQL = @"INSERT INTO Category OUTPUT inserted.[Id]
                            VALUES(NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured)";

      var Id = connection.ExecuteScalar<Guid>(insertSQL, category);

      Console.WriteLine($"Exibe o ID que foi inserido: {Id}");
    }

    static void ReadViewCourse(SqlConnection connection)
    {
      var query = "select * from vwCourse";

      var viewCourse = connection.Query(query);

      foreach(var item in viewCourse)
      {
        Console.WriteLine($"{item.Id} - {item.Title}");
      }
    }

    static void OneToOne(SqlConnection connection)
    {
      var innerJoinQuery = @"
          select * from CareerItem as CI
          inner join Course on Course.Id = CI.CourseId order by [CI].[Order]";

      var CareerItemJoinsCourse = connection.Query<CareerItem, Course, CareerItem>(innerJoinQuery,
        (careerItem, course) => {
          careerItem.Course = course;

          return careerItem;
        }, splitOn: "Id");

      foreach (var item in CareerItemJoinsCourse)
      {
        Console.Write($"{item.Title} - ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write($" Curso: {item.Course.Title} ");        
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write($" {item.Course.Title} ");
        Console.ResetColor();
        Console.WriteLine("");
      }
    }

    static void OneToMany(SqlConnection connection)
    {
      /*
        SELECT 
            [Career].[Id],
            [Career].[Title],
            [CareerItem].[CareerId],
            [CareerItem].[Title]
        FROM 
            [Career] 
        INNER JOIN 
            [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
        ORDER BY
            [Career].[Title]
       */

    }
  }
}

