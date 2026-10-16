using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200041A RID: 1050
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x060021D5 RID: 8661 RVA: 0x00073AD9 File Offset: 0x00071CD9
		public static string UnsafeGetFullPath(string fileName)
		{
			return Path.GetFullPath(fileName);
		}
	}
}
