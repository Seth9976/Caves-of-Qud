using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7B RID: 2939
	[Serializable]
	public class ChangeEquipment : GoalHandler
	{
		// Token: 0x060075B1 RID: 30129 RVA: 0x002C2D44 File Offset: 0x002C0F44
		public ChangeEquipment()
		{
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x002C2D4C File Offset: 0x002C0F4C
		public ChangeEquipment(string ToUnequip)
			: this()
		{
			this.ToUnequip = ToUnequip;
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x002C2D5B File Offset: 0x002C0F5B
		public ChangeEquipment(GameObject ToUnequip)
			: this(ToUnequip.ID)
		{
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x002C2D69 File Offset: 0x002C0F69
		public override bool Finished()
		{
			return !this.ParentBrain.ParentObject.HasEquippedItem((GameObject obj) => obj.IDMatch(this.ToUnequip));
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x002C2D8C File Offset: 0x002C0F8C
		public override void TakeAction()
		{
			GameObject gameObject = this.ParentBrain.ParentObject.FindEquippedItem((GameObject obj) => obj.IDMatch(this.ToUnequip));
			if (gameObject == null)
			{
				base.FailToParent();
				return;
			}
			if (!gameObject.TryUnequip(false, false))
			{
				base.FailToParent();
				return;
			}
			this.ParentBrain.PerformReequip(false, false, false, gameObject);
		}

		// Token: 0x040035E5 RID: 13797
		public string ToUnequip;
	}
}
