using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001448 RID: 5192
	[Serializable]
	public class HeightenedStrength : BaseMutation
	{
		// Token: 0x0600CABE RID: 51902 RVA: 0x0049B7EC File Offset: 0x004999EC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || (ID == PooledEvent<CheckOverburdenedOnStrengthUpdateEvent>.ID && this.UpdatingStatShifts);
		}

		// Token: 0x0600CABF RID: 51903 RVA: 0x0049B812 File Offset: 0x00499A12
		public override bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return !this.UpdatingStatShifts && base.HandleEvent(E);
		}

		// Token: 0x0600CAC0 RID: 51904 RVA: 0x0049B825 File Offset: 0x00499A25
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAC1 RID: 51905 RVA: 0x0049B84D File Offset: 0x00499A4D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CAC2 RID: 51906 RVA: 0x0049B864 File Offset: 0x00499A64
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DealDamage" && this.GetDazedChance(base.Level).in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null)
				{
					gameObjectParameter.ApplyEffect(new Dazed("2-3".RollCached()), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CAC3 RID: 51907 RVA: 0x0049B8C4 File Offset: 0x00499AC4
		public override string GetDescription()
		{
			return "You are possessed of hulking strength.";
		}

		// Token: 0x0600CAC4 RID: 51908 RVA: 0x0049B8CB File Offset: 0x00499ACB
		public int GetDazedChance(int Level)
		{
			return 13 + 2 * Level;
		}

		// Token: 0x0600CAC5 RID: 51909 RVA: 0x0049B8D3 File Offset: 0x00499AD3
		public int GetStrengthBonus(int Level)
		{
			return 2 + (Level - 1) / 2;
		}

		// Token: 0x0600CAC6 RID: 51910 RVA: 0x0049B8DC File Offset: 0x00499ADC
		public int GetStrengthBonusAttributableToTemporaryLevels()
		{
			return this.GetStrengthBonus(base.Level) - this.GetStrengthBonus(base.Level - base.GetTemporaryLevels());
		}

		// Token: 0x0600CAC7 RID: 51911 RVA: 0x0049B900 File Offset: 0x00499B00
		public override string GetLevelText(int Level)
		{
			return string.Concat(new string[]
			{
				"+{{C|",
				this.GetStrengthBonus(Level).ToString(),
				"}} Strength\n{{C|",
				this.GetDazedChance(Level).ToString(),
				"%}} chance to daze your opponent on a successful melee attack for 2-3 rounds"
			});
		}

		// Token: 0x0600CAC8 RID: 51912 RVA: 0x0049B954 File Offset: 0x00499B54
		public override bool ChangeLevel(int NewLevel)
		{
			int strengthBonusAttributableToTemporaryLevels = this.GetStrengthBonusAttributableToTemporaryLevels();
			int num = this.GetStrengthBonus(base.Level) - strengthBonusAttributableToTemporaryLevels;
			try
			{
				this.UpdatingStatShifts = true;
				base.StatShifter.SetStatShift(this.ParentObject, "Strength", num, true);
				base.StatShifter.SetStatShift(this.ParentObject, "Strength", strengthBonusAttributableToTemporaryLevels, false);
			}
			finally
			{
				this.UpdatingStatShifts = false;
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CAC9 RID: 51913 RVA: 0x0049B9D4 File Offset: 0x00499BD4
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CACA RID: 51914 RVA: 0x0049B9DE File Offset: 0x00499BDE
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(GO);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EF8 RID: 20216
		[NonSerialized]
		private bool UpdatingStatShifts;
	}
}
