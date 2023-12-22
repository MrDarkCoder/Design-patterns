namespace SOLID._03_LSP
{
    internal class LSP
    {
        public static int Area(Rectanlge rect) => rect.Width * rect.Height;

        // Liskov substitution principle
        // Should be able to substitute a base type for subtype!

        // Rectanlge rect = new Rectanlge(2, 3);
        // Console.WriteLine($"{rect} has area {Area(rect)}");


        // Rectanlge square = new Square();
        // square.Width = 4;
        // Console.WriteLine($"{square} has area {Area(square)}");
    }

    public class Rectanlge
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectanlge()
        {
        }

        public Rectanlge(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string? ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)} : {Height}";
        }
    }

    public class Square : Rectanlge
    {
        //public new int Width
        //{
        //    set
        //    {
        //        base.Width = base.Height = value;
        //    }
        //}

        //public new int Height
        //{
        //    set
        //    {
        //        base.Width = base.Height = value;
        //    }
        //}

        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }

}
