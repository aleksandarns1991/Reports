using Reports.DataAccess;
using Reports.Models;
using Splat;

namespace Reports.Utility
{
    public class AppBootStrapper
    {
        public AppBootStrapper()
        {
            Locator.CurrentMutable.RegisterConstant(new NewtonSerializer(), typeof(IDataPersistence<Guard>));
        }
    }
}
