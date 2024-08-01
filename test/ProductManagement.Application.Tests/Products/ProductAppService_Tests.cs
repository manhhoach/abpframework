using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace ProductManagement.Products
{
    public class ProductAppService_Tests(IServiceProvider serviceProvider) : ProductManagementApplicationTestBase
    {
        private readonly IProductAppService _productAppService = serviceProvider.GetRequiredService<IProductAppService>();
        private readonly IServiceProvider _serviceProvider;

        [Fact]
        public async Task Should_Get_Product_List()
        {
            //Act
            var output = await _productAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
            );
            //Assert
            output.TotalCount.ShouldBe(3);
            output.Items.ShouldContain(
         x => x.Name.Contains("Acme Monochrome Laser Printer"));
        }
    }
}
