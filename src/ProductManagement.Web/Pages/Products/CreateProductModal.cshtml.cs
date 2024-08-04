using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ProductManagement.Web.Pages.Products
{
    public class CreateProductModalModel : PageModel
    {
        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }
        public SelectListItem[] Categories { get; set; }
        private readonly IProductAppService _productAppService;
        private readonly IMapper _objectMapper;
        public CreateProductModalModel(IProductAppService productAppService, IMapper mapper)
        {
            _productAppService = productAppService;
            _objectMapper = mapper;
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
            await _productAppService.CreateAsync(_objectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));
            return StatusCode(204);
        }
    }
}
