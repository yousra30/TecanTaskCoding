
public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter the CSV file path:");
        string? filePath = Console.ReadLine();
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return;
        }
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            string[][] transposedTable = lines
                 .Select(line => line.Split(';'))
                 .ToArray();
            int columns = transposedTable[0].Last().Length == 0 ? transposedTable[0].Length - 1 : transposedTable[0].Length;

            string fileDirectory = Path.GetDirectoryName(filePath) ?? Directory.GetCurrentDirectory();
            string transposedFileName = Path.GetFileNameWithoutExtension(filePath) + "_transposed.csv";
            string transposedFilePath = Path.Combine(fileDirectory, transposedFileName);

            using (StreamWriter writer = new StreamWriter(transposedFilePath))
            {
                for (int i = 0; i < columns; i++)
                {
                    string rowText = string.Join(";", transposedTable.Select(row => row[i]));
                    writer.WriteLine(rowText + ";");
                }
            }
            Console.WriteLine($"Table transposed and saved to: {transposedFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}