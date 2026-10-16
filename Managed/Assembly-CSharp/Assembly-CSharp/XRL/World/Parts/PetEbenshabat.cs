using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Skills.Cooking;

namespace XRL.World.Parts
{
	// Token: 0x020011A2 RID: 4514
	[Serializable]
	public class PetEbenshabat : IPart
	{
		// Token: 0x0600B386 RID: 45958 RVA: 0x00417E28 File Offset: 0x00416028
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, PooledEvent<AfterLevelGainedEvent>.ID, 0, false);
		}

		// Token: 0x0600B387 RID: 45959 RVA: 0x00417E3C File Offset: 0x0041603C
		public override bool HandleEvent(AfterLevelGainedEvent E)
		{
			if (E.Actor.IsPlayer() && E.Actor == this.ParentObject.PartyLeader && E.Actor.InSameZone(this.ParentObject) && Stat.Random(1, 100) <= 40)
			{
				string text = "Ingredients" + E.Actor.GetTier().ToString();
				List<string> list = new List<string> { "Starapple Preserves" };
				int num = ((Stat.Random(1, 100) <= 50) ? 3 : 2);
				int num2 = 0;
				while (num2 < 100 && list.Count < num)
				{
					string text2 = CookingRecipe.RollOvenSafeIngredient(text);
					if (!list.Contains(text2))
					{
						list.Add(text2);
					}
					num2++;
				}
				CookingRecipe cookingRecipe = CookingGameState.LearnRecipe(CookingRecipe.FromIngredients(list, null, this.ParentObject.DisplayNameOnlyDirectAndStripped), null);
				base.DidXToY("teach", E.Actor, cookingRecipe.GetDisplayName(), "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}
	}
}
