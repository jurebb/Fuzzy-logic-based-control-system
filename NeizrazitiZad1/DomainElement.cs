using System;

namespace Neizraziti
{
    public class DomainElement //primjerci predstavljaju konkretne ntorke
    {
        private int[] _values; //ntorka!!!

        public DomainElement(int[] element)
        {
            _values = element;
        }
        
        public int GetNumberOfComponents()
        {
            return _values.Length;
        }

        public int GetComponentValue(int index)   //komponenta ntorke?
        {
            return _values[index];
        }

        public override int GetHashCode()
        {
            int hc = _values.Length;
            for (int i = 0; i < _values.Length; ++i)
            {
                hc = unchecked(hc * 31 + _values[i]);
            }
            return hc;
        }

        public override bool Equals(object obj)   //kako se dvije ntorke uspoređuju
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            DomainElement domEl = (DomainElement)obj;
            if(domEl._values.Length != GetNumberOfComponents())
            {
                return false;
            }
            for(int i=0; i<GetNumberOfComponents(); i++)
            {
                if(domEl._values[i] != _values[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string textToPrint = "(";
            for (int i=0; i < GetNumberOfComponents(); i++)
            {
                textToPrint += _values[i].ToString();
                if (i != GetNumberOfComponents() - 1)
                {
                    textToPrint += ", ";
                }
            }
            textToPrint += ")";
            return textToPrint;
        }

        public static DomainElement Of(params int[] element)
        {
            return new DomainElement(element);
        }
    }
}