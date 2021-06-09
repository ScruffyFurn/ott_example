using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SportsClub.Checkout
{
    public class VideoPlayerModel : PageModel
    {

        public string posterURL;
        public string backgroundURL;
        public string eventName;

        public VideoPlayerModel(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            posterURL = configuration["PosterURL"]; 
            backgroundURL = configuration["BackgroundURL"];
        }

        public void OnGet()
        {
            eventName = Request.Query["event"];
        }

    }
}