using System;

namespace XRL.World.Units
{
	// Token: 0x020009B0 RID: 2480
	[Serializable]
	public class GameObjectPlaceholderUnit : GameObjectUnit
	{
		// Token: 0x06006AEE RID: 27374 RVA: 0x0022721C File Offset: 0x0022541C
		public override bool CanInscribe()
		{
			return false;
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0022721F File Offset: 0x0022541F
		public override string GetDescription(bool Inscription = false)
		{
			return this.Description;
		}

		// Token: 0x040030F8 RID: 12536
		public string Description;
	}
}
