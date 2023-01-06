using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using WorldCup.Data.Enums;
using WorldCup.Data.Services;
//using WorldCup.Migrations;
using WorldCup.Models;
using X.PagedList;

namespace WorldCup.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductsService _productsService;

		public ProductsController(IProductsService productsService)
		{
			_productsService = productsService;
		}
		public async Task<IActionResult> Index(int? page)
		{


			var allProducts = await _productsService.GetAllAsync(page);
			return View(allProducts);
		}
		public async Task<IActionResult> Sorting(string sortOrder, string searchString)
		{
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
			ViewBag.ProductTypeSortParam = sortOrder == "ProductType" ? "Product_type" : "ProductType";
			ViewBag.PriceSortParam = sortOrder == "Price" ? "Price_type" : "Price";
			var Products = await _productsService.GetAllAsync();
			if (!String.IsNullOrEmpty(searchString))
			{
				Products = Products.Where(p => p.Name.Contains(searchString));

			}
			switch (sortOrder)
			{
				case "Name":
					Products = Products.OrderBy(s => s.Name);
					break;
				case "ProductType":
					Products = Products.OrderBy(s => s.ProductType);
					break;
				case "Product_type":
					Products = Products.OrderByDescending(s => s.ProductType);
					break;
				case "Price":
					Products = Products.OrderByDescending(s => s.Price);
					break;
				case "Price_type":
					Products = Products.OrderBy(s => s.Price);
					break;
				default:
					Products = Products.OrderByDescending(s => s.Name);
					break;
			}


			return View(Products.ToList());
		}


		//get
		public IActionResult Create()
		{
			return View();
		}
		//Get :products/Create

		[HttpPost]
		public async Task<IActionResult> Create(Product products)
		{

			if (!ModelState.IsValid)
			{
				return View(products);
			}

			await _productsService.AddAsync(products);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var productDetails = await _productsService.GetByIdAsync(id);
			if (productDetails == null) return View("NotFound");
			return View(productDetails);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(int id, Product product)
		{

			if (!ModelState.IsValid)
			{
				return View(product);
			}
			await _productsService.UpdateAsync(id, product);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			var productDetails = await _productsService.GetByIdAsync(id);
			return View(productDetails);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var productDetails = await _productsService.GetByIdAsync(id);
			if (productDetails == null) return View("NotFound");
			return View(productDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> Deleteconfirmed(int id)
		{
			var productDetails = await _productsService.GetByIdAsync(id);
			if (productDetails == null) return View("NotFound");

			await _productsService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Filter(string searchString)
		{

			var allProducts = await _productsService.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = allProducts.Where(n => n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();

				return View("Filtered", filterResults);
			}
			return View("Filtered", allProducts);
		}
		public async Task<IActionResult> FilterStateProducts(int searchString)
		{


			var allProducts = await _productsService.GetAllAsync();

			/*if (!string.IsNullOrEmpty(searchString))*/
			if (searchString != null)
			{
				/*var filterResults = allProducts.Where(n => n.State.ToString().Equals(searchString)).ToList();*/
				var filterResults = allProducts.Where(n => n.State.Equals(searchString)).ToList();

				return View("Filtered", filterResults);
			}
			return View("Filtered", allProducts);
		}

		//states sorted dropdown
		public async Task<IActionResult> Argentina()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Argentina;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}
		public async Task<IActionResult> Australia()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Australia;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Belgium()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Belgium;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}


		public async Task<IActionResult> Brazil()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Brazil;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Cameroon()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Cameroon;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Canada()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Canada;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Costa_Rica()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Costa_Rica;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Croatia()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Croatia;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Denmark()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Denmark;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}
		public async Task<IActionResult> Ecuador()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Ecuador;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}
		public async Task<IActionResult> England()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.England;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}
		public async Task<IActionResult> France()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.France;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}

		public async Task<IActionResult> Portugal()
		{
			var allProducts = await _productsService.GetAllAsync();
			State s = State.Portugal;
			var filterResults = allProducts.Where(n => n.State.Equals(s)).ToList();
			return View("Filtered", filterResults);
		}



	}
}
