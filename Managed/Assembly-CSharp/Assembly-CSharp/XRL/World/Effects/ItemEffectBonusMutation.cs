using System;
using UnityEngine;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DAE RID: 3502
	[Serializable]
	public class ItemEffectBonusMutation<T> : Effect where T : BaseMutation, new()
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06008BDE RID: 35806 RVA: 0x0031AA14 File Offset: 0x00318C14
		// (set) Token: 0x06008BDF RID: 35807 RVA: 0x0031AA47 File Offset: 0x00318C47
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

		// Token: 0x06008BE0 RID: 35808 RVA: 0x0031AA50 File Offset: 0x00318C50
		public void Init(GameObject target)
		{
			T t = new T();
			this._MutationDisplayName = t.GetDisplayName(true);
			this.MutationClassName = t.Name;
			this.TrackingProperty = "Equipped" + this.MutationClassName;
			if (!target.HasPart(typeof(T)))
			{
				this.mutationWasAdded = true;
				this.Tier = Stat.Roll(this.AddedTier, null);
				return;
			}
			this.Tier = Stat.Roll(this.BonusTier, null);
		}

		// Token: 0x06008BE1 RID: 35809 RVA: 0x0031AADA File Offset: 0x00318CDA
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008BE2 RID: 35810 RVA: 0x0031AAE0 File Offset: 0x00318CE0
		public override string GetDetails()
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
			return this.Tier.Signed(false) + " levels to " + this.MutationDisplayName + ".";
		}

		// Token: 0x06008BE3 RID: 35811 RVA: 0x0031AB7C File Offset: 0x00318D7C
		public override bool Apply(GameObject go)
		{
			this.Init(go);
			this.Duration = 3;
			if (!go.HasPart(typeof(T)))
			{
				T t = new T();
				if (!t.CompatibleWith(go))
				{
					return false;
				}
				this.wasApplied = true;
				this.mutationWasAdded = true;
				this.moddedAmount = this.Tier;
				t.ParentObject = go;
				t.Mutate(go, 0);
				go.AddPart<T>(t, true, false);
				go.GetPart<T>().Level = 0;
				go.ModIntProperty(this.MutationClassName, this.Tier, false);
				go.ModIntProperty(this.TrackingProperty, 1, false);
			}
			else
			{
				this.mutationWasAdded = false;
				this.wasApplied = true;
				if (go.HasIntProperty(this.TrackingProperty))
				{
					this.moddedAmount = this.Tier;
					go.ModIntProperty(this.TrackingProperty, 1, false);
					go.ModIntProperty(typeof(T).Name, this.Tier, false);
				}
				else
				{
					this.moddedAmount = this.Tier;
					go.ModIntProperty(typeof(T).Name, this.Tier, false);
				}
			}
			go.RegisterEffectEvent(this, "BeforeMutationAdded");
			go.SyncMutationLevelAndGlimmer();
			return true;
		}

		// Token: 0x06008BE4 RID: 35812 RVA: 0x0031ACC8 File Offset: 0x00318EC8
		public override void Remove(GameObject go)
		{
			if (!this.wasApplied)
			{
				return;
			}
			go.ModIntProperty(typeof(T).Name, -this.moddedAmount, false);
			this.moddedAmount = 0;
			if (go.HasIntProperty(this.TrackingProperty))
			{
				go.ModIntProperty(this.TrackingProperty, -1, true);
				if (go.GetIntProperty(this.TrackingProperty, 0) <= 0 && go.HasPart(typeof(T)))
				{
					go.GetPart<T>().Unmutate(go);
					go.RemovePart(typeof(T));
				}
			}
			go.UnregisterEffectEvent(this, "BeforeMutationAdded");
			go.SyncMutationLevelAndGlimmer();
			this.wasApplied = false;
		}

		// Token: 0x06008BE5 RID: 35813 RVA: 0x0031AD80 File Offset: 0x00318F80
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeMutationAdded")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (E.GetStringParameter("Mutation", null) == this.MutationClassName && gameObjectParameter.HasIntProperty(this.TrackingProperty))
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
						Debug.LogError("Exception on ProceduralCookingEffectUnitMutation during BeforeMutationAdded: " + ex.ToString());
					}
				}
			}
			return true;
		}

		// Token: 0x04003ADC RID: 15068
		public bool wasApplied;

		// Token: 0x04003ADD RID: 15069
		public string AddedTier = "1";

		// Token: 0x04003ADE RID: 15070
		public string BonusTier = "0";

		// Token: 0x04003ADF RID: 15071
		public int Tier = 1;

		// Token: 0x04003AE0 RID: 15072
		public int moddedAmount;

		// Token: 0x04003AE1 RID: 15073
		public string _MutationDisplayName;

		// Token: 0x04003AE2 RID: 15074
		public string MutationClassName;

		// Token: 0x04003AE3 RID: 15075
		public string TrackingProperty;

		// Token: 0x04003AE4 RID: 15076
		public bool mutationWasAdded;
	}
}
