/*
 
 */

namespace design_pattern_c_.Factory.Abstract
{
    internal class AbstractFactory
    {
        /*
            void Main()
            {
                new NavigationBar(new Android());
                new DropdownMenu(new Android());
            }
         */
    }


    internal class NavigationBar
    {
        public NavigationBar(IUIFactory factory) => factory.CreateButton();
    }

    internal class DropdownMenu
    {
        internal DropdownMenu(IUIFactory factory) => factory.CreateButton();
    }

    internal interface IUIFactory
    {
        public Button CreateButton();
    }

    internal class Apple : IUIFactory
    {
        public Button CreateButton()
        {
            return new Button { Type = "iOS Button" };
        }
    }

    internal class Android : IUIFactory
    {
        public Button CreateButton()
        {
            return new Button { Type = "Android Button" };
        }
    }

    internal class Button
    {
        public string Type { get; set; } = default!;
    }
}
