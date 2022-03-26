using Lets.Code.Application.Shared.Domain.Models;
using Lets.Code.Application.Shared.Extensions;
using Lets.Code.Application.Shared.Interfaces;
using Lets.Code.Application.Shared.Repository;
using Lets.Code.Application.Shared.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Lets.Code.WebApi.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/")]
    
    public class CardsController : ControllerBase
    {
        private readonly ICardApplication _cardApplication;
        private readonly IAutenticateRepository _autenticateRepository;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardApplication"></param>
        /// <param name="autenticateRepository"></param>
        /// <param name="configuration"></param>
        public CardsController(ICardApplication cardApplication, IAutenticateRepository autenticateRepository, IConfiguration configuration)
        {
            _cardApplication = cardApplication;
            _autenticateRepository = autenticateRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// Autentication
        /// </summary>
        /// <param name="model"> Data of Login and Password</param>
        /// <returns>Object if User and Token</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<dynamic>> Authenticate([Required][FromBody] User model)
        {
            model.ThrowIfIsNotValid();

            var user = await _autenticateRepository.Get(model.Login, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = await Task.Run(() => TokenService.GenerateToken(user, _configuration));

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
        /// <summary>
        /// Get Cards
        /// </summary>
        /// <returns>List of Cards</returns>
        [HttpGet]
        [Route("card")]
        [Authorize]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await _cardApplication.GetCards());
        }
        /// <summary>
        /// Add a new card
        /// </summary>
        /// <param name="card">values of new card</param>
        /// <returns>The card created</returns>
        [HttpPost]
        [Route("card")]
        [Authorize]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> CreateNewCard([FromBody] CardInput card)
        {
            
            return Ok(await _cardApplication.AddCard(card));
        }
        /// <summary>
        /// Update a Card
        /// </summary>
        /// <param name="id">key of card</param>
        /// <param name="card">updated values</param>
        /// <returns>The card Updated</returns>
        [HttpPut]
        [Route("card/{id}")]
        [Authorize]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> EditCard([Required] Guid id, [BindRequired][FromBody] CardInput card)
        {
            card.Id = id;

            var cardOutput = await _cardApplication.UpdateCard(card);

            if(cardOutput == null)
                return NotFound();
            else
                return Ok(cardOutput);
        }
        /// <summary>
        /// Remove a card
        /// </summary>
        /// <param name="id"> key of card</param>
        /// <returns>return a boll to indicate sucess or error</returns>
        [HttpDelete]
        [Route("card/{id}")]
        [Authorize]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<dynamic>> DeleteCard([Required] Guid id)
        {
            var sucess = await _cardApplication.RemoveCard(id);
            if (sucess) 
            {
                return Ok(await _cardApplication.GetCards());
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
