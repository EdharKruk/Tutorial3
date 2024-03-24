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

    public Container CreateContainer(string type, double tareWeight, int height, int depth, double maxPayload,
        bool isHazardous = false, string productType = null, double temperature = 0, double pressure = 0)
    {
        switch (type)
        {
            case "C":
                return new RefrigeratedContainer(tareWeight, height, depth, maxPayload, productType, temperature);
            case "L":
                return new LiquidContainer(tareWeight, height, depth, maxPayload, isHazardous);
            case "G":
                return new GasContainer(tareWeight, height, depth, maxPayload, pressure);
            default:
                throw new ArgumentException("Unknown container type.");
        }
    }
    public void LoadCargoIntoContainer(Container container, double mass)
    {
        container.LoadCargo(mass);
        Console.WriteLine($"Cargo with mass {mass} was load in container: {container.SerialNumber}.");
    }
    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount || Containers.Sum(c => c.CargoMass + container.CargoMass) > MaxWeight)
        {
            throw new Exception("Limit of container or weight.");
        }
        Containers.Add(container);
    }
    public void LoadContainers(List<Container> containersToAdd)
    {
        foreach (var container in containersToAdd)
        {
            AddContainer(container); 
        }
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
    public void UnloadContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        container?.UnloadCargo();
    }
    public bool ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index != -1)
        {
            Containers[index] = newContainer;
            return true;
        }
        return false;
    }
    public static bool TransferContainer(ContainerShip fromShip, ContainerShip toShip, string serialNumber)
    {
        var container = fromShip.Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null && toShip.Containers.Count < toShip.MaxContainerCount && toShip.Containers.Sum(c => c.CargoMass) + container.CargoMass <= toShip.MaxWeight)
        {
            fromShip.RemoveContainer(serialNumber);
            toShip.AddContainer(container);
            return true;
        }
        return false;
    }
    public void PrintContainerInfo(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            Console.WriteLine($"Container Serial: {container.SerialNumber}, Cargo Mass: {container.CargoMass}");
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }
    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship Max Speed: {MaxSpeed} knots, Max Containers: {MaxContainerCount}, Max Weight: {MaxWeight} tons");
        foreach (var container in Containers)
        {
            Console.WriteLine($"Container Serial: {container.SerialNumber}, Cargo Mass: {container.CargoMass}");
        }
    }

}


  