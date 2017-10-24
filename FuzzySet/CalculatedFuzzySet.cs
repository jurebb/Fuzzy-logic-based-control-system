using Neizraziti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySet
{
    public class CalculatedFuzzySet : IFuzzySet
    {
        IDomain _domain;
        IIntUnaryFunction _interfaceInstance;

        public CalculatedFuzzySet(IDomain domain, IIntUnaryFunction function)
        {
            _domain = domain;
            _interfaceInstance = function;
        }

        public IDomain GetDomain()
        {
            _domain.Reset();
            return _domain;
        }

        public double GetValueAt(DomainElement element)
        {
            return _interfaceInstance.ValueAt(_domain.IndexOfElement(element));
        }
    }
}
