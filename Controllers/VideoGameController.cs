using Microsoft.AspNetCore.Mvc;

namespace RestApiVideoGamesExcercise.Controllers;

[Route("api/games")]
[ApiController]
public class VideoGameController : ControllerBase
{
    private static List<VideoGames> games = new List<VideoGames>
    {
        new VideoGames
        {
            Id=1,
            Name = "GTA V",
            Size = 2000,
            Studio = "RockStar"
        }
    };

    // GET: api/games
    [HttpGet]
    public IActionResult Get()
    {
        // if no games then no content
        if (games.Any() == false)
        {
            return NoContent();
        }

        // success flow
        return Ok(games);
    }

    // GET api/games/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        //If requested Id is 0 or negative
        if (id <= 0)
        {
            return BadRequest();
        }
        //Check if the game is there
        var game = games.FirstOrDefault(x => x.Id == id);

        //If game doesn't exist
        if (game == null)
        {
            return NotFound();
        }

        //Return found game
        return Ok(game);
    }

    // POST api/games
    [HttpPost]
    public IActionResult Post([FromBody] VideoGames game)
    {
        // if no request then bad request
        if (game == null)
        {
            return BadRequest();
        }

        // check each property
        if (string.IsNullOrEmpty(game.Name) || string.IsNullOrEmpty(game.Studio) || game.Size <= 0)
        {
            return BadRequest();
        }

        game.Id = games.Count + 1;
        games.Add(game);
        return CreatedAtAction(nameof(Post), game);
    }

    // PUT api/games/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    // DELETE api/games/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        //If requested Id is 0 or negative
        if (id <= 0)
        {
            return BadRequest();
        }

        //Check if the game is there
        var game = games.FirstOrDefault(x => x.Id == id);

        //If the game doesn't exist
        if (game == null)
        {
            return NotFound();
        }

        //Detele game
        _ = games.Remove(game);
        return NoContent();
    }
}
