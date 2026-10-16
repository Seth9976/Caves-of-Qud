using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B79 RID: 2937
	public class AICommandList
	{
		// Token: 0x060075A0 RID: 30112 RVA: 0x002C20FF File Offset: 0x002C02FF
		public AICommandList(string Command, int Priority)
		{
			this.Priority = Priority;
			this.Command = Command;
		}

		// Token: 0x060075A1 RID: 30113 RVA: 0x002C2115 File Offset: 0x002C0315
		public AICommandList(string Command, int Priority, GameObject Object = null, bool Inv = false, bool Self = false, GameObject TargetOverride = null, Cell TargetCellOverride = null)
			: this(Command, Priority)
		{
			this.Object = Object;
			this.Inv = Inv;
			this.Self = Self;
			this.TargetOverride = TargetOverride;
			this.TargetCellOverride = TargetCellOverride;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060075A2 RID: 30114 RVA: 0x002C2148 File Offset: 0x002C0348
		public string DebugName
		{
			get
			{
				string text = this.Command;
				if (this.Self)
				{
					text += "/self";
				}
				if (this.TargetOverride != null)
				{
					text = text + "/target:" + this.TargetOverride.DebugName;
				}
				if (this.TargetCellOverride != null)
				{
					text = text + "/targetcell:" + this.TargetCellOverride.DebugName;
				}
				if (this.Priority != 0)
				{
					text = text + "(" + this.Priority.ToString() + ")";
				}
				if (this.Inv)
				{
					text = "inv:" + text;
				}
				return text;
			}
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x002C21E7 File Offset: 0x002C03E7
		private bool ProcessCommand(GameObject Handler, GameObject Owner, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0)
		{
			return CommandEvent.Send(Owner, this.Command, Target, TargetCell, StandoffDistance, false, false, Handler);
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x002C2200 File Offset: 0x002C0400
		public bool HandleCommand(GameObject Owner, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0)
		{
			if (this.Command == "MetaCommandMoveAway")
			{
				string text;
				if ((text = ((TargetCell != null) ? TargetCell.GetDirectionFromCell(Owner.CurrentCell, false) : null)) == null)
				{
					if (Target == null)
					{
						text = null;
					}
					else
					{
						Cell currentCell = Target.CurrentCell;
						text = ((currentCell != null) ? currentCell.GetDirectionFromCell(Owner.CurrentCell, false) : null);
					}
				}
				string text2 = text;
				if (!text2.IsNullOrEmpty())
				{
					if (text2 == "." || text2 == "?")
					{
						text2 = Owner.CurrentCell.GetDirectionFromCell(Owner.CurrentCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(Owner, true)), true);
					}
					if (!text2.IsNullOrEmpty() && Owner.Move(text2, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
					{
						return true;
					}
				}
			}
			else
			{
				GameObject gameObject = this.Object ?? Owner;
				if (this.Inv)
				{
					GameObject gameObject2 = gameObject;
					GameObject owner = Owner;
					GameObject gameObject3 = gameObject;
					string command = this.Command;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					int num = 0;
					int num2 = 0;
					GameObject gameObject4 = this.TargetOverride ?? (this.Self ? Owner : Target);
					Cell cell = this.TargetCellOverride ?? TargetCell;
					if (InventoryActionEvent.Check(gameObject2, owner, gameObject3, command, flag, flag2, flag3, flag4, flag5, num, num2, StandoffDistance, gameObject4, cell, null, null))
					{
						return true;
					}
				}
				else if (this.ProcessCommand(gameObject, Owner, this.TargetOverride ?? (this.Self ? Owner : Target), this.TargetCellOverride ?? TargetCell, StandoffDistance))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x002C23A8 File Offset: 0x002C05A8
		public static bool HandleCommandList(List<AICommandList> List, GameObject Owner, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0)
		{
			if (List != null && List.Count > 0)
			{
				int num = Stat.Random(0, List.Count - 1);
				if (List[num].HandleCommand(Owner, Target, TargetCell, StandoffDistance))
				{
					return true;
				}
				if (List.Count > 1)
				{
					int num2 = Stat.Random(0, List.Count - 1);
					if (num2 != num && List[num2].HandleCommand(Owner, Target, TargetCell, StandoffDistance))
					{
						return true;
					}
					if (List.Count > 2)
					{
						int num3 = Stat.Random(0, List.Count - 1);
						if (num3 != num2 && num3 != num && List[num3].HandleCommand(Owner, Target, TargetCell, StandoffDistance))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040035D7 RID: 13783
		public const string META_COMMAND_MOVE_AWAY = "MetaCommandMoveAway";

		// Token: 0x040035D8 RID: 13784
		public int Priority;

		// Token: 0x040035D9 RID: 13785
		public string Command;

		// Token: 0x040035DA RID: 13786
		public GameObject Object;

		// Token: 0x040035DB RID: 13787
		public bool Inv;

		// Token: 0x040035DC RID: 13788
		public bool Self;

		// Token: 0x040035DD RID: 13789
		public GameObject TargetOverride;

		// Token: 0x040035DE RID: 13790
		public Cell TargetCellOverride;
	}
}
