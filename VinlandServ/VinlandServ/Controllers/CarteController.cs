using Microsoft.AspNetCore.Mvc;

namespace VinlandServ.Controllers
{
    /// <summary>
    /// Controller de Carte - Lien avec l'API
    /// </summary>
    [Route("api/cartes")]
    [ApiController]
    public class CarteController : ControllerBase
    {
        private readonly Context _context;

        public CarteController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Donne une liste de toutes les cartes
        /// </summary>
        /// <returns>Une liste des cartes</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Carte>>> GetCartes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Donne la carte correspondante à l'id donné
        /// </summary>
        /// <param name="id">l'id de la carte demandée</param>
        /// <returns>La carte demandée ou une indication que cette dernière n'a pas été trouvée</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Carte>> GetCarte(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ajoute la carte donnée au Context 
        /// </summary>
        /// <param name="carte">la nouvelle carte</param>
        /// <returns>La carte nouvellement créée (voir GetCarte)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Carte>> NewCarte(Carte carte)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ecrase les données actuelles de la carte avec l'id donné par la carte donnée
        /// </summary>
        /// <param name="id">l'id de la carte à modifier</param>
        /// <param name="carte">la nouvelle carte</param>
        /// <returns>Rien ou une indication d'échec</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCarte(int id, Carte carte)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime la carte avec l'id donné
        /// </summary>
        /// <param name="id">l'id de la carte à supprimer</param>
        /// <returns>Rien ou une indication que la carte n'a pas été trouvée</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCarte(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indique si la carte avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool CarteExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}