using System;
using System.Collections.Generic;
using XRL.Core;

namespace XRL.World.Effects
{
	/// This class is not used in the base game.
	// Token: 0x02000D4C RID: 3404
	[Serializable]
	public abstract class ICamouflageEffect : Effect
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600868C RID: 34444 RVA: 0x00306334 File Offset: 0x00304534
		// (set) Token: 0x0600868B RID: 34443 RVA: 0x003062D4 File Offset: 0x003044D4
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (base.Object != null && base.Object.HasStat("DV"))
				{
					this._Level = Math.Max(value, 0);
					base.StatShifter.SetStatShift(base.Object, "DV", this._Level, false);
					return;
				}
				this._Level = Math.Max(value, 0);
			}
		}

		// Token: 0x0600868D RID: 34445 RVA: 0x0030633C File Offset: 0x0030453C
		public ICamouflageEffect()
		{
			this.Duration = 1;
			this.DisplayName = "camouflaged";
		}

		// Token: 0x0600868E RID: 34446 RVA: 0x00306356 File Offset: 0x00304556
		public override int GetEffectType()
		{
			return 131072;
		}

		// Token: 0x0600868F RID: 34447 RVA: 0x00306360 File Offset: 0x00304560
		public void SetContribution(GameObject obj, int amount)
		{
			if (amount < 0)
			{
				throw new Exception("amount cannot be negative");
			}
			if (this.Contributions == null)
			{
				if (amount == 0)
				{
					return;
				}
				this.Contributions = new Dictionary<GameObject, int>(ICamouflageEffect.MaxContributionsSize);
				this.ContributionTicks = new Dictionary<GameObject, long>(ICamouflageEffect.MaxContributionsSize);
			}
			if (this.Contributions.ContainsKey(obj))
			{
				int num = this.Contributions[obj];
				if (amount > 0)
				{
					this.Contributions[obj] = amount;
					this.ContributionTicks[obj] = XRLCore.CurrentTurn;
					this.Level += amount - num;
					return;
				}
				this.Contributions.Remove(obj);
				this.ContributionTicks.Remove(obj);
				this.Level -= num;
				if (this.Level <= 0)
				{
					base.Object.RemoveEffect(this, true);
					return;
				}
			}
			else if (amount > 0)
			{
				this.Contributions.Add(obj, amount);
				this.ContributionTicks.Add(obj, XRLCore.CurrentTurn);
				this.Level += amount;
				if (this.Contributions.Count > ICamouflageEffect.MaxContributionsSize)
				{
					ICamouflageEffect.MaxContributionsSize = this.Contributions.Count;
				}
			}
		}

		// Token: 0x06008690 RID: 34448 RVA: 0x0030648B File Offset: 0x0030468B
		public void RemoveContribution(GameObject obj)
		{
			this.SetContribution(obj, 0);
		}

		// Token: 0x06008691 RID: 34449 RVA: 0x00306495 File Offset: 0x00304695
		public override string GetDescription()
		{
			return this.DisplayName;
		}

		// Token: 0x06008692 RID: 34450 RVA: 0x0030649D File Offset: 0x0030469D
		public override string GetDetails()
		{
			return this.Level.Signed(false) + " DV.";
		}

		// Token: 0x06008693 RID: 34451 RVA: 0x003064B5 File Offset: 0x003046B5
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect(base.ClassName))
			{
				return false;
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x06008694 RID: 34452 RVA: 0x003064CE File Offset: 0x003046CE
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
		}

		// Token: 0x06008695 RID: 34453 RVA: 0x003064D6 File Offset: 0x003046D6
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "DV", this.Level, false);
		}

		// Token: 0x06008696 RID: 34454 RVA: 0x003064F6 File Offset: 0x003046F6
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008697 RID: 34455 RVA: 0x00306509 File Offset: 0x00304709
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008698 RID: 34456
		public abstract bool EnablesCamouflage(GameObject GO);

		// Token: 0x06008699 RID: 34457 RVA: 0x00306534 File Offset: 0x00304734
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.Contributions == null || base.Object.CurrentCell == null || !base.Object.CurrentCell.HasObjectOtherThan(new Predicate<GameObject>(this.EnablesCamouflage), base.Object))
				{
					base.Object.RemoveEffect(this, true);
					goto IL_00EE;
				}
				GameObject gameObject = null;
				for (;;)
				{
					IL_0064:
					if (gameObject != null)
					{
						this.RemoveContribution(gameObject);
						gameObject = null;
					}
					long num = XRLCore.CurrentTurn - 1L;
					using (Dictionary<GameObject, long>.Enumerator enumerator = this.ContributionTicks.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<GameObject, long> keyValuePair = enumerator.Current;
							if (keyValuePair.Value < num)
							{
								gameObject = keyValuePair.Key;
								goto IL_0064;
							}
						}
						goto IL_00EE;
					}
					break;
				}
			}
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			IL_00EE:
			return base.FireEvent(E);
		}

		// Token: 0x040039E6 RID: 14822
		[NonSerialized]
		public Dictionary<GameObject, int> Contributions;

		// Token: 0x040039E7 RID: 14823
		[NonSerialized]
		public Dictionary<GameObject, long> ContributionTicks;

		// Token: 0x040039E8 RID: 14824
		[NonSerialized]
		public static int MaxContributionsSize = 1;

		// Token: 0x040039E9 RID: 14825
		public int _Level;
	}
}
