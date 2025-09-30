using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    //lista som lagrar alla gästboksinlägg
    static List<GuestbookEntry> guestbook = new List<GuestbookEntry>();

    //filväg där alla inläggen sparas i json
    static string filePath = "guestbook.json";

    static void Main()
    {
        LoadFromFile(); //läser in befintliga inlägg vid start
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Gästbok ===");
            Console.WriteLine("1. Visa alla inlägg");
            Console.WriteLine("2. Lägg till inlägg");
            Console.WriteLine("3. Ta bort inlägg");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj ett alternativ: ");

            string choice = Console.ReadLine();

            //hanterar användarens val i start
            switch (choice)
            {
                case "1":
                    ShowEntries();
                    break;
                case "2":
                    AddEntry();
                    break;
                case "3":
                    RemoveEntry();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta");
                    Console.ReadKey();
                    break;
            }
        }
    }
 
 //visar alla inlägg i gästboken
    static void ShowEntries()
    {
        Console.Clear();
        Console.WriteLine("=== Alla inlägg ===");

        if (guestbook.Count == 0)
        {
            Console.WriteLine("Inga inlägg ännu");
        }
        else
        {
            for (int i = 0; i < guestbook.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {guestbook[i].Owner}: {guestbook[i].Message}");
            }
        }

        Console.WriteLine("\nTryck på valfri tangent för att gå tillbaka till menyn");
        Console.ReadKey();
    }

//lägg till ett nytt inlägg
    static void AddEntry()
    {
        Console.Clear();
        Console.WriteLine("=== Lägg till nytt inlägg ===");

        Console.Write("Skriv in ägarens namn: ");
        string owner = Console.ReadLine();

        Console.Write("Skriv här: ");
        string message = Console.ReadLine();

        //felhantering
        if (string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine("Fel: Fälten får inte vara tomma. Tryck på valfri tangent för att fortsätta");
            Console.ReadKey();
            return;
        }

        GuestbookEntry entry = new GuestbookEntry(owner, message);
        guestbook.Add(entry);
        SaveToFile();

        Console.WriteLine("Inlägget har lagts till! Tryck på valfri tangent för att fortsätta");
        Console.ReadKey();
    }

    //ta bort ett inlägg baserat på index
    static void RemoveEntry()
    {
        Console.Clear();
        Console.WriteLine("=== Ta bort ett inlägg ===");

        if (guestbook.Count == 0)
        {
            Console.WriteLine("Det finns inga inlägg att ta bort");
            Console.WriteLine("Tryck på valfri tangent för att gå tillbaka");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < guestbook.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {guestbook[i].Owner}: {guestbook[i].Message}");
        }

        Console.Write("\nAnge numret på inlägget du vill ta bort: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index))
        {
            index -= 1;

            if (index >= 0 && index < guestbook.Count)
            {
                guestbook.RemoveAt(index);
                SaveToFile();
                Console.WriteLine("Inlägget har tagits bort");
            }
            else
            {
                Console.WriteLine("Fel: Ogiltigt index");
            }
        }
        else
        {
            Console.WriteLine("Fel: Ange ett giltigt nummer");
        }

        Console.WriteLine("Tryck på valfri tangent för att gå tillbaka");
        Console.ReadKey();
    }

    //sparar alla inlägg till json
    static void SaveToFile()
    {
        string json = JsonSerializer.Serialize(guestbook, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    //läser in inlägg från json
    static void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                guestbook = JsonSerializer.Deserialize<List<GuestbookEntry>>(json);
            }
        }
    }
}
