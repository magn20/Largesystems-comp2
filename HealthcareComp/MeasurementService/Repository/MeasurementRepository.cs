using Microsoft.Data.SqlClient;
using Serilog;
using SharedModels.Exception;
using SharedModels.models;

namespace MeasurementService.Repository;

public class MeasurementRepository : IMeasurementRepository
{
    private SqlConnection GetConnection()
    {
        var connection = new SqlConnection("Server=measurement-db;User Id=sa;Password=measurementPassword7!;Encrypt=false;");
        connection.Open();
        return connection;
    }
    private void Execute(SqlConnection connection, string sql)
    {
        using var trans = connection.BeginTransaction();
        var cmd = connection.CreateCommand();
        cmd.Transaction = trans;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        trans.Commit();
    }
    public void RecreateDatabase()
    {
        using var con = GetConnection();
        Execute(con,
            "CREATE TABLE Measurement(id int IDENTITY(1,1) PRIMARY KEY,  date datetime, systolic int, diastolic int  ,patientSSN varchar(10))");
    }

    public void AddMeasurement(Measurement measurement, int patientSsn)
    {
        try
        {
            using var con = GetConnection();
            using SqlCommand cmd =
                new SqlCommand(
                    "INSERT INTO Measurement(date, systolic, diastolic, patientSSN) VALUES (@date, @systolic, @diastolic, @patientSSN)",
                    con);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@systolic", measurement.Systolic);
            cmd.Parameters.AddWithValue("@diastolic", measurement.Diastolic);
            cmd.Parameters.AddWithValue("@patientSSN", patientSsn);
            cmd.ExecuteNonQuery();
        } catch (System.Exception ex)
        {
            Log.Logger.Error("Create Measurement failed with the Exception: {CreatePatientException}", ex);
            throw new DatabaseWriteException("Failed to create Measurement", ex);
        }
    }

    public void UpdateMeasurement(Measurement measurement)
    {
        try
        {
            using var con = GetConnection();
            using SqlCommand cmd =
                new SqlCommand(
                    "UPDATE Measurement WHERE id = @id SET (systolic, diastolic ) VALUES (@systolic, @diastolic)",
                    con);
            cmd.Parameters.AddWithValue("@id", measurement.Id);
            cmd.Parameters.AddWithValue("@systolic", measurement.Systolic);
            cmd.Parameters.AddWithValue("@diastolic", measurement.Diastolic);
            cmd.ExecuteNonQuery();
        } catch (System.Exception ex)
        {
            Log.Logger.Error("Update Measurement failed with the Exception: {CreatePatientException}", ex);
            throw new DatabaseWriteException("Failed to Update Measurement", ex);
        }
    }

    public List<Measurement> GetMeasurement(int patientSsn)
    {
        try
        {
            var MeasurementList = new List<Measurement>();
            using var con = GetConnection();
            var command = new SqlCommand("SELECT * FROM Measurement WHERE patientSSN= @patientSsn", con);
            command.Parameters.AddWithValue("@patientSSN", patientSsn);

            using var reader =  command.ExecuteReader();
            while (reader.Read())
            {
                var measurement = new Measurement()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Date = reader.GetDateTime(reader.GetOrdinal("date")),
                    Systolic = reader.GetInt32(reader.GetOrdinal("systolic")),
                    Diastolic = reader.GetInt32(reader.GetOrdinal("diastolic")),
                };
                MeasurementList.Add(measurement);
            }

            return MeasurementList;
        }
        catch (System.Exception ex)
        {
            Log.Logger.Error("Get measurement failed with the Exception: {DatabaseReadException}", ex);
            throw new DatabaseReadException("Failed to get measurement", ex);
        }
    }
}