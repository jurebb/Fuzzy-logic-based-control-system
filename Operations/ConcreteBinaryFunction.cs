using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    class ConcreteBinaryFunction : IBinaryFunction
    {
        Func<double, double, double> _doBinaryFunction;

        public ConcreteBinaryFunction(Func<double, double, double> functionA)
        {
            _doBinaryFunction = functionA;
        }
        
        public double ValueAt(double valueA, double valueB)
        {
            return _doBinaryFunction(valueA, valueB);

        }
    }
}
