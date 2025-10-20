using MarcasApi.Data;
using MarcasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasAutosController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetAll()
        => await db.MarcasAutos.AsNoTracking().OrderBy(m => m.Id).ToListAsync();
}
