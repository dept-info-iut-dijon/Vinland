using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    [Route("api/campagnes")]
    [ApiController]
    public class CampagneController : ControllerBase
    {
        private readonly Context _context;

        public CampagneController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campagne>>> GetCampagnes()
        {
            return await _context.Campagnes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campagne>> GetCampagnes(int id)
        {
            var campagne = await _context.Campagnes.FindAsync(id);

            if (campagne == null)
            {
                return NotFound();
            }

            return campagne;
        }

        [HttpPost]
        public async Task<ActionResult<Campagne>> NewCampagne(Campagne campagne)
        {
            _context.Campagnes.Add(campagne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampagne", new { id = campagne.Id }, campagne);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampagne(int id, Campagne campagne)
        {
            if (id != campagne.Id)
            {
                return BadRequest();
            }

            _context.Entry(campagne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampagneExists(id))
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
        public async Task<IActionResult> DeleteCampagne(int id)
        {
            var campagne = await _context.Campagnes.FindAsync(id);
            if (campagne == null)
            {
                return NotFound();
            }

            _context.Campagnes.Remove(campagne);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampagneExists(int id)
        {
            return _context.Campagnes.Any(e => e.Id == id);
        }
    }
}