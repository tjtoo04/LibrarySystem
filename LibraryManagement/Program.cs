using DatabaseConnector;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

DBConnect conn = new();

Book book = new Book();

Dictionary<int, Dictionary<string,string>> data = book.Select("books", "title", "author").Where("book_id", "=", "1").Get();
