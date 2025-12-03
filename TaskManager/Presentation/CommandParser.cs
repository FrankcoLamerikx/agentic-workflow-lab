using System;

namespace TaskManager.Presentation
{
    public class CommandParser
    {
        public string Command { get; private set; }
        public string Argument { get; private set; }

        public bool Parse(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return false;
            }

            Command = args[0].ToLower();

            if (args.Length > 1)
            {
                Argument = args[1];
            }

            return true;
        }
    }
}
