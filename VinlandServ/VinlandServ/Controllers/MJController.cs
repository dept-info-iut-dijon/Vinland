using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    [Route("api/mjs")]
    [ApiController]
    public class MJController : ControllerBase
    {
        private readonly Context _context;

        public MJController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MJ>>> GetMJs()
        {
            return await _context.MJs.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MJ>> GetMJ(int id)
        {
            var mj = await _context.MJs.FindAsync(id);

            if (mj == null)
            {
                return NotFound();
            }

            return mj;
        }


        [HttpPost]
        public async Task<ActionResult<MJ>> NewMJ(MJ mj)
        {
            _context.MJs.Add(mj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMJ", new { id = mj.Id }, mj);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMJ(int id, MJ mj)
        {
            if (id != mj.Id)
            {
                return BadRequest();
            }

            _context.Entry(mj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MJExists(id))
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
        public async Task<IActionResult> DeleteMJ(int id)
        {
            var mj = await _context.MJs.FindAsync(id);
            if (mj == null)
            {
                return NotFound();
            }

            _context.MJs.Remove(mj);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MJExists(int id)
        {
            return _context.MJs.Any(e => e.Id == id);
        }
    }
}
