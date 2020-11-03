using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SportsClub.Checkout
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public string eventName = "Event 1";
        public double amount = 5.00;

        public string Event { get; set; }

        public double Amount { get; set; }
    }
}
