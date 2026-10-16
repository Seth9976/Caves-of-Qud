using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F4F RID: 3919
	[Serializable]
	public class CyberneticsTonicDurationModifier : IActivePart
	{
		// Token: 0x06009C6F RID: 40047 RVA: 0x0038B94A File Offset: 0x00389B4A
		public CyberneticsTonicDurationModifier()
		{
			this.WorksOnImplantee = true;
		}

		// Token: 0x06009C70 RID: 40048 RVA: 0x0038B978 File Offset: 0x00389B78
		public override bool SameAs(IPart p)
		{
			CyberneticsTonicDurationModifier cyberneticsTonicDurationModifier = p as CyberneticsTonicDurationModifier;
			return cyberneticsTonicDurationModifier.Chance == this.Chance && !(cyberneticsTonicDurationModifier.Types != this.Types) && cyberneticsTonicDurationModifier.Percentage == this.Percentage && cyberneticsTonicDurationModifier.Pass == this.Pass && cyberneticsTonicDurationModifier.HealingOnly == this.HealingOnly && cyberneticsTonicDurationModifier.AsSubject == this.AsSubject && base.SameAs(p);
		}

		// Token: 0x06009C71 RID: 40049 RVA: 0x0038B9F8 File Offset: 0x00389BF8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetTonicDurationEvent>.ID;
		}

		// Token: 0x06009C72 RID: 40050 RVA: 0x0038BA10 File Offset: 0x00389C10
		public override bool HandleEvent(GetTonicDurationEvent E)
		{
			if (E.Pass == this.Pass && (this.Types.IsNullOrEmpty() || this.Types.CachedCommaExpansion().Contains(E.Type)) && (!this.HealingOnly || E.Healing) && E.Checking == (this.AsSubject ? "Subject" : "Actor") && this.IsObjectActivePartSubject(this.AsSubject ? E.Subject : E.Actor) && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Duration += E.BaseDuration * this.Percentage / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040B1 RID: 16561
		public int Chance = 100;

		// Token: 0x040040B2 RID: 16562
		public string Types;

		// Token: 0x040040B3 RID: 16563
		public int Percentage = 100;

		// Token: 0x040040B4 RID: 16564
		public int Pass = 2;

		// Token: 0x040040B5 RID: 16565
		public bool HealingOnly;

		// Token: 0x040040B6 RID: 16566
		public bool AsSubject = true;
	}
}
