using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    /// <summary>
    /// Controller de MJ - Lien avec l'API
    /// </summary>
    [Route("api/mjs")]
    [ApiController]
    public class MJController : ControllerBase
    {
        private readonly Context _context;

        public MJController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Donne une liste de tout les mjs
        /// </summary>
        /// <returns>Une liste des mjs</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<MJ>>> GetMJs()
        {
            return Ok(await _context.MJs.ToListAsync());
        }

        /// <summary>
        /// Donne le mj correspondant à l'id donné
        /// </summary>
        /// <param name="id">l'id du mj demandé</param>
        /// <returns>Le mj demandé ou une indication que ce dernier n'a pas été trouvé</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<MJ>> GetMJ(int id)
        {
            var mj = await _context.MJs.FindAsync(id);

            if (mj == null)
            {
                return NotFound();
            }

            return Ok(mj);
        }

        /// <summary>
        /// Ajoute le mj donné au Context
        /// </summary>
        /// <param name="mj">le nouveau mj</param>
        /// <returns>Le mj nouvellement créé (voir GetMJ)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<MJ>> NewMJ(MJ mj)
        {
            _context.MJs.Add(mj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMJ", new { id = mj.Id }, mj);
        }

        /// <summary>
        /// Ecrase les données actuelles du mj avec l'id donné par le mj donné
        /// </summary>
        /// <param name="id">l'id du mj à modifier</param>
        /// <param name="mj">le nouveau mj</param>
        /// <returns>Rien ou une indication d'échec</returns>
        [HttpPut("update/{id}")]
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

        /// <summary>
        /// Supprime le mj avec l'id donné
        /// </summary>
        /// <param name="id">l'id du mj à supprimer</param>
        /// <returns>Rien ou une indication que le mj n'a pas été trouvé</returns>
        [HttpDelete("delete/{id}")]
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

        /// <summary>
        /// Indique si je mj avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool MJExists(int id)
        {
            return _context.MJs.Any(e => e.Id == id);
        }
    }
}