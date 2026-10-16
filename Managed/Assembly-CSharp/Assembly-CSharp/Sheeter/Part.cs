using System;
using XRL.World;

namespace Sheeter
{
	// Token: 0x02000310 RID: 784
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class Part : BlueprintElement
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008DEF6 File Offset: 0x0008C0F6
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			if (!Blueprint.Parts.ContainsKey(this.Key))
			{
				return null;
			}
			return "true";
		}
	}
}
