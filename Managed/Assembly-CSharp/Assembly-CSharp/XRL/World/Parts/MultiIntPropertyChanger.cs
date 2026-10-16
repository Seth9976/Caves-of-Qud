using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x0200106B RID: 4203
	[Serializable]
	public class MultiIntPropertyChanger : IPoweredPart
	{
		// Token: 0x0600A759 RID: 42841 RVA: 0x003D2555 File Offset: 0x003D0755
		public MultiIntPropertyChanger()
		{
			this.MustBeUnderstood = true;
			this.WorksOnWearer = true;
			this.NameForStatus = "ActiveSystems";
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600A75B RID: 42843 RVA: 0x003D2586 File Offset: 0x003D0786
		// (set) Token: 0x0600A75A RID: 42842 RVA: 0x003D2576 File Offset: 0x003D0776
		public string AffectedProperties
		{
			get
			{
				return this._AffectedProperties;
			}
			set
			{
				this._AffectedProperties = value;
				this._PropertyMap = null;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600A75C RID: 42844 RVA: 0x003D258E File Offset: 0x003D078E
		public Dictionary<string, int> PropertyMap
		{
			get
			{
				if (this._PropertyMap == null)
				{
					this._PropertyMap = IComponent<GameObject>.MapFromString(this._AffectedProperties);
				}
				return this._PropertyMap;
			}
		}

		// Token: 0x0600A75D RID: 42845 RVA: 0x003D25B0 File Offset: 0x003D07B0
		public override bool SameAs(IPart p)
		{
			MultiIntPropertyChanger multiIntPropertyChanger = p as MultiIntPropertyChanger;
			return !(multiIntPropertyChanger.AffectedProperties != this.AffectedProperties) && !(multiIntPropertyChanger.AfterUpdateEvent != this.AfterUpdateEvent) && !(multiIntPropertyChanger.BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600A75E RID: 42846 RVA: 0x003D260C File Offset: 0x003D080C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeUnequippedEvent.ID || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription));
		}

		// Token: 0x0600A75F RID: 42847 RVA: 0x003D267D File Offset: 0x003D087D
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.CheckApply(null, false, 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A760 RID: 42848 RVA: 0x003D269D File Offset: 0x003D089D
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApply(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A761 RID: 42849 RVA: 0x003D26B4 File Offset: 0x003D08B4
		public override bool HandleEvent(BeforeUnequippedEvent E)
		{
			this.Unapply(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A762 RID: 42850 RVA: 0x003D26C9 File Offset: 0x003D08C9
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A763 RID: 42851 RVA: 0x003D26DB File Offset: 0x003D08DB
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A764 RID: 42852 RVA: 0x003D26ED File Offset: 0x003D08ED
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A765 RID: 42853 RVA: 0x003D26FF File Offset: 0x003D08FF
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A766 RID: 42854 RVA: 0x003D2711 File Offset: 0x003D0911
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApply(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A767 RID: 42855 RVA: 0x003D2723 File Offset: 0x003D0923
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.BehaviorDescription))
			{
				E.Postfix.AppendRules(this.BehaviorDescription, base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A768 RID: 42856 RVA: 0x003D2752 File Offset: 0x003D0952
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A769 RID: 42857 RVA: 0x003D2755 File Offset: 0x003D0955
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckApply(null, !base.IsWorldMapActive, Amount);
		}

		// Token: 0x0600A76A RID: 42858 RVA: 0x003D2768 File Offset: 0x003D0968
		private void Apply(GameObject GO)
		{
			if (!this.Applied)
			{
				if (GO != null)
				{
					foreach (string text in this.PropertyMap.Keys)
					{
						GO.ModIntProperty(text, this.PropertyMap[text], true);
					}
					if (!string.IsNullOrEmpty(this.AfterUpdateEvent))
					{
						GO.FireEvent(this.AfterUpdateEvent);
					}
				}
				this.Applied = true;
			}
		}

		// Token: 0x0600A76B RID: 42859 RVA: 0x003D27FC File Offset: 0x003D09FC
		private void Unapply(GameObject GO)
		{
			if (this.Applied)
			{
				if (GO != null)
				{
					foreach (string text in this.PropertyMap.Keys)
					{
						GO.ModIntProperty(text, -this.PropertyMap[text], true);
					}
					if (!string.IsNullOrEmpty(this.AfterUpdateEvent))
					{
						GO.FireEvent(this.AfterUpdateEvent);
					}
				}
				this.Applied = false;
			}
		}

		// Token: 0x0600A76C RID: 42860 RVA: 0x003D2890 File Offset: 0x003D0A90
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

		// Token: 0x04004517 RID: 17687
		public string _AffectedProperties;

		// Token: 0x04004518 RID: 17688
		public string AfterUpdateEvent;

		// Token: 0x04004519 RID: 17689
		public string BehaviorDescription;

		// Token: 0x0400451A RID: 17690
		public bool Applied;

		// Token: 0x0400451B RID: 17691
		[NonSerialized]
		private Dictionary<string, int> _PropertyMap;
	}
}
