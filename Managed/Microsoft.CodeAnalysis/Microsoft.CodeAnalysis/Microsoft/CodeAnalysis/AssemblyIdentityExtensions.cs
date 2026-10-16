using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B2 RID: 434
	internal static class AssemblyIdentityExtensions
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x00038B16 File Offset: 0x00036D16
		internal static bool IsWindowsComponent(this AssemblyIdentity identity)
		{
			return identity.ContentType == 1 && identity.Name.StartsWith("windows.", 5);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00038B34 File Offset: 0x00036D34
		internal static bool IsWindowsRuntime(this AssemblyIdentity identity)
		{
			return identity.ContentType == 1 && string.Equals(identity.Name, "windows", 5);
		}
	}
}
