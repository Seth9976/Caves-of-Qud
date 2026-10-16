using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F6F RID: 3951
	[Serializable]
	public class Fireflies : IPart
	{
		// Token: 0x06009D31 RID: 40241 RVA: 0x0038ED5C File Offset: 0x0038CF5C
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009D32 RID: 40242 RVA: 0x0038ED5F File Offset: 0x0038CF5F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06009D33 RID: 40243 RVA: 0x0038ED78 File Offset: 0x0038CF78
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (base.IsNight() && Stat.RandomCosmetic(1, 3000) == 1)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && currentCell.ParentZone != null && currentCell.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone && !currentCell.IsLit())
				{
					XRLCore.ParticleManager.Add("firefly", (float)currentCell.X, (float)currentCell.Y, 0f, 0f, 60, 0f, 0f, 0L);
				}
			}
			return base.HandleEvent(E);
		}
	}
}
