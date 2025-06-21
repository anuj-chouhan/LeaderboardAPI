namespace LeaderboardAPI.Models
{
    /// <summary>Request to register a new player.</summary>
    public class RegisterRequest
    {
        /// <summary>Desired unique username.</summary>
        /// <example>Player123</example>
        public string? Username { get; set; }
    }

    /// <summary>Request to fetch player data by user ID.</summary>
    public class GetPlayerDataRequest
    {
        /// <summary>Unique identifier for the player.</summary>
        /// <example>abc123</example>
        public string? UserId { get; set; }
    }

    /// <summary>Request to update a player's score.</summary>
    public class UpdateScoreValue
    {
        /// <summary>The new score to update for the player.</summary>
        /// <example>2000</example>
        public int NewScore { get; set; }
    }


    /// <summary>Response containing detailed player information and rank.</summary>
    public class PlayerResponse
    {
        /// <example>abc123</example>
        public string? UserId { get; set; }

        /// <example>Player123</example>
        public string? Username { get; set; }

        /// <example>1500</example>
        public int Score { get; set; }

        /// <example>3</example>
        public int Rank { get; set; }
    }

    /// <summary>A single leaderboard entry.</summary>
    public class LeaderboardEntry
    {
        /// <example>1</example>
        public int Rank { get; set; }

        /// <example>TopPlayer</example>
        public string? Username { get; set; }

        /// <example>2500</example>
        public int Score { get; set; }
    }

    /// <summary>Internal player model used for tracking data.</summary>
    public class Player
    {
        /// <example>abc123</example>
        public string? Id { get; set; }

        /// <example>Player123</example>
        public string? Username { get; set; }

        /// <example>1500</example>
        public int Score { get; set; }
    }

    
    /// <summary>Response after registering a player.</summary>
    public class RegisterResponse
    {
        /// <example>true</example>
        public bool Available { get; set; }

        /// <example>abc123</example>
        public string? UserId { get; set; }

        /// <example>Player123</example>
        public string? Username { get; set; }

        /// <example>0</example>
        public int Score { get; set; }
    }

    /// <summary>Response containing the top leaderboard players.</summary>
    public class LeaderboardResponse
    {
        public List<LeaderboardEntry>? Leaderboard { get; set; }
    }

    ///// <summary>Generic error message for failed requests.</summary>
    //public class ErrorResponse
    //{
    //    /// <example>Player not found.</example>
    //    public string? Message { get; set; }
    //}

    ///// <summary>Generic success message response.</summary>
    //public class SuccessResponse
    //{
    //    /// <example>Score updated successfully.</example>
    //    public string? Message { get; set; }
    //}
}


//namespace LeaderboardAPI.Models
//{
//    public class RegisterRequest
//    {
//        public string Username { get; set; }
//    }

//    public class GetPlayerDataRequest
//    {
//        public string UserId { get; set; }
//    }

//    public class UpdateScoreRequest
//    {
//        public string UserId { get; set; }
//        public int NewScore { get; set; }
//    }

//    public class PlayerResponse
//    {
//        public string UserId { get; set; }
//        public string Username { get; set; }
//        public int Score { get; set; }
//        public int Rank { get; set; }
//    }

//    public class LeaderboardEntry
//    {
//        public int Rank { get; set; }
//        public string Username { get; set; }
//        public int Score { get; set; }
//    }

//    public class Player
//    {
//        public string Id { get; set; }
//        public string Username { get; set; }
//        public int Score { get; set; }
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//    public class RegisterResponse
//    {
//        public bool Available { get; set; }
//        public string UserId { get; set; }
//        public string Username { get; set; }
//        public int Score { get; set; }
//    }

//    public class LeaderboardResponse
//    {
//        public List<LeaderboardEntry> Leaderboard { get; set; }
//    }

//    /// <summary>
//    /// Generic error message for failed requests.
//    /// </summary>
//    public class ErrorResponse
//    {
//        /// <example>Player not found.</example>
//        public string Message { get; set; }
//    }

//    /// <summary>Generic success message response.</summary>
//    public class SuccessResponse
//    {
//        /// <example>Score updated successfully.</example>
//        public string Message { get; set; }
//    }
//}