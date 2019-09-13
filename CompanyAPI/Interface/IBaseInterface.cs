using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Model;

namespace CompanyAPI.Interface
{
    public interface IBaseInterface<T>
    {
        List<T> Read();
        T Read(int id);
        T Create(T data);
        T Update(T data);
        bool Delete(int id);

    }
}
