﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
