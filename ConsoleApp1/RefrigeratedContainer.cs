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

    public void SetProductTypeAndTemperature(string productType, double temperature)
    {
        if (CargoMass > 0)
        {
            throw new InvalidOperationException(
                "Cannot change product type or temperature when the container is loaded.");
        }

        ProductType = productType;
        Temperature = temperature;
    }

    public override void LoadCargo(double mass)
    {
        if (CargoMass + mass > MaxPayload)
        {
            throw new OverfillException("Loading the given mass would exceed the container's payload capacity.");
        }

        if (Temperature < TemperatureForProduct(ProductType))
        {
            throw new InvalidOperationException($"Cannot load {ProductType} at current temperature {Temperature}°C.");
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