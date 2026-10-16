using System;

namespace XRL.World.Parts
{
	// Token: 0x0200113C RID: 4412
	[Serializable]
	public class ModFlexiweaved : IModification
	{
		// Token: 0x0600AEF4 RID: 44788 RVA: 0x004095AB File Offset: 0x004077AB
		public ModFlexiweaved()
		{
		}

		// Token: 0x0600AEF5 RID: 44789 RVA: 0x004095B3 File Offset: 0x004077B3
		public ModFlexiweaved(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AEF6 RID: 44790 RVA: 0x004095BC File Offset: 0x004077BC
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AEF7 RID: 44791 RVA: 0x004095C8 File Offset: 0x004077C8
		public override bool ModificationApplicable(GameObject Object)
		{
			Armor part = Object.GetPart<Armor>();
			if (part == null)
			{
				return false;
			}
			int num = part.DV;
			if (Object.HasPart<ModVisored>())
			{
				num--;
			}
			return num < 0;
		}

		// Token: 0x0600AEF8 RID: 44792 RVA: 0x004095FC File Offset: 0x004077FC
		public override void ApplyModification(GameObject Object)
		{
			Armor part = Object.GetPart<Armor>();
			part.DV += this.GetModificationLevel();
			int num = (Object.HasPart<ModVisored>() ? 1 : 0);
			if (part.DV > num)
			{
				part.DV = num;
			}
			base.IncreaseComplexity(1, null);
		}

		// Token: 0x0600AEF9 RID: 44793 RVA: 0x00409648 File Offset: 0x00407848
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AEFA RID: 44794 RVA: 0x00409668 File Offset: 0x00407868
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("flexiweaved(" + this.GetModificationLevel().ToString() + ")", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEFB RID: 44795 RVA: 0x004096B5 File Offset: 0x004078B5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModFlexiweaved.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEFC RID: 44796 RVA: 0x004096D5 File Offset: 0x004078D5
		public static int GetModificationLevel(int Tier)
		{
			return (int)Math.Ceiling((double)((float)Tier / 2f));
		}

		// Token: 0x0600AEFD RID: 44797 RVA: 0x004096E6 File Offset: 0x004078E6
		public int GetModificationLevel()
		{
			return ModFlexiweaved.GetModificationLevel(this.Tier);
		}

		// Token: 0x0600AEFE RID: 44798 RVA: 0x004096F4 File Offset: 0x004078F4
		public static string GetDescription(int Tier)
		{
			return "Flexiweaved: This item's DV penalty is reduced" + ((Tier > 0) ? (" by " + ModFlexiweaved.GetModificationLevel(Tier).ToString()) : "") + ".";
		}
	}
}
