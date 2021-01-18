using System;
using System.IO;

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
                apiToken = File.ReadAllText(path);
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
            Runtime settings = new Runtime(args);
            run(settings);
        }

        static void run(Runtime settings)
        {
            Console.WriteLine($"bot token value is \"{settings.apiToken}\"");
        }
    }
}
