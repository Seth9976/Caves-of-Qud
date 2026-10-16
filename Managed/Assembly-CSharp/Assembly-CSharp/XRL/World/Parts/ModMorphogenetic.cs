using System;
using System.Collections.Generic;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, save targets to resist the effect are
	/// increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400.
	/// </remarks>
	// Token: 0x02001152 RID: 4434
	[Serializable]
	public class ModMorphogenetic : IModification
	{
		// Token: 0x0600B017 RID: 45079 RVA: 0x0040D9F4 File Offset: 0x0040BBF4
		public ModMorphogenetic()
		{
		}

		// Token: 0x0600B018 RID: 45080 RVA: 0x0040DA5C File Offset: 0x0040BC5C
		public ModMorphogenetic(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B019 RID: 45081 RVA: 0x0040DAC4 File Offset: 0x0040BCC4
		public override void Configure()
		{
			this.ChargeUse = 200;
			this.WorksOnSelf = true;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "MorphogeneticResonator";
		}

		// Token: 0x0600B01A RID: 45082 RVA: 0x0040DAFF File Offset: 0x0040BCFF
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>() || Object.HasPart<MissileWeapon>() || Object.HasPart<ThrownWeapon>();
		}

		// Token: 0x0600B01B RID: 45083 RVA: 0x0040DB1C File Offset: 0x0040BD1C
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(2, 2, null);
		}

		// Token: 0x0600B01C RID: 45084 RVA: 0x0040DB2F File Offset: 0x0040BD2F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B01D RID: 45085 RVA: 0x0040DB55 File Offset: 0x0040BD55
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{m|morphogenetic}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B01E RID: 45086 RVA: 0x0040DB7F File Offset: 0x0040BD7F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B01F RID: 45087 RVA: 0x0040DBA1 File Offset: 0x0040BDA1
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 2);
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B020 RID: 45088 RVA: 0x0040DBD0 File Offset: 0x0040BDD0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B021 RID: 45089 RVA: 0x0040DBD3 File Offset: 0x0040BDD3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("WeaponPseudoThrowHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B022 RID: 45090 RVA: 0x0040DC0C File Offset: 0x0040BE0C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" || E.ID == "LauncherProjectileHit" || E.ID == "WeaponThrowHit" || E.ID == "WeaponPseudoThrowHit")
			{
				Damage parameter = E.GetParameter<Damage>();
				int num = ((parameter != null) ? parameter.Amount : E.GetIntParameter("Damage", 0));
				if (num > 0)
				{
					GameObject defender = E.GetGameObjectParameter("Defender");
					if (defender != null && defender.Brain != null)
					{
						string species = defender.GetSpecies();
						Cell currentCell = defender.CurrentCell;
						if (!species.IsNullOrEmpty() && ((currentCell != null) ? currentCell.ParentZone : null) != null)
						{
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
							int? num3 = null;
							bool flag11 = false;
							long num4 = 0L;
							int? num5 = null;
							if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, num3, flag11, num4, num5))
							{
								GameObject attacker = E.GetGameObjectParameter("Attacker");
								Event @event = Event.New("InitiateRealityDistortionLocal", 0, 0, 0);
								@event.SetParameter("Object", attacker);
								@event.SetParameter("Device", this.ParentObject);
								@event.SetParameter("Operator", attacker);
								@event.SetParameter("Cell", currentCell);
								if (defender.FireEvent(@event, E))
								{
									List<GameObject> list = Event.NewGameObjectList();
									currentCell.ParentZone.FindObjects(list, (GameObject obj) => obj != defender && ModMorphogenetic.MorphicShockMatch(obj, species));
									if (list.Count > 0)
									{
										int num6 = this.MyPowerLoadLevel();
										bool flag12 = true;
										bool flag13 = false;
										bool flag14 = false;
										bool flag15 = false;
										bool flag16 = false;
										bool flag17 = false;
										bool flag18 = false;
										bool flag19 = false;
										bool flag20 = false;
										bool flag21 = false;
										int num7 = 1;
										num5 = new int?(num6);
										if (base.IsReady(flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, flag21, num7, null, false, 0L, num5))
										{
											if (list.Count > 1)
											{
												list.Sort((GameObject a, GameObject b) => a.DistanceTo(attacker).CompareTo(b.DistanceTo(attacker)));
											}
											int i = 0;
											int count = list.Count;
											while (i < count)
											{
												GameObject gameObject = list[i];
												if (GameObject.Validate(ref gameObject))
												{
													this.ApplyMorphicShock(gameObject, num, attacker, num6);
												}
												i++;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B023 RID: 45091 RVA: 0x0040DE6D File Offset: 0x0040C06D
		public static string GetDescription(int Tier)
		{
			return "Morphogenetic: When powered and used to perform a successful, damaging hit, this weapon attempts to daze all other creatures of the same species as your target on the local map. Compute power on the local lattice increases the strength of this effect.";
		}

		// Token: 0x0600B024 RID: 45092 RVA: 0x0040DE74 File Offset: 0x0040C074
		public string GetInstanceDescription()
		{
			return ModMorphogenetic.GetDescription(this.Tier);
		}

		// Token: 0x0600B025 RID: 45093 RVA: 0x0040DE84 File Offset: 0x0040C084
		public bool ApplyMorphicShock(GameObject Subject, int Damage, GameObject Owner, int PowerLoad = 100)
		{
			if (Subject.Brain == null)
			{
				return false;
			}
			if (!IComponent<GameObject>.CheckRealityDistortionAccessibility(Subject, null, Owner, this.ParentObject, null, 0))
			{
				return false;
			}
			int num = this.SaveBaseDifficulty + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150) + (int)((float)Damage * this.SaveDamageDifficultyFactor) + GetAvailableComputePowerEvent.GetFor(Owner) / 3;
			if (Subject.HasPart<Analgesia>())
			{
				num -= 5;
			}
			if (num <= 0)
			{
				return false;
			}
			this.eCanApplyMorphicShock.SetParameter("Attacker", Owner);
			this.eCanApplyMorphicShock.SetParameter("Defender", Subject);
			this.eCanApplyMorphicShock.SetParameter("Difficulty", num);
			if (!Subject.FireEvent(this.eCanApplyMorphicShock))
			{
				return false;
			}
			if (Subject.MakeSave(this.SaveAttribute, num, null, null, this.SaveVs, false, false, false, false, this.ParentObject))
			{
				return false;
			}
			this.eApplyMorphicShock.SetParameter("Attacker", Owner);
			this.eApplyMorphicShock.SetParameter("Defender", Subject);
			this.eApplyMorphicShock.SetParameter("Difficulty", num);
			if (!Subject.FireEvent(this.eApplyMorphicShock))
			{
				return false;
			}
			if (Subject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("A weird" + (Subject.HasPart<Analgesia>() ? "" : ", painful") + " shock reverberates through you.", null, true);
			}
			if (!Subject.ApplyEffect(new Dazed(this.DazeDuration.RollCached()), null))
			{
				return false;
			}
			Subject.TelekinesisBlip();
			return true;
		}

		// Token: 0x0600B026 RID: 45094 RVA: 0x0040DFF0 File Offset: 0x0040C1F0
		public static bool MorphicShockMatch(GameObject Subject, string Species)
		{
			if (Subject.Brain == null)
			{
				return false;
			}
			if (Species == "*")
			{
				return true;
			}
			string species = Subject.GetSpecies();
			return species == "*" || species == Species;
		}

		// Token: 0x0400480B RID: 18443
		public const int DEFAULT_SAVE_BASE_DIFFICULTY = 10;

		// Token: 0x0400480C RID: 18444
		public const float DEFAULT_SAVE_DAMAGE_DIFFICULTY_FACTOR = 0.2f;

		// Token: 0x0400480D RID: 18445
		public const string DEFAULT_SAVE_ATTRIBUTE = "Willpower";

		// Token: 0x0400480E RID: 18446
		public const string DEFAULT_SAVE_VS = "MorphicShock Daze";

		// Token: 0x0400480F RID: 18447
		public const string DEFAULT_DAZE_DURATION = "2-3";

		// Token: 0x04004810 RID: 18448
		public int SaveBaseDifficulty = 10;

		// Token: 0x04004811 RID: 18449
		public float SaveDamageDifficultyFactor = 0.2f;

		// Token: 0x04004812 RID: 18450
		public string SaveAttribute = "Willpower";

		// Token: 0x04004813 RID: 18451
		public string SaveVs = "MorphicShock Daze";

		// Token: 0x04004814 RID: 18452
		public string DazeDuration = "2-3";

		// Token: 0x04004815 RID: 18453
		[NonSerialized]
		private Event eCanApplyMorphicShock = new Event("CanApplyMorphicShock");

		// Token: 0x04004816 RID: 18454
		[NonSerialized]
		private Event eApplyMorphicShock = new Event("ApplyMorphicShock");
	}
}
