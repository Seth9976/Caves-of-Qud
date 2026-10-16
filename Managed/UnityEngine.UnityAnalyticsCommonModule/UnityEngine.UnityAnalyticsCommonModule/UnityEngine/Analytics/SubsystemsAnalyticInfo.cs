using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000005 RID: 5
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SubsystemsAnalyticInfo : SubsystemsAnalyticBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020B3 File Offset: 0x000002B3
		public SubsystemsAnalyticInfo()
			: base("SubsystemInfo")
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		[RequiredByNativeCode]
		internal static SubsystemsAnalyticInfo CreateSubsystemsAnalyticInfo()
		{
			return new SubsystemsAnalyticInfo();
		}

		// Token: 0x04000002 RID: 2
		private string id;

		// Token: 0x04000003 RID: 3
		private string plugin_name;

		// Token: 0x04000004 RID: 4
		private string version;

		// Token: 0x04000005 RID: 5
		private string library_name;
	}
}
