namespace ConsoleApp1.DataManagement
{
    public class UserData
    {
        public static readonly string _HomeEnv = Environment.OSVersion.Platform == PlatformID.Unix ? "HOME" : "HOMEPATH";
        public static readonly string _HomePath = Environment.GetEnvironmentVariable(_HomeEnv) ?? throw new NullReferenceException($"Could not find environment variable for {_HomeEnv}. Is it properly set?");
        public static readonly string _LocalPath = Path.Combine(_HomePath, ".local/share") ?? throw new FileNotFoundException($"Could not find .local/share folder at {Path.GetFullPath(_HomePath)}. Maybe it doesnt exist?");
        public static readonly string _PublisherPath = new Func<string>(() =>
            {
                if (!Directory.Exists(Path.Combine(_LocalPath, "Zephyros1938")))
                {
                    Directory.CreateDirectory(Path.Combine(_LocalPath, "Zephyros1938"));
                }
                return Path.Combine(_LocalPath, "Zephyros1938");
            }
        )();
        public static readonly string _GamePath = new Func<string>(() =>
            {
                if (!Directory.Exists(Path.Combine(_PublisherPath, "ConsoleApp1")))
                {
                    Directory.CreateDirectory(Path.Combine(_PublisherPath, "ConsoleApp1"));
                }
                return Path.Combine(_PublisherPath, "ConsoleApp1");
            }
        )();
        public static readonly string _WorldsPath = new Func<string>(() =>
            {
                if (!Directory.Exists(Path.Combine(_GamePath, "Worlds")))
                {
                    Directory.CreateDirectory(Path.Combine(_GamePath, "Worlds"));
                }
                return Path.Combine(_GamePath, "Worlds");
            }
        )();
        public static string GetWorldFileLocation(string worldName)
        {
            return Path.Combine(_WorldsPath, worldName);
        }
        public static void AppendToFile(string path, byte[] buffer)
        {
            //FileStream filePath = File.Open(GetWorldFileLocation(path), FileMode.Append, FileAccess.Write, FileShare.Write);
            using (FileStream fileStream = new(GetWorldFileLocation(path), FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(buffer);
            }
        }

        public static void DeleteFile(string path)
        {
            File.Delete(GetWorldFileLocation(path));
        }
    }
}