using System;
using System.IO;

namespace bingo
{
    class BingoJatekos // 2
    {
        private string nev;
        private string[,] szamok;
        private bool[,] talalt;

        public string Nev { get => nev; set => nev = value; }

        public BingoJatekos(string jatekos)
        {
            szamok = new string[5, 5];
            talalt = new bool[5, 5];
            talalt[2, 2] = true;
            Nev = jatekos.Split('.')[0];
            int sorI = 0;
            foreach (var sor in File.ReadAllLines(jatekos))
            {
                string[] szamokSplit = sor.Split(';');
                int oszlopI = 0;
                foreach (var szam in szamokSplit) szamok[sorI, oszlopI++] = szam;
                sorI++;
            }
        }

        // 5
        public void Sorsol(int sorsoltSzam)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (szamok[i, j] == sorsoltSzam.ToString())
                    {
                        talalt[i, j] = true;
                        return;
                    }
                }
        }

        public void Kiir()
        {
            Console.WriteLine(Nev);
            for (int sor = 0; sor < 5; sor++)
            {
                for (int oszlop = 0; oszlop < 5; oszlop++)
                {
                    if (talalt[sor, oszlop])
                    {
                        if (szamok[sor, oszlop].Length == 2)
                        {
                            Console.Write(szamok[sor, oszlop] + " ");
                        }
                        else Console.Write(szamok[sor, oszlop] + "  ");
                    }
                    else Console.Write("0  ");
                }
                Console.WriteLine();
            }
        }

        // 6
        public bool Ell
        {
            get
            {
                for (int sor = 0; sor < 5; sor++)
                {
                    if (talalt[sor, 0] && talalt[sor, 1] && talalt[sor, 2] && talalt[sor, 3] && talalt[sor, 4]) return true;
                }
                for (int oszlop = 0; oszlop < 5; oszlop++)
                {
                    if (talalt[0, oszlop] && talalt[1, oszlop] && talalt[2, oszlop] && talalt[3, oszlop] && talalt[4, oszlop]) return true;
                }
                if (talalt[0, 0] && talalt[1, 1] && talalt[3, 3] && talalt[4, 4]) return true;
                else if (talalt[0, 4] && talalt[1, 3] && talalt[3, 1] && talalt[4, 0]) return true;
                else return false;
            }
        }        
    }

    class Bingo
    {
        static void Main()
        {
            List<BingoJatekos> jatekosok = new List<BingoJatekos>();
            foreach (var file in File.ReadAllLines("nevek.text")) jatekosok.Add(new BingoJatekos(file));

            Console.Write("4. Feladat: Játékosok száma: " + jatekosok.Count);
            Console.WriteLine();
            Console.Write("7. feladat: Kihúzott számok");
            Console.WriteLine();
            bool huzas = true;
            int sorszam = 1;
            Random rnd = new Random();
            List<int> kihuzottSzamok = new List<int>();
            do
            {
                int sorsoltSzam = 0;
                do
                {
                    sorsoltSzam = rnd.Next(1, 76);
                } while (kihuzottSzamok.Contains(sorsoltSzam));
                kihuzottSzamok.Add(sorsoltSzam);

                Console.Write(sorszam++ + ".->" + sorsoltSzam + "  ");

                foreach (var j in jatekosok)
                {
                    j.Sorsol(sorsoltSzam);
                    if (j.Ell) huzas = false;
                }
            } while (huzas);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("8. Feladat: Lehetséges nyertes(ek):");
            foreach (var j in jatekosok) if (j.Ell) j.Kiir();

        }
    }
}
