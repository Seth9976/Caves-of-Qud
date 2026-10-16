using System;
using System.Collections.Generic;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	// Token: 0x02000F50 RID: 3920
	[Serializable]
	public class CyberneticsTreeSkillsoft : IPart
	{
		// Token: 0x06009C73 RID: 40051 RVA: 0x0038BB01 File Offset: 0x00389D01
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C74 RID: 40052 RVA: 0x0038BB04 File Offset: 0x00389D04
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == ObjectCreatedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009C75 RID: 40053 RVA: 0x0038BB34 File Offset: 0x00389D34
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (!this.Skill.IsNullOrEmpty())
			{
				if (this.AddOn)
				{
					E.Add("You gain access to the " + SkillFactory.GetSkillOrPowerName(this.Skill) + " skill tree.");
				}
				else
				{
					E.Description = "You gain access to the " + SkillFactory.GetSkillOrPowerName(this.Skill) + " skill tree.";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C76 RID: 40054 RVA: 0x0038BBA0 File Offset: 0x00389DA0
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.PowersAdded = "";
			foreach (PowerEntry powerEntry in SkillFactory.Factory.SkillByClass[this.Skill].PowerList)
			{
				if (!powerEntry.Class.IsNullOrEmpty())
				{
					E.Implantee.ModIntProperty(CyberneticsTreeSkillsoft.TrackingKey(powerEntry.Class), 1, false);
					if (!E.Implantee.HasPart(powerEntry.Class))
					{
						try
						{
							E.Implantee.AddSkill(powerEntry.Class, null, null);
						}
						catch (Exception ex)
						{
							if (powerEntry.Class != null)
							{
								Logger.gameLog.Error("Invalid Skillsoft skill class: " + powerEntry.Class, Array.Empty<object>());
							}
							Logger.Exception(ex);
						}
						if (this.PowersAdded != "")
						{
							this.PowersAdded += ",";
						}
						this.PowersAdded += powerEntry.Class;
						E.Implantee.SetIntProperty(CyberneticsTreeSkillsoft.DependencyKey(powerEntry.Class), 1, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C77 RID: 40055 RVA: 0x0038BD00 File Offset: 0x00389F00
		public override bool HandleEvent(UnimplantedEvent E)
		{
			foreach (PowerEntry powerEntry in SkillFactory.Factory.SkillByClass[this.Skill].PowerList)
			{
				if (!powerEntry.Class.IsNullOrEmpty())
				{
					E.Implantee.ModIntProperty(CyberneticsTreeSkillsoft.TrackingKey(powerEntry.Class), -1, true);
					if (E.Implantee.GetIntProperty(CyberneticsTreeSkillsoft.DependencyKey(powerEntry.Class), 0) > 0 && E.Implantee.GetIntProperty(CyberneticsTreeSkillsoft.TrackingKey(powerEntry.Class), 0) <= 0)
					{
						E.Implantee.RemoveSkill(powerEntry.Class);
					}
				}
			}
			this.PowersAdded = "";
			return base.HandleEvent(E);
		}

		// Token: 0x06009C78 RID: 40056 RVA: 0x0038BDE0 File Offset: 0x00389FE0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.InitChip(true, false, 1.0);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C79 RID: 40057 RVA: 0x0038BDFA File Offset: 0x00389FFA
		public override void AddedAfterCreation()
		{
			this.InitChip(false, true, 1.0);
			base.AddedAfterCreation();
		}

		// Token: 0x06009C7A RID: 40058 RVA: 0x0038BE13 File Offset: 0x0038A013
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C7B RID: 40059 RVA: 0x0038BE16 File Offset: 0x0038A016
		public static string DependencyKey(string Skill)
		{
			return Skill + "DependsOnSkillsoft";
		}

		// Token: 0x06009C7C RID: 40060 RVA: 0x0038BE23 File Offset: 0x0038A023
		public static string TrackingKey(string Skill)
		{
			return Skill + "Skillsofts";
		}

		// Token: 0x06009C7D RID: 40061 RVA: 0x0038BE30 File Offset: 0x0038A030
		public void InitChip(bool ChangeName = true, bool AddCost = false, double CostFactor = 1.0)
		{
			if (this.Skill == null)
			{
				List<SkillEntry> skillPool = SkillFactory.GetSkillPool(null, null, this.MinCost, this.MaxCost, false, false, true);
				if (skillPool.Count > 0)
				{
					SkillEntry randomElement = skillPool.GetRandomElement<SkillEntry>(null);
					this.Skill = randomElement.Class;
					if (this.ParentObject != null)
					{
						if (ChangeName)
						{
							this.ParentObject.Render.DisplayName = "{{Y|Skillsoft Plus [{{W|" + randomElement.Name + "}}]}}";
						}
						int num = randomElement.Cost;
						foreach (PowerEntry powerEntry in randomElement.PowerList)
						{
							num += powerEntry.Cost;
						}
						int num2 = ((CostFactor == 1.0) ? (num / 100) : ((int)((double)num * CostFactor / 100.0)));
						if (num2 != 0)
						{
							CyberneticsBaseItem part = this.ParentObject.GetPart<CyberneticsBaseItem>();
							if (AddCost)
							{
								part.Cost += num2;
								return;
							}
							part.Cost = num2;
						}
					}
				}
			}
		}

		// Token: 0x040040B7 RID: 16567
		public int MinCost = 1;

		// Token: 0x040040B8 RID: 16568
		public int MaxCost = int.MaxValue;

		// Token: 0x040040B9 RID: 16569
		public string Skill;

		// Token: 0x040040BA RID: 16570
		public bool Applied;

		// Token: 0x040040BB RID: 16571
		public string PowersAdded = "";

		// Token: 0x040040BC RID: 16572
		public bool AddOn;
	}
}
