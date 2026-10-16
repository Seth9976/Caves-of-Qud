using System;

namespace XRL.World.Parts
{
	// Token: 0x02001297 RID: 4759
	[Serializable]
	public class EnableSwitch : IPart
	{
		// Token: 0x0600BC31 RID: 48177 RVA: 0x0044CF28 File Offset: 0x0044B128
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SwitchActivated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC32 RID: 48178 RVA: 0x0044CF40 File Offset: 0x0044B140
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SwitchActivated")
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentZone.GetCell(this.x, this.y).GetObjectsWithPart("Switch"))
				{
					gameObject.FireEvent("Enable");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B6D RID: 19309
		public int x = 10;

		// Token: 0x04004B6E RID: 19310
		public int y = 15;
	}
}
