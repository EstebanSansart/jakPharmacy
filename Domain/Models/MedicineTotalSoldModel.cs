namespace Domain.Models;

public class MedicineTotalSoldModel
{
    public DateTime? InitialDate { get; set; } = null;
    public DateTime? FinalDate { get; set; } = null;
    public bool GetLessSales { get; set; } = false;
}
