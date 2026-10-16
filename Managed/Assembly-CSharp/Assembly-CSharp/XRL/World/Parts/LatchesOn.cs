using System;
using XRL.Language;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to
	/// true, which it is not by default, save targets and damage from auto
	/// attacks are increased by the standard power load bonus, i.e. 2 for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x020010E4 RID: 4324
	[Serializable]
	public class LatchesOn : IActivePart
	{
		// Token: 0x0600AB91 RID: 43921 RVA: 0x003ECC7C File Offset: 0x003EAE7C
		public LatchesOn()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB92 RID: 43922 RVA: 0x003ECD08 File Offset: 0x003EAF08
		public override bool SameAs(IPart Part)
		{
			LatchesOn latchesOn = Part as LatchesOn;
			return latchesOn.LatchedOnto == null && this.LatchedOnto == null && latchesOn.InitialSaveTarget == this.InitialSaveTarget && !(latchesOn.InitialSaveStat != this.InitialSaveStat) && !(latchesOn.InitialSaveDifficultyStat != this.InitialSaveDifficultyStat) && latchesOn.MoveSaveTarget == this.MoveSaveTarget && !(latchesOn.MoveSaveStat != this.MoveSaveStat) && !(latchesOn.MoveSaveDifficultyStat != this.MoveSaveDifficultyStat) && latchesOn.BreakSaveTarget == this.BreakSaveTarget && !(latchesOn.BreakSaveStat != this.BreakSaveStat) && !(latchesOn.BreakSaveDifficultyStat != this.BreakSaveDifficultyStat) && !(latchesOn.Duration != this.Duration) && !(latchesOn.RequiresPartOnTargetOrEquipment != this.RequiresPartOnTargetOrEquipment) && !(latchesOn.SuppressedByPartOnTargetOrEquipment != this.SuppressedByPartOnTargetOrEquipment) && latchesOn.AutoAttackOnMoveAway == this.AutoAttackOnMoveAway && latchesOn.AutoAttackOnMove == this.AutoAttackOnMove && latchesOn.AutoAttackPerTurn == this.AutoAttackPerTurn && latchesOn.BloodSpatterOnLatch == this.BloodSpatterOnLatch && latchesOn.IsMagnetic == this.IsMagnetic && !(latchesOn.BehaviorDescription != this.BehaviorDescription) && base.SameAs(Part);
		}

		// Token: 0x0600AB93 RID: 43923 RVA: 0x003ECE87 File Offset: 0x003EB087
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && !this.BehaviorDescription.IsNullOrEmpty()) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AB94 RID: 43924 RVA: 0x003ECEC2 File Offset: 0x003EB0C2
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.BehaviorDescription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB95 RID: 43925 RVA: 0x003ECEE0 File Offset: 0x003EB0E0
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.FirstTurn)
			{
				this.FirstTurn = false;
			}
			else if (this.GetLatchedOnEffectValidated(true, 1, null, null) != null && this.AutoAttackPerTurn)
			{
				Combat.MeleeAttackWithWeapon(this.ParentObject.Equipped, this.LatchedOnto, this.ParentObject, this.ParentObject.Equipped.Body.FindDefaultOrEquippedItem(this.ParentObject), "Autohit", 0, 0, 0, this.MyPowerLoadBonus(int.MinValue, 100, 150), 0, false, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB96 RID: 43926 RVA: 0x003ECF70 File Offset: 0x003EB170
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "BeforeDeathRemoval");
			E.Actor.RegisterPartEvent(this, "BeginMove");
			E.Actor.RegisterPartEvent(this, "BeginTakeAction");
			E.Actor.RegisterPartEvent(this, "EnteredCell");
			E.Actor.RegisterPartEvent(this, "LeftCell");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB97 RID: 43927 RVA: 0x003ECFDC File Offset: 0x003EB1DC
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "BeforeDeathRemoval");
			E.Actor.UnregisterPartEvent(this, "BeginMove");
			E.Actor.UnregisterPartEvent(this, "BeginTakeAction");
			E.Actor.UnregisterPartEvent(this, "EnteredCell");
			E.Actor.UnregisterPartEvent(this, "LeftCell");
			LatchedOnto latchedOnEffectValidated = this.GetLatchedOnEffectValidated(false, 1, E.Actor, null);
			if (latchedOnEffectValidated != null)
			{
				if (this.LatchedOnto.ReceiveObject(this.ParentObject, false, null))
				{
					string text = IComponent<GameObject>.ConsequentialColor(null, E.Actor);
					if (E.Actor.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"Since ",
							this.ParentObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" still latched onto ",
							this.LatchedOnto.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							", releasing ",
							this.ParentObject.them,
							" leaves ",
							this.ParentObject.them,
							" in ",
							this.LatchedOnto.its,
							" possession!"
						}), text, true);
					}
					else if (this.LatchedOnto.IsPlayer())
					{
						if (this.ParentObject.HasProperName)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"Since ",
								this.ParentObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" still latched onto you, ",
								E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" releasing ",
								this.ParentObject.them,
								" leaves ",
								this.ParentObject.them,
								" in your possession!"
							}), text, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"Since ",
								this.ParentObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" still latched onto you, ",
								E.Actor.its,
								" releasing ",
								this.ParentObject.them,
								" leaves ",
								this.ParentObject.them,
								" in your possession!"
							}), null, true);
						}
					}
					else if (this.LatchedOnto.IsVisible() && E.Actor.IsVisible())
					{
						if (this.ParentObject.HasProperName)
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"Since ",
								this.ParentObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" still latched onto ",
								this.LatchedOnto.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", ",
								E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" releasing ",
								this.ParentObject.them,
								" leaves ",
								this.ParentObject.them,
								" in ",
								this.LatchedOnto.poss("possession", true),
								"!"
							}), null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"Since ",
								this.ParentObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" still latched onto ",
								this.LatchedOnto.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", ",
								E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" releasing ",
								this.ParentObject.them,
								" leaves ",
								this.ParentObject.them,
								" in ",
								this.LatchedOnto.poss("possession", true),
								"!"
							}), null, true);
						}
					}
				}
				this.LatchedOnto.RemoveEffect(latchedOnEffectValidated, true);
			}
			this.LatchedOnto = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB98 RID: 43928 RVA: 0x003ED544 File Offset: 0x003EB744
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600AB99 RID: 43929 RVA: 0x003ED547 File Offset: 0x003EB747
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			Registrar.Register("CanMeleeAttack");
			Registrar.Register("StopFighting");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB9A RID: 43930 RVA: 0x003ED580 File Offset: 0x003EB780
		private bool WeaponMatch(Effect FX)
		{
			LatchedOnto latchedOnto = FX as LatchedOnto;
			return latchedOnto != null && latchedOnto.LatchedOnWeapon == this.ParentObject;
		}

		// Token: 0x0600AB9B RID: 43931 RVA: 0x003ED5A8 File Offset: 0x003EB7A8
		private GameObject ValidTargetVia(GameObject who)
		{
			if (this.CanLatchOnto(who))
			{
				return who;
			}
			Body body = who.Body;
			if (body == null)
			{
				return null;
			}
			foreach (BodyPart bodyPart in body.GetParts())
			{
				if (this.CanLatchOnto(bodyPart.Equipped))
				{
					return bodyPart.Equipped;
				}
				if (this.CanLatchOnto(bodyPart.Cybernetics))
				{
					return bodyPart.Cybernetics;
				}
			}
			return null;
		}

		// Token: 0x0600AB9C RID: 43932 RVA: 0x003ED63C File Offset: 0x003EB83C
		private bool CanLatchOnto(GameObject Object)
		{
			return Object != null && (this.SuppressedByPartOnTargetOrEquipment.IsNullOrEmpty() || !Object.HasPart(this.SuppressedByPartOnTargetOrEquipment)) && (this.RequiresPartOnTargetOrEquipment.IsNullOrEmpty() || Object.HasPart(this.RequiresPartOnTargetOrEquipment)) && (!this.IsMagnetic || CanBeMagneticallyManipulatedEvent.Check(Object));
		}

		// Token: 0x0600AB9D RID: 43933 RVA: 0x003ED69B File Offset: 0x003EB89B
		private bool CheckLatchedOnto()
		{
			if (GameObject.Validate(ref this.LatchedOnto) && this.LatchedOnto.IsNowhere())
			{
				this.LatchedOnto = null;
			}
			return this.LatchedOnto != null;
		}

		// Token: 0x0600AB9E RID: 43934 RVA: 0x003ED6C8 File Offset: 0x003EB8C8
		public LatchedOnto GetLatchedOnEffectValidated(out int Distance, bool UseCharge = false, int MaxDistance = 1, GameObject User = null, Cell UseCell = null)
		{
			Distance = 0;
			if (!this.CheckLatchedOnto())
			{
				return null;
			}
			LatchedOnto latchedOnto = this.LatchedOnto.GetEffect(typeof(LatchedOnto), new Predicate<Effect>(this.WeaponMatch)) as LatchedOnto;
			if (latchedOnto != null)
			{
				if (latchedOnto.Duration <= 0)
				{
					latchedOnto = null;
					this.LatchedOnto.CleanEffects();
				}
				else if ((UseCell != null || !this.ParentObject.IsNowhere()) && this.ParentObject.Physics != null && this.LatchedOnto.PhaseMatches(User ?? this.ParentObject.Equipped))
				{
					if (UseCell == null)
					{
						Distance = this.LatchedOnto.DistanceTo(User ?? this.ParentObject.Equipped);
					}
					else
					{
						Distance = this.LatchedOnto.DistanceTo(UseCell);
					}
					if (Distance <= MaxDistance && this.ValidTargetVia(this.LatchedOnto) != null && !base.IsDisabled(UseCharge, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						return latchedOnto;
					}
				}
			}
			if (latchedOnto != null)
			{
				this.LatchedOnto.RemoveEffect(latchedOnto, true);
			}
			this.LatchedOnto = null;
			return null;
		}

		// Token: 0x0600AB9F RID: 43935 RVA: 0x003ED7F8 File Offset: 0x003EB9F8
		public LatchedOnto GetLatchedOnEffectValidated(bool UseCharge = false, int MaxDistance = 1, GameObject User = null, Cell UseCell = null)
		{
			int num;
			return this.GetLatchedOnEffectValidated(out num, UseCharge, MaxDistance, User, UseCell);
		}

		// Token: 0x0600ABA0 RID: 43936 RVA: 0x003ED814 File Offset: 0x003EBA14
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.GetLatchedOnEffectValidated(false, 1, null, null);
			}
			else if (E.ID == "BeginMove")
			{
				LatchedOnto latchedOnEffectValidated = this.GetLatchedOnEffectValidated(false, 1, null, null);
				if (latchedOnEffectValidated != null)
				{
					Cell cell = E.GetParameter("DestinationCell") as Cell;
					if (cell != null && this.LatchedOnto.DistanceTo(cell) == 2)
					{
						GameObject latchedOnWeapon = latchedOnEffectValidated.LatchedOnWeapon;
						GameObject gameObject = ((latchedOnWeapon != null) ? latchedOnWeapon.Equipped : null) ?? this.ParentObject;
						if (!this.LatchedOnto.FireEvent("BeforeGrabbed") || this.LatchedOnto.MakeSave(this.MoveSaveStat, this.MoveSaveTarget + this.MyPowerLoadBonus(-2147483648, 100, 150), gameObject, this.MoveSaveDifficultyStat, "LatchOn Move Drag Restraint", false, false, false, false, this.ParentObject))
						{
							IComponent<GameObject>.XDidY(this.LatchedOnto, "stand", this.LatchedOnto.its + " ground", "!", null, null, null, gameObject, false, false, null, null, false, false, false, false, false, null);
							gameObject.UseEnergy(1000, "Movement Failure", null, null, false);
							return false;
						}
					}
				}
			}
			else if (E.ID == "CanMeleeAttack")
			{
				if (this.GetLatchedOnEffectValidated(false, 1, null, null) != null)
				{
					return false;
				}
			}
			else if (E.ID == "LeftCell")
			{
				Cell cell2 = E.GetParameter("Cell") as Cell;
				if (this.GetLatchedOnEffectValidated(false, 2, null, cell2) != null)
				{
					this.LeftCell = cell2;
				}
				else
				{
					this.LeftCell = null;
				}
			}
			else if (E.ID == "EnteredCell")
			{
				int num;
				if (this.GetLatchedOnEffectValidated(out num, false, 2, null, null) != null)
				{
					if (num == 2 && this.LeftCell != null)
					{
						string directionFromCell = this.LatchedOnto.CurrentCell.GetDirectionFromCell(this.LeftCell, false);
						this.LatchedOnto.Move(directionFromCell, false, false, false, false, true, true, this.ParentObject, null, false, null, null, null, false, false, null, null, 0);
					}
					if (this.AutoAttackOnMove || (this.AutoAttackOnMoveAway && num == 2))
					{
						Combat.MeleeAttackWithWeapon(this.ParentObject.Equipped, this.LatchedOnto, this.ParentObject, this.ParentObject.Equipped.Body.FindDefaultOrEquippedItem(this.ParentObject), "Autohit", 0, 0, 0, this.MyPowerLoadBonus(int.MinValue, 100, 150), 0, false, true);
					}
				}
			}
			else if (E.ID == "WeaponHit")
			{
				if (this.GetLatchedOnEffectValidated(false, 1, null, null) == null)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
					GameObject gameObject2 = this.ValidTargetVia(gameObjectParameter);
					if (gameObject2 != null && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
						if (gameObjectParameter2 != gameObjectParameter && gameObjectParameter.IsCombat && gameObjectParameter2.PhaseMatches(gameObjectParameter) && !gameObjectParameter.MakeSave(this.InitialSaveStat, this.InitialSaveTarget + this.MyPowerLoadBonus(-2147483648, 100, 150), this.ParentObject.Equipped ?? this.ParentObject, this.InitialSaveDifficultyStat, "LatchOn Initial Grab Restraint", false, false, false, false, this.ParentObject))
						{
							LatchedOnto latchedOnto = new LatchedOnto(this.ParentObject, this.BreakSaveTarget + this.MyPowerLoadBonus(int.MinValue, 100, 150), this.BreakSaveStat, this.BreakSaveDifficultyStat, Stat.Roll(this.Duration, null));
							if (gameObjectParameter.FireEvent("BeforeGrabbed") && gameObjectParameter.ApplyEffect(latchedOnto, null))
							{
								string text = IComponent<GameObject>.ConsequentialColor(null, gameObjectParameter);
								if (gameObjectParameter.IsPlayer())
								{
									if (this.ParentObject.HasProperName)
									{
										IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
										{
											text,
											this.ParentObject.The,
											this.ParentObject.ShortDisplayName,
											text,
											this.ParentObject.GetVerb("latch", true, false, false, true, false),
											" onto ",
											(gameObject2 == gameObjectParameter) ? "you" : (gameObject2.HasProperName ? (gameObject2.ShortDisplayName + text) : ("your " + gameObject2.ShortDisplayName + text)),
											"!"
										}), null, true);
									}
									else
									{
										IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
										{
											text,
											Grammar.MakePossessive(gameObjectParameter2.The + gameObjectParameter2.ShortDisplayName),
											" ",
											this.ParentObject.ShortDisplayName,
											text,
											this.ParentObject.GetVerb("latch", true, false, false, true, false),
											" onto ",
											(gameObject2 == gameObjectParameter) ? "you" : (gameObject2.HasProperName ? (gameObject2.ShortDisplayName + text) : ("your " + gameObject2.ShortDisplayName + text)),
											"!"
										}), null, true);
									}
								}
								else if (gameObjectParameter.IsVisible())
								{
									if (gameObjectParameter2.IsPlayer())
									{
										if (this.ParentObject.HasProperName)
										{
											IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
											{
												text,
												this.ParentObject.The,
												this.ParentObject.ShortDisplayName,
												text,
												this.ParentObject.GetVerb("latch", true, false, false, true, false),
												" onto ",
												(gameObject2 == gameObjectParameter) ? (gameObjectParameter.the + gameObjectParameter.ShortDisplayName) : (gameObject2.HasProperName ? gameObject2.ShortDisplayName : (Grammar.MakePossessive(gameObjectParameter.the + gameObjectParameter.ShortDisplayName) + text + " " + gameObject2.ShortDisplayName)),
												text,
												"!"
											}), null, true);
										}
										else
										{
											IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
											{
												text,
												"Your ",
												this.ParentObject.ShortDisplayName,
												text,
												this.ParentObject.GetVerb("latch", true, false, false, true, false),
												" onto ",
												(gameObject2 == gameObjectParameter) ? (gameObjectParameter.the + gameObjectParameter.ShortDisplayName) : (gameObject2.HasProperName ? gameObject2.ShortDisplayName : (Grammar.MakePossessive(gameObjectParameter.the + gameObjectParameter.ShortDisplayName) + text + " " + gameObject2.ShortDisplayName)),
												text,
												"!"
											}), null, true);
										}
									}
									else if (this.ParentObject.HasProperName)
									{
										IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
										{
											text,
											this.ParentObject.The,
											this.ParentObject.ShortDisplayName,
											text,
											this.ParentObject.GetVerb("latch", true, false, false, true, false),
											" onto ",
											(gameObject2 == gameObjectParameter) ? (gameObjectParameter.the + gameObjectParameter.ShortDisplayName) : (gameObject2.HasProperName ? gameObject2.ShortDisplayName : (Grammar.MakePossessive(gameObjectParameter.the + gameObjectParameter.ShortDisplayName) + text + " " + gameObject2.ShortDisplayName)),
											text,
											"!"
										}), null, true);
									}
									else
									{
										IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
										{
											text,
											Grammar.MakePossessive(gameObjectParameter2.The + gameObjectParameter2.ShortDisplayName),
											" ",
											this.ParentObject.ShortDisplayName,
											text,
											this.ParentObject.GetVerb("latch", true, false, false, true, false),
											" onto ",
											(gameObject2 == gameObjectParameter) ? (gameObjectParameter.the + gameObjectParameter.ShortDisplayName) : (gameObject2.HasProperName ? gameObject2.ShortDisplayName : (Grammar.MakePossessive(gameObjectParameter.the + gameObjectParameter.ShortDisplayName) + text + " " + gameObject2.ShortDisplayName)),
											text,
											"!"
										}), null, true);
									}
								}
								this.LatchedOnto = gameObjectParameter;
								this.FirstTurn = true;
								if (this.BloodSpatterOnLatch)
								{
									this.LatchedOnto.Bloodsplatter();
								}
							}
						}
					}
				}
			}
			else if (E.ID == "StopFighting")
			{
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Target");
				if (gameObjectParameter3 == null || gameObjectParameter3 == this.LatchedOnto)
				{
					LatchedOnto latchedOnEffectValidated2 = this.GetLatchedOnEffectValidated(false, 1, null, null);
					if (latchedOnEffectValidated2 != null)
					{
						latchedOnEffectValidated2.Expired();
						this.LatchedOnto.RemoveEffect(latchedOnEffectValidated2, true);
						this.LatchedOnto = null;
					}
				}
			}
			else if (E.ID == "BeforeDeathRemoval")
			{
				LatchedOnto latchedOnEffectValidated3 = this.GetLatchedOnEffectValidated(false, 1, null, null);
				if (latchedOnEffectValidated3 != null)
				{
					this.LatchedOnto.RemoveEffect(latchedOnEffectValidated3, true);
					this.LatchedOnto = null;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046CF RID: 18127
		public GameObject LatchedOnto;

		// Token: 0x040046D0 RID: 18128
		public Cell LeftCell;

		// Token: 0x040046D1 RID: 18129
		public int InitialSaveTarget = 5;

		// Token: 0x040046D2 RID: 18130
		public string InitialSaveStat = "Agility";

		// Token: 0x040046D3 RID: 18131
		public string InitialSaveDifficultyStat = "Agility";

		// Token: 0x040046D4 RID: 18132
		public int MoveSaveTarget = 15;

		// Token: 0x040046D5 RID: 18133
		public string MoveSaveStat = "Strength";

		// Token: 0x040046D6 RID: 18134
		public string MoveSaveDifficultyStat = "Strength";

		// Token: 0x040046D7 RID: 18135
		public int BreakSaveTarget = 15;

		// Token: 0x040046D8 RID: 18136
		public string BreakSaveStat = "Strength";

		// Token: 0x040046D9 RID: 18137
		public string BreakSaveDifficultyStat = "Strength";

		// Token: 0x040046DA RID: 18138
		public string Duration = "3d4";

		// Token: 0x040046DB RID: 18139
		public string RequiresPartOnTargetOrEquipment;

		// Token: 0x040046DC RID: 18140
		public string SuppressedByPartOnTargetOrEquipment;

		// Token: 0x040046DD RID: 18141
		public bool AutoAttackOnMoveAway;

		// Token: 0x040046DE RID: 18142
		public bool AutoAttackOnMove;

		// Token: 0x040046DF RID: 18143
		public bool AutoAttackPerTurn = true;

		// Token: 0x040046E0 RID: 18144
		public bool BloodSpatterOnLatch = true;

		// Token: 0x040046E1 RID: 18145
		public bool IsMagnetic;

		// Token: 0x040046E2 RID: 18146
		public string BehaviorDescription;

		// Token: 0x040046E3 RID: 18147
		public bool FirstTurn;
	}
}
