using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.Wish
{
	/// <summary>
	///     Community member helado created this wish which helps a lot with debugging.
	/// </summary>
	// Token: 0x02000567 RID: 1383
	[HasWishCommand]
	public static class MutationBGoneWishHandler
	{
		// Token: 0x06003C34 RID: 15412 RVA: 0x001265A8 File Offset: 0x001247A8
		[WishCommand(null, null, Command = "mutationbgone")]
		public static bool MutationBGone()
		{
			Mutations mutations = MutationBGoneWishHandler.GetMutations();
			List<BaseMutation> mutationList = mutations.MutationList;
			if (mutationList.Count > 0)
			{
				int num = Popup.PickOption("Choose a mutation for me to gobble up!", null, "", "Sounds/UI/ui_notification", mutationList.ConvertAll<string>((BaseMutation mutation) => mutation.GetDisplayName(true)).ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num != -1)
				{
					MutationBGoneWishHandler.RemoveMutation(mutations, mutationList[num]);
				}
			}
			else
			{
				Popup.Show("Huh? Get some mutations first if you want me to eat them!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		/// <summary>
		///  Given a specific mutation class name remove that mutation.
		/// </summary>
		// Token: 0x06003C35 RID: 15413 RVA: 0x00126648 File Offset: 0x00124848
		[WishCommand(null, null, Command = "mutationbgone")]
		public static bool MutationBGone(string argument)
		{
			Mutations part = The.Player.GetPart<Mutations>();
			BaseMutation mutation = part.GetMutation(argument);
			if (mutation == null)
			{
				Popup.Show("Didn't find that one. Try again?", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				MutationBGoneWishHandler.RemoveMutation(part, mutation);
			}
			return true;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0012668A File Offset: 0x0012488A
		public static Mutations GetMutations()
		{
			return The.Player.GetPart<Mutations>();
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x00126696 File Offset: 0x00124896
		public static void RemoveMutation(Mutations mutations, BaseMutation mutation)
		{
			mutations.RemoveMutation(mutation, true);
			Popup.Show("Om nom nom! " + mutation.GetDisplayName(true) + " is gone! {{w|*belch*}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}
	}
}
