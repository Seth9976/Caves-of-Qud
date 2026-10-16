using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C63 RID: 3171
	[Serializable]
	public class ProceduralCookingEffectUnit_LessThirst : ProceduralCookingEffectUnit
	{
		// Token: 0x060080E7 RID: 32999 RVA: 0x002F55CC File Offset: 0x002F37CC
		public override string GetDescription()
		{
			return "@thisCreature thirst@s at half rate.";
		}

		// Token: 0x060080E8 RID: 33000 RVA: 0x002F55D3 File Offset: 0x002F37D3
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "CalculatingThirst");
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x002F55E1 File Offset: 0x002F37E1
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "CalculatingThirst");
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x002F55F0 File Offset: 0x002F37F0
		public override void FireEvent(Event E)
		{
			if (E.ID == "CalculatingThirst")
			{
				int num = (int)Math.Ceiling((double)((float)E.GetIntParameter("Amount", 0) * 0.5f));
				if (num < 1)
				{
					num = 1;
				}
				E.SetParameter("Amount", num);
				return;
			}
		}
	}
}
