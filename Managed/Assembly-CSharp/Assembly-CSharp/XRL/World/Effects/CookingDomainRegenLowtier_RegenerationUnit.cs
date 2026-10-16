using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CFC RID: 3324
	[Serializable]
	public class CookingDomainRegenLowtier_RegenerationUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x0600837D RID: 33661 RVA: 0x002FAE62 File Offset: 0x002F9062
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(10, 15);
		}

		// Token: 0x0600837E RID: 33662 RVA: 0x002FAE73 File Offset: 0x002F9073
		public override string GetDescription()
		{
			return this.Tier.Signed(false) + "% to natural healing rate";
		}

		// Token: 0x0600837F RID: 33663 RVA: 0x002FAE8B File Offset: 0x002F908B
		public override string GetTemplatedDescription()
		{
			return "+10-15% to natural healing rate";
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x002FAE92 File Offset: 0x002F9092
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x06008381 RID: 33665 RVA: 0x002FAEA0 File Offset: 0x002F90A0
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x06008382 RID: 33666 RVA: 0x002FAEB0 File Offset: 0x002F90B0
		public override void FireEvent(Event E)
		{
			if (E.ID == "Regenerating2")
			{
				float num = 1f + (float)this.Tier * 0.01f;
				int num2 = (int)Math.Ceiling((double)((float)E.GetIntParameter("Amount", 0) * num));
				E.SetParameter("Amount", num2);
				return;
			}
		}

		// Token: 0x04003967 RID: 14695
		public int Tier;
	}
}
