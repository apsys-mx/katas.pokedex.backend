using katas.pokedex.services.pokemons;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace katas.pokedex.webapi.Controllers
{

    /// <summary>
    /// Pokemon controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PokemonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public IActionResult GetPokemons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize= 20)
        {
            var command = new GetPokemonsServices.Command(pageNumber, pageSize);
            var results = this.mediator.Send(command).Result;
            return Ok(results);
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetPokemonById(string id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IActionResult CreatePokemon()
        {
            return Ok("Create");
        }

        [HttpPost, Route("init")]
        public async Task<IActionResult> Initialize()
        {
            var command = new InitializeDatabase.Command();
            await this.mediator.Send(command);
            return Ok();
        }

    }
}
