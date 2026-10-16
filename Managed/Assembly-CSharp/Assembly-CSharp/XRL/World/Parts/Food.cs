using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F81 RID: 3969
	[Serializable]
	public class Food : IPart
	{
		// Token: 0x06009DCE RID: 40398 RVA: 0x003923E4 File Offset: 0x003905E4
		public override bool SameAs(IPart p)
		{
			Food food = p as Food;
			return food.Thirst == this.Thirst && !(food.Satiation != this.Satiation) && food.Gross == this.Gross && food.IllOnEat == this.IllOnEat && !(food.Healing != this.Healing) && !(food.Message != this.Message) && base.SameAs(p);
		}

		// Token: 0x06009DCF RID: 40399 RVA: 0x0039246E File Offset: 0x0039066E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == PooledEvent<GetAutoEquipPriorityEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009DD0 RID: 40400 RVA: 0x0039249C File Offset: 0x0039069C
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (this.Healing != "0" && !this.ParentObject.IsImportant() && E.Actor.HasPart<Stomach>())
			{
				int num = this.Healing.RollMinCached();
				if (num >= 0)
				{
					int num2 = this.Healing.RollMaxCached();
					if (num2 > 0)
					{
						int hitpoints = E.Actor.hitpoints;
						int baseHitpoints = E.Actor.baseHitpoints;
						if (hitpoints < baseHitpoints)
						{
							int num3 = 0;
							int num4 = baseHitpoints - hitpoints;
							if (num4 >= num)
							{
								num3++;
							}
							if (num4 >= num2)
							{
								num3++;
							}
							if (num4 >= baseHitpoints * 9 / 10)
							{
								num3++;
							}
							if (num3 > 0)
							{
								E.Add("Eat", num3, this.ParentObject, true, true, null, null);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DD1 RID: 40401 RVA: 0x00392566 File Offset: 0x00390766
		public override bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			E.Priority -= E.Default;
			return base.HandleEvent(E);
		}

		// Token: 0x06009DD2 RID: 40402 RVA: 0x00392584 File Offset: 0x00390784
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			int num = 20;
			if (this.Healing == "0" && !this.ParentObject.HasPart<HealOnEat>() && !this.ParentObject.HasPart<GeometricHealOnEat>())
			{
				num = 0;
			}
			else if (!E.Actor.HasPart<Stomach>())
			{
				num = 0;
			}
			E.AddAction("Eat", "eat", "Eat", null, 'a', false, num, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009DD3 RID: 40403 RVA: 0x00392600 File Offset: 0x00390800
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Eat")
			{
				if (!E.Actor.CanMoveExtremities("Eat", true, false, false))
				{
					return false;
				}
				Stomach part = E.Actor.GetPart<Stomach>();
				if (part == null)
				{
					E.Actor.Fail("You are unable to consume food.");
					return false;
				}
				if (this.Gross && (!E.Actor.HasPart<Carnivorous>() || !this.ParentObject.HasTag("Meat")) && (part == null || (part.HungerLevel < 2 && !E.Actor.HasSkill("Discipline_MindOverBody"))))
				{
					E.Actor.Fail("You're not hungry enough to bring " + E.Actor.itself + " to eat that.");
					return true;
				}
				if (!BeforeConsumeEvent.Check(E.Actor, E.Actor, E.Item, true, false, false, false, false, true))
				{
					return false;
				}
				Event @event = Event.New("OnEat", 0, 0, 0);
				@event.SetParameter("Actor", E.Actor);
				@event.SetParameter("Eater", E.Actor);
				@event.SetParameter("Subject", E.Actor);
				@event.SetParameter("Food", this.ParentObject);
				@event.SetParameter("Object", this.ParentObject);
				if (!this.ParentObject.FireEvent(@event, E))
				{
					return false;
				}
				int hungerLevel = part.HungerLevel;
				@event.ID = "Eating";
				if (!E.Actor.FireEvent(@event))
				{
					return false;
				}
				E.Actor.FireEvent(Event.New("AddWater", "Amount", this.Thirst));
				E.Actor.Heal(this.Healing.RollCached(), false, true, true);
				if (E.Actor.HasPart<Stomach>())
				{
					if (!E.Actor.HasPart<Carnivorous>() || this.ParentObject.HasTag("Meat"))
					{
						if (this.Satiation == "Snack")
						{
							part.CookingCounter -= Food.SMALL_HUNGER_AMOUNT;
						}
						if (this.Satiation == "Meal")
						{
							part.CookingCounter = 0;
						}
					}
					if (E.Actor.IsPlayer())
					{
						string text = this.Message ?? "";
						if (text != "")
						{
							if (text.Contains("~"))
							{
								text = text.Split('~', StringSplitOptions.None)[0];
							}
							text = GameText.VariableReplace(text, this.ParentObject, E.Actor, false);
							if (!text.EndsWith("!") && !text.EndsWith(".") && !text.EndsWith("?"))
							{
								text += ".";
							}
							if (!text.EndsWith("\n"))
							{
								text += "\n";
							}
						}
						Popup.Show(string.Concat(new string[]
						{
							"You eat ",
							this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							".\n",
							text,
							"You are now {{|",
							part.FoodStatus(),
							"}} and {{|",
							part.WaterStatus(),
							"}}."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				AfterConsumeEvent.Send(E.Actor, E.Actor, E.Item, true, false, false, false, false, true);
				@event.ID = "Eaten";
				this.ParentObject.FireEvent(@event);
				if (this.IllnessChance(E.Actor).in100())
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("Ugh, you feel sick.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					E.Actor.ApplyEffect(new Ill(100, 1, null, false, false), null);
				}
				E.Actor.UseEnergy(1000, "Item Eat", null, null, false);
				E.RequestInterfaceExit();
				@event.ID = "AfterEat";
				this.ParentObject.FireEvent(@event, E);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DD4 RID: 40404 RVA: 0x00392A32 File Offset: 0x00390C32
		public virtual int IllnessChance(GameObject Actor)
		{
			if (Actor.HasPart<Carnivorous>())
			{
				if (this.ParentObject.HasTag("Meat"))
				{
					return 0;
				}
				if (!this.IllOnEat)
				{
					return 50;
				}
				return 100;
			}
			else
			{
				if (this.IllOnEat)
				{
					return 100;
				}
				return 0;
			}
		}

		// Token: 0x04004140 RID: 16704
		public static readonly int SMALL_HUNGER_AMOUNT = 200;

		// Token: 0x04004141 RID: 16705
		public int Thirst;

		// Token: 0x04004142 RID: 16706
		public string Satiation = "None";

		// Token: 0x04004143 RID: 16707
		public bool Gross;

		// Token: 0x04004144 RID: 16708
		public bool IllOnEat;

		// Token: 0x04004145 RID: 16709
		public string Healing = "0";

		// Token: 0x04004146 RID: 16710
		public string Message = "That hits the spot!";
	}
}
