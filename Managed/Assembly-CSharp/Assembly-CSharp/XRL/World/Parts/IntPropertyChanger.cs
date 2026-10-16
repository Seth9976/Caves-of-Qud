using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to
	/// true, which it is not by default, the property change is increased
	/// in magnitude by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400. ("Magnitude" change means a negative
	/// amount will have two subtracted, not two added.)
	/// </remarks>
	// Token: 0x02001053 RID: 4179
	[Serializable]
	public class IntPropertyChanger : IPoweredPart
	{
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600A663 RID: 42595 RVA: 0x003CE182 File Offset: 0x003CC382
		// (set) Token: 0x0600A662 RID: 42594 RVA: 0x003CE157 File Offset: 0x003CC357
		public string AffectedProperty
		{
			get
			{
				return this._AffectedProperty;
			}
			set
			{
				if (this.NameForStatus == null || this.NameForStatus == this._AffectedProperty)
				{
					this.NameForStatus = value;
				}
				this._AffectedProperty = value;
			}
		}

		// Token: 0x0600A664 RID: 42596 RVA: 0x003CE18A File Offset: 0x003CC38A
		public IntPropertyChanger()
		{
			this.MustBeUnderstood = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A665 RID: 42597 RVA: 0x003CE1C0 File Offset: 0x003CC3C0
		public override bool SameAs(IPart p)
		{
			IntPropertyChanger intPropertyChanger = p as IntPropertyChanger;
			return !(intPropertyChanger.AffectedProperty != this.AffectedProperty) && !(intPropertyChanger.AfterUpdateEvent != this.AfterUpdateEvent) && intPropertyChanger.Amount == this.Amount && !(intPropertyChanger.BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600A666 RID: 42598 RVA: 0x003CE22C File Offset: 0x003CC42C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeUnequippedEvent.ID || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || (ID == GetShortDescriptionEvent.ID && !this.BehaviorDescription.IsNullOrEmpty());
		}

		// Token: 0x0600A667 RID: 42599 RVA: 0x003CE29D File Offset: 0x003CC49D
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.CheckApply(null, false, 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A668 RID: 42600 RVA: 0x003CE2BD File Offset: 0x003CC4BD
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.BehaviorDescription.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(this.BehaviorDescription, base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A669 RID: 42601 RVA: 0x003CE2EC File Offset: 0x003CC4EC
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApply(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66A RID: 42602 RVA: 0x003CE303 File Offset: 0x003CC503
		public override bool HandleEvent(BeforeUnequippedEvent E)
		{
			this.Unapply(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66B RID: 42603 RVA: 0x003CE318 File Offset: 0x003CC518
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66C RID: 42604 RVA: 0x003CE32A File Offset: 0x003CC52A
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66D RID: 42605 RVA: 0x003CE33C File Offset: 0x003CC53C
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66E RID: 42606 RVA: 0x003CE34E File Offset: 0x003CC54E
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A66F RID: 42607 RVA: 0x003CE360 File Offset: 0x003CC560
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A670 RID: 42608 RVA: 0x003CE372 File Offset: 0x003CC572
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A671 RID: 42609 RVA: 0x003CE375 File Offset: 0x003CC575
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckApply(null, !base.IsWorldMapActive, Amount);
		}

		// Token: 0x0600A672 RID: 42610 RVA: 0x003CE388 File Offset: 0x003CC588
		private void Apply(GameObject GO)
		{
			if (!this.Applied)
			{
				if (GO != null)
				{
					int num = this.Amount;
					if (this.IsPowerLoadSensitive)
					{
						int num2 = this.MyPowerLoadBonus(int.MinValue, 100, this.PowerLoadBonusDivisor);
						if (num2 != 0)
						{
							if (num >= 0)
							{
								num += num2;
							}
							else
							{
								num -= num2;
							}
						}
					}
					GO.ModIntProperty(this.AffectedProperty, num, true);
					this.AmountApplied = num;
					if (!this.AfterUpdateEvent.IsNullOrEmpty())
					{
						GO.FireEvent(this.AfterUpdateEvent);
					}
				}
				this.Applied = true;
			}
		}

		// Token: 0x0600A673 RID: 42611 RVA: 0x003CE40C File Offset: 0x003CC60C
		private void Unapply(GameObject GO)
		{
			if (this.Applied)
			{
				if (GO != null)
				{
					int num = this.AmountApplied;
					if (num == -2147483648)
					{
						num = this.Amount;
					}
					GO.ModIntProperty(this.AffectedProperty, -num, true);
					if (!this.AfterUpdateEvent.IsNullOrEmpty())
					{
						GO.FireEvent(this.AfterUpdateEvent);
					}
				}
				this.Applied = false;
			}
		}

		// Token: 0x0600A674 RID: 42612 RVA: 0x003CE46C File Offset: 0x003CC66C
		private void CheckApply(GameObject GO = null, bool ConsumeCharge = false, int Turns = 1)
		{
			if (this.Applied)
			{
				if (base.IsDisabled(ConsumeCharge, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
				{
					this.Unapply(GO ?? this.ParentObject.Equipped);
					return;
				}
			}
			else if (base.IsReady(ConsumeCharge, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.Apply(GO ?? this.ParentObject.Equipped);
			}
		}

		// Token: 0x040044B4 RID: 17588
		public string _AffectedProperty;

		// Token: 0x040044B5 RID: 17589
		public string AfterUpdateEvent;

		// Token: 0x040044B6 RID: 17590
		public string BehaviorDescription;

		// Token: 0x040044B7 RID: 17591
		public int Amount = 1;

		// Token: 0x040044B8 RID: 17592
		public int AmountApplied = int.MinValue;

		// Token: 0x040044B9 RID: 17593
		public int PowerLoadBonusDivisor = 150;

		// Token: 0x040044BA RID: 17594
		public bool Applied;
	}
}
