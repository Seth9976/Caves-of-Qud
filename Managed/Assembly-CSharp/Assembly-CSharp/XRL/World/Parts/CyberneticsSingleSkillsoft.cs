using System;
using System.Collections.Generic;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	// Token: 0x02000F45 RID: 3909
	[Serializable]
	public class CyberneticsSingleSkillsoft : IPart
	{
		// Token: 0x06009BED RID: 39917 RVA: 0x00388C77 File Offset: 0x00386E77
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BEE RID: 39918 RVA: 0x00388C7A File Offset: 0x00386E7A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == ObjectCreatedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BEF RID: 39919 RVA: 0x00388CA8 File Offset: 0x00386EA8
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (!this.Skill.IsNullOrEmpty())
			{
				if (this.AddOn)
				{
					E.Add("You gain the skill " + SkillFactory.GetSkillOrPowerName(this.Skill) + ".");
				}
				else
				{
					E.Description = "You gain the skill " + SkillFactory.GetSkillOrPowerName(this.Skill) + ".";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BF0 RID: 39920 RVA: 0x00388D14 File Offset: 0x00386F14
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (!E.Implantee.HasPart(this.Skill))
			{
				E.Implantee.AddSkill(this.Skill, null, null);
				E.Implantee.SetIntProperty(this.DependencyKey(), 1, false);
				this.Applied = true;
			}
			E.Implantee.ModIntProperty(this.TrackingKey(), 1, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BF1 RID: 39921 RVA: 0x00388D80 File Offset: 0x00386F80
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.ModIntProperty(this.TrackingKey(), -1, true);
			if (E.Implantee.GetIntProperty(this.DependencyKey(), 0) > 0 && E.Implantee.GetIntProperty(this.TrackingKey(), 0) <= 0)
			{
				GameObject implantee = E.Implantee;
				if (implantee != null)
				{
					implantee.RemoveSkill(this.Skill);
				}
			}
			this.Applied = false;
			return base.HandleEvent(E);
		}

		// Token: 0x06009BF2 RID: 39922 RVA: 0x00388DF0 File Offset: 0x00386FF0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.InitChip(true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BF3 RID: 39923 RVA: 0x00388E00 File Offset: 0x00387000
		public override void AddedAfterCreation()
		{
			this.InitChip(false);
			base.AddedAfterCreation();
		}

		// Token: 0x06009BF4 RID: 39924 RVA: 0x00388E0F File Offset: 0x0038700F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009BF5 RID: 39925 RVA: 0x00388E12 File Offset: 0x00387012
		public string DependencyKey()
		{
			return CyberneticsTreeSkillsoft.DependencyKey(this.Skill);
		}

		// Token: 0x06009BF6 RID: 39926 RVA: 0x00388E1F File Offset: 0x0038701F
		public string TrackingKey()
		{
			return CyberneticsTreeSkillsoft.TrackingKey(this.Skill);
		}

		// Token: 0x06009BF7 RID: 39927 RVA: 0x00388E2C File Offset: 0x0038702C
		public void InitChip(bool ChangeName = true)
		{
			if (this.Skill == null)
			{
				List<PowerEntry> powerPool = SkillFactory.GetPowerPool(null, null, this.MinCost, this.MaxCost, false, false, true);
				if (powerPool.Count > 0)
				{
					PowerEntry randomElement = powerPool.GetRandomElement<PowerEntry>(null);
					if (ChangeName && this.ParentObject != null)
					{
						this.ParentObject.Render.DisplayName = "{{Y|Skillsoft [{{W|" + randomElement.Name + "}}]}}";
					}
					this.Skill = randomElement.Class;
				}
			}
		}

		// Token: 0x04004094 RID: 16532
		public int MinCost;

		// Token: 0x04004095 RID: 16533
		public int MaxCost = 50;

		// Token: 0x04004096 RID: 16534
		public string Skill;

		// Token: 0x04004097 RID: 16535
		public bool Applied;

		// Token: 0x04004098 RID: 16536
		public bool AddOn;
	}
}
