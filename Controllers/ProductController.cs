using CartManagementMVC.Models;
using CartManagementMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CartManagementMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetProductList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedBy = 1; // Temporary user ID
                bool result = _productRepository.SaveProduct(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
