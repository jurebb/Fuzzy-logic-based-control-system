using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Neizraziti
{
    [Serializable]
    public abstract class Domain : IDomain
    {

        public Domain()
        {

        }

        public abstract int GetCardinality();
        public abstract IDomain GetComponent(int element);
        public abstract int GetNumberOfComponents();

        public abstract object Current { get; }
        public abstract bool MoveNext();
        public abstract void Reset();
        public abstract IEnumerator GetEnumerator();

        public static IDomain IntRange(int first, int last)
        {
            return new SimpleDomain(first, last);
        }

        public static Domain Combine(IDomain first, IDomain second)
        {
            if (first.GetNumberOfComponents() + second.GetNumberOfComponents() > 2)
            {

                SimpleDomain[] domainsArray = new SimpleDomain[first.GetNumberOfComponents() + second.GetNumberOfComponents()];
                int i = 0;

                if(first is CompositeDomain)                            //prvi argument je tipa 'CompositeDomain'
                {
                    for (int j = 0; j < first.GetNumberOfComponents(); j++)
                    {
                        domainsArray[i] = (SimpleDomain)first.GetComponent(j);
                        i++;
                    }
                }
                else
                {
                    domainsArray[i] = (SimpleDomain)first.GetComponent(0);
                    i++;
                }
                if (second is CompositeDomain)                          //drugi argument je tipa 'CompositeDomain'
                {
                    for (int j = 0; j < second.GetNumberOfComponents(); j++)
                    {
                        domainsArray[i] = (SimpleDomain)second.GetComponent(j);
                        i++;
                    }
                }
                else
                {
                    domainsArray[i] = (SimpleDomain)second.GetComponent(0);
                    i++;
                }

                return new CompositeDomain(domainsArray);
            }
            else
            {
                SimpleDomain[] domainsArray = new SimpleDomain[] { (SimpleDomain)first, (SimpleDomain)second };
                return new CompositeDomain(domainsArray);
            }
        }

        public DomainElement ElementForIndex(int index)                 //vraca vrijednost elementa na indexu
        {
            Domain copiedObj = DeepCopy(this);
            int i = 0;
            copiedObj.Reset();
            foreach (DomainElement de in copiedObj)
            {
                if (i == index)
                {
                    return de;
                }
                i++;
            }
            return null;
        }     

        public int IndexOfElement(DomainElement domainElement)          //vraca vrijednost indexa za element
        {
            //Domain copiedObj = (Domain)this.DeepCopy();
            Domain copiedObj = DeepCopy(this);
            int i = 0;
            copiedObj.Reset();                
            foreach (DomainElement de in copiedObj)
            {
                if(domainElement.Equals(de))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }



        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
