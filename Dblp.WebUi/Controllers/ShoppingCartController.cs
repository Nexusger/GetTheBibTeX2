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

        public void AddKey(string dblpKey)
        {



            //var searchResult = _repo.Conferences.FirstOrDefault(sr => sr.Key == dblpKey);
            //if (searchResult != null)
            //{
            //    searchResult = _repo.Conferences.Where(t=>t.SubConferences.Where(c=>c.PublicationKeys==dblpKey).Any())
            //}
            //if (searchResult != null)
            //{
                GetCart().AddItem(new SearchResult(dblpKey,"",0,SearchResultSourceType.Conference, ""));
            //}
        }

        public void DeleteKey(string dblpKey)
        {
                GetCart().RemoveItem(new SearchResult(dblpKey, "", 0, SearchResultSourceType.Conference, ""));
            //var searchResult = _repo.Conferences.FirstOrDefault(sr => sr.Key == dblpKey);
            //if (searchResult != null)
            //{
            //}
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
        {
            var cartIndexViewModel = new CartIndexViewModel
            {
                Cart = GetCart(), 
                ReturnUrl = returnUrl
            };
            return View(cartIndexViewModel);
        }

        public FileResult Download()
        {
            byte[] fileBytes = _bibTeXContentProvider.GetBibTexFileBytes(BibTeXContentOptions.None);
            const string fileName = "myfile.bib";
            return File(fileBytes, "text/x-bibtex", fileName);
        }

    }
}