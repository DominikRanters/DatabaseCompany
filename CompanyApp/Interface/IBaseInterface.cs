using System;
using System.Collections.Generic;
using System.Text;
using CompanyApp.Model;

namespace CompanyApp.Interface
{
    public interface IBaseInterface<T>
    {
        List<T> Read(string dbSConStr);
        T Read(string dbSConStr, int id);
        T Create(string dbSConStr, T data);
        T Update(string dbSConStr, T data);
        bool Delete(string dbSConStr, int id);

    }
}
