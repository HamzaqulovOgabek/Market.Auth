using BRB.Core.Common.Models.Base;
using System.Net;

namespace AppError.Contracts;

public class AppError : ModelBase<long>
{
    public string IpAddress { get; set; } = default!;// index
    public int Port { get; set; } // index
    public DateTime CreatedDate { get; set; } // index
    public string ProjectName { get; set; } = default!; // index
    public string Controller { get; set; } = default!; // index
    public string Action { get; set; } = default!; // index
    public string MethodType { get; set; } = default!; // index
    public HttpStatusCode? StatusCode { get; set; } // index
    public string RequestHeader { get; set; } = default!;
    public string? RequestBody { get; set; }
    public string Exception { get; set; } = default!;
    public string TraceIdentifier { get; set; } = default!;// index

}
