using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012A2 RID: 4770
	[Serializable]
	public class WalltrapCrabs : IPart
	{
		// Token: 0x0600BC7C RID: 48252 RVA: 0x0044E7ED File Offset: 0x0044C9ED
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC7D RID: 48253 RVA: 0x0044E7F0 File Offset: 0x0044C9F0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC7E RID: 48254 RVA: 0x0044E808 File Offset: 0x0044CA08
		public void SpawnCrab(string D)
		{
			Cell cell = this.ParentObject.CurrentCell;
			if (cell == null)
			{
				return;
			}
			for (int i = 0; i < this.SpawnDistance; i++)
			{
				cell = cell.GetCellFromDirection(D, true);
				if (cell == null || cell.IsSolid(true))
				{
					break;
				}
				if (cell.IsEmpty())
				{
					GameObject gameObject = GameObject.Create("Chute Crab");
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					return;
				}
			}
		}

		// Token: 0x0600BC7F RID: 48255 RVA: 0x0044E880 File Offset: 0x0044CA80
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger")
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
								this.SpawnCrab(text);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B8A RID: 19338
		public int SpawnDistance = 3;

		// Token: 0x04004B8B RID: 19339
		public bool Seen;
	}
}
