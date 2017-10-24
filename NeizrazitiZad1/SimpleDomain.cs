using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neizraziti
{
    [Serializable]
    class SimpleDomain : Domain     //jednostavna domena (n=1)
    {
        private int _first;
        private int _last;
        private int position = -1;

        public SimpleDomain(int first, int last)    //granice domene, last iskljuciv
        {
            _first = first;
            _last = last;
        }

        public override int GetCardinality()
        {
            return (_last - _first);
        }

        public override IDomain GetComponent(int i)  // vraća i-tu komponentu tog kartezijevog skupa
        {
            //Jednostavna domena vraća 1 i samu sebe.
            return this;
        }

        public override int GetNumberOfComponents() //vraća broj jednostavnih domena koje sudjeluju u kartezijevom skupu
        {
            //Jednostavna domena vraća 1 i samu sebe.
            return 1;
        }


        public override bool MoveNext()
        {
            position++;
            return ((_first + position) < _last);
        }

        public override void Reset()
        {
            position = -1;
        }

        public override IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public override object Current
        {
            get
            {
                return new DomainElement(new int[] { _first + position });
            }
        }

        public int GetFirst
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }
        public int GetLast
        {
            get
            {
                return _last;
            }
            set
            {
                _last = value;
            }
        }
    }
}
