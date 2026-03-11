using System.Collections.Generic;

namespace Reports.DataAccess
{
    public interface IDataPersistence<T> where T : class
    {
        void Load(ICollection<T> data);
        void Save(ICollection<T> data); 
    }
}
