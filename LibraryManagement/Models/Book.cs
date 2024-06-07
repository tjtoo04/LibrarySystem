using QueryBuilder;

public class Book: Models.Model{
    public Book() {
        SetPrimaryKey("book_ID");
        SetTableSearchableFields(["title", "author", "is_available"]); 
    }

}

