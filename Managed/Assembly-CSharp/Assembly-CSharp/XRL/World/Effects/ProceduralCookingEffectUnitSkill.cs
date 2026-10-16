using System;
using XRL.Rules;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Effects
{
	// Token: 0x02000CE3 RID: 3299
	[Serializable]
	public class ProceduralCookingEffectUnitSkill<T> : ProceduralCookingEffectUnit where T : BaseSkill, new()
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06008303 RID: 33539 RVA: 0x002F96C0 File Offset: 0x002F78C0
		// (set) Token: 0x06008304 RID: 33540 RVA: 0x002F9706 File Offset: 0x002F7906
		public string SkillDisplayName
		{
			get
			{
				if (this._SkillDisplayName == null)
				{
					T t = new T();
					this._SkillDisplayName = SkillFactory.Factory.PowersByClass[t.Name].Name;
				}
				return this._SkillDisplayName;
			}
			set
			{
				this._SkillDisplayName = value;
			}
		}

		// Token: 0x06008305 RID: 33541 RVA: 0x002F9710 File Offset: 0x002F7910
		public override void Init(GameObject target)
		{
			T t = new T();
			this.wasApplied = false;
			this._SkillDisplayName = SkillFactory.Factory.PowersByClass[t.Name].Name;
			this.TrackingProperty = "Equipped" + typeof(T).Name;
			if (!target.HasPart(typeof(T)))
			{
				this.skillWasAdded = true;
				this.Tier = Stat.Roll(this.AddedTier, null);
				return;
			}
			this.Tier = Stat.Roll(this.BonusTier, null);
		}

		// Token: 0x06008306 RID: 33542 RVA: 0x002F97AC File Offset: 0x002F79AC
		public override string GetDescription()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008307 RID: 33543 RVA: 0x002F97B3 File Offset: 0x002F79B3
		public override string GetTemplatedDescription()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008308 RID: 33544 RVA: 0x002F97BC File Offset: 0x002F79BC
		public override void Apply(GameObject go, Effect parent)
		{
			if (!go.HasPart(typeof(T)))
			{
				T t = new T();
				this.wasApplied = true;
				this.skillWasAdded = true;
				this.moddedAmount = this.Tier;
				t.ParentObject = go;
				t.AddSkill(go);
				go.AddPart<T>(t, true, false);
				go.ModIntProperty(typeof(T).Name, this.Tier, false);
				go.ModIntProperty(this.TrackingProperty, 1, false);
			}
			else
			{
				this.skillWasAdded = false;
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
			go.RegisterEffectEvent(parent, "AfterAddSkill");
			go.RegisterEffectEvent(parent, "BeforeAddSkill");
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x002F98E4 File Offset: 0x002F7AE4
		public override void Remove(GameObject go, Effect parent)
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
					go.GetPart<T>().RemoveSkill(go);
					go.RemovePart(typeof(T));
				}
			}
			go.UnregisterEffectEvent(parent, "AfterAddSkill");
			go.UnregisterEffectEvent(parent, "BeforeAddSkill");
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x002F999C File Offset: 0x002F7B9C
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeAddSkill")
			{
				if (E.GetParameter("Skill") is T)
				{
					this.Remove(E.GetGameObjectParameter("Actor"), this.parent);
					return;
				}
			}
			else if (E.ID == "AfterAddSkill" && E.GetParameter("Skill") is T)
			{
				this.Apply(E.GetGameObjectParameter("Actor"), this.parent);
			}
		}

		// Token: 0x04003954 RID: 14676
		public bool wasApplied;

		// Token: 0x04003955 RID: 14677
		public string AddedTier = "1-2";

		// Token: 0x04003956 RID: 14678
		public string BonusTier = "1";

		// Token: 0x04003957 RID: 14679
		public int Tier = 1;

		// Token: 0x04003958 RID: 14680
		public int moddedAmount;

		// Token: 0x04003959 RID: 14681
		public string _SkillDisplayName;

		// Token: 0x0400395A RID: 14682
		public string TrackingProperty;

		// Token: 0x0400395B RID: 14683
		public bool skillWasAdded;
	}
}
