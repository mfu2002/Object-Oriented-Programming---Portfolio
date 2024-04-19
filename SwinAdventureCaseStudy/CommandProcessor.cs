namespace SwinAdventureCaseStudy
{
    public class CommandProcessor
    {
        Command[] _registeredCommands = [
            new MoveCommand(),
            new LookCommand()
            ];
        public string Execute(Player p, string[] text)
        {
            Command? cmd = FindCommand(text[0]);
            if (cmd == null) { return $"I don't understand ${text[0]}"; }
            return cmd.Execute(p, text);
        }

        public Command? FindCommand(string commandStr)
        {
            foreach (Command command in _registeredCommands)
            {
                if (command.AreYou(commandStr))
                {
                    return command;
                }
            }
            return null;
        }

    }
}
