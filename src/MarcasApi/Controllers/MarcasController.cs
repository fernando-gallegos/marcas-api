using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarcasApi.Data;
using MarcasApi.Models;

namespace MarcasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly MarcasDbContext _context;
    private readonly ILogger<MarcasController> _logger;

    public MarcasController(MarcasDbContext context, ILogger<MarcasController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
    {
        return await _context.Marcas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Marca>> GetMarca(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);

        if (marca == null)
        {
            return NotFound();
        }

        return marca;
    }

    [HttpPost]
    public async Task<ActionResult<Marca>> PostMarca(Marca marca)
    {
        marca.FechaCreacion = DateTime.UtcNow;
        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMarca), new { id = marca.Id }, marca);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMarca(int id, Marca marca)
    {
        if (id != marca.Id)
        {
            return BadRequest();
        }

        _context.Entry(marca).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MarcaExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarca(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null)
        {
            return NotFound();
        }

        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> MarcaExists(int id)
    {
        return await _context.Marcas.AnyAsync(e => e.Id == id);
    }
}
