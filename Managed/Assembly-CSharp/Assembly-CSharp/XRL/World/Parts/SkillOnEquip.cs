using System;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	/// <summary>This class is not used in the base game.</summary>
	/// <remarks>There is currently no concept of temporary skills, granted skill will function as prerequisite.</remarks>
	// Token: 0x02001096 RID: 4246
	[Serializable]
	public class SkillOnEquip : IActivePart
	{
		// Token: 0x0600A8F6 RID: 43254 RVA: 0x003DCDCF File Offset: 0x003DAFCF
		public SkillOnEquip()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnWearer = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A8F7 RID: 43255 RVA: 0x003DCE04 File Offset: 0x003DB004
		public override void Initialize()
		{
			if (!this.ClassName.IsNullOrEmpty())
			{
				BaseSkill genericSkill = Skills.GetGenericSkill(this.ClassName, null);
				if (genericSkill != null)
				{
					if (this.DisplayName == null)
					{
						this.DisplayName = genericSkill.DisplayName;
					}
					return;
				}
			}
			if (this.DisplayName == null)
			{
				this.DisplayName = SkillFactory.GetSkillOrPowerName(this.ClassName);
			}
		}

		// Token: 0x0600A8F8 RID: 43256 RVA: 0x003DCE5C File Offset: 0x003DB05C
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			SkillOnEquip skillOnEquip = base.DeepCopy(Parent, MapInv) as SkillOnEquip;
			skillOnEquip.AddedID = 0;
			return skillOnEquip;
		}

		// Token: 0x0600A8F9 RID: 43257 RVA: 0x003DCE72 File Offset: 0x003DB072
		public override bool SameAs(IPart Part)
		{
			return false;
		}

		// Token: 0x0600A8FA RID: 43258 RVA: 0x003DCE78 File Offset: 0x003DB078
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && this.Describe) || ID == PowerSwitchFlippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A8FB RID: 43259 RVA: 0x003DCEE9 File Offset: 0x003DB0E9
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8FC RID: 43260 RVA: 0x003DCF04 File Offset: 0x003DB104
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8FD RID: 43261 RVA: 0x003DCF1F File Offset: 0x003DB11F
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8FE RID: 43262 RVA: 0x003DCF3A File Offset: 0x003DB13A
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8FF RID: 43263 RVA: 0x003DCF55 File Offset: 0x003DB155
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A900 RID: 43264 RVA: 0x003DCF70 File Offset: 0x003DB170
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A901 RID: 43265 RVA: 0x003DCF8B File Offset: 0x003DB18B
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckApplyBonus(this.ParentObject.Equipped, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A902 RID: 43266 RVA: 0x003DCFA6 File Offset: 0x003DB1A6
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyBonus(E.Actor, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A903 RID: 43267 RVA: 0x003DCFBC File Offset: 0x003DB1BC
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyBonus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A904 RID: 43268 RVA: 0x003DCFD1 File Offset: 0x003DB1D1
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Grants you " + this.DisplayName + ".");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A905 RID: 43269 RVA: 0x003DCFFC File Offset: 0x003DB1FC
		private void ApplyBonus(GameObject Subject)
		{
			if (this.AddedID > 0 && Subject.IDMatch(this.AddedID))
			{
				return;
			}
			this.UnapplyBonus(Subject);
			if (this.IsObjectActivePartSubject(Subject) && !Subject.HasPart(this.ClassName))
			{
				Subject.AddSkill(this.ClassName, this.ParentObject, "SkillOnEquip");
				this.AddedID = Subject.BaseID;
			}
		}

		// Token: 0x0600A906 RID: 43270 RVA: 0x003DD064 File Offset: 0x003DB264
		private void UnapplyBonus(GameObject Subject)
		{
			if (this.AddedID > 0)
			{
				if (GameObject.Validate(ref Subject) && Subject.IDMatch(this.AddedID))
				{
					Subject.RemoveSkill(this.ClassName);
				}
				else
				{
					GameObject gameObject = GameObject.FindByID(this.AddedID);
					if (gameObject != null)
					{
						gameObject.RemoveSkill(this.ClassName);
					}
				}
				this.AddedID = 0;
			}
		}

		// Token: 0x0600A907 RID: 43271 RVA: 0x003DD0C4 File Offset: 0x003DB2C4
		public void CheckApplyBonus(GameObject Subject, bool UseCharge = false)
		{
			if (this.AddedID > 0)
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

		// Token: 0x0600A908 RID: 43272 RVA: 0x003DD142 File Offset: 0x003DB342
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040045CA RID: 17866
		public string DisplayName;

		// Token: 0x040045CB RID: 17867
		public string ClassName;

		// Token: 0x040045CC RID: 17868
		public int AddedID;

		// Token: 0x040045CD RID: 17869
		public bool Describe = true;
	}
}
