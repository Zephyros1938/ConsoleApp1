namespace ConsoleApp1
{
    public class ModelInfoLoader
    {
        public static string LoadModel(string modelinfopath){
            return FileUtils.LoadFile(modelinfopath);
        }
    }

    public class ModelInfo
    {
        string NAME;
        string VERT;
        string FRAG;
        string TEX;
        string TTR;
        string SPC;
        string STR;
        string NRM;
        string NTR;
        Shader shader;

        public ModelInfo(string modelinfopath, Shader sh)
        {
            string[] modelInfo = ModelInfoLoader.LoadModel(modelinfopath).Split('\n');
            foreach (var chunk in modelInfo)
            {
                switch (chunk.Split(' ').First())
                {
                    
                    case "VERT":
                        this.VERT = chunk.Split(' ').Last();
                        break;
                    case "FRAG":
                        this.FRAG = chunk.Split(' ').Last();
                        break;
                    case "TEX":
                        this.TEX = chunk.Split(' ').Last();
                        break;
                    case "TTR":
                        this.TTR = chunk.Split(' ').Last();
                        break;
                    case "SPC":
                        this.SPC = chunk.Split(' ').Last();
                        break;
                    case "STR":
                        this.STR = chunk.Split(' ').Last();
                        break;
                    case "NRM":
                        this.NRM = chunk.Split(' ').Last();
                        break;
                    case "NTR":
                        this.NTR = chunk.Split(' ').Last();
                        break;
                    case "NAME":
                        this.NAME = chunk.Split(' ').Last();
                        break;
                    default:
                        Console.WriteLine($"No Chunk {chunk.Split(' ').First()} for {modelinfopath}");
                        break;
                }
            }

            if(VERT!=null&&FRAG!=null){
                this.shader = sh;
            }
        }
    }
}