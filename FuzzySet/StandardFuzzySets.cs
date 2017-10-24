using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySet
{
    public class StandardFuzzySets
    {
        public StandardFuzzySets()
        {

        }

        public static IIntUnaryFunction LFunction(int a, int b)
        {
            ConcreteIntUnaryFunction Obj1 = new ConcreteIntUnaryFunction((x) =>
            {
                if (x < a)
                {
                    return 1;
                }
                else if (a <= x && x < b)
                {
                    return (double)(b - x) / (b - a);
                }
                else
                {
                    return 0;
                }
            });
            return Obj1;
        }

        public static IIntUnaryFunction GammaFunction(int a, int b)
        {
            ConcreteIntUnaryFunction Obj1 = new ConcreteIntUnaryFunction((x) =>
            {
                if (x < a)
                {
                    return 0;
                }
                else if (a <= x && x < b)
                {
                    return (double)(x - a) / (b - a);
                }
                else
                {
                    return 1;
                }
            });
            return Obj1;
        }

        public static IIntUnaryFunction LambdaFunction(int a, int b, int c)
        {
            ConcreteIntUnaryFunction Obj1 = new ConcreteIntUnaryFunction((x) => 
            {
                if(a<=x && x<b)
                {
                    return (double)(x - a) / (b - a);
                }
                else if(b<=x && x<c)
                {
                    return (double)(c - x) / (c - b);
                }
                else
                {
                    return 0;
                }
            });
            return Obj1;
        }
    }
}
