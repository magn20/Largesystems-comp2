using Microsoft.Data.SqlClient;
using PatientService.Exception;
using Serilog;
using SharedModels.models;

namespace PatientService.Repository;

public class PatientRepository : IPatientRepository
{

    private SqlConnection GetConnection()
    {
        var connection = new SqlConnection("Server=patient-db;User Id=sa;Password=patientPassword7!;Encrypt=false;");
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
            "CREATE TABLE Patient(ssn varchar(10) PRIMARY KEY, mail varchar(128), name varchar(128))");
    }

    public void AddPatient(Patient patient)
    {
        try
        {
            using var con = GetConnection();
            using SqlCommand cmd =
                new SqlCommand(
                    "INSERT INTO Patient(ssn, mail, name) VALUES (@ssn, @mail, @name)",
                    con);
            cmd.Parameters.AddWithValue("@ssn", patient.Ssn);
            cmd.Parameters.AddWithValue("@mail", patient.Mail);
            cmd.Parameters.AddWithValue("@name", patient.Name);
            cmd.ExecuteNonQuery();
        } catch (System.Exception ex)
        {
            Log.Logger.Error("Create calculation history failed with the Exception: {CreatePatientException}", ex);
            throw new DatabaseWriteException("Failed to create patient", ex);
        }
    }

    public void DeletePatient(Patient patient)
    {
        try
        {
            using var con = GetConnection();
            using SqlCommand cmd =
                new SqlCommand(
                    "DELETE FROM Patient WHERE ssn= @ssn",
                    con);
            cmd.Parameters.AddWithValue("@ssn", patient.Ssn);
            cmd.ExecuteNonQuery();
        } catch (System.Exception ex)
        {
            Log.Logger.Error("Create calculation history failed with the Exception: {CreatePatientException}", ex);
            throw new DatabaseWriteException("Failed to create patient", ex);
        }
    }

    public Patient GetPatient(string ssn)
    {
        try
        {
            var patient = new Patient();
            using var con = GetConnection();
            var command = new SqlCommand("SELECT * FROM Patient WHERE ssn= @ssn", con);
            command.Parameters.AddWithValue("@ssn", ssn);


             using var reader =  command.ExecuteReader();
            while (reader.Read())
            {
                patient = new Patient()
                {
                    Ssn = reader.GetString(reader.GetOrdinal("ssn")),
                    Mail = reader.GetString(reader.GetOrdinal("mail")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                };
               
            }

            return patient;
        }
        catch (System.Exception ex)
        {
            Log.Logger.Error("Get patient failed with the Exception: {DatabaseReadException}", ex);
            throw new DatabaseReadException("Failed to get patient", ex);
        }
    }

    public List<Patient> GetAllPatient()
    {
        
        try
        {
            var patientList = new List<Patient>();
             using var con = GetConnection();
            var command = new SqlCommand("SELECT * FROM Patient", con);

            using var reader =  command.ExecuteReader();
            while (reader.Read())
            {
                var patient = new Patient()
                {
                    Ssn = reader.GetString(reader.GetOrdinal("ssn")),
                    Mail = reader.GetString(reader.GetOrdinal("mail")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                };
                patientList.Add(patient);
            }

            return patientList;
        }
        catch (System.Exception ex)
        {
            Log.Logger.Error("Get all patients failed with the Exception: {DatabaseReadException}", ex);
            throw new DatabaseReadException("Failed to get all patients", ex);
        }
    }
    
    
}