using System;
using System.Collections.Generic;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BBE RID: 3006
	[Obsolete]
	[Serializable]
	public class CookingGamestate : IGamestateSingleton, IGamestatePostload
	{
		// Token: 0x06007772 RID: 30578 RVA: 0x002CF22C File Offset: 0x002CD42C
		public void OnGamestatePostload(XRLGame game, SerializationReader reader)
		{
			CookingGameState cookingGameState = new CookingGameState();
			cookingGameState.knownRecipies = this.knownRecipies;
			game.ObjectGameState["CookingGameState"] = cookingGameState;
			game.ObjectGameState.Remove("cookingGamestate");
		}

		// Token: 0x040036D9 RID: 14041
		public List<CookingRecipe> knownRecipies = new List<CookingRecipe>();
	}
}
