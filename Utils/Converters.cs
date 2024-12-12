namespace NoteApp.Utils
{
    public static class Converters
    {
        public static async Task<string?> ConvertToBase64(string path) {
            byte[] imgBytes;
            string base64 = null;
            try
            {
                if (File.Exists(path))
                {
                    imgBytes = File.ReadAllBytes(path);
                    base64 = Convert.ToBase64String(imgBytes);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
            }
            return base64;
        }
        public static async Task<string> ConvertBase64ToFile(string base64, int id)
        {
            string path = null;
            try
            {
                byte[] filyArr = Convert.FromBase64String(base64);
                string local = Path.Combine(FileSystem.AppDataDirectory, $"imagen_{id}.jpg");
                await File.WriteAllBytesAsync(local, filyArr);
                path = local;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return path;
        }
    }
}
