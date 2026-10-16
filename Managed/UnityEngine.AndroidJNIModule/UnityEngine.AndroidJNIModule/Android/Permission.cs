using System;

namespace UnityEngine.Android
{
	// Token: 0x0200002D RID: 45
	public struct Permission
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000AE90 File Offset: 0x00009090
		private static AndroidJavaObject GetUnityPermissions()
		{
			bool flag = Permission.m_UnityPermissions != null;
			AndroidJavaObject androidJavaObject;
			if (flag)
			{
				androidJavaObject = Permission.m_UnityPermissions;
			}
			else
			{
				Permission.m_UnityPermissions = new AndroidJavaClass("com.unity3d.player.UnityPermissions");
				androidJavaObject = Permission.m_UnityPermissions;
			}
			return androidJavaObject;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AECC File Offset: 0x000090CC
		public static bool ShouldShowRequestPermissionRationale(string permission)
		{
			bool flag = string.IsNullOrWhiteSpace(permission);
			return !flag;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public static bool HasUserAuthorizedPermission(string permission)
		{
			bool flag = permission == null;
			return !flag;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AF10 File Offset: 0x00009110
		public static void RequestUserPermission(string permission)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[] { permission }, null);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AF3C File Offset: 0x0000913C
		public static void RequestUserPermissions(string[] permissions)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (!flag)
			{
				Permission.RequestUserPermissions(permissions, null);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AF64 File Offset: 0x00009164
		public static void RequestUserPermission(string permission, PermissionCallbacks callbacks)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[] { permission }, callbacks);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AF90 File Offset: 0x00009190
		public static void RequestUserPermissions(string[] permissions, PermissionCallbacks callbacks)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (flag)
			{
			}
		}

		// Token: 0x04000093 RID: 147
		public const string Camera = "android.permission.CAMERA";

		// Token: 0x04000094 RID: 148
		public const string Microphone = "android.permission.RECORD_AUDIO";

		// Token: 0x04000095 RID: 149
		public const string FineLocation = "android.permission.ACCESS_FINE_LOCATION";

		// Token: 0x04000096 RID: 150
		public const string CoarseLocation = "android.permission.ACCESS_COARSE_LOCATION";

		// Token: 0x04000097 RID: 151
		public const string ExternalStorageRead = "android.permission.READ_EXTERNAL_STORAGE";

		// Token: 0x04000098 RID: 152
		public const string ExternalStorageWrite = "android.permission.WRITE_EXTERNAL_STORAGE";

		// Token: 0x04000099 RID: 153
		private static AndroidJavaObject m_UnityPermissions;
	}
}
