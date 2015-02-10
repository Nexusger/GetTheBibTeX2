using Dblp.Domain.Interfaces.Entities;

namespace Dblp.WebUi.Models
{
    public class CartIndexViewModel
    {
        public ShoppingCart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}