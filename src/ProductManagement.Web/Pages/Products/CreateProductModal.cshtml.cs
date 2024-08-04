using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ProductManagement.Web.Pages.Products
{
    public class CreateProductModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }
        public SelectListItem[] Categories { get; set; }
        private readonly IProductAppService _productAppService;

        public CreateProductModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        public async void OnGetAsync()
        {
            var vm = new CreateEditProductViewModel()
            {
                ReleaseDate = DateTime.Now,
                StockState = ProductStockState.PreOrder
            };
            var categories = await _productAppService.GetCategoriesAsync();
            Categories = categories.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.CreateAsync(ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));
            return NoContent();
        }
    }
}
