namespace OT.Assesment.EntityFrameworkCore.Dto.Player;

public class TopPlayersBySpendDto
{
    public string Username { get; set; }    
    public Guid AccountId { get; set; }
    public decimal TotalAmountSpend { get; set; }


}