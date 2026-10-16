using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x0200112E RID: 4398
	[Serializable]
	public class ModCounterweighted : IMeleeModification
	{
		// Token: 0x0600AE1B RID: 44571 RVA: 0x00405C78 File Offset: 0x00403E78
		public ModCounterweighted()
		{
		}

		// Token: 0x0600AE1C RID: 44572 RVA: 0x00405C80 File Offset: 0x00403E80
		public ModCounterweighted(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE1D RID: 44573 RVA: 0x00405C89 File Offset: 0x00403E89
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "BalanceSystems";
		}

		// Token: 0x0600AE1E RID: 44574 RVA: 0x00405C9D File Offset: 0x00403E9D
		public override bool SameAs(IPart p)
		{
			return (p as ModCounterweighted).Tier == this.Tier && base.SameAs(p);
		}

		// Token: 0x0600AE1F RID: 44575 RVA: 0x00405CBB File Offset: 0x00403EBB
		public static int GetModificationLevel(int Tier)
		{
			return (int)Math.Ceiling((double)((float)(Tier + 1) / 3f));
		}

		// Token: 0x0600AE20 RID: 44576 RVA: 0x00405CCE File Offset: 0x00403ECE
		public int GetModificationLevel()
		{
			return ModCounterweighted.GetModificationLevel(this.Tier);
		}

		// Token: 0x0600AE21 RID: 44577 RVA: 0x00405CDB File Offset: 0x00403EDB
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600AE22 RID: 44578 RVA: 0x00405CE8 File Offset: 0x00403EE8
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<MeleeWeapon>().HitBonus += this.GetModificationLevel();
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AE23 RID: 44579 RVA: 0x00405D0B File Offset: 0x00403F0B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE24 RID: 44580 RVA: 0x00405D2C File Offset: 0x00403F2C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				int modificationLevel = this.GetModificationLevel();
				stringBuilder.Append("counterweighted");
				if (modificationLevel > 1)
				{
					stringBuilder.Append('(').Append(modificationLevel).Append(')');
				}
				E.AddAdjective(stringBuilder.ToString(), 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE25 RID: 44581 RVA: 0x00405D96 File Offset: 0x00403F96
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModCounterweighted.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE26 RID: 44582 RVA: 0x00405DB6 File Offset: 0x00403FB6
		public static string GetDescription(int Tier)
		{
			return "Counterweighted: Adds " + ((Tier > 0) ? ModCounterweighted.GetModificationLevel(Tier).Signed(false) : "a bonus") + " to hit.";
		}
	}
}
