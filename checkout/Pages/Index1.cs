using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SportsClub.Checkout
{
  public class Index1 : PageModel
  {

    public string posterURL;

        public void OnGet()
        {
          //posterURL = string.Format("http://{0}/images/choir.png", HttpContext.Request.Host.Value);
        }

  }
}