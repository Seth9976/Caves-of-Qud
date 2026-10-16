using System;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to
	/// true, which it is not by default, and ChargeUse is over 0 and
	/// Strength is not explicitly set so that ChargeStrengthFactor is in
	/// play, the effective value of ChargeStrengthFactor will be increased
	/// by ((power load - 100) / 30), i.e. 10 for the standard overload power
	/// load of 400.
	/// </remarks>
	// Token: 0x02001089 RID: 4233
	[Serializable]
	public class RealityStabilization : IPoweredPart
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600A86D RID: 43117 RVA: 0x003D97A3 File Offset: 0x003D79A3
		// (set) Token: 0x0600A86E RID: 43118 RVA: 0x003D97AB File Offset: 0x003D79AB
		public string VariableStrength
		{
			get
			{
				return this._VariableStrength;
			}
			set
			{
				this._VariableStrength = value;
				this.VariableStrengthRoll = null;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600A870 RID: 43120 RVA: 0x003D97C4 File Offset: 0x003D79C4
		// (set) Token: 0x0600A86F RID: 43119 RVA: 0x003D97BB File Offset: 0x003D79BB
		public int Strength
		{
			get
			{
				if (base.WasDisabled())
				{
					return 0;
				}
				if (this.VariableStrength != null)
				{
					if (this.VariableStrengthRoll == null)
					{
						this.VariableStrengthRoll = new DieRoll(this.VariableStrength);
					}
					return Math.Max(Stat.Roll(this.VariableStrengthRoll), this._Strength);
				}
				if (this._Strength != 0)
				{
					return this._Strength;
				}
				if (this.FromSource != null)
				{
					if (this.FromSource.IsInvalid())
					{
						this.FromSource = null;
					}
					else
					{
						RealityStabilization part = this.FromSource.GetPart<RealityStabilization>();
						if (part != null)
						{
							return part.EffectiveStrength;
						}
						Debug.LogError("Had FromSource in " + this.ParentObject.DisplayNameOnly + " of object with no RealityStabilization part, " + this.FromSource.DisplayNameOnly);
					}
				}
				if (this.FromGas)
				{
					if (this.pGas == null)
					{
						this.pGas = this.ParentObject.GetPart<Gas>();
					}
					if (this.pGas != null)
					{
						return Math.Max(this.pGas.Density * (100 + this.pGas.Level * 20) / 100, 1);
					}
					Debug.LogError("Had RealityStabilization FromGas but no gas part on " + this.ParentObject.DebugName);
				}
				if (this.ChargeUse <= 0)
				{
					return 0;
				}
				int num = this.ChargeStrengthFactor;
				if (this.IsPowerLoadSensitive)
				{
					num += this.MyPowerLoadBonus(int.MinValue, 100, 30);
				}
				int num2 = this.ChargeUse * num;
				int num3 = this.ParentObject.QueryCharge(false, 0L, true, true);
				if (num3 >= num2)
				{
					return this.FullChargeStrength;
				}
				return Math.Max(num3 * this.FullChargeStrength / num2, 0);
			}
			set
			{
				this._Strength = value;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600A871 RID: 43121 RVA: 0x003D994C File Offset: 0x003D7B4C
		public int EffectiveStrength
		{
			get
			{
				if (this.EffectiveStrengthCacheTurn >= XRLCore.CurrentTurn && this.EffectiveStrengthCacheDisabled == base.WasDisabled())
				{
					return this.EffectiveStrengthCache;
				}
				int num = this.Strength;
				if (this.GlimmerInterference)
				{
					this.HighestGlimmer = 0;
					this.ForeachActivePartSubject(new Action<GameObject>(this.CollectGlimmer), false);
					if (this.HighestGlimmer > 0)
					{
						num -= this.HighestGlimmer;
						if (num < 0)
						{
							num = 0;
						}
					}
				}
				if (this.HitpointsAffectPerformance && num != 0)
				{
					Statistic stat = this.ParentObject.GetStat("Hitpoints");
					if (stat != null)
					{
						int value = stat.Value;
						int baseValue = stat.BaseValue;
						if (value < baseValue)
						{
							num = (int)Math.Round((double)((float)(num * value) / (float)baseValue));
						}
					}
				}
				this.EffectiveStrengthCache = num;
				this.EffectiveStrengthCacheTurn = XRLCore.CurrentTurn;
				this.EffectiveStrengthCacheDisabled = base.WasDisabled();
				return num;
			}
		}

		// Token: 0x0600A872 RID: 43122 RVA: 0x003D9A1B File Offset: 0x003D7C1B
		public void FlushEffectiveStrengthCache()
		{
			this.EffectiveStrengthCacheTurn = 0L;
		}

		// Token: 0x0600A873 RID: 43123 RVA: 0x003D9A28 File Offset: 0x003D7C28
		private void CollectGlimmer(GameObject obj)
		{
			int psychicGlimmer = obj.GetPsychicGlimmer(null);
			if (psychicGlimmer > this.HighestGlimmer)
			{
				this.HighestGlimmer = psychicGlimmer;
			}
		}

		// Token: 0x0600A874 RID: 43124 RVA: 0x003D9A4D File Offset: 0x003D7C4D
		public RealityStabilization()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
			this.WorksOnCellContents = true;
		}

		// Token: 0x0600A875 RID: 43125 RVA: 0x003D9A84 File Offset: 0x003D7C84
		public int GetVisibilityFor(GameObject GO)
		{
			int num = ((GO == this.ParentObject) ? this.SelfVisibility : this.Visibility);
			if (this.CellVisibilityOffset != 0 && this.ParentObject.CurrentCell != null)
			{
				num += this.CellVisibilityOffset;
			}
			return num;
		}

		// Token: 0x0600A876 RID: 43126 RVA: 0x003D9AC8 File Offset: 0x003D7CC8
		public override bool SameAs(IPart p)
		{
			RealityStabilization realityStabilization = p as RealityStabilization;
			return realityStabilization.Visibility == this.Visibility && realityStabilization.SelfVisibility == this.SelfVisibility && realityStabilization.CellVisibilityOffset == this.CellVisibilityOffset && realityStabilization.ChargeStrengthFactor == this.ChargeStrengthFactor && realityStabilization.FullChargeStrength == this.FullChargeStrength && realityStabilization.Projective == this.Projective && realityStabilization.FromGas == this.FromGas && realityStabilization.FromSource == this.FromSource && realityStabilization.HitpointsAffectPerformance == this.HitpointsAffectPerformance && !(realityStabilization.VariableStrength != this.VariableStrength) && base.SameAs(p);
		}

		// Token: 0x0600A877 RID: 43127 RVA: 0x003D9B88 File Offset: 0x003D7D88
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A878 RID: 43128 RVA: 0x003D9B8C File Offset: 0x003D7D8C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
			this.TakeEffect();
		}

		// Token: 0x0600A879 RID: 43129 RVA: 0x003D9BD0 File Offset: 0x003D7DD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EquippedEvent.ID || ID == EnteredCellEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || (ID == PooledEvent<GlimmerChangeEvent>.ID && this.GlimmerInterference) || ID == ObjectEnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID || (ID == PooledEvent<StatChangeEvent>.ID && this.HitpointsAffectPerformance) || ID == UnequippedEvent.ID || ID == UsingChargeEvent.ID;
		}

		// Token: 0x0600A87A RID: 43130 RVA: 0x003D9C59 File Offset: 0x003D7E59
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Hitpoints" && this.HitpointsAffectPerformance)
			{
				this.FlushEffectiveStrengthCache();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A87B RID: 43131 RVA: 0x003D9C82 File Offset: 0x003D7E82
		public override bool HandleEvent(GlimmerChangeEvent E)
		{
			if (this.GlimmerInterference)
			{
				this.FlushEffectiveStrengthCache();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A87C RID: 43132 RVA: 0x003D9C99 File Offset: 0x003D7E99
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.FlushEffectiveStrengthCache();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A87D RID: 43133 RVA: 0x003D9CA8 File Offset: 0x003D7EA8
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.FlushEffectiveStrengthCache();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A87E RID: 43134 RVA: 0x003D9CB7 File Offset: 0x003D7EB7
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.TakeEffect();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A87F RID: 43135 RVA: 0x003D9CC6 File Offset: 0x003D7EC6
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.TakeEffect();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A880 RID: 43136 RVA: 0x003D9CD5 File Offset: 0x003D7ED5
		public override bool HandleEvent(EquippedEvent E)
		{
			this.TakeEffect();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A881 RID: 43137 RVA: 0x003D9CE4 File Offset: 0x003D7EE4
		public override bool HandleEvent(UnequippedEvent E)
		{
			RealityStabilized effect = E.Actor.GetEffect<RealityStabilized>();
			if (effect != null)
			{
				effect.Maintain();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A882 RID: 43138 RVA: 0x003D9D04 File Offset: 0x003D7F04
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.PossibleEffectDisruption();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A883 RID: 43139 RVA: 0x003D9D13 File Offset: 0x003D7F13
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.PossibleEffectDisruption();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A884 RID: 43140 RVA: 0x003D9D22 File Offset: 0x003D7F22
		public override bool HandleEvent(UsingChargeEvent E)
		{
			if (this.ChargeUse > 0)
			{
				this.FlushEffectiveStrengthCache();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A885 RID: 43141 RVA: 0x003D9D3C File Offset: 0x003D7F3C
		public bool ApplyRealityStabilization(GameObject GO)
		{
			if (!GameObject.Validate(ref GO))
			{
				return true;
			}
			if (GO.GetEffect<RealityStabilized>() == null)
			{
				RealityStabilized realityStabilized = new RealityStabilized();
				if (!GO.ForceApplyEffect(realityStabilized, null))
				{
					return true;
				}
				if (this.WorksOnAdjacentCellContents && !realityStabilized.ScanAdjacentCells && !GO.InSameCellAs(this.ParentObject))
				{
					realityStabilized.ScanAdjacentCells = true;
				}
				realityStabilized.SynchronizeEffect();
			}
			return true;
		}

		// Token: 0x0600A886 RID: 43142 RVA: 0x003D9DA0 File Offset: 0x003D7FA0
		public void TakeEffect()
		{
			if (this.EffectiveStrength > 0 && !this.ParentObject.IsInGraveyard())
			{
				Predicate<GameObject> predicate;
				if ((predicate = this.ApplyDelegate) == null)
				{
					predicate = (this.ApplyDelegate = new Predicate<GameObject>(this.ApplyRealityStabilization));
				}
				this.ForeachActivePartSubjectWhile(predicate, true);
			}
		}

		// Token: 0x0600A887 RID: 43143 RVA: 0x003D9DEA File Offset: 0x003D7FEA
		public void PossibleEffectDisruption()
		{
			if (this.WorksOnAdjacentCellContents)
			{
				this.ParentObject.ExtendedLocalEvent("MaintainRealityStabilization");
			}
			else
			{
				this.ParentObject.LocalEvent("MaintainRealityStabilization");
			}
			this.FlushEffectiveStrengthCache();
		}

		// Token: 0x04004592 RID: 17810
		public int Visibility = 2;

		// Token: 0x04004593 RID: 17811
		public int SelfVisibility;

		// Token: 0x04004594 RID: 17812
		public int CellVisibilityOffset;

		// Token: 0x04004595 RID: 17813
		public int ChargeStrengthFactor = 25;

		// Token: 0x04004596 RID: 17814
		public int FullChargeStrength = 100;

		// Token: 0x04004597 RID: 17815
		public bool Projective;

		// Token: 0x04004598 RID: 17816
		public bool FromGas;

		// Token: 0x04004599 RID: 17817
		public bool GlimmerInterference;

		// Token: 0x0400459A RID: 17818
		public GameObject FromSource;

		// Token: 0x0400459B RID: 17819
		public bool HitpointsAffectPerformance;

		// Token: 0x0400459C RID: 17820
		public string _VariableStrength;

		// Token: 0x0400459D RID: 17821
		[NonSerialized]
		private int HighestGlimmer;

		// Token: 0x0400459E RID: 17822
		[NonSerialized]
		private Gas pGas;

		// Token: 0x0400459F RID: 17823
		[NonSerialized]
		private DieRoll VariableStrengthRoll;

		// Token: 0x040045A0 RID: 17824
		[NonSerialized]
		private int EffectiveStrengthCache;

		// Token: 0x040045A1 RID: 17825
		[NonSerialized]
		private long EffectiveStrengthCacheTurn;

		// Token: 0x040045A2 RID: 17826
		[NonSerialized]
		private bool EffectiveStrengthCacheDisabled;

		// Token: 0x040045A3 RID: 17827
		public int _Strength;

		// Token: 0x040045A4 RID: 17828
		[NonSerialized]
		private Predicate<GameObject> ApplyDelegate;
	}
}
