using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neizraziti;

namespace FuzzySet
{
    public interface IFuzzySet
    {
        IDomain GetDomain();
        double GetValueAt(DomainElement element);
    }
}
