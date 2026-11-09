using BlaisePascal.MagazzinoArray_Sofia_Benedettini.Domain;

namespace BlaisePascal.MagazzinoArray_Sofia_Benedettini.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warehouse warehouse = new Warehouse(3, 2);

            warehouse.AddBoxes(0, 0, 10);
            warehouse.AddBoxes(0, 1, 20);
            warehouse.AddBoxes(1, 0, 30);

            warehouse.ProductStorage(0, 12345);
            warehouse.ProductStorage(0, 23456);
            warehouse.ProductStorage(1, 12345);
            warehouse.ProductStorage(2, 34567);

            warehouse.RemoveProductFromFloor(0, 23456);

            int crowdedFloor = warehouse.FindMoreCrowded();

            Console.WriteLine($"Most crowded floor: {crowdedFloor}");

            int[] floors = warehouse.SearchProduct(12345);

            Console.WriteLine("the prduct 12345 is in the floors:");

            for (int i = 0; i < floors.Length; i += 1)
            {
                Console.WriteLine(floors[i]);
            }
        }
    }
}