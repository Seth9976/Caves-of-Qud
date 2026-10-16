using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002A9 RID: 681
	internal class ScriptingUtility
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x00041574 File Offset: 0x0003F774
		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00002925 File Offset: 0x00000B25
		[RequiredByNativeCode]
		private static void SetupCallbacks(IntPtr p)
		{
		}

		// Token: 0x020002AA RID: 682
		private struct TestClass
		{
			// Token: 0x040009D2 RID: 2514
			public int value;
		}
	}
}
