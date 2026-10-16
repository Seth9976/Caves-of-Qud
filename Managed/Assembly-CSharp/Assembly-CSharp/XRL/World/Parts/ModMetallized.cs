using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118C RID: 4492
	[Serializable]
	public class ModMetallized : IModification
	{
		// Token: 0x0600B29A RID: 45722 RVA: 0x0041466F File Offset: 0x0041286F
		public ModMetallized()
		{
		}

		// Token: 0x0600B29B RID: 45723 RVA: 0x00414677 File Offset: 0x00412877
		public ModMetallized(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B29C RID: 45724 RVA: 0x00414680 File Offset: 0x00412880
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B29D RID: 45725 RVA: 0x00414689 File Offset: 0x00412889
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.Physics != null && !Object.HasPart<Metal>();
		}

		// Token: 0x0600B29E RID: 45726 RVA: 0x004146A0 File Offset: 0x004128A0
		public override void ApplyModification(GameObject Object)
		{
			Object.AddPart<Metal>(true, false);
			Armor part = Object.GetPart<Armor>();
			Shield part2 = Object.GetPart<Shield>();
			if (part == null && part2 == null)
			{
				MeleeWeapon part3 = Object.GetPart<MeleeWeapon>();
				if (part3 != null)
				{
					part3.PenBonus++;
				}
			}
			else
			{
				if (part != null)
				{
					Armor armor = part;
					int av = armor.AV;
					armor.AV = av + 1;
				}
				if (part2 != null)
				{
					part2.AV++;
				}
			}
			Object.IsOrganic = false;
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B29F RID: 45727 RVA: 0x00414718 File Offset: 0x00412918
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2A0 RID: 45728 RVA: 0x00414736 File Offset: 0x00412936
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|metallized}}", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2A1 RID: 45729 RVA: 0x00414761 File Offset: 0x00412961
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModMetallized.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2A2 RID: 45730 RVA: 0x00414781 File Offset: 0x00412981
		public static string GetDescription(int Tier)
		{
			return "Metallized: +1 AV or penetration";
		}

		// Token: 0x0600B2A3 RID: 45731 RVA: 0x00414788 File Offset: 0x00412988
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&c", "C", ModMetallized.ICON_COLOR_FOREGROUND_PRIORITY, ModMetallized.ICON_COLOR_DETAIL_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x04004836 RID: 18486
		public static readonly int ICON_COLOR_FOREGROUND_PRIORITY = 50;

		// Token: 0x04004837 RID: 18487
		public static readonly int ICON_COLOR_DETAIL_PRIORITY = 90;
	}
}
