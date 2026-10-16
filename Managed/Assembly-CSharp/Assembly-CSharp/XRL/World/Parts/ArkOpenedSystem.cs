using System;
using XRL.World.Conversations.Parts;

namespace XRL.World.Parts
{
	// Token: 0x02000FF8 RID: 4088
	[Serializable]
	public class ArkOpenedSystem : IGameSystem
	{
		// Token: 0x0600A2E8 RID: 41704 RVA: 0x003B881C File Offset: 0x003B6A1C
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(SingletonEvent<EndTurnEvent>.ID, 0, false);
		}

		// Token: 0x0600A2E9 RID: 41705 RVA: 0x003B882B File Offset: 0x003B6A2B
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!EndGame.IsAnyEnding)
			{
				EndGame.CheckMarooned();
			}
			return base.HandleEvent(E);
		}
	}
}
