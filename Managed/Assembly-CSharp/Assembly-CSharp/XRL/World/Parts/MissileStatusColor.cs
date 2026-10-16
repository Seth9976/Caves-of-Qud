using System;

namespace XRL.World.Parts
{
	// Token: 0x02001112 RID: 4370
	[Serializable]
	public class MissileStatusColor : IPart
	{
		// Token: 0x0600AD24 RID: 44324 RVA: 0x003F8A4D File Offset: 0x003F6C4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<GetMissileStatusColorEvent>.ID && !string.IsNullOrEmpty(this.Color));
		}

		// Token: 0x0600AD25 RID: 44325 RVA: 0x003F8A73 File Offset: 0x003F6C73
		public override bool HandleEvent(GetMissileStatusColorEvent E)
		{
			if (!string.IsNullOrEmpty(this.Color) && this.ParentObject.Understood())
			{
				E.Color = this.Color;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004779 RID: 18297
		public string Color;
	}
}
