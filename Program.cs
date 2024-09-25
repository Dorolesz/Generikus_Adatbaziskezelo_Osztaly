using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Gyak01
{
    class Etel
    {
        public string Nev { get; set; }
        public string Leiras { get; set; }

        public Etel(string nev, string leiras)
        {
            Nev = nev;
            Leiras = leiras;
        }

        public override string ToString()
        {
            return $"Étel: {Nev}, Leírás: {Leiras}";
        }
    }
	internal class Program
	{
		static Random r = new Random();
		static void Feladat01()
		{
			List<int> maxHomerseklet = new List<int>();
			List<int> minHomerseklet = new List<int>();
			for (int i = 0; i < 7; i++)
			{
				maxHomerseklet.Add(r.Next(25,40));
				minHomerseklet.Add(r.Next(25, 40));

			}
			for (int i = 0; i < 7; i++)
			{
				Console.WriteLine($"{i+1}. napon a minimum: {minHomerseklet[i]} " +
					$"a maximum: {maxHomerseklet[i]}");
			}

			double atlag = (maxHomerseklet.Sum() + minHomerseklet.Sum()) / (1.0 * maxHomerseklet.Count + minHomerseklet.Count);
            Console.WriteLine($"A heti átlaghőmérséklet: {Math.Round(atlag,2)}");

			//legmelegebb nap:

			int maxi = maxHomerseklet.Max();
			int index = maxHomerseklet.IndexOf(maxi);
            Console.WriteLine($"A legmelegebb ({maxi} Celsius) {index+1}. napon volt");
        }
		static void Feladat02()
		{
			StreamReader r = new StreamReader("szavak.txt");
			List<string> szavak = new List<string>();
			while (!r.EndOfStream) 
			{
				szavak.Add(r.ReadLine());
			}        
            foreach (var item in szavak)
            {
                Console.WriteLine(item);
            }
			r.Close();
			StreamWriter sw = new StreamWriter("szavak.txt", true);
            Console.WriteLine("Kérek egy szót, amit a fájlba íruhnk: ");
			string szo = Console.ReadLine();
			sw.WriteLine(szo);
			sw.Close();
        }
		static void Feladat03()
		{
			Dictionary<string, string> nevjegyzek = new Dictionary<string, string>();

			nevjegyzek.Add("Joska", "0690631631");
			nevjegyzek.Add("Piroska", "0690631632");
			nevjegyzek.Add("Aladár", "0690631633");
			nevjegyzek.Add("Julcsa", "0690631634");
			foreach (var item in nevjegyzek)
			{
                Console.WriteLine(item);
            }

            foreach (var item in nevjegyzek)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.WriteLine($"Aladár telefonszáma:{nevjegyzek["Aladár"]}");

			foreach (var item in nevjegyzek)
			{
                if (item.Value.Equals("0690631633"))
                {
                    Console.WriteLine($"{item.Key} száma 0690631633");
                }
            }
        }
		static void Feladat04()
		{
			int sorokSzama = 5;
			int oszlopokSzama = 10;
			bool[,] matrix = new bool[sorokSzama, oszlopokSzama];
			for (int i = 0; i < oszlopokSzama; i++)
			{
                Console.WriteLine($"{i+1,6} |");
            }
            Console.WriteLine();
            for (int i = 0; i < sorokSzama; i++)
			{
                Console.WriteLine($"{i+1}. sor: ");
                for (int j = 0; j < oszlopokSzama; j++)
				{
					Console.WriteLine($"{matrix[i, j],6} |");
				}
                Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------");
            }
			string valasz;
			do
			{
				Console.WriteLine("Melyik helyet szeretné lefoglalni (sor oszlop)");
				valasz = Console.ReadLine();
				if (valasz != "")
				{
					string[] szamok = valasz.Split(' ');
					int sor = int.Parse(szamok[0]);
					int oszlop = Convert.ToInt32(szamok[1]);
					matrix[sor, oszlop - 1] = true;
				}
			}
			while (valasz != "");
 
		}
		static void MatrixKiiratas(bool[,] matrix)
		{
            Console.WriteLine("\t");
			for (int i = 0; i < matrix.GetLength(1); i++)
			{
                Console.WriteLine($"{i + 1,6} |");
            }
        }

		static void Main(string[] args)
		{
			//Feladat01();
			//Feladat02();
			//Feladat03();
			Feladat04();
			Console.ReadLine();
		}
	}
}
