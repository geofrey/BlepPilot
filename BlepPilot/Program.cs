using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlepPilot
{
    class Runtime
    {
        public String apiToken;

        public Runtime(String[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("expected path to Discord bot token file as first argument");
            }

            loadAPIToken(args[0]);
        }

        void loadAPIToken(String path)
        {
            try
            {
                apiToken = File.ReadAllText(path).Trim();
            }
            catch (Exception whoopsadoodle)
            {
                throw new Exception("could not read token file", whoopsadoodle);
            }
        }
    }

    class DiscordBot
    {
        static void Main(string[] args)
        {
            try
            {
                Runtime settings = new Runtime(args);
                run(settings).GetAwaiter().GetResult();
            }
            catch(Exception malfunction)
            {
                Console.Error.WriteLine($"oh no program error: {malfunction}");
            }
        }

        static async Task run(Runtime settings)
        {
            //Console.WriteLine($"bot token value is \"{settings.apiToken}\"");
            DiscordConfiguration config = new DiscordConfiguration()
            {
                Token = settings.apiToken,
                TokenType = TokenType.Bot
            };
            DiscordClient api = new DiscordClient(config);

            api.MessageCreated += async (e) => {
                if (e.Message.Content == "xyzzy")
                {
                    await e.Message.RespondAsync("Nothing happens.");
                    Console.Out.WriteLine($"did nothing for {e.Author}");
                }

                if(e.Message.Content == "begone, bot")
                {
                    await e.Message.RespondAsync("Botface OUT");
                    Console.Out.WriteLine($"banished by {e.Author}");
                    await api.DisconnectAsync();
                }
            };

            Boolean everConnected = false;
            api.SocketOpened += async () =>
            {
                await Console.Out.WriteLineAsync("connection opened");
                everConnected = true;
            };

            Boolean disconnected = false;
            api.SocketClosed += async (e) =>
            {
                await Console.Out.WriteLineAsync("connection closed");
                disconnected = true;
            };

            await api.ConnectAsync();
            while (!everConnected) await Task.Delay(1000);
            while (!disconnected) await Task.Delay(1000);
        }
    }
}
