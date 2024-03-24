class Program
{
    static void Main(string[] args)
    {
        ContainerShip ship = new ContainerShip(maxSpeed: 30, maxContainerCount: 5, maxWeight: 200000);
        
        Container refrigeratedContainer = ship.CreateContainer("C", tareWeight: 1000, height: 250, depth: 250, maxPayload: 5000, productType: "Bananas", temperature: 14);
        Container liquidContainer = ship.CreateContainer("L", tareWeight: 1200, height: 250, depth: 250, maxPayload: 6000, isHazardous: true);
        Container gasContainer = ship.CreateContainer("G", tareWeight: 950, height: 250, depth: 250, maxPayload: 4500, pressure: 100);
        


        ship.LoadCargoIntoContainer(refrigeratedContainer, 4000);
        ship.LoadCargoIntoContainer(liquidContainer, 5000);
        ship.LoadCargoIntoContainer(gasContainer, 3000);
        
        

        ship.AddContainer(refrigeratedContainer);
        ship.AddContainer(liquidContainer);
        ship.AddContainer(gasContainer);
        
        ship.PrintShipInfo();
        
        ship.PrintContainerInfo(refrigeratedContainer.SerialNumber);
        
        ship.UnloadContainer(gasContainer.SerialNumber);
        
        bool replaceResult = ship.ReplaceContainer(liquidContainer.SerialNumber, new LiquidContainer(tareWeight: 1100, height: 250, depth: 250, maxPayload: 5000, isHazardous: false));
        if (replaceResult)
        {
            Console.WriteLine("Container replaced successfully.");
        }
        else
        {
            Console.WriteLine("Container replacement failed.");
        }
        
        ContainerShip anotherShip = new ContainerShip(maxSpeed: 25, maxContainerCount: 10, maxWeight: 30000);
        bool transferResult = ContainerShip.TransferContainer(ship, anotherShip, refrigeratedContainer.SerialNumber);
        if (transferResult)
        {
            Console.WriteLine($"Container {refrigeratedContainer.SerialNumber} transferred to another ship.");
        }
        else
        {
            Console.WriteLine("Container transfer failed.");
        }
        
        Console.WriteLine("\nAfter transfer:");
        Console.WriteLine("First Ship:");
        ship.PrintShipInfo();
        Console.WriteLine("Second Ship:");
        anotherShip.PrintShipInfo();

        ship.RemoveContainer(gasContainer.SerialNumber);
        ship.RemoveContainer(refrigeratedContainer.SerialNumber);
        ship.RemoveContainer(liquidContainer.SerialNumber);
        
        
        
        List<Container> containersToAdd = new List<Container> { refrigeratedContainer, liquidContainer, gasContainer };
        ship.LoadContainers(containersToAdd);
        ship.PrintShipInfo();
       
        
    }
}