using System;
using System.IO;

namespace BlepPilot
{
    class DiscordBot
    {
        static void Main(string[] args)
        {
            String apiToken;

            try {
                if (args.Length < 1)
                {
                    throw new ArgumentException("expected path to Discord bot token file as first argument");
                }
                String pathToToken = args[0];
                Console.Out.WriteLine($"read {pathToToken}");

                apiToken = File.ReadAllText(pathToToken);
            } catch (Exception whoopsadoodle)
            {
                Console.Error.WriteLine($"couk  wvszld not open token file: {whoopsadoodle}");
                return;
            }

            Console.WriteLine($"bot token value is \"{apiToken}\"");
        }
    }
}
