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
            var pagination = new PaginationResult<Pokemon>(pageNumber, pageSize, results.Items, results.Count);
            return Ok(pagination);
        }

        [HttpGet, Route("{name}")]
        public IActionResult GetPokemonById(string name)
        {
            var command = new GetPokemonByName.Command(name);
            var result = this.mediator.Send(command).Result;
            if (result == null)
                return NotFound($"No pokemon found with name {name}");
            return Ok(result);
        }

        [HttpPost, Route("{name}")]
        public IActionResult CreatePokemon(string name)
        {
            try
            {
                var command = new CreatePokemonService.Command(name);
                var result = this.mediator.Send(command).Result;
                return Ok(result);
            }
            catch
            {
                return BadRequest($"Invalid name [{name}] for a pokemon.");
            }
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
