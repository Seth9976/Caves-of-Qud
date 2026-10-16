using System;
using Qud.API;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFF RID: 2815
	public static class ChildrenOfTheTombQuestHandler
	{
		// Token: 0x06007085 RID: 28805 RVA: 0x002838FC File Offset: 0x00281AFC
		public static void ChooseNacham()
		{
			The.Game.SetIntGameState("ChoseNacham", 1);
			Achievement.STAT_GAVE_REPULSIVE_DEVICE_NACHAM.Increment(1);
			ChildrenOfTheTombQuestHandler.AddAccomplishment("Nacham");
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x00283923 File Offset: 0x00281B23
		public static void ChooseVaam()
		{
			The.Game.SetIntGameState("ChoseVaam", 1);
			Achievement.STAT_GAVE_REPULSIVE_DEVICE_VAAM.Increment(1);
			ChildrenOfTheTombQuestHandler.AddAccomplishment("Vaam");
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x0028394A File Offset: 0x00281B4A
		public static void ChooseDagasha()
		{
			The.Game.SetIntGameState("ChoseDagasha", 1);
			Achievement.STAT_GAVE_REPULSIVE_DEVICE_DAGASHA.Increment(1);
			ChildrenOfTheTombQuestHandler.AddAccomplishment("Dagasha");
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x00283971 File Offset: 0x00281B71
		public static void ChooseKah()
		{
			The.Game.SetIntGameState("ChoseKah", 1);
			Achievement.STAT_GAVE_REPULSIVE_DEVICE_KAH.Increment(1);
			ChildrenOfTheTombQuestHandler.AddAccomplishment("Kah");
		}

		// Token: 0x06007089 RID: 28809 RVA: 0x00283998 File Offset: 0x00281B98
		public static void FinishFrayingFavorites()
		{
			The.Game.SetIntGameState("FinishedFrayingFavorites", 1);
		}

		// Token: 0x0600708A RID: 28810 RVA: 0x002839AC File Offset: 0x00281BAC
		public static void AddAccomplishment(string EntityName)
		{
			JournalAPI.AddAccomplishment("You restored functionality to one of the four strange entities in the Tomb.", "Remember the kindness of =name=, who absolved =player.possessive= treacherous brother of sin and steered him to salvation!", "In a moss-caked tomb under the light of the Spindle, =name= cemented their relationship with enigmatic beings by marrying the entity " + EntityName + ".", null, "general", MuralCategory.DoesSomethingHumble, MuralWeight.Medium, null, -1L, true);
		}

		// Token: 0x04003412 RID: 13330
		public const int NACHAM = 1;

		// Token: 0x04003413 RID: 13331
		public const int VAAM = 2;

		// Token: 0x04003414 RID: 13332
		public const int DAGASHA = 4;

		// Token: 0x04003415 RID: 13333
		public const int KAH = 8;
	}
}
