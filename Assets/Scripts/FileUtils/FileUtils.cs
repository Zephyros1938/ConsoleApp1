namespace ConsoleApp1
{
    public class FileUtils
    {
        // Function to load a file and return its contents
        public static string LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new FileNotFoundException("The specified file was not found.", filePath);
            }
        }

        // Function to write contents to a file
        public static void WriteFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}