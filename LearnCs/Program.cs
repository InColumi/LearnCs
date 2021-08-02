using System;
using System.Collections.Generic;

namespace MyBookstore
{
    class Book
    {
        public string Name { get; private set; }
        public int Serial { get; private set; }
        public bool Status { get; private set; } //false means available

        public Book(string name, int serial)
        {
            //to do
            Name = name;
            Serial = serial;
        }
        //public bool IsAvailable()
        //{
        //    //to do
        //    //should return true if a book is available

        //    bool bAvailale = true;

        //    if (bAvailale)
        //    {
        //        Status = false;
        //    }
        //    return Status;
        //}
        public void Rent()
        {
            if (Status == false)
            {
                //A book can be rented if it's rental status is false
                //Console.WriteLine("A book can be rented");
                Status = true;
            }
            else
            {
                //otherwise, the book is not available.
                //Console.WriteLine("The book is not available");
            }
        }

        public void Return()
        {
            if (Status == true)
            {
                //A book can be returned only if, it was rented before!
                //How to ace a job interview successfully returned.
                Status = false;
            }
            else
            {
                // rent status false means, it is available in the store.
                // Therefore, you should generate error message if some users tries to return this book.
                //Console.WriteLine("");
                throw new Exception("users tries to return this book");
            }
        }

        private string GetStatus()
        {
            return (Status) ? "Rented" : "Available";
        }
        public void ShowInfo()
        {
            //Show name of the book, it's serial and rental status.
            Console.WriteLine($"Book Name: {Name}, Serial: {1}, Status: {GetStatus()}");
        }
    }

    class Reader
    {
        public string Name { get; set; }
        public int CountBooksWasRented { get; private set; }
        private List<Book> _rentedBooks;

        public Reader(string name)
        {
            //to do
            //initialize a reader object
            Name = name;
            _rentedBooks = new List<Book>();
            CountBooksWasRented = _rentedBooks.Count;
        }

        public void RentABook(Book book)
        {
            //to do
            //user is allowed to rent maximum two books at a time.
            //issue error message, if users want to rent more than two books.
            //for (int i = 1; i <= 2; i++)
            //{
            //    _rentedBooks.Add(book);
            //}
            //if (_rentedBooks.Count > 2)
            if (_rentedBooks.Count >= 2)
            {
                //Sorry! Mahbub, You cannot rent more than two books!
                Console.Write($"Sorry! + '{Name},' + You cannot rent more than two books");
            }
            else
            {
                book.Rent();
                _rentedBooks.Add(book);
                CountBooksWasRented = _rentedBooks.Count;
            }
        }

        public void ReturnABook(Book book)
        {
            //to do
            //return a book, means change book status and remove the book for the readers list.
            //book.Status = false;
            book.Return();
            _rentedBooks.Remove(book);
            CountBooksWasRented = _rentedBooks.Count;
        }

        public void ShowInfo()
        {
            //to do
            //show reader's name and the list of books rented by the reader.
            //Reader David rented following books:
            //Book Name: Object Oriented Programming, Serial: 2, Status: Rented
            //Book Name: Programming Fundamentals, Serial: 1, Status: Rented

            Console.WriteLine($"Reader {Name} rented following books:");
            foreach (var book in _rentedBooks)
            {
                book.ShowInfo();
            }
        }
    }

    class BookStore
    {
        private List<Book> _books;
        private List<Reader> _readers;

        public BookStore()
        {
            //to do
            //initialize book store
            _books = new List<Book>();
            _readers = new List<Reader>();
        }

        public void AddAReader(string name)
        {
            //add a new reader to the bookstore's reader list.
            _readers.Add(new Reader(name));
        }

        public void RemoveAReader(string name)
        {
            //to do
            //remove a reader, therefore, first return all books(if any) rented by the reader then remove the reader.
            for (int i = 0; i < _readers.Count; i++)
            {
                if (_readers[i].Name == name && _readers[i].CountBooksWasRented == 0)
                {
                    _readers.Remove(_readers[i]);
                    return;
                }
            }
        }

        public void AddABook(string name, int serial)
        {
            //to do
            // add a book object to the bookstore's book list.
            Book newBook = new Book(name, serial);
            _books.Add(newBook);
        }

        public void RemoveABook(string name, int serial)
        {
            //to do
            //remove a book from book store. Only allowed if bookstore already have the book 'available'!
            //Otherwise, issue an error message because the book is already issued by some reader!
            Book bookForDelete = new Book(name, serial);
            _books.Remove(bookForDelete);  
        }

        public void RentABook(string nameReader, string nameBook)
        {
            //to do
            // A book can be rented, if it is available to the store and not already rented to somone else!
            for (int i = 0; i < _readers.Count; i++)
            {
                if (_readers[i].Name == nameReader)
                {
                    for (int j = 0; j < _books.Count; j++)
                    {
                        if (_books[j].Name == nameBook && _books[j].Status == false)
                        {
                            _readers[i].RentABook(_books[j]);
                            Console.WriteLine($"Book: {nameBook} successfully rented.");
                            return;
                        }
                    }
                }
            }
        }

