using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VinlandServ.Controllers
{
    /// <summary>
    /// Controller de Personnage - Lien avec l'API
    /// </summary>
    [Route("api/personnage")]
    [ApiController]
    public class PersonnageController : ControllerBase
    {
        private readonly Context _context;

        public PersonnageController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Donne une liste de tout les personnages
        /// </summary>
        /// <returns>Une liste des personnages</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Personnage>>> GetPersonnages()
        {
            return Ok(await _context.Personnages.ToListAsync());
        }

        /// <summary>
        /// Donne le personnage correspondant à l'id donné
        /// </summary>
        /// <param name="id">l'id du personnage demandé</param>
        /// <returns>Le personnage demandé ou une indication que ce dernier n'a pas été trouvé</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Personnage>> GetPersonnage(int id)
        {
            var personnage = await _context.Personnages.FindAsync(id);

            if (personnage == null)
            {
                return NotFound();
            }

            return Ok(personnage);
        }

        /// <summary>
        /// Ajoute le personnage donné au Context
        /// </summary>
        /// <param name="personnage">le nouveau personnage</param>
        /// <returns>Le personnage nouvellement créé (voir GetPersonnage)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Personnage>> NewPersonnage(Personnage personnage)
        {
            _context.Personnages.Add(personnage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonnage", new { id = personnage.Id }, personnage);
        }

        /// <summary>
        /// Ecrase les données actuelles du personnage avec l'id donné par le personnage donné
        /// </summary>
        /// <param name="id">l'id du personnage à modifier</param>
        /// <param name="personnage">le nouveau personnage</param>
        /// <returns>Rien ou une indication d'échec</returns>
        [HttpPut("update/{id}")]
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

        /// <summary>
        /// Supprime le personnage avec l'id donné
        /// </summary>
        /// <param name="id">l'id du personnage à supprimer</param>
        /// <returns>Rien ou une indication que le personnage n'a pas été trouvé</returns>
        [HttpDelete("delete/{id}")]
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

        /// <summary>
        /// Indique si le personnage avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool PersonnageExists(int id)
        {
            return _context.Personnages.Any(e => e.Id == id);
        }
    }
}