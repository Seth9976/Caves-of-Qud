using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class LicensingErrorAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000022AF File Offset: 0x000004AF
		public LicensingErrorAnalytic()
			: base("license_error", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022C8 File Offset: 0x000004C8
		[RequiredByNativeCode]
		internal static LicensingErrorAnalytic CreateLicensingErrorAnalytic()
		{
			return new LicensingErrorAnalytic();
		}

		// Token: 0x04000037 RID: 55
		public string licensingErrorType;

		// Token: 0x04000038 RID: 56
		public string additionalData;

		// Token: 0x04000039 RID: 57
		public string errorMessage;

		// Token: 0x0400003A RID: 58
		public string correlationId;

		// Token: 0x0400003B RID: 59
		public string sessionId;
	}
}
