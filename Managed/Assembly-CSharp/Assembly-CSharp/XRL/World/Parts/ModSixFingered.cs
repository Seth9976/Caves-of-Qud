using System;

namespace XRL.World.Parts
{
	// Token: 0x0200116A RID: 4458
	[Serializable]
	public class ModSixFingered : IModification
	{
		// Token: 0x0600B158 RID: 45400 RVA: 0x004117D6 File Offset: 0x0040F9D6
		public ModSixFingered()
		{
		}

		// Token: 0x0600B159 RID: 45401 RVA: 0x004117DE File Offset: 0x0040F9DE
		public ModSixFingered(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B15A RID: 45402 RVA: 0x004117E7 File Offset: 0x0040F9E7
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B15B RID: 45403 RVA: 0x004117F0 File Offset: 0x0040F9F0
		public override void ApplyModification(GameObject Object)
		{
			if (Object.HasPart<Armor>())
			{
				Object.GetPart<Armor>().Agility++;
			}
			else
			{
				EquipStatBoost.AppendBoostOnEquip(Object, "Agility:1", null, false);
			}
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B15C RID: 45404 RVA: 0x00411825 File Offset: 0x0040FA25
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ShouldDescribeStatBonusEvent>.ID;
		}

		// Token: 0x0600B15D RID: 45405 RVA: 0x0041184B File Offset: 0x0040FA4B
		public override bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return (!(E.Component is Armor) || !(E.Stat == "Agility")) && base.HandleEvent(E);
		}

		// Token: 0x0600B15E RID: 45406 RVA: 0x00411875 File Offset: 0x0040FA75
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{G|six-fingered}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B15F RID: 45407 RVA: 0x0041189F File Offset: 0x0040FA9F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSixFingered.GetDescription(this.Tier, this.ParentObject));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B160 RID: 45408 RVA: 0x004118C5 File Offset: 0x0040FAC5
		public static string GetDescription(int Tier)
		{
			return "Six-Fingered: +1 Agility";
		}

		// Token: 0x0600B161 RID: 45409 RVA: 0x004118CC File Offset: 0x0040FACC
		public static string GetDescription(int Tier, GameObject obj)
		{
			Armor part = obj.GetPart<Armor>();
			int num = Math.Max((part != null) ? part.Agility : 1, 1);
			return "Six-Fingered: " + num.Signed(false) + " Agility";
		}
	}
}
