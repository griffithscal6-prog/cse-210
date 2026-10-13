namespace Costume;
class Costume
{
    // attributes 
    public string _headWear = "";
    public string _upperGarment;
    public string _lowerGarment;
    public string _footWear;
    public string _accessories;
    // behavior
        public void Output()
        {
            Console.WriteLine("Costume pieces: ");
            Console.WriteLine($"head: {_headWear}");
            Console.WriteLine($"top: {_upperGarment}");
            Console.WriteLine($"bottom: {_lowerGarment}");
            Console.WriteLine($"feet: {_footWear}");
            Console.WriteLine($"other: {_accessories}");
        }
}