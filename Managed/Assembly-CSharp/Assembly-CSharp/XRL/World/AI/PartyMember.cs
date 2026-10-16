using System;

namespace XRL.World.AI
{
	// Token: 0x02000B6C RID: 2924
	public struct PartyMember
	{
		// Token: 0x06007543 RID: 30019 RVA: 0x002BF7BC File Offset: 0x002BD9BC
		public PartyMember(GameObjectReference Reference, int Flags)
		{
			this.Reference = Reference;
			this.Flags = Flags;
		}

		// Token: 0x0400359A RID: 13722
		public GameObjectReference Reference;

		// Token: 0x0400359B RID: 13723
		public int Flags;
	}
}
