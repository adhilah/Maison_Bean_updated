namespace MaisonBean.Application.AI.DTOs;

public class SearchResultDto
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public double Score { get; set; }
}