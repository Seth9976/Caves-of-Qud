using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001128 RID: 4392
	[Serializable]
	public class ModBeamsplitter : IModification
	{
		// Token: 0x0600ADC4 RID: 44484 RVA: 0x00404D31 File Offset: 0x00402F31
		public ModBeamsplitter()
		{
		}

		// Token: 0x0600ADC5 RID: 44485 RVA: 0x00404D39 File Offset: 0x00402F39
		public ModBeamsplitter(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADC6 RID: 44486 RVA: 0x00404D42 File Offset: 0x00402F42
		public override void Configure()
		{
			base.IsTechScannable = true;
			this.WorksOnSelf = true;
			this.NameForStatus = "Beamsplitter";
		}

		// Token: 0x0600ADC7 RID: 44487 RVA: 0x00404D5D File Offset: 0x00402F5D
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MissileWeapon>();
		}

		// Token: 0x0600ADC8 RID: 44488 RVA: 0x00404D68 File Offset: 0x00402F68
		public override void ApplyModification(GameObject Object)
		{
			MissileWeapon part = Object.GetPart<MissileWeapon>();
			if (part != null)
			{
				part.ShotsPerAction *= 3;
				part.ShotsPerAnimation *= 3;
			}
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600ADC9 RID: 44489 RVA: 0x00404DA4 File Offset: 0x00402FA4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetFixedMissileSpreadEvent>.ID || ID == PooledEvent<GetMissileWeaponPerformanceEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADCA RID: 44490 RVA: 0x00404DD2 File Offset: 0x00402FD2
		public override bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.Spread = 18;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADCB RID: 44491 RVA: 0x00404DF3 File Offset: 0x00402FF3
		public override bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			if (E.Subject == this.ParentObject)
			{
				E.BasePenetration--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADCC RID: 44492 RVA: 0x00404E18 File Offset: 0x00403018
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{R-R-r-r-g-g-G-G-B-B-b-b sequence|beamsplitter}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADCD RID: 44493 RVA: 0x00404E41 File Offset: 0x00403041
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModBeamsplitter.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADCE RID: 44494 RVA: 0x00404E61 File Offset: 0x00403061
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ADCF RID: 44495 RVA: 0x00404E64 File Offset: 0x00403064
		public static string GetDescription(int Tier)
		{
			return "Fitted with beamsplitter: This weapon has " + Grammar.A(3, false) + "-way spread with each shot at -1 penetration roll.";
		}

		// Token: 0x040047DC RID: 18396
		public const int SPLIT_FACTOR = 3;
	}
}
