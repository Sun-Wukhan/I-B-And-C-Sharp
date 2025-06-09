using Grpc.Core;
using FF7Game.Server;

namespace FF7Game.Server.Services
{
    public class GameService : FF7Game.Server.GameService.GameServiceBase
    {
        private readonly ILogger<GameService> _logger;
        private readonly Dictionary<string, Character> _characters;

        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
            _characters = new Dictionary<string, Character>
            {
                ["Cloud"] = new Character
                {
                    Name = "Cloud",
                    Level = 1,
                    Hp = 100,
                    Mp = 50,
                    Materia = { "Fire", "Cure" }
                },
                ["Tifa"] = new Character
                {
                    Name = "Tifa",
                    Level = 1,
                    Hp = 90,
                    Mp = 40,
                    Materia = { "Ice", "Cure" }
                }
            };
        }

        public override Task<Character> GetCharacter(CharacterRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Getting character information for {request.CharacterName}");

            if (_characters.TryGetValue(request.CharacterName, out var character))
            {
                return Task.FromResult(character);
            }

            throw new RpcException(new Status(StatusCode.NotFound, $"Character {request.CharacterName} not found"));
        }

        public override Task<BattleResponse> StartBattle(BattleRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Starting battle between {request.CharacterName} and {request.EnemyName}");

            if (!_characters.TryGetValue(request.CharacterName, out var character))
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Character {request.CharacterName} not found"));
            }

            // Simple battle logic
            var random = new Random();
            var damageDealt = random.Next(10, 30);
            var damageReceived = random.Next(5, 20);
            var victory = damageDealt > damageReceived;

            return Task.FromResult(new BattleResponse
            {
                Victory = victory,
                DamageDealt = damageDealt,
                DamageReceived = damageReceived,
                ExpGained = victory ? random.Next(10, 30) : 0,
                ItemsDropped = { victory ? "Potion" : "" }
            });
        }

        public override Task<Character> LevelUp(CharacterRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Leveling up character {request.CharacterName}");

            if (!_characters.TryGetValue(request.CharacterName, out var character))
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Character {request.CharacterName} not found"));
            }

            character.Level++;
            character.Hp += 20;
            character.Mp += 10;

            return Task.FromResult(character);
        }
    }
} 