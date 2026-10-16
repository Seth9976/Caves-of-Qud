using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C96 RID: 3222
	[Serializable]
	public class CookingDomainElectric_Discharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600819C RID: 33180 RVA: 0x002F68C5 File Offset: 0x002F4AC5
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(5, 6);
			base.Init(target);
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x002F68DB File Offset: 0x002F4ADB
		public override string GetDescription()
		{
			return "@they release an electrical discharge per Electrical Generation at level " + this.Tier.ToString() + ".";
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x002F68F7 File Offset: 0x002F4AF7
		public override string GetTemplatedDescription()
		{
			return "@they release an electrical discharge per Electrical Generation at level 5-6.";
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x002F68FE File Offset: 0x002F4AFE
		public override string GetNotification()
		{
			return "@they release a powerful electrical discharge.";
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x002F6905 File Offset: 0x002F4B05
		public override void Apply(GameObject go)
		{
			ElectricalGeneration electricalGeneration = new ElectricalGeneration();
			electricalGeneration.ParentObject = go;
			electricalGeneration.Level = this.Tier;
			electricalGeneration.Charge = electricalGeneration.GetMaxCharge();
			electricalGeneration.PerformDischarge(false);
		}

		// Token: 0x04003924 RID: 14628
		public int Tier = 5;
	}
}
