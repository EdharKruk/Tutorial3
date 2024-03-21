public class ContainerShip
{
    public List<Container> Containers { get; private set; } = new List<Container>();
    public double MaxSpeed { get; private set; }
    public int MaxContainerCount { get; private set; }
    public double MaxWeight { get; private set; }

    public ContainerShip(double maxSpeed, int maxContainerCount, double maxWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
    }

    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount || CurrentWeight() + container.CargoMass > MaxWeight)
        {
            throw new InvalidOperationException("Cannot add container: exceeds ship capacity.");
        }

        Containers.Add(container);
    }

    public double CurrentWeight()
    {
        return Containers.Sum(c => c.CargoMass + c.TareWeight);
    }

    public bool RemoveContainer(string serialNumber)
    {
        var containerToRemove = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (containerToRemove != null)
        {
            Containers.Remove(containerToRemove);
            return true;
        }

        return false;
    }

    public static Container CreateContainer(string type, double tareWeight, int height, int depth, double maxPayload,
        bool isHazardous = false, string productType = null, double temperature = 0, double pressure = 0)
    {
        switch (type.ToLower())
        {
            case "refrigerated":
                return new RefrigeratedContainer(tareWeight, height, depth, maxPayload, productType, temperature);
            case "liquid":
                return new LiquidContainer(tareWeight, height, depth, maxPayload, isHazardous);
            case "gas":
                return new GasContainer(tareWeight, height, depth, maxPayload, pressure);
            default:
                throw new ArgumentException("Invalid container type specified.");
        }
    }

    public void LoadCargoIntoContainer(Container container, double mass)
    {
        try
        {
            container.LoadCargo(mass);
            Console.WriteLine($"Loaded {mass}kg into container {container.SerialNumber}.");
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Could not load cargo: {ex.Message}");
        }
    }

    public void LoadContainerOntoShip(ContainerShip ship, Container container)
    {
        try
        {
            ship.AddContainer(container);
            Console.WriteLine($"Container {container.SerialNumber} added to ship.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Could not add container to ship: {ex.Message}");
        }
    }

    public void LoadContainers(List<Container> containersToAdd)
    {
        foreach (var container in containersToAdd)
        {
            if (Containers.Count >= MaxContainerCount || CurrentWeight() + container.CargoMass > MaxWeight)
            {
                throw new InvalidOperationException(
                    $"Cannot add container {container.SerialNumber}: exceeds ship capacity.");
            }

            Containers.Add(container);
        }
    }

    public void UnloadContainer(string serialNumber)
    {
        var container =
            Containers.FirstOrDefault(c => c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        container?.UnloadCargo();
    }

    public bool ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = Containers.FindIndex(c => c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        if (index != -1)
        {
            Containers[index] = newContainer;
            return true;
        }

        return false;
    }

    public static bool TransferContainer(ContainerShip fromShip, ContainerShip toShip, string serialNumber)
    {
        var container = fromShip.Containers.FirstOrDefault(c =>
            c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        if (container != null && toShip.Containers.Count < toShip.MaxContainerCount &&
            toShip.CurrentWeight() + container.CargoMass <= toShip.MaxWeight)
        {
            fromShip.RemoveContainer(container.SerialNumber);
            toShip.AddContainer(container);
            return true;
        }

        return false;
    }

    public void PrintContainerInfo(string serialNumber)
    {
        var container =
            Containers.FirstOrDefault(c => c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        if (container != null)
        {
            Console.WriteLine(
                $"Container Serial: {container.SerialNumber}, Type: {container.GetType().Name}, Cargo Mass: {container.CargoMass}kg");
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }

    public void PrintShipInfo()
    {
        Console.WriteLine(
            $"Ship Details: Max Speed = {MaxSpeed} knots, Max Containers = {MaxContainerCount}, Max Weight = {MaxWeight} tons");
        Console.WriteLine("Loaded Containers:");
        foreach (var container in Containers)
        {
            Console.WriteLine($" - Serial: {container.SerialNumber}, Cargo Mass: {container.CargoMass}kg");
        }
    }
}