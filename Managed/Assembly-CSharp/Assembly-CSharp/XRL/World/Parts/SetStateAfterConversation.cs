using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EFE RID: 3838
	[Serializable]
	public class SetStateAfterConversation : IPart
	{
		// Token: 0x0600993A RID: 39226 RVA: 0x00379DE8 File Offset: 0x00377FE8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterConversationEvent.ID;
		}

		// Token: 0x0600993B RID: 39227 RVA: 0x00379DFE File Offset: 0x00377FFE
		public override bool HandleEvent(AfterConversationEvent E)
		{
			if (!this.Activated)
			{
				if (!this.State.IsNullOrEmpty())
				{
					The.Game.SetBooleanGameState(this.State, true);
				}
				this.Activated = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003FD5 RID: 16341
		public string State;

		// Token: 0x04003FD6 RID: 16342
		[NonSerialized]
		private bool Activated;
	}
}
