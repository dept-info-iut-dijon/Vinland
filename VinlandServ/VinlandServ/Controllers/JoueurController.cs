using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    [Route("api/joueurs")]
    [ApiController]
    public class JoueurController : ControllerBase
    {
        private readonly Context _context;

        public JoueurController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Joueur>>> GetJoueurs()
        {
            return await _context.Joueurs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Joueur>> GetJoueur(int id)
        {
            var joueur = await _context.Joueurs.FindAsync(id);

            if (joueur == null)
            {
                return NotFound();
            }

            return joueur;
        }

        [HttpPost]
        public async Task<ActionResult<Joueur>> NewJoueur(Joueur joueur)
        {
            _context.Joueurs.Add(joueur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJoueur", new { id = joueur.Id }, joueur);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJoueur(int id, Joueur joueur)
        {
            if (id != joueur.Id)
            {
                return BadRequest();
            }

            _context.Entry(joueur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoueurExists(id))
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
        public async Task<IActionResult> DeleteJoueur(int id)
        {
            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }

            _context.Joueurs.Remove(joueur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JoueurExists(int id)
        {
            return _context.Joueurs.Any(e => e.Id == id);
        }
    }
}

