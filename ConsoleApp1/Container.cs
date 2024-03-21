public abstract class Container
{
    public double CargoMass { get; protected set; }
    public int Height { get; private set; }
    public double TareWeight { get; private set; }
    public int Depth { get; private set; }
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; private set; }

    public Container(double tareWeight, int height, int depth, double maxPayload, string type)
    {
        TareWeight = tareWeight;
        Height = height;
        Depth = depth;
        MaxPayload = maxPayload;
        SerialNumber = SerialNumberGenerator.GenerateSerialNumber(type); 
    }

    public virtual void LoadCargo(double mass)
    {
        if (mass + CargoMass > MaxPayload)
        {
            throw new OverfillException("Loading the given mass would exceed maximum payload capacity.");
        }
        CargoMass += mass;
    }

    public virtual void UnloadCargo()
    {
        this.CargoMass = 0;
    }
}