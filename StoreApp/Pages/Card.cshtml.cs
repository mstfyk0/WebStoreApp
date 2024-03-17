using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages
{
    public class CardModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Card Card { get; set; } //IOC

        // private readonly Card _card;

        public CardModel(IServiceManager manager , Card cardService)
        {
            _manager = manager;
            Card = cardService;
        }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            // Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
        }
        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId, false);

            if (product is not null)
            {
                // Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
                Card.AddItem(product, 1);
                // HttpContext.Session.SetJson<Card>("card", Card);
            }
            return RedirectToPage( new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int id, string returnUrl)
        {
            // Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
            Card.RemoveLine(Card.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
            // HttpContext.Session.SetJson<Card>("card", Card);

            return Page();

        }
    }
}