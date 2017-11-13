using FuzzySet;
using Neizraziti;
using System;
using Operations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relations;

namespace Debug
{
    class Debug1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi");

            //Primjer11();
            //Primjer12();
            //Primjer13();
            //Primjer21();
            //Primjer22();
            //Primjer23();

            Test33();

            Console.ReadKey();
        }


        static void Primjer11()
        {
            Console.WriteLine("Hi");
            IDomain d1 = Domain.IntRange(0, 5);
            Debug1.Print(d1, "Elementi domene d1:");

            IDomain d2 = Domain.IntRange(0, 3);
            Debug1.Print(d2, "Elementi domene d2:");

            IDomain d3 = Domain.Combine(d1, d2);
            Debug1.Print(d3, "Elementi domene d3:");

            //IDomain d4 = Domain.Combine(d1, d3);
            //Debug1.Print(d4, "Elementi domene d4:");              

            Console.WriteLine(d3.ElementForIndex(0));
            Console.WriteLine(d3.ElementForIndex(5));
            Console.WriteLine(d3.ElementForIndex(14));
            Console.WriteLine(d3.IndexOfElement(DomainElement.Of(4, 1)));

        }

        static void Primjer12()
        {
            IDomain d = Domain.IntRange(0, 11); // {0,1,...,10}
            //Debug1.Print(d, "Elementi domene d1:");

            IFuzzySet set1 = new MutableFuzzySet(d)
            .Set(DomainElement.Of(0), 1.0)
            .Set(DomainElement.Of(1), 0.8)
            .Set(DomainElement.Of(2), 0.6)
            .Set(DomainElement.Of(3), 0.4)
            .Set(DomainElement.Of(4), 0.2);
            Debug1.Print(set1, "Set1:");

            IDomain d2 = Domain.IntRange(-5, 6); // {-5,-4,...,4,5}
            IFuzzySet set2 = new CalculatedFuzzySet(
            d2,
            StandardFuzzySets.LambdaFunction(
                d2.IndexOfElement(DomainElement.Of(-4)),
                d2.IndexOfElement(DomainElement.Of(0)),
                d2.IndexOfElement(DomainElement.Of(4))
            )
            );
            Debug1.Print(set2, "Set2:");


        }

        static void Primjer13()
        {
            IDomain d = Domain.IntRange(0, 11);
            IFuzzySet set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            Debug1.Print(set1, "Set1:");

            IFuzzySet notSet1 = Operations.Operations.UnaryOperation(
                set1, Operations.Operations.ZadehNot());
            Debug1.Print(notSet1, "notSet1:");



            IFuzzySet union = Operations.Operations.BinaryOperation(
                set1, notSet1, Operations.Operations.ZadehOr());
            Debug1.Print(union, "Set1 union notSet1:");

            IFuzzySet hinters = Operations.Operations.BinaryOperation(
                set1, notSet1, Operations.Operations.HamacherTNorm(1.0));
            Debug1.Print(hinters, "Set1 intersection with notSet1 using parameterised Hamacher T norm with parameter 1.0:");

            IFuzzySet presjek = Operations.Operations.BinaryOperation(set1, notSet1, Operations.Operations.ZadehAnd());
            Debug1.Print(presjek, "Set1 presjek notSet1:");


        }




        public static void Primjer21()
        {
            IDomain u = Domain.IntRange(1, 6); // {1,2,3,4,5}
            IDomain u2 = Domain.Combine(u, u);

            IFuzzySet r1 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(3, 1), 0.5)
                .Set(DomainElement.Of(1, 3), 0.5);

            IFuzzySet r2 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(3, 1), 0.5)
                .Set(DomainElement.Of(1, 3), 0.1);

            IFuzzySet r3 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 0.3)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(1, 2), 0.6)
                .Set(DomainElement.Of(2, 1), 0.6)
                .Set(DomainElement.Of(2, 3), 0.7)
                .Set(DomainElement.Of(3, 2), 0.7)
                .Set(DomainElement.Of(3, 1), 0.5)
                .Set(DomainElement.Of(1, 3), 0.5);

            IFuzzySet r4 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(1, 2), 0.4)
                .Set(DomainElement.Of(2, 1), 0.4)
                .Set(DomainElement.Of(2, 3), 0.5)
                .Set(DomainElement.Of(3, 2), 0.5)
                .Set(DomainElement.Of(1, 3), 0.4)
                .Set(DomainElement.Of(3, 1), 0.4);

            bool test1 = Relations.Relations.IsUtimesURelation(r1);
            Console.WriteLine("r1 je definiran nad UxU? " + test1);
            bool test2 = Relations.Relations.IsSymmetric(r1);
            Console.WriteLine("r1 je simetrična? " + test2);
            bool test3 = Relations.Relations.IsSymmetric(r2);
            Console.WriteLine("r2 je simetrična? " + test3);
            bool test4 = Relations.Relations.IsReflexive(r1);
            Console.WriteLine("r1 je refleksivna? " + test4);
            bool test5 = Relations.Relations.IsReflexive(r3);
            Console.WriteLine("r3 je refleksivna? " + test5);
            bool test6 = Relations.Relations.IsMaxMinTransitive(r3);
            Console.WriteLine("r3 je max-min tranzitivna? " + test6);
            bool test7 = Relations.Relations.IsMaxMinTransitive(r4);
            Console.WriteLine("r4 je max-min tranzitivna? " + test7);

        }

        public static void Primjer22()
        {
            IDomain u1 = Domain.IntRange(1, 5); // {1,2,3,4}
            IDomain u2 = Domain.IntRange(1, 4); // {1,2,3}
            IDomain u3 = Domain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r1 = new MutableFuzzySet(Domain.Combine(u1, u2))
                .Set(DomainElement.Of(1, 1), 0.3)
                .Set(DomainElement.Of(1, 2), 1)
                .Set(DomainElement.Of(3, 3), 0.5)
                .Set(DomainElement.Of(4, 3), 0.5);
            IFuzzySet r2 = new MutableFuzzySet(Domain.Combine(u2, u3))
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 1), 0.5)
                .Set(DomainElement.Of(2, 2), 0.7)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(3, 4), 0.4);

            IFuzzySet r1r2 = Relations.Relations.CompositionOfBinaryRelations(r1, r2, Operations.Operations.ZadehAnd(), Operations.Operations.ZadehOr());

            foreach (DomainElement e in r1r2.GetDomain())
            {
                Console.WriteLine("mu(" + e + ")=" + r1r2.GetValueAt(e));
            }

        }

        public static void Primjer23()
        {
            IDomain u = Domain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r = new MutableFuzzySet(Domain.Combine(u, u))
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(1, 2), 0.3)
                .Set(DomainElement.Of(2, 1), 0.3)
                .Set(DomainElement.Of(2, 3), 0.5)
                .Set(DomainElement.Of(3, 2), 0.5)
                .Set(DomainElement.Of(3, 4), 0.2)
                .Set(DomainElement.Of(4, 3), 0.2);

            IFuzzySet r2 = r;
            Console.WriteLine("Početna relacija je neizrazita relacija ekvivalencije? " + Relations.Relations.IsFuzzyEquivalence(r2));
            Console.WriteLine();

            for (int i = 1; i <= 3; i++)
            {
                r2 = Relations.Relations.CompositionOfBinaryRelations(r2, r, Operations.Operations.ZadehAnd(), Operations.Operations.ZadehOr());
                Console.WriteLine("Broj odrađenih kompozicija: " + i + ". Relacija je:");
                foreach (DomainElement e in r2.GetDomain())
                {
                    Console.WriteLine("mu(" + e + ")=" + r2.GetValueAt(e));
                }
                Console.WriteLine("Ova relacija je neizrazita relacija ekvivalencije? " + Relations.Relations.IsFuzzyEquivalence(r2));
                Console.WriteLine();
            }

        }


        public static void Test31()
        {
            IDomain u = Domain.IntRange(-5, 6); // {-5,-4,...,4,5}
            IDomain u2 = Domain.Combine(u, u);

            IFuzzySet set = new CalculatedFuzzySet(
            u2,
            StandardFuzzySets.LambdaFunction(
                u2.IndexOfElement(DomainElement.Of(-4, 2)),
                u2.IndexOfElement(DomainElement.Of(0, 0)),
                u2.IndexOfElement(DomainElement.Of(4, 2))
            )
            );

            Debug1.Print(set, "Set cf:");
        }

        public static void Test32()
        {
            IDomain u = Domain.IntRange(-5, 6); // {-5,-4,...,4,5}
            IDomain u2 = Domain.Combine(u, u);

            IFuzzySet set = new CalculatedFuzzySet(
            u2,
            StandardFuzzySets.LambdaFunction(
                u2.IndexOfElement(DomainElement.Of(-4, 2)),
                u2.IndexOfElement(DomainElement.Of(0, 0)),
                u2.IndexOfElement(DomainElement.Of(4, 2))
            )
            );

            Debug1.Print(set, "Set cf:");

            IFuzzySet notSet1 = Operations.Operations.UnaryOperation(
                set, Operations.Operations.ZadehNot());
            Debug1.Print(notSet1, "notSet1:");



            IFuzzySet union = Operations.Operations.BinaryOperation(
                set, notSet1, Operations.Operations.ZadehOr());
            Debug1.Print(union, "Set1 union notSet1:");
        }

        public static void Test33()
        {

            double[] antecMembershipFunctions = new double[] {
                0.8, 0.8, 0.7, 1, 0.7, 0.9
            };

            double minMembershipFunc = antecMembershipFunctions.Min();

            IDomain u = Domain.IntRange(-5, 6);

            IFuzzySet _conseq = new CalculatedFuzzySet(
            u,
            StandardFuzzySets.LambdaFunction(
                u.IndexOfElement(DomainElement.Of(-5)),
                u.IndexOfElement(DomainElement.Of(-3)),
                u.IndexOfElement(DomainElement.Of(0))
            )
            );

            Debug1.Print(_conseq, "Konsekv1:");


            IFuzzySet antecedents = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                _conseq.GetDomain(),
                StandardFuzzySets.UniversalFunction(minMembershipFunc)
            );

            IFuzzySet conclusion = Operations.Operations.BinaryOperation(antecedents, _conseq, Operations.Operations.ZadehAnd());
            Debug1.Print(conclusion, "Zakljucak1:");
        }


        public static void Print(IDomain domain, string headingText)    //metoda za ispis elemenata domene
        {
            if (headingText != null)
            {
                Console.WriteLine(headingText);
            }
            foreach (DomainElement e in domain)
            {
                Console.WriteLine("Element domene: " + e);
            }
            Console.WriteLine("Kardinalitet domene je: " + domain.GetCardinality());
            Console.WriteLine();
        }

        public static void Print(IFuzzySet set, string headingText)    //metoda za ispis neizr skupa
        {
            if (headingText != null)
            {
                Console.WriteLine(headingText);
            }
            foreach (DomainElement e in set.GetDomain())
            {
                Console.WriteLine("D{0} = {1}", e, set.GetValueAt(e));
            }
            Console.WriteLine();
        }
    }
}

