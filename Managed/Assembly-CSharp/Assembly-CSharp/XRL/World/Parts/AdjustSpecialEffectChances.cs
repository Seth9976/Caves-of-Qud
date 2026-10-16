using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, adjustments are multiplied by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02000FF1 RID: 4081
	[Serializable]
	public class AdjustSpecialEffectChances : IPoweredPart
	{
		// Token: 0x0600A2B3 RID: 41651 RVA: 0x003B7107 File Offset: 0x003B5307
		public AdjustSpecialEffectChances()
		{
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A2B4 RID: 41652 RVA: 0x003B7124 File Offset: 0x003B5324
		public override bool SameAs(IPart p)
		{
			AdjustSpecialEffectChances adjustSpecialEffectChances = p as AdjustSpecialEffectChances;
			return adjustSpecialEffectChances.LinearAdjust == this.LinearAdjust && adjustSpecialEffectChances.FactorAdjust == this.FactorAdjust && !(adjustSpecialEffectChances.RequireType != this.RequireType) && !(adjustSpecialEffectChances.ExcludeType != this.ExcludeType) && adjustSpecialEffectChances.UsesChargePerTurn == this.UsesChargePerTurn && adjustSpecialEffectChances.UsesChargePerActivation == this.UsesChargePerActivation && base.SameAs(p);
		}

		// Token: 0x0600A2B5 RID: 41653 RVA: 0x003B71A9 File Offset: 0x003B53A9
		public override bool WantTurnTick()
		{
			return this.UsesChargePerTurn;
		}

		// Token: 0x0600A2B6 RID: 41654 RVA: 0x003B71B4 File Offset: 0x003B53B4
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.UsesChargePerTurn && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A2B7 RID: 41655 RVA: 0x003B71F7 File Offset: 0x003B53F7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSpecialEffectChanceEvent>.ID;
		}

		// Token: 0x0600A2B8 RID: 41656 RVA: 0x003B7210 File Offset: 0x003B5410
		public override bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			if ((this.IsObjectActivePartSubject(E.Actor) || this.IsObjectActivePartSubject(E.Object)) && (this.RequireType == null || E.Type.Contains(this.RequireType)) && (this.ExcludeType == null || !E.Type.Contains(this.ExcludeType)))
			{
				int num = this.MyPowerLoadLevel();
				bool flag = this.UsesChargePerActivation && E.BaseChance < 100;
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
					int num4 = this.LinearAdjust;
					float num5 = this.FactorAdjust;
					int num6 = IComponent<GameObject>.PowerLoadBonus(num, 100, 10);
					if (num6 != 0)
					{
						if (num4 != 0)
						{
							num4 = num4 * (100 + num6) / 100;
						}
						if (num5 != 1f)
						{
							num5 = 1f + (num5 - 1f) * (float)(100 + num6) / 100f;
						}
					}
					if (num4 != 0)
					{
						E.Chance += num4;
					}
					if (num5 != 1f)
					{
						E.Chance += (int)Math.Round((double)((float)E.BaseChance * (num5 - 1f)), MidpointRounding.AwayFromZero);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2B9 RID: 41657 RVA: 0x003B7357 File Offset: 0x003B5557
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004363 RID: 17251
		public int LinearAdjust;

		// Token: 0x04004364 RID: 17252
		public float FactorAdjust = 1f;

		// Token: 0x04004365 RID: 17253
		public string RequireType;

		// Token: 0x04004366 RID: 17254
		public string ExcludeType;

		// Token: 0x04004367 RID: 17255
		public bool UsesChargePerTurn;

		// Token: 0x04004368 RID: 17256
		public bool UsesChargePerActivation;
	}
}
