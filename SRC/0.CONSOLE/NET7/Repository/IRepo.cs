using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET7.Repository
{
    public interface IRepo<T>
    {
        List<T> getAll();
    }
}
