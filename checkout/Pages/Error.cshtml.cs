using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SportsClub.Checkout.Pages
{
  public class ErrorModel : PageModel
  {
    public string ErrorMessage { get; set; }

    public void OnGet(string error)
    {
      ErrorMessage = error;
    }
  }
}
