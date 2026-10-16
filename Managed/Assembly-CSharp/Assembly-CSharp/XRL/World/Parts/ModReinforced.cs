using System;

namespace XRL.World.Parts
{
	// Token: 0x02001162 RID: 4450
	[Serializable]
	public class ModReinforced : IModification
	{
		// Token: 0x0600B0F5 RID: 45301 RVA: 0x00410803 File Offset: 0x0040EA03
		public ModReinforced()
		{
		}

		// Token: 0x0600B0F6 RID: 45302 RVA: 0x0041080B File Offset: 0x0040EA0B
		public ModReinforced(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0F7 RID: 45303 RVA: 0x00410814 File Offset: 0x0040EA14
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0F8 RID: 45304 RVA: 0x00410820 File Offset: 0x0040EA20
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Body", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B0F9 RID: 45305 RVA: 0x00410843 File Offset: 0x0040EA43
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<Armor>().AV++;
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B0FA RID: 45306 RVA: 0x00410861 File Offset: 0x0040EA61
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0FB RID: 45307 RVA: 0x00410887 File Offset: 0x0040EA87
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModOverbuilt>())
			{
				E.AddAdjective("reinforced", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0FC RID: 45308 RVA: 0x004108BE File Offset: 0x0040EABE
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModOverbuilt>())
			{
				E.Postfix.AppendRules(ModReinforced.GetDescription(this.Tier));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0FD RID: 45309 RVA: 0x004108EB File Offset: 0x0040EAEB
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0FE RID: 45310 RVA: 0x0041090E File Offset: 0x0040EB0E
		public static string GetDescription(int Tier)
		{
			return "Reinforced: +1 AV";
		}
	}
}
