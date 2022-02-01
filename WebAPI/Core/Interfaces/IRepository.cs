using System;
using System.Collections.Generic;

namespace WebAPI.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetInvoiceList(); 
        T GetInvoice(int id); 
        void Create(T item); 
        void Update(T item);  
    }
}
