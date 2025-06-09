using Grpc.Net.Client;
using FF7Game.Server;

namespace FF7Game.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configure the channel to use HTTP/2 without TLS
            var channel = GrpcChannel.ForAddress("http://localhost:5205", new GrpcChannelOptions
            {
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
            });
            
            var client = new GameService.GameServiceClient(channel);

            Console.WriteLine("Welcome to FF7 Game Client!");
            Console.WriteLine("Available characters: Cloud, Tifa");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Get character info");
                Console.WriteLine("2. Start battle");
                Console.WriteLine("3. Level up character");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice (1-4): ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await GetCharacterInfo(client);
                            break;
                        case "2":
                            await StartBattle(client);
                            break;
                        case "3":
                            await LevelUpCharacter(client);
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static async Task GetCharacterInfo(GameService.GameServiceClient client)
        {
            Console.Write("Enter character name: ");
            var name = Console.ReadLine();

            var character = await client.GetCharacterAsync(new CharacterRequest { CharacterName = name });
            Console.WriteLine($"\nCharacter Info:");
            Console.WriteLine($"Name: {character.Name}");
            Console.WriteLine($"Level: {character.Level}");
            Console.WriteLine($"HP: {character.Hp}");
            Console.WriteLine($"MP: {character.Mp}");
            Console.WriteLine($"Materia: {string.Join(", ", character.Materia)}");
        }

        static async Task StartBattle(GameService.GameServiceClient client)
        {
            Console.Write("Enter character name: ");
            var characterName = Console.ReadLine();
            Console.Write("Enter enemy name: ");
            var enemyName = Console.ReadLine();

            var response = await client.StartBattleAsync(new BattleRequest
            {
                CharacterName = characterName,
                EnemyName = enemyName
            });

            Console.WriteLine($"\nBattle Results:");
            Console.WriteLine($"Victory: {(response.Victory ? "Yes" : "No")}");
            Console.WriteLine($"Damage Dealt: {response.DamageDealt}");
            Console.WriteLine($"Damage Received: {response.DamageReceived}");
            Console.WriteLine($"EXP Gained: {response.ExpGained}");
            if (response.ItemsDropped.Any())
            {
                Console.WriteLine($"Items Dropped: {string.Join(", ", response.ItemsDropped)}");
            }
        }

        static async Task LevelUpCharacter(GameService.GameServiceClient client)
        {
            Console.Write("Enter character name: ");
            var name = Console.ReadLine();

            var character = await client.LevelUpAsync(new CharacterRequest { CharacterName = name });
            Console.WriteLine($"\nCharacter Leveled Up!");
            Console.WriteLine($"New Level: {character.Level}");
            Console.WriteLine($"New HP: {character.Hp}");
            Console.WriteLine($"New MP: {character.Mp}");
        }
    }
}
