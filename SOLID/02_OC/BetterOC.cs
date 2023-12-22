namespace SOLID._02_OC
{
    internal class BetterOC
    {
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item))
                    yield return item;
            }
        }
    }


    // perdicate
    public interface ISpecification<T>
    {
        bool IsSatisfied(T value);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    // Filter products by color
    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product value)
        {
            return value.Color == _color;
        }
    }

    // Filter products by Size
    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product value)
        {
            return value.Size == _size;
        }
    }

    // combinator
    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> _specificationOne, _specificationTwo;

        public AndSpecification(ISpecification<T> specificationOne, ISpecification<T> specificationTwo)
        {
            _specificationOne = specificationOne;
            _specificationTwo = specificationTwo;
        }

        public bool IsSatisfied(T value)
        {
            return _specificationOne.IsSatisfied(value) && _specificationTwo.IsSatisfied(value);
        }
    }
}


/**
using SOLID._02_OC;

var apple = new Product("Apple", Color.Green, Size.Small);
var tree = new Product("Tree", Color.Green, Size.Large);
var house = new Product("House", Color.Blue, Size.Large);

Product[] products = { apple, house, tree };

var productFilter = new ProductFilter();

Console.WriteLine("Green products (old): ");

foreach (var pf in productFilter.FilterByColor(products, Color.Green))
{
    Console.WriteLine(pf.ToString());
}

var bf = new BetterFilter();

Console.WriteLine("Green products (new): ");

foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
{
    Console.WriteLine(p.ToString());
}


Console.WriteLine("\n\nLarge blue items! (andSpec): ");

foreach (var p in bf.Filter(products, new AndSpecification<Product>
                                                (
                                                new ColorSpecification(Color.Blue),
                                                new SizeSpecification(Size.Large)
                                                )
                            )
    )
{
    Console.WriteLine(p.ToString());
}

 */