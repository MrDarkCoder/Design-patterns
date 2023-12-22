namespace SOLID._02_OC
{
    //var apple = new Product("Apple", Color.Green, Size.Small);
    //var tree = new Product("Tree", Color.Green, Size.Large);
    //var house = new Product("House", Color.Blue, Size.Large);

    //Product[] products = { apple, house, tree };

    //var productFilter = new ProductFilter();

    //Console.WriteLine("Green products (old): ");

    //foreach (var pf in productFilter.FilterByColor(products, Color.Green))
    //{
    //    Console.WriteLine(pf.ToString());
    //}


    //Console.WriteLine("Green and Large products (old): ");

    //foreach (var pf in productFilter.FilterBySizeAndColor(products, Size.Large, Color.Green))
    //{
    //    Console.WriteLine(pf.ToString());
    //}



    // may want to filter based on specifications (certain criteria).
    // our product filter class will be modified too many times!!
    public class ProductFilter
    {
        // filter by size 
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (Product product in products)
            {
                if (product.Size == size)
                    yield return product;
            }
        }

        // filter by size 
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (Product product in products)
            {
                if (product.Color == color)
                    yield return product;
            }
        }

        // now you going to filter by color and size!
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (Product product in products)
            {
                if (product.Color == color && product.Size == size)
                    yield return product;
            }
        }
    }

    // let suppose we are building a ordering system, 
    // maybe a website where we can buy products
    // and the products have certain categories and certain traits
    // Colors, Size etc..
}
