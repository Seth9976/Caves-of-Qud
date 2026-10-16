using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001191 RID: 4497
	[Serializable]
	public class ModOverbuilt : IModification
	{
		// Token: 0x0600B2D3 RID: 45779 RVA: 0x00414D92 File Offset: 0x00412F92
		public ModOverbuilt()
		{
		}

		// Token: 0x0600B2D4 RID: 45780 RVA: 0x00414D9A File Offset: 0x00412F9A
		public ModOverbuilt(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x00414DA3 File Offset: 0x00412FA3
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B2D6 RID: 45782 RVA: 0x00414DAC File Offset: 0x00412FAC
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ModReinforced>();
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x00414DB4 File Offset: 0x00412FB4
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<Armor>().AV += 2;
			Object.GetPart<Armor>().DV -= 2;
			Object.GetPart<Armor>().SpeedPenalty += 10;
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B2D8 RID: 45784 RVA: 0x00414E04 File Offset: 0x00413004
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetIntrinsicWeightEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2D9 RID: 45785 RVA: 0x00414E32 File Offset: 0x00413032
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("overbuilt", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2DA RID: 45786 RVA: 0x00414E5C File Offset: 0x0041305C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModOverbuilt.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2DB RID: 45787 RVA: 0x00414E7C File Offset: 0x0041307C
		public override bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			E.Weight += (double)this.GetAddedWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2DC RID: 45788 RVA: 0x00414E99 File Offset: 0x00413099
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2DD RID: 45789 RVA: 0x00414EBC File Offset: 0x004130BC
		public static string GetDescription(int Tier)
		{
			return "Overbuilt: +3 AV, -2 DV, -10 move speed, x2 weight";
		}

		// Token: 0x0600B2DE RID: 45790 RVA: 0x00414EC4 File Offset: 0x004130C4
		public int GetAddedWeight()
		{
			if (!this.HaveAddedWeight)
			{
				try
				{
					this.AddedWeight = this.ParentObject.GetBlueprint(true).GetPartParameter<int>("Physics", "Weight", 0);
				}
				catch
				{
				}
				if (this.AddedWeight < 2)
				{
					this.AddedWeight = 2;
				}
				this.HaveAddedWeight = true;
			}
			return this.AddedWeight;
		}

		// Token: 0x0400483C RID: 18492
		[NonSerialized]
		private bool HaveAddedWeight;

		// Token: 0x0400483D RID: 18493
		[NonSerialized]
		private int AddedWeight;
	}
}
