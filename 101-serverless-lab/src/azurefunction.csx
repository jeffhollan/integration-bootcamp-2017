using System.Net;
using System.Text.RegularExpressions;

private static string CustomerNamePattern = "CustomerName:([^\\r\\n]*)";
private static string CustomerEmailPattern = "CustomerEmail:([^\\r\\n]*)";
private static string IssuePattern = "Issue:([^\\r\\n]*)";
private static string DetailsPattern = "Details:([^\\r\\n]*)";

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // Get request body (plain text email)
    string plaintextBody = await req.Content.ReadAsStringAsync();

    //Parse out Ticket
    Ticket customerTicket = new Ticket
    {
        CustomerName = new Regex(CustomerNamePattern).Match(plaintextBody).Groups[1].Value,
        CustomerEmail = new Regex(CustomerEmailPattern).Match(plaintextBody).Groups[1].Value,
        Issue = new Regex(IssuePattern).Match(plaintextBody).Groups[1].Value,
        Details = new Regex(DetailsPattern).Match(plaintextBody).Groups[1].Value
    };

    return req.CreateResponse(HttpStatusCode.OK, customerTicket);
}

internal class Ticket {
    public string CustomerName { get; set;}
    public string CustomerEmail {get; set;}
    public string Issue { get; set;}
    public string Details {get; set;}
}