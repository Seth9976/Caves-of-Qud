using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020010CC RID: 4300
	[Serializable]
	public class Tonic : IPart
	{
		// Token: 0x0600AB07 RID: 43783 RVA: 0x003E8D68 File Offset: 0x003E6F68
		public override bool SameAs(IPart p)
		{
			Tonic tonic = p as Tonic;
			return tonic.CausesOverdose == this.CausesOverdose && tonic.Eat == this.Eat && !(tonic.BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600AB08 RID: 43784 RVA: 0x003E8DB8 File Offset: 0x003E6FB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == GetInventoryActionsAlwaysEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AB09 RID: 43785 RVA: 0x003E8DE6 File Offset: 0x003E6FE6
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.BehaviorDescription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB0A RID: 43786 RVA: 0x003E8E04 File Offset: 0x003E7004
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			if (!E.Object.IsBroken() && !E.Object.IsRusted())
			{
				int num = 0;
				if (E.Object.HasPart<Empty_Tonic_Applicator>())
				{
					num = -100;
				}
				else if (E.Object.Equipped == E.Actor || E.Object.InInventory == E.Actor)
				{
					if (E.Object.IsImportant())
					{
						num = -1;
					}
					else if (E.Actor.FireEvent("CanApplyTonic") && (!this.Eat || E.Actor.HasPart<Stomach>()))
					{
						num = 100;
					}
				}
				if (this.Eat)
				{
					E.AddAction("Eat", "eat", "Apply", null, 'e', false, num, 0, true, false, false, false, true, null, false);
					if (!E.Actor.OnWorldMap())
					{
						E.AddAction("Feed To", "feed to", "ApplyTo", null, 'f', false, -2, 0, true, false, false, false, true, null, false);
					}
				}
				else
				{
					E.AddAction("Apply", "apply", "Apply", null, 'a', false, num, 0, false, false, false, false, true, null, false);
					if (!E.Actor.OnWorldMap())
					{
						E.AddAction("Apply To", "apply to", "ApplyTo", null, 'a', false, -2, 0, false, false, false, false, true, null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB0B RID: 43787 RVA: 0x003E8F60 File Offset: 0x003E7160
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply" || E.Command == "ApplyInvoluntarily" || E.Command == "ApplyExternally")
			{
				bool flag = E.Command == "ApplyInvoluntarily" || E.Command == "ApplyExternally";
				bool flag2 = E.Command == "ApplyInvoluntarily";
				GameObject gameObject = E.ObjectTarget ?? E.Actor;
				if (this.Eat && !gameObject.HasPart<Stomach>())
				{
					E.Actor.Fail(gameObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unable to consume tonics.");
					return false;
				}
				if (E.Item.IsBroken())
				{
					E.Actor.Fail(E.Item.Itis + " broken...");
					return false;
				}
				if (E.Item.IsRusted())
				{
					E.Actor.Fail(E.Item.Itis + " rusted...");
					return false;
				}
				if (!flag2 && !flag && !E.Item.ConfirmUseImportant(E.Actor, "use", "up", 1))
				{
					return false;
				}
				E.Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_applicator_apply", null);
				int @for = GetTonicDosageEvent.GetFor(this.ParentObject, gameObject, E.Actor, 1);
				Event @event = Event.New("ApplyingTonic", 0, 0, 0);
				@event.SetParameter("Subject", gameObject);
				@event.SetParameter("Actor", E.Actor);
				@event.SetParameter("Tonic", this.ParentObject);
				@event.SetParameter("Dosage", @for);
				@event.SetFlag("External", flag);
				@event.SetFlag("Involuntary", flag2);
				@event.SetFlag("ShowMessage", true);
				if (!gameObject.FireEvent(@event))
				{
					return false;
				}
				if (!BeforeConsumeEvent.Check(E.Actor, gameObject, this.ParentObject, this.Eat, false, !this.Eat, false, false, !flag2))
				{
					return false;
				}
				string text = null;
				if (E.Actor.IsPlayer() || IComponent<GameObject>.Visible(E.Actor))
				{
					this.ParentObject.MakeUnderstood(out text);
				}
				List<Effect> tonicEffects = gameObject.GetTonicEffects();
				int tonicCapacity = gameObject.GetTonicCapacity();
				if (tonicEffects.Count >= tonicCapacity && this.CausesOverdose)
				{
					foreach (Effect effect in tonicEffects)
					{
						if (!gameObject.MakeSave("Toughness", 16 + 3 * (tonicEffects.Count - tonicCapacity), null, null, "Overdose", false, false, false, false, this.ParentObject))
						{
							Event event2 = Event.New("Overdose", 0, 0, 0);
							event2.SetParameter("Subject", gameObject);
							event2.SetParameter("Actor", E.Actor);
							event2.SetParameter("Tonic", this.ParentObject);
							event2.SetParameter("Dosage", @for);
							event2.SetFlag("External", flag);
							event2.SetFlag("Involuntary", flag2);
							effect.FireEvent(event2);
						}
					}
				}
				bool flag3 = false;
				string text2 = "No";
				if (gameObject.IsMutant())
				{
					int num = 5;
					if (gameObject.HasPart<TonicAllergy>())
					{
						num = 33;
					}
					if (gameObject.IsMutant() && num.in100())
					{
						gameObject.SetLongProperty("Overdosing", 1L);
						flag3 = true;
					}
				}
				try
				{
					Event event3 = Event.New("ApplyTonic", 0, 0, 0);
					event3.SetParameter("Subject", gameObject);
					event3.SetParameter("Target", gameObject);
					event3.SetParameter("Actor", E.Actor);
					event3.SetParameter("Owner", E.Actor);
					event3.SetParameter("Attacker", E.Actor);
					event3.SetParameter("Overdose", text2);
					event3.SetParameter("Dosage", @for);
					event3.SetFlag("External", flag);
					event3.SetFlag("Involuntary", flag2);
					if (this.ParentObject.FireEvent(event3))
					{
						AfterConsumeEvent.Send(E.Actor, gameObject, this.ParentObject, this.Eat, false, !this.Eat, false, false, !flag2);
						if (this.Eat)
						{
							Event event4 = Event.New("Eating", "Food", this.ParentObject, "Subject", gameObject);
							gameObject.FireEvent(event4);
							if (!flag)
							{
								gameObject.UseEnergy(1000, "Item Eat", null, null, false);
							}
						}
						if (!flag && !gameObject.IsPlayer() && IComponent<GameObject>.Visible(gameObject))
						{
							IComponent<GameObject>.AddPlayerMessage(gameObject.Does(this.Eat ? "eat" : "apply", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
						}
						this.ParentObject.Destroy(null, true, false, null);
					}
					goto IL_0B81;
				}
				finally
				{
					if (flag3)
					{
						gameObject.SetLongProperty("Overdosing", 0L);
					}
					if (!text.IsNullOrEmpty())
					{
						Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
			}
			if (E.Command == "ApplyTo")
			{
				if (E.Item.IsBroken())
				{
					return E.Actor.Fail(E.Item.Itis + " broken...");
				}
				if (E.Item.IsRusted())
				{
					return E.Actor.Fail(E.Item.Itis + " rusted...");
				}
				if (E.Actor.OnWorldMap())
				{
					return E.Actor.Fail("You cannot do that on the world map.");
				}
				if (!E.Item.ConfirmUseImportant(E.Actor, "use", "up", 1))
				{
					return false;
				}
				bool flag4 = false;
				GameObject gameObject2 = E.Actor;
				Cell cell = base.PickDirection(flag4, "Apply " + E.Item.GetDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true), gameObject2);
				if (cell == null)
				{
					return false;
				}
				GameObject gameObject3 = cell.GetCombatTarget(E.Actor, false, false, false, 0, null, null, null, null, null, false, false, null);
				if (gameObject3 == null)
				{
					gameObject3 = cell.GetCombatTarget(E.Actor, true, false, false, 5, null, null, null, null, null, false, false, null);
					if (gameObject3 == null)
					{
						return E.Actor.Fail(string.Concat(new string[]
						{
							"There is no one there you can ",
							this.Eat ? "feed" : "apply",
							" ",
							this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" to."
						}));
					}
					if (cell.GetCombatTarget(E.Actor, true, false, false, 0, null, null, null, null, null, false, false, null) == null)
					{
						return E.Actor.Fail("You are out of phase with " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					}
					return E.Actor.Fail("You cannot reach " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				else
				{
					Event event5 = Event.New("CanApplyTonic", 0, 0, 0);
					event5.SetParameter("Subject", gameObject3);
					event5.SetParameter("Actor", E.Actor);
					event5.SetParameter("Tonic", this.ParentObject);
					event5.SetFlag("External", true);
					event5.SetFlag("ShowMessage", true);
					if (!gameObject3.FireEvent(event5))
					{
						return false;
					}
					if (gameObject3 == E.Actor)
					{
						if (this.Eat)
						{
							return E.Actor.Fail(string.Concat(new string[]
							{
								"If you want to eat ",
								this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" ",
								E.Actor.itself,
								", you can do so through the eat action."
							}));
						}
						return E.Actor.Fail(string.Concat(new string[]
						{
							"If you want to apply ",
							this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" to ",
							E.Actor.itself,
							", you can do so through the apply action."
						}));
					}
					else if (gameObject3.IsHostileTowards(E.Actor) || (!gameObject3.IsLedBy(E.Actor) && GetUtilityScoreEvent.GetFor(gameObject3, this.ParentObject, null, true) <= 0))
					{
						if (this.Eat)
						{
							return E.Actor.Fail(gameObject3.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not want to consume " + this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
						}
						return E.Actor.Fail(string.Concat(new string[]
						{
							gameObject3.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" not want ",
							this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" applied to ",
							gameObject3.them,
							". You'll need to equip ",
							this.ParentObject.them,
							" as a weapon and attack with ",
							this.ParentObject.them,
							"."
						}));
					}
					else
					{
						this.ParentObject.SplitFromStack();
						IComponent<GameObject>.WDidXToYWithZ(E.Actor, this.Eat ? "feed" : "apply", this.ParentObject, "to", gameObject3, null, null, null, null, gameObject3, null, false, false, false, false, false, false, false, false, null, null, null, null, false, false, false, false, false, null);
						GameObject parentObject = this.ParentObject;
						GameObject actor = E.Actor;
						GameObject parentObject2 = this.ParentObject;
						string text3 = "ApplyExternally";
						bool flag5 = false;
						bool flag6 = false;
						gameObject2 = gameObject3;
						InventoryActionEvent.Check(parentObject, actor, parentObject2, text3, flag5, flag6, !this.Eat, false, false, 0, 0, 0, gameObject2, null, null, null);
						E.Actor.UseEnergy(1000, "Item ApplyTo", null, null, false);
						E.RequestInterfaceExit();
					}
				}
			}
			IL_0B81:
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB0C RID: 43788 RVA: 0x003E9B2C File Offset: 0x003E7D2C
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && 50.in100() && !this.Eat && !this.ParentObject.IsBroken() && !this.ParentObject.IsRusted())
			{
				IComponent<GameObject>.XDidY(E.Actor, "accidentally prick", E.Actor.itself + " with " + this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, null, null, null, null, false, false, null, null, false, false, false, false, E.Actor.IsPlayer(), null);
				if (InventoryActionEvent.Check(this.ParentObject, E.Actor, this.ParentObject, "Apply", false, false, false, false, false, 0, 0, 0, null, null, null, null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB0D RID: 43789 RVA: 0x003E9C27 File Offset: 0x003E7E27
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB0E RID: 43790 RVA: 0x003E9C2A File Offset: 0x003E7E2A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			Registrar.Register("ThrownProjectileHit");
			Registrar.Register("WeaponAfterDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB0F RID: 43791 RVA: 0x003E9C58 File Offset: 0x003E7E58
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "ProjectileHit" || E.ID == "ThrownProjectileHit" || E.ID == "WeaponAfterDamage") && !this.Eat)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (GameObject.Validate(ref gameObjectParameter))
				{
					if (E.GetIntParameter("Penetrations", 0) > 0 && !base.IsBroken() && !base.IsRusted())
					{
						if (gameObjectParameter.IsCreature)
						{
							GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
							InventoryActionEvent.Check(this.ParentObject, gameObjectParameter2, this.ParentObject, "ApplyInvoluntarily", false, false, true, false, false, 0, 0, 0, gameObjectParameter, null, null, null);
						}
					}
					else
					{
						if (IComponent<GameObject>.Visible(gameObjectParameter))
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								this.ParentObject.Does("fail", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" to penetrate ",
								gameObjectParameter.poss("armor", true),
								" and",
								this.ParentObject.Is,
								" destroyed."
							}), null, true);
						}
						this.ParentObject.Destroy(null, true, false, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004683 RID: 18051
		public bool CausesOverdose = true;

		// Token: 0x04004684 RID: 18052
		public bool Eat;

		// Token: 0x04004685 RID: 18053
		public string BehaviorDescription = "This item is a tonic. Applying one tonic while under the effects of another may produce undesired results.";
	}
}
