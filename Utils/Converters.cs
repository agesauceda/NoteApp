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
    }
}
