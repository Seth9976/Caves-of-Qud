using System;
using XRL.Language;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x02000EEB RID: 3819
	[HasWishCommand]
	[Serializable]
	public class Pariah : IPart
	{
		// Token: 0x060098A3 RID: 39075 RVA: 0x00376BDC File Offset: 0x00374DDC
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098A4 RID: 39076 RVA: 0x00376BDF File Offset: 0x00374DDF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x060098A5 RID: 39077 RVA: 0x00376BF5 File Offset: 0x00374DF5
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			Pariah.MakePariah(this.ParentObject, true, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060098A6 RID: 39078 RVA: 0x00376C0C File Offset: 0x00374E0C
		public static void MakePariah(GameObject go, bool AlterName = true, bool IsUnique = false)
		{
			if (go.Brain != null)
			{
				if (go.Brain.Allegiance.ContainsKey("Pariahs"))
				{
					return;
				}
				go.Brain.Allegiance.Clear();
				go.Brain.Allegiance.Add("Pariahs", 100);
				go.Brain.Allegiance.Hostile = false;
			}
			if (IsUnique)
			{
				HeroMaker.MakeHero(go, null, null, -1, "Hero");
				AlterName = false;
			}
			if (AlterName)
			{
				if (go.HasProperName)
				{
					go.RequirePart<SocialRoles>(false).RequireRole(Grammar.MakeTitleCase(go.GetBlueprint(true).DisplayName()) + " Pariah");
					return;
				}
				go.RequirePart<SocialRoles>(false).RequireRole("pariah to =pronouns.possessive= people");
			}
		}

		// Token: 0x060098A7 RID: 39079 RVA: 0x00376CCC File Offset: 0x00374ECC
		[WishCommand("pariah", null)]
		public static void Wish(string Blueprint)
		{
			WishResult wishResult = WishSearcher.SearchForBlueprint(Blueprint);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			Pariah.MakePariah(gameObject, true, false);
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}
	}
}
