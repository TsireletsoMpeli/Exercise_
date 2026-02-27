using System;

public struct LibraryMember
{
    public int MemberId { get; set; }
    public string Name { get; set; }

    public LibraryMember(int memberId, string name)
    {
        MemberId = memberId;
        Name = name;
    }
}


public class LibraryItem
{
    protected string Title;
    protected int ItemId;

    public LibraryItem(string title, int itemId)
    {
        this.Title = title;
        this.ItemId = itemId;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Title: {Title}, Item ID: {ItemId}");
    }

    public string GetTitle()
    {
        return Title;
    }
}
//interface


public interface ILoanable
{
    void BorrowItem();
    void ReturnItem();
}

public class Book : LibraryItem, ILoanable
{
    private string Author;
    private bool IsBorrowed;

    public Book(string title, int itemId, string author) : base(title, itemId)
    {
        this.Author = author;
        this.IsBorrowed = false;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Author: {Author}");
    }

    public void BorrowItem()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"{Title} has been borrowed.");
        }
        else
        {
            Console.WriteLine($"{Title} is currently unavailable.");
        }
    }

    public void ReturnItem()
    {
        IsBorrowed = false;
        Console.WriteLine($"{Title} has been returned.");
    }
}


public class DVD : LibraryItem, ILoanable
{
    private int DurationInMinutes;
    private bool IsBorrowed;

    public DVD(string title, int itemId, int duration) : base(title, itemId)
    {
        this.DurationInMinutes = duration;
        this.IsBorrowed = false;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Duration (mins): {DurationInMinutes}");
    }

    public void BorrowItem()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"{Title} has been borrowed.");
        }
        else
        {
            Console.WriteLine($"{Title} is currently unavailable.");
        }
    }

    public void ReturnItem()
    {
        IsBorrowed = false;
        Console.WriteLine($"{Title} has been returned.");
    }
}

// subclass
public class Library
{
    private LibraryItem[] catalog;
    private int itemCount;

    public Library(int size = 10)
    {
        catalog = new LibraryItem[size];
        itemCount = 0;
    }

    public void AddItem(LibraryItem item)
    {
        if (itemCount < catalog.Length)
        {
            catalog[itemCount] = item;
            itemCount++;
        }
        else
        {
            Console.WriteLine("Catalog is full. Cannot add more items.");
        }
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("\n*** Full Library Catalog ***");
        for (int i = 0; i < itemCount; i++)
        {
            catalog[i].DisplayDetails();
            Console.WriteLine();
        }
    }

    public void Search(string title)
    {
        bool found = false;
        for (int i = 0; i < itemCount; i++)
        {
            if (catalog[i].GetTitle().Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n*** Search Result ***");
                catalog[i].DisplayDetails();
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine($"Item with title '{title}' not found.");
        }
    }

    public LibraryItem GetItemByIndex(int index)
    {
        if (index >= 0 && index < itemCount)
        {
            return catalog[index];
        }
        return null;
    }
}
// the main method

public class Program
{
    public static void Main(string[] args)
    {
        // Create Library
        Library myLibrary = new Library();

        // Create Items
        Book book1 = new Book("Bophelo ba Lillo", 101, "T.M Tokelo");
        Book book2 = new Book("The communication", 102, "Jhon Wick");
        DVD dvd1 = new DVD("Mopheme", 201, 136);

        // Add Items to Catalog
        myLibrary.AddItem(book1);
        myLibrary.AddItem(book2);
        myLibrary.AddItem(dvd1);

        // Create Library Member
        LibraryMember member = new LibraryMember(1, "Relebohile");

        // Welcome Message
        Console.WriteLine($"Welcome to the Digital Library, {member.Name}!");

        // Display Catalog
        myLibrary.DisplayCatalog();

        // Borrowing Test
        Console.WriteLine("*** Testing Borrowing System ***");
        Book borrowTest = (Book)myLibrary.GetItemByIndex(0); 
        borrowTest.BorrowItem(); 
        borrowTest.BorrowItem(); 

        // Search Test
        myLibrary.Search("The  communication");
    }
}