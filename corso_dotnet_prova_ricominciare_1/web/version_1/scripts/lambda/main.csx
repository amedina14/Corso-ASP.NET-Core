using System;
//Per fare il debug di questi esempi, devi prima installare il global tool dotnet-script con questo comando:
//dotnet tool install -g dotnet-script

Console.WriteLine("Hello world!");

//ESEMPIO #1: Definisco una lambda che accetta un parametro DateTime e restituisce un bool, e l'assegno alla variabile canDrive
Func<DateTime, bool> canDrive = 
    dob => {
        return dob.AddYears(18) <= DateTime.Today;
    };
//Eseguo la lambda passandole il parametro DateTime
DateTime dob = new DateTime(2000,12,25);
bool result = canDrive(dob);
//Poi stampo il risultato bool che ha restituito
Console.WriteLine(result);


//ESEMPIO #2: Stavolta definisco una lambda che accetta un parametro DateTime ma non restituisce nulla
Action<DateTime> printDate = date => Console.WriteLine(date);

//La invoco passandole l'argomento DateTime
DateTime date = DateTime.Today; //new DateTime(2000,12,25);
printDate(date);

/*** ESERCIZI! ***/

// ESERCIZIO #1: Scrivi una lambda che prende due parametri stringa (nome e cognome) e restituisce la loro concatenazione
// Func<...> concatFirstAndLastName = ...;
Func<String, String, string> concatFirstAndLastName =
    (nome, cognome) => nome + " " + cognome;
// Qui invoca la lambda
String nome = "Adrian";
String cognome = "Rodriguez";
String nomeCompleto = concatFirstAndLastName(nome, cognome);
Console.WriteLine(nomeCompleto);

// ESERCIZIO #2: Una lambda che prende tre parametri interi (tre numeri) e restituisce il maggiore dei tre
// Func<...> getMaximum = ...;
Func<Int32,Int32,Int32,Int32> getMaximum =
    (num1,num2,num3) => {
        int max = 0;

        for(int i = 0; i < 3; i++){
            if(max < num1){
                max = num1;
            } else if(max < num2){
                max = num2;
            } else if(max < num3){
                max = num3;
            }
        }

        /*
        List<int> nums = new List<int>();
        nums.Add(num1);
        nums.Add(num2);        
        nums.Add(num3);

        foreach(int numero in nums){
            if(max >= nums[numero]){
                Console.WriteLine(max);                
            } else {
                max = nums[numero];
            }
        }
        */

        return max;
    };
// Qui invoca la lambda
Random random = new Random();
int num1 = random.Next(0,10);
int num2 = random.Next(0,10);
int num3 = random.Next(0,10);
Int32 maggiore = getMaximum(num1,num2,num3);
Console.WriteLine(maggiore);


// ESERCIZIO #3: Una lambda che prende due parametri DateTime e non restituisce nulla, ma stampa la minore delle due date in console con un Console.WriteLine
// Action<...> printLowerDate = ...;
Action<DateTime, DateTime> dataMin =
(DateTime1, DateTime2) => {
    var min = new DateTime();
    if(DateTime1 < DateTime2){
        min = DateTime1;
        Console.WriteLine(min);
    } else {
        min = DateTime2;
        Console.WriteLine(min);
    }
};
// Qui invoca la lambda
DateTime dateTime1 = new DateTime(2001,12,25);
DateTime dateTime2 = new DateTime(2002,12,25);

dataMin(dateTime1,dateTime2);
//Console.WriteLine();