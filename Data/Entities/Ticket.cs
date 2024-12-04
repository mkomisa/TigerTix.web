namespace Data.Entities.Ticket.cs{
public class Ticket{
    public int ticketID {get; set;}
    public int EventId {get; set;}
    public string ticketName {get; set;}
    public double price {get; set;}
     public override string ToString()
    {
        return $"TicketID: {ticketID}, EventId: {EventId}, TicketName: {ticketName}, Price: {price}";
    }
    }
}