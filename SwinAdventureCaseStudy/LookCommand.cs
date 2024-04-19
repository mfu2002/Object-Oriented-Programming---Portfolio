namespace SwinAdventureCaseStudy
{
    public class LookCommand() : Command(["look"])
    {
        public override string Execute(Player p, string[] text)
        {
            if (text.Length == 1 && text[0].Equals("look", StringComparison.CurrentCultureIgnoreCase)) { return p.Location.FullDescription; }

            if (text.Length != 3 && text.Length != 5) { return "I don't know how to look like that"; }
            if (!text[0].Equals("look", StringComparison.CurrentCultureIgnoreCase)) { return "Error in look input"; }
            if (!text[1].Equals("at", StringComparison.CurrentCultureIgnoreCase)) { return "What do you want to look at?"; }
            if (text.Length == 5 && !text[3].Equals("in", StringComparison.CurrentCultureIgnoreCase)) { return "What do you want to look in?"; }

            IHaveInventory? container;
            if (text.Length == 3)
            {
                container = p;
            }
            else
            {
                container = FetchContainer(p, text[4]);
            }
            if (container == null) { return $"I can't find the {text[4]}"; }
            string itemId = text[2];
            string? itemInfo = LookAtIn(itemId, container);
            if (itemInfo == null)
            {
                if (text.Length == 3)
                {
                    return $"I can't find the {itemId}";
                }
                else
                {
                    return $"I can't find the {itemId} in the {container.Name}";
                }

            }
            return itemInfo;
        }
        public IHaveInventory? FetchContainer(Player p, string commandId)
        {
            return p.Locate(commandId) as IHaveInventory;
        }
        public string? LookAtIn(string thingId, IHaveInventory container)
        {
            return container.Locate(thingId)?.FullDescription;
        }
    }
}
