using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Components
{
    public class CardSummaryViewComponent : ViewComponent
    {
        private readonly Card _card;

        public CardSummaryViewComponent(Card cardService)
        {
            _card = cardService;
        }

        public string Invoke()
        {
            return _card.Lines.Count().ToString();
        }
    }
}