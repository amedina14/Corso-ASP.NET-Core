using System;
using MyCourse.Models.Enums;

namespace MyCourse.Models.ValueTypes
{
    public class Money
    {
        /* Euro azzerati */
        public Money() : this(Currency.EUR, 0.00m)
        {
            
        }
        /* Costruttore */
        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
            Currency = currency;
        }

        // Dichiara ed inizia l'importo
        private decimal amount = 0;

        /* 
            Funzione di valore di vendita del corso.
            Informazioni finanziarie rappresentate da decimal.
        */
        public decimal Amount
        {
            get{
                return amount;
            }
            set{
                if(value < 0){
                    throw new InvalidOperationException("The amount cannot be negative");
                }
                amount = value;
            }
        }

        public Currency Currency
        {
            get; set;
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            return money != null &&
                   Amount == money.Amount &&
                   Currency == money.Currency;
        }

        // Unisce importo e valuta
        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        // Li ritorna stringhe
        public override string ToString()
        {
            return $"{Currency} {Amount:#.00}";
        }
    }
}