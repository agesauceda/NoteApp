﻿namespace NoteApp.Utils
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

        public static async Task<bool> CheckPermissionMicrophoneAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Microphone>();
                }
                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking microphone permission: " + ex.Message);
                return false;
            }
        }

        public static async Task<PermissionStatus> SolicitarPermisosUbicacion()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
                {
                    await App.Current.MainPage.DisplayAlert("Permisos necesarios", "Se necesitan permisos para mostrar el mapa.", "OK");
                }
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            return status;
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
