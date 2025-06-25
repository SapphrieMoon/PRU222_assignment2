public class NewsArticleDTO
{
    public string NewsArticleId { get; set; } = Guid.NewGuid().ToString();
    public string? NewsTitle { get; set; }
    public string Headline { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? NewsContent { get; set; }
    public string? NewsSource { get; set; }
    public bool? NewsStatus { get; set; } = true;
    public DateTime? ModifiedDate { get; set; }
    public int? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
    public int? UpdatedById { get; set; }
    public string? UpdatedByName { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    // Add tag support
    public List<int> TagIds { get; set; } = new List<int>();
    public List<string> TagNames { get; set; } = new List<string>();
}

public class NewsArticleCreateDTO
{
    public string? NewsTitle { get; set; }
    public string Headline { get; set; }
    public string? NewsContent { get; set; }
    public string? NewsSource { get; set; }
    public bool? NewsStatus { get; set; } = true;
    public int CategoryId { get; set; }

    // Add tag support
    public List<int> TagIds { get; set; } = new List<int>();
}

public class NewsArticleUpdateDTO
{
    public string NewsArticleId { get; set; } = Guid.NewGuid().ToString();
    public string? NewsTitle { get; set; }
    public string? Headline { get; set; }
    public string? NewsContent { get; set; }
    public string? NewsSource { get; set; }
    public bool? NewsStatus { get; set; } = true;
    public int CategoryId { get; set; }

    // Add tag support
    public List<int> TagIds { get; set; } = new List<int>();
}

// Add TagDTO for displaying tags
public class TagDTO
{
    public int TagId { get; set; }
    public string? TagName { get; set; }
    public string? Note { get; set; }
}