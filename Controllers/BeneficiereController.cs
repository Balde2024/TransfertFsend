using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForSendKH.Models;
using ForSendKH.Contracts;
using AutoMapper;
using ForSendKH.Models.ModelsDto.Beneficiere;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ForSendKH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BeneficiereController : ControllerBase
    {
        private readonly IBeneficiereRepository _beneficiereRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Le controleur pour injection de dependance
        /// </summary>
        /// <param name="IBeneficiereRepository">objet de IAuthManager</param>
        /// <param name="IMapper">objet de ILogger<AccountController></param>
        public BeneficiereController(IBeneficiereRepository beneficiereRepository , IMapper mapper)
        {
            _beneficiereRepository = beneficiereRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Retourne la liste des beneficieres 
        /// </summary>
        /// <returns>Retourne les beneficieres</returns>
        // GET: api/Beneficiere
        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<IEnumerable<GetBeneficiereDto>>> GetBeneficieres()
        {
            
            var beneficiere = await _beneficiereRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GetBeneficiereDto>>(beneficiere));
        }

        /// <summary>
        /// Retourne le beneficiere recherche a traver l'identifiant 
        /// </summary>
        /// <param name="id"> l'identifiant du beneficiere</param>
        /// <returns>Retourne le beneficiere </returns>
        // GET: api/Beneficiere/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<BeneficiereDto>> GetBeneficiere(int id)
        {
            var beneficiere = await _beneficiereRepository.GetDetails(id);

            if (beneficiere == null)
            {
                return NotFound();
            }
           
            return Ok(_mapper.Map<BeneficiereDto>(beneficiere));
        }

        /// <summary>
        /// Mise à jour des information du beneficiere
        /// </summary>
        /// <param name="id"> l'identifiant du beneficiere </param>
        /// <param name="updateBeneficiere"> objet de UpdateBeneficiereDto </param>
        /// <returns> Retourne le status de la reponse </returns>
        // PUT: api/Beneficiere/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<IActionResult> PutBeneficiere(int id, [FromForm] UpdateBeneficiereDto updateBeneficiere)
        {
            if (id != updateBeneficiere.Id)
            {
                return BadRequest();
            }

            var beneficiere = await _beneficiereRepository.GetAsync(id);

            if (beneficiere == null)
            {
                return NotFound();
            }

            _mapper.Map(updateBeneficiere, beneficiere);

            try
            {
                await _beneficiereRepository.UpdateAsync(beneficiere);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BeneficiereExists(id))
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
        /// Ajout d'un beneficiere
        /// </summary>
        /// <param name="createBeneficiere"> objet de CreateBeneficiereDto </param>
        /// <returns>Retourne le beneficiere ajoute </returns>
        // POST: api/Beneficiere
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<Beneficiere>> PostBeneficiere([FromForm] CreateBeneficiereDto createBeneficiere)
        {
            var beneficiere = _mapper.Map<Beneficiere>(createBeneficiere);
            await _beneficiereRepository.AddAsync(beneficiere);

            return CreatedAtAction("GetBeneficiere", new { id = beneficiere.Id }, beneficiere);
        }

        /// <summary>
        /// Supprime le beneficiere
        /// </summary>
        /// <param name="id"> l'identifiant du beneficiere </param>
        /// <returns> Retourne le status de la reponse </returns>
        // DELETE: api/Beneficiere/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> DeleteBeneficiere(int id)
        {
            var beneficiere = await _beneficiereRepository.GetAsync(id);
            if (beneficiere == null)
            {
                return NotFound();
            }

            await _beneficiereRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> BeneficiereExists(int id)
        {
            return await _beneficiereRepository.ExistAsync(id);
        }
    }
}
