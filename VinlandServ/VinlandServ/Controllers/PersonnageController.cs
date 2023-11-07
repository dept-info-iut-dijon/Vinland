using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    [Route("api/personnage")]
    [ApiController]
    public class PersonnageController : ControllerBase
    {
        private readonly Context _context;

        public PersonnageController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnage>>> GetPersonnages()
        {
            return await _context.Personnages.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personnage>> GetPersonnage(int id)
        {
            var personnage = await _context.Personnages.FindAsync(id);

            if (personnage == null)
            {
                return NotFound();
            }

            return personnage;
        }

        [HttpPost]
        public async Task<ActionResult<Personnage>> NewPersonnage(Personnage personnage)
        {
            _context.Personnages.Add(personnage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonnage", new { id = personnage.Id }, personnage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonnage(int id, Personnage personnage)
        {
            if (id != personnage.Id)
            {
                return BadRequest();
            }

            _context.Entry(personnage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnageExists(id))
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
        public async Task<IActionResult> DeletePersonnage(int id)
        {
            var personnage = await _context.Personnages.FindAsync(id);
            if (personnage == null)
            {
                return NotFound();
            }

            _context.Personnages.Remove(personnage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonnageExists(int id)
        {
            return _context.Personnages.Any(e => e.Id == id);
        }
    }
}

