using System;
using XRL.World.Quests;

namespace XRL.World.QuestManagers
{
	// Token: 0x020009BA RID: 2490
	[Serializable]
	public class FrayingFavorites : QuestManager
	{
		// Token: 0x06006B1E RID: 27422 RVA: 0x00227BE8 File Offset: 0x00225DE8
		public override void FinalizeRead(SerializationReader Reader)
		{
			base.FinalizeRead(Reader);
			XRLGame game = The.Game;
			if (game != null && game.HasUnfinishedQuest("Fraying Favorites"))
			{
				game.RequireSystem<FrayingFavoritesSystem>();
			}
		}
	}
}
