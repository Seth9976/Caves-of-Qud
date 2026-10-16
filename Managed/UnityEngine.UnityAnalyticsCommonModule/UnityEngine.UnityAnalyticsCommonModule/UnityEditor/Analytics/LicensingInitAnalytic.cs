using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000017 RID: 23
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class LicensingInitAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000022DF File Offset: 0x000004DF
		public LicensingInitAnalytic()
			: base("license_init", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022F8 File Offset: 0x000004F8
		[RequiredByNativeCode]
		internal static LicensingInitAnalytic CreateLicensingInitAnalytic()
		{
			return new LicensingInitAnalytic();
		}

		// Token: 0x0400003C RID: 60
		public string licensingProtocolVersion;

		// Token: 0x0400003D RID: 61
		public string licensingClientVersion;

		// Token: 0x0400003E RID: 62
		public string channelType;

		// Token: 0x0400003F RID: 63
		public double initTime;

		// Token: 0x04000040 RID: 64
		public bool isLegacy;

		// Token: 0x04000041 RID: 65
		public string sessionId;

		// Token: 0x04000042 RID: 66
		public string correlationId;
	}
}
