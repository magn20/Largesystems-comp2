namespace PatientService.Exception;

public class DatabaseReadException: System.Exception
{
    public DatabaseReadException(string message)  
        : base(message)  
    {  
    }  
  
    public DatabaseReadException(string message, System.Exception inner)  
        : base(message, inner)  
    {  
    }  
}