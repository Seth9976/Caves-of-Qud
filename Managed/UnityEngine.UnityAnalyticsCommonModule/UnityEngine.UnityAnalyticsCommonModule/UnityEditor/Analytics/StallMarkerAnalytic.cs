using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000029 RID: 41
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class StallMarkerAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000025DF File Offset: 0x000007DF
		public StallMarkerAnalytic()
			: base("editorStallMarker", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025F8 File Offset: 0x000007F8
		[RequiredByNativeCode]
		internal static StallMarkerAnalytic CreateStallMarkerAnalytic()
		{
			return new StallMarkerAnalytic();
		}

		// Token: 0x0400006A RID: 106
		public string Name;

		// Token: 0x0400006B RID: 107
		public bool HasProgressMarkup;

		// Token: 0x0400006C RID: 108
		public double Duration;
	}
}
