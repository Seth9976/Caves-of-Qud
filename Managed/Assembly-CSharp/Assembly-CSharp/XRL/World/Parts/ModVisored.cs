using System;

namespace XRL.World.Parts
{
	// Token: 0x02001176 RID: 4470
	[Serializable]
	public class ModVisored : IModification
	{
		// Token: 0x0600B1E0 RID: 45536 RVA: 0x00412E5D File Offset: 0x0041105D
		public ModVisored()
		{
		}

		// Token: 0x0600B1E1 RID: 45537 RVA: 0x00412E65 File Offset: 0x00411065
		public ModVisored(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x00412E6E File Offset: 0x0041106E
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Visor";
		}

		// Token: 0x0600B1E3 RID: 45539 RVA: 0x00412E84 File Offset: 0x00411084
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B1E4 RID: 45540 RVA: 0x00412EA7 File Offset: 0x004110A7
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<Armor>().DV++;
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B1E5 RID: 45541 RVA: 0x00412EC4 File Offset: 0x004110C4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1E6 RID: 45542 RVA: 0x00412EE2 File Offset: 0x004110E2
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("visored", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1E7 RID: 45543 RVA: 0x00412F0C File Offset: 0x0041110C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModVisored.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1E8 RID: 45544 RVA: 0x00412F2C File Offset: 0x0041112C
		public static string GetDescription(int Tier)
		{
			return "Visored: +1 DV";
		}
	}
}
