using System;
using FuzzySet;

namespace Boatich
{
    public interface Defuzzifier
    {
        int Defuzzyfy(IFuzzySet union);
    }
}