using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Square;
using Square.Models;
using Square.Apis;
using Square.Exceptions;

namespace SportsClub.Checkout.Pages
{
  public class CheckoutModel : PageModel
  {
    private SquareClient client;
    private readonly string locationId;

    public CheckoutModel( Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      // Get environment
      Square.Environment environment = configuration["AppSettings:Environment"] == "sandbox" ?
        Square.Environment.Sandbox : Square.Environment.Production;

      // Build base client
      client = new SquareClient.Builder()
        .Environment(environment)
        .AccessToken(configuration["AppSettings:AccessToken"])
        .Build();

      locationId = configuration["AppSettings:LocationId"];


    }

    public IActionResult OnPost(string eventName, int quantity, string email, string amount)
    {
      ICheckoutApi checkoutApi = client.CheckoutApi;
      try
      {
        // create line items for the order
        // This example assumes the order information is retrieved and hard coded
        // You can find different ways to retrieve order information and fill in the following lineItems object.
        List<OrderLineItem> lineItems = new List<OrderLineItem>();

        Money firstLineItemBasePriceMoney = new Money.Builder()
          .Amount(System.Convert.ToInt64(amount))
          .Currency("USD")
          .Build();

        OrderLineItem firstLineItem = new OrderLineItem.Builder(quantity.ToString())
          .Name(eventName)
          .BasePriceMoney(firstLineItemBasePriceMoney)
          .Build();

        lineItems.Add(firstLineItem);

        // create Order object with line items
        Order order = new Order.Builder(locationId)
          .LineItems(lineItems)
          .Build();

        // create order request with order
        CreateOrderRequest orderRequest = new CreateOrderRequest.Builder()
          .Order(order)
          .Build();

        // create checkout request with the previously created order
        CreateCheckoutRequest createCheckoutRequest = new CreateCheckoutRequest.Builder(
            Guid.NewGuid().ToString(),
            orderRequest)
          .PrePopulateBuyerEmail(email)
          .RedirectUrl("http://52.229.94.77")
          .Build();
    

        // create checkout response, and redirect to checkout page if successful
        CreateCheckoutResponse response = checkoutApi.CreateCheckout(locationId, createCheckoutRequest);
        return Redirect(response.Checkout.CheckoutPageUrl);
      }
      catch (ApiException e)
      {
        return RedirectToPage("Error", new { error = e.Message});
      }
    }
  }
}
