using System;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F00 RID: 3840
	[Serializable]
	public class SlowDangerousMovement : IPart
	{
		// Token: 0x0600993F RID: 39231 RVA: 0x00379E74 File Offset: 0x00378074
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EnteredCellEvent.ID || ID == GetAdjacentNavigationWeightEvent.ID || (ID == GetShortDescriptionEvent.ID && !this.PreparedDirection.IsNullOrEmpty());
		}

		// Token: 0x06009940 RID: 39232 RVA: 0x00379EB4 File Offset: 0x003780B4
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (!this.PreparedDirection.IsNullOrEmpty() && this.Active && (!this.ParentObject.IsMobile() || !this.ParentObject.CanChangeMovementMode(null, false, false, false, false)))
			{
				this.PreparedDirection = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x00379F04 File Offset: 0x00378104
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.PreparedDirection = null;
			foreach (Cell cell in E.Cell.GetLocalAdjacentCells())
			{
				cell.FlushNavigationCache();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x00379F68 File Offset: 0x00378168
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(5);
			return base.HandleEvent(E);
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x00379F78 File Offset: 0x00378178
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.PreparedDirection.IsNullOrEmpty() && this.Active)
			{
				E.Infix.Compound(this.ParentObject.Itis, "\n").Append(" preparing to move ").Append(Directions.GetIndicativeDirection(this.PreparedDirection));
				Consumer consumer = (this.LinkedToConsumer ? this.ParentObject.GetPart<Consumer>() : null);
				if (consumer != null && consumer.Chance > 0)
				{
					E.Infix.Append(", ");
					if (consumer.Chance < 100)
					{
						E.Infix.Append("potentially ");
					}
					E.Infix.Append("consuming anything in ").Append(this.ParentObject.its).Append(" path");
				}
				E.Infix.Append('.');
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x0037A065 File Offset: 0x00378265
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			Registrar.Register("CurioInit");
			Registrar.Register("BeginMove");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009945 RID: 39237 RVA: 0x0037A090 File Offset: 0x00378290
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				if (this.Active && E.GetStringParameter("Type", null).IsNullOrEmpty())
				{
					return this.TryToMoveTo(E.GetParameter("DestinationCell") as Cell);
				}
			}
			else if (E.ID == "VillageInit" || E.ID == "CurioInit")
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009946 RID: 39238 RVA: 0x0037A114 File Offset: 0x00378314
		public bool TryToMoveTo(Cell C)
		{
			string directionFromCell = this.ParentObject.CurrentCell.GetDirectionFromCell(C, false);
			if (!Directions.IsActualDirection(directionFromCell))
			{
				return true;
			}
			if (directionFromCell == this.PreparedDirection)
			{
				return true;
			}
			if (this.ParentObject.OnWorldMap())
			{
				return true;
			}
			Consumer consumer = (this.LinkedToConsumer ? this.ParentObject.GetPart<Consumer>() : null);
			if (consumer != null && this.ParentObject.Target != null && !consumer.AnythingToConsume(C))
			{
				return true;
			}
			this.PreparedDirection = directionFromCell;
			string text = (this.ParentObject.IsPlayer() ? this.PrepMessageSelf : this.PrepMessageOther);
			if (!text.IsNullOrEmpty() && base.Visible())
			{
				if (text.Contains("=dir="))
				{
					text = text.Replace("=dir=", Directions.GetExpandedDirection(directionFromCell));
				}
				if (text.Contains("=dirward="))
				{
					text = text.Replace("=dirward=", Directions.GetIndicativeDirection(directionFromCell));
				}
				IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(text, this.ParentObject, null, false), null, true);
			}
			this.ParentObject.UseEnergy(1000, "Movement", null, null, false);
			this.CauseFleeing(C, true);
			foreach (Cell cell in C.GetDirectionAndAdjacentCells(directionFromCell, false, true))
			{
				this.CauseFleeing(cell, false);
			}
			if (!this.ParentObject.IsPlayer())
			{
				this.ParentObject.SetIntProperty("AIKeepMoving", 1, false);
			}
			return false;
		}

		// Token: 0x06009947 RID: 39239 RVA: 0x0037A2AC File Offset: 0x003784AC
		public void CauseFleeing(Cell C, bool Immediate = false)
		{
			foreach (GameObject gameObject in C.Objects)
			{
				this.CauseFleeing(gameObject, C, Immediate);
			}
		}

		// Token: 0x06009948 RID: 39240 RVA: 0x0037A304 File Offset: 0x00378504
		public void CauseFleeing(GameObject obj, Cell C = null, bool Immediate = false)
		{
			if (((obj != null) ? obj.Brain : null) != null && obj.IsCreature && obj.IsMobile() && obj.Stat("Intelligence", 0) > 6)
			{
				if (obj.IsPlayer())
				{
					if (AutoAct.IsActive(false))
					{
						AutoAct.Interrupt("you are " + (Immediate ? "in" : "near") + " the path of " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false), null, this.ParentObject, true);
						return;
					}
				}
				else
				{
					Consumer consumer = (this.LinkedToConsumer ? this.ParentObject.GetPart<Consumer>() : null);
					if ((consumer == null || consumer.WouldConsume(obj)) && (Immediate || !obj.IsEngagedInMelee()))
					{
						obj.Brain.PushGoal(new FleeLocation(C ?? obj.CurrentCell, 1, false, false));
					}
				}
			}
		}

		// Token: 0x06009949 RID: 39241 RVA: 0x0037A3FF File Offset: 0x003785FF
		public override bool FinalRender(RenderEvent E)
		{
			if (!this.PreparedDirection.IsNullOrEmpty() && this.Active)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x0037A420 File Offset: 0x00378620
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (!this.PreparedDirection.IsNullOrEmpty() && base.Visible() && this.Active)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Cell cellFromDirection = currentCell.GetCellFromDirection(this.PreparedDirection, true);
					if (cellFromDirection != null && cellFromDirection.IsVisible())
					{
						buffer.Goto(cellFromDirection.X, cellFromDirection.Y);
						if (XRLCore.CurrentFrame >= 20)
						{
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].TileForeground = The.Color.DarkRed;
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].Detail = The.Color.DarkRed;
						}
						else
						{
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].TileForeground = The.Color.Red;
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].Detail = The.Color.Red;
						}
						if (XRLCore.CurrentFrame % 20 >= 10)
						{
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].TileBackground = The.Color.Red;
							buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].SetBackground('R');
						}
						buffer.Buffer[cellFromDirection.X, cellFromDirection.Y].SetForeground('r');
					}
				}
			}
			base.OnPaint(buffer);
		}

		// Token: 0x04003FD9 RID: 16345
		public string PreparedDirection;

		// Token: 0x04003FDA RID: 16346
		public bool LinkedToConsumer;

		// Token: 0x04003FDB RID: 16347
		public string PrepMessageSelf;

		// Token: 0x04003FDC RID: 16348
		public string PrepMessageOther;

		// Token: 0x04003FDD RID: 16349
		public bool Active = true;
	}
}
