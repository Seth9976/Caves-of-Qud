using System;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CE2 RID: 3298
	[Serializable]
	public class ProceduralCookingEffectUnitMutation<T> : ProceduralCookingEffectUnit where T : BaseMutation, new()
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060082F9 RID: 33529 RVA: 0x002F9324 File Offset: 0x002F7524
		// (set) Token: 0x060082FA RID: 33530 RVA: 0x002F9357 File Offset: 0x002F7557
		public string MutationDisplayName
		{
			get
			{
				if (this._MutationDisplayName == null)
				{
					T t = new T();
					this._MutationDisplayName = t.GetDisplayName(true);
				}
				return this._MutationDisplayName;
			}
			set
			{
				this._MutationDisplayName = value;
			}
		}

		// Token: 0x060082FB RID: 33531 RVA: 0x002F9360 File Offset: 0x002F7560
		public override void Init(GameObject target)
		{
			T t = new T();
			this.wasApplied = false;
			this._MutationDisplayName = t.GetDisplayName(true);
			this.ClassName = t.Name;
			this.TrackingProperty = "Equipped" + this.ClassName;
			if (!target.HasPart(typeof(T)))
			{
				this.mutationWasAdded = true;
				this.Tier = this.AddedTier.RollCached();
				return;
			}
			this.Tier = this.BonusTier.RollCached();
		}

		// Token: 0x060082FC RID: 33532 RVA: 0x002F93EF File Offset: 0x002F75EF
		public virtual string GetBonusZeroMessage()
		{
			return "No effect.";
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x002F93F8 File Offset: 0x002F75F8
		public override string GetDescription()
		{
			if (this.mutationWasAdded)
			{
				return string.Concat(new string[]
				{
					"Can use ",
					this.MutationDisplayName,
					" at level ",
					this.Tier.ToString(),
					"."
				});
			}
			if (this.Tier == 1)
			{
				return this.Tier.Signed(false) + " level to " + this.MutationDisplayName + ".";
			}
			if (this.Tier == 0)
			{
				return this.GetBonusZeroMessage();
			}
			return this.Tier.Signed(false) + " levels to " + this.MutationDisplayName + ".";
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x002F94A4 File Offset: 0x002F76A4
		public override string GetTemplatedDescription()
		{
			string text = string.Concat(new string[] { "Can use ", this.MutationDisplayName, " at level ", this.AddedTier, "." });
			if (this.BonusTier != "0")
			{
				text = string.Concat(new string[] { text, " If @they already have ", this.MutationDisplayName, ", it's enhanced by ", this.BonusTier, " levels." });
			}
			return text;
		}

		// Token: 0x060082FF RID: 33535 RVA: 0x002F9534 File Offset: 0x002F7734
		public override void Apply(GameObject go, Effect parent)
		{
			this.wasApplied = true;
			string name = typeof(T).Name;
			if (!go.HasPart(name))
			{
				go.SetIntProperty(this.TrackingProperty, 1, false);
			}
			this.modTracker = go.RequirePart<Mutations>(false).AddMutationMod(name, null, this.Tier, Mutations.MutationModifierTracker.SourceType.Cooking, parent.DisplayName);
			go.RegisterEffectEvent(parent, "BeforeMutationAdded");
			Body body = go.Body;
			if (body != null)
			{
				body.RegenerateDefaultEquipment();
			}
			go.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x06008300 RID: 33536 RVA: 0x002F95B4 File Offset: 0x002F77B4
		public override void Remove(GameObject go, Effect parent)
		{
			if (!this.wasApplied)
			{
				return;
			}
			go.RequirePart<Mutations>(false).RemoveMutationMod(this.modTracker);
			Body body = go.Body;
			if (body != null)
			{
				body.RegenerateDefaultEquipment();
			}
			go.UnregisterEffectEvent(parent, "BeforeMutationAdded");
			go.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x002F95F4 File Offset: 0x002F77F4
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeMutationAdded")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (E.GetStringParameter("Mutation", null) == this.ClassName && gameObjectParameter.HasIntProperty(this.TrackingProperty))
				{
					try
					{
						BaseMutation baseMutation = gameObjectParameter.GetPart<T>();
						if (baseMutation != null)
						{
							baseMutation.Unmutate(gameObjectParameter);
							gameObjectParameter.RemovePart(baseMutation);
						}
						gameObjectParameter.RemoveIntProperty(this.TrackingProperty);
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("Exception on ProceduralCookingEffectUnitMutation during BeforeMutationAdded", ex);
					}
				}
			}
			base.FireEvent(E);
		}

		// Token: 0x0400394A RID: 14666
		public bool wasApplied;

		// Token: 0x0400394B RID: 14667
		public string AddedTier = "1-2";

		// Token: 0x0400394C RID: 14668
		public string BonusTier = "2-3";

		// Token: 0x0400394D RID: 14669
		public int Tier = 1;

		// Token: 0x0400394E RID: 14670
		public int moddedAmount;

		// Token: 0x0400394F RID: 14671
		public string _MutationDisplayName;

		// Token: 0x04003950 RID: 14672
		public string ClassName;

		// Token: 0x04003951 RID: 14673
		public string TrackingProperty;

		// Token: 0x04003952 RID: 14674
		public bool mutationWasAdded;

		// Token: 0x04003953 RID: 14675
		public Guid modTracker;
	}
}
