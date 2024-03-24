public abstract class Container
{
    public string SerialNumber { get; protected set; }
    public double CargoMass { get; protected set; }
    public double MaxPayload { get; protected set; }
    public double TareWeight { get; protected set; }
    public int Height { get; protected set; }
    public int Depth { get; protected set; }

    protected Container(double tareWeight, int height, int depth, double maxPayload, string type)
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
            throw new OverfillException("Error");
        }
        CargoMass = mass;
    }

    public virtual void UnloadCargo()
    {
        CargoMass = 0;
    }
}