using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118F RID: 4495
	[Serializable]
	public class ModNanochelated : IModification
	{
		// Token: 0x0600B2BE RID: 45758 RVA: 0x00414A76 File Offset: 0x00412C76
		public ModNanochelated()
		{
		}

		// Token: 0x0600B2BF RID: 45759 RVA: 0x00414A7E File Offset: 0x00412C7E
		public ModNanochelated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2C0 RID: 45760 RVA: 0x00414A87 File Offset: 0x00412C87
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B2C1 RID: 45761 RVA: 0x00414A90 File Offset: 0x00412C90
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Metal>();
		}

		// Token: 0x0600B2C2 RID: 45762 RVA: 0x00414AA0 File Offset: 0x00412CA0
		public override void ApplyModification(GameObject Object)
		{
			Object.RemovePart<Metal>();
			Armor part = Object.GetPart<Armor>();
			Shield part2 = Object.GetPart<Shield>();
			if (part == null && part2 == null)
			{
				MeleeWeapon part3 = Object.GetPart<MeleeWeapon>();
				if (part3 != null)
				{
					part3.PenBonus--;
				}
			}
			else
			{
				if (part != null)
				{
					if (part.AV > 0)
					{
						Armor armor = part;
						int num = armor.AV;
						armor.AV = num - 1;
					}
					int num2 = (this.ParentObject.HasPart<ModVisored>() ? 1 : 0);
					if (part.DV < num2)
					{
						Armor armor2 = part;
						int num = armor2.DV;
						armor2.DV = num + 1;
					}
				}
				if (part2 != null)
				{
					if (part2.AV > 0)
					{
						part2.AV--;
					}
					if (part2.DV < 0)
					{
						part2.DV++;
					}
				}
			}
			Object.IsOrganic = true;
			base.IncreaseDifficultyAndComplexityIfComplex(2, 1, null);
		}

		// Token: 0x0600B2C3 RID: 45763 RVA: 0x00414B71 File Offset: 0x00412D71
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2C4 RID: 45764 RVA: 0x00414B8F File Offset: 0x00412D8F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|nanochelated}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2C5 RID: 45765 RVA: 0x00414BB9 File Offset: 0x00412DB9
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModNanochelated.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x00414BD9 File Offset: 0x00412DD9
		public static string GetDescription(int Tier)
		{
			return "Nanochelated: This item's metals have been replaced with carbon fiber. -1 AV or penetration, +1 DV if below zero";
		}

		// Token: 0x0600B2C7 RID: 45767 RVA: 0x00414BE0 File Offset: 0x00412DE0
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&K", "y", ModNanochelated.ICON_COLOR_FOREGROUND_PRIORITY, ModNanochelated.ICON_COLOR_DETAIL_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x04004839 RID: 18489
		public static readonly int ICON_COLOR_FOREGROUND_PRIORITY = 60;

		// Token: 0x0400483A RID: 18490
		public static readonly int ICON_COLOR_DETAIL_PRIORITY = 80;
	}
}
