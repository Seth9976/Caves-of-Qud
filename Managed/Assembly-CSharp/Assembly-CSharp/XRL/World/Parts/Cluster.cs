using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E56 RID: 3670
	[Serializable]
	public class Cluster : IPart
	{
		// Token: 0x06009486 RID: 38022 RVA: 0x00357FF0 File Offset: 0x003561F0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009487 RID: 38023 RVA: 0x00358008 File Offset: 0x00356208
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				Cluster.Cells.Clear();
				this.ParentObject.Physics.CurrentCell.GetAdjacentCells(Stat.Roll(this.Radius, null), Cluster.Cells, true, true);
				if (this.VisibleOnly)
				{
					Cluster.Cells.RemoveAll((Cell c) => !this.ParentObject.HasLOSTo(c, true, false, false, true, null));
				}
				if (this.PassableOnly)
				{
					Cluster.Cells.RemoveAll((Cell c) => !c.IsPassable(null, true));
				}
				Cluster.Cells.ShuffleInPlace<Cell>(null);
				int num = Stat.Roll(this.Number, null);
				int num2 = 0;
				while (num2 < num && num2 < Cluster.Cells.Count)
				{
					GameObject gameObject = GameObjectFactory.Factory.CreateObject(this.Blueprint);
					Cluster.Cells[num2].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					num2++;
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D6D RID: 15725
		public string Blueprint = "";

		// Token: 0x04003D6E RID: 15726
		public string Number = "3-6";

		// Token: 0x04003D6F RID: 15727
		public string Radius = "3-4";

		// Token: 0x04003D70 RID: 15728
		public bool VisibleOnly = true;

		// Token: 0x04003D71 RID: 15729
		public bool PassableOnly = true;

		// Token: 0x04003D72 RID: 15730
		[NonSerialized]
		private static List<Cell> Cells = new List<Cell>();
	}
}
