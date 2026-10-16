using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E6B RID: 3691
	[Serializable]
	public class CryptFerretBehavior : IPart
	{
		// Token: 0x0600952A RID: 38186 RVA: 0x0035F5D0 File Offset: 0x0035D7D0
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600952B RID: 38187 RVA: 0x0035F5D3 File Offset: 0x0035D7D3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600952C RID: 38188 RVA: 0x0035F5D6 File Offset: 0x0035D7D6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("AICantAttackRange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600952D RID: 38189 RVA: 0x0035F5F8 File Offset: 0x0035D7F8
		public void scurry()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				if (!currentCell.IsVisible())
				{
					return;
				}
				if (this.hiddenCells.Count == 0)
				{
					this.hiddenCells.Clear();
					this.hiddenCells.AddRange(from c in currentCell.ParentZone.GetCells()
						where !c.IsVisible() && c.IsPassable(null, true)
						select c);
					this.hiddenCells.Sort((Cell a, Cell b) => a.DistanceTo(this.ParentObject).CompareTo(b.DistanceTo(this.ParentObject)));
				}
				List<Cell> list = null;
				int num = 4;
				foreach (Cell cell in this.hiddenCells)
				{
					if (!cell.IsVisible())
					{
						if (this.ParentObject.canPathTo(cell, false))
						{
							this.ParentObject.Brain.Goals.Clear();
							this.ParentObject.Brain.FleeTo(cell, 1);
							this.ParentObject.Brain.MoveTo(cell, false);
							if (list != null)
							{
								foreach (Cell cell2 in list)
								{
									this.hiddenCells.Remove(cell2);
								}
							}
							return;
						}
						if (list == null)
						{
							list = new List<Cell>();
						}
						list.Add(cell);
					}
					num--;
					if (num < 0)
					{
						break;
					}
				}
				this.hiddenCells.Clear();
			}
		}

		// Token: 0x0600952E RID: 38190 RVA: 0x0035F79C File Offset: 0x0035D99C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AICantAttackRange")
			{
				if (this.Fleeing)
				{
					GameObject target = this.ParentObject.Target;
					if (target != null && (target.IsPlayer() || target.IsPlayerLed()))
					{
						this.scurry();
						return false;
					}
					if (this.ParentObject.CurrentCell.FastFloodVisibilityFirstBlueprint("Reliquary", this.ParentObject) != null)
					{
						this.behaviorState = "looting";
					}
				}
				return true;
			}
			if (E.ID == "BeginTakeAction")
			{
				if (this.behaviorState == "fleeing")
				{
					this.behaviorState = "hunting";
					this.ParentObject.UseEnergy(1000);
					this.ParentObject.TeleportSwirl(null, "&C", false, null, 'ù', true);
					this.ParentObject.TeleportTo(this.ParentObject.CurrentZone.GetEmptyCells(null, null).GetRandomElement<Cell>(null), new int?(0), true, false, false, false, "disappear", "appear");
					this.ParentObject.TeleportSwirl(null, "&C", false, null, 'ù', false);
				}
				else if (this.behaviorState == "looting")
				{
					GameObject gameObject = this.ParentObject.CurrentCell.FastFloodVisibilityFirstBlueprint("Reliquary", this.ParentObject);
					if (gameObject != null)
					{
						if (this.ParentObject.DistanceTo(gameObject) <= 1)
						{
							GameObject gameObject2 = gameObject.Inventory.Objects.RemoveRandomElement<GameObject>(null);
							if (gameObject2 != null)
							{
								gameObject.GetPart<AICryptHelpBroadcaster>().BroadcastHelp();
								base.DidXToY("filch", gameObject2, null, "!", null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
								this.ParentObject.TakeObject(gameObject2, false, false, new int?(0), null, null);
							}
							this.behaviorState = "fleeing";
						}
						else
						{
							if (this.ParentObject.Brain.Goals.Items.Any<GoalHandler>((GoalHandler g) => g.GetType() == typeof(Step) || g.GetType() == typeof(MoveTo)))
							{
								return true;
							}
							this.ParentObject.Brain.PushGoal(new NoFightGoal());
							this.ParentObject.Brain.MoveTo(gameObject, false);
						}
					}
				}
				else if (this.behaviorState == "hunting")
				{
					int num = 200;
					if (this.ParentObject.CurrentZone != null)
					{
						if (this.ParentObject.CurrentZone.Z == 14)
						{
							num = The.Game.RequireSystem<CatacombsAnchorSystem>().Timer;
						}
						if (this.ParentObject.CurrentZone.Z == 13)
						{
							num = The.Game.RequireSystem<CryptOfLandlordsAnchorSystem>().Timer;
						}
						if (this.ParentObject.CurrentZone.Z == 12)
						{
							num = The.Game.RequireSystem<CryptOfWarriorsAnchorSystem>().Timer;
						}
						if (this.ParentObject.CurrentZone.Z == 11)
						{
							num = The.Game.RequireSystem<CryptOfPriestsAnchorSystem>().Timer;
						}
					}
					if (num <= 20)
					{
						if (this.Fleeing)
						{
							this.ParentObject.Brain.Goals.Clear();
							this.Fleeing = false;
						}
					}
					else if (!this.Fleeing)
					{
						this.ParentObject.Brain.Goals.Clear();
						this.Fleeing = true;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DCE RID: 15822
		public bool Fleeing;

		// Token: 0x04003DCF RID: 15823
		public string behaviorState = "hunting";

		// Token: 0x04003DD0 RID: 15824
		public Location2D startingLocation;

		// Token: 0x04003DD1 RID: 15825
		private List<Cell> hiddenCells = new List<Cell>();
	}
}
