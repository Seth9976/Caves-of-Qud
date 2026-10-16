using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200002A RID: 42
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class TestAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000260F File Offset: 0x0000080F
		public TestAnalytic()
			: base("TestAnalytic", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002628 File Offset: 0x00000828
		[RequiredByNativeCode]
		public static TestAnalytic CreateTestAnalytic()
		{
			return new TestAnalytic();
		}

		// Token: 0x0400006D RID: 109
		public int param;
	}
}
