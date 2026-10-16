using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	/// <remarks>
	/// This part is not used in the base game.
	///
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, chance to activate is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400, and damage is increased
	/// by the standard power load bonus, i.e. 2 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x02000FFD RID: 4093
	[Serializable]
	public class AutomatedExternalDefibrillator : IPoweredPart
	{
		// Token: 0x0600A316 RID: 41750 RVA: 0x003B9AB0 File Offset: 0x003B7CB0
		public AutomatedExternalDefibrillator()
		{
			this.ChargeUse = 500;
			this.IsPowerLoadSensitive = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A317 RID: 41751 RVA: 0x003B9B08 File Offset: 0x003B7D08
		public override bool SameAs(IPart p)
		{
			AutomatedExternalDefibrillator automatedExternalDefibrillator = p as AutomatedExternalDefibrillator;
			return automatedExternalDefibrillator.Chance == this.Chance && !(automatedExternalDefibrillator.Damage != this.Damage) && !(automatedExternalDefibrillator.DamageAttributes != this.DamageAttributes) && !(automatedExternalDefibrillator.RequireSkill != this.RequireSkill) && base.SameAs(p);
		}

		// Token: 0x0600A318 RID: 41752 RVA: 0x003B9B72 File Offset: 0x003B7D72
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A319 RID: 41753 RVA: 0x003B9BA8 File Offset: 0x003B7DA8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Defibrillate", "activate", AutomatedExternalDefibrillator.COMMAND_NAME, null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A31A RID: 41754 RVA: 0x003B9BDF File Offset: 0x003B7DDF
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == AutomatedExternalDefibrillator.COMMAND_NAME)
			{
				this.AttemptDefibrillate(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A31B RID: 41755 RVA: 0x003B9C08 File Offset: 0x003B7E08
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.RequireSkill.IsNullOrEmpty())
			{
				int num = this.MyPowerLoadLevel();
				int effectiveChance = this.GetEffectiveChance(new int?(num));
				if (effectiveChance >= 100)
				{
					E.Postfix.AppendRules("Stops cardiac arrest.");
				}
				else if (effectiveChance > 0)
				{
					E.Postfix.AppendRules(effectiveChance.ToString() + "% chance to stop cardiac arrest.");
				}
				if (!this.Damage.IsNullOrEmpty())
				{
					E.Postfix.AppendRules("Does " + this.GetEffectiveDamage(new int?(num)) + ((!this.DamageAttributes.IsNullOrEmpty()) ? (" " + this.DamageAttributes.ToLower()) : "") + " damage per application.");
				}
				if (!this.RequireSkill.IsNullOrEmpty())
				{
					E.Postfix.AppendRules("Requires training in " + SkillFactory.GetSkillOrPowerName(this.RequireSkill) + " to use.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A31C RID: 41756 RVA: 0x003B9D09 File Offset: 0x003B7F09
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A31D RID: 41757 RVA: 0x003B9D1F File Offset: 0x003B7F1F
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A31E RID: 41758 RVA: 0x003B9D38 File Offset: 0x003B7F38
		public bool AttemptDefibrillate(GameObject Actor, IEvent FromEvent = null)
		{
			if (!Actor.CanMoveExtremities("Defibrillate", true, false, true))
			{
				return false;
			}
			if (!this.RequireSkill.IsNullOrEmpty() && !Actor.HasSkill(this.RequireSkill))
			{
				return Actor.Fail("You don't know how to use " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
			}
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
			}
			Cell cell = Actor.PickDirection(null);
			if (cell == null)
			{
				return false;
			}
			GameObject gameObject = AutomatedExternalDefibrillator.FindOptimalTarget(cell, Actor);
			if (gameObject == null)
			{
				gameObject = AutomatedExternalDefibrillator.FindAlternateTarget(cell, Actor);
				if (gameObject == null)
				{
					if (Actor.IsPlayer())
					{
						if (cell.GetCombatTarget(Actor, true, false, false, 5, null, null, null, null, null, true, false, null) == null)
						{
							Actor.Fail("There is no one there to use " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " on.");
						}
						else
						{
							Actor.Fail("There is no one there you can use " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " on.");
						}
					}
					return false;
				}
				string text;
				if (gameObject == Actor)
				{
					text = string.Concat(new string[]
					{
						"You are not in cardiac arrest. Do you want to use ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" on ",
						Actor.itself,
						" anyway?"
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						gameObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						gameObject.Is,
						" not in cardiac arrest. Do you want to use ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" on ",
						gameObject.them,
						" anyway?"
					});
				}
				if (Popup.ShowYesNo(text, "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return false;
				}
			}
			num3 = new int?(num);
			base.ConsumeCharge(null, num3);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			int num4 = 1000;
			string text2 = "Item Medical Defibrillator";
			string text3 = null;
			num3 = null;
			Actor.UseEnergy(num4, text2, text3, num3, false);
			if (gameObject != Actor && gameObject.IsHostileTowards(Actor) && gameObject.IsMobile())
			{
				int combatDV = Stats.GetCombatDV(gameObject);
				if (Stat.Random(1, 20) + Actor.StatMod("Agility", 0) < combatDV)
				{
					IComponent<GameObject>.WDidXToYWithZ(Actor, "try", "to use", this.ParentObject, "on", gameObject, ", but " + gameObject.it + gameObject.GetVerb("dodge", true, true, false, true, false), "!", null, null, null, Actor, false, false, false, false, false, false, false, false, null, null, null, null, false, false, false, true, false, null);
					return false;
				}
			}
			IComponent<GameObject>.WDidXToYWithZ(Actor, "use", this.ParentObject, "on", gameObject, null, null, null, null, null, null, false, false, false, false, false, false, false, false, null, null, null, null, false, false, false, true, false, null);
			if (!this.Damage.IsNullOrEmpty())
			{
				GameObject gameObject2 = gameObject;
				int num5 = this.GetEffectiveDamage(new int?(num)).RollCached();
				string text4 = "from the device.";
				string damageAttributes = this.DamageAttributes;
				string text5 = null;
				string text6 = null;
				GameObject gameObject3 = null;
				bool flag11 = gameObject.HasEffect<CardiacArrest>();
				gameObject2.TakeDamage(num5, text4, damageAttributes, text5, text6, gameObject3, Actor, null, null, null, flag11, false, false, false, false, false, false, false, false, 5, null);
			}
			if (GameObject.Validate(ref gameObject) && gameObject.HasEffect<CardiacArrest>() && this.GetEffectiveChance(new int?(num)).in100())
			{
				gameObject.RemoveEffect<CardiacArrest>();
			}
			return true;
		}

		// Token: 0x0600A31F RID: 41759 RVA: 0x003BA17C File Offset: 0x003B837C
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				int num = this.MyPowerLoadLevel();
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					int num4 = 1 + IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
					int num5 = (3 + IComponent<GameObject>.PowerLoadBonus(num, 100, 150)) * 3 / 2;
					int num6 = Stat.Random(num4, num5);
					string text = num4.ToString() + "-" + num5.ToString();
					GameObject parentObject = this.ParentObject;
					Cell cell = null;
					GameObject actor = E.Actor;
					if (parentObject.Discharge(cell, num6, 0, text, null, E.Actor, this.ParentObject, actor, this.ParentObject, null, null, null, 0, false, false, E.Actor.IsPlayer()) > 0)
					{
						E.Identify = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600A320 RID: 41760 RVA: 0x003BA278 File Offset: 0x003B8478
		private static GameObject FindOptimalTarget(Cell C, GameObject Subject)
		{
			foreach (GameObject gameObject in C.Objects)
			{
				if (gameObject.HasEffect<CardiacArrest>() && Subject.PhaseAndFlightMatches(gameObject))
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x0600A321 RID: 41761 RVA: 0x003BA2DC File Offset: 0x003B84DC
		private static GameObject FindAlternateTarget(Cell C, GameObject Subject)
		{
			return C.GetCombatTarget(Subject, false, false, false, 0, null, null, null, null, null, true, false, null);
		}

		// Token: 0x0600A322 RID: 41762 RVA: 0x003BA2FC File Offset: 0x003B84FC
		public int GetEffectiveChance(int? PowerLoadLevel = null)
		{
			int num = this.Chance;
			int num2 = IComponent<GameObject>.PowerLoadBonus(PowerLoadLevel ?? this.MyPowerLoadLevel(), 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return num;
		}

		// Token: 0x0600A323 RID: 41763 RVA: 0x003BA344 File Offset: 0x003B8544
		public string GetEffectiveDamage(int? PowerLoadLevel = null)
		{
			string text = this.Damage;
			int num = IComponent<GameObject>.PowerLoadBonus(PowerLoadLevel ?? this.MyPowerLoadLevel(), 100, 150);
			if (num != 0)
			{
				text += num.Signed(false);
			}
			return text;
		}

		// Token: 0x04004390 RID: 17296
		public static readonly string COMMAND_NAME = "Defibrillate";

		// Token: 0x04004391 RID: 17297
		public int Chance = 50;

		// Token: 0x04004392 RID: 17298
		public string Damage = "1d4";

		// Token: 0x04004393 RID: 17299
		public string DamageAttributes = "Electric";

		// Token: 0x04004394 RID: 17300
		public string RequireSkill = "Physic";
	}
}
