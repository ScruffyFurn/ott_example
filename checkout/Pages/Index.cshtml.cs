using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Square;
using Square.Models;
using Square.Apis;
using Square.Exceptions;

namespace SportsClub.Checkout
{
    public class IndexModel : PageModel
    {
        private SquareClient client;
        private readonly string locationId;

        public IList<CatalogObject> events;

        public IndexModel(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            // Get environment
            Square.Environment environment = configuration["Environment"] == "sandbox" ?
              Square.Environment.Sandbox : Square.Environment.Production;

            // Build base client
            client = new SquareClient.Builder()
              .Environment(environment)
              .AccessToken(configuration["AccessToken"])
              .Build();

            locationId = configuration["LocationId"];
        }

        public void OnGet()
        {
            ICatalogApi catalogApi = client.CatalogApi;
            try
            {
                ListCatalogResponse catalog = catalogApi.ListCatalog();
                events = catalog.Objects;
            }
            catch (ApiException e)
            {
                RedirectToPage("Error", new { error = e.Message });
            }
        }
    }
}
