public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }

    public RefrigeratedContainer(double tareWeight, int height, int depth, double maxPayload, string productType,
        double temperature)
        : base(tareWeight, height, depth, maxPayload, "C")
    {
        ProductType = productType;
        Temperature = temperature;
    }

    public void SetProductType(string productType)
    {
        if (ProductType != null && ProductType != productType)
        {
            throw new OverfillException($"Container only for: {ProductType}.");
        }
        
        ProductType = productType;
    }

    public override void LoadCargo(double mass)
    {
        if ( mass > MaxPayload)
        {
            throw new OverfillException("Loading the given mass would exceed the container's payload capacity.");
        }

        if (Temperature < TemperatureForProduct(ProductType))
        {
            throw new OverfillException($"Cannot load {ProductType} at current temperature {Temperature}°C.");
        }
        
        CargoMass += mass;
    }

    private double TemperatureForProduct(string productType)
    {
        switch (productType)
        {
            case "Bananas": return 13.3;
            case "Chocolate": return 18;
            default: throw new ArgumentException("Unknown product type.");
        }
    }

    public override void UnloadCargo()
    {
        CargoMass = 0;
    }
}