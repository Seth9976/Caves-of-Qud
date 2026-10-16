using System;
using System.Collections.Generic;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB5 RID: 2997
	public interface ICookingRecipeComponent : IComposite
	{
		// Token: 0x06007729 RID: 30505
		bool doesPlayerHaveEnough();

		// Token: 0x0600772A RID: 30506
		string createPlayerDoesNotHaveEnoughMessage();

		// Token: 0x0600772B RID: 30507
		void use(List<GameObject> used);

		// Token: 0x0600772C RID: 30508
		string getDisplayName();

		// Token: 0x0600772D RID: 30509
		bool HasPlants();

		// Token: 0x0600772E RID: 30510
		bool HasFungi();

		// Token: 0x0600772F RID: 30511
		int PlayerHolding();

		// Token: 0x06007730 RID: 30512
		string getIngredientId();
	}
}
