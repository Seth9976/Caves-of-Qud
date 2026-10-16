using System;
using System.Text;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200145A RID: 5210
	[Serializable]
	public class Wings : BaseDefaultEquipmentMutation, IFlightSource
	{
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600CBFC RID: 52220 RVA: 0x004A266A File Offset: 0x004A086A
		public int FlightLevel
		{
			get
			{
				return base.Level;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600CBFD RID: 52221 RVA: 0x004A2672 File Offset: 0x004A0872
		public int FlightBaseFallChance
		{
			get
			{
				return this.BaseFallChance;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600CBFE RID: 52222 RVA: 0x004A267A File Offset: 0x004A087A
		public bool FlightRequiresOngoingEffort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600CBFF RID: 52223 RVA: 0x004A267D File Offset: 0x004A087D
		public string FlightEvent
		{
			get
			{
				return Wings.COMMAND_NAME;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600CC00 RID: 52224 RVA: 0x004A2684 File Offset: 0x004A0884
		public string FlightActivatedAbilityClass
		{
			get
			{
				return "Physical Mutation";
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600CC01 RID: 52225 RVA: 0x004A268B File Offset: 0x004A088B
		public string FlightSourceDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600CC02 RID: 52226 RVA: 0x004A268E File Offset: 0x004A088E
		// (set) Token: 0x0600CC03 RID: 52227 RVA: 0x004A2696 File Offset: 0x004A0896
		public bool FlightFlying
		{
			get
			{
				return this._FlightFlying;
			}
			set
			{
				this._FlightFlying = value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600CC04 RID: 52228 RVA: 0x004A269F File Offset: 0x004A089F
		// (set) Token: 0x0600CC05 RID: 52229 RVA: 0x004A26A7 File Offset: 0x004A08A7
		public Guid FlightActivatedAbilityID
		{
			get
			{
				return this._FlightActivatedAbilityID;
			}
			set
			{
				this._FlightActivatedAbilityID = value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600CC06 RID: 52230 RVA: 0x004A26B0 File Offset: 0x004A08B0
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::Wings";
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600CC07 RID: 52231 RVA: 0x004A26C7 File Offset: 0x004A08C7
		public string BlueprintName
		{
			get
			{
				return this.Variant.Coalesce("Wings");
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600CC08 RID: 52232 RVA: 0x004A26DC File Offset: 0x004A08DC
		public GameObjectBlueprint Blueprint
		{
			get
			{
				GameObjectBlueprint gameObjectBlueprint;
				if ((gameObjectBlueprint = this._Blueprint) == null)
				{
					gameObjectBlueprint = (this._Blueprint = GameObjectFactory.Factory.GetBlueprint(this.BlueprintName));
				}
				return gameObjectBlueprint;
			}
		}

		// Token: 0x0600CC09 RID: 52233 RVA: 0x004A270C File Offset: 0x004A090C
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Wings wings = base.DeepCopy(Parent, MapInv) as Wings;
			wings.WingsObject = null;
			return wings;
		}

		// Token: 0x0600CC0B RID: 52235 RVA: 0x004A2747 File Offset: 0x004A0947
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CC0C RID: 52236 RVA: 0x004A274C File Offset: 0x004A094C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetPassiveAbilityListEvent.ID || ID == PooledEvent<AttemptToLandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<BodyPositionChangedEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetJumpingBehaviorEvent>.ID || ID == GetLostChanceEvent.ID || ID == GetMovementCapabilitiesEvent.ID || ID == PooledEvent<GetPropertyModDescription>.ID || ID == PooledEvent<MovementModeChangedEvent>.ID || ID == ObjectStoppedFlyingEvent.ID || ID == ReplicaCreatedEvent.ID || ID == PooledEvent<TransparentToEMPEvent>.ID || ID == TravelSpeedEvent.ID;
		}

		// Token: 0x0600CC0D RID: 52237 RVA: 0x004A2814 File Offset: 0x004A0A14
		public override bool HandleEvent(GetPropertyModDescription E)
		{
			if (E.Property == "ChargeRangeModifier")
			{
				E.AddLinearBonusModifier(this.appliedChargeBonus, "wings");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC0E RID: 52238 RVA: 0x004A2840 File Offset: 0x004A0A40
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("CrashChance", Flight.GetMoveFallChance(this.ParentObject, this), false, 0);
			stats.Set("SwoopCrashChance", Flight.GetSwoopFallChance(this.ParentObject, this), false, 0);
		}

		// Token: 0x0600CC0F RID: 52239 RVA: 0x004A2874 File Offset: 0x004A0A74
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.FlightActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject);
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = ((parentObject != null) ? parentObject.GetActivatedAbilityByCommand(Flight.SWOOP_ATTACK_COMMAND_NAME) : null);
			if (activatedAbilityEntry != null)
			{
				base.DescribeMyActivatedAbility(activatedAbilityEntry.ID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC10 RID: 52240 RVA: 0x004A28E4 File Offset: 0x004A0AE4
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			if (E.Actor == this.ParentObject)
			{
				ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.FlightActivatedAbilityID, null);
				E.Add(activatedAbilityEntry.DisplayName, this.FlightEvent, 19000, activatedAbilityEntry, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC11 RID: 52241 RVA: 0x004A2930 File Offset: 0x004A0B30
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Wings.COMMAND_NAME)
			{
				if (base.IsMyActivatedAbilityToggledOn(this.FlightActivatedAbilityID, null))
				{
					if (this.ParentObject.IsPlayer() && base.currentCell != null && this.ParentObject.GetEffectCount(typeof(Flying)) <= 1)
					{
						int i = 0;
						int count = base.currentCell.Objects.Count;
						while (i < count)
						{
							GameObject gameObject = base.currentCell.Objects[i];
							StairsDown part = gameObject.GetPart<StairsDown>();
							if (part != null && part.IsLongFall() && Popup.WarnYesNo("It looks like a long way down " + gameObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " you're above. Are you sure you want to stop flying?", "Sounds/UI/ui_notification_warning", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								return false;
							}
							i++;
						}
					}
					Flight.StopFlying(this.ParentObject, this.ParentObject, this, false, false);
				}
				else
				{
					if (this.ParentObject.IsEMPed() && MutationsSubjectToEMPEvent.Check(this.ParentObject))
					{
						return this.ParentObject.Fail(this.ParentObject.Poss(this.Blueprint.CachedDisplayNameStripped, true) + " will not move!");
					}
					Flight.StartFlying(this.ParentObject, this.ParentObject, this);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC12 RID: 52242 RVA: 0x004A2A98 File Offset: 0x004A0C98
		public override bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			if (E.AbilityName == "Jump" && !E.CanJumpOverCreatures && base.IsMyActivatedAbilityUsable(this.FlightActivatedAbilityID, null) && !this.ParentObject.HasEffect<Grounded>())
			{
				E.CanJumpOverCreatures = true;
				Templates.StatCollector stats = E.Stats;
				if (stats != null)
				{
					stats.Set("CanJumpOverCreatures", "true", false, 0);
				}
				Templates.StatCollector stats2 = E.Stats;
				if (stats2 != null)
				{
					stats2.AddLinearBonusModifier("Range", this.appliedJumpBonus, "wings");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC13 RID: 52243 RVA: 0x004A2B27 File Offset: 0x004A0D27
		public override bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			Acrobatics_Jump.SyncAbility(this.ParentObject, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC14 RID: 52244 RVA: 0x004A2B3C File Offset: 0x004A0D3C
		public override bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			if (!this.FlightFlying && E.Actor == this.ParentObject && Flight.EnvironmentAllowsFlight(E.Actor) && Flight.IsAbilityAIUsable(this, E.Actor) && (!E.Actor.IsEMPed() || !MutationsSubjectToEMPEvent.Check(E.Actor)))
			{
				E.Add(this.FlightEvent, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC15 RID: 52245 RVA: 0x004A2BB0 File Offset: 0x004A0DB0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.FlightFlying && Flight.EnvironmentAllowsFlight(E.Actor) && Flight.IsAbilityAIUsable(this, E.Actor) && (!E.Actor.IsEMPed() || !MutationsSubjectToEMPEvent.Check(E.Actor)))
			{
				E.Add(this.FlightEvent, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC16 RID: 52246 RVA: 0x004A2C13 File Offset: 0x004A0E13
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return !MutationsSubjectToEMPEvent.Check(this.ParentObject) && base.HandleEvent(E);
		}

		// Token: 0x0600CC17 RID: 52247 RVA: 0x004A2C2B File Offset: 0x004A0E2B
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckEMP();
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC18 RID: 52248 RVA: 0x004A2C3A File Offset: 0x004A0E3A
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckEMP();
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC19 RID: 52249 RVA: 0x004A2C49 File Offset: 0x004A0E49
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			E.PercentageBonus += 36 + 4 * base.Level;
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC1A RID: 52250 RVA: 0x004A2C6A File Offset: 0x004A0E6A
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				Flight.SyncFlying(this.ParentObject, this.ParentObject, this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC1B RID: 52251 RVA: 0x004A2C93 File Offset: 0x004A0E93
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			E.PercentageBonus += 50 + 50 * base.Level;
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC1C RID: 52252 RVA: 0x004A2CB8 File Offset: 0x004A0EB8
		public override bool HandleEvent(BodyPositionChangedEvent E)
		{
			if (this.FlightFlying && E.To != "Flying")
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, this.ParentObject, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, this.ParentObject, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC1D RID: 52253 RVA: 0x004A2D18 File Offset: 0x004A0F18
		public override bool HandleEvent(MovementModeChangedEvent E)
		{
			if (this.FlightFlying && E.To != "Flying")
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, this.ParentObject, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, this.ParentObject, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC1E RID: 52254 RVA: 0x004A2D78 File Offset: 0x004A0F78
		public override bool HandleEvent(AttemptToLandEvent E)
		{
			return (!this.FlightFlying || !Flight.StopFlying(this.ParentObject, this.ParentObject, this, false, false)) && base.HandleEvent(E);
		}

		// Token: 0x0600CC1F RID: 52255 RVA: 0x004A2DA1 File Offset: 0x004A0FA1
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckEMP();
			Flight.MaintainFlight(this.ParentObject, this.ParentObject, this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC20 RID: 52256 RVA: 0x004A2DC3 File Offset: 0x004A0FC3
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckEMP();
			Flight.CheckFlight(this.ParentObject, this.ParentObject, this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC21 RID: 52257 RVA: 0x004A2DE5 File Offset: 0x004A0FE5
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC22 RID: 52258 RVA: 0x004A2E08 File Offset: 0x004A1008
		public override string GetDescription()
		{
			return this.Blueprint.GetTag("VariantDescription", "").Coalesce("You fly.");
		}

		// Token: 0x0600CC23 RID: 52259 RVA: 0x004A2E29 File Offset: 0x004A1029
		public float SprintingMoveSpeedBonus(int Level)
		{
			return 0.1f + 0.1f * (float)Level;
		}

		// Token: 0x0600CC24 RID: 52260 RVA: 0x004A2E39 File Offset: 0x004A1039
		public int GetJumpDistanceBonus(int Level)
		{
			return 1 + Level / 3;
		}

		// Token: 0x0600CC25 RID: 52261 RVA: 0x004A2E40 File Offset: 0x004A1040
		public int GetChargeDistanceBonus(int Level)
		{
			return 2 + Level / 3;
		}

		// Token: 0x0600CC26 RID: 52262 RVA: 0x004A2E48 File Offset: 0x004A1048
		public override string GetLevelText(int Level)
		{
			int num = Math.Max(0, this.FlightBaseFallChance - Level);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("You travel on the world map at {{rules|").Append(1.5 + 0.5 * (double)Level).Append("x}} speed.\n");
			stringBuilder.Append("{{rules|" + (36 + Level * 4).ToString()).Append("%}} reduced chance of becoming lost\n");
			stringBuilder.Append("While outside, you may fly. You cannot be hit in melee by grounded creatures while flying.\n");
			stringBuilder.Append("{{rules|" + num.ToString()).Append("%}} chance of falling clumsily to the ground\n");
			stringBuilder.Append("{{rules|" + ((int)(this.SprintingMoveSpeedBonus(Level) * 100f)).Signed(false) + "%}} move speed while sprinting\n");
			stringBuilder.Append("You can jump {{rules|" + this.GetJumpDistanceBonus(Level).ToString() + ((this.GetJumpDistanceBonus(Level) == 1) ? "}} square" : "}} squares") + " farther.\n");
			stringBuilder.Append("You can charge {{rules|" + this.GetChargeDistanceBonus(Level).ToString() + ((this.GetChargeDistanceBonus(Level) == 1) ? "}} square" : "}} squares") + " farther.\n");
			stringBuilder.Append("+300 reputation with {{w|birds}} and {{w|winged mammals}}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600CC27 RID: 52263 RVA: 0x004A2FA4 File Offset: 0x004A11A4
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			string partParameter = this.Blueprint.GetPartParameter<string>("Armor", "WornOn", "Back");
			BodyPart bodyPart;
			if (!base.TryGetRegisteredSlot(body, partParameter, out bodyPart, false))
			{
				bodyPart = body.GetFirstPart(partParameter) ?? this.AddBodyPart(body);
				if (bodyPart != null)
				{
					base.RegisterSlot(partParameter, bodyPart);
				}
			}
			if (bodyPart != null)
			{
				bodyPart.Description = this.Blueprint.GetTag("PartDescription", "Worn around Wings");
				bodyPart.DescriptionPrefix = null;
				bodyPart.DefaultBehavior = GameObject.Create(this.Blueprint, 0, 0, null, null, null, null, null);
				bodyPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "Wings", false);
			}
		}

		// Token: 0x0600CC28 RID: 52264 RVA: 0x004A304C File Offset: 0x004A124C
		public BodyPart AddBodyPart(Body Body)
		{
			BodyPart body = Body.GetBody();
			string partParameter = this.Blueprint.GetPartParameter<string>("Armor", "WornOn", "Back");
			BodyPart bodyPart = body;
			string text = partParameter;
			int num = 0;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			int? num2 = new int?(body.Category);
			string managerID = this.ManagerID;
			int? num3 = num2;
			string tag = this.Blueprint.GetTag("InsertPartAfter", "Head");
			string[] array = new string[] { "Arm", "Missile Weapon", "Hands" };
			return bodyPart.AddPartAt(text, num, text2, text3, text4, text5, managerID, num3, null, null, null, null, null, null, null, null, null, null, null, null, tag, array, true);
		}

		// Token: 0x0600CC29 RID: 52265 RVA: 0x004A3150 File Offset: 0x004A1350
		public override bool ChangeLevel(int NewLevel)
		{
			if (this.appliedChargeBonus > 0)
			{
				this.ParentObject.ModIntProperty("ChargeRangeModifier", -this.appliedChargeBonus, false);
			}
			if (this.appliedJumpBonus > 0)
			{
				this.ParentObject.ModIntProperty("JumpRangeModifier", -this.appliedJumpBonus, false);
			}
			this.appliedChargeBonus = this.GetChargeDistanceBonus(NewLevel);
			this.appliedJumpBonus = this.GetJumpDistanceBonus(NewLevel);
			this.ParentObject.ModIntProperty("ChargeRangeModifier", this.appliedChargeBonus, false);
			this.ParentObject.ModIntProperty("JumpRangeModifier", this.appliedJumpBonus, false);
			Acrobatics_Jump.SyncAbility(this.ParentObject, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CC2A RID: 52266 RVA: 0x004A31FE File Offset: 0x004A13FE
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			this._Blueprint = null;
		}

		// Token: 0x0600CC2B RID: 52267 RVA: 0x004A320E File Offset: 0x004A140E
		public override bool Mutate(GameObject GO, int Level)
		{
			Flight.AbilitySetup(GO, GO, this);
			Acrobatics_Jump.SyncAbility(GO, true);
			GO.WantToReequip();
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CC2C RID: 52268 RVA: 0x004A3230 File Offset: 0x004A1430
		public override bool Unmutate(GameObject GO)
		{
			if (this.FlightFlying)
			{
				Flight.FailFlying(GO, GO, this);
			}
			GO.ModIntProperty("ChargeRangeModifier", -this.appliedChargeBonus, true);
			GO.ModIntProperty("JumpRangeModifier", -this.appliedJumpBonus, true);
			GO.RemoveBodyPartsByManager(this.ManagerID, true);
			this.appliedChargeBonus = 0;
			this.appliedJumpBonus = 0;
			Flight.AbilityTeardown(GO, GO, this);
			Acrobatics_Jump.SyncAbility(GO, false);
			GO.WantToReequip();
			return base.Unmutate(GO);
		}

		// Token: 0x0600CC2D RID: 52269 RVA: 0x004A32AF File Offset: 0x004A14AF
		public void CheckEMP()
		{
			if (this.FlightFlying && this.ParentObject.IsEMPed() && MutationsSubjectToEMPEvent.Check(this.ParentObject))
			{
				Flight.FailFlying(this.ParentObject, this.ParentObject, this);
			}
		}

		// Token: 0x04004F2E RID: 20270
		public static readonly string COMMAND_NAME = "CommandFlight";

		// Token: 0x04004F2F RID: 20271
		public GameObject WingsObject;

		// Token: 0x04004F30 RID: 20272
		public string BodyPartType = "Back";

		// Token: 0x04004F31 RID: 20273
		public int BaseFallChance = 6;

		// Token: 0x04004F32 RID: 20274
		public bool _FlightFlying;

		// Token: 0x04004F33 RID: 20275
		public Guid _FlightActivatedAbilityID = Guid.Empty;

		// Token: 0x04004F34 RID: 20276
		[NonSerialized]
		protected GameObjectBlueprint _Blueprint;

		// Token: 0x04004F35 RID: 20277
		public int appliedChargeBonus;

		// Token: 0x04004F36 RID: 20278
		public int appliedJumpBonus;
	}
}
