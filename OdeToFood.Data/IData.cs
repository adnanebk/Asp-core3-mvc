using OdeToFood.Core;
using System.Collections.Generic;


namespace OdeToFood.Data
{
    public interface IData<T>
    {
        IEnumerable<T> GetByName(string name);
        T GetById(int id);
        T Update(T updatedRestaurant);
        T Add(T newRestaurant);
        T Delete(int id);
        int GetCount();
        int Commit();
    
    }  
}
