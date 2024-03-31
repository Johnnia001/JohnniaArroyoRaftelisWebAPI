using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ParcelsController : ControllerBase
{
    private readonly IParcelService _parcelService;

    public ParcelsController(IParcelService parcelService)
    {
        _parcelService = parcelService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Parcel>> GetParcels()
    {
        var parcels = _parcelService.GetParcels();
        return Ok(parcels);
    }

    [HttpGet("sortedByStreet")]
    public ActionResult<IEnumerable<Parcel>> GetParcelsSortedByStreet()
    {
        var parcels = _parcelService.GetParcelsSortedByStreet();
        return Ok(parcels);
    }

    [HttpGet("sortedByFirstName")]
    public ActionResult<IEnumerable<Parcel>> GetParcelsSortedByFirstName()
    {
        var parcels = _parcelService.GetParcelsSortedByFirstName();
        return Ok(parcels);
    }
}
