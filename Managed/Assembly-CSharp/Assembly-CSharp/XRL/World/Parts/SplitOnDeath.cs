using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F06 RID: 3846
	[Serializable]
	public class SplitOnDeath : IPart
	{
		// Token: 0x06009965 RID: 39269 RVA: 0x0037AB9C File Offset: 0x00378D9C
		public override bool SameAs(IPart Part)
		{
			SplitOnDeath splitOnDeath = Part as SplitOnDeath;
			return splitOnDeath.Chance == this.Chance && !(splitOnDeath.Blueprint != this.Blueprint) && !(splitOnDeath.Amount != this.Amount) && !(splitOnDeath.Message != this.Message) && base.SameAs(Part);
		}

		// Token: 0x06009966 RID: 39270 RVA: 0x0037AC06 File Offset: 0x00378E06
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06009967 RID: 39271 RVA: 0x0037AC1C File Offset: 0x00378E1C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.Amount.IsNullOrEmpty() && !this.Blueprint.IsNullOrEmpty() && !this.ParentObject.IsNowhere() && this.Chance.in100())
			{
				this.Split();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009968 RID: 39272 RVA: 0x0037AC6C File Offset: 0x00378E6C
		public void Split()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			int num = this.Amount.RollCached();
			if (num <= 0)
			{
				return;
			}
			if (!this.Message.IsNullOrEmpty())
			{
				base.EmitMessage(GameText.VariableReplace(this.Message, this.ParentObject, null, false), IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, false, false, null, null);
			}
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = GameObject.Create(this.Blueprint);
				gameObject.TakeBaseAllegiance(this.ParentObject);
				gameObject.CopyTarget(this.ParentObject);
				gameObject.CopyLeader(this.ParentObject);
				if (i == 0)
				{
					currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				else
				{
					currentCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x04003FEC RID: 16364
		public int Chance = 100;

		// Token: 0x04003FED RID: 16365
		public string Blueprint;

		// Token: 0x04003FEE RID: 16366
		public string Amount;

		// Token: 0x04003FEF RID: 16367
		public string Message;
	}
}
