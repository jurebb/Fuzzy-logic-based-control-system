using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    class ConcreteUnaryFunction : IUnaryFunction
    {
        Func<double, double> _doUnaryFunction;

        public ConcreteUnaryFunction(Func<double, double> functionA)
        {
            _doUnaryFunction = functionA;
        }

        public double ValueAt(double value)
        {
            return _doUnaryFunction(value);
        }
    }
}
