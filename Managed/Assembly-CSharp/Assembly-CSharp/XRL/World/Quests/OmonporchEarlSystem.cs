using System;

namespace XRL.World.Quests
{
	// Token: 0x02000B26 RID: 2854
	[Serializable]
	public class OmonporchEarlSystem : IQuestSystem
	{
		// Token: 0x060072B3 RID: 29363 RVA: 0x002B6AE4 File Offset: 0x002B4CE4
		public override void Start()
		{
			if (The.Game.GetIntGameState("AsphodelSlain", 0) == 1)
			{
				The.Game.FinishQuestStep("The Earl of Omonporch", "Travel to Omonporch", -1, true, null);
				The.Game.FinishQuestStep("The Earl of Omonporch", "Secure the Spindle", -1, true, null);
			}
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x002B6B34 File Offset: 0x002B4D34
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Otho");
		}
	}
}
