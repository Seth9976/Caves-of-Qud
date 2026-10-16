using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E65 RID: 3685
	[Serializable]
	public class AddlingUrchin : IPart
	{
		// Token: 0x060094F3 RID: 38131 RVA: 0x0035DE16 File Offset: 0x0035C016
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("BeforeDie");
			Registrar.Register("PuffPlease");
			base.Register(Object, Registrar);
		}

		// Token: 0x060094F4 RID: 38132 RVA: 0x0035DE44 File Offset: 0x0035C044
		public void Puff()
		{
			this.Puffed = 40;
			foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
			{
				cell.AddObject(GameObject.Create("ConfusionGas80"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			this.ParentObject.DustPuff("&y", 15);
		}

		// Token: 0x060094F5 RID: 38133 RVA: 0x0035DED4 File Offset: 0x0035C0D4
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

		// Token: 0x04003DB4 RID: 15796
		public int Puffed;
	}
}
