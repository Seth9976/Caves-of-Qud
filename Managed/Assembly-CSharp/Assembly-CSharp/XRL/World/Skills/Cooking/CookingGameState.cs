using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using XRL.Core;
using XRL.World.Parts.Mutation;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BBD RID: 3005
	[GameStateSingleton]
	[Serializable]
	public class CookingGameState : IGameStateSingleton, IComposite
	{
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06007766 RID: 30566 RVA: 0x002CF09D File Offset: 0x002CD29D
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x002CF0A0 File Offset: 0x002CD2A0
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteComposite<CookingRecipe>(this.knownRecipies);
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x002CF0AE File Offset: 0x002CD2AE
		public void Read(SerializationReader Reader)
		{
			this.knownRecipies = Reader.ReadCompositeList<CookingRecipe>();
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x002CF0BC File Offset: 0x002CD2BC
		public static List<GameObject> GetInventorySnapshot()
		{
			if (CookingGameState.inventorySnapshot == null)
			{
				bool carnivorous = The.Player.HasPart<Carnivorous>();
				CookingGameState.inventorySnapshot = The.Player.GetInventoryDirectAndEquipment((GameObject go) => !carnivorous || (!go.HasTag("Plant") && !go.HasTag("Fungus")));
			}
			return CookingGameState.inventorySnapshot;
		}

		// Token: 0x0600776A RID: 30570 RVA: 0x002CF106 File Offset: 0x002CD306
		public static void ResetInventorySnapshot()
		{
			CookingGameState.inventorySnapshot = null;
			if (CookingGameState.ingredientQuantity != null)
			{
				CookingGameState.ingredientQuantity.Clear();
			}
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x002CF120 File Offset: 0x002CD320
		public static int GetIngredientQuantity(ICookingRecipeComponent component)
		{
			if (CookingGameState.ingredientQuantity == null)
			{
				CookingGameState.ingredientQuantity = new Dictionary<string, int>();
			}
			string ingredientId = component.getIngredientId();
			if (!CookingGameState.ingredientQuantity.ContainsKey(ingredientId))
			{
				CookingGameState.ingredientQuantity[ingredientId] = component.PlayerHolding();
			}
			return CookingGameState.ingredientQuantity[ingredientId];
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x002CF170 File Offset: 0x002CD370
		public static bool KnowsRecipe(CookingRecipe newRecipe)
		{
			return CookingGameState.instance.knownRecipies.Any<CookingRecipe>((CookingRecipe i) => newRecipe != null && i != null && newRecipe.GetDisplayName() == i.GetDisplayName());
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x002CF1A8 File Offset: 0x002CD3A8
		public static bool KnowsRecipe(string ClassName)
		{
			return !ClassName.IsNullOrEmpty() && CookingGameState.instance.knownRecipies.Any<CookingRecipe>((CookingRecipe i) => i.GetType().Name == ClassName);
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x002CF1EC File Offset: 0x002CD3EC
		public static CookingRecipe LearnRecipe(CookingRecipe newRecipe, GameObject Chef = null)
		{
			JournalAPI.AddRecipeNote(newRecipe, Chef, true, false, null);
			return newRecipe;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600776F RID: 30575 RVA: 0x002CF1FA File Offset: 0x002CD3FA
		public static CookingGameState instance
		{
			get
			{
				return XRLCore.Core.Game.GetObjectGameState("CookingGameState") as CookingGameState;
			}
		}

		// Token: 0x06007770 RID: 30576 RVA: 0x002CF215 File Offset: 0x002CD415
		public void Initialize()
		{
		}

		// Token: 0x040036D6 RID: 14038
		public static List<GameObject> inventorySnapshot;

		// Token: 0x040036D7 RID: 14039
		public static Dictionary<string, int> ingredientQuantity;

		// Token: 0x040036D8 RID: 14040
		public List<CookingRecipe> knownRecipies = new List<CookingRecipe>();
	}
}
