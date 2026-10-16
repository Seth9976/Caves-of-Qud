using System;

namespace HarmonyLib
{
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyPriority : HarmonyAttribute
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000AF90 File Offset: 0x00009190
		public HarmonyPriority(int priority)
		{
			this.info.priority = priority;
		}
	}
}
