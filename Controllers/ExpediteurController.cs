using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForSendKH.Models;
using AutoMapper;
using ForSendKH.Models.ModelsDto.Expediteur;
using ForSendKH.Contracts;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace ForSendKH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExpediteurController : ControllerBase
    {
        private readonly IExpediteurRepository _expediteurRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Le controleur pour injection de dependance
        /// </summary>
        /// <param name="expediteurRepository">objet IExpediteurRepository </param>
        /// <param name="mapper">objet IMapper </param>
        public ExpediteurController(IExpediteurRepository expediteurRepository, IMapper mapper)
        {
            _expediteurRepository = expediteurRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retourne les informations sur les expediteurs.
        /// </summary>
        /// <returns>Liste des expediteurs et leurs informations</returns>
        
        // GET: api/Expediteur/GetAll
        [HttpGet("GetAll")]
        [EnableQuery]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<IEnumerable<GetExpediteurDto>>> GetExpediteurs()
        {
            var expediteurs = await _expediteurRepository.GetAllAsync();
            var records = _mapper.Map<List<GetExpediteurDto>>(expediteurs);
            return Ok(records);
        }

        /// <summary>
        /// Retourne les informations sur les expediteurs par tri.
        /// </summary>
        /// <param name="queryParameters"> objet QueryParameters </param>
        /// <returns>Retourn les informations des expediteurs en fonctions du besoins</returns>

        // GET: api/Expediteur/?StartIndex=0&pageSize=25&PageNumber=1
        [HttpGet]
        [EnableQuery]
        //[Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<PageResult<GetExpediteurDto>>> GetPageExpediteurs([FromQuery] QueryParameters queryParameters)
        {
            var PageExpediteursResult = await _expediteurRepository.GetAllAsync<GetExpediteurDto>(queryParameters);
    
            return Ok(PageExpediteursResult);
        }

        /// <summary>
        /// Retourne les informations d'un expediteur
        /// Accessible aux administrateurs
        /// </summary>
        /// <param name="id">Id de l'expediteur</param>
        /// <returns>Retourne l'expediteur en question</returns>
        // GET: api/Expediteur/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<ExpediteurDto>> GetExpediteur(int id)
        {
            var expediteur = await _expediteurRepository.GetDetails(id);

            if (expediteur == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<ExpediteurDto>(expediteur);

            return Ok(records);
        }

        /// <summary>
        /// Mise à jour d'un expediteur
        /// Accessible aux administrateurs
        /// </summary>
        /// <param name="updateExpediteur">objet expediteur</param>
        /// <param name="id">Id de l'expediteur</param>
        /// <returns>Retourne l'expediteur ayant fait l'objet d'une mise à jour</returns>
        
        // PUT: api/Expediteur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<IActionResult> PutExpediteur(int id, [FromForm] UpdateExpediteurDto updateExpediteur)
        {
            if (id != updateExpediteur.Id)
            {
                return BadRequest();
            }

            /// _context.Entry(expediteur).State = EntityState.Modified;
            var expediteur = await _expediteurRepository.GetAsync(id);

            if (expediteur == null)
            {
                return NotFound();
            }

            _mapper.Map(updateExpediteur,expediteur);

            try
            {
                await _expediteurRepository.UpdateAsync(expediteur);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ExpediteurExists(id))
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
        /// Ajouter un expediteur
        /// </summary>
        /// <param name="createExpediteur">objet expediteur</param>
        /// <returns>Retourne l'expediteur créer </returns>
        // POST: api/Expediteur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<Expediteur>> PostExpediteur([FromForm] CreateExpediteurDto createExpediteur)
        {
            var expediteur = _mapper.Map<Expediteur>(createExpediteur);
            await _expediteurRepository.AddAsync(expediteur);

            return CreatedAtAction("GetExpediteur", new { id = expediteur.Id }, expediteur);
        }

        /// <summary>
        /// Permet de supprimer un expediteur. 
        /// </summary>
        /// <param name="id">represente l'identificant de l'expediteur</param>
        /// <returns>l'état de la suppression</returns>
        // DELETE: api/Expediteur/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> DeleteExpediteur(int id)
        {
            var expediteur = await _expediteurRepository.GetAsync(id);

            if (expediteur == null)
            {
                return NotFound();
            }

            await _expediteurRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ExpediteurExists(int id)
        {
            return await _expediteurRepository.ExistAsync(id);
        }
    }
}
