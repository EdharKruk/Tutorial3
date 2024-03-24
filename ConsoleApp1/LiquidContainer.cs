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
        double allowedMass = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (mass > allowedMass)
        {
            SendHazardNotification($"Attempt to load {mass}kg exceeds allowed capacity for hazardous cargo. Operation denied.");
        }
        base.LoadCargo(mass);
    }

    public void SendHazardNotification(string message)
    {
        Console.WriteLine($"Hazard Alert for {SerialNumber}: {message}");
    }
}