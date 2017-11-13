using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boatich
{
    class Program
    {
        static void Main(string[] args)
        {
            int L, D, LK, DK, V, S, A, K;

            // Biramo način dekodiranja neizrazitosti:
            Defuzzifier def = new COADefuzzifier();
            // Stvaranje oba sustava:
            // Grade se baze pravila i sve se inicijalizira
            FuzzySystem fsAkcel = new AkcelFuzzySystemMin(def);
            FuzzySystem fsKormilo = new KormiloFuzzySystemMin(def);
            // Glavna petlja:

             while (true)
            {
                String str = Console.ReadLine();
                if (str[0] == 'K') break;
                else if (str[0] == 'p')
                {
                    Pravilo(def, fsAkcel, fsKormilo);
                    break;
                }
                else if (str[0] == 'm')
                {
                    Manual(def, fsAkcel, fsKormilo);
                    break;
                }
                String[] p = str.Split(' ');
                L = int.Parse(p[0]);
                D = int.Parse(p[1]);
                LK = int.Parse(p[2]);
                DK = int.Parse(p[3]);
                V = int.Parse(p[4]);
                S = int.Parse(p[5]);

                // Zadaj ulaze, generiraj neizraziti izlaz, dekodiraj i vrati ga:
                A = fsAkcel.Zakljuci(L, D, LK, DK, V, S);
                //A = 4;
                K = fsKormilo.Zakljuci(L, D, LK, DK, V, S);
                //K = 5;
                //akcel = 10; kormilo = 5;
                Console.Write(A.ToString() + " " + K.ToString() + "\r\n");
                Console.Out.Flush();
            }
        }

        private static void Manual(Defuzzifier def, FuzzySystem fsAkcel, FuzzySystem fsKormilo)
        {
            FuzzySystem.ManualSystem(def, fsAkcel, fsKormilo);
        }

        private static void Pravilo(Defuzzifier def, FuzzySystem fsAkcel, FuzzySystem fsKormilo)
        {
            Console.WriteLine("Unesite slovo baze (kor/akc) i redni broj pravila: (npr. 'k2') ");
            string pravilo = Console.ReadLine();

            FuzzySystem.ManualSingleRule(pravilo, def, fsAkcel, fsKormilo);
            Console.ReadKey();
        }
    }
}
