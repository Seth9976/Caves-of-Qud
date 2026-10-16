using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001452 RID: 5202
	[Serializable]
	public class Quills : BaseMutation
	{
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600CB65 RID: 52069 RVA: 0x0049F221 File Offset: 0x0049D421
		// (set) Token: 0x0600CB66 RID: 52070 RVA: 0x0049F22C File Offset: 0x0049D42C
		public int nQuills
		{
			get
			{
				return this._nQuills;
			}
			set
			{
				if (value > this.nMaxQuills)
				{
					value = this.nMaxQuills;
				}
				if (this._nQuills != value)
				{
					this._nQuills = value;
					if (this._nQuills >= this.nMaxQuills / 2)
					{
						if (this.nPenalty > 0)
						{
							base.StatShifter.RemoveStatShift(this.ParentObject, "AV", false);
							this.nPenalty = 0;
						}
					}
					else if (this.nPenalty == 0)
					{
						this.nPenalty = this.GetAVPenalty(base.Level);
						base.StatShifter.SetStatShift(this.ParentObject, "AV", -this.nPenalty, false);
					}
					StringBuilder stringBuilder = Event.NewStringBuilder(this.AbilityName).Append(" [").Append(this.nQuills)
						.Append(' ')
						.Append((this.nQuills == 1) ? this.ObjectNameSingular : this.ObjectName)
						.Append(" left]");
					base.SetMyActivatedAbilityDisplayName(this.QuillFlingActivatedAbilityID, Event.FinalizeString(stringBuilder), null);
				}
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600CB67 RID: 52071 RVA: 0x0049F331 File Offset: 0x0049D531
		public string BlueprintName
		{
			get
			{
				return this.Variant.Coalesce("Quills");
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600CB68 RID: 52072 RVA: 0x0049F344 File Offset: 0x0049D544
		public string AbilityName
		{
			get
			{
				string text;
				if (!this.Blueprint.Tags.TryGetValue("AbilityName", out text))
				{
					return "Quill Fling";
				}
				return text;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600CB69 RID: 52073 RVA: 0x0049F371 File Offset: 0x0049D571
		public string ObjectName
		{
			get
			{
				return this.Blueprint.CachedDisplayNameStripped ?? "quills";
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600CB6A RID: 52074 RVA: 0x0049F388 File Offset: 0x0049D588
		public string ObjectNameSingular
		{
			get
			{
				string text;
				if (!this.Blueprint.Tags.TryGetValue("DisplayNameSingular", out text))
				{
					return "quill";
				}
				return text;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600CB6B RID: 52075 RVA: 0x0049F3B5 File Offset: 0x0049D5B5
		public GameObjectBlueprint Blueprint
		{
			get
			{
				if (this._Blueprint == null)
				{
					this._Blueprint = GameObjectFactory.Factory.GetBlueprint(this.BlueprintName);
				}
				return this._Blueprint;
			}
		}

		// Token: 0x0600CB6D RID: 52077 RVA: 0x0049F400 File Offset: 0x0049D600
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Quills", this.ObjectName, false, 0);
			stats.Set("RegenRate", this.GetRegenRate(Level).ToString(), false, 0);
			stats.Set("AVPenalty", this.GetAVPenalty(Level), false, 0);
			stats.Set("QuillPen", Grammar.InitCap(this.ObjectNameSingular) + " penetration: " + this.GetQuillPenetration(Level).ToString(), false, 0);
			stats.Set("QuillDamage", Grammar.InitCap(this.ObjectNameSingular) + " damage: 1d3", false, 0);
		}

		// Token: 0x0600CB6E RID: 52078 RVA: 0x0049F4A3 File Offset: 0x0049D6A3
		public float GetRegenRate(int Level)
		{
			return (float)Level / 4f;
		}

		// Token: 0x0600CB6F RID: 52079 RVA: 0x0049F4AD File Offset: 0x0049D6AD
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CB70 RID: 52080 RVA: 0x0049F4B0 File Offset: 0x0049D6B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == TookDamageEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600CB71 RID: 52081 RVA: 0x0049F4EE File Offset: 0x0049D6EE
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.QuillFlingActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB72 RID: 52082 RVA: 0x0049F514 File Offset: 0x0049D714
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.nQuills >= 80 && (double)this.nQuills > (double)this.nMaxQuills * 0.65 && (E.Distance <= 1 || this.ParentObject.HasEffect<Engulfed>()) && base.IsMyActivatedAbilityAIUsable(this.QuillFlingActivatedAbilityID, null) && E.Target != null && E.Target.IsCombatObject())
			{
				int num = 1;
				int num2 = 1;
				if (!this.ParentObject.HasEffect<Engulfed>())
				{
					foreach (Cell cell in E.Actor.CurrentCell.GetAdjacentCells(true))
					{
						GameObject combatTarget = cell.GetCombatTarget(E.Actor, true, false, false, 0, null, null, null, null, null, true, false, null);
						if (combatTarget != null && combatTarget != E.Target)
						{
							if (combatTarget.Brain != null)
							{
								if (E.Actor.IsHostileTowards(combatTarget))
								{
									num++;
								}
								else
								{
									num2++;
									if (combatTarget.isDamaged(0.1, false))
									{
										num2++;
									}
								}
							}
							else if (!OkayToDamageEvent.Check(combatTarget, E.Actor))
							{
								num2++;
							}
						}
					}
				}
				if ((25 * num / num2).in100())
				{
					E.Add(Quills.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB73 RID: 52083 RVA: 0x0049F684 File Offset: 0x0049D884
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			Damage damage = E.Damage;
			return (damage == null || !damage.HasAttribute("Quills")) && base.HandleEvent(E);
		}

		// Token: 0x0600CB74 RID: 52084 RVA: 0x0049F6A8 File Offset: 0x0049D8A8
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Actor != null && E.Actor != this.ParentObject && !this.ParentObject.OnWorldMap() && !E.Actor.HasPart<Quills>() && E.Damage.Amount > 0 && !E.Damage.HasAttribute("reflected") && E.Damage.HasAttribute("Unarmed"))
			{
				int num = (int)((double)this.nQuills * 0.01) + Stat.Random(1, 2) - 1;
				this.nQuills -= num;
				if (num > 0)
				{
					int num2 = (int)((float)E.Damage.Amount * ((float)(num * 3) / 100f));
					if (num2 == 0)
					{
						num2 = 1;
					}
					if (num2 > 0)
					{
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								E.Actor.Does("impale", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" ",
								E.Actor.itself,
								" on ",
								this.ParentObject.poss(this.QuillsObject, true, null),
								" and",
								E.Actor.GetVerb("take", true, false, false, true, false),
								" ",
								num2.ToString(),
								" damage!"
							}), 'G', true);
						}
						else if (E.Actor != null)
						{
							if (E.Actor.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
								{
									"You impale ",
									E.Actor.itself,
									" on ",
									this.ParentObject.poss(this.QuillsObject, true, null),
									" and take ",
									num2.ToString(),
									" damage!"
								}), 'R', true);
							}
							else if (IComponent<GameObject>.Visible(E.Actor))
							{
								if (E.Actor.IsPlayerLed())
								{
									IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
									{
										E.Actor.Does("impale", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
										" ",
										E.Actor.itself,
										" on ",
										this.ParentObject.poss(this.QuillsObject, true, null),
										" and",
										E.Actor.GetVerb("take", true, false, false, true, false),
										" ",
										num2.ToString(),
										" damage!"
									}), 'r', true);
								}
								else
								{
									IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
									{
										E.Actor.Does("impale", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
										" ",
										E.Actor.itself,
										" on ",
										this.ParentObject.poss(this.QuillsObject, true, null),
										" and",
										E.Actor.GetVerb("take", true, false, false, true, false),
										" ",
										num2.ToString(),
										" damage!"
									}), 'g', true);
								}
							}
						}
						Event @event = new Event("TakeDamage");
						Damage damage = new Damage(num2);
						damage.Attributes = new List<string>(E.Damage.Attributes);
						if (!damage.HasAttribute("reflected"))
						{
							damage.Attributes.Add("reflected");
						}
						@event.SetParameter("Damage", damage);
						@event.SetParameter("Owner", this.ParentObject);
						@event.SetParameter("Attacker", this.ParentObject);
						@event.SetParameter("Message", null);
						E.Actor.FireEvent(@event);
						this.ParentObject.FireEvent("ReflectedDamage");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB75 RID: 52085 RVA: 0x0049FB30 File Offset: 0x0049DD30
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			float num = (float)base.Level;
			float num2 = ((float)this.ParentObject.Stat("Willpower", 0) - 16f) * 0.05f;
			float num3 = 1f - num2;
			if ((double)num3 <= 0.2)
			{
				num3 = 0.2f;
			}
			if (num2 < 1f)
			{
				num *= 1f / num3;
			}
			this.QuillRegenerationCounter += num;
			if (this.QuillRegenerationCounter >= 4f)
			{
				int num4 = (int)(this.QuillRegenerationCounter / 4f);
				this.nQuills += num4;
				this.QuillRegenerationCounter -= (float)(4 * num4);
			}
			if (this.nQuills > this.nMaxQuills)
			{
				this.nQuills = this.nMaxQuills;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB76 RID: 52086 RVA: 0x0049FBFC File Offset: 0x0049DDFC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Quills.COMMAND_NAME)
			{
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return false;
				}
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				string objectName = this.ObjectName;
				if (this.nQuills < 80)
				{
					return this.ParentObject.Fail(string.Format("You don't have enough {0}! You need at least {1} {2} to {3}.", new object[] { objectName, 80, objectName, this.AbilityName }));
				}
				Engulfed effect = this.ParentObject.GetEffect<Engulfed>((Engulfed sfx) => sfx.IsEngulfedByValid());
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_quills_expel", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (effect != null)
				{
					int num = (int)((double)this.nQuills * 0.1);
					if (num <= 0)
					{
						return false;
					}
					GameObject engulfedBy = effect.EngulfedBy;
					base.DidX("fling", this.ParentObject.its + " " + objectName, "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					this.QuillFling(engulfedBy.CurrentCell, num, true, false, engulfedBy);
				}
				else
				{
					List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
					if (adjacentCells.Count <= 0)
					{
						return false;
					}
					int num2 = (int)((double)this.nQuills * 0.1) / adjacentCells.Count;
					if (num2 <= 0)
					{
						return false;
					}
					base.DidX("fling", this.ParentObject.its + " " + objectName + " everywhere", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					foreach (Cell cell in adjacentCells)
					{
						this.QuillFling(cell, num2, true, false, null);
					}
				}
				base.UseEnergy(1000, "Physical Mutation Quills");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB77 RID: 52087 RVA: 0x0049FE48 File Offset: 0x0049E048
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DefenderHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CB78 RID: 52088 RVA: 0x0049FE60 File Offset: 0x0049E060
		public void QuillFling(Cell Cell, int Quills, bool UseQuills = true, bool Reactive = false, GameObject Target = null)
		{
			if (Cell == null || Cell.OnWorldMap())
			{
				return;
			}
			if (UseQuills)
			{
				if (Quills > this.nQuills)
				{
					return;
				}
				this.nQuills -= Quills;
				if (this.nQuills < 0)
				{
					this.nQuills = 0;
				}
			}
			bool flag = Cell.IsVisible();
			if (Target == null)
			{
				Target = Cell.GetCombatTarget(this.ParentObject, true, false, false, 0, null, null, null, null, null, true, false, null);
				if (Target == null)
				{
					return;
				}
			}
			int num = 0;
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			if (flag)
			{
				The.Core.RenderMapToBuffer(scrapBuffer);
			}
			int num2 = Math.Min((base.Level - 1) / 2, 6);
			for (int i = 0; i < Quills; i++)
			{
				int num3 = Stat.RollDamagePenetrations(Stats.GetCombatAV(Target), num2, num2);
				if (num3 > 0)
				{
					if (flag)
					{
						scrapBuffer.Goto(Cell.X, Cell.Y);
						int num4 = Stat.Random(1, 4);
						if (num4 == 1)
						{
							scrapBuffer.Write("&Y\\", true, false, false, null, -1);
						}
						else if (num4 == 2)
						{
							scrapBuffer.Write("&Y-", true, false, false, null, -1);
						}
						else if (num4 == 3)
						{
							scrapBuffer.Write("&Y/", true, false, false, null, -1);
						}
						else if (num4 == 4)
						{
							scrapBuffer.Write("&Y|", true, false, false, null, -1);
						}
						textConsole.DrawBuffer(scrapBuffer, null, false);
						Thread.Sleep(10);
					}
					for (int j = 0; j < num3; j++)
					{
						num += Stat.Random(1, 3);
					}
				}
			}
			GameObject gameObject = Target;
			int num5 = num;
			GameObject parentObject = this.ParentObject;
			gameObject.TakeDamage(num5, "from %t " + this.ObjectName + "!", "Stabbing Quills", null, null, null, parentObject, null, null, null, Reactive, false, false, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x0600CB79 RID: 52089 RVA: 0x004A001C File Offset: 0x0049E21C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefenderHit" && 5.in100())
			{
				int num = Stat.Random(1, 4);
				if (num > this.nQuills)
				{
					num = this.nQuills;
				}
				if (num > 0)
				{
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"The attack breaks ",
							Grammar.Cardinal(num),
							" ",
							(num == 1) ? this.ObjectNameSingular : this.ObjectName,
							"!"
						}), null, true);
					}
					this.nQuills -= num;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CB7A RID: 52090 RVA: 0x004A00CC File Offset: 0x0049E2CC
		public override string GetDescription()
		{
			return this.Blueprint.GetTag("VariantDescription", "").Coalesce("Hundreds of needle-pointed quills cover your body.");
		}

		// Token: 0x0600CB7B RID: 52091 RVA: 0x004A00ED File Offset: 0x0049E2ED
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			this._Blueprint = null;
		}

		// Token: 0x0600CB7C RID: 52092 RVA: 0x004A00FD File Offset: 0x0049E2FD
		public int GetAV(int Level)
		{
			if (Level <= 2)
			{
				return 2;
			}
			return Level / 3 + 2;
		}

		// Token: 0x0600CB7D RID: 52093 RVA: 0x004A010A File Offset: 0x0049E30A
		public int GetAVPenalty(int Level)
		{
			return this.GetAV(Level) / 2;
		}

		// Token: 0x0600CB7E RID: 52094 RVA: 0x004A0115 File Offset: 0x0049E315
		public int GetQuillPenetration(int Level)
		{
			return Math.Min(6, (Level - 1) / 2);
		}

		// Token: 0x0600CB7F RID: 52095 RVA: 0x004A0124 File Offset: 0x0049E324
		public override string GetLevelText(int Level)
		{
			string text = this.GetQuillPenetration(Level).ToString();
			int avpenalty = this.GetAVPenalty(Level);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string objectName = this.ObjectName;
			if (Level == base.Level)
			{
				stringBuilder.Append("{{rules|").Append(this.nMaxQuills).Append("}} ")
					.Append(objectName)
					.Append('\n');
			}
			else
			{
				stringBuilder.Append("+{{rules|80-120}} ").Append(objectName).Append('\n');
			}
			stringBuilder.Append("May expel 10% of your ").Append(objectName).Append(" in a burst around yourself ({{c|\u001a}}{{rules|")
				.Append(text)
				.Append("}} {{r|\u0003}}1d3)\n")
				.Append("Regenerate ")
				.Append(objectName)
				.Append(" at the approximate rate of {{rules|")
				.Append((float)Level / 4f)
				.Append("}} per round\n")
				.Append("+{{rules|")
				.Append(this.GetAV(Level))
				.Append("}} AV as long as you retain half your ")
				.Append(objectName)
				.Append(" (+{{rules|")
				.Append(this.GetAV(Level) - avpenalty)
				.Append("}} AV otherwise)\n")
				.Append("Creatures attacking you in melee may impale themselves on your ")
				.Append(objectName)
				.Append(", breaking roughly 1% of them and reflecting 3% damage per ")
				.Append(this.ObjectNameSingular)
				.Append(" broken.\n")
				.Append("Cannot wear body armor\n")
				.Append("Immune to other creatures' ")
				.Append(objectName);
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x0600CB80 RID: 52096 RVA: 0x004A02A0 File Offset: 0x0049E4A0
		public override bool ChangeLevel(int NewLevel)
		{
			if (NewLevel != this.oldLevel)
			{
				int num = (NewLevel - this.oldLevel) * Stat.Random(80, 120);
				this.nMaxQuills = Math.Max(300, this.nMaxQuills + num);
				this.oldLevel = NewLevel;
			}
			this.nQuills = this.nMaxQuills;
			if (this.QuillsObject != null)
			{
				this.QuillsObject.GetPart<Armor>().AV = this.GetAV(base.Level);
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB81 RID: 52097 RVA: 0x004A0320 File Offset: 0x0049E520
		public override bool Mutate(GameObject GO, int Level)
		{
			Body body = GO.Body;
			BodyPart bodyPart = ((body != null) ? body.GetBody() : null);
			if (bodyPart != null)
			{
				bodyPart.ForceUnequip(true, false, false);
				this.QuillsObject = GameObject.Create(this.Blueprint, 0, 0, null, null, null, null, null);
				GO.ForceEquipObject(this.QuillsObject, bodyPart, true, new int?(0));
				this.QuillFlingActivatedAbilityID = base.AddMyActivatedAbility(this.AbilityName, Quills.COMMAND_NAME, "Physical Mutations", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB82 RID: 52098 RVA: 0x004A03B7 File Offset: 0x0049E5B7
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.QuillFlingActivatedAbilityID, null);
			base.CleanUpMutationEquipment(GO, ref this.QuillsObject);
			base.StatShifter.RemoveStatShift(GO, "AV", false);
			this.nPenalty = 0;
			return base.Unmutate(GO);
		}

		// Token: 0x04004F16 RID: 20246
		public const int MINIMUM_QUILLS_TO_FLING = 80;

		// Token: 0x04004F17 RID: 20247
		public static readonly string COMMAND_NAME = "CommandQuillFling";

		// Token: 0x04004F18 RID: 20248
		public GameObject QuillsObject;

		// Token: 0x04004F19 RID: 20249
		public int oldLevel = 1;

		// Token: 0x04004F1A RID: 20250
		public int nMaxQuills = 300;

		// Token: 0x04004F1B RID: 20251
		public int _nQuills;

		// Token: 0x04004F1C RID: 20252
		public int nPenalty;

		// Token: 0x04004F1D RID: 20253
		public Guid QuillFlingActivatedAbilityID = Guid.Empty;

		// Token: 0x04004F1E RID: 20254
		public float QuillRegenerationCounter;

		// Token: 0x04004F1F RID: 20255
		[NonSerialized]
		protected GameObjectBlueprint _Blueprint;
	}
}
