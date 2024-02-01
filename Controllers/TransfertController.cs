using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForSendKH.Models;
using ForSendKH.Contracts;
using AutoMapper;
using ForSendKH.Models.ModelsDto.Transfert;
using Microsoft.AspNetCore.Authorization;
using ForSendKH.Exceptions;
using Microsoft.AspNetCore.OData.Query;

namespace ForSendKH.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json", "application/xml", Type = typeof(List<string>))]
    public class TransfertController : ControllerBase
    {
        private readonly ITransfertRepository _transfertRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Le controleur pour injection de dependance
        /// </summary>
        /// <param name="transfertRepository">objet ITransfertRepository</param>
        /// <param name="mapper">objet IMapper </param>
        public TransfertController(ITransfertRepository transfertRepository , IMapper mapper)
        {
            _transfertRepository = transfertRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste des transfert 
        /// </summary>
        /// <returns>Retourne les transfert</returns>
        // GET: api/Transfert
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<IEnumerable<TransfertDto>>> GetTransferts()
        {
            var transfert = await _transfertRepository.GetAllAsync();

            var records = _mapper.Map<List<GetTransfertDto>>(transfert);
            return Ok(records);
        }

        /// <summary>
        /// Retourne le transfert recherché a traver l'identifiant 
        /// </summary>
        /// <param name="id"> l'identifiant du transfert</param>
        /// <returns>Retourne le transfert </returns>
        // GET: api/Transfert/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrateur,User")]
        public async Task<ActionResult<TransfertDto>> GetTransfert(int id)
        {
            var transfert = await _transfertRepository.GetAsync(id);

            if (transfert == null)
            {
                throw new NotFoundException(nameof(GetTransfert), id);
            }

            var records = _mapper.Map<GetTransfertDto>(transfert);

            return Ok(records);
        }

        /// <summary>
        /// Recuperer des information du transfert
        /// </summary>
        /// <param name="codeTransfert"> le code du transfert </param>
        /// <returns> Retourne le status de la reponse et les informations </returns>
        // GET: api/Transfert/Balde3667
        [HttpGet("GetInformation{codeTransfert}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<TransfertDto>> GetInformationTransfert(string codeTransfert)
        {
            var transfertResult = await _transfertRepository.GetFerifyCode(codeTransfert);

            if (transfertResult == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<GetTransfertDto>(transfertResult);

            return Ok(records);
        }

        /// <summary>
        /// Mise à jour des information du transfert
        /// </summary>
        /// <param name="id"> l'identifiant du transfert </param>
        /// <param name="updateTransfert"> objet de UpdateTransfertDto </param>
        /// <returns> Retourne le status de la reponse </returns>
        // PUT: api/Transfert/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> PutTransfert(int id, [FromForm] UpdateTransfertDto updateTransfert)
        {
            if (id != updateTransfert.Id)
            {
                return BadRequest();
            }

            var transfert = await _transfertRepository.GetAsync(id);

            if (transfert == null)
            {
                throw new NotFoundException(nameof(GetTransferts), id);
            }

            _mapper.Map(updateTransfert, transfert);
            _transfertRepository.InitializeData(transfert);

            try
            {
                await _transfertRepository.UpdateAsync(transfert);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransfertExists(id))
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
        /// Permet de faire le retrait du transfert et une mise à jour des information du transfert est effectué
        /// </summary>
        /// <param name="id"> l'identifiant du transfert </param>
        /// <param name="executeTransact"> objet de ExecuteTransactDto </param>
        /// <returns> Retourne le status de la reponse </returns>

        // PUT: api/Transfert/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Executer{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> ExecuterTransfert(int id,[FromForm] ExecuteTransactDto executeTransact)
        {
            var result = await _transfertRepository.GetFerifyCode(executeTransact.CodeTransfert);

            if(result == null)
            {
                return NotFound("Le code utilisé n'existe pas");
            }

            var recup = await _transfertRepository.GetVerifyEtat(executeTransact.CodeTransfert);

            if (recup != null)
            {
                return NotFound("Cette operation ne peut être traité , code déjà utilisé ");
            }

            var transfert = await _transfertRepository.GetAsync(id);

            if (transfert == null)
            {
                throw new NotFoundException(nameof(ExecuterTransfert), id);
            }

            _transfertRepository.Init(transfert);

            _mapper.Map(transfert,executeTransact);

            try
            {
                await _transfertRepository.UpdateAsync(transfert);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransfertExists(executeTransact.Id))
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
        /// Ajout d'un transfert
        /// </summary>
        /// <param name="createTransfert"> objet de CreateTransfertDto </param>
        /// <returns>Retourne le transfert ajoute </returns>
        // POST: api/Transfert
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Transfert>> PostTransfert([FromForm] CreateTransfertDto createTransfert)
        {
            var transfert = _mapper.Map<Transfert>(createTransfert);
            _transfertRepository.InitializeData(transfert);
            await _transfertRepository.AddAsync(transfert);

            return CreatedAtAction("GetTransfert", new { id = transfert.Id }, transfert);
        }

        /// <summary>
        /// Supprime le transfert
        /// </summary>
        /// <param name="id"> l'identifiant du transfert </param>
        /// <returns> Retourne le status de la reponse </returns>
        
        // DELETE: api/Transfert/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> DeleteTransfert(int id)
        {
            var transfert = await _transfertRepository.GetAsync(id);
            if (transfert == null)
            {
                throw new NotFoundException(nameof(GetTransferts), id);
            }

            await _transfertRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> TransfertExists(int id)
        {
            return await _transfertRepository.ExistAsync(id);
        }
    }
}
