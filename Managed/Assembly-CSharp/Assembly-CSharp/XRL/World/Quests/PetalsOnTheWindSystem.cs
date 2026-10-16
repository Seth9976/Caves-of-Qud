using System;

namespace XRL.World.Quests
{
	// Token: 0x02000B28 RID: 2856
	[Serializable]
	public class PetalsOnTheWindSystem : IQuestSystem
	{
		// Token: 0x060072BD RID: 29373 RVA: 0x002B6C59 File Offset: 0x002B4E59
		public override void Start()
		{
			ZoneManager.instance.GetZone("JoppaWorld").BroadcastEvent("BeyLahReveal");
			if (The.Game.HasQuest("Find Eskhind"))
			{
				The.Game.CompleteQuest("Petals on the Wind");
			}
		}

		// Token: 0x060072BE RID: 29374 RVA: 0x002B6C95 File Offset: 0x002B4E95
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Lulihart");
		}
	}
}
