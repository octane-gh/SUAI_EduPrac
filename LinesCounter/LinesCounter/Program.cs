public class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter Scripts folder path: ");
            string path = Console.ReadLine();
            DirectoryInfo rootDirectory = new DirectoryInfo(path);
            try
            {
                if (rootDirectory.Exists)
                {
                    int numberOfFiles = 0;
                    int totalLines = 0;
                    Queue<DirectoryInfo> directories = new Queue<DirectoryInfo>();
                    directories.Enqueue(rootDirectory);
                    while (directories.Count > 0)
                    {
                        DirectoryInfo currentDirectory = directories.Dequeue();
                        foreach (DirectoryInfo di in currentDirectory.GetDirectories())
                        {
                            directories.Enqueue(di);
                        }

                        foreach (FileInfo file in currentDirectory.GetFiles())
                        {
                            numberOfFiles += 1;
                            totalLines += CountLinesInFile(file);
                        }
                    }
                    Console.WriteLine($"Number of files: {numberOfFiles}");
                    Console.WriteLine($"Total lines: {totalLines}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                continue;
            }
        }
    }

    private static int CountLinesInFile(FileInfo file)
    {
        if (!file.FullName.Contains(".cs") || file.FullName.Contains(".meta"))
            return 0;

        Console.WriteLine(file.FullName);
        int number = 0;
        string[] lines = File.ReadAllLines(file.FullName);
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine == string.Empty || trimmedLine.Contains("using") || (trimmedLine[0] == '}' || trimmedLine[0] == '{'))
            {
                continue;
            }
            number += 1;
        }

        return number;
    }
}