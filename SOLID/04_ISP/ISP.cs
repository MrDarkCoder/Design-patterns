namespace SOLID._04_ISP
{

    public class Document
    {
    }

    public interface IMachine
    {
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);
    }


    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }

    // What if i have old fashsion printer that can do only print and scan?


    public class OldFashsionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            //
        }

        // does it feasible solution? oh no! we are jsut throwing expection which doesn't make sense!
        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    // so we need to make smaller chucnks of interfaces!

    public interface IPrint
    {
        void Print(Document document);
    }

    public interface IScan
    {
        void Scan(Document document);
    }

    public interface IFax
    {
        void Fax(Document document);
    }



    public class MultiFunctionalPrinter : IPrint, IScan, IFax
    {
        public void Fax(Document document)
        {
            //
        }

        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }


    public class OldFunctionalPrinter : IPrint, IScan
    {
        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }


    // this is what interface segregation principle!


    // delegate the calls to separate it's own implementations (decorator pattern)

    public interface IMultiFunctionedPrinter : IPrint, IScan
    { }

    public class MultiFunctionedPrinter : IMultiFunctionedPrinter
    {
        // instead of implementing it in here!, what if we had already implement printer and scanner class sperately
        private readonly IPrint _print;
        private readonly IScan _scan;

        public MultiFunctionedPrinter(IPrint print, IScan scan)
        {
            _print = print;
            _scan = scan;
        }

        public void Print(Document document)
        {
            _print.Print(document);
        }

        public void Scan(Document document)
        {
            _scan.Scan(document);
        }
    }


}
