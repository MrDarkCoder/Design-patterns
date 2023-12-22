namespace SOLID._02_OC
{
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }

        public override string? ToString()
        {
            return $"{Name} - {Color} - {Size}";
        }
    }

    // let suppose we are building a ordering system, 
    // maybe a website where we can buy products
    // and the products have certain categories and certain traits
    // Colors, Size etc..
}
