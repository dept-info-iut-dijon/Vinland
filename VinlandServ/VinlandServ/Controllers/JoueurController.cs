using Microsoft.AspNetCore.Mvc;


namespace VinlandServ.Controllers
{
    /// <summary>
    /// Controller de Joueur - Lien avec l'API
    /// </summary>
    [Route("api/joueurs")]
    [ApiController]
    public class JoueurController : ControllerBase
    {
        private readonly Context _context;

        public JoueurController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Donne une liste de tout les joueurs
        /// </summary>
        /// <returns>Une liste des joueurs</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Joueur>>> GetJoueurs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Donne le joueur correspondant à l'id donné
        /// </summary>
        /// <param name="id">l'id du joueur demandé</param>
        /// <returns>Le joueur demandé ou une indication que ce dernier n'a pas été trouvé</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Joueur>> GetJoueur(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ajoute le joueur donné au Context
        /// </summary>
        /// <param name="joueur">le nouveau joueur</param>
        /// <returns>Le joueur nouvellement créé (voir GetJoueur)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Joueur>> NewJoueur(Joueur joueur)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ecrase les données actuelles du joueur avec l'id donné par le joueur donné
        /// </summary>
        /// <param name="id">l'id du joueur à modifier</param>
        /// <param name="joueur">le nouveau joueur</param>
        /// <returns>Rien ou une indication d'échec</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJoueur(int id, Joueur joueur)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime le joueur avec l'id donné
        /// </summary>
        /// <param name="id">l'id du joueur à supprimer</param>
        /// <returns>Rien ou une indication que le joueur n'a pas été trouvé</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJoueur(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indique si le joueur avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool JoueurExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}