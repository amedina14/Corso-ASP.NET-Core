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

//ESERCIZIO #2: Di che colore è la mela che pesa 190 grammi?
//string color = apples...;

//ESERCIZIO #3: Quante sono le mele rosse?
//int redAppleCount = apples...;

//ESERCIZIO #4: Qual è il peso totale delle mele verdi?
//int totalWeight = apples...;

