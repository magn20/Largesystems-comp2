namespace SharedModels.models;

public class Patient
{
    public string Ssn { get; set; }
    public string Mail { get; set; }
    public string Name { get; set; }
    public List<Measurement> Measurement { get; set; }
}