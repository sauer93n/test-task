using System.Linq.Expressions;
using Application.Interface;
using Application.Model;
using Application.Model.DTO;
using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("offer")]
public class OfferController(
    IOfferService offerService,
    ITransformer<Offer, OfferDTO> offerTransformer) : ControllerBase
{
    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Create(OfferDTO offerData) =>
        Ok(await offerService.Create(await offerTransformer.Transform(offerData)));

    [Route("search")]
    [HttpPost]
    public async Task<IActionResult> Search(OfferDTO searchParams)
    {
        Expression<Func<Offer, bool>> predicate = o =>
            (string.IsNullOrEmpty(searchParams.Brand) || o.Brand == searchParams.Brand)
            && (string.IsNullOrEmpty(searchParams.Model) || o.Model == searchParams.Model)
            && (searchParams.SupplierId == null || o.SupplierId == searchParams.SupplierId)
            && (searchParams.RegisterAt == null || o.RegistrationDate.Date == searchParams.RegisterAt);

        var offers = await offerService.List(predicate);
        return Ok(new SearchResult<Offer>(offers.ToList()));
    }
}