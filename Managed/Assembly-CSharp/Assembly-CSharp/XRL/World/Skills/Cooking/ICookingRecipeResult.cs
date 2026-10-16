using System;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BBA RID: 3002
	public interface ICookingRecipeResult : IComposite
	{
		// Token: 0x06007759 RID: 30553
		string GetCampfireDescription();

		// Token: 0x0600775A RID: 30554
		string apply(GameObject eater);
	}
}
