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
    public string? CreatedByName { get; set; } // Add this property
    public int? UpdatedById { get; set; }
    public string? UpdatedByName { get; set; } // Add this property
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; } // Add this property
}

public class NewsArticleCreateDTO
{
    public string? NewsTitle { get; set; }
    public string Headline { get; set; }
    public string? NewsContent { get; set; }
    public string? NewsSource { get; set; }
    public bool? NewsStatus { get; set; } = true;
    public int CategoryId { get; set; }
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
}