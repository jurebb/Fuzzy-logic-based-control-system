using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boatich
{
    public abstract class FuzzySystem
    {
        internal List<Rule> KormiloRules = new List<Rule>(11);
        internal List<Rule> AkcelRules = new List<Rule>(11);

        public abstract int Zakljuci(int L, int D, int LK, int DK, int V, int S);
        public abstract int ZakljuciProd(int L, int D, int LK, int DK, int V, int S);
        public abstract void ZakljuciPrint(int L, int D, int LK, int DK, int V, int S, Rule rule);


        public static void ManualSystem(Defuzzifier def, FuzzySystem fsAkcel, FuzzySystem fsKormilo)
        {
            int L, D, LK, DK, V, S, A, K;

            while (true)
            {
                Console.WriteLine("Unesi L D LK DK V S        ||  ili 'K' za kraj");
                String str = Console.ReadLine();

                if (str[0] == 'K') break;

                String[] p = str.Split(' ');

                L = int.Parse(p[0]);
                D = int.Parse(p[1]);
                LK = int.Parse(p[2]);
                DK = int.Parse(p[3]);
                V = int.Parse(p[4]);
                S = int.Parse(p[5]);


                A = fsAkcel.Zakljuci(L, D, LK, DK, V, S);
                //A = 4;
                K = fsKormilo.Zakljuci(L, D, LK, DK, V, S);
                //K = 5;
                //akcel = 10; kormilo = 5;
                Console.Write("Akcel:" + A.ToString() + " Kormilo:" + K.ToString() + "\r\n");
                Console.Out.Flush();
            }
        }

        internal static void ManualSingleRule(string pravilo, Defuzzifier def, FuzzySystem fsAkcel, FuzzySystem fsKormilo)
        {
            int L, D, LK, DK, V, S, A, K;


            if (pravilo[0] == 'a' || pravilo[0] == 'A')
            {
                int redni = int.Parse(pravilo[1].ToString());

                Console.WriteLine("Unesi L D LK DK V S");
                String str = Console.ReadLine();
                String[] p = str.Split(' ');

                L = int.Parse(p[0]);
                D = int.Parse(p[1]);
                LK = int.Parse(p[2]);
                DK = int.Parse(p[3]);
                V = int.Parse(p[4]);
                S = int.Parse(p[5]);

                fsAkcel.ZakljuciPrint(L, D, LK, DK, V, S, fsAkcel.AkcelRules[redni]);
            }

            else if (pravilo[0] == 'k' || pravilo[0] == 'K')
            {
                int redni = int.Parse(pravilo[1].ToString());

                Console.WriteLine("Unesi L D LK DK V S");
                String str = Console.ReadLine();
                String[] p = str.Split(' ');

                L = int.Parse(p[0]);
                D = int.Parse(p[1]);
                LK = int.Parse(p[2]);
                DK = int.Parse(p[3]);
                V = int.Parse(p[4]);
                S = int.Parse(p[5]);

                fsKormilo.ZakljuciPrint(L, D, LK, DK, V, S, fsKormilo.KormiloRules[redni]);
            }
        }
    }
}
