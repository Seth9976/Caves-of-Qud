using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EFF RID: 3839
	[Serializable]
	public class SetStateOnSeen : IPart
	{
		// Token: 0x0600993D RID: 39229 RVA: 0x00379E3C File Offset: 0x0037803C
		public override bool Render(RenderEvent E)
		{
			if (!this.Activated)
			{
				if (!this.State.IsNullOrEmpty())
				{
					The.Game.SetBooleanGameState(this.State, true);
				}
				this.Activated = true;
			}
			return true;
		}

		// Token: 0x04003FD7 RID: 16343
		public string State;

		// Token: 0x04003FD8 RID: 16344
		[NonSerialized]
		private bool Activated;
	}
}
