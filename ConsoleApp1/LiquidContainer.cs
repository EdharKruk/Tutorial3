public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(double tareWeight, int height, int depth, double maxPayload, bool isHazardous)
        : base(tareWeight, height, depth, maxPayload, "L")
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double mass)
    {
        double maxAllowedMass = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (mass + CargoMass > maxAllowedMass)
        {
            SendHazardNotification(
                $"Attempt to exceed safe loading capacity for container {SerialNumber}. Operation denied.");
            throw new InvalidOperationException("Exceeds allowed loading capacity.");
        }

        CargoMass = mass;
    }

    public override void UnloadCargo()
    {
        CargoMass = 0;
    }

    public void SendHazardNotification(string message)
    {
        Console.WriteLine($"Hazard Alert for Liquid Container {SerialNumber}: {message}");
    }
}