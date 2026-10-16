using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F02 RID: 3842
	[Serializable]
	public class SpawnOnDeath : IPart
	{
		// Token: 0x06009950 RID: 39248 RVA: 0x0037A6A6 File Offset: 0x003788A6
		public SpawnOnDeath()
		{
		}

		// Token: 0x06009951 RID: 39249 RVA: 0x0037A6CB File Offset: 0x003788CB
		public SpawnOnDeath(string blueprint)
		{
			this.Blueprint = blueprint;
		}

		// Token: 0x06009952 RID: 39250 RVA: 0x0037A6F7 File Offset: 0x003788F7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009953 RID: 39251 RVA: 0x0037A70C File Offset: 0x0037890C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval" && this.ParentObject.Physics.CurrentCell != null)
			{
				this.ParentObject.Physics.CurrentCell.AddObject(this.Blueprint, null, null, null, null);
				if (this.DoPuff)
				{
					this.ParentObject.DustPuff(this.PuffColor, 15);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003FDF RID: 16351
		public string Blueprint = "Bloatfly";

		// Token: 0x04003FE0 RID: 16352
		public bool DoPuff = true;

		// Token: 0x04003FE1 RID: 16353
		public string PuffColor = "&K";
	}
}
