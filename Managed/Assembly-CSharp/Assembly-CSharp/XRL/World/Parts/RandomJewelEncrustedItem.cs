using System;
using Qud.API;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001086 RID: 4230
	[Serializable]
	public class RandomJewelEncrustedItem : IPart
	{
		// Token: 0x0600A853 RID: 43091 RVA: 0x003D90A3 File Offset: 0x003D72A3
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600A854 RID: 43092 RVA: 0x003D90A6 File Offset: 0x003D72A6
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600A855 RID: 43093 RVA: 0x003D90A9 File Offset: 0x003D72A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID || ID == EnteredCellEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A856 RID: 43094 RVA: 0x003D90CF File Offset: 0x003D72CF
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			this.GenerateJewelEncrustedItem(E.Context, E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A857 RID: 43095 RVA: 0x003D90E5 File Offset: 0x003D72E5
		public override bool HandleEvent(TakenEvent E)
		{
			this.GenerateJewelEncrustedItem(E.Context, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A858 RID: 43096 RVA: 0x003D90FB File Offset: 0x003D72FB
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.GenerateJewelEncrustedItem(null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A859 RID: 43097 RVA: 0x003D910C File Offset: 0x003D730C
		private void GenerateJewelEncrustedItem(string Context = null, IObjectCreationEvent E = null)
		{
			GameObject gameObject = null;
			bool flag = false;
			do
			{
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				gameObject = GameObject.Create(EncountersAPI.GetARandomDescendantOf("Item"), 0, 0, null, null, null, Context, null);
				if (!gameObject.HasTag("NoSwapInAtGenerate") && gameObject.GetPropertyOrTag("Mods", "").Contains("CommonMods") && gameObject.HasTag("Tier") && gameObject.GetTier() < 7)
				{
					flag = true;
				}
			}
			while (!flag);
			if (!gameObject.HasPart<ModJewelEncrusted>())
			{
				ItemModding.ApplyModification(gameObject, "ModJewelEncrusted", 1, true, null, false);
			}
			if (E != null)
			{
				E.ReplacementObject = gameObject;
				return;
			}
			this.ParentObject.ReplaceWith(gameObject);
		}
	}
}
