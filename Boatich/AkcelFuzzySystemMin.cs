using System;
using FuzzySet;
using Neizraziti;

namespace Boatich
{
    public class AkcelFuzzySystemMin : FuzzySystem
    {
        private Defuzzifier def;

        public AkcelFuzzySystemMin(Defuzzifier def)
        {
            this.def = def;
            AkcelRuleBook();
        }

        
        public override int Zakljuci(int L, int D, int LK, int DK, int V, int S)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-100, 100),
                StandardFuzzySets.EmptyFunction()
            );

            foreach (Rule rule in AkcelRules)
            {
                union = Operations.Operations.BinaryOperation(
                union, rule.SingleRuleConclusion(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
                //KormiloFuzzySystemMin.Print(union, "union foreach:");
            }

            return def.Defuzzyfy(union);
        }

        public override int ZakljuciProd(int L, int D, int LK, int DK, int V, int S)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-100, 100),
                StandardFuzzySets.EmptyFunction()
            );

            foreach (Rule rule in AkcelRules)
            {
                union = Operations.Operations.BinaryOperation(
                union, rule.SingleRuleConclusionProduct(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
                //KormiloFuzzySystemMin.Print(union, "union foreach:");
            }

            return def.Defuzzyfy(union);
        }


        public override void ZakljuciPrint(int L, int D, int LK, int DK, int V, int S, Rule rule)         //TODO bude li trebala diferencijacija, svaki zakljuci u svoju klasu
        {
            IFuzzySet union = new CalculatedFuzzySet(                                     //mjere pripadnosti su na minimumu mj.pripadnosti antecedenata
                Domain.IntRange(-100, 100),
                StandardFuzzySets.EmptyFunction()
            );


            union = Operations.Operations.BinaryOperation(
            union, rule.SingleRuleConclusion(L, D, LK, DK, V, S), Operations.Operations.ZadehOr());         //TODO IMPORTANT postoji li efikasniji nacin nego doslovno unija svega?
            KormiloFuzzySystemMin.Print(union, "Zakljucak:");
            
            int rez = def.Defuzzyfy(union);
            Console.WriteLine("Dekodirana vrijednost: {0}", rez);
            return;
        }



        private void AkcelRuleBook()
        {


            AkcelRules.Add(
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

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(50)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(75)))
                            ),      

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      

                        Rule.MakeFuzzyRuleConsequensAcc(StandardFuzzySets.LambdaFunction(
                            Rule.UACC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(10)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(20)))
                            )                                                                       //ubrzaj do 75
                    )
                );

            AkcelRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       //vrlo blizu obali lijevo

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   //nebitno desno

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       //vrlo blizu LK

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.GammaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(200)))
                            ),       //umjereno blizu LK

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(65)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(135)))  //do cca 90 px/s
                            ),     //velika brzina

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      //nebitno S??     //TODO mozda popraviti domenu od S

                         Rule.MakeFuzzyRuleConsequensAcc(StandardFuzzySets.LambdaFunction(
                            Rule.UACC.IndexOfElement(DomainElement.Of(-30)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(-22)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(0)))
                            )                                                               //zavoj
                    )
                );

            AkcelRules.Add(
                new Rule(
                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),   //nebitno lijevo

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       //umjereno blizu desno

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.GammaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(200)))
                            ),       //umjereno blizu LK

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(0)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(80)))
                            ),       //umjereno blizu DK

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.LambdaFunction(
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(35)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(65)),
                            Rule.UANTEC.IndexOfElement(DomainElement.Of(135)))  //do cca 90 px/s
                            ),

                        Rule.MakeFuzzyRuleAntecedent(StandardFuzzySets.UniversalFunction(
                            1)
                            ),      //nebitno S??     //TODO mozda popraviti domenu od S

                         Rule.MakeFuzzyRuleConsequensAcc(StandardFuzzySets.LambdaFunction(
                            Rule.UACC.IndexOfElement(DomainElement.Of(-30)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(-22)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(0)))
                            )                                                               //zavoj
                    )
                );

            AkcelRules.Add(
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
                            Rule.UKOR.IndexOfElement(DomainElement.Of(-1)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(0)),
                            Rule.UKOR.IndexOfElement(DomainElement.Of(1)))
                            ),

                         Rule.MakeFuzzyRuleConsequensAcc(StandardFuzzySets.LambdaFunction(
                            Rule.UACC.IndexOfElement(DomainElement.Of(-30)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(-22)),
                            Rule.UACC.IndexOfElement(DomainElement.Of(0)))
                            )                                                               //rotiraj
                    )
                );


           
        }
    }
}