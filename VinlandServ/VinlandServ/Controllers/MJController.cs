using Microsoft.AspNetCore.Mvc;


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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Donne le mj correspondant à l'id donné
        /// </summary>
        /// <param name="id">l'id du mj demandé</param>
        /// <returns>Le mj demandé ou une indication que ce dernier n'a pas été trouvé</returns>
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<MJ>> GetMJ(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ajoute le mj donné au Context
        /// </summary>
        /// <param name="mj">le nouveau mj</param>
        /// <returns>Le mj nouvellement créé (voir GetMJ)</returns>
        [HttpPost("new")]
        public async Task<ActionResult<MJ>> NewMJ(MJ mj)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime le mj avec l'id donné
        /// </summary>
        /// <param name="id">l'id du mj à supprimer</param>
        /// <returns>Rien ou une indication que le mj n'a pas été trouvé</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMJ(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indique si je mj avec l'id donné existe ou non
        /// </summary>
        /// <param name="id">l'id recherché</param>
        /// <returns>true si trouvé, sinon false</returns>
        private bool MJExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}