using System;

namespace XRL.World.Parts
{
	// Token: 0x0200114F RID: 4431
	[Serializable]
	public class ModMasterwork : IModification
	{
		// Token: 0x0600AFF6 RID: 45046 RVA: 0x0040D51F File Offset: 0x0040B71F
		public ModMasterwork()
		{
		}

		// Token: 0x0600AFF7 RID: 45047 RVA: 0x0040D52E File Offset: 0x0040B72E
		public ModMasterwork(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFF8 RID: 45048 RVA: 0x0040D53E File Offset: 0x0040B73E
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AFF9 RID: 45049 RVA: 0x0040D547 File Offset: 0x0040B747
		public override bool ModificationApplicable(GameObject Object)
		{
			return (Object.HasPart<MeleeWeapon>() || Object.HasPart<MissileWeapon>()) && !Object.HasPart<GeomagneticDisc>();
		}

		// Token: 0x0600AFFA RID: 45050 RVA: 0x0040D566 File Offset: 0x0040B766
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AFFB RID: 45051 RVA: 0x0040D571 File Offset: 0x0040B771
		public override bool SameAs(IPart Part)
		{
			return (Part as ModMasterwork).Bonus == this.Bonus && base.SameAs(Part);
		}

		// Token: 0x0600AFFC RID: 45052 RVA: 0x0040D58F File Offset: 0x0040B78F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCriticalThresholdEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<TriggersMakersMarkCreationEvent>.ID;
		}

		// Token: 0x0600AFFD RID: 45053 RVA: 0x0040D5C5 File Offset: 0x0040B7C5
		public override bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return E.ModAdded != null && E.ModAdded != this && base.HandleEvent(E);
		}

		// Token: 0x0600AFFE RID: 45054 RVA: 0x0040D5E1 File Offset: 0x0040B7E1
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModLegendary>())
			{
				E.AddAdjective("{{Y|masterwork}}", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFFF RID: 45055 RVA: 0x0040D61C File Offset: 0x0040B81C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModLegendary>())
			{
				E.Postfix.Append("\n{{rules|Masterwork").Append(": This weapon scores critical hits ").Append(this.Bonus * 5 + 5)
					.Append("% of the time instead of 5%.}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B000 RID: 45056 RVA: 0x0040D674 File Offset: 0x0040B874
		public override bool HandleEvent(GetCriticalThresholdEvent E)
		{
			if ((E.Weapon == this.ParentObject || E.Projectile == this.ParentObject) && !this.ParentObject.HasPart<ModLegendary>())
			{
				E.Threshold -= this.Bonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B001 RID: 45057 RVA: 0x0040D6C4 File Offset: 0x0040B8C4
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B002 RID: 45058 RVA: 0x0040D6E7 File Offset: 0x0040B8E7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004809 RID: 18441
		public int Bonus = 1;
	}
}
