using System;
using FuzzySet;
using Neizraziti;

namespace Boatich
{
    public class KormiloFuzzySystemMin : FuzzySystem
    {
        private Defuzzifier def;

        public KormiloFuzzySystemMin(Defuzzifier def)
        {
            this.def = def;
            KormiloRuleBook();
        }


        public override int Zakljuci(int L, int D, int LK, int DK, int V, int S)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-90, 90),
                StandardFuzzySets.EmptyFunction()
            );

            foreach (Rule rule in KormiloRules)
            {
                union = Operations.Operations.BinaryOperation(
                union, rule.SingleRuleConclusion(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
                //Print(union, "union foreach:");
            }

            return def.Defuzzyfy(union);
        }

        public override int ZakljuciProd(int L, int D, int LK, int DK, int V, int S)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-90, 90),
                StandardFuzzySets.EmptyFunction()
            );

            foreach (Rule rule in KormiloRules)
            {
                union = Operations.Operations.BinaryOperation(
                union, rule.SingleRuleConclusionProduct(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
                //Print(union, "union foreach:");
            }

            return def.Defuzzyfy(union);
        }

        public override void ZakljuciPrint(int L, int D, int LK, int DK, int V, int S, Rule rule)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-90, 90),
                StandardFuzzySets.EmptyFunction()
            );


            union = Operations.Operations.BinaryOperation(
            union, rule.SingleRuleConclusion(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
            Print(union, "Zakljucak:");


            int rez = def.Defuzzyfy(union);
            Console.WriteLine("Dekodirana vrijednost: {0}", rez);
            return;
        }


        private void KormiloRuleBook()
        {
            KormiloRules.Add(
                 new Rule(
                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                             ),       

                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                             1)
                             ),   

                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                             ),      

                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                             1)
                             ),    

                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                             Rule.UANTEC.IndexOfElement(DomainElement.Of(60)))  
                             ),

                         Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                             1)
                             ),      

                         Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.LambdaFunction(
                             Rule.UKOR.IndexOfElement(DomainElement.Of(-60)),
                             Rule.UKOR.IndexOfElement(DomainElement.Of(-45)),
                             Rule.UKOR.IndexOfElement(DomainElement.Of(-30)))  //umjeren udesno
                             )
                     )
                 );


            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),  

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(50)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.GammaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(70)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(90)))
                            ),       

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(75)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(135)))  
                            ),     

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),     

                        Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.LambdaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-90)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-80)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-55)))  //ostro udesno
                            )
                    )
                );



            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),     

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ), 

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(60)))  
                            ),

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.LambdaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(30)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(45)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(60)))  //umjeren ulijevo
                            )
                    )
                );


            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),  

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.GammaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(70)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(90)))
                            ),       

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(50)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(75)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(135)))  
                            ),

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),     

                        Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.LambdaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(55)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(80)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(90)))  //ostro ulijevo
                            )
                    )
                );





            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(30)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(50)))
                            ),       

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),  

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(30)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(60)))
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),     

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)  
                            ),     

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.LFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-80)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-25)))  //ostro udesno vrlo blizu lijevo neovisno o brzini
                            )
                    )
                );


            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(30)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(50)))
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),  

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(30)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(60)))
                            ),

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleConsequensKor(StandardFuzzySets.GammaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(25)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(80)))  //ostro ulijevo vrlo blizu desno neovisno o brzini
                            )
                    )
                );


            KormiloRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),     

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-2)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(0)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(1)))  
                            ),

                         Rule.MakeFuzzyRuleConsequensAcc(StandardFuzzySets.LambdaFunction(
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-90)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-85)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-80)))
                            )                                                               //rotiraj
                    )
                );
            
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