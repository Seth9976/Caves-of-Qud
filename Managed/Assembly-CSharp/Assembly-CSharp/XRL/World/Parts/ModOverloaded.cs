using System;

namespace XRL.World.Parts
{
	// Token: 0x02001156 RID: 4438
	[Serializable]
	public class ModOverloaded : IModification
	{
		// Token: 0x0600B04F RID: 45135 RVA: 0x0040EBA6 File Offset: 0x0040CDA6
		public ModOverloaded()
		{
		}

		// Token: 0x0600B050 RID: 45136 RVA: 0x0040EBAE File Offset: 0x0040CDAE
		public ModOverloaded(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B051 RID: 45137 RVA: 0x0040EBB7 File Offset: 0x0040CDB7
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.IsEMPSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "PowerRegulator";
		}

		// Token: 0x0600B052 RID: 45138 RVA: 0x0040EBD9 File Offset: 0x0040CDD9
		public override bool ModificationApplicable(GameObject Object)
		{
			return IsOverloadableEvent.Check(Object);
		}

		// Token: 0x0600B053 RID: 45139 RVA: 0x0040EBE1 File Offset: 0x0040CDE1
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(2, 1, null);
		}

		// Token: 0x0600B054 RID: 45140 RVA: 0x0040EBEC File Offset: 0x0040CDEC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetPowerLoadLevelEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B055 RID: 45141 RVA: 0x0040EC12 File Offset: 0x0040CE12
		public override bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			E.Level += 300;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B056 RID: 45142 RVA: 0x0040EC2D File Offset: 0x0040CE2D
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModMassivelyOverloaded>())
			{
				E.AddAdjective("{{overloaded|overloaded}}", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B057 RID: 45143 RVA: 0x0040EC65 File Offset: 0x0040CE65
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModMassivelyOverloaded>())
			{
				E.Postfix.AppendRules(ModOverloaded.GetDescription(this.Tier));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B058 RID: 45144 RVA: 0x0040EC92 File Offset: 0x0040CE92
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B059 RID: 45145 RVA: 0x0040EC95 File Offset: 0x0040CE95
		public override void Register(GameObject Basis, IEventRegistrar Registrar)
		{
			Registrar.Register("QueryMissileFireSound");
		}

		// Token: 0x0600B05A RID: 45146 RVA: 0x0040ECA4 File Offset: 0x0040CEA4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "QueryMissileFireSound")
			{
				base.PlayWorldSound("sfx_missile_addLayer_overloaded_fire", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B05B RID: 45147 RVA: 0x0040ECF5 File Offset: 0x0040CEF5
		public static string GetDescription(int Tier)
		{
			return "Overloaded: This item has increased performance but consumes extra charge, generates heat when used, and has a chance to break relative to its charge draw.";
		}
	}
}
