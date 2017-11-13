using FuzzySet;
using Boatich;
using System;
using Neizraziti;

namespace Boatich
{
    class COADefuzzifier : Defuzzifier
    {
        public int Defuzzyfy(IFuzzySet union)
        {
            double sum1 = 0;
            double sum2 = 0;

            foreach(DomainElement element in union.GetDomain())
            {
                sum1 += element.GetComponentValue(0) * union.GetValueAt(element);
                sum2 += union.GetValueAt(element);
            }

            double CoA = sum1 / sum2;

            if (sum2 < 0.00001)
                return 0;

            return (int)CoA;
        }
    }
}