using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to
	/// true, which it is not by default, temperature changes are increased
	/// in magnitude by a percentage equal to ((power load - 100) / 10), i.e.
	/// 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x020010AF RID: 4271
	[Serializable]
	public class TemperatureAdjuster : IPoweredPart
	{
		// Token: 0x0600A9FA RID: 43514 RVA: 0x003E2870 File Offset: 0x003E0A70
		public TemperatureAdjuster()
		{
			this.WorksOnSelf = true;
			this.WorksOnHolder = true;
			this.WorksOnWearer = true;
			this.WorksOnCarrier = true;
			this.WorksOnCellContents = true;
			this.WorksOnAdjacentCellContents = true;
		}

		// Token: 0x0600A9FB RID: 43515 RVA: 0x003E28C4 File Offset: 0x003E0AC4
		public override bool SameAs(IPart p)
		{
			TemperatureAdjuster temperatureAdjuster = p as TemperatureAdjuster;
			return temperatureAdjuster.TemperatureAmount == this.TemperatureAmount && temperatureAdjuster.TemperatureThreshold == this.TemperatureThreshold && temperatureAdjuster.ThresholdAbove == this.ThresholdAbove && temperatureAdjuster.AlwaysUseCharge == this.AlwaysUseCharge && !(temperatureAdjuster.BehaviorDescription != this.BehaviorDescription) && temperatureAdjuster.InactiveOnWorldMap == this.InactiveOnWorldMap && base.SameAs(p);
		}

		// Token: 0x0600A9FC RID: 43516 RVA: 0x003E2944 File Offset: 0x003E0B44
		public override bool GetActivePartLocallyDefinedFailure()
		{
			if (this.InactiveOnWorldMap)
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.OnWorldMap())
							{
								return true;
							}
						}
						return false;
					}
				}
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null && activePartFirstSubject.OnWorldMap())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A9FD RID: 43517 RVA: 0x003E29C4 File Offset: 0x003E0BC4
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "Inactive";
		}

		// Token: 0x0600A9FE RID: 43518 RVA: 0x003E29CC File Offset: 0x003E0BCC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetShortDescriptionEvent.ID && !this.BehaviorDescription.IsNullOrEmpty()) || (ID == SingletonEvent<RadiatesHeatAdjacentEvent>.ID && this.WorksOnAdjacentCellContents && this.TemperatureAmount > 0) || (ID == SingletonEvent<RadiatesHeatEvent>.ID && this.WorksOnCellContents && this.TemperatureAmount > 0);
		}

		// Token: 0x0600A9FF RID: 43519 RVA: 0x003E2A2E File Offset: 0x003E0C2E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.BehaviorDescription.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(this.BehaviorDescription, base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA00 RID: 43520 RVA: 0x003E2A60 File Offset: 0x003E0C60
		public override bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return (!this.WorksOnAdjacentCellContents || this.TemperatureAmount <= 0 || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA01 RID: 43521 RVA: 0x003E2AB0 File Offset: 0x003E0CB0
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return (!this.WorksOnCellContents || this.TemperatureAmount <= 0 || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA02 RID: 43522 RVA: 0x003E2AFF File Offset: 0x003E0CFF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AA03 RID: 43523 RVA: 0x003E2B02 File Offset: 0x003E0D02
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessTurns(Amount);
		}

		// Token: 0x0600AA04 RID: 43524 RVA: 0x003E2B0C File Offset: 0x003E0D0C
		public int ProcessTurns(int Turns)
		{
			int num = 0;
			int num2 = this.MyPowerLoadLevel();
			bool alwaysUseCharge = this.AlwaysUseCharge;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			int? num3 = new int?(num2);
			if (base.IsReady(alwaysUseCharge, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, Turns, null, false, 0L, num3))
			{
				bool flag10 = false;
				if (this.ActivePartHasMultipleSubjects())
				{
					using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject = enumerator.Current;
							int num4 = 1;
							while (num4 <= Turns && this.AdjustTemperature(gameObject, num2))
							{
								flag10 = true;
								if (num4 > num)
								{
									num = num4;
								}
								num4++;
							}
						}
						goto IL_00CD;
					}
				}
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null)
				{
					int num5 = 1;
					while (num5 <= Turns && this.AdjustTemperature(activePartFirstSubject, num2))
					{
						flag10 = true;
						if (num5 > num)
						{
							num = num5;
						}
						num5++;
					}
				}
				IL_00CD:
				if (flag10 && !this.AlwaysUseCharge)
				{
					int num6 = num;
					num3 = new int?(num2);
					base.ConsumeCharge(num6, null, num3);
				}
			}
			return num;
		}

		// Token: 0x0600AA05 RID: 43525 RVA: 0x003E2C20 File Offset: 0x003E0E20
		public bool AdjustTemperature(GameObject obj, int PowerLoad = -2147483648)
		{
			if (obj.Physics == null)
			{
				return false;
			}
			if (this.ThresholdAbove)
			{
				if (obj.Physics.Temperature >= this.TemperatureThreshold)
				{
					return false;
				}
			}
			else if (obj.Physics.Temperature <= this.TemperatureThreshold)
			{
				return false;
			}
			int num = this.TemperatureAmount;
			if (this.IsPowerLoadSensitive)
			{
				if (PowerLoad == -2147483648)
				{
					PowerLoad = this.MyPowerLoadLevel();
				}
				int num2 = this.MyPowerLoadBonus(PowerLoad, 100, 10);
				if (num2 != 0)
				{
					num = num * (100 + num2) / 100;
				}
			}
			obj.TemperatureChange(num, obj.Equipped ?? obj, false, false, false, false, 0, null, null);
			return true;
		}

		// Token: 0x04004609 RID: 17929
		public int TemperatureAmount = 5;

		// Token: 0x0400460A RID: 17930
		public int TemperatureThreshold = 100;

		// Token: 0x0400460B RID: 17931
		public bool ThresholdAbove = true;

		// Token: 0x0400460C RID: 17932
		public bool AlwaysUseCharge;

		// Token: 0x0400460D RID: 17933
		public string BehaviorDescription;

		// Token: 0x0400460E RID: 17934
		public bool InactiveOnWorldMap;
	}
}
