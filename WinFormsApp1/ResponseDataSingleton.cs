public class ResponseDataSingleton
{
    private static readonly Lazy<ResponseDataSingleton> _instance =
        new Lazy<ResponseDataSingleton>(() => new ResponseDataSingleton());

    public static ResponseDataSingleton Instance => _instance.Value;

    public ReservationResponse CurrentReservation { get; set; }

    private ResponseDataSingleton() { }
}