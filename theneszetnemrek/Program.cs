using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theneszetnemrek
{
    internal class Program
    {
        static int Koltseg(int[] megoldas)
        {
            int sum = 0;
            for (int teheneszetID = 1; teheneszetID <= megoldas.Length; teheneszetID++)
            {
                int tejuzemID = megoldas[teheneszetID - 1];
                if (tejuzemID != -1)
                {
                    sum += napi_termelt_tejek[teheneszetID - 1] * teheneszetek_tejuzemek_kms_matrix[teheneszetID - 1, tejuzemID - 1];

                }
            }
            return sum;
        }

        static bool Elhelyezheto(int tejuzemID, int teheneszetID)
        {
            if (((defined && Koltseg(jelenlegi_megoldas) < Koltseg(legjobb_megoldas)) || !defined) && napi_feldologozo_kapacitas[tejuzemID - 1] - napi_termelt_tejek[teheneszetID - 1] >= 0)
            {
                return true;
            }

            return false;
        }

        static bool defined;
        static int[] legjobb_megoldas;
        static int[] jelenlegi_megoldas;
        static void Megoldas()
        {
            for (int i = 0; i < jelenlegi_megoldas.Length; i++)
            {
                jelenlegi_megoldas[i] = -1;
            }

            int jelenleg_vizsgalt_teheneszet = 0;

            while (-1 < jelenleg_vizsgalt_teheneszet && jelenleg_vizsgalt_teheneszet < tehenesztek_szama)
            {
                for (int valasztott_tejuzemID = 1; valasztott_tejuzemID <= tejuzemek_szama; valasztott_tejuzemID++)
                {


                    if (Elhelyezheto(valasztott_tejuzemID, jelenleg_vizsgalt_teheneszet + 1))
                    {
                        jelenlegi_megoldas[jelenleg_vizsgalt_teheneszet] = valasztott_tejuzemID;
                        napi_feldologozo_kapacitas[valasztott_tejuzemID - 1] = napi_feldologozo_kapacitas[valasztott_tejuzemID - 1] - napi_termelt_tejek[jelenleg_vizsgalt_teheneszet];
                        jelenleg_vizsgalt_teheneszet++;


                        if (jelenleg_vizsgalt_teheneszet == tehenesztek_szama)
                        {
                            if (!defined || Koltseg(legjobb_megoldas) > Koltseg(jelenlegi_megoldas))
                            {
                                legjobb_megoldas = jelenlegi_megoldas.ToArray();
                                defined = true;
                                jelenleg_vizsgalt_teheneszet = 0;
                            }
                        }
                    }
                    else
                    {
                        jelenlegi_megoldas[jelenleg_vizsgalt_teheneszet] = -1;
                        napi_feldologozo_kapacitas[valasztott_tejuzemID - 1] = napi_feldologozo_kapacitas[valasztott_tejuzemID - 1] - napi_termelt_tejek[jelenleg_vizsgalt_teheneszet];
                        jelenleg_vizsgalt_teheneszet--;
                        break;
                    }
                }
            }
        }

        static int tehenesztek_szama;
        static int tejuzemek_szama;
        static int[] napi_termelt_tejek;
        static int[] napi_feldologozo_kapacitas;

        static int[,] teheneszetek_tejuzemek_kms_matrix;

        static void Main(string[] args)
        {
            //Beolvasás
            string[] temp1 = Console.ReadLine().Split(' ');
            tehenesztek_szama = int.Parse(temp1[0]);
            tejuzemek_szama = int.Parse(temp1[1]);

            string[] temp2 = Console.ReadLine().Split(' ');
            napi_termelt_tejek = new int[temp2.Length];
            for (int i = 0; i < tehenesztek_szama; i++)
            {
                napi_termelt_tejek[i] = int.Parse(temp2[i]);
            }

            string[] temp3 = Console.ReadLine().Split(' ');
            napi_feldologozo_kapacitas = new int[temp3.Length];
            for (int i = 0; i < tejuzemek_szama; i++)
            {
                napi_feldologozo_kapacitas[i] = int.Parse(temp3[i]);
            }

            teheneszetek_tejuzemek_kms_matrix = new int[tehenesztek_szama, tejuzemek_szama];
            for (int i = 0; i < tehenesztek_szama; i++)
            {
                string[] temp4 = Console.ReadLine().Split(' ');
                for (int j = 0; j < tejuzemek_szama; j++)
                {
                    teheneszetek_tejuzemek_kms_matrix[i, j] = int.Parse(temp4[j]);
                }
            }

            //Megoldás
            defined = false;
            jelenlegi_megoldas = new int[tehenesztek_szama];
            legjobb_megoldas = new int[tehenesztek_szama];

            Megoldas();

            Console.WriteLine(Koltseg(legjobb_megoldas));
            Console.WriteLine(string.Join(" ", legjobb_megoldas));

            Console.ReadKey();
        }
    }
}
