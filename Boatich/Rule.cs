using FuzzySet;
using Neizraziti;
using Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boatich
{
    public class Rule
    {
        IFuzzySet _antec1, _antec2, _antec3, _antec4, _antec5, _antec6, _conseq;
        static IDomain u = Domain.IntRange(0, 1300);
        public static IDomain UANTEC = Domain.IntRange(0, 1300);
        public static IDomain UKOR = Domain.IntRange(-90, 90);
        public static IDomain UACC = Domain.IntRange(-100, 100);


        public Rule(IFuzzySet antec1, IFuzzySet antec2, IFuzzySet antec3, IFuzzySet antec4, IFuzzySet antec5, IFuzzySet antec6, IFuzzySet conseq)
        {
            _antec1 = antec1;
            _antec2 = antec2;
            _antec3 = antec3;
            _antec4 = antec4;
            _antec5 = antec5;
            _antec6 = antec6;
            _conseq = conseq;

            //ne radimo t norme i kart. prod?
            //metoda valueat(x1, x2, ..., x6) {return double[] {pripadnosti ...}}
            //metodda RuleZakljuci (za pojedini rule)?
        }

        public static IFuzzySet MakeFuzzyRuleAntecedent(IIntUnaryFunction function)       //primjerice, posaljemo lambda( u2.IndexOfElement(DomainElement.Of(250, 31, 21, ...), ..., ...)
        {

            IFuzzySet antec = new CalculatedFuzzySet(
            u,
            function
            );

            return antec;
        }

        public static IFuzzySet MakeFuzzyRuleConsequensAcc(IIntUnaryFunction function)       //primjerice, posaljemo lambda( u2.IndexOfElement(DomainElement.Of(250, 31, 21, ...), ..., ...)
        {
            IDomain uacc = Domain.IntRange(-100, 100);

            IFuzzySet conseq = new CalculatedFuzzySet(
            uacc,
            function
            );

            return conseq;
        }

        public static IFuzzySet MakeFuzzyRuleConsequensKor(IIntUnaryFunction function)       //primjerice, posaljemo lambda( u2.IndexOfElement(DomainElement.Of(250, 31, 21, ...), ..., ...)
        {
            IDomain ukor = Domain.IntRange(-90, 90);

            IFuzzySet conseq = new CalculatedFuzzySet(
            ukor,
            function
            );

            return conseq;
        }

        public IFuzzySet SingleRuleConclusion(int L, int D, int LK, int DK, int V, int S)       //TODO napravi varijantu i za StrojMinimum i za StrojProdukt
        {
            double[] antecMembershipFunctions = new double[] {
                _antec1.GetValueAt(DomainElement.Of(L)),
                _antec2.GetValueAt(DomainElement.Of(D)),
                _antec3.GetValueAt(DomainElement.Of(LK)),
                _antec4.GetValueAt(DomainElement.Of(DK)),
                _antec5.GetValueAt(DomainElement.Of(V)),
                _antec6.GetValueAt(DomainElement.Of(S))
            };

            double minMembershipFunc = antecMembershipFunctions.Min();

            IFuzzySet antecedents = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                _conseq.GetDomain(),
                StandardFuzzySets.UniversalFunction(minMembershipFunc)
            );

            //KormiloFuzzySystemMin.Print(antecedents, "antecedents:");
            //KormiloFuzzySystemMin.Print(_conseq, "_conseq:");
            //KormiloFuzzySystemMin.Print(_antec5, "_antec:");


            IFuzzySet conclusion = Operations.Operations.BinaryOperation(antecedents, _conseq, Operations.Operations.ZadehAnd());

            //KormiloFuzzySystemMin.Print(conclusion, "conclusion:");

            return conclusion;
        }

        public IFuzzySet SingleRuleConclusionProduct(int L, int D, int LK, int DK, int V, int S)       //TODO napravi varijantu i za StrojMinimum i za StrojProdukt
        {
            double[] antecMembershipFunctions = new double[] {
                _antec1.GetValueAt(DomainElement.Of(L)),
                _antec2.GetValueAt(DomainElement.Of(D)),
                _antec3.GetValueAt(DomainElement.Of(LK)),
                _antec4.GetValueAt(DomainElement.Of(DK)),
                _antec5.GetValueAt(DomainElement.Of(V)),
                _antec6.GetValueAt(DomainElement.Of(S))
            };

            double prodMembershipFunc = 1;

            foreach( double var in antecMembershipFunctions)
            {
                prodMembershipFunc *= var;
            }

            IFuzzySet antecedents = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                _conseq.GetDomain(),
                StandardFuzzySets.UniversalFunction(prodMembershipFunc)
            );


            //KormiloFuzzySystemMin.Print(antecedents, "antecedents:");
            //KormiloFuzzySystemMin.Print(_conseq, "_conseq:");
            //KormiloFuzzySystemMin.Print(_antec5, "_antec:");


            IFuzzySet conclusion = Operations.Operations.BinaryOperation(antecedents, _conseq, Operations.Operations.AlgProduct());

            //KormiloFuzzySystemMin.Print(conclusion, "conclusion:");

            return conclusion;
        }
    }
}
