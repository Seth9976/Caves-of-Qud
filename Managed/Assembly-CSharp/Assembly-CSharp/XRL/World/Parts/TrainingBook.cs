using System;
using XRL.UI;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	// Token: 0x02000E1B RID: 3611
	[Serializable]
	public class TrainingBook : IPart
	{
		// Token: 0x0600930E RID: 37646 RVA: 0x0034FD98 File Offset: 0x0034DF98
		public override bool SameAs(IPart p)
		{
			TrainingBook trainingBook = p as TrainingBook;
			return !(trainingBook.Attribute != this.Attribute) && !(trainingBook.Skill != this.Skill) && base.SameAs(p);
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x0034FDDD File Offset: 0x0034DFDD
		public override void Initialize()
		{
			base.Initialize();
			this.ParentObject.SetStringProperty("BookID", this.ParentObject.ID, false);
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x0034FE01 File Offset: 0x0034E001
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x0034FE3F File Offset: 0x0034E03F
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return !this.HasRead(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x06009312 RID: 37650 RVA: 0x0034FE58 File Offset: 0x0034E058
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (!this.HasRead(E.Actor))
			{
				InventoryActionEvent.Check(this.ParentObject, E.Actor, this.ParentObject, "Read", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009313 RID: 37651 RVA: 0x0034FEA8 File Offset: 0x0034E0A8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.Attribute.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					"Increases the ",
					this.Attribute,
					" of anyone who reads ",
					this.ParentObject.them,
					"."
				}));
			}
			if (!this.Skill.IsNullOrEmpty())
			{
				SkillEntry skillEntry;
				PowerEntry powerEntry;
				if (SkillFactory.Factory.SkillByClass.TryGetValue(this.Skill, out skillEntry))
				{
					if (skillEntry.Initiatory)
					{
						E.Postfix.AppendRules(string.Concat(new string[]
						{
							"Allows anyone who reads ",
							this.ParentObject.them,
							" to initiate themselves in ",
							skillEntry.Name,
							"."
						}));
					}
					else
					{
						E.Postfix.AppendRules(string.Concat(new string[]
						{
							"Teaches ",
							skillEntry.Name,
							" to anyone who reads ",
							this.ParentObject.them,
							"."
						}));
					}
				}
				else if (SkillFactory.Factory.PowersByClass.TryGetValue(this.Skill, out powerEntry))
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						"Teaches ",
						powerEntry.Name,
						" to anyone who reads ",
						this.ParentObject.them,
						"."
					}));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009314 RID: 37652 RVA: 0x00350038 File Offset: 0x0034E238
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, (!this.Skill.IsNullOrEmpty() && !E.Actor.HasSkill(this.Skill)) ? 100 : 15, 0, true, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x00350094 File Offset: 0x0034E294
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read")
			{
				string readKey = this.GetReadKey();
				if (!E.Actor.IsPlayer())
				{
					IComponent<GameObject>.XDidYToZ(E.Actor, "read", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				}
				if (!this.Attribute.IsNullOrEmpty() && !this.HasRead(E.Actor, readKey) && E.Actor.HasStat(this.Attribute))
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("Your " + this.Attribute + " is increased by {{G|1}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					Statistic stat = E.Actor.GetStat(this.Attribute);
					int baseValue = stat.BaseValue;
					stat.BaseValue = baseValue + 1;
				}
				if (!this.Skill.IsNullOrEmpty())
				{
					SkillEntry skillEntry;
					PowerEntry powerEntry;
					if (SkillFactory.Factory.SkillByClass.TryGetValue(this.Skill, out skillEntry))
					{
						if (skillEntry.Initiatory)
						{
							if (!this.HasRead(E.Actor, readKey))
							{
								IBaseSkillEntry skillFor = BaseInitiatorySkill.GetSkillFor(E.Actor, skillEntry);
								if (skillFor == null)
								{
									if (E.Actor.IsPlayer())
									{
										BaseInitiatorySkill.ShowCompletedPopup(E.Actor, this.ParentObject, skillEntry, "TrainingBook");
									}
								}
								else
								{
									E.Actor.AddSkill(skillFor.Class, this.ParentObject, "TrainingBook");
								}
							}
							else if (E.Actor.IsPlayer())
							{
								BaseInitiatorySkill.ShowExpendedPopup(E.Actor, this.ParentObject, skillEntry, "TrainingBook");
							}
						}
						else if (!E.Actor.HasSkill(skillEntry.Class) && !this.HasRead(E.Actor, readKey))
						{
							E.Actor.AddSkill(skillEntry.Class, this.ParentObject, "TrainingBook");
						}
					}
					else if (SkillFactory.Factory.PowersByClass.TryGetValue(this.Skill, out powerEntry) && !E.Actor.HasSkill(powerEntry.Class) && !this.HasRead(E.Actor, readKey))
					{
						E.Actor.AddSkill(powerEntry.Class, this.ParentObject, "TrainingBook");
					}
				}
				E.Actor.SetIntProperty(readKey, 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009316 RID: 37654 RVA: 0x003502FB File Offset: 0x0034E4FB
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Skill.IsNullOrEmpty() && this.Attribute.IsNullOrEmpty())
			{
				this.AssignRandomTraining();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009317 RID: 37655 RVA: 0x00350325 File Offset: 0x0034E525
		public string GetReadKey()
		{
			return "HasReadBook_" + this.ParentObject.GetStringProperty("BookID", null);
		}

		// Token: 0x06009318 RID: 37656 RVA: 0x00350342 File Offset: 0x0034E542
		public bool HasRead(GameObject who, string ReadKey)
		{
			return who.GetIntProperty(ReadKey, 0) > 0;
		}

		// Token: 0x06009319 RID: 37657 RVA: 0x0035034F File Offset: 0x0034E54F
		public bool HasRead(GameObject who)
		{
			return this.HasRead(who, this.GetReadKey());
		}

		// Token: 0x0600931A RID: 37658 RVA: 0x00350360 File Offset: 0x0034E560
		public bool AssignRandomTraining()
		{
			if (70.in100())
			{
				PowerEntry randomElement = SkillFactory.GetPowerPool(null, null, 0, int.MaxValue, false, false, true).GetRandomElement<PowerEntry>(null);
				if (randomElement.ParentSkill != null && (randomElement.Cost <= 0 || randomElement.ParentSkill.Initiatory))
				{
					this.Skill = randomElement.ParentSkill.Class;
				}
				else
				{
					this.Skill = randomElement.Class;
				}
			}
			else
			{
				this.Attribute = Statistic.Attributes.GetRandomElement<string>(null);
			}
			return true;
		}

		// Token: 0x04003CD8 RID: 15576
		public const string SKL_CONTEXT = "TrainingBook";

		// Token: 0x04003CD9 RID: 15577
		public string Attribute;

		// Token: 0x04003CDA RID: 15578
		public string Skill;
	}
}
