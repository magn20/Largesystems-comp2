namespace PatientService.Exception;

public class DatabaseWriteException: System.Exception
{
    public DatabaseWriteException(string message)  
        : base(message)  
    {  
    }  
  
    public DatabaseWriteException(string message, System.Exception inner)  
        : base(message, inner)  
    {  
    }  
}