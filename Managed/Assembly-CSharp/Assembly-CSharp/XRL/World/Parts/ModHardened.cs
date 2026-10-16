using System;

namespace XRL.World.Parts
{
	// Token: 0x02001144 RID: 4420
	[Serializable]
	public class ModHardened : IModification
	{
		// Token: 0x0600AF66 RID: 44902 RVA: 0x0040B283 File Offset: 0x00409483
		public ModHardened()
		{
		}

		// Token: 0x0600AF67 RID: 44903 RVA: 0x0040B28B File Offset: 0x0040948B
		public ModHardened(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF68 RID: 44904 RVA: 0x0040B294 File Offset: 0x00409494
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "ElectromagneticShielding";
		}

		// Token: 0x0600AF69 RID: 44905 RVA: 0x0040B2A8 File Offset: 0x004094A8
		private void HardenActivePart(IActivePart Part)
		{
			Part.IsEMPSensitive = false;
		}

		// Token: 0x0600AF6A RID: 44906 RVA: 0x0040B2B1 File Offset: 0x004094B1
		public void Harden(GameObject Object)
		{
			Object.ForeachPartDescendedFrom<IActivePart>(new Action<IActivePart>(this.HardenActivePart));
		}

		// Token: 0x0600AF6B RID: 44907 RVA: 0x0040B2C5 File Offset: 0x004094C5
		public bool HardenablePartCheck(IActivePart Part)
		{
			return !Part.IsEMPSensitive;
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x0040B2D0 File Offset: 0x004094D0
		public override bool ModificationApplicable(GameObject Object)
		{
			return CanBeMagneticallyManipulatedEvent.Check(Object) || !Object.ForeachPartDescendedFrom<IActivePart>(new Predicate<IActivePart>(this.HardenablePartCheck));
		}

		// Token: 0x0600AF6D RID: 44909 RVA: 0x0040B2F3 File Offset: 0x004094F3
		public override void ApplyModification(GameObject Object)
		{
			this.Harden(Object);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AF6E RID: 44910 RVA: 0x0040B305 File Offset: 0x00409505
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeMagneticallyManipulatedEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ModificationAppliedEvent>.ID;
		}

		// Token: 0x0600AF6F RID: 44911 RVA: 0x0040B333 File Offset: 0x00409533
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{mercurial|electromagnetic}} shielding");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF70 RID: 44912 RVA: 0x0040B35C File Offset: 0x0040955C
		public override bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			E.Allow = false;
			return false;
		}

		// Token: 0x0600AF71 RID: 44913 RVA: 0x0040B366 File Offset: 0x00409566
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModHardened.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF72 RID: 44914 RVA: 0x0040B386 File Offset: 0x00409586
		public override bool HandleEvent(ModificationAppliedEvent E)
		{
			this.Harden(this.ParentObject);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF73 RID: 44915 RVA: 0x0040B39B File Offset: 0x0040959B
		public static string GetDescription(int Tier)
		{
			return "Electromagnetic shielding: This item is immune to electromagnetic pulses and magnetic pull.";
		}
	}
}
