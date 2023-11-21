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
        /// Donne la carte correspondante � l'id donn�
        /// </summary>
        /// <param name="id">l'id de la carte demand�e</param>
        /// <returns>La carte demand�e ou une indication que cette derni�re n'a pas �t� trouv�e</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Carte>> GetCarte(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ajoute la carte donn�e au Context 
        /// </summary>
        /// <param name="carte">la nouvelle carte</param>
        /// <returns>La carte nouvellement cr��e (voir GetCarte)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Carte>> NewCarte(Carte carte)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ecrase les donn�es actuelles de la carte avec l'id donn� par la carte donn�e
        /// </summary>
        /// <param name="id">l'id de la carte � modifier</param>
        /// <param name="carte">la nouvelle carte</param>
        /// <returns>Rien ou une indication d'�chec</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCarte(int id, Carte carte)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime la carte avec l'id donn�
        /// </summary>
        /// <param name="id">l'id de la carte � supprimer</param>
        /// <returns>Rien ou une indication que la carte n'a pas �t� trouv�e</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCarte(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indique si la carte avec l'id donn� existe ou non
        /// </summary>
        /// <param name="id">l'id recherch�</param>
        /// <returns>true si trouv�, sinon false</returns>
        private bool CarteExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}