using System;
using System.Linq;
using XRL.Collections;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200126B RID: 4715
	[Serializable]
	public class Fan : IPoweredPart
	{
		// Token: 0x0600BAE2 RID: 47842 RVA: 0x00443C27 File Offset: 0x00441E27
		public Fan()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600BAE3 RID: 47843 RVA: 0x00443C54 File Offset: 0x00441E54
		public override bool Render(RenderEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 0 && num < 15)
				{
					E.Tile = null;
					E.RenderString = Directions.GetArrowForDirection(this.Direction);
					E.ApplyColors("&C", Fan.ICON_COLOR_PRIORITY);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BAE4 RID: 47844 RVA: 0x00443CC6 File Offset: 0x00441EC6
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BAE5 RID: 47845 RVA: 0x00443CCC File Offset: 0x00441ECC
		public override void TurnTick(long TimeTick, int Amount)
		{
			Cell cell = this.ParentObject.GetCurrentCell();
			if (cell == null)
			{
				return;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			using (ScopeDisposedList<Cell> fromPool = ScopeDisposedList<Cell>.GetFromPool())
			{
				for (int i = 0; i < this.BlowRange; i++)
				{
					cell = cell.GetCellFromDirection(this.Direction, true);
					if (cell == null || cell.IsSolid())
					{
						break;
					}
					fromPool.Add(cell);
				}
				fromPool.Reverse<Cell>();
				foreach (Cell cell2 in fromPool)
				{
					foreach (GameObject gameObject in from o in cell2.GetObjectsViaEventList()
						where o.IsCombatObject() || (o.Physics != null && !o.Physics.Solid && (o.Physics.Takeable || o.HasPart<Gas>()))
						where o.CanBeInvoluntarilyMoved() && o.PhaseMatches(this.ParentObject)
						select o)
					{
						int num = (gameObject.HasStat("Strength") ? (gameObject.GetStatValue("Strength", 0) * 20) : (gameObject.Weight * 3 / 2));
						int num2 = gameObject.DistanceTo(this.ParentObject);
						int num3 = this.BlowStrength * Math.Max(10 - num2, 0);
						int num4 = Math.Min(this.BlowRange, (int)Math.Ceiling((double)((float)(num3 - num) / 100f)));
						num4 = Math.Max(num4, 0);
						if (num4 == 0 && num3 > 100 && gameObject.HasStat("Strength") && gameObject.IsCombatObject())
						{
							IComponent<GameObject>.XDidYToZ(gameObject, "resist", "being blown back by", this.ParentObject, null, null, null, null, gameObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						for (int j = 0; j < num4; j++)
						{
							Cell currentCell = gameObject.CurrentCell;
							Cell cell3 = ((currentCell != null) ? currentCell.GetCellFromDirection(this.Direction, true) : null);
							if (cell3 != null && cell3.IsSolidFor(gameObject, false))
							{
								break;
							}
							if (j % 3 == 0 || !gameObject.HasPart<Gas>())
							{
								gameObject.Smoke();
							}
							gameObject.Move(this.Direction, true, false, false, false, true, true, null, null, false, new int?(0), null, null, false, false, null, null, 0);
							if (gameObject.IsPlayer() && !gameObject.CurrentZone.IsActive())
							{
								gameObject.CurrentZone.SetActive();
							}
							if (gameObject.CurrentCell == currentCell)
							{
								break;
							}
							if (gameObject.IsVisible())
							{
								XRLCore.Core.RenderBase(true, false);
							}
						}
						if (num4 > 0 && gameObject.IsCombatObject())
						{
							if (num4 == 1)
							{
								IComponent<GameObject>.XDidYToZ(gameObject, "are", "blown back by", this.ParentObject, null, null, null, null, null, gameObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
							else if (num4 == 2)
							{
								IComponent<GameObject>.XDidYToZ(gameObject, "are", "blown forcefully back by", this.ParentObject, null, null, null, null, null, gameObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
							else if (num4 >= 3)
							{
								IComponent<GameObject>.XDidYToZ(gameObject, "are", "blown into the air by", this.ParentObject, null, "!", null, null, null, gameObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x04004AFC RID: 19196
		public static readonly int ICON_COLOR_PRIORITY = 200;

		// Token: 0x04004AFD RID: 19197
		public string Direction = "S";

		// Token: 0x04004AFE RID: 19198
		public int BlowStrength = 500;

		// Token: 0x04004AFF RID: 19199
		public int BlowRange = 16;
	}
}
