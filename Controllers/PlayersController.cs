using Microsoft.AspNetCore.Mvc;
using LeaderboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaderboardAPI.Controllers
{
    [ApiController]
    [Route("players")]
    [Tags("Player Management")]
    public class PlayersController : ControllerBase
    {
        private static readonly List<Player> Players = new();

        /// <summary>
        /// Registers a new player or checks if the username is available.
        /// </summary>
        /// <param name="request">Contains the desired username.</param>
        /// <returns>Success or availability status, and player details if created.</returns>
        [Produces("application/json")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var existing = Players.FirstOrDefault(p => p.Username == request.Username);
            if (existing != null)
            {
                return Ok(new RegisterResponse
                {
                    Available = false
                });
            }

            var newPlayer = new Player
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                Score = 0
            };

            Players.Add(newPlayer);

            return Ok(new RegisterResponse
            {
                Available = true,
                UserId = newPlayer.Id,
                Username = newPlayer.Username,
                Score = newPlayer.Score
            });
        }

        /// <summary>
        /// Updates a player's score if the new score is higher.
        /// </summary>
        /// <param name="id">The unique player ID.</param>
        /// <param name="request">New score value.</param>
        /// <returns>Success message or error if player is not found.</returns>
        [Produces("application/json")]
        [HttpPut("players/{id}/score")]
        [ProducesResponseType(200)]
        public IActionResult UpdateScore(string id, [FromBody] UpdateScoreValue request)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
                return NotFound();

            if (request.NewScore > player.Score)
                player.Score = request.NewScore;

            return Ok();
        }


        /// <summary>
        /// Retrieves player data and their current rank.
        /// </summary>
        /// <param name="request">Contains the player's unique ID.</param>
        /// <returns>Player info including score and rank or error if not found.</returns>
        [Produces("application/json")]
        [HttpPost("getPlayerData")]
        [ProducesResponseType(typeof(PlayerResponse), 200)]
        public IActionResult GetPlayerData([FromBody] GetPlayerDataRequest request)
        {
            var player = Players.FirstOrDefault(p => p.Id == request.UserId);
            if (player == null)
                return NotFound();

            var rank = Players
                .OrderByDescending(p => p.Score)
                .Select((p, index) => new { p.Id, Rank = index + 1 })
                .FirstOrDefault(p => p.Id == player.Id)?.Rank ?? -1;

            return Ok(new PlayerResponse
            {
                UserId = player.Id,
                Username = player.Username,
                Score = player.Score,
                Rank = rank
            });
        }

        /// <summary>
        /// Gets the top 10 players ranked by score.
        /// </summary>
        /// <returns>A list of top players with username, score, and rank.</returns>
        [Produces("application/json")]
        [HttpGet("leaderboard")]
        [ProducesResponseType(typeof(LeaderboardResponse), 200)]
        public IActionResult GetLeaderboard()
        {
            var leaderboard = Players
                .OrderByDescending(p => p.Score)
                .Take(10)
                .Select((p, index) => new LeaderboardEntry
                {
                    Rank = index + 1,
                    Username = p.Username,
                    Score = p.Score
                })
                .ToList();

            return Ok(new LeaderboardResponse
            {
                Leaderboard = leaderboard
            });
        }
    }
}




//using Microsoft.AspNetCore.Mvc;
//using LeaderboardAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace LeaderboardAPI.Controllers
//{
//    [ApiController]
//    [Route("players")]
//    public class PlayersController : ControllerBase
//    {
//        private static readonly List<Player> Players = new();

//        // 🔹 Register New Player or Check Username Availability
//        [HttpPost("register")]
//        [ProducesResponseType(typeof(RegisterResponse), 200)]
//        public IActionResult Register([FromBody] RegisterRequest request)
//        {
//            var existing = Players.FirstOrDefault(p => p.Username == request.Username);
//            if (existing != null)
//            {
//                return Ok(new RegisterResponse
//                {
//                    Available = false
//                });
//            }

//            var newPlayer = new Player
//            {
//                Id = Guid.NewGuid().ToString(),
//                Username = request.Username,
//                Score = 0
//            };

//            Players.Add(newPlayer);

//            return Ok(new RegisterResponse
//            {
//                Available = true,
//                UserId = newPlayer.Id,
//                Username = newPlayer.Username,
//                Score = newPlayer.Score
//            });
//        }

//        // 🔹 Update Player Score (Only if Higher)
//        [HttpPost("updateScore")]
//        [ProducesResponseType(typeof(SuccessResponse), 200)]
//        [ProducesResponseType(typeof(ErrorResponse), 404)]
//        public IActionResult UpdateScore([FromBody] UpdateScoreRequest request)
//        {
//            var player = Players.FirstOrDefault(p => p.Id == request.UserId);
//            if (player == null)
//                return NotFound(new ErrorResponse { Message = "Player not found." });

//            if (request.NewScore > player.Score)
//                player.Score = request.NewScore;

//            return Ok(new SuccessResponse { Message = "Score updated successfully." });
//        }


//        // 🔹 Get Player Data + Rank
//        [HttpPost("getPlayerData")]
//        [ProducesResponseType(typeof(PlayerResponse), 200)]
//        [ProducesResponseType(typeof(ErrorResponse), 404)]
//        public IActionResult GetPlayerData([FromBody] GetPlayerDataRequest request)
//        {
//            var player = Players.FirstOrDefault(p => p.Id == request.UserId);
//            if (player == null)
//                return NotFound(new ErrorResponse { Message = "Player not found." });

//            var rank = Players
//                .OrderByDescending(p => p.Score)
//                .Select((p, index) => new { p.Id, Rank = index + 1 })
//                .FirstOrDefault(p => p.Id == player.Id)?.Rank ?? -1;

//            return Ok(new PlayerResponse
//            {
//                UserId = player.Id,
//                Username = player.Username,
//                Score = player.Score,
//                Rank = rank
//            });
//        }

//        // 🔹 Get Top 10 Players Leaderboard
//        [HttpGet("leaderboard")]
//        [ProducesResponseType(typeof(LeaderboardResponse), 200)]
//        public IActionResult GetLeaderboard()
//        {
//            var leaderboard = Players
//                .OrderByDescending(p => p.Score)
//                .Take(10)
//                .Select((p, index) => new LeaderboardEntry
//                {
//                    Rank = index + 1,
//                    Username = p.Username,
//                    Score = p.Score
//                })
//                .ToList();

//            return Ok(new LeaderboardResponse
//            {
//                Leaderboard = leaderboard
//            });
//        }
//    }
//}