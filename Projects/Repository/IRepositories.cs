using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
    public interface IRepositories
    {
        void SaveChanges();

        UsersRepository Users { get; set; }
        DictionariesRepository Dictionaries { get; set; }
    }
}
