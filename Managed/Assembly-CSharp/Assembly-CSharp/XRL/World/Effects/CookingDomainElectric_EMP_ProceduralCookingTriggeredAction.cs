using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C95 RID: 3221
	[Serializable]
	public class CookingDomainElectric_EMP_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008196 RID: 33174 RVA: 0x002F683C File Offset: 0x002F4A3C
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(8, 9);
			base.Init(target);
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x002F6853 File Offset: 0x002F4A53
		public override string GetDescription()
		{
			return "@they release an electromagnetic pulse at level " + this.Tier.ToString() + ".";
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x002F686F File Offset: 0x002F4A6F
		public override string GetTemplatedDescription()
		{
			return "@they release an electromagnetic pulse at level 8-9.";
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x002F6876 File Offset: 0x002F4A76
		public override string GetNotification()
		{
			return "@they release a powerful electromagnetic pulse.";
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x002F687D File Offset: 0x002F4A7D
		public override void Apply(GameObject go)
		{
			ElectromagneticPulse.EMP(go.CurrentCell, ElectromagneticPulse.GetRadius(this.Tier), Stat.Random(4 + this.Tier * 2, 13 + this.Tier * 2), false, go.GetPhase());
		}

		// Token: 0x04003923 RID: 14627
		public int Tier = 8;
	}
}
