namespace Market.Auth.Application.Extensions;

public class OperationResult
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public string? Token { get; set; }
    
    public OperationResult()
    {
    }
    //Other functions here, ctor with success, AddError methods
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}
