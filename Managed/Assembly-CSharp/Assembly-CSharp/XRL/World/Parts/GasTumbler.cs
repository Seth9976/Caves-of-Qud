using System;
using UnityEngine;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the gas multiplier is increased and dispersal
	/// multiplier is decreased by a percentage equal to ((power load - 100) / 10),
	/// i.e. 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200103B RID: 4155
	[Serializable]
	public class GasTumbler : IPoweredPart
	{
		// Token: 0x0600A56C RID: 42348 RVA: 0x003C81CC File Offset: 0x003C63CC
		public GasTumbler()
		{
			this.IsPowerLoadSensitive = true;
			this.WorksOnWearer = true;
			this.NameForStatus = "GasDensifier";
		}

		// Token: 0x0600A56D RID: 42349 RVA: 0x003C8200 File Offset: 0x003C6400
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A56E RID: 42350 RVA: 0x003C8203 File Offset: 0x003C6403
		public override bool WantTurnTick()
		{
			return this.ChargeUse > 0;
		}

		// Token: 0x0600A56F RID: 42351 RVA: 0x003C8210 File Offset: 0x003C6410
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A570 RID: 42352 RVA: 0x003C824B File Offset: 0x003C644B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A571 RID: 42353 RVA: 0x003C8274 File Offset: 0x003C6474
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int num = this.MyPowerLoadLevel();
			int num2 = this.GetDensity(num) - 100;
			int num3 = this.GetDispersal(num) - 100;
			E.Postfix.Append("\n{{rules|");
			if (num2 >= 0)
			{
				E.Postfix.Append("Gases you release are " + num2.ToString() + "% denser.");
			}
			else
			{
				E.Postfix.Append("Gases you release are " + (-num2).ToString() + "% less dense.");
			}
			E.Postfix.Append("\n");
			if (num3 <= 0)
			{
				E.Postfix.Append("Gases you release disperse " + (-num3).ToString() + "% slower.");
			}
			else
			{
				E.Postfix.Append("Gases you release disperse " + num3.ToString() + "% faster.");
			}
			E.Postfix.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A572 RID: 42354 RVA: 0x003C8372 File Offset: 0x003C6572
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "CreatorModifyGas");
			E.Actor.RegisterPartEvent(this, "CreatorModifyGasDispersal");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A573 RID: 42355 RVA: 0x003C839D File Offset: 0x003C659D
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "CreatorModifyGas");
			E.Actor.UnregisterPartEvent(this, "CreatorModifyGasDispersal");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A574 RID: 42356 RVA: 0x003C83C8 File Offset: 0x003C65C8
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A575 RID: 42357 RVA: 0x003C83CC File Offset: 0x003C65CC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CreatorModifyGasDispersal")
			{
				int num = this.MyPowerLoadLevel();
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
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					float num4 = (float)this.GetDispersal(num) / 100f;
					E.SetParameter("Rate", Mathf.RoundToInt((float)E.GetIntParameter("Rate", 0) * num4));
				}
			}
			else if (E.ID == "CreatorModifyGas")
			{
				int num5 = this.MyPowerLoadLevel();
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = false;
				bool flag19 = false;
				bool flag20 = false;
				int num6 = 1;
				int? num3 = new int?(num5);
				if (base.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num6, null, false, 0L, num3))
				{
					Gas gas = E.GetParameter("Gas") as Gas;
					if (gas != null)
					{
						float num7 = (float)this.GetDensity(num5) / 100f;
						gas.Density = Mathf.RoundToInt((float)gas.Density * num7);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A576 RID: 42358 RVA: 0x003C84DC File Offset: 0x003C66DC
		public int GetDispersal(int PowerLoad)
		{
			int num = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num == 0)
			{
				return this.DispersalMultiplier;
			}
			return this.DispersalMultiplier * (100 - num) / 100;
		}

		// Token: 0x0600A577 RID: 42359 RVA: 0x003C8510 File Offset: 0x003C6710
		public int GetDensity(int PowerLoad)
		{
			int num = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num == 0)
			{
				return this.DensityMultiplier;
			}
			return this.DensityMultiplier * (100 + num) / 100;
		}

		// Token: 0x0400445C RID: 17500
		public int DispersalMultiplier = 25;

		// Token: 0x0400445D RID: 17501
		public int DensityMultiplier = 200;
	}
}
