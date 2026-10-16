using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, effective radius is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001072 RID: 4210
	[Serializable]
	public class NightVision : IPoweredPart
	{
		// Token: 0x0600A7B4 RID: 42932 RVA: 0x003D4B17 File Offset: 0x003D2D17
		public NightVision()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A7B5 RID: 42933 RVA: 0x003D4B35 File Offset: 0x003D2D35
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == BeforeRenderEvent.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == EquippedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600A7B6 RID: 42934 RVA: 0x003D4B6C File Offset: 0x003D2D6C
		public override bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			if (!base.OnWorldMap && base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null))
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null && equipped.IsPlayer() && AutoAct.IsInterruptable() && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					AutoAct.Interrupt(equipped.poss(this.ParentObject, true, null) + this.ParentObject.GetVerb("have", true, false, false, true, false) + " stopped working", null, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7B7 RID: 42935 RVA: 0x003D4C38 File Offset: 0x003D2E38
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && E.Damage.IsLightDamage())
			{
				E.Damage.Amount = E.Damage.Amount * 5 / 4;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7B8 RID: 42936 RVA: 0x003D4C9C File Offset: 0x003D2E9C
		public override bool HandleEvent(EquippedEvent E)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7B9 RID: 42937 RVA: 0x003D4CE0 File Offset: 0x003D2EE0
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && equipped.IsPlayer() && !equipped.OnWorldMap())
			{
				Cell currentCell = equipped.CurrentCell;
				if (currentCell != null)
				{
					int lastPowerLoadLevel = base.GetLastPowerLoadLevel();
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num = 1;
					int? num2 = new int?(lastPowerLoadLevel);
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, 0L, num2))
					{
						int num3 = this.Radius;
						int num4 = this.MyPowerLoadBonus(lastPowerLoadLevel, 100, 10);
						if (num4 != 0)
						{
							num3 = num3 * (100 + num4) / 100;
						}
						currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, num3, LightLevel.Darkvision, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7BA RID: 42938 RVA: 0x003D4D95 File Offset: 0x003D2F95
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004535 RID: 17717
		public int Radius = 40;
	}
}
