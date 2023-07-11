namespace design_pattern_c_.Factory
{
    internal class Factory
    {
        /*
        void Main()
            {
	            new NavigationBar();
	            new DropdownMenu();
            }
        */
    }

    // Building objects  - process of creating object inside a factory.

    internal class NavigationBar
    {
        public NavigationBar() => ButtonFactory.CreateButton();
    }

    internal class DropdownMenu
    {
        public DropdownMenu() => ButtonFactory.CreateButton();
    }

    internal class ButtonFactory
    {
        public static Button CreateButton()
        {
            return new Button { Type = "Red Button" };
        }
    }

    internal class Button
    {
        public string Type { get; set; } = default!;
    }
}
