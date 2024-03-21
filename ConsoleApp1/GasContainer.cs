public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double tareWeight, int height, int depth, double maxPayload, double pressure)
        : base(tareWeight, height, depth, maxPayload, "G")
    {
        Pressure = pressure;
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaxPayload)
        {
            SendHazardNotification($"Attempt to overfill gas container {SerialNumber}. Operation denied.");
            throw new OverfillException($"Exceeds maximum payload for gas container {SerialNumber}.");
        }
        CargoMass = mass;
    }

    public override void UnloadCargo()
    {
        CargoMass *= 0.05;
    }

    public void SendHazardNotification(string message)
    {
        Console.WriteLine($"Hazard Alert for Gas Container {SerialNumber}: {message}");
    }
}