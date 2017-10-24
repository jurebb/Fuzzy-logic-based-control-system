using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySet
{
    class ConcreteIntUnaryFunction : IIntUnaryFunction
    {
        Func<int, double> _doUnaryFunction;

        public ConcreteIntUnaryFunction(Func<int, double> functionA)
        {
            _doUnaryFunction = functionA;
        }

        public double ValueAt(int index)
        {
            return _doUnaryFunction(index);
        }
    }
}
