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
        public IActionResult GetPokemons()
        {
            return Ok("All pokemons");
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
    }
}
