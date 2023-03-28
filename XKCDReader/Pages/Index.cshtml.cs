using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

using XKCDReader.Models;

namespace XKCDReader.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public string DateDisplay { get; set; }
    [BindProperty]
    public string Title { get; set; }
    [BindProperty]
    public string URL { get; set; }
    [BindProperty]
    public string NextURL { get; set; }
    [BindProperty]
    public string PrevURL { get; set; }

    public async Task<IActionResult> OnGet(int id = 614)
    {
        var baseURL = "https://xkcd.com";

        var client = new RestClient();
        var request = new RestRequest($"{baseURL}/{id.ToString()}/info.0.json", Method.Get);
        RestResponse response = await client.ExecuteAsync(request);

        if (!(response.IsSuccessful) || (response.Content == null))
        {
            return NotFound();
        }

        //Deserialize response content
        var xKCD = JsonConvert.DeserializeObject<XKCD>(response.Content);
        ParseXKCDResult(xKCD);
        id = SetButtonRedirects(id);

        return Page();

    }

    private int SetButtonRedirects(int id)
    {
        string thisURL = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host.Value;
        NextURL = thisURL + $"?id={(id+1).ToString()}";
        PrevURL = thisURL + $"?id={(id-1).ToString()}";
        return id;
    }

    private void ParseXKCDResult(XKCD? xKCD)
    {
        DateDisplay = $"{xKCD?.Month.ToString()} - {xKCD?.Day.ToString()} - {xKCD?.Year.ToString()} ";
        Title = (xKCD != null) ? xKCD.Title : "";
        URL = (xKCD != null) ? xKCD.Img.ToString() : "";
    }
}

