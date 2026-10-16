using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F40 RID: 3904
	[Serializable]
	public class CyberneticsPrecisionForceLathe : IPoweredPart
	{
		// Token: 0x06009BBA RID: 39866 RVA: 0x00387BDD File Offset: 0x00385DDD
		public CyberneticsPrecisionForceLathe()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "PrecisionForceLathe";
		}

		// Token: 0x06009BBB RID: 39867 RVA: 0x00387C14 File Offset: 0x00385E14
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BBC RID: 39868 RVA: 0x00387C17 File Offset: 0x00385E17
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == ImplantedEvent.ID || ID == PooledEvent<ReplaceThrownWeaponEvent>.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BBD RID: 39869 RVA: 0x00387C58 File Offset: 0x00385E58
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.CommandID.IsNullOrEmpty() && GameObject.Validate(E.Actor) && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && this.IsObjectActivePartSubject(E.Actor) && CyberneticsPrecisionForceLathe.GetTargetBodyPart(E.Actor) != null && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				E.Add(this.CommandID, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BBE RID: 39870 RVA: 0x00387CEC File Offset: 0x00385EEC
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (!(E.Object.Blueprint == this.Blueprint) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || !this.IsObjectActivePartSubject(E.Object.Equipped)) && base.HandleEvent(E);
		}

		// Token: 0x06009BBF RID: 39871 RVA: 0x00387D58 File Offset: 0x00385F58
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.CommandID == null)
			{
				this.CommandID = Guid.NewGuid().ToString();
			}
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Fabricate Force Knife", this.CommandID, "Cybernetics", null, "û", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandFabricateForceKnife", null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BC0 RID: 39872 RVA: 0x00387DCB File Offset: 0x00385FCB
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BC1 RID: 39873 RVA: 0x00387DE6 File Offset: 0x00385FE6
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == this.CommandID) || this.ActivatePrecisionForceLathe(E.Actor, E.Target, E)) && base.HandleEvent(E);
		}

		// Token: 0x06009BC2 RID: 39874 RVA: 0x00387E1C File Offset: 0x0038601C
		public override bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			GameObject previouslyEquipped = E.PreviouslyEquipped;
			if (((previouslyEquipped != null) ? previouslyEquipped.Blueprint : null) == this.Blueprint && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				base.ConsumeCharge(null, null);
				if (this.CheckRealityDistortion(E.Actor, E))
				{
					GameObject gameObject = this.GenerateObject();
					if (E.EquipOrDestroy(gameObject, true))
					{
						IComponent<GameObject>.XDidYToZ(gameObject, "shimmer", "into existence in", E.Actor, "grasp", null, null, null, null, null, false, true, false, false, true, null, null, null, false, false, false, false, false, null);
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BC3 RID: 39875 RVA: 0x00387F07 File Offset: 0x00386107
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009BC4 RID: 39876 RVA: 0x00387F0C File Offset: 0x0038610C
		public bool ActivatePrecisionForceLathe(GameObject Actor, GameObject Target = null, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (!this.IsObjectActivePartSubject(Actor))
			{
				return false;
			}
			if (!Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return false;
			}
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
			}
			BodyPart targetBodyPart = CyberneticsPrecisionForceLathe.GetTargetBodyPart(Actor);
			GameObject gameObject = this.GenerateObject();
			if (targetBodyPart == null)
			{
				string text = "You have no place available to hold " + (((gameObject != null) ? gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) : null) ?? "the result") + ".";
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				return Actor.Fail(text);
			}
			base.ConsumeCharge(null, null);
			if (!this.CheckRealityDistortion(Actor, FromEvent))
			{
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				return false;
			}
			Actor.ReceiveObject(gameObject, false, null);
			if (!GameObject.Validate(gameObject) || gameObject.InInventory != Actor)
			{
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				return Actor.Fail("Something went wrong.");
			}
			Event @event = Event.New("CommandEquipObject", 0, 0, 0);
			@event.SetParameter("Object", gameObject);
			@event.SetParameter("BodyPart", targetBodyPart);
			@event.SetSilent(true);
			if (!Actor.FireEvent(@event))
			{
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				return Actor.Fail("Something went wrong.");
			}
			IComponent<GameObject>.XDidYToZ(gameObject, "shimmer", "into existence in", Actor, (targetBodyPart.Type == "Thrown Weapon") ? "grasp" : targetBodyPart.GetOrdinalName(), null, null, null, null, null, false, true, false, false, true, null, null, null, false, false, false, false, false, null);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x06009BC5 RID: 39877 RVA: 0x00388128 File Offset: 0x00386328
		private bool CheckRealityDistortion(GameObject Actor, IEvent FromEvent = null)
		{
			Event @event = Event.New("InitiateRealityDistortionLocal", 0, 0, 0);
			@event.SetParameter("Object", Actor);
			@event.SetParameter("Device", this);
			return Actor.FireEvent(@event, FromEvent);
		}

		// Token: 0x06009BC6 RID: 39878 RVA: 0x00388168 File Offset: 0x00386368
		private GameObject GenerateObject()
		{
			GameObject gameObject = GameObject.Create(this.Blueprint);
			gameObject.SetIntProperty("NeverStack", 1, false);
			gameObject.RemovePart<ExistenceSupport>();
			ExistenceSupport existenceSupport = gameObject.RequirePart<ExistenceSupport>(false);
			existenceSupport.SupportedBy = this.ParentObject;
			existenceSupport.ValidateEveryTurn = true;
			existenceSupport.SilentRemoval = true;
			return gameObject;
		}

		// Token: 0x06009BC7 RID: 39879 RVA: 0x003881B5 File Offset: 0x003863B5
		public static BodyPart GetTargetBodyPart(GameObject Subject)
		{
			if (Subject == null)
			{
				return null;
			}
			return Subject.GetUnequippedPreferredBodyPartOrAlternate("Thrown Weapon", "Hand");
		}

		// Token: 0x06009BC8 RID: 39880 RVA: 0x003881CC File Offset: 0x003863CC
		public BodyPart GetTargetBodyPart()
		{
			return CyberneticsPrecisionForceLathe.GetTargetBodyPart(this.ParentObject.Implantee);
		}

		// Token: 0x04004088 RID: 16520
		public string Blueprint = "ForceKnife";

		// Token: 0x04004089 RID: 16521
		public string CommandID;

		// Token: 0x0400408A RID: 16522
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
