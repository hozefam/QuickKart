using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKart.ProductService.Models;
using QuickKart.ProductService.Repository;

namespace QuickKart.ProductService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository productRepository;

		public ProductController(IProductRepository productRepository)
        {
			this.productRepository = productRepository;
		}

		[HttpGet]
		[Route("GetProductsByCategory")]
		public ActionResult<Task<List<Products>>> GetProductsByCategory([FromQuery] int categoryId)
		{
			try
			{
				var response = this.productRepository.GetProductsByCategory(categoryId);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(null);
			}
		}

		[HttpGet]
		[Route("GetProductsByName")]
		public ActionResult<Task<List<Products>>> GetProductsByName([FromQuery] string productName)
		{
			try
			{
				var response = this.productRepository.GetProductsByName(productName);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(null);
			}
		}

		[HttpGet]
		[Route("GetProductQuantity")]
		public ActionResult<Task<int>> GetProductQuantity([FromQuery] string productId)
		{
			try
			{
				var response = this.productRepository.GetProductQuantity(productId);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(-99);
			}
		}

		[HttpPost]
		[Route("UpdateProductQuantity")]
		public ActionResult<Task<bool>> UpdateProductQuantity([FromQuery] string productId, [FromQuery] int quantitytoReduce)
		{
			try
			{
				var response = this.productRepository.UpdateProductQuantity(productId, quantitytoReduce);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(false);
			}
		}

	}
}
