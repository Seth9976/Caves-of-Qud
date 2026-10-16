using System;

namespace XRL.World.Parts
{
	// Token: 0x0200122D RID: 4653
	[Serializable]
	public class SetBooleanStateOnRead : IPart
	{
		// Token: 0x0600B8CD RID: 47309 RVA: 0x0043652D File Offset: 0x0043472D
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<AfterReadBookEvent>.ID;
		}

		// Token: 0x0600B8CE RID: 47310 RVA: 0x00436537 File Offset: 0x00434737
		public override bool HandleEvent(AfterReadBookEvent E)
		{
			if (!this.State.IsNullOrEmpty())
			{
				The.Game.SetBooleanGameState(this.State, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004A65 RID: 19045
		public string State;
	}
}
