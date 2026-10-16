using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200116F RID: 4463
	[Serializable]
	public class ModSpringLoaded : IModification
	{
		// Token: 0x0600B18F RID: 45455 RVA: 0x00411F46 File Offset: 0x00410146
		public ModSpringLoaded()
		{
		}

		// Token: 0x0600B190 RID: 45456 RVA: 0x00411F4E File Offset: 0x0041014E
		public ModSpringLoaded(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B191 RID: 45457 RVA: 0x00411F57 File Offset: 0x00410157
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "MobilityAssist";
		}

		// Token: 0x0600B192 RID: 45458 RVA: 0x00411F6C File Offset: 0x0041016C
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Feet", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B193 RID: 45459 RVA: 0x00411F90 File Offset: 0x00410190
		public override void ApplyModification(GameObject Object)
		{
			int num = Stat.Random(-10, -5) + this.Tier - Object.GetTier();
			MoveCostMultiplier part = this.ParentObject.GetPart<MoveCostMultiplier>();
			if (part != null)
			{
				part.Amount += num;
			}
			else
			{
				this.ParentObject.AddPart<MoveCostMultiplier>(new MoveCostMultiplier(num), true, false);
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B194 RID: 45460 RVA: 0x00411FF1 File Offset: 0x004101F1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B195 RID: 45461 RVA: 0x0041200F File Offset: 0x0041020F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("spring-loaded", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B196 RID: 45462 RVA: 0x00412039 File Offset: 0x00410239
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSpringLoaded.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B197 RID: 45463 RVA: 0x00412059 File Offset: 0x00410259
		public static string GetDescription(int Tier)
		{
			return "Spring-loaded: This item grants bonus move speed.";
		}
	}
}
