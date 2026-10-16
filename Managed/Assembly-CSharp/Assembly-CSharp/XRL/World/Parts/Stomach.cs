using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000F07 RID: 3847
	[Serializable]
	public class Stomach : IPart
	{
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600996A RID: 39274 RVA: 0x0037AD51 File Offset: 0x00378F51
		// (set) Token: 0x0600996B RID: 39275 RVA: 0x0037AD59 File Offset: 0x00378F59
		public int CookingCounter
		{
			get
			{
				return this._CookingCounter;
			}
			set
			{
				this._CookingCounter = value;
				this.UpdateHunger();
			}
		}

		// Token: 0x0600996C RID: 39276 RVA: 0x0037AD68 File Offset: 0x00378F68
		public void GetHungry()
		{
			this.CookingCounter = this.CalculateCookingIncrement();
			this.UpdateHunger();
		}

		// Token: 0x0600996D RID: 39277 RVA: 0x0037AD7C File Offset: 0x00378F7C
		public void ClearHunger()
		{
			this.ParentObject.RemoveEffect<Famished>();
			this.HungerLevel = 0;
			this.CookingCounter = 0;
			this.WasOnWorldMap = 0;
		}

		// Token: 0x0600996E RID: 39278 RVA: 0x0037AD9F File Offset: 0x00378F9F
		public static void ClearHunger(GameObject Subject = null)
		{
			if (Subject != null)
			{
				Stomach part = Subject.GetPart<Stomach>();
				if (part == null)
				{
					return;
				}
				part.ClearHunger();
			}
		}

		// Token: 0x0600996F RID: 39279 RVA: 0x0037ADB4 File Offset: 0x00378FB4
		public int CalculateCookingIncrement()
		{
			int num = 1200;
			if (this.ParentObject.HasSkill("Discipline_FastingWay"))
			{
				num *= 2;
			}
			if (this.ParentObject.HasSkill("Discipline_MindOverBody"))
			{
				num *= 6;
			}
			return num;
		}

		// Token: 0x06009970 RID: 39280 RVA: 0x0037ADF4 File Offset: 0x00378FF4
		public string FoodStatus()
		{
			if (this.HungerLevel == 0)
			{
				return "{{g|Sated}}";
			}
			if (this.HungerLevel == 1)
			{
				return "{{W|Hungry}}";
			}
			if (this.ParentObject.HasPart<PhotosyntheticSkin>())
			{
				return "{{R|Wilted!}}";
			}
			return "{{R|Famished!}}";
		}

		// Token: 0x06009971 RID: 39281 RVA: 0x0037AE2C File Offset: 0x0037902C
		public string WaterStatus()
		{
			if (this.ParentObject.HasPart<Amphibious>())
			{
				if (this.Water <= 0)
				{
					return "{{R|Desiccated!}}";
				}
				if (this.Water <= RuleSettings.WATER_PARCHED)
				{
					return "{{r|Dry}}";
				}
				if (this.Water <= RuleSettings.WATER_THIRSTY)
				{
					return "{{c|Moist}}";
				}
				if (this.Water <= RuleSettings.WATER_QUENCHED)
				{
					return "{{b|Wet}}";
				}
				return "{{B|Soaked}}";
			}
			else
			{
				if (this.Water <= 0)
				{
					return "{{R|Dehydrated!}}";
				}
				if (this.Water <= RuleSettings.WATER_PARCHED)
				{
					return "{{r|Parched}}";
				}
				if (this.Water <= RuleSettings.WATER_THIRSTY)
				{
					return "{{Y|Thirsty}}";
				}
				if (this.Water <= RuleSettings.WATER_QUENCHED)
				{
					return "{{g|Quenched}}";
				}
				return "{{G|Tumescent}}";
			}
		}

		// Token: 0x06009972 RID: 39282 RVA: 0x0037AEE1 File Offset: 0x003790E1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009973 RID: 39283 RVA: 0x0037AEE4 File Offset: 0x003790E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanDrinkEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<HealsNaturallyEvent>.ID || ID == PooledEvent<InduceVomitingEvent>.ID || ID == TookDamageEvent.ID || ID == PooledEvent<CanTravelEvent>.ID;
		}

		// Token: 0x06009974 RID: 39284 RVA: 0x0037AF45 File Offset: 0x00379145
		public override bool HandleEvent(HealsNaturallyEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x06009975 RID: 39285 RVA: 0x0037AF60 File Offset: 0x00379160
		public override bool HandleEvent(InduceVomitingEvent E)
		{
			if (E.Object == this.ParentObject && E.Object.GetLongProperty("VomitedOnTurn", 0L) <= The.CurrentTurn - 3L)
			{
				E.Vomited = true;
				if (E.Object.IsPlayer())
				{
					E.Message("You vomit!");
				}
				else
				{
					E.Message(E.Object.Does("vomit", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " everywhere!");
				}
				this.Water = Stat.Random(Math.Min(this.Water, RuleSettings.WATER_MAXIMUM) * 2 / 5, Math.Min(this.Water, RuleSettings.WATER_MAXIMUM) * 3 / 5);
				E.Object.ApplyEffect(new LiquidCovered("putrid", 2, 1, false, null, false), null);
				if (E.Object.CurrentCell != null && !E.Object.OnWorldMap())
				{
					E.Object.CurrentCell.AddObject("VomitPool", null, null, null, null);
				}
				E.Object.SetLongProperty("VomitedOnTurn", The.CurrentTurn);
				E.Object.UseEnergy(1000, "Vomit", null, null, false);
				E.InterfaceExit = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009976 RID: 39286 RVA: 0x0037B0C6 File Offset: 0x003792C6
		public override bool HandleEvent(CanDrinkEvent E)
		{
			E.CanDrinkThis = true;
			return base.HandleEvent(E);
		}

		// Token: 0x06009977 RID: 39287 RVA: 0x0037B0D8 File Offset: 0x003792D8
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == this.ParentObject && this.Water <= 0)
			{
				E.AddStatus(this.ParentObject.HasPart<Amphibious>() ? "desiccated" : "dehydrated", -80);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009978 RID: 39288 RVA: 0x0037B124 File Offset: 0x00379324
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.ParentObject.IsPlayer())
			{
				if (!this.ParentObject.OnWorldMap())
				{
					if (this.WasOnWorldMap > 0)
					{
						if (this.WasOnWorldMap > 2 && this.HungerLevel < 1)
						{
							this.CookingCounter = this.CalculateCookingIncrement();
							this.ParentObject.FireEvent("BecameHungry");
						}
						this.WasOnWorldMap = 0;
					}
					else
					{
						this.ParentObject.SetLongProperty("OnWorldMapSince", XRLCore.CurrentTurn);
					}
				}
				else
				{
					this.WasOnWorldMap++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009979 RID: 39289 RVA: 0x0037B1B8 File Offset: 0x003793B8
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			int water = this.Water;
			bool flag = false;
			if (this.ParentObject.IsPlayer())
			{
				if (!this.ParentObject.OnWorldMap())
				{
					if (this.WasOnWorldMap > 0)
					{
						this.WasOnWorldMap = 0;
						this.CookingCounter = this.CalculateCookingIncrement();
						int num = (int)Math.Min(XRLCore.CurrentTurn - this.ParentObject.GetLongProperty("OnWorldMapSince", 0L), 100000L);
						using (List<GameObject>.Enumerator enumerator = this.ParentObject.GetCompanions().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Stomach stomach;
								if (enumerator.Current.TryGetPart<Stomach>(out stomach) && stomach.HitCounter <= 5)
								{
									stomach.ProcessNaturalHealing(num);
								}
							}
						}
					}
					int cookingCounter = this.CookingCounter;
					this.CookingCounter = cookingCounter + 1;
				}
				if (!this.ParentObject.HasEffect<Asleep>())
				{
					if (this.ParentObject.HasPart<FattyHump>())
					{
						if (this.Water > 0)
						{
							this.Water--;
						}
					}
					else if (this.ParentObject.Speed != 0)
					{
						int num2 = 1;
						if (this.ParentObject.HasPart<Discipline_FastingWay>())
						{
							num2 *= 2;
						}
						if (this.ParentObject.HasPart<Discipline_MindOverBody>())
						{
							num2 *= 6;
						}
						int num3 = this.ParentObject.Speed / (this.ParentObject.HasPart<Amphibious>() ? 3 : 5) / num2;
						Stomach.eCalculatingThirst.SetParameter("Amount", num3);
						this.ParentObject.FireEvent(Stomach.eCalculatingThirst);
						num3 = Stomach.eCalculatingThirst.GetIntParameter("Amount", 0);
						this.Water -= num3;
					}
					else
					{
						this.Water -= 20;
					}
					if (this.Water < RuleSettings.WATER_MINIMUM)
					{
						this.Water = RuleSettings.WATER_MINIMUM;
					}
				}
				if (this.Water < 0)
				{
					this.Water = 0;
				}
				if (Options.AutoSip)
				{
					int num4 = RuleSettings.WATER_THIRSTY;
					string autoSipLevel = Options.AutoSipLevel;
					if (!string.IsNullOrEmpty(autoSipLevel))
					{
						if (autoSipLevel == "Dehydrated")
						{
							num4 = RuleSettings.WATER_MINIMUM;
						}
						else if (autoSipLevel == "Parched")
						{
							num4 = RuleSettings.WATER_PARCHED;
						}
						else if (autoSipLevel == "Thirsty")
						{
							num4 = RuleSettings.WATER_THIRSTY;
						}
						else if (autoSipLevel == "Quenched")
						{
							num4 = RuleSettings.WATER_QUENCHED;
						}
						else if (autoSipLevel == "Tumescent")
						{
							num4 = RuleSettings.WATER_TUMESCENT;
						}
					}
					if (this.Water < num4)
					{
						flag = true;
					}
				}
			}
			else
			{
				int water_THIRSTY = RuleSettings.WATER_THIRSTY;
				if (this.Water < water_THIRSTY)
				{
					flag = true;
				}
			}
			if (flag && !this.ParentObject.IsFrozen() && this.ParentObject.CanMoveExtremities(null, false, false, true))
			{
				Stomach.containers.Clear();
				bool flag2 = this.ParentObject.HasPart<Amphibious>();
				if (this.ParentObject.UseDrams(1, "water", null, null, null, Stomach.containers, !flag2))
				{
					GameObject gameObject = ((Stomach.containers.Count > 0) ? Stomach.containers[0] : null);
					if (flag2)
					{
						if (gameObject == null)
						{
							base.DidXToY("pour", "fresh water over", this.ParentObject, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						else
						{
							base.DidXToYWithZ("pour", "fresh water from", gameObject, "over", this.ParentObject, null, null, null, null, this.ParentObject, null, false, false, false, false, true, false, false, false, null, this.ParentObject, null, null, false, false, false, false, false, null);
						}
						this.ParentObject.ApplyEffect(new LiquidCovered("water", 1, 1, false, null, false), null);
					}
					else
					{
						if (gameObject == null)
						{
							base.DidX("take", "a sip of fresh water", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
						}
						else
						{
							this.ParentObject.FireEvent(Event.New("DrinkingFrom", "Container", gameObject));
							base.DidXToY("take", "a sip of fresh water from", gameObject, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, this.ParentObject, null, false, false, false, false, false, null);
						}
						this.FireEvent(Event.New("AddWater", "Amount", 10000));
					}
				}
				else if (E.Traveling && !E.TravelMessagesSuppressed)
				{
					E.TravelMessagesSuppressed = true;
					if (Popup.ShowYesNo("You have run out of {{B|water}}! Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						return false;
					}
				}
				Stomach.containers.Clear();
			}
			bool flag3 = this.ProcessNaturalHealing(1);
			if (base.IsPlayer())
			{
				if (water > RuleSettings.WATER_THIRSTY && this.Water < RuleSettings.WATER_THIRSTY)
				{
					base.PlayWorldSound("sfx_characterMod_thirsty", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				if (!flag3 && Stat.RollPenetratingSuccesses("1d" + this.ParentObject.Stat("Toughness", 0).ToString(), 2, 0, 1, null) <= 0)
				{
					if (E.Traveling)
					{
						if (!E.TravelMessagesSuppressed)
						{
							E.TravelMessagesSuppressed = true;
							if (this.ParentObject.HasPart<Amphibious>())
							{
								if (Popup.ShowYesNo("You are drying out! Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
								{
									return false;
								}
							}
							else if (Popup.ShowYesNo("You are dying of thirst! Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
							{
								return false;
							}
						}
					}
					else if (this.ParentObject.HasPart<Amphibious>())
					{
						Popup.Show("You are drying out!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("You are dying of thirst!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.ParentObject.Physics.LastDamagedByType = "Dessicated";
					this.ParentObject.Physics.LastDeathReason = "You died of thirst.";
					this.ParentObject.Physics.LastThirdPersonDeathReason = "";
					this.ParentObject.Physics.LastDamagedBy = null;
					XRLCore.Core.Game.DeathReason = "You died of thirst.";
					The.Game.DeathCategory = "thirst";
					this.ParentObject.GetStat("Hitpoints").Penalty += 2;
				}
				if (XRLCore.Core.IDKFA)
				{
					this.ParentObject.GetStat("Hitpoints").Penalty = 0;
					this.Water = RuleSettings.WATER_MAXIMUM - 1000;
					this.ParentObject.Physics.Temperature = 25;
					Sidebar.Update();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600997A RID: 39290 RVA: 0x0037B84C File Offset: 0x00379A4C
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.HitCounter = 10;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600997B RID: 39291 RVA: 0x0037B86C File Offset: 0x00379A6C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Water", this.Water);
			E.AddEntry(this, "HungerLevel", this.HungerLevel);
			E.AddEntry(this, "CookCount", this.CookCount);
			E.AddEntry(this, "CookingCounter", this.CookingCounter);
			return base.HandleEvent(E);
		}

		// Token: 0x0600997C RID: 39292 RVA: 0x0037B8C8 File Offset: 0x00379AC8
		public override bool HandleEvent(CanTravelEvent E)
		{
			if (E.Object == this.ParentObject && this.IsFamished() && !E.Object.HasSkill("Discipline_MindOverBody") && !The.Core.IDKFA)
			{
				return E.Object.ShowFailure("You're too famished to travel long distances.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600997D RID: 39293 RVA: 0x0037B921 File Offset: 0x00379B21
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600997E RID: 39294 RVA: 0x0037B924 File Offset: 0x00379B24
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AddFood");
			Registrar.Register("AddWater");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600997F RID: 39295 RVA: 0x0037B944 File Offset: 0x00379B44
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AddWater")
			{
				int num = E.GetIntParameter("Amount", 0);
				int water = this.Water;
				int num2 = this.Water + num;
				bool flag = E.HasFlag("Forced");
				bool flag2 = E.HasFlag("External");
				bool flag3 = false;
				if (this.ParentObject.HasPart<Amphibious>())
				{
					if (num < 0 && base.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("The moisture is sucked out of your body.", null, true);
						if (water > RuleSettings.WATER_THIRSTY && num2 < RuleSettings.WATER_THIRSTY)
						{
							base.PlayWorldSound("sfx_characterMod_thirsty", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						}
					}
					if (!flag2)
					{
						num /= 10;
					}
					this.Water += num;
					if (this.Water > RuleSettings.WATER_MAXIMUM)
					{
						this.Water = RuleSettings.WATER_MAXIMUM;
					}
					else if (this.Water < RuleSettings.WATER_MINIMUM)
					{
						this.Water = RuleSettings.WATER_MINIMUM;
					}
				}
				else
				{
					if (num < 0 && base.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("The moisture is sucked out of your throat.", null, true);
						if (water > RuleSettings.WATER_THIRSTY && num2 < RuleSettings.WATER_THIRSTY)
						{
							base.PlayWorldSound("sfx_characterMod_thirsty", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						}
					}
					if (!flag && this.Water + num > RuleSettings.WATER_MAXIMUM && base.IsPlayer() && Popup.ShowYesNo("Drinking that much will probably make you sick, do you want to continue?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
					this.Water += num;
					if (this.Water > RuleSettings.WATER_MAXIMUM)
					{
						StringBuilder parameter = E.GetParameter<StringBuilder>("MessageHolder");
						StringBuilder stringBuilder = parameter ?? Event.NewStringBuilder(null);
						if (base.IsPlayer())
						{
							stringBuilder.Compound("You drank way too much!");
						}
						flag3 = InduceVomitingEvent.Send(this.ParentObject, stringBuilder, E);
						if (this.Water > RuleSettings.WATER_MAXIMUM)
						{
							this.Water = RuleSettings.WATER_MAXIMUM;
						}
						if (stringBuilder.Length > 0 && parameter == null)
						{
							base.EmitMessage(stringBuilder, ' ', true, false, false, null, null);
						}
					}
					else if (this.Water < RuleSettings.WATER_MINIMUM)
					{
						this.Water = RuleSettings.WATER_MINIMUM;
					}
				}
				if (this.ParentObject.HasRegisteredEvent("AfterDrank"))
				{
					Event @event = Event.New("AfterDrank", 0, 0, 0);
					@event.SetParameter("Amount", num);
					@event.SetFlag("Forced", flag);
					@event.SetFlag("External", flag2);
					@event.SetFlag("Vomited", flag3);
					this.ParentObject.FireEvent(@event);
				}
				if (num < 0)
				{
					this.CheckCompanionThirstNotify(true);
				}
				if (flag3)
				{
					return false;
				}
			}
			else if (E.ID == "AddFood")
			{
				bool flag4 = this.HungerLevel > 0;
				string stringParameter = E.GetStringParameter("Satiation", "Snack");
				bool flag5 = E.HasFlag("Meat");
				bool flag6 = this.ParentObject.HasPart<Carnivorous>();
				if (!flag6 || flag5)
				{
					if (stringParameter == "Snack")
					{
						this.CookingCounter -= Food.SMALL_HUNGER_AMOUNT;
					}
					else if (stringParameter == "Meal")
					{
						this.CookingCounter = 0;
					}
				}
				if (flag6)
				{
					if (flag5)
					{
						if (flag4)
						{
							Campfire.RollTasty(10, true, false);
						}
					}
					else if (Stat.Random(0, 1) == 0)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show("Ugh, you feel sick.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						this.ParentObject.ApplyEffect(new Ill(100, 1, null, false, false), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009980 RID: 39296 RVA: 0x0037BCEA File Offset: 0x00379EEA
		public bool IsFamished()
		{
			return this.HungerLevel >= 2;
		}

		// Token: 0x06009981 RID: 39297 RVA: 0x0037BCF8 File Offset: 0x00379EF8
		public void UpdateHunger()
		{
			int num = this.CalculateCookingIncrement();
			if (this.CookingCounter < num || this.ParentObject.HasPropertyOrTag("Robot"))
			{
				if (this.HungerLevel != 0)
				{
					this.ParentObject.RemoveEffect<Famished>();
					this.HungerLevel = 0;
					this.CookCount = 0;
					return;
				}
			}
			else if (this.CookingCounter >= num * 2)
			{
				if (this.HungerLevel != 2)
				{
					this.HungerLevel = 2;
					if (!this.ParentObject.HasEffect<Famished>() && !this.ParentObject.HasPart<Discipline_MindOverBody>())
					{
						this.ParentObject.ApplyEffect(new Famished(), null);
					}
					this.ParentObject.FireEvent("BecameFamished");
					this.CookCount = 0;
					return;
				}
			}
			else if (this.CookingCounter >= num && this.HungerLevel != 1)
			{
				if (base.IsPlayer())
				{
					base.PlayWorldSound("sfx_characterMod_hungry", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				this.ParentObject.RemoveEffect<Famished>();
				this.HungerLevel = 1;
				this.ParentObject.FireEvent("BecameHungry");
				this.CookCount = 0;
			}
		}

		// Token: 0x06009982 RID: 39298 RVA: 0x0037BE20 File Offset: 0x0037A020
		public bool ProcessNaturalHealing(int Turns = 1)
		{
			if (this.HitCounter > 0)
			{
				this.HitCounter -= Turns;
			}
			bool flag = this.ParentObject.HasPart<Regeneration>();
			if (this.HitCounter <= 0 || flag)
			{
				int num = (20 + 2 * this.ParentObject.StatMod("Toughness", 0) + 2 * this.ParentObject.StatMod("Willpower", 0)) * Turns;
				Stomach.eRegenerating.ID = "Regenerating";
				Stomach.eRegenerating.SetParameter("Amount", num);
				this.ParentObject.FireEvent(Stomach.eRegenerating);
				Stomach.eRegenerating.ID = "Regenerating2";
				this.ParentObject.FireEvent(Stomach.eRegenerating);
				num = Stomach.eRegenerating.GetIntParameter("Amount", 0);
				if (num < 0)
				{
					num = 0;
				}
				if (this.HitCounter > 0)
				{
					num /= 2;
				}
				if (this.ParentObject.HasEffect<Meditating>())
				{
					num *= 3;
				}
				if (this.ParentObject.HasPart<LuminousInfection>() && base.IsDay() && this.ParentObject.CurrentZone != null && this.ParentObject.CurrentZone.Z <= 10)
				{
					num = num * 85 / 100;
				}
				this.RegenCounter += num;
				if (this.RegenCounter > 100)
				{
					int num2 = (int)Math.Floor((double)this.RegenCounter / 100.0);
					this.RegenCounter %= 100;
					if (this.Water > 0)
					{
						this.ParentObject.GetStat("Hitpoints").Penalty -= num2;
						if (this.ParentObject.IsPlayer())
						{
							Sidebar.Update();
						}
					}
					else
					{
						if (base.IsPlayer())
						{
							return false;
						}
						this.CheckCompanionThirstNotify(false);
					}
				}
			}
			return true;
		}

		// Token: 0x06009983 RID: 39299 RVA: 0x0037BFE0 File Offset: 0x0037A1E0
		public void CheckCompanionThirstNotify(bool HighPriority = false)
		{
			if (this.Water > 0)
			{
				return;
			}
			if (this.ParentObject.IsPlayer())
			{
				return;
			}
			if (this.ParentObject.IsTrifling)
			{
				return;
			}
			if (!this.ParentObject.IsPlayerLed())
			{
				return;
			}
			if (!IComponent<GameObject>.Visible(this.ParentObject))
			{
				return;
			}
			if (XRLCore.Core.Game.Turns - this.ParentObject.GetLongProperty("LastCompanionThirstNotify", 0L) < 100L)
			{
				return;
			}
			if (this.ParentObject.GetFreeDrams("water", null, null, null, false) >= 1)
			{
				return;
			}
			string text = string.Concat(new string[]
			{
				"{{R|",
				this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
				this.ParentObject.Is,
				" dehydrated and will be unable to heal naturally until ",
				this.ParentObject.it,
				this.ParentObject.GetVerb("get", true, true, false, true, false),
				" water to ",
				this.ParentObject.HasPart<Amphibious>() ? ("douse " + this.ParentObject.itself + " with") : "drink",
				".}}"
			});
			if (HighPriority)
			{
				Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				IComponent<GameObject>.AddPlayerMessage(text, null, true);
			}
			this.ParentObject.SetLongProperty("LastCompanionThirstNotify", XRLCore.Core.Game.Turns);
		}

		// Token: 0x06009984 RID: 39300 RVA: 0x0037C16B File Offset: 0x0037A36B
		public void ResetCookingCounter()
		{
			this.CookingCounter = 0;
		}

		// Token: 0x04003FF0 RID: 16368
		public const int COOKING_INCREMENT = 1200;

		// Token: 0x04003FF1 RID: 16369
		public int CookCount;

		// Token: 0x04003FF2 RID: 16370
		public int Water = 30000;

		// Token: 0x04003FF3 RID: 16371
		public int RegenCounter;

		// Token: 0x04003FF4 RID: 16372
		[NonSerialized]
		private static Event eCalculatingThirst = new Event("CalculatingThirst", "Amount", 0);

		// Token: 0x04003FF5 RID: 16373
		[NonSerialized]
		private static Event eRegenerating = new Event("Regenerating", "Amount", 0);

		// Token: 0x04003FF6 RID: 16374
		public int HungerLevel;

		// Token: 0x04003FF7 RID: 16375
		public int _CookingCounter;

		// Token: 0x04003FF8 RID: 16376
		public int HitCounter;

		// Token: 0x04003FF9 RID: 16377
		public int WasOnWorldMap;

		// Token: 0x04003FFA RID: 16378
		[NonSerialized]
		private static List<GameObject> containers = new List<GameObject>();
	}
}
