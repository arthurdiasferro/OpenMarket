using Application.Services;
using Application.ViewModels.Stocks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StocksController(StockService stockService) : ControllerBase
{
    private readonly StockService _stockService = stockService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var stocks = await _stockService.GetAllAsync(cancellationToken);
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var stock = await _stockService.GetByIdAsync(id, cancellationToken);
        if (stock is null) return NotFound();
        return Ok(stock);
    }

    [HttpGet("by-product/{productId}")]
    public async Task<IActionResult> GetByProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        var stock = await _stockService.GetByProductIdAsync(productId, cancellationToken);
        if (stock is null) return NotFound();
        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockViewModel createStockViewModel, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var stock = await _stockService.AddAsync(createStockViewModel, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
    }
}
