using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E6F RID: 3695
	[Serializable]
	public class DrowsingUrchin : IPart
	{
		// Token: 0x0600954B RID: 38219 RVA: 0x00360033 File Offset: 0x0035E233
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("BeforeDie");
			Registrar.Register("PuffPlease");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600954C RID: 38220 RVA: 0x00360060 File Offset: 0x0035E260
		public void Puff()
		{
			this.Puffed = 40;
			foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
			{
				cell.AddObject(GameObject.Create("SleepGas80"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			this.ParentObject.DustPuff("&y", 15);
		}

		// Token: 0x0600954D RID: 38221 RVA: 0x003600F0 File Offset: 0x0035E2F0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDie")
			{
				this.Puff();
			}
			else if (E.ID == "PuffPlease")
			{
				this.Puff();
			}
			else if (E.ID == "EndTurn")
			{
				if (this.Puffed > 0)
				{
					this.Puffed--;
				}
				else if (this.ParentObject.CurrentCell != null)
				{
					foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
					{
						foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Brain"))
						{
							if (this.ParentObject.IsHostileTowards(gameObject))
							{
								this.Puff();
								return true;
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DD3 RID: 15827
		public int Puffed;
	}
}
