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
	// Token: 0x02001071 RID: 4209
	[Serializable]
	public class NightSightInterpolators : IPoweredPart
	{
		// Token: 0x0600A7AE RID: 42926 RVA: 0x003D48FE File Offset: 0x003D2AFE
		public NightSightInterpolators()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A7AF RID: 42927 RVA: 0x003D491C File Offset: 0x003D2B1C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == EquippedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600A7B0 RID: 42928 RVA: 0x003D494C File Offset: 0x003D2B4C
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

		// Token: 0x0600A7B1 RID: 42929 RVA: 0x003D4A18 File Offset: 0x003D2C18
		public override bool HandleEvent(EquippedEvent E)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7B2 RID: 42930 RVA: 0x003D4A5C File Offset: 0x003D2C5C
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
						currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, num3, LightLevel.Interpolight, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7B3 RID: 42931 RVA: 0x003D4B14 File Offset: 0x003D2D14
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004534 RID: 17716
		public int Radius = 18;
	}
}
