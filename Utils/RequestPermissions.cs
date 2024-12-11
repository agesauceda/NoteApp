namespace NoteApp.Utils
{
    public static class RequestPermissions
    {
        public static async Task CheckPermissionNotification() {
            var status = await Permissions.RequestAsync<Permissions.PostNotifications>();
            if (status == PermissionStatus.Granted)
            {
                Console.WriteLine("Permission granted");
            }
            else
            {
                Console.WriteLine("Permission denied");
            }
        }
        public static async Task<string?> CheckPermissionCameraAsync()
        {
            string localPath = null;
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult file = await MediaPicker.CapturePhotoAsync();
                    if (file != null)
                    {
                        localPath = Path.Combine(FileSystem.AppDataDirectory, file.FileName);
                        using (Stream sourceStream = await file.OpenReadAsync())
                        using (FileStream localFileStream = File.Create(localPath))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Permission denied: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return localPath;
        }
    }
}
