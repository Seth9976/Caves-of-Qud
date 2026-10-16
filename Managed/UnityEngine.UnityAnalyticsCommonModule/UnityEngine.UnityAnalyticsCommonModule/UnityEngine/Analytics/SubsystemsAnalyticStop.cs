using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000004 RID: 4
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SubsystemsAnalyticStop : SubsystemsAnalyticBase
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000208B File Offset: 0x0000028B
		public SubsystemsAnalyticStop()
			: base("SubsystemStop")
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		[RequiredByNativeCode]
		internal static SubsystemsAnalyticStop CreateSubsystemsAnalyticStop()
		{
			return new SubsystemsAnalyticStop();
		}
	}
}
