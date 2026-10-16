using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7F RID: 2943
	[Serializable]
	public class DisposeOfCorpse : GoalHandler
	{
		// Token: 0x060075CA RID: 30154 RVA: 0x002C2FFB File Offset: 0x002C11FB
		public DisposeOfCorpse()
		{
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x002C3003 File Offset: 0x002C1203
		public DisposeOfCorpse(GameObject Corpse, GameObject Container)
			: this()
		{
			this.Corpse = Corpse;
			this.Container = Container;
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x002C3019 File Offset: 0x002C1219
		public override bool CanFight()
		{
			return true;
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x002C301C File Offset: 0x002C121C
		public override bool Finished()
		{
			return this.Done;
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x002C3024 File Offset: 0x002C1224
		public override void TakeAction()
		{
			if (!GameObject.Validate(ref this.Corpse) || !GameObject.Validate(ref this.Container) || !base.ParentObject.InSameZone(this.Container))
			{
				base.FailToParent();
				return;
			}
			if (this.Corpse.InInventory == base.ParentObject)
			{
				if (base.ParentObject.InSameOrAdjacentCellTo(this.Container))
				{
					if (!this.Container.FireEvent(Event.New("PerformTake", "Object", this.Corpse, "PutBy", base.ParentObject, "Context", "DisposeOfCorpse")))
					{
						base.ParentObject.FireEvent(Event.New("PerformDrop", "Object", this.Corpse));
					}
					this.Done = true;
					return;
				}
				int num = this.GoToContainerTries + 1;
				this.GoToContainerTries = num;
				if (num <= 10)
				{
					this.PushChildGoal(new MoveTo(this.Container, false, false, 1, false, false, false, -1, -1, 95));
					return;
				}
				base.ParentObject.FireEvent(Event.New("PerformDrop", "Object", this.Corpse));
				this.Done = true;
				return;
			}
			else
			{
				Cell currentCell = this.Corpse.CurrentCell;
				if (currentCell == null || currentCell.ParentZone != base.ParentObject.CurrentZone)
				{
					base.FailToParent();
					return;
				}
				if (base.ParentObject.InSameOrAdjacentCellTo(this.Corpse))
				{
					if (!base.ParentObject.ReceiveObject(this.Corpse, false, "DisposeOfCorpse"))
					{
						base.FailToParent();
						return;
					}
				}
				else
				{
					int num = this.GoToCorpseTries + 1;
					this.GoToCorpseTries = num;
					if (num <= 10)
					{
						this.PushChildGoal(new MoveTo(this.Corpse, false, false, 1, false, false, false, -1, -1, 95));
						return;
					}
					this.Done = true;
				}
				return;
			}
		}

		// Token: 0x040035ED RID: 13805
		public GameObject Corpse;

		// Token: 0x040035EE RID: 13806
		public GameObject Container;

		// Token: 0x040035EF RID: 13807
		public Cell Destination;

		// Token: 0x040035F0 RID: 13808
		public bool Done;

		// Token: 0x040035F1 RID: 13809
		public int GoToContainerTries;

		// Token: 0x040035F2 RID: 13810
		public int GoToCorpseTries;
	}
}
