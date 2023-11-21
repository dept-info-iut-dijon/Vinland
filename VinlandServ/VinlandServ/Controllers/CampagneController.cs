using Microsoft.AspNetCore.Mvc;

namespace VinlandServ.Controllers
{
    /// <summary>
    /// Controller de Campagne - Lien avec l'API
    /// </summary>
    [Route("api/campagnes")]
    [ApiController]
    public class CampagneController : ControllerBase
    {
        private readonly Context _context;

        public CampagneController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Donne une liste de toutes les campagnes 
        /// </summary>
        /// <returns>Une liste des campagnes</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Campagne>>> GetCampagnes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Donne la campagne correspondante à l'id donné
        /// </summary>
        /// <param name="id">l'id de la campagne demandée</param>
        /// <returns>La campagne demandée ou une indication que cette dernière n'a pas été trouvée</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Campagne>> GetCampagne(int id)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Ajoute la campagne donnée au Context 
        /// </summary>
        /// <param name="campagne">la nouvelle campagne</param>
        /// <returns>La campagne nouvellement créée (voir GetCampagne)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Campagne>> NewCampagne(Campagne campagne)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Ecrase les données actuelles de la campagne avec l'id donné par la campagne donnée
        /// </summary>
        /// <param name="id">l'id de la campagne à modifier</param>
        /// <param name="campagne">la nouvelle campagne</param>
        /// <returns>Rien ou une indication d'échec</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCampagne(int id, Campagne campagne)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime la campagne avec l'id donné
        /// </summary>
        /// <param name="id">l'id de la campagne à supprimer</param>
        /// <returns>Rien ou une indication que la campagne n'a pas été trouvée</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCampagne(int id)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Indique si la campagne avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool CampagneExists(int id)
        {
            return _context.Campagnes.Any(e => e.Id == id);
        }
    }
}