using System;
using System.Collections.Generic;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200121D RID: 4637
	[Serializable]
	public class DanceRitualOpponent : IPart
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600B830 RID: 47152 RVA: 0x0042F798 File Offset: 0x0042D998
		private PlayerDanceRitual Ritual
		{
			get
			{
				return IComponent<GameObject>.ThePlayer.GetPart<PlayerDanceRitual>();
			}
		}

		// Token: 0x0600B831 RID: 47153 RVA: 0x0042F7A4 File Offset: 0x0042D9A4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B832 RID: 47154 RVA: 0x0042F7A7 File Offset: 0x0042D9A7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeAITakingActionEvent>.ID;
		}

		// Token: 0x0600B833 RID: 47155 RVA: 0x0042F7C0 File Offset: 0x0042D9C0
		public override bool HandleEvent(BeforeAITakingActionEvent E)
		{
			MessageQueue.AddPlayerMessage("&KDebug: Angor taking a turn...", null, true);
			this.Ritual.TurnsLeft--;
			if (this.Ritual.CurrentState != "#MakeChoice" && this.Ritual.TurnsLeft <= 0)
			{
				if (this.Ritual.CurrentState == "FreeTurn")
				{
					this.Ritual.CurrentState = "#MakeChoice";
					this.Ritual.CurrentLeader = "Angor";
				}
				else
				{
					MessageQueue.AddPlayerMessage("&KDebug: Dance Phase Ends Positive:" + this.Ritual.StepsPassed.ToString() + " Negative:" + this.Ritual.StepsFailed.ToString(), null, true);
					this.ParentObject.Render.ColorString = "&G";
					this.ParentObject.Render.TileColor = "&G";
					this.Ritual.PlayerMovementLog.Clear();
					this.Ritual.OpponentMovementLog.Clear();
					this.Ritual.TurnsLeft = 2;
					this.Ritual.StepsPassed = 0;
					this.Ritual.StepsFailed = 0;
					this.Ritual.CurrentState = "FreeTurn";
				}
			}
			if (this.Ritual.CurrentState == "#MakeChoice" && this.Ritual.CurrentLeader == "Angor")
			{
				int num = Stat.Random(0, this.Ritual.Rituals.Count - 1);
				RitualTypeEntry ritualTypeEntry = this.Ritual.Rituals[num];
				this.ParentObject.Render.ColorString = "&" + ritualTypeEntry.Color;
				this.ParentObject.Render.TileColor = "&" + ritualTypeEntry.Color;
				this.Ritual.CurrentState = ritualTypeEntry.Name;
				MessageQueue.AddPlayerMessage("&KDebug: Angor chooses " + ritualTypeEntry.Name, null, true);
				this.Ritual.PlayerMovementLog.Clear();
				this.Ritual.OpponentMovementLog.Clear();
				this.Ritual.TurnsLeft = 5;
			}
			if (this.Ritual.CurrentState == "Mimic" || this.Ritual.CurrentState == "Mirror")
			{
				if (this.Ritual.CurrentState == "Mirror")
				{
					bool flag = true;
					if (this.Ritual.PlayerMovementLog.Count != this.Ritual.OpponentMovementLog.Count)
					{
						flag = false;
						this.Ritual.PlayerMovementLog.Clear();
						this.Ritual.OpponentMovementLog.Clear();
					}
					else
					{
						int num2 = 0;
						while (num2 < this.Ritual.OpponentMovementLog.Count && num2 < this.Ritual.PlayerMovementLog.Count)
						{
							if (this.Ritual.OpponentMovementLog[num2] != Directions.GetOppositeDirection(this.Ritual.PlayerMovementLog[num2]))
							{
								flag = false;
								break;
							}
							num2++;
						}
					}
					if (!flag)
					{
						this.Ritual.FailStep("Movement log didn't match");
					}
					else
					{
						this.Ritual.PassStep("Movement log matches");
					}
				}
				if (this.Ritual.CurrentState == "Mimic")
				{
					bool flag2 = true;
					if (this.Ritual.PlayerMovementLog.Count != this.Ritual.OpponentMovementLog.Count)
					{
						flag2 = false;
						this.Ritual.PlayerMovementLog.Clear();
						this.Ritual.OpponentMovementLog.Clear();
					}
					else
					{
						int num3 = 0;
						while (num3 < this.Ritual.OpponentMovementLog.Count && num3 < this.Ritual.PlayerMovementLog.Count)
						{
							if (this.Ritual.OpponentMovementLog[num3] != this.Ritual.PlayerMovementLog[num3])
							{
								flag2 = false;
								break;
							}
							num3++;
						}
					}
					if (!flag2)
					{
						this.Ritual.FailStep("Movement log didn't match");
					}
					else
					{
						this.Ritual.PassStep("Movement log matches");
					}
				}
				List<Cell> localEmptyAdjacentCells = this.ParentObject.Physics.CurrentCell.GetLocalEmptyAdjacentCells();
				if (localEmptyAdjacentCells.Count > 0)
				{
					this.ParentObject.Move(localEmptyAdjacentCells.GetRandomElement<Cell>(null).GetDirectionFromCell(this.ParentObject.CurrentCell, false), false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B834 RID: 47156 RVA: 0x0042FC74 File Offset: 0x0042DE74
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterMoved");
			Registrar.Register("EndAction");
			Registrar.Register("BeforeDie");
			Registrar.Register("PreventSmartUse");
			Registrar.Register("EndTurn");
			IComponent<GameObject>.AddPlayerMessage("Debug: Angor Began The Dance", 'K', true);
			this.ParentObject.Energy.BaseValue = 1001;
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B835 RID: 47157 RVA: 0x0042FCE4 File Offset: 0x0042DEE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndAction")
			{
				this.ParentObject.Energy.BaseValue = 999;
				IComponent<GameObject>.ThePlayer.Energy.BaseValue = 1001;
				return true;
			}
			if (E.ID == "AfterMoved")
			{
				this.Ritual.ExecuteMove("Opponent", E.GetStringParameter("Direction", null));
				return true;
			}
			if (E.ID == "BeforeDie")
			{
				The.Player.GetPart<PlayerDanceRitual>().FireEvent(Event.New("DanceOpponentDied", 0, 0, 0));
				return true;
			}
			if (E.ID == "PreventSmartUse")
			{
				return false;
			}
			if (E.ID == "CanHaveConversation")
			{
				if (!E.IsSilent())
				{
					Popup.ShowFail(this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.Is + " busy dancing!", true, true, true);
				}
				return false;
			}
			if (E.ID == "EndTurn")
			{
				this.Ritual.EndTurn("Actor");
				MessageQueue.AddPlayerMessage("&KDebug: Dance party opponent turn tick " + this.n.ToString(), null, true);
				this.n++;
				return true;
			}
			return true;
		}

		// Token: 0x04004A1F RID: 18975
		private int n;
	}
}
