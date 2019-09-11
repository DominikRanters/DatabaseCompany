using System;
using System.Collections.Generic;
using System.Text;
using CompanyApp.Model;

namespace CompanyApp.Interface
{
    public interface IBaseInterface<T>
    {
        List<T> Read(string dbConStr);
        T Read(string dbConStr, int id);
        T Create(string dbConStr, T data);
        T Update(string dbConStr, T data);
        bool Delete(string dbConStr, int id);

    }
}