        public void ReturnABook(string nameReader, string nameBook, int serial)
        {
            //A book can be returned by a reader, if he/she actually rented the book.
            for (int i = 0; i < _readers.Count; i++)
            {
                if (_readers[i].Name == nameReader && _readers[i].CountBooksWasRented != 0)
                {
                    for (int j = 0; j < _books.Count; j++)
                    {
                        if (_books[j].Name == nameBook && _books[j].Serial == serial)
                        {
                            _readers[i].ReturnABook(_books[j]);
                            Console.WriteLine($"Book: {nameBook} successfully returned.");
                            return;
                        }
                    }
                }
            }
        }

        public void ShowInfo()
        {
            //to do
            //show bookstore information
            //first show all books that are already rented to some readers.
            //then show all books thar are available to the store.
            // bs.RentABook("Mahbub", "Object Oriented Programming");

            foreach (var reader in _readers)
            {
                reader.ShowInfo();
            }

            Console.WriteLine("The bookstore have following books available:");
            foreach (var book in _books)
            {
                if (book.Status == false)
                {
                    book.ShowInfo();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BookStore bookStore = new BookStore();
            bookStore.AddAReader("Mahbub");
            bookStore.AddAReader("David");
            bookStore.AddAReader("Susan");
            bookStore.AddABook("Object Oriented Programming", 1);
            bookStore.AddABook("Object Oriented Programming", 2);
            bookStore.AddABook("Object Oriented Programming", 3);
            bookStore.AddABook("Programming Fundamentals", 1);
            bookStore.AddABook("Programming Fundamentals", 2);
            bookStore.AddABook("Let us C#", 1);
            bookStore.AddABook("Programming is Fun", 1);
            bookStore.AddABook("Life is Beautiful", 1);
            bookStore.AddABook("Let's Talk About the Logic", 1);
            bookStore.AddABook("How to ace a job interview", 1);
            bookStore.ShowInfo();

            Console.WriteLine();
            bookStore.RentABook("Mahbub", "Object Oriented Programming");
            bookStore.RentABook("Mahbub", "How to ace a job interview");
            bookStore.RentABook("Mahbub", "Life is Beautiful");

            Console.WriteLine();
            bookStore.RentABook("David", "Object Oriented Programming");
            bookStore.RentABook("David", "Programming Fundamentals");

            Console.WriteLine();
            bookStore.RentABook("Susan", "Let's Talk About the Logic");
            Console.WriteLine();
            bookStore.ShowInfo();

            Console.WriteLine();
            bookStore.ReturnABook("Mahbub", "Object Oriented Programming", 1);
            bookStore.ReturnABook("Mahbub", "How to ace a job interview", 1);
            Console.WriteLine();

            bookStore.RemoveABook("Let us C#", 1);
            bookStore.RemoveABook("Let's Talk About the Logic", 1);
            Console.WriteLine();

            bookStore.RemoveAReader("Mahbub");
            bookStore.ShowInfo();
            Console.ReadKey();
        }
    }
}


/*
 Once Executed, Your program will have the following output:

The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 1, Status: Available
Book Name: Object Oriented Programming, Serial: 2, Status: Available
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamentals, Serial: 1, Status: Available
Book Name: Programming Fundamentals, Serial: 2, Status: Available
Book Name: Let us C#, Serial: 1, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available
Book Name: Let's Talk About the Logic, Serial: 1, Status: Available
Book Name: How to ace a job interview, Serial: 1, Status: Available

Book: 'Object Oriented Programming' successfully rented.
Book: 'How to ace a job interview' successfully rented.
Sorry! Mahbub, You cannot rent more than two books!

Book: 'Object Oriented Programming' successfully rented.
Book: 'Programming Fundamentals' successfully rented.

Book: 'Let's Talk About the Logic' successfully rented.

Reader Mahbub rented following books:
Book Name: Object Oriented Programming, Serial: 1, Status: Rented
Book Name: How to ace a job interview, Serial: 1, Status: Rented
Reader David rented following books:
Book Name: Object Oriented Programming, Serial: 2, Status: Rented
Book Name: Programming Fundamentals, Serial: 1, Status: Rented
Reader Susan rented following books:
Book Name: Let's Talk About the Logic, Serial: 1, Status: Rented
The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamentals, Serial: 2, Status: Available
Book Name: Let us C#, Serial: 1, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available

Book: Object Oriented Programming successfully returned.
Book: 'Life is Beautiful' successfully rented.

Sorry! 'Let's Talk About the Logic' is already rented. Syatem cannot remove a rented book!

Book: How to ace a job interview successfully returned.
Book: Life is Beautiful successfully returned.

Reader David rented following books:
Book Name: Object Oriented Programming, Serial: 2, Status: Rented
Book Name: Programming Fundamentals, Serial: 1, Status: Rented
Reader Susan rented following books:
Book Name: Let's Talk About the Logic, Serial: 1, Status: Rented
The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 1, Status: Available
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamentals, Serial: 2, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available
Book Name: How to ace a job interview, Serial: 1, Status: Available
 */
