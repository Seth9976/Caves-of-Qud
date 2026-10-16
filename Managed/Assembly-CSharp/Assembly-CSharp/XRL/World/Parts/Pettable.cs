using System;
using ConsoleLib.Console;
using XRL.World.AI.GoalHandlers;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000EED RID: 3821
	[Serializable]
	public class Pettable : IPart
	{
		// Token: 0x060098AF RID: 39087 RVA: 0x00376EF9 File Offset: 0x003750F9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098B0 RID: 39088 RVA: 0x00376EFC File Offset: 0x003750FC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060098B1 RID: 39089 RVA: 0x00376F34 File Offset: 0x00375134
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Pet", "pet", "Pet", null, 'p', false, 5, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060098B2 RID: 39090 RVA: 0x00376F6B File Offset: 0x0037516B
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Pet" && this.Pet(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060098B3 RID: 39091 RVA: 0x00376F9A File Offset: 0x0037519A
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return ((E.Actor.IsPlayer() && this.ParentObject.IsPlayerLed() && this.ParentObject.IsMobile()) || this.PreferConversation()) && base.HandleEvent(E);
		}

		// Token: 0x060098B4 RID: 39092 RVA: 0x00376FD4 File Offset: 0x003751D4
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if ((!E.Actor.IsPlayer() || !this.ParentObject.IsPlayerLed() || !this.ParentObject.IsMobile()) && !this.PreferConversation())
			{
				this.Pet(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060098B5 RID: 39093 RVA: 0x00377024 File Offset: 0x00375224
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return !this.CheckAIPetting(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x060098B6 RID: 39094 RVA: 0x0037703D File Offset: 0x0037523D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098B7 RID: 39095 RVA: 0x00377040 File Offset: 0x00375240
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanHaveSmartUseConversation");
			base.Register(Object, Registrar);
		}

		// Token: 0x060098B8 RID: 39096 RVA: 0x00377055 File Offset: 0x00375255
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CanHaveSmartUseConversation") || this.PreferConversation()) && base.FireEvent(E);
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x0037707C File Offset: 0x0037527C
		public bool Pet(GameObject Actor)
		{
			if (!Actor.CanMoveExtremities("Pet", true, false, false))
			{
				return false;
			}
			string text;
			if (!Pettable.CanPet(Actor, out text))
			{
				if (!text.IsNullOrEmpty())
				{
					Actor.Fail(text);
				}
				return false;
			}
			BodyPart bodyPart;
			BodyPart bodyPart2;
			if (!Pettable.TryGetLimbs(Actor, this.ParentObject, out bodyPart, out bodyPart2))
			{
				return Actor.ShowFailure("You lack the means to do that.");
			}
			if (!this.WillingToBePettedBy(Actor))
			{
				Actor.Fail(this.ParentObject.Does("shy", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " away from you.");
				return false;
			}
			CombatJuiceEntryPunch combatJuiceEntryPunch = CombatJuice.punch(Actor, this.ParentObject, 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f, 0.5f, "PetSound", "Sounds/Interact/sfx_interact_pet", true, true);
			if (Actor.IsPlayer())
			{
				CombatJuice.BlockUntilFinished(combatJuiceEntryPunch, null, 500, true);
			}
			IComponent<GameObject>.XDidYToZ(Actor, "pet", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
			string text2 = this.ParentObject.GetPropertyOrTag("PetResponse", "=subject.T= =verb:stare= at =object.t= blankly.");
			if (!text2.IsNullOrEmpty())
			{
				if (text2.Contains(","))
				{
					text2 = text2.CachedCommaExpansion().GetRandomElement<string>(null);
				}
				if (!text2.Contains("=") && !text2.StartsWith("*"))
				{
					if (text2.Contains(" you "))
					{
						text2 = text2.Replace(" you ", " =object.t= ");
					}
					if (text2.Contains(" you."))
					{
						text2 = text2.Replace(" you.", " =object.t=.");
					}
					if (text2.Contains(" you!"))
					{
						text2 = text2.Replace(" you!", " =object.t=!");
					}
					if (text2.Contains(" your "))
					{
						text2 = text2.Replace(" your ", " =objpronouns.possessive= ");
					}
					if (text2.Contains(" yours "))
					{
						text2 = text2.Replace(" your ", " =objpronouns.substantivePossessive= ");
					}
					if (text2.Contains(" yours."))
					{
						text2 = text2.Replace(" yours.", " =objpronouns.substantivePossessive=.");
					}
					if (text2.Contains(" yours!"))
					{
						text2 = text2.Replace(" yours!", " =objpronouns.substantivePossessive=!");
					}
					text2 = "=subject.T= " + text2;
					char c = ColorUtility.LastCharacterExceptFormatting(text2);
					if (c != '.' && c != '!' && c != '?')
					{
						text2 += ".";
					}
				}
				text2 = GameText.VariableReplace(text2, this.ParentObject, Actor, true);
				if (!text2.IsNullOrEmpty())
				{
					base.EmitMessage(text2, ' ', false, Actor.IsPlayer() || this.ParentObject.IsPlayer(), false, null, null);
				}
			}
			Actor.UseEnergy(1000, "Petting", null, null, false);
			AfterPetEvent.Send(this.ParentObject, Actor);
			PhysicalContactEvent.Send(Actor, this.ParentObject, bodyPart, bodyPart2);
			return true;
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x0037738C File Offset: 0x0037558C
		public static bool TryGetLimbs(GameObject Actor, GameObject Object, out BodyPart ActorLimb, out BodyPart ObjectLimb)
		{
			BodyPart bodyPart;
			if ((bodyPart = Actor.Body.GetFirstPart("Hand")) == null)
			{
				bodyPart = Actor.Body.GetFirstPart((BodyPart x) => x.Appendage);
			}
			ActorLimb = bodyPart;
			ObjectLimb = Object.Body.GetBody();
			if (50.in100())
			{
				ObjectLimb = Object.Body.GetFirstPart("Head") ?? ObjectLimb;
			}
			return ActorLimb != null && ObjectLimb != null;
		}

		// Token: 0x060098BB RID: 39099 RVA: 0x00377414 File Offset: 0x00375614
		public bool WillingToBePettedBy(GameObject Actor)
		{
			if (this.PettableIfPositiveFeeling && this.ParentObject.Brain.GetFeeling(Actor) < 0)
			{
				return false;
			}
			if (this.UseFactionForFeelingFloor == null)
			{
				if (this.OnlyAllowIfLiked && Actor != null)
				{
					Brain brain = this.ParentObject.Brain;
					if (((brain != null) ? brain.GetFeeling(Actor) : 0) < 50)
					{
						return false;
					}
				}
			}
			else if (!this.PettableIfPositiveFeeling)
			{
				if (this.ParentObject.Brain.GetFeeling(Actor) >= 0)
				{
					if (!Actor.IsPlayer())
					{
						return true;
					}
					int feeling = The.Game.PlayerReputation.GetFeeling(this.UseFactionForFeelingFloor);
					Brain brain2 = this.ParentObject.Brain;
					if (Math.Max(feeling, (brain2 != null) ? brain2.GetFeeling(Actor) : 0) >= 50)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060098BC RID: 39100 RVA: 0x003774CD File Offset: 0x003756CD
		public static bool CanPet(GameObject Actor, out string FailureMessage)
		{
			if (Actor.Brain == null || Actor.HasTagOrProperty("NoPetting"))
			{
				FailureMessage = "You cannot do that.";
				return false;
			}
			FailureMessage = null;
			return true;
		}

		// Token: 0x060098BD RID: 39101 RVA: 0x003774F4 File Offset: 0x003756F4
		public static bool CanPet(GameObject Actor)
		{
			string text;
			return Pettable.CanPet(Actor, out text);
		}

		// Token: 0x060098BE RID: 39102 RVA: 0x0037750C File Offset: 0x0037570C
		public static bool PreferConversation(GameObject Actor)
		{
			return Actor.GetIntProperty("NamedVillager", 0) > 0 || Actor.GetIntProperty("ParticipantVillager", 0) > 0 || Actor.GetIntProperty("Hero", 0) > 0 || Actor.GetIntProperty("Librarian", 0) > 0 || Actor.HasTagOrProperty("PreferChatToPet");
		}

		// Token: 0x060098BF RID: 39103 RVA: 0x0037756D File Offset: 0x0037576D
		public bool PreferConversation()
		{
			return Pettable.PreferConversation(this.ParentObject);
		}

		// Token: 0x060098C0 RID: 39104 RVA: 0x0037757C File Offset: 0x0037577C
		public bool CheckAIPetting(GameObject Subject)
		{
			if (!GameObject.Validate(Subject))
			{
				return false;
			}
			if (Subject == this.ParentObject)
			{
				return false;
			}
			if (this.ParentObject.CurrentZone == null)
			{
				return false;
			}
			if (!Pettable.CanPet(Subject))
			{
				return false;
			}
			if (!this.WillingToBePettedBy(Subject))
			{
				return false;
			}
			if (!this.ParentObject.GetIntProperty("PetGoalChance", Pettable.DEFAULT_PET_GOAL_CHANCE).in100())
			{
				return false;
			}
			if (Subject.InSameOrAdjacentCellTo(this.ParentObject))
			{
				InventoryActionEvent.Check(this.ParentObject, Subject, this.ParentObject, "Pet", false, false, false, false, false, 0, 0, 0, null, null, null, null);
			}
			else if ((this.MaxAIDistance < 0 || Subject.DistanceTo(this.ParentObject) < this.MaxAIDistance) && Subject.IsMobile())
			{
				if (Subject.GetIntProperty("AIPetWait", 0) > 0)
				{
					if (Subject.ModIntProperty("AIPetWait", -1, true) > 0)
					{
						return false;
					}
				}
				else
				{
					Subject.Brain.PushGoal(new Pet(this.ParentObject, 5, this.MaxAIDistance));
					int intProperty = this.ParentObject.GetIntProperty("PetGoalWait", Pettable.DEFAULT_PET_GOAL_WAIT);
					Subject.SetIntProperty("AIPetWait", intProperty, false);
				}
			}
			return true;
		}

		// Token: 0x04003F95 RID: 16277
		public static readonly int DEFAULT_PET_GOAL_CHANCE = 10;

		// Token: 0x04003F96 RID: 16278
		public static readonly int DEFAULT_PET_GOAL_WAIT = 100;

		// Token: 0x04003F97 RID: 16279
		public string UseFactionForFeelingFloor;

		// Token: 0x04003F98 RID: 16280
		public bool PettableIfPositiveFeeling;

		// Token: 0x04003F99 RID: 16281
		public bool OnlyAllowIfLiked = true;

		// Token: 0x04003F9A RID: 16282
		public int MaxAIDistance = 5;
	}
}
