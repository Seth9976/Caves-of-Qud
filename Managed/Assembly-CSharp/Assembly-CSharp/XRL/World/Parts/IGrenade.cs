using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FD5 RID: 4053
	[Serializable]
	public abstract class IGrenade : IPart
	{
		// Token: 0x0600A10E RID: 41230 RVA: 0x003A9AC4 File Offset: 0x003A7CC4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterThrownEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x0600A10F RID: 41231 RVA: 0x003A9B15 File Offset: 0x003A7D15
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ParentObject.HasIntProperty("Primed"))
			{
				this.ParentObject.RemoveIntProperty("Primed");
				this.Detonate(null, null, null, false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A110 RID: 41232 RVA: 0x003A9B4D File Offset: 0x003A7D4D
		public override bool HandleEvent(AfterThrownEvent E)
		{
			return !this.Detonate(this.ParentObject.CurrentCell, E.Actor, E.ApparentTarget, false) && base.HandleEvent(E);
		}

		// Token: 0x0600A111 RID: 41233 RVA: 0x003A9B78 File Offset: 0x003A7D78
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				E.AddAction("Detonate", "detonate", "Detonate", null, 'n', false, 0, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A112 RID: 41234 RVA: 0x003A9BBC File Offset: 0x003A7DBC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Detonate")
			{
				if (base.OnWorldMap)
				{
					Popup.ShowFail("You cannot do that on the world map.", true, true, true);
				}
				else
				{
					E.Item.SplitFromStack();
					MissileWeapon.SetupProjectile(E.Item, E.Actor, null, null);
					if (this.Detonate(null, E.Actor, null, false))
					{
						E.Actor.UseEnergy(1000, "Item Grenade Detonate", null, null, false);
						E.RequestInterfaceExit();
					}
					else
					{
						MissileWeapon.CleanupProjectile(E.Item);
						E.Item.CheckStack();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A113 RID: 41235 RVA: 0x003A9C6C File Offset: 0x003A7E6C
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.ParentObject.SplitFromStack();
				if (!this.Detonate(null, E.Actor, null, true))
				{
					return false;
				}
				this.ParentObject.CheckStack();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A114 RID: 41236 RVA: 0x003A9CB9 File Offset: 0x003A7EB9
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A115 RID: 41237 RVA: 0x003A9CCF File Offset: 0x003A7ECF
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A116 RID: 41238 RVA: 0x003A9CE8 File Offset: 0x003A7EE8
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				E.IdentifyIfDestroyed = true;
				this.ParentObject.SplitFromStack();
				if (this.Detonate(null, E.Actor, null, true))
				{
					this.ParentObject.CheckStack();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600A117 RID: 41239
		protected abstract bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false);

		// Token: 0x0600A118 RID: 41240 RVA: 0x003A9D48 File Offset: 0x003A7F48
		public bool Detonate(Cell InCell = null, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			Cell cell = InCell ?? this.ParentObject.GetCurrentCell();
			if (cell == null)
			{
				return false;
			}
			if (this.ParentObject.CurrentCell != cell)
			{
				this.ParentObject.RemoveFromContext(null);
				cell.AddObject(this.ParentObject, true, false, true, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			if (!BeforeDetonateEvent.Check(this.ParentObject, Actor, ApparentTarget, Indirect))
			{
				return false;
			}
			bool flag = this.DoDetonate(cell, Actor, ApparentTarget, Indirect);
			if (!flag)
			{
				this.ParentObject.Gravitate();
			}
			return flag;
		}
	}
}
