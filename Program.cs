namespace BIVT_2024_Lab_6;

using Help;
using Purple_1;

class Program
{
    static void Main(string[] args)
    {
        Purple_1.Participant p = new Purple_1.Participant("Ivan", "Ivanov");
        p.SetCriterias(new double[] { 2.5, 2.75, 3.0, 3.5 });
        p.Jump(new int[] { 1, 2, 3, 4, 5, 6, 6 });
        p.Jump(new int[] { 1, 2, 3, 4, 5, 6, 6 });
        p.Jump(new int[] { 1, 2, 3, 4, 5, 6, 6 });
        p.Jump(new int[] { 1, 2, 3, 4, 5, 6, 6 });
        p.Jump(new int[] { 1, 2, 3, 4, 5, 6, 6 });

        Help.Print(p.Marks);

        System.Console.WriteLine(p.TotalScore);
    }
}
