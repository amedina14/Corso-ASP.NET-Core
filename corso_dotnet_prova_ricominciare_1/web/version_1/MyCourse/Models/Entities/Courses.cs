using System;
using System.Collections.Generic;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.Entities
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if(string.IsNullOrWhiteSpace(title)){
                throw new ArgumentException("The course must have a title");
            }
            if(string.IsNullOrWhiteSpace(author)){
                throw new ArgumentException("The course must have an author");
            }

            Title = title;
            Author = author;
            Lessons = new HashSet<Lesson>();
        }

        // Private per evitare di cambiare il titolo dall'esterno.
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Author { get; private set; }
        public string Email { get; private set; }
        public double Rating { get; private set; }

        // Proprieta complesse
        public Money FullPrice { get; private set; }
        public Money CurrentPrice { get; private set; }

        // Metodo per cambiare il titolo
        public void ChangeTitle(string NewTitle){
            if(string.IsNullOrWhiteSpace(NewTitle)){
                throw new ArgumentException("The title must have a title");
            }

            Title = NewTitle;
        }

        // Metodo per cambiare e validare il FullPrice
        public void ChangePrices(Money newFullPrice, Money newDiscountPrice){
            // Non possono essere vuoiti
            if(newFullPrice == null || newDiscountPrice == null){
                throw new ArgumentException("The prices can't be null");
            }
            // Le valute devono essere uguali
            if(newFullPrice.Currency != newDiscountPrice.Currency){
                throw new ArgumentException("The currencies don't match");
            }
            // Przzo listino non può essere minore del prezzo scontato
            if(newFullPrice.Amount < newDiscountPrice.Amount){
                throw new ArgumentException("Full price can't be less than current price.");
            }
            FullPrice = newFullPrice;
            CurrentPrice = newDiscountPrice;
        }

        // Proprieta di navigazione: Relazione / mapping
        public virtual ICollection<Lesson> Lessons { get; private set; }

    }
}
