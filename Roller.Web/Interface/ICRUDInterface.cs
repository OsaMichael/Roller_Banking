using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.Interface
{
    public interface ICRUDInterface<T>: IDependencyRegister
    {
        //put UPDATE    post  CREATE
        //get READ   delete DELETE

        bool Post(T type);
        bool Register(T type);
        
        T Get(int id);
        List<T> GetAll(int offset, int count);
        bool Put(T type);
        bool Delete(int id);

    }
}
