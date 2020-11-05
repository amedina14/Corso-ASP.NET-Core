using System.Collections.Generic;

class Apple{
    public string color { get; set; }
    public int weigth { get; set; }
}

List<Apple> apples = new List<Apple> {
    new Apple { color = "Red", weigth = 180 },
    new Apple { color = "Green", weigth = 195 },
    new Apple { color = "Red", weigth = 190 },
    new Apple { color = "Green", weigth = 185 },
};

/*
    Func<Apple, bool> rossa = apple => apple.color == "Red";
    IEnumerable<Apple> redApples = apples.Where(apple => apple.color == "Red");
*/
//ESEMPIO #1: Ottengo i pesi delle mele rosse
IEnumerable<int> weigthOfRedApples = apples
                            .Where(apple => 
                            apple.color == "Red")
                            .Select(apple => 
                            apple.weigth);

//ESEMPIO #2: Calcolo la media dei pesi ottenuti
double average = weigthOfRedApples.Average();
Console.WriteLine("Peso medio delle mele: "+average);

/*
List<Apple> rosse = new List<Apple>();
foreach (Apple apple in apples)
{
    if(apple.color == "Red"){
        rosse.Add(apple);
        Console.WriteLine(apple);
    }
}
*/

/*
Average
Sum
Min
Max
Count
Any
All
*/

//ESERCIZIO #1: Qual è il peso minimo delle 4 mele?
IEnumerable<int> pesoMele = apples.Select(apple => apple.weigth);
int minimumWeight = pesoMele.Min();
Console.WriteLine("Peso minimo delle mele: "+minimumWeight);

//ESERCIZIO #2: Di che colore è la mela che pesa 190 grammi? ESERCIZIO #2: colore della mela di 190 grammi?
IEnumerable<string> appleColor = apples
                        .Where(apple => apple.weigth == 190)
                        .Select(apple => apple.color);
string color = Convert.ToString(appleColor);
Console.WriteLine("La mela di 190g è di colore: "+color);

//ESERCIZIO #3: Quante sono le mele rosse?
IEnumerable<int> meleRosse = apples
                        .Where(apple => apple.color == "Red")
                        .Select(apple => apple.weigth);
int redAppleCount = meleRosse.Count();
Console.WriteLine("Numero di mele rosse: "+redAppleCount);

//ESERCIZIO #4: Qual è il peso totale delle mele verdi?
IEnumerable<int> pesoMeleVerdi = apples
                        .Where(apple => apple.color == "Green")
                        .Select(apple => apple.weigth);
int totalWeight = pesoMeleVerdi.Sum();
Console.WriteLine("Peso mele verdi: "+totalWeight);