using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000002 RID: 2
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SubsystemsAnalyticBase : AnalyticsEventBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public SubsystemsAnalyticBase(string eventName)
			: base(eventName, 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x04000001 RID: 1
		public string subsystem;
	}
}
