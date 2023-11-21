using Microsoft.AspNetCore.Mvc;


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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Donne le personnage correspondant à l'id donné
        /// </summary>
        /// <param name="id">l'id du personnage demandé</param>
        /// <returns>Le personnage demandé ou une indication que ce dernier n'a pas été trouvé</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Personnage>> GetPersonnage(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ajoute le personnage donné au Context
        /// </summary>
        /// <param name="personnage">le nouveau personnage</param>
        /// <returns>Le personnage nouvellement créé (voir GetPersonnage)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<Personnage>> NewPersonnage(Personnage personnage)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime le personnage avec l'id donné
        /// </summary>
        /// <param name="id">l'id du personnage à supprimer</param>
        /// <returns>Rien ou une indication que le personnage n'a pas été trouvé</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePersonnage(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indique si le personnage avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool PersonnageExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}