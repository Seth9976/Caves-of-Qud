using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012A1 RID: 4769
	[Serializable]
	public class WalltrapClockworkBeetles : IPart
	{
		// Token: 0x0600BC77 RID: 48247 RVA: 0x0044E684 File Offset: 0x0044C884
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC78 RID: 48248 RVA: 0x0044E687 File Offset: 0x0044C887
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC79 RID: 48249 RVA: 0x0044E69C File Offset: 0x0044C89C
		public void SpawnBeetle(string D)
		{
			Cell cell = this.ParentObject.CurrentCell;
			if (cell == null)
			{
				return;
			}
			int i = 0;
			while (i < this.SpawnDistance)
			{
				cell = cell.GetCellFromDirection(D, true);
				if (cell == null || cell.IsSolid(true))
				{
					break;
				}
				if (cell.IsEmpty())
				{
					GameObject gameObject = GameObject.Create("ClockworkBeetle");
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					if (this.SpawnLimit > 0)
					{
						this.SpawnLimit--;
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600BC7A RID: 48250 RVA: 0x0044E728 File Offset: 0x0044C928
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger" && this.SpawnLimit != 0)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					if (!this.Seen && currentCell.IsVisible())
					{
						this.Seen = true;
					}
					if (this.Seen)
					{
						foreach (string text in Directions.CardinalDirectionList)
						{
							Cell cellFromDirection = currentCell.GetCellFromDirection(text, true);
							if (cellFromDirection != null && !cellFromDirection.IsSolid(true))
							{
								this.SpawnBeetle(text);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B87 RID: 19335
		public int SpawnDistance = 3;

		// Token: 0x04004B88 RID: 19336
		public int SpawnLimit = 30;

		// Token: 0x04004B89 RID: 19337
		public bool Seen;
	}
}
