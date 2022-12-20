using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using WorldCup.Data.Services;
using WorldCup.Migrations;
using WorldCup.Models;
using X.PagedList;

namespace WorldCup.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductsService _productsService;
	
		public ProductsController(IProductsService productsService)
		{
			_productsService= productsService;
		}
		public async Task<IActionResult> Index(int? page)
		{
		

			var allProducts = await _productsService.GetAllAsync(page);
			return View(allProducts);
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
		public async Task<IActionResult> Edit(int id,Product product)
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
			if(!string.IsNullOrEmpty(searchString))
			{
				var filterResults = allProducts.Where(n=>n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();
				
				return View("Filtered", filterResults);
			}
			return View("Filtered", allProducts);
		}
		
	}
}
