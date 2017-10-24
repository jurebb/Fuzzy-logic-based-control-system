using FuzzySet;
using Neizraziti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class Operations
    {
        public Operations()
        {

        }

        public static IFuzzySet UnaryOperation(IFuzzySet fuzzySet, IUnaryFunction unary)
        {
            MutableFuzzySet A = new MutableFuzzySet(fuzzySet.GetDomain());
            foreach (DomainElement e in fuzzySet.GetDomain())
            {
                A.Set(e, unary.ValueAt(fuzzySet.GetValueAt(e)));
            }
            return A;
        }

        public static IFuzzySet BinaryOperation(IFuzzySet fuzzySetA, IFuzzySet fuzzySetB, IBinaryFunction binary)
        {
            MutableFuzzySet A = new MutableFuzzySet(fuzzySetA.GetDomain());
            if(fuzzySetA.GetDomain() != fuzzySetB.GetDomain())
            {
                Console.WriteLine("Can't do binary operation on sets with different domains");
                return A;
            }

            foreach (DomainElement e in fuzzySetA.GetDomain())
            {
                A.Set(e, binary.ValueAt(fuzzySetA.GetValueAt(e), fuzzySetB.GetValueAt(e)));
            }
            return A;
        }

        public static IUnaryFunction ZadehNot()
        {
            ConcreteUnaryFunction Obj1 = new ConcreteUnaryFunction((x) =>
            {
                return 1 - x;
            });
            return Obj1;
        }

        public static IBinaryFunction ZadehAnd()
        {
            ConcreteBinaryFunction Obj1 = new ConcreteBinaryFunction((x, y) =>
            {
                return Math.Min(x, y);
            });
            return Obj1;
        }

        public static IBinaryFunction ZadehOr()
        {
            ConcreteBinaryFunction Obj1 = new ConcreteBinaryFunction((x, y) =>
            {
                return Math.Max(x, y);
            });
            return Obj1;
        }

        public static IBinaryFunction HamacherTNorm(double param)
        {
            ConcreteBinaryFunction Obj1 = new ConcreteBinaryFunction((x, y) =>
            {
                if (param >= 0)
                {
                    return (double)(x * y) / (param + (1 - param) * (x + y - x * y));
                }
                else
                {
                    Console.WriteLine("Param must be >= 0!");
                    return 0;
                }
            });
            return Obj1;
        }

        public static IBinaryFunction HamacherSNorm(double param)
        {
            ConcreteBinaryFunction Obj1 = new ConcreteBinaryFunction((x, y) =>
            {
                if (param >= 0)
                {
                    return (double)(x + y - (2 - param)*x*y) / (1 - (1 - param)*x*y);
                }
                else
                {
                    Console.WriteLine("Param must be >= 0!");
                    return 0;
                }
            });
            return Obj1;
        }
    }
}
