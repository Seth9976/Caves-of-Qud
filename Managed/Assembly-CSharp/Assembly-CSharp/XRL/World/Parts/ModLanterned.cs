using System;

namespace XRL.World.Parts
{
	// Token: 0x0200114C RID: 4428
	[Serializable]
	public class ModLanterned : IModification
	{
		// Token: 0x0600AFBB RID: 44987 RVA: 0x0040BE67 File Offset: 0x0040A067
		public ModLanterned()
		{
		}

		// Token: 0x0600AFBC RID: 44988 RVA: 0x0040BE6F File Offset: 0x0040A06F
		public ModLanterned(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFBD RID: 44989 RVA: 0x0040BE78 File Offset: 0x0040A078
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AFBE RID: 44990 RVA: 0x0040BE81 File Offset: 0x0040A081
		public override bool ModificationApplicable(GameObject Object)
		{
			return !Object.HasPart<LightSource>();
		}

		// Token: 0x0600AFBF RID: 44991 RVA: 0x0040BE8C File Offset: 0x0040A08C
		public override void ApplyModification(GameObject Object)
		{
			if (!Object.HasPart<LightSource>())
			{
				Object.AddPart<LightSource>(new LightSource
				{
					Lit = true,
					Radius = this.Tier + 1
				}, true, false);
			}
			base.IncreaseComplexityIfComplex(1, null);
		}

		// Token: 0x0600AFC0 RID: 44992 RVA: 0x0040BECE File Offset: 0x0040A0CE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AFC1 RID: 44993 RVA: 0x0040BEF4 File Offset: 0x0040A0F4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{lanterned|lanterned}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFC2 RID: 44994 RVA: 0x0040BF1E File Offset: 0x0040A11E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModLanterned.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFC3 RID: 44995 RVA: 0x0040BF3E File Offset: 0x0040A13E
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("stars", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFC4 RID: 44996 RVA: 0x0040BF61 File Offset: 0x0040A161
		public static string GetDescription(int Tier)
		{
			return "Lanterned: This item provides light.";
		}
	}
}
