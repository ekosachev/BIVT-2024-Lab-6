namespace Help
{
    public class Help
    {
        public static void Print(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    System.Console.Write($"{matrix[row, col]}");
                }
                System.Console.WriteLine();
            }
        }
    }
}
