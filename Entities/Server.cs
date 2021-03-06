using Microsoft.Extensions.Logging;
using TriviadorServerApi.Entities;

namespace TriviadorServerApi
{
    public class Server
    {
        private readonly ILogger _logger;
        public Server(ILogger logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation("Initialize game session");

            GameSession.Initialize();
            _logger.LogInformation("Game session started");
            _logger.LogDebug($"Map initialized: {GameSession.GetSerializedMap()}");
        }
    }
}
