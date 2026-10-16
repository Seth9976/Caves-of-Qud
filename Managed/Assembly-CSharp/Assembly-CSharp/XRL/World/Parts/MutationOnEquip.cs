using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200106D RID: 4205
	[Serializable]
	public class MutationOnEquip : IActivePart
	{
		// Token: 0x0600A77B RID: 42875 RVA: 0x003D2F0D File Offset: 0x003D110D
		public MutationOnEquip()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A77C RID: 42876 RVA: 0x003D2F40 File Offset: 0x003D1140
		public override void Initialize()
		{
			if (!this.ClassName.IsNullOrEmpty())
			{
				BaseMutation genericMutation = Mutations.GetGenericMutation(this.ClassName, this.Variant, null);
				if (genericMutation != null)
				{
					if (this.DisplayName == null)
					{
						this.DisplayName = genericMutation.GetDisplayName(true);
					}
					this.CanLevel = genericMutation.CanLevel();
					return;
				}
			}
			MutationEntry mutationEntry;
			if (MutationFactory.TryGetMutationEntry(this.DisplayName, this.ClassName, out mutationEntry))
			{
				if (this.DisplayName == null)
				{
					this.DisplayName = mutationEntry.GetDisplayName(false);
				}
				if (this.ClassName == null)
				{
					this.ClassName = mutationEntry.Class;
				}
				if (this.Variant == null)
				{
					this.Variant = mutationEntry.Variant;
				}
				this.CanLevel = mutationEntry.Mutation.CanLevel();
			}
		}

		// Token: 0x0600A77D RID: 42877 RVA: 0x003D2FF5 File Offset: 0x003D11F5
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			MutationOnEquip mutationOnEquip = base.DeepCopy(Parent, MapInv) as MutationOnEquip;
			mutationOnEquip.MutationWasAdded = false;
			mutationOnEquip.MutationWasAddedTo = null;
			return mutationOnEquip;
		}

		// Token: 0x0600A77E RID: 42878 RVA: 0x003D3012 File Offset: 0x003D1212
		public override bool SameAs(IPart Part)
		{
			return false;
		}

		// Token: 0x0600A77F RID: 42879 RVA: 0x003D3018 File Offset: 0x003D1218
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && this.Describe) || ID == PowerSwitchFlippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A780 RID: 42880 RVA: 0x003D3089 File Offset: 0x003D1289
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A781 RID: 42881 RVA: 0x003D30A4 File Offset: 0x003D12A4
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A782 RID: 42882 RVA: 0x003D30BF File Offset: 0x003D12BF
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A783 RID: 42883 RVA: 0x003D30DA File Offset: 0x003D12DA
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A784 RID: 42884 RVA: 0x003D30F5 File Offset: 0x003D12F5
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A785 RID: 42885 RVA: 0x003D3110 File Offset: 0x003D1310
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A786 RID: 42886 RVA: 0x003D312B File Offset: 0x003D132B
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A787 RID: 42887 RVA: 0x003D3146 File Offset: 0x003D1346
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyBonus(E.Actor, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A788 RID: 42888 RVA: 0x003D315C File Offset: 0x003D135C
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyBonus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A789 RID: 42889 RVA: 0x003D3174 File Offset: 0x003D1374
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.CanLevel)
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					"Grants you ",
					this.DisplayName,
					" at level ",
					this.Level.ToString(),
					"."
				}));
			}
			else
			{
				E.Postfix.AppendRules("Grants you " + this.DisplayName + ".");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A78A RID: 42890 RVA: 0x003D31FC File Offset: 0x003D13FC
		private void ApplyBonus(GameObject Subject)
		{
			if (this.MutationWasAdded && Subject.IDMatch(this.MutationWasAddedTo))
			{
				GameObject gameObject = GameObject.FindByID(this.MutationWasAddedTo);
				if (gameObject != null)
				{
					this.UnapplyBonus(gameObject);
				}
			}
			if (!this.MutationWasAdded && this.IsObjectActivePartSubject(Subject))
			{
				this.Tracker = Subject.RequirePart<Mutations>(false).AddMutationMod(this.ClassName, this.Variant, this.Level, Mutations.MutationModifierTracker.SourceType.Equipment, this.ParentObject.DisplayName);
				this.MutationWasAdded = true;
				this.MutationWasAddedTo = Subject.ID;
			}
		}

		// Token: 0x0600A78B RID: 42891 RVA: 0x003D328C File Offset: 0x003D148C
		private void UnapplyBonus(GameObject Subject)
		{
			if (this.MutationWasAdded)
			{
				if (GameObject.Validate(ref Subject) && Subject.IDMatch(this.MutationWasAddedTo))
				{
					Subject.RequirePart<Mutations>(false).RemoveMutationMod(this.Tracker);
				}
				else
				{
					GameObject gameObject = GameObject.FindByID(this.MutationWasAddedTo);
					if (gameObject != null)
					{
						gameObject.RequirePart<Mutations>(false).RemoveMutationMod(this.Tracker);
					}
				}
				this.MutationWasAdded = false;
				this.MutationWasAddedTo = null;
			}
		}

		// Token: 0x0600A78C RID: 42892 RVA: 0x003D32FC File Offset: 0x003D14FC
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

		// Token: 0x0600A78D RID: 42893 RVA: 0x003D3379 File Offset: 0x003D1579
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004522 RID: 17698
		public int Level = 1;

		// Token: 0x04004523 RID: 17699
		public string DisplayName;

		// Token: 0x04004524 RID: 17700
		public string ClassName;

		// Token: 0x04004525 RID: 17701
		public string Variant;

		// Token: 0x04004526 RID: 17702
		public string MutationWasAddedTo;

		// Token: 0x04004527 RID: 17703
		public bool CanLevel;

		// Token: 0x04004528 RID: 17704
		public bool Describe = true;

		// Token: 0x04004529 RID: 17705
		public bool MutationWasAdded;

		// Token: 0x0400452A RID: 17706
		public Guid Tracker;
	}
}
