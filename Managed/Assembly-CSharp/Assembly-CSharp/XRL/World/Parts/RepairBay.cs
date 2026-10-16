using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000FB4 RID: 4020
	[Serializable]
	public class RepairBay : IPoweredPart
	{
		// Token: 0x06009FCF RID: 40911 RVA: 0x003A18A6 File Offset: 0x0039FAA6
		public RepairBay()
		{
			this.ChargeUse = 1000;
			this.WorksOnInventory = true;
		}

		// Token: 0x06009FD0 RID: 40912 RVA: 0x003A18CB File Offset: 0x0039FACB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009FD1 RID: 40913 RVA: 0x003A18D0 File Offset: 0x0039FAD0
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ProcessItems();
			}
		}

		// Token: 0x06009FD2 RID: 40914 RVA: 0x003A190C File Offset: 0x0039FB0C
		public bool ProcessItems()
		{
			bool flag = this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.ProcessItem), true);
			if (!flag)
			{
				base.ConsumeCharge(null, null);
			}
			return flag;
		}

		// Token: 0x06009FD3 RID: 40915 RVA: 0x003A1948 File Offset: 0x0039FB48
		private bool ProcessItem(GameObject obj)
		{
			if (!IsRepairableEvent.Check(this.ParentObject, obj, this.ParentObject, null, new int?(this.MaxRepairTier)))
			{
				return true;
			}
			if (!Tinkering_Repair.IsRepairableBy(obj, this.ParentObject, null, null, new int?(this.MaxRepairTier)))
			{
				return true;
			}
			obj.SplitFromStack();
			if (!string.IsNullOrEmpty(this.Verb))
			{
				base.DidXToY(this.Verb, obj, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
			}
			obj.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
			RepairedEvent.Send(this.ParentObject, obj, null, null, this.MaxRepairTier);
			obj.CheckStack();
			return false;
		}

		// Token: 0x04004270 RID: 17008
		public int MaxRepairTier;

		// Token: 0x04004271 RID: 17009
		public string Verb = "repair";
	}
}
