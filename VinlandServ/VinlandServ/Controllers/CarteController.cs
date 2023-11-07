using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    [Route("api/cartes")]
    [ApiController]
    public class CarteController : ControllerBase
    {
        private readonly Context _context;

        public CarteController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carte>>> GetCartes()
        {
            return await _context.Cartes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carte>> GetCarte(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);

            if (carte == null)
            {
                return NotFound();
            }

            return carte;
        }

        [HttpPost]
        public async Task<ActionResult<Carte>> NewCarte(Carte carte)
        {
            _context.Cartes.Add(carte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarte", new { id = carte.Id }, carte);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarte(int id, Carte carte)
        {
            if (id != carte.Id)
            {
                return BadRequest();
            }

            _context.Entry(carte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarte(int id)
        {
            var carte = await _context.Cartes.FindAsync(id);
            if (carte == null)
            {
                return NotFound();
            }

            _context.Cartes.Remove(carte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarteExists(int id)
        {
            return _context.Cartes.Any(e => e.Id == id);
        }
    }
}

