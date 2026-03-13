using Autos.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Autos.Api.Controllers;

[ApiController]
[Route("api/marcasautos")]
public class MarcasAutosController(AutosDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetMarcasAutosResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetMarcasAutosResponse>> GetAll(CancellationToken cancellationToken)
    {
        var marcas = await dbContext.MarcasAutos
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .Select(x => new MarcaAutoDto(x.Id, x.Nombre, x.PaisOrigen))
            .ToListAsync(cancellationToken);

        return Ok(new GetMarcasAutosResponse(marcas.Count, marcas));
    }

    public sealed record MarcaAutoDto(int Id, string Nombre, string PaisOrigen);

    public sealed record GetMarcasAutosResponse(int Total, IReadOnlyCollection<MarcaAutoDto> Data);
}
