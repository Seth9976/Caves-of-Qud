using System;
using System.IO;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000129 RID: 297
	internal static class PathUtil
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x00025EE5 File Offset: 0x000240E5
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string GetSystem32Path()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.System);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00025EEE File Offset: 0x000240EE
		internal static string GetSystemDllFullPath(string filename)
		{
			return Path.Combine(PathUtil._system32Path, filename);
		}

		// Token: 0x040011C2 RID: 4546
		private static string _system32Path = PathUtil.GetSystem32Path();
	}
}
