using FuzzySet;
using Neizraziti;
using Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Relations
{
    public class Relations
    {
        public Relations()
        {

        }

        public static bool IsSymmetric(IFuzzySet relation)
        {
            foreach(DomainElement element1 in relation.GetDomain())
            {
                if (relation.GetValueAt(DomainElement.Of(element1.GetComponentValue(0), element1.GetComponentValue(1))) != relation.GetValueAt(DomainElement.Of(element1.GetComponentValue(1), element1.GetComponentValue(0))))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsReflexive(IFuzzySet relation)
        {
            foreach (DomainElement element1 in relation.GetDomain())
            {
                if (element1.GetComponentValue(0) == element1.GetComponentValue(1))
                {
                    if (relation.GetValueAt(DomainElement.Of(element1.GetComponentValue(0), element1.GetComponentValue(1))) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsMaxMinTransitive(IFuzzySet relation)
        {
            IFuzzySet copiedRelation = DeepCopy(relation);              //jer ugnjezdjeni foreach nad istom 'relation' instancom stvara probleme
            foreach (DomainElement element1 in relation.GetDomain())
            {
                int x = element1.GetComponentValue(0);
                int z = element1.GetComponentValue(1);
                double funcxz = relation.GetValueAt(DomainElement.Of(x, z));
                double Max = 0;
                foreach (DomainElement element2 in copiedRelation.GetDomain())
                {
                    if (element2.GetComponentValue(0) == element2.GetComponentValue(1))
                    {
                        int y = element2.GetComponentValue(0);
                        if (Math.Min(copiedRelation.GetValueAt(DomainElement.Of(x, y)), copiedRelation.GetValueAt(DomainElement.Of(y, z))) > Max)
                        {
                            Max = Math.Min(copiedRelation.GetValueAt(DomainElement.Of(x, y)), copiedRelation.GetValueAt(DomainElement.Of(y, z)));
                        }
                    }
                }
                if(funcxz < Max)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsUtimesURelation(IFuzzySet relation)
        {
            if(relation.GetDomain().GetNumberOfComponents() == 2 )
            {
                if(relation.GetDomain().GetComponent(0) == relation.GetDomain().GetComponent(1))
                {
                    return true;
                }
            }
            return false;
        }

        public static IFuzzySet CompositionOfBinaryRelations(IFuzzySet relation1, IFuzzySet relation2, IBinaryFunction tNorm, IBinaryFunction sNorm)    //zadatak ne specificira koristi li se max-min kompozicija 
        {                                                                                                                                               //stoga je konfigurabilno
            MutableFuzzySet compositeRelation = new MutableFuzzySet(Domain.Combine(relation1.GetDomain().GetComponent(0), relation2.GetDomain().GetComponent(1)));

            if (relation1 == relation2)             //jer ugnjezdjeni foreach nad istom 'relation' instancom stvara probleme
            {
                relation2 = DeepCopy(relation1);
            }

            foreach (DomainElement element1 in compositeRelation.GetDomain())
            {
                int x = element1.GetComponentValue(0);
                int z = element1.GetComponentValue(1);
                double funccomp = compositeRelation.GetValueAt(DomainElement.Of(x, z));
                double Max = 0;

                foreach (DomainElement element_a in relation1.GetDomain())          //funca
                {
                    foreach (DomainElement element_b in relation2.GetDomain())      //funcb
                    {
                        if (element_a.GetComponentValue(1) == element_b.GetComponentValue(0))         
                        {
                            int y = element_a.GetComponentValue(1);                 //y
                            Max = sNorm.ValueAt(tNorm.ValueAt(relation1.GetValueAt(DomainElement.Of(x, y)), relation2.GetValueAt(DomainElement.Of(y, z))), Max);
                        }
                    }
                }

                compositeRelation.Set(DomainElement.Of(x, z), Max);
            }

            return compositeRelation;
        }

        public static bool IsFuzzyEquivalence(IFuzzySet relation)
        {
            return (IsReflexive(relation) && IsSymmetric(relation) && IsMaxMinTransitive(relation));
        }


        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
