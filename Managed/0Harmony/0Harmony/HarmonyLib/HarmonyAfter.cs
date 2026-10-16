using System;

namespace HarmonyLib
{
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyAfter : HarmonyAttribute
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public HarmonyAfter(params string[] after)
		{
			this.info.after = after;
		}
	}
}
