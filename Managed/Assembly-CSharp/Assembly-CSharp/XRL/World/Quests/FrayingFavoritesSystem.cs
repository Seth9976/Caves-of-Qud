using System;
using XRL.Wish;
using XRL.World.ZoneBuilders;

namespace XRL.World.Quests
{
	// Token: 0x02000B23 RID: 2851
	[HasWishCommand]
	[Serializable]
	public class FrayingFavoritesSystem : IQuestSystem
	{
		// Token: 0x0600729A RID: 29338 RVA: 0x002B65BE File Offset: 0x002B47BE
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(AfterConversationEvent.ID, 0, false);
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x002B65CD File Offset: 0x002B47CD
		public override bool HandleEvent(AfterConversationEvent E)
		{
			this.CheckChatted();
			return base.HandleEvent(E);
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x002B65DC File Offset: 0x002B47DC
		public void CheckChatted()
		{
			if (base.Player.HasProperty("DoyobaChat") && base.Player.HasProperty("DadogomChat") && base.Player.HasProperty("GyamyoChat") && base.Player.HasProperty("YonaChat"))
			{
				The.Game.FinishQuestStep("Fraying Favorites", "Speak with the Watchers", -1, true, null);
			}
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x002B6649 File Offset: 0x002B4849
		public override void Start()
		{
			this.CheckChatted();
			if (The.Game.GetIntGameState("FinishedFrayingFavorites", 0) == 1)
			{
				The.Game.FinishQuest("Fraying Favorites");
			}
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x002B6673 File Offset: 0x002B4873
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Lebah");
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x002B6680 File Offset: 0x002B4880
		[WishCommand("fraying", null)]
		public static void Wish(string Biomech)
		{
			if (Biomech.EqualsNoCase("nacham"))
			{
				ChildrenOfTheTombQuestHandler.ChooseNacham();
			}
			else if (Biomech.EqualsNoCase("vaam") || Biomech.EqualsNoCase("va'am"))
			{
				ChildrenOfTheTombQuestHandler.ChooseVaam();
			}
			else if (Biomech.EqualsNoCase("dagasha"))
			{
				ChildrenOfTheTombQuestHandler.ChooseDagasha();
			}
			else if (Biomech.EqualsNoCase("kah"))
			{
				ChildrenOfTheTombQuestHandler.ChooseKah();
			}
			else
			{
				if (!Biomech.EqualsNoCase("all"))
				{
					return;
				}
				ChildrenOfTheTombQuestHandler.ChooseNacham();
				ChildrenOfTheTombQuestHandler.ChooseVaam();
				ChildrenOfTheTombQuestHandler.ChooseDagasha();
				ChildrenOfTheTombQuestHandler.ChooseKah();
			}
			ChildrenOfTheTombQuestHandler.FinishFrayingFavorites();
			The.Game.CompleteQuest("Fraying Favorites");
		}
	}
}
