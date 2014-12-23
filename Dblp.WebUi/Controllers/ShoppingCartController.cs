using System.Linq;
using System.Web.Mvc;
using Dblp.Domain.Abstract;
using Dblp.Domain.Entities;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IDblpRepository _repo;
        private IBibTeXContentProvider _bibTeXContentProvider;

        public ShoppingCartController(IDblpRepository repo, IBibTeXContentProvider bibTeXContentProvider)
        {
            _repo = repo;
            _bibTeXContentProvider = bibTeXContentProvider;
        }

        [HttpPost]
        public RedirectToRouteResult AddItem(string dblpKey, string returnUrl)
        {
            var searchResult = _repo.SearchResults.FirstOrDefault(sr => sr.Key == dblpKey);
            if (searchResult != null)
            {
                GetCart().AddItem(searchResult);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(string dblpKey, string returnUrl)
        {
            var searchResult = _repo.SearchResults.FirstOrDefault(sr => sr.Key == dblpKey);
            if (searchResult != null)
            {
                GetCart().RemoveItem(searchResult);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private ShoppingCart GetCart()
        {
            var cart = (ShoppingCart)Session["ShoppingCart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["ShoppingCart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        { return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl }); }

        public FileResult Download()
        {
            byte[] fileBytes = _bibTeXContentProvider.GetBibTexFileBytes(BibTeXContentOptions.None);
            string fileName = "myfile.bib";
            return File(fileBytes, "text/x-bibtex", fileName);
        }

    }
}