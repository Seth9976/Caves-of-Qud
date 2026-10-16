using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000003 RID: 3
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SubsystemsAnalyticStart : SubsystemsAnalyticBase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		public SubsystemsAnalyticStart()
			: base("SubsystemStart")
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		[RequiredByNativeCode]
		internal static SubsystemsAnalyticStart CreateSubsystemsAnalyticStart()
		{
			return new SubsystemsAnalyticStart();
		}
	}
}
