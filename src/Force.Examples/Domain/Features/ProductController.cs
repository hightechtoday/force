using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Examples.AspNetCore;
using Force.OperationContext;
using Microsoft.AspNetCore.Mvc;

namespace Force.Examples.Domain.Features
{
    public class ProductController: ApiControllerBase
    {
        [HttpPost]
        public IActionResult Create(CreateProduct command) => 
            this.Process(command);
        
        [HttpPost("CreateProductAsync")]
        public async Task<IActionResult> CreateAsync(CreateProductAsync command) => 
            await this.ProcessAsync(command);
        
        [HttpGet("{id}")]
        public ActionResult<ProductDetails> Get(GetProductDetailsById query) => 
            this.Process(query);

        [HttpGet]
        public IActionResult Get([FromQuery] GetProductList query) => 
            this.Process(query);
        
        [HttpGet("GetAsync")]
        public async Task<ActionResult<IEnumerable<ProductListItem>>> GetAsync([FromQuery] GetProductListAsync query) => 
            await this.ProcessAsync(query);
        
        [HttpPut("{id}")]
        public IActionResult Update(UpdateProduct command) => 
            this.Process(command);

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(
            [FromServices] IAsyncOperationContextFactory<UpdateProductAsync, UpdateProductAsyncContext> factory)
        {
            var ctx = await factory.BuildAsync(new UpdateProductAsync());
            return await this.ProcessAsync(ctx);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(DeleteProduct command) => this.Process(command);
        
        [HttpPost("MassIncreasePrice")]
        public IActionResult MassIncreasePrice(MassIncreasePrice command) => this.Process(command);
    }
}