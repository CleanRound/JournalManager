using Newtonsoft.Json;

class Journal
{
    public string JournalName { get; set; }
    public string PublishingHouse { get; set; }
    public DateTime PublicationDate { get; set; }
    public int NumberOfPages { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();

    public override string ToString()
    {
        string articlesInfo = string.Join("\n", Articles);
        return $"Journal Name: {JournalName}, Publishing House: {PublishingHouse}, Publication Date: {PublicationDate.ToShortDateString()}, Number of Pages: {NumberOfPages}, Articles:\n{articlesInfo}";
    }
}

class Article
{
    public string Title { get; set; }
    public int NumberOfCharacters { get; set; }
    public string Announcement { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}, Number of Characters: {NumberOfCharacters}, Announcement: {Announcement}";
    }
}

class Program
{
    static void Main()
    {
        List<Journal> journals = EnterJournals();
        DisplayJournals(journals);

        string serializedJournals = SerializeJournals(journals);
        SaveToFile(serializedJournals, "journals.json");

        string loadedData = LoadFromFile("journals.json");
        List<Journal> deserializedJournals = DeserializeJournals(loadedData);

        Console.WriteLine("Deserialized Journals:");
        DisplayJournals(deserializedJournals);
    }

    static List<Journal> EnterJournals()
    {
        List<Journal> journals = new List<Journal>();
        Console.WriteLine("Enter the number of journals:");
        int numberOfJournals = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfJournals; i++)
        {
            Console.WriteLine($"Enter information for journal {i + 1}:");
            journals.Add(EnterJournalInfo());
        }

        return journals;
    }

    static Journal EnterJournalInfo()
    {
        Journal journal = new Journal();

        Console.WriteLine("Enter the name of the journal:");
        journal.JournalName = Console.ReadLine();

        Console.WriteLine("Enter the name of the publishing house:");
        journal.PublishingHouse = Console.ReadLine();

        Console.WriteLine("Enter the date of publication (yyyy-mm-dd):");
        journal.PublicationDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter the number of pages:");
        journal.NumberOfPages = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the number of articles:");
        int numberOfArticles = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfArticles; i++)
        {
            Console.WriteLine($"Enter information for article {i + 1}:");
            journal.Articles.Add(EnterArticleInfo());
        }

        return journal;
    }

    static Article EnterArticleInfo()
    {
        Article article = new Article();

        Console.WriteLine("Enter the title of the article:");
        article.Title = Console.ReadLine();

        Console.WriteLine("Enter the number of characters:");
        article.NumberOfCharacters = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the announcement of the article:");
        article.Announcement = Console.ReadLine();

        return article;
    }

    static void DisplayJournals(List<Journal> journals)
    {
        foreach (var journal in journals)
        {
            Console.WriteLine(journal);
            Console.WriteLine(new string('-', 50));
        }
    }

    static string SerializeJournals(List<Journal> journals)
    {
        return JsonConvert.SerializeObject(journals, Formatting.Indented);
    }

    static void SaveToFile(string data, string fileName)
    {
        File.WriteAllText(fileName, data);
    }

    static string LoadFromFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    static List<Journal> DeserializeJournals(string data)
    {
        return JsonConvert.DeserializeObject<List<Journal>>(data);
    }
}
