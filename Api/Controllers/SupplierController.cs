using Application.Interface;
using Application.Model.DTO;
using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("supplier")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService supplierService;
    private readonly ITransformer<Supplier, SupplierDTO> transformer;

    public SupplierController(ISupplierService supplierService, ITransformer<Supplier, SupplierDTO> transformer)
    {
        this.supplierService = supplierService;
        this.transformer = transformer;
    }

    [Route("listPopular")]
    [HttpGet]
    public async Task<IActionResult> ListPopular(int limit = 3)
    {
        var result = await supplierService.Query(supplier => supplier.OrderByDescending(s => s.Offers.Count), limit);

        var dtos = await Task.WhenAll(result.Select(transformer.Transform));
        return Ok(dtos);
    }
}