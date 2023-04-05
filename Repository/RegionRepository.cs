using Connection.Context;
using Connection.Models;
using Connection.Repositories.Interfaces;
using System.Data.SqlClient;

namespace Connection.Repositories;
public class RegionRepository : IRegionRepository
{
    public List<Region> GetAll()
    {
        List<Region> regions = new List<Region>();

        // Membuat instance SQL Server Connection
        var connection = MyContext.GetConnection();

        // Membuat instance SQL Command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select * From region;";

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                // alt 1
                /*Region region = new Region();
                region.Id = reader.GetInt32(0);
                region.Name = reader.GetString(1);*/

                // alt 2
                /*Region region = new Region {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                regions.Add(region);*/

                // alt 3
                regions.Add(new Region
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
        }
        else
        {
            return null;
        }
        reader.Close();
        connection.Close();

        return regions;
    }

    public Region GetById(int id)
    {
        Region region = new Region();
        // Membuat instance SQL Server Connection
        var connection = MyContext.GetConnection();

        // Membuat instance SQL Command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select * From region Where id = @id;";

        // Membuat instance SQL Parameter
        SqlParameter pId = new SqlParameter();
        pId.ParameterName = "@id";
        pId.SqlDbType = System.Data.SqlDbType.Int;
        pId.Value = id;
        command.Parameters.Add(pId);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine("Id : " + reader[0]);
                Console.WriteLine("Name : " + reader[1]);
            }
        }
        else
        {
            Console.WriteLine($"id = {id} is not found!");
        }
        reader.Close();
        connection.Close();
        return region;
    }

    public int Insert(Region region)
    {
        var result = 0;
        var connection = MyContext.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Insert Into region (name) Values (@name);";
            command.Transaction = transaction;

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = region.Name;
            command.Parameters.Add(pName);

            result = command.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();

        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        return result;
    }

    public int Update(Region region)
    {
        var result = 0;
        var connection = MyContext.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Update region Set name = @name Where id = @id;";
            command.Transaction = transaction;

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = System.Data.SqlDbType.VarChar;
            pName.Value = region.Name;
            command.Parameters.Add(pName);

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = region.Id;
            command.Parameters.Add(pId);

            int a = command.ExecuteNonQuery();
            if (a > 0)
            {
                Console.WriteLine("Update Success!");
            }
            else
            {
                Console.WriteLine("Update Failed");
            }

            transaction.Commit();
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Update Failed : " + e.Message);
            try
            {
                transaction.Rollback();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        return result;
    }

    public int Delete(int id)
    {
        var result = 0;
        var connection = MyContext.GetConnection();
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Delete From region Where id = @id;";
            command.Transaction = transaction;

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            command.Parameters.Add(pId);

            int a = command.ExecuteNonQuery();
            if (a > 0)
            {
                Console.WriteLine("Delete Success!");
            }
            else
            {
                Console.WriteLine("Delete Failed!");
            }

            transaction.Commit();
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Delete Failed! : " + e.Message);
            try
            {
                transaction.Rollback();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        return result;
    }
}
