namespace Costume;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Costume World!");
        List<Costume> myCostumes = new List<Costume>();
        Costume detective = new Costume();
        detective._headWear = "fedora";
        detective._upperGarment = "trench coat";
        detective._lowerGarment = "slacks";
        detective._footWear = "Ghillie Broques";
        detective._accessories = "fingerprint kit";
        myCostumes.Add(detective);

        Costume nurse = new Costume();
        nurse._headwear = "hairnet";
        nurse._upperGarment = "scrubs";
        nurse._lowerGarment = "scrubs";
        nurse._footwear = "orthopedic shoes";
        nurse._accessories = "surgical mask";
        myCostumes.Add(nurse);

        foreach (Costume c in myCostumes)
        {
            c.Output();
        }
    }
}
