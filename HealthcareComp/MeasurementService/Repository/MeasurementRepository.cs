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
            "CREATE TABLE Measurement(id int IDENTITY(1,1) PRIMARY KEY,  date datetime, systolic int, diastolic int, seen bit ,patientSSN varchar(10))");
    }

    public void AddMeasurement(Measurement measurement, string patientSsn)
    {
        try
        {
            using var con = GetConnection();
            using SqlCommand cmd =
                new SqlCommand(
                    "INSERT INTO Measurement(date, systolic, diastolic,seen,  patientSSN) VALUES (@date, @systolic, @diastolic,@seen, @patientSSN)",
                    con);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@systolic", measurement.Systolic);
            cmd.Parameters.AddWithValue("@diastolic", measurement.Diastolic);
            cmd.Parameters.AddWithValue("@seen", measurement.Seen ? 1 : 0); 
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
                    "UPDATE Measurement SET seen = @seen WHERE id = @id",
                    con);
            cmd.Parameters.AddWithValue("@id", measurement.Id);
            cmd.Parameters.AddWithValue("@seen", measurement.Seen ? 1 : 0); 
            cmd.ExecuteNonQuery();
        } catch (System.Exception ex)
        {
            Log.Logger.Error("Update Measurement failed with the Exception: {CreatePatientException}", ex);
            throw new DatabaseWriteException("Failed to Update Measurement", ex);
        }
    }

    public List<Measurement> GetMeasurement(string patientSsn)
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
                    Seen = reader.GetBoolean(reader.GetOrdinal("seen")),
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