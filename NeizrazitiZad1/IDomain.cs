using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neizraziti
{
    public interface IDomain : IEnumerator, IEnumerable
    {
        int GetCardinality();
        IDomain GetComponent(int element);
        int GetNumberOfComponents();
        int IndexOfElement(DomainElement domainElement);
        DomainElement ElementForIndex(int element);
    }
}
