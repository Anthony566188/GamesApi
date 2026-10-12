namespace GamesApi.Models;

public class PlatformStockReport
{
    public string Platform { get; set; } = null!;
    public int TotalTitles { get; set; }
    public decimal TotalInventoryValue { get; set; }
}