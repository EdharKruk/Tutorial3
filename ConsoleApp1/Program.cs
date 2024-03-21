class Program
{
    static void Main(string[] args)
    {
        ContainerShip myShip = new ContainerShip(20, 10, 10000);

        try
        {
            Container refrigeratedContainer = ContainerShip.CreateContainer("Refrigerated", 500, 200, 200, 2000,
                productType: "Bananas", temperature: 14);
            Container liquidContainer = ContainerShip.CreateContainer("Liquid", 450, 190, 190, 4000, isHazardous: true);
            Container gasContainer = ContainerShip.CreateContainer("Gas", 400, 180, 180, 1000, pressure: 100);

            myShip.LoadCargoIntoContainer(refrigeratedContainer, 1000);
            myShip.LoadCargoIntoContainer(liquidContainer, 800);
            myShip.LoadCargoIntoContainer(gasContainer, 500);


            myShip.LoadContainerOntoShip(myShip, refrigeratedContainer);
            myShip.LoadContainerOntoShip(myShip, liquidContainer);
            myShip.LoadContainerOntoShip(myShip, gasContainer);
            
            List<Container> containersToLoad = new List<Container> { refrigeratedContainer, liquidContainer, gasContainer };
            myShip.LoadContainers(containersToLoad);

            myShip.PrintShipInfo();
            
            Console.WriteLine("Detailed Container Info:");
            foreach (Container container in containersToLoad)
            {
                myShip.PrintContainerInfo(container.SerialNumber);
            }


            myShip.UnloadContainer(gasContainer.SerialNumber);


            if (myShip.ReplaceContainer(liquidContainer.SerialNumber, new GasContainer(400, 180, 180, 1000, 90)))
            {
                Console.WriteLine("Container replaced successfully.");
            }
            else
            {
                Console.WriteLine("Container replacement failed.");
            }


            ContainerShip anotherShip = new ContainerShip(30, 5, 15000);


            if (ContainerShip.TransferContainer(myShip, anotherShip, refrigeratedContainer.SerialNumber))
            {
                Console.WriteLine(
                    $"Transfer successful: Container {refrigeratedContainer.SerialNumber} moved to another ship.");
            }
            else
            {
                Console.WriteLine("Transfer failed.");
            }


            Console.WriteLine("\nMy Ship Info:");
            myShip.PrintShipInfo();
            Console.WriteLine("\nAnother Ship Info:");
            anotherShip.PrintShipInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}