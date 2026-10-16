using System;
using System.Collections.Generic;
using System.Text;
using XRL.Messages;
using XRL.World.AI.GoalHandlers;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.AI
{
	// Token: 0x02000B52 RID: 2898
	[Serializable]
	public class GoalHandler
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060074C1 RID: 29889 RVA: 0x002BE9B0 File Offset: 0x002BCBB0
		public GameObject ParentObject
		{
			get
			{
				Brain parentBrain = this.ParentBrain;
				if (parentBrain == null)
				{
					return null;
				}
				return parentBrain.ParentObject;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x002BE9C3 File Offset: 0x002BCBC3
		public Zone CurrentZone
		{
			get
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject == null)
				{
					return null;
				}
				return parentObject.CurrentZone;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060074C3 RID: 29891 RVA: 0x002BE9D6 File Offset: 0x002BCBD6
		public Cell CurrentCell
		{
			get
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject == null)
				{
					return null;
				}
				return parentObject.CurrentCell;
			}
		}

		// Token: 0x060074C4 RID: 29892 RVA: 0x002BE9E9 File Offset: 0x002BCBE9
		public virtual bool CanFight()
		{
			return true;
		}

		// Token: 0x060074C5 RID: 29893 RVA: 0x002BE9EC File Offset: 0x002BCBEC
		public virtual bool IsBusy()
		{
			return true;
		}

		// Token: 0x060074C6 RID: 29894 RVA: 0x002BE9EF File Offset: 0x002BCBEF
		public virtual bool IsNonAggressive()
		{
			return false;
		}

		// Token: 0x060074C7 RID: 29895 RVA: 0x002BE9F2 File Offset: 0x002BCBF2
		public virtual bool IsFleeing()
		{
			return false;
		}

		// Token: 0x060074C8 RID: 29896 RVA: 0x002BE9F5 File Offset: 0x002BCBF5
		public virtual void Failed()
		{
		}

		// Token: 0x060074C9 RID: 29897 RVA: 0x002BE9F7 File Offset: 0x002BCBF7
		public virtual string GetDetails()
		{
			return null;
		}

		// Token: 0x060074CA RID: 29898 RVA: 0x002BE9FC File Offset: 0x002BCBFC
		public virtual string GetDescription()
		{
			GoalHandler.DescBuilder.Length = 0;
			GoalHandler.DescBuilder.Append(base.GetType().Name);
			string details = this.GetDetails();
			if (!string.IsNullOrEmpty(details))
			{
				GoalHandler.DescBuilder.Append(": ").Append(details);
			}
			return GoalHandler.DescBuilder.ToString();
		}

		// Token: 0x060074CB RID: 29899 RVA: 0x002BEA5C File Offset: 0x002BCC5C
		public void FailToParent()
		{
			while (this.ParentBrain.Goals.Count > 0 && this.ParentBrain.Goals.Peek() != this.ParentHandler)
			{
				this.ParentBrain.Goals.Pop();
			}
			if (this.ParentBrain.Goals.Count > 0)
			{
				this.ParentBrain.Goals.Peek().Failed();
			}
		}

		// Token: 0x060074CC RID: 29900 RVA: 0x002BEACF File Offset: 0x002BCCCF
		public virtual void PushGoal(GoalHandler Goal)
		{
			Goal.Push(this.ParentBrain);
		}

		// Token: 0x060074CD RID: 29901 RVA: 0x002BEADD File Offset: 0x002BCCDD
		public virtual void ForcePushGoal(GoalHandler Goal)
		{
			Goal.ParentBrain = this.ParentBrain;
			this.ParentBrain.Goals.Push(this);
			Goal.Create();
		}

		// Token: 0x060074CE RID: 29902 RVA: 0x002BEB02 File Offset: 0x002BCD02
		public virtual void PushChildGoal(GoalHandler Child)
		{
			Child.ParentHandler = this;
			Child.Push(this.ParentBrain);
		}

		// Token: 0x060074CF RID: 29903 RVA: 0x002BEB17 File Offset: 0x002BCD17
		public void PushChildGoal(GoalHandler Child, GoalHandler Parent)
		{
			Child.ParentHandler = Parent;
			Child.Push(this.ParentBrain);
		}

		// Token: 0x060074D0 RID: 29904 RVA: 0x002BEB2C File Offset: 0x002BCD2C
		public virtual void Push(Brain Brain)
		{
			this.ParentBrain = Brain;
			Brain.Goals.Push(this);
			this.Create();
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x002BEB47 File Offset: 0x002BCD47
		public virtual void InsertGoalAfter(GoalHandler Goal)
		{
			Goal.InsertAfter(this, this.ParentBrain);
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x002BEB56 File Offset: 0x002BCD56
		public virtual void InsertGoalAfter(GoalHandler After, GoalHandler Goal)
		{
			Goal.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x002BEB65 File Offset: 0x002BCD65
		public virtual void ForceInsertGoalAfter(GoalHandler After, GoalHandler Goal)
		{
			Goal.ParentBrain = this.ParentBrain;
			this.ParentBrain.Goals.InsertUnder(After, Goal);
			Goal.Create();
		}

		// Token: 0x060074D4 RID: 29908 RVA: 0x002BEB8C File Offset: 0x002BCD8C
		public virtual void InsertChildGoalAfter(GoalHandler After, GoalHandler Child)
		{
			Child.ParentHandler = this;
			Child.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074D5 RID: 29909 RVA: 0x002BEBA2 File Offset: 0x002BCDA2
		public virtual void InsertChildGoalAfter(GoalHandler After, GoalHandler Child, GoalHandler Parent)
		{
			Child.ParentHandler = Parent;
			Child.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074D6 RID: 29910 RVA: 0x002BEBB8 File Offset: 0x002BCDB8
		public virtual void InsertAfter(GoalHandler After, Brain Brain)
		{
			this.ParentBrain = Brain;
			Brain.Goals.InsertUnder(After, this);
			this.Create();
		}

		// Token: 0x060074D7 RID: 29911 RVA: 0x002BEBD5 File Offset: 0x002BCDD5
		public virtual void InsertGoalAfter(string After, GoalHandler Goal)
		{
			Goal.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074D8 RID: 29912 RVA: 0x002BEBE4 File Offset: 0x002BCDE4
		public virtual void ForceInsertGoalAfter(string After, GoalHandler Goal)
		{
			Goal.ParentBrain = this.ParentBrain;
			this.ParentBrain.Goals.InsertUnder(this.ParentBrain.FindGoal(After), Goal);
			Goal.Create();
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x002BEC16 File Offset: 0x002BCE16
		public virtual void InsertChildGoalAfter(string After, GoalHandler Child)
		{
			Child.ParentHandler = this;
			Child.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x002BEC2C File Offset: 0x002BCE2C
		public virtual void InsertChildGoalAfter(string After, GoalHandler Child, GoalHandler Parent)
		{
			Child.ParentHandler = Parent;
			Child.InsertAfter(After, this.ParentBrain);
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x002BEC42 File Offset: 0x002BCE42
		public virtual void InsertAfter(string After, Brain Brain)
		{
			this.ParentBrain = Brain;
			Brain.Goals.InsertUnder(this.ParentBrain.FindGoal(After), this);
			this.Create();
		}

		// Token: 0x060074DC RID: 29916 RVA: 0x002BEC6A File Offset: 0x002BCE6A
		public virtual void InsertGoalAsParent(GoalHandler Goal)
		{
			this.ParentHandler = Goal;
			Goal.InsertAsParent(this, this.ParentBrain);
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x002BEC80 File Offset: 0x002BCE80
		public virtual void InsertGoalAsParent(GoalHandler BecomesChild, GoalHandler Goal)
		{
			Goal.InsertAsParent(BecomesChild, this.ParentBrain);
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x002BEC8F File Offset: 0x002BCE8F
		public virtual void ForceInsertGoalAsParent(GoalHandler BecomesChild, GoalHandler Goal)
		{
			BecomesChild.ParentHandler = Goal;
			Goal.ParentBrain = this.ParentBrain;
			this.ParentBrain.Goals.InsertUnder(BecomesChild, Goal);
			Goal.Create();
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x002BECBD File Offset: 0x002BCEBD
		public virtual void InsertAsParent(GoalHandler BecomesChild, Brain Brain)
		{
			BecomesChild.ParentHandler = this;
			this.ParentBrain = Brain;
			Brain.Goals.InsertUnder(BecomesChild, this);
			this.Create();
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x002BECE1 File Offset: 0x002BCEE1
		public virtual void InsertGoalAsParent(string BecomesChild, GoalHandler Goal)
		{
			Goal.InsertAsParent(BecomesChild, this.ParentBrain);
		}

		// Token: 0x060074E1 RID: 29921 RVA: 0x002BECF0 File Offset: 0x002BCEF0
		public virtual void ForceInsertGoalAsParent(string BecomesChild, GoalHandler Goal)
		{
			GoalHandler goalHandler = this.ParentBrain.FindGoal(BecomesChild);
			if (goalHandler != null)
			{
				goalHandler.ParentHandler = Goal;
			}
			Goal.ParentBrain = this.ParentBrain;
			this.ParentBrain.Goals.InsertUnder(goalHandler, Goal);
			Goal.Create();
		}

		// Token: 0x060074E2 RID: 29922 RVA: 0x002BED3C File Offset: 0x002BCF3C
		public virtual void InsertAsParent(string BecomesChild, Brain Brain)
		{
			GoalHandler goalHandler = this.ParentBrain.FindGoal(BecomesChild);
			if (goalHandler != null)
			{
				goalHandler.ParentHandler = this;
			}
			this.ParentBrain = Brain;
			Brain.Goals.InsertUnder(goalHandler, this);
			this.Create();
		}

		// Token: 0x060074E3 RID: 29923 RVA: 0x002BED7B File Offset: 0x002BCF7B
		public void Pop()
		{
			if (this.ParentBrain.Goals.Count > 0)
			{
				this.ParentBrain.Goals.Pop();
			}
		}

		// Token: 0x060074E4 RID: 29924 RVA: 0x002BEDA1 File Offset: 0x002BCFA1
		public virtual void Create()
		{
		}

		// Token: 0x060074E5 RID: 29925 RVA: 0x002BEDA3 File Offset: 0x002BCFA3
		public virtual void TakeAction()
		{
		}

		// Token: 0x060074E6 RID: 29926 RVA: 0x002BEDA5 File Offset: 0x002BCFA5
		public virtual bool Finished()
		{
			return true;
		}

		// Token: 0x060074E7 RID: 29927 RVA: 0x002BEDA8 File Offset: 0x002BCFA8
		public void Think(string Hrm)
		{
			this.ParentBrain.Think(Hrm);
		}

		// Token: 0x060074E8 RID: 29928 RVA: 0x002BEDB8 File Offset: 0x002BCFB8
		public bool MoveTowards(Cell targetCell, bool Global = false, bool MoveAwayIfAt = true)
		{
			this.Think("I'm going to move towards my target.");
			if (targetCell.ParentZone.IsWorldMap())
			{
				this.Think("Target's on the world map, can't follow!");
				return false;
			}
			if (MoveAwayIfAt && targetCell == this.ParentObject.CurrentCell)
			{
				Cell randomLocalAdjacentCell = this.ParentObject.CurrentCell.GetRandomLocalAdjacentCell();
				if (randomLocalAdjacentCell != null)
				{
					this.PushChildGoal(new Step(this.ParentObject.CurrentCell.GetDirectionFromCell(randomLocalAdjacentCell, false), false, false, false, false, null, false));
					return true;
				}
			}
			FindPath findPath = new FindPath(this.ParentObject.CurrentZone.ZoneID, this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, targetCell.ParentZone.ZoneID, targetCell.X, targetCell.Y, Global, false, this.ParentObject, false, false, false, false, 100);
			if (findPath.Usable)
			{
				using (List<string>.Enumerator enumerator = findPath.Directions.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.PushChildGoal(new Step(text, false, false, false, false, null, false));
						return true;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060074E9 RID: 29929 RVA: 0x002BEEF4 File Offset: 0x002BD0F4
		public static void AddPlayerMessage(string Message, string Color = null, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x060074EA RID: 29930 RVA: 0x002BEEFE File Offset: 0x002BD0FE
		public static void AddPlayerMessage(string Message, char Color, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x060074EB RID: 29931 RVA: 0x002BEF08 File Offset: 0x002BD108
		public static bool Visible(GameObject obj)
		{
			return obj != null && obj.IsVisible();
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x002BEF15 File Offset: 0x002BD115
		public bool Visible()
		{
			return GoalHandler.Visible(this.ParentObject);
		}

		// Token: 0x0400358E RID: 13710
		public int Age;

		// Token: 0x0400358F RID: 13711
		public Brain ParentBrain;

		// Token: 0x04003590 RID: 13712
		public GoalHandler ParentHandler;

		// Token: 0x04003591 RID: 13713
		[NonSerialized]
		private static StringBuilder DescBuilder = new StringBuilder();
	}
}
