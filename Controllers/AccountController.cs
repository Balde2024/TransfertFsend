using ForSendKH.Contracts;
using ForSendKH.Models.ModelsDto.Users;
using Microsoft.AspNetCore.Mvc;

namespace ForSendKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Le controleur pour injection de dependance
        /// </summary>
        /// <param name="IAuthManager">objet de IAuthManager</param>
        /// <param name="ILogger">objet de ILogger<AccountController></param>
        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        /// <summary>
        /// Permet de s'inscrire 
        /// </summary>
        /// <param name="apiUserDto">objet de ApiUserDto</param>
        /// <returns>Retourne le resultat de l'action , si bon ou pas</returns>
        //api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromForm] ApiUserDto apiUserDto)
        {
            var errors = await _authManager.Register(apiUserDto);
            if(errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code , error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Permet de se connecter a l'application
        /// </summary>
        /// <param name="loginDto">objet de LoginDto</param>
        /// <returns>Retourne les informations d'enregistrement et le token de connexion</returns>
        //api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromForm] LoginDto loginDto)
        {
            _logger.LogInformation($"Connexion de l'utilisateur {loginDto.Email}");

            
            var authResponse = await _authManager.Login(loginDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
                 
        }


        /// <summary>
        /// Permet d'actualiser le token
        /// </summary>
        /// <param name="request">objet AuthResponseDto</param>
        /// <returns>Retourne les informations d'enregistrement et le nouveau token de connexion</returns>
        //api/Account/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Refreshtoken([FromForm] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

    }
}
