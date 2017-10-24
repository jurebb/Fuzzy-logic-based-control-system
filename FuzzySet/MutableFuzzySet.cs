using Neizraziti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySet
{
    [Serializable]
    public class MutableFuzzySet : IFuzzySet
    {
        private double[] _memberships;
        private IDomain _domain;

        public MutableFuzzySet(IDomain domain)
        {
            _domain = domain;
            _memberships = new double[domain.GetCardinality()];
        }

        public IDomain GetDomain()
        {
            _domain.Reset();
            return _domain;
        }

        public double GetValueAt(DomainElement element)
        {
            return _memberships[_domain.IndexOfElement(element)];
        }

        public MutableFuzzySet Set(DomainElement element, double value)
        {
            _memberships[_domain.IndexOfElement(element)] = value;
            return this;
        }
    }
}
