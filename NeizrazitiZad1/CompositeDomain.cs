using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neizraziti
{
    [Serializable]
    class CompositeDomain : Domain      //kartezijev produkt jednostavnih domena (n>1)
    {
        private SimpleDomain[] _elements;
        private int [] position;
        private int _components;

        public CompositeDomain(SimpleDomain[] domains)
        {
            _elements = domains;
            _components = GetNumberOfComponents();
            position = new int[_components];
            position[_components-1] = -1;
        }

        public override int GetCardinality()
        {
            int product = 1;
            foreach(SimpleDomain sd in _elements)
            {
                product *= sd.GetCardinality();
            }
            return product;
        }

        public override IDomain GetComponent(int i)  // vraća i-tu komponentu tog kartezijevog skupa
        {
            return _elements[i];
        }

        public override int GetNumberOfComponents() //vraća broj jednostavnih domena koje sudjeluju u kartezijevom skupu
        {
            return _elements.Count();
        }


        public override bool MoveNext()
        {
            for (int i = _components - 1; i >= 0; i--)
            {
                if (position[i] < _elements[i].GetCardinality() - 1)    
                {
                    position[i]++;
                    break;
                }
                else if (i != 0)
                {
                    position[i] = 0;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override void Reset()
        {
            Array.Clear(position, 0, position.Length);
            position[_components - 1] = -1;
        }

        public override IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public override object Current
        {
            get
            {
                int[] array = new int[_components];
                for(int i=0; i<_components; i++)
                {
                    array[i] = _elements[i].GetFirst + position[i];
                }
                return new DomainElement(array);
            }
        }
    }
}
