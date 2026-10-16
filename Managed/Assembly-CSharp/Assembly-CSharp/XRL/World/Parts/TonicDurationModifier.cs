using System;

namespace XRL.World.Parts
{
	// Token: 0x020010B5 RID: 4277
	[Serializable]
	public class TonicDurationModifier : IPoweredPart
	{
		// Token: 0x0600AA66 RID: 43622 RVA: 0x003E5040 File Offset: 0x003E3240
		public TonicDurationModifier()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600AA67 RID: 43623 RVA: 0x003E5070 File Offset: 0x003E3270
		public override bool SameAs(IPart p)
		{
			TonicDurationModifier tonicDurationModifier = p as TonicDurationModifier;
			return tonicDurationModifier.Chance == this.Chance && !(tonicDurationModifier.Types != this.Types) && tonicDurationModifier.Percentage == this.Percentage && tonicDurationModifier.Pass == this.Pass && tonicDurationModifier.HealingOnly == this.HealingOnly && tonicDurationModifier.AsSubject == this.AsSubject && base.SameAs(p);
		}

		// Token: 0x0600AA68 RID: 43624 RVA: 0x003E50F0 File Offset: 0x003E32F0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetTonicDurationEvent>.ID;
		}

		// Token: 0x0600AA69 RID: 43625 RVA: 0x003E5108 File Offset: 0x003E3308
		public override bool HandleEvent(GetTonicDurationEvent E)
		{
			if (E.Pass == this.Pass && (this.Types.IsNullOrEmpty() || this.Types.CachedCommaExpansion().Contains(E.Type)) && (!this.HealingOnly || E.Healing) && E.Checking == (this.AsSubject ? "Subject" : "Actor") && this.IsObjectActivePartSubject(this.AsSubject ? E.Subject : E.Actor) && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Duration += E.BaseDuration * this.Percentage / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004629 RID: 17961
		public int Chance = 100;

		// Token: 0x0400462A RID: 17962
		public string Types;

		// Token: 0x0400462B RID: 17963
		public int Percentage = 100;

		// Token: 0x0400462C RID: 17964
		public int Pass = 2;

		// Token: 0x0400462D RID: 17965
		public bool HealingOnly;

		// Token: 0x0400462E RID: 17966
		public bool AsSubject = true;
	}
}
