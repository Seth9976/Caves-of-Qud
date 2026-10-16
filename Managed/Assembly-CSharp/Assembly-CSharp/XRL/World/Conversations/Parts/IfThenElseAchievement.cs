using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C03 RID: 3075
	public class IfThenElseAchievement : IConversationPart
	{
		// Token: 0x06007B90 RID: 31632 RVA: 0x002E66C7 File Offset: 0x002E48C7
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x002E66E0 File Offset: 0x002E48E0
		public override bool HandleEvent(EnteredElementEvent E)
		{
			XRLGame game = The.Game;
			if (game.HasIntGameState("ElseingComplete") && !game.HasDelimitedGameState("TauElse", ',', "Dead") && !game.HasDelimitedGameState("TauCompanion", ',', "Dead"))
			{
				Achievement.TAU_THEN.Unlock();
			}
			else
			{
				Achievement.TAU_ELSE.Unlock();
			}
			return base.HandleEvent(E);
		}
	}
}
