using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000FED RID: 4077
	[Obsolete("Use MutationOnEquip")]
	[Serializable]
	public abstract class AddsMutationOnEquip<T> : IActivePart where T : BaseMutation, new()
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600A27E RID: 41598 RVA: 0x003B5E58 File Offset: 0x003B4058
		// (set) Token: 0x0600A27F RID: 41599 RVA: 0x003B5E8A File Offset: 0x003B408A
		public string MutationDisplayName
		{
			get
			{
				if (this._MutationDisplayName == null)
				{
					T t = new T();
					this._MutationDisplayName = t.DisplayName;
				}
				return this._MutationDisplayName;
			}
			set
			{
				this._MutationDisplayName = value;
			}
		}

		// Token: 0x0600A280 RID: 41600 RVA: 0x003B5E94 File Offset: 0x003B4094
		public AddsMutationOnEquip()
		{
			T t = new T();
			this._MutationDisplayName = t.DisplayName;
			this.ClassName = t.Name;
			this.TrackingProperty = "Equipped" + this.ClassName;
			this.CanLevel = t.CanLevel();
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A281 RID: 41601 RVA: 0x003B5F20 File Offset: 0x003B4120
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			AddsMutationOnEquip<T> addsMutationOnEquip = base.DeepCopy(Parent, MapInv) as AddsMutationOnEquip<T>;
			addsMutationOnEquip.MutationWasAdded = false;
			addsMutationOnEquip.MutationWasAddedTo = null;
			return addsMutationOnEquip;
		}

		// Token: 0x0600A282 RID: 41602 RVA: 0x003B5F3D File Offset: 0x003B413D
		public override bool SameAs(IPart p)
		{
			return p is AddsMutationOnEquip<T> && base.SameAs(p);
		}

		// Token: 0x0600A283 RID: 41603 RVA: 0x003B5F50 File Offset: 0x003B4150
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && this.Describe) || ID == PowerSwitchFlippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A284 RID: 41604 RVA: 0x003B5FC1 File Offset: 0x003B41C1
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A285 RID: 41605 RVA: 0x003B5FDC File Offset: 0x003B41DC
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A286 RID: 41606 RVA: 0x003B5FF7 File Offset: 0x003B41F7
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A287 RID: 41607 RVA: 0x003B6012 File Offset: 0x003B4212
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A288 RID: 41608 RVA: 0x003B602D File Offset: 0x003B422D
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A289 RID: 41609 RVA: 0x003B6048 File Offset: 0x003B4248
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A28A RID: 41610 RVA: 0x003B6063 File Offset: 0x003B4263
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A28B RID: 41611 RVA: 0x003B607E File Offset: 0x003B427E
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyBonus(E.Actor, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A28C RID: 41612 RVA: 0x003B6094 File Offset: 0x003B4294
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyBonus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A28D RID: 41613 RVA: 0x003B60AC File Offset: 0x003B42AC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.CanLevel)
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					"Grants you ",
					this.MutationDisplayName,
					" at level ",
					this.Level.ToString(),
					"."
				}));
			}
			else
			{
				E.Postfix.AppendRules("Grants you " + this.MutationDisplayName + ".");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A28E RID: 41614 RVA: 0x003B6134 File Offset: 0x003B4334
		private void ApplyBonus(GameObject Subject)
		{
			if (this.MutationWasAdded && !Subject.IDMatch(this.MutationWasAddedTo))
			{
				GameObject gameObject = GameObject.FindByID(this.MutationWasAddedTo);
				if (gameObject != null)
				{
					this.UnapplyBonus(gameObject);
				}
			}
			if (!this.MutationWasAdded && this.IsObjectActivePartSubject(Subject))
			{
				this.mutationTracker = Subject.RequirePart<Mutations>(false).AddMutationMod(typeof(T), null, this.Level, Mutations.MutationModifierTracker.SourceType.Equipment, this.ParentObject.DisplayName);
				this.MutationWasAdded = true;
				this.MutationWasAddedTo = Subject.ID;
			}
		}

		// Token: 0x0600A28F RID: 41615 RVA: 0x003B61C0 File Offset: 0x003B43C0
		private void UnapplyBonus(GameObject Subject)
		{
			if (this.MutationWasAdded)
			{
				if (GameObject.Validate(ref Subject) && Subject.IDMatch(this.MutationWasAddedTo))
				{
					Subject.RequirePart<Mutations>(false).RemoveMutationMod(this.mutationTracker);
				}
				else
				{
					GameObject gameObject = GameObject.FindByID(this.MutationWasAddedTo);
					if (gameObject != null)
					{
						gameObject.RequirePart<Mutations>(false).RemoveMutationMod(this.mutationTracker);
					}
				}
				this.MutationWasAdded = false;
				this.MutationWasAddedTo = null;
			}
		}

		// Token: 0x0600A290 RID: 41616 RVA: 0x003B6230 File Offset: 0x003B4430
		public void CheckApplyBonus(GameObject Subject, bool UseCharge = false)
		{
			if (this.MutationWasAdded)
			{
				if (!base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || !this.IsObjectActivePartSubject(Subject))
				{
					this.UnapplyBonus(Subject);
					return;
				}
			}
			else if (base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ApplyBonus(Subject);
			}
		}

		// Token: 0x0600A291 RID: 41617 RVA: 0x003B62AD File Offset: 0x003B44AD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004355 RID: 17237
		public int moddedAmount;

		// Token: 0x04004356 RID: 17238
		public int Level = 1;

		// Token: 0x04004357 RID: 17239
		public string _MutationDisplayName;

		// Token: 0x04004358 RID: 17240
		public string ClassName;

		// Token: 0x04004359 RID: 17241
		public string TrackingProperty;

		// Token: 0x0400435A RID: 17242
		public bool MutationWasAdded;

		// Token: 0x0400435B RID: 17243
		public string MutationWasAddedTo;

		// Token: 0x0400435C RID: 17244
		public bool CanLevel;

		// Token: 0x0400435D RID: 17245
		public bool Describe = true;

		// Token: 0x0400435E RID: 17246
		public Guid mutationTracker;
	}
}
