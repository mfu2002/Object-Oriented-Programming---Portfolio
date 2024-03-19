
namespace CustomProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[,] mapSchema;
            if (args.Length == 1) {
                mapSchema = GetSchemaFromFile(args[0])!;
            }
            else {
                mapSchema = new int[,]{
                    { 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                    { 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    { 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    { 1, 1, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
                    { 1, 1, 0, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 0, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 0, 1, 1, 1,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    { 1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    { 1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    { 1, 1, 0, 0, 0, 0,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    { 1, 1, 1, 1, 1, 1,1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    { 1, 1, 1, 1, 1, 1,1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    { 1, 1, 1, 1, 1, 1,1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1, 1,1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                };
            }
            new Game(mapSchema).Start();
           
        }

        static int[,]? GetSchemaFromFile(string fileName)
        {
            int[,]? schema = null;
            StreamReader? file = null;
            try
            {
                file = new StreamReader(fileName);
                string[] fileContent = file.ReadToEnd().Split('\n');
                int rows = fileContent.Length;
                int cols = fileContent[0].Split(',').Length;
                schema = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    string[] values = fileContent[i].Split(",");

                    for (int j = 0; j < cols; j++)
                    {
                        schema[i, j] = int.Parse(values[j]);
                    }

                }

            }
            finally
            {
                file?.Close();
            }
            return schema;
        }

    }
}
