using System.Linq;
using System.Web.Mvc;
using Dblp.Domain;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;
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

            if (!_repo.PublicationExists(dblpKey))
                return;
            var searchResult = _repo.GetPublicationByKeyAsSearchResult(dblpKey);
            if (searchResult != null)
                GetCart().AddItem(searchResult);
        }

        public void DeleteKey(string dblpKey)
        {
            GetCart().RemoveItem(new SearchResult(dblpKey, "", 0, SearchResultSourceType.Conference, ""));
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
            byte[] fileBytes = _bibTeXContentProvider.GetBibTexFileBytes(GetCart().SearchResults,BibTeXContentOptions.None);
            const string fileName = "myfile.bib";
            return File(fileBytes, "text/x-bibtex", fileName);
        }

    }
}