using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FF6 RID: 4086
	[Serializable]
	public class AnimateObject : IPoweredPart
	{
		// Token: 0x0600A2DD RID: 41693 RVA: 0x003B7F30 File Offset: 0x003B6130
		public AnimateObject()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 5000;
		}

		// Token: 0x0600A2DE RID: 41694 RVA: 0x003B7F4A File Offset: 0x003B614A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A2DF RID: 41695 RVA: 0x003B7F68 File Offset: 0x003B6168
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivateAnimateObject", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2E0 RID: 41696 RVA: 0x003B7FA0 File Offset: 0x003B61A0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateAnimateObject")
			{
				Cell cell = null;
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					cell = E.Actor.Physics.PickDirection("Animate", null);
				}
				if (cell != null)
				{
					List<GameObject> list = new List<GameObject>();
					List<string> list2 = new List<string>();
					char c = 'a';
					List<char> list3 = new List<char>();
					foreach (GameObject gameObject in cell.Objects)
					{
						if (AnimateObject.CanBeAnimated(gameObject))
						{
							list.Add(gameObject);
							list2.Add(gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true));
							List<char> list4 = list3;
							int num;
							if (c > 'z')
							{
								num = 32;
							}
							else
							{
								c = (char)((num = (int)c) + 1);
							}
							list4.Add((char)num);
						}
					}
					if (list.Count == 0)
					{
						Popup.Show("There's nothing viable to animate here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						int num2 = Popup.PickOption("Choose an object to animate.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num2 >= 0)
						{
							GameObject gameObject2 = list[num2];
							if (gameObject2.HasPart<Brain>())
							{
								if (E.Actor.IsPlayer())
								{
									Popup.Show("You can't animate an object that already has a brain.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
							}
							else if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
							{
								if (E.Actor.IsPlayer())
								{
									Popup.Show(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unresponsive.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
							}
							else
							{
								gameObject2.PlayWorldSound("Sounds/Interact/sfx_interact_sentience_imbue", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								E.Actor.UseEnergy(1000, "Item Animate", null, null, false);
								if (E.Actor.IsPlayer())
								{
									Popup.Show("You imbue " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " with life.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
									JournalAPI.AddAccomplishment("You imbued " + gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " with life. Why?", string.Concat(new string[]
									{
										"While traveling in ",
										Grammar.GetProsaicZoneName(The.Player.CurrentZone),
										", =name= performed a sacred ritual with ",
										gameObject2.a,
										gameObject2.ShortDisplayName,
										", imbuing ",
										gameObject2.them,
										" with life and arranging ",
										gameObject2.them,
										" ",
										HistoricStringExpander.ExpandString("<spice.elements." + IComponent<GameObject>.ThePlayerMythDomain + ".babeTrait.!random>", null),
										". Many of the local denizens declared it a miracle. Some weren't so sure."
									}), string.Concat(new string[]
									{
										"While traveling in ",
										Grammar.GetProsaicZoneName(The.Player.CurrentZone),
										", =name= performed a sacred ritual with ",
										gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										", imbuing ",
										gameObject2.them,
										" with life and arranging ",
										gameObject2.them,
										" ",
										HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".babeTrait.!random>", null),
										". Many of the local denizens declared it a miracle."
									}), null, "general", MuralCategory.CommitsFolly, MuralWeight.Medium, null, -1L, true);
								}
								AnimateObject.Animate(gameObject2, E.Actor, this.ParentObject);
								E.RequestInterfaceExit();
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2E1 RID: 41697 RVA: 0x003B8420 File Offset: 0x003B6620
		public static bool CanAnimate(GameObject frankenObject)
		{
			return frankenObject.HasTagOrProperty("Animatable") && !frankenObject.HasPart<Brain>();
		}

		// Token: 0x0600A2E2 RID: 41698 RVA: 0x003B843C File Offset: 0x003B663C
		public static void Animate(GameObject frankenObject, GameObject Actor = null, GameObject Using = null)
		{
			GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("Creature");
			frankenObject.Physics.Category = "Creatures";
			frankenObject.Physics.Solid = false;
			frankenObject.Physics.Takeable = false;
			frankenObject.SetIntProperty("Creature", 1, false);
			frankenObject.SetIntProperty("UseFullDisplayNameForCreatureType", 1, false);
			frankenObject.Render.RenderIfDark = false;
			foreach (KeyValuePair<string, Statistic> keyValuePair in blueprint.Stats)
			{
				if (!frankenObject.HasStat(keyValuePair.Key))
				{
					Statistic statistic = new Statistic(keyValuePair.Value);
					frankenObject.Statistics.Add(keyValuePair.Key, statistic);
					statistic.Owner = frankenObject;
				}
			}
			frankenObject.RequirePart<Brain>(false);
			frankenObject.RequirePart<AnimatedObject>(false);
			frankenObject.Brain.Wanders = true;
			frankenObject.Brain.Allegiance.Clear();
			frankenObject.Brain.Allegiance.Add("Newly Sentient Beings", 100);
			frankenObject.Energy = frankenObject.Statistics.GetValue("Energy", null);
			if (frankenObject.Body == null)
			{
				frankenObject.AddPart<Body>(new Body(), true, false).Anatomy = frankenObject.GetTag("BodyType", "Humanoid");
			}
			if (frankenObject.Blueprint != "Ercolano")
			{
				frankenObject.RequirePart<Combat>(false);
			}
			frankenObject.RequirePart<Experience>(false);
			frankenObject.RequirePart<Inventory>(false);
			frankenObject.RequirePart<Mutations>(false);
			frankenObject.RequirePart<Skills>(false);
			frankenObject.RequirePart<ActivatedAbilities>(false);
			frankenObject.RequirePart<Leveler>(false);
			frankenObject.RequirePart<Stomach>(false);
			frankenObject.RequirePart<Corpse>(false);
			if (!frankenObject.HasPart<ConversationScript>())
			{
				frankenObject.AddPart<ConversationScript>(new ConversationScript("NewlySentientBeings"), true, false);
			}
			if (frankenObject.GetIntProperty("ForceEffects", 0) == 0)
			{
				frankenObject.SetIntProperty("ForceEffects", 1, false);
			}
			if (frankenObject.GetSoundTag("PunchSound", null).IsNullOrEmpty())
			{
				frankenObject.SetStringProperty("PunchSound", "Sounds/Creatures/VO/sfx_animatedFurniture_vo_attack", false);
			}
			if (frankenObject.GetSoundTag("DeathSounds", null).IsNullOrEmpty())
			{
				frankenObject.SetStringProperty("DeathSounds", "Sounds/Creatures/VO/sfx_animatedFurniture_vo_die", false);
			}
			if (frankenObject.GetSoundTag("TakeDamageSound", null).IsNullOrEmpty())
			{
				frankenObject.SetStringProperty("TakeDamageSound", "Sounds/Creatures/VO/sfx_animatedFurniture_vo_hurt", false);
			}
			if (frankenObject.GetSoundTag("AmbientIdleSound", null).IsNullOrEmpty())
			{
				frankenObject.SetStringProperty("AmbientIdleSound", "Sounds/Creatures/VO/sfx_animatedFurniture_vo_idle", false);
			}
			frankenObject.RemoveIntProperty("Anchoring");
			string propertyOrTag = frankenObject.GetPropertyOrTag("AnimatedSkills", null);
			if (!string.IsNullOrEmpty(propertyOrTag))
			{
				foreach (string text in propertyOrTag.Split(',', StringSplitOptions.None))
				{
					frankenObject.AddSkill(text, null, null);
				}
			}
			if (frankenObject.GetGender(false).Subjective == "it")
			{
				frankenObject.SetGender("nonspecific");
			}
			if (50.in100())
			{
				frankenObject.SetPronounSet("any");
			}
			else if (50.in100())
			{
				frankenObject.SetPronounSet("generate");
			}
			string tag = frankenObject.GetTag("AnimatedInventoryPopulationTable", null);
			if (tag != null)
			{
				frankenObject.ReceivePopulation(tag);
			}
			AnimateEvent.Send(Actor, frankenObject, Using);
			if (!frankenObject.IsNowhere())
			{
				frankenObject.MakeActive(false);
			}
			if (Actor != null && Actor.IsPlayer())
			{
				AchievementInfo bestow_LIFE_ = Achievement.BESTOW_LIFE_20;
				if (bestow_LIFE_ == null)
				{
					return;
				}
				StatInfo progress = bestow_LIFE_.Progress;
				if (progress == null)
				{
					return;
				}
				progress.Increment(1);
			}
		}

		// Token: 0x0600A2E3 RID: 41699 RVA: 0x003B87B8 File Offset: 0x003B69B8
		public static bool CanBeAnimated(GameObject obj)
		{
			return obj.HasTagOrProperty("Animatable") && obj.GetMatterPhase() == 1 && !obj.IsTemporary;
		}
	}
}
