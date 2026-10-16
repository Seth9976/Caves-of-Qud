using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	/// <summary>
	///   FlamingHands powers the "Flaming Ray" mutation.  You can now choose a variant from hands, feet or face.
	/// </summary>
	// Token: 0x02001435 RID: 5173
	[Serializable]
	public class FlamingRay : BaseDefaultEquipmentMutation
	{
		/// <summary>Create or retrive the already created Projectile game object.</summary>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600C9E8 RID: 51688 RVA: 0x00497BB0 File Offset: 0x00495DB0
		private static GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref FlamingRay._Projectile))
				{
					FlamingRay._Projectile = GameObject.CreateUnmodified(FlamingRay.PROJECTILE_BLUEPRINT);
				}
				return FlamingRay._Projectile;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600C9E9 RID: 51689 RVA: 0x00497BD2 File Offset: 0x00495DD2
		public override bool UseVariantName
		{
			get
			{
				return false;
			}
		}

		/// <summary>We are request to be re-mutated automatically when our body is rebuilt. Thanks slog.</summary>
		// Token: 0x0600C9EA RID: 51690 RVA: 0x00497BD5 File Offset: 0x00495DD5
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C9EB RID: 51691 RVA: 0x00497BD8 File Offset: 0x00495DD8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C9EC RID: 51692 RVA: 0x00497BF8 File Offset: 0x00495DF8
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("EmitText", this.GetDescription(), false, 0);
			stats.Set("Range", FlamingRay.RANGE, false, 0);
			stats.Set("Damage", this.ComputeDamage(), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 10, 0);
		}

		// Token: 0x0600C9ED RID: 51693 RVA: 0x00497C68 File Offset: 0x00495E68
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.CheckObjectProperlyEquipped() && E.Distance <= FlamingRay.RANGE && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add(FlamingRay.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9EE RID: 51694 RVA: 0x00497CD8 File Offset: 0x00495ED8
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == FlamingRay.COMMAND_NAME)
			{
				if (base.OnWorldMap)
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				if (!this.CheckObjectProperlyEquipped())
				{
					BodyPart firstBodyPart = this.ParentObject.GetFirstBodyPart(this.BodyPartType);
					if (firstBodyPart != null)
					{
						return this.ParentObject.Fail(string.Concat(new string[]
						{
							"Your ",
							firstBodyPart.GetOrdinalName(),
							" ",
							firstBodyPart.Plural ? "are" : "is",
							" too damaged to do that!"
						}));
					}
					return this.ParentObject.Fail("Your " + this.BodyPartType + " is too damaged to do that!");
				}
				else if (!FlamingRay.Cast(this, "5-6"))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9EF RID: 51695 RVA: 0x00497DB9 File Offset: 0x00495FB9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C9F0 RID: 51696 RVA: 0x00497DBC File Offset: 0x00495FBC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			base.Register(Object, Registrar);
		}

		/// <summary>Show selected variant in character creation.</summary>
		// Token: 0x0600C9F1 RID: 51697 RVA: 0x00497DD1 File Offset: 0x00495FD1
		public override string GetCreateCharacterDisplayName()
		{
			if (this.BodyPartType != null)
			{
				return base.GetDisplayName(true) + " (" + this.BodyPartType + ")";
			}
			return base.GetDisplayName(true);
		}

		// Token: 0x0600C9F2 RID: 51698 RVA: 0x00497E00 File Offset: 0x00496000
		public override string GetDescription()
		{
			BodyPart registeredSlot = base.GetRegisteredSlot(this.BodyPartType, true);
			if (registeredSlot != null)
			{
				return "You emit a ray of flame from your " + registeredSlot.GetOrdinalName() + ".";
			}
			return "You emit a ray of flame.";
		}

		// Token: 0x0600C9F3 RID: 51699 RVA: 0x00497E3C File Offset: 0x0049603C
		public override string GetLevelText(int level)
		{
			return "Emits a " + FlamingRay.RANGE.ToString() + "-square ray of flame in the direction of your choice.\n" + "Damage: {{rules|" + this.ComputeDamage(level) + "}}\n" + "Cooldown: 10 rounds\n" + "Melee attacks heat opponents by {{rules|" + this.GetHeatOnHitAmount(level) + "}} degrees";
		}

		// Token: 0x0600C9F4 RID: 51700 RVA: 0x00497E9C File Offset: 0x0049609C
		public string GetHeatOnHitAmount(int level)
		{
			return (level * 2).ToString() + "d8";
		}

		// Token: 0x0600C9F5 RID: 51701 RVA: 0x00497EC0 File Offset: 0x004960C0
		public string ComputeDamage(int level)
		{
			string text = level.ToString() + "d4";
			if (this.ParentObject != null)
			{
				Body body = this.ParentObject.Body;
				int num = ((body != null) ? body.GetPartCount(this.BodyPartType) : 0);
				if (num > 0)
				{
					text += num.Signed(false);
				}
			}
			else
			{
				text += "+1";
			}
			return text;
		}

		// Token: 0x0600C9F6 RID: 51702 RVA: 0x00497F26 File Offset: 0x00496126
		public string ComputeDamage()
		{
			return this.ComputeDamage(base.Level);
		}

		// Token: 0x0600C9F7 RID: 51703 RVA: 0x00497F34 File Offset: 0x00496134
		public void Flame(Cell C, ScreenBuffer Buffer = null, bool DoEffect = true, bool UsePopups = false)
		{
			if (C != null)
			{
				List<GameObject> objectsInCell = C.GetObjectsInCell();
				bool flag = false;
				foreach (GameObject gameObject in objectsInCell)
				{
					if (gameObject.PhaseMatches(this.ParentObject))
					{
						gameObject.TemperatureChange(310 + 25 * base.Level, this.ParentObject, false, false, false, false, 0, null, null);
						if (DoEffect && !flag)
						{
							for (int i = 0; i < 5; i++)
							{
								gameObject.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
							for (int j = 0; j < 5; j++)
							{
								gameObject.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
							for (int k = 0; k < 5; k++)
							{
								gameObject.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
							flag = true;
						}
					}
				}
				int phase = this.ParentObject.GetPhase();
				DieRoll cachedDieRoll = this.ComputeDamage().GetCachedDieRoll();
				foreach (GameObject gameObject2 in C.GetObjectsWithPartReadonly("Combat"))
				{
					int num = cachedDieRoll.Resolve();
					string text = "from %t flames!";
					string text2 = "Fire";
					string text3 = null;
					string text4 = null;
					GameObject parentObject = this.ParentObject;
					GameObject gameObject3 = null;
					GameObject gameObject4 = null;
					GameObject gameObject5 = null;
					GameObject gameObject6 = null;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					int num2 = phase;
					gameObject2.TakeDamage(num, text, text2, text3, text4, parentObject, gameObject3, gameObject4, gameObject5, gameObject6, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, UsePopups, num2, null);
				}
			}
			if (DoEffect)
			{
				C.Flameburst(Buffer);
			}
		}

		// Token: 0x0600C9F8 RID: 51704 RVA: 0x0049814C File Offset: 0x0049634C
		public static bool Cast(FlamingRay mutation = null, string level = "5-6")
		{
			if (mutation == null)
			{
				mutation = new FlamingRay();
				mutation.Level = level.RollCached();
				mutation.ParentObject = The.Player;
			}
			List<Cell> list = mutation.PickLine(FlamingRay.RANGE, AllowVis.Any, null, false, true, true, false, null, null, "Flaming Ray", true, true);
			if (list == null || list.Count <= 0)
			{
				return false;
			}
			if (list.Count == 1 && mutation.ParentObject.IsPlayer() && Popup.ShowYesNoCancel("Are you sure you want to target " + mutation.ParentObject.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
			{
				return false;
			}
			bool? flag;
			if (mutation == null)
			{
				flag = null;
			}
			else
			{
				GameObject parentObject = mutation.ParentObject;
				if (parentObject == null)
				{
					flag = null;
				}
				else
				{
					Cell currentCell = parentObject.CurrentCell;
					if (currentCell == null)
					{
						flag = null;
					}
					else
					{
						Zone parentZone = currentCell.ParentZone;
						flag = ((parentZone != null) ? new bool?(parentZone.IsActive()) : null);
					}
				}
			}
			bool? flag2 = flag;
			bool valueOrDefault = flag2.GetValueOrDefault();
			ScreenBuffer screenBuffer = null;
			if (valueOrDefault)
			{
				screenBuffer = ScreenBuffer.GetScrapBuffer1(true);
				XRLCore.Core.RenderMapToBuffer(screenBuffer);
			}
			mutation.UseEnergy(1000, "Physical Mutation Flaming Hands");
			mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, 10, null, null, false);
			mutation.PlayWorldSound(mutation.Sound, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			int i = 0;
			int num = Math.Min(list.Count, 10);
			while (i < num)
			{
				if (list.Count == 1 || list[i] != mutation.ParentObject.CurrentCell)
				{
					mutation.Flame(list[i], screenBuffer, valueOrDefault, false);
				}
				if (i < num - 1 && list[i].IsSolidForProjectile(FlamingRay.Projectile, mutation.ParentObject, null, mutation.ParentObject.Target, null, null, false))
				{
					break;
				}
				i++;
			}
			BodyPart registeredSlot = mutation.GetRegisteredSlot(mutation.BodyPartType, false);
			IComponent<GameObject>.XDidY(mutation.ParentObject, "emit", "a flaming ray" + ((registeredSlot != null) ? (" from " + mutation.ParentObject.its + " " + registeredSlot.GetOrdinalName()) : ""), "!", null, null, mutation.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x0600C9F9 RID: 51705 RVA: 0x004983A8 File Offset: 0x004965A8
		public bool CheckObjectProperlyEquipped()
		{
			return !this.CreateObject || (base.HasRegisteredSlot(this.BodyPartType) && base.GetRegisteredSlot(this.BodyPartType, false) != null);
		}

		// Token: 0x0600C9FA RID: 51706 RVA: 0x004983D4 File Offset: 0x004965D4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit")
			{
				if (!this.CheckObjectProperlyEquipped())
				{
					return true;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null)
				{
					string heatOnHitAmount = this.GetHeatOnHitAmount(base.Level);
					int num = 400;
					int num2 = heatOnHitAmount.RollMaxCached();
					if ((num2 > 0 && gameObjectParameter.Physics.Temperature < num) || (num2 < 0 && gameObjectParameter.Physics.Temperature > num))
					{
						gameObjectParameter.TemperatureChange(heatOnHitAmount.RollCached(), E.GetGameObjectParameter("Attacker"), false, false, false, false, this.ParentObject.GetPhase(), null, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C9FB RID: 51707 RVA: 0x00498490 File Offset: 0x00496690
		private void AddAbility()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Flaming Ray", FlamingRay.COMMAND_NAME, "Physical Mutations", this.GetLevelText(base.Level), "\u00a8", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x0600C9FC RID: 51708 RVA: 0x004984DC File Offset: 0x004966DC
		public override bool ChangeLevel(int NewLevel)
		{
			bool flag = base.ChangeLevel(NewLevel);
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			if (activatedAbilityEntry != null)
			{
				activatedAbilityEntry.Description = this.GetLevelText(base.Level);
			}
			return flag;
		}

		// Token: 0x0600C9FD RID: 51709 RVA: 0x00498514 File Offset: 0x00496714
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			GameObjectBlueprint value = GameObjectFactory.Factory.Blueprints.GetValue(Variant, null);
			this.BodyPartType = ((value != null) ? value.GetPartParameter<string>("Armor", "WornOn", "Hands") : null);
		}

		// Token: 0x0600C9FE RID: 51710 RVA: 0x0049855B File Offset: 0x0049675B
		public override string GetVariantName(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return null;
			}
			return Blueprint.GetPartParameter<string>("Armor", "WornOn", "Hands");
		}

		// Token: 0x0600C9FF RID: 51711 RVA: 0x00498578 File Offset: 0x00496778
		public override IRenderable GetIcon()
		{
			MutationEntry mutationEntry;
			if (!MutationFactory.TryGetMutationEntry(this, out mutationEntry))
			{
				return null;
			}
			return mutationEntry.GetRenderable();
		}

		// Token: 0x0600CA00 RID: 51712 RVA: 0x00498598 File Offset: 0x00496798
		public override bool TryGetVariantValidity(GameObject Object, string Variant, out string Message)
		{
			GameObjectBlueprint value = GameObjectFactory.Factory.Blueprints.GetValue(Variant, null);
			string text = ((value != null) ? value.GetPartParameter<string>("Armor", "WornOn", "Hands") : null);
			if (!Object.Body.HasPart(text, true))
			{
				BodyPartType bodyPartTypeOrFail = Anatomies.GetBodyPartTypeOrFail(text);
				Message = ("Missing " + bodyPartTypeOrFail.Name).WithColor("R");
				return false;
			}
			return base.TryGetVariantValidity(Object, Variant, out Message);
		}

		// Token: 0x0600CA01 RID: 51713 RVA: 0x00498610 File Offset: 0x00496810
		public void MakeFlaming(BodyPart part)
		{
			if (part == null)
			{
				return;
			}
			if (part.DefaultBehavior != null && part.DefaultBehavior.Blueprint != this.Variant)
			{
				part.DefaultBehavior.RequirePart<Flaming>(false);
			}
			if (part.Parts != null)
			{
				for (int i = 0; i < part.Parts.Count; i++)
				{
					this.MakeFlaming(part.Parts[i]);
				}
			}
		}

		// Token: 0x0600CA02 RID: 51714 RVA: 0x00498680 File Offset: 0x00496880
		public override void OnDecorateDefaultEquipment(Body Body)
		{
			if (this.CreateObject)
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(this.Variant);
				this.BodyPartType = blueprint.GetPartParameter<string>("Armor", "WornOn", "Hands");
				BodyPart firstPart;
				if (!base.TryGetRegisteredSlot(Body, this.BodyPartType, out firstPart, true))
				{
					firstPart = Body.GetFirstPart(this.BodyPartType);
					if (firstPart != null)
					{
						base.RegisterSlot(this.BodyPartType, firstPart);
					}
				}
				if (firstPart != null && firstPart.DefaultBehavior == null)
				{
					GameObject gameObject = GameObject.Create(blueprint, 0, 0, null, null, null, null, null);
					gameObject.GetPart<Armor>().WornOn = this.BodyPartType;
					firstPart.DefaultBehavior = gameObject;
					firstPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "FlamingRay", false);
				}
				this.MakeFlaming(firstPart);
				if (this.BodyPartType == "Hands")
				{
					foreach (BodyPart bodyPart in Body.GetPart("Hand"))
					{
						this.MakeFlaming(bodyPart);
					}
				}
			}
			base.OnDecorateDefaultEquipment(Body);
		}

		// Token: 0x0600CA03 RID: 51715 RVA: 0x004987A8 File Offset: 0x004969A8
		public override bool Mutate(GameObject GO, int Level)
		{
			if (this.Variant.IsNullOrEmpty())
			{
				List<string> variants = this.GetVariants();
				this.Variant = variants.GetRandomElement<string>(null);
				if (!this.BodyPartType.IsNullOrEmpty())
				{
					foreach (string text in variants)
					{
						if (GameObjectFactory.Factory.GetBlueprint(text).GetPartParameter<string>("Armor", "WornOn", "") == this.BodyPartType)
						{
							this.Variant = text;
							break;
						}
					}
				}
			}
			this.AddAbility();
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA04 RID: 51716 RVA: 0x00498864 File Offset: 0x00496A64
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004ED3 RID: 20179
		public static readonly string COMMAND_NAME = "CommandFlamingRay";

		// Token: 0x04004ED4 RID: 20180
		public static readonly string PROJECTILE_BLUEPRINT = "ProjectileFlamingRay";

		// Token: 0x04004ED5 RID: 20181
		public static readonly int RANGE = 9;

		/// <summary>The <see cref="F:XRL.World.Anatomy.BodyPart.Type" /> we replace (chosen by variant selection.)</summary>
		// Token: 0x04004ED6 RID: 20182
		public string BodyPartType;

		/// <summary>Do we still need to create the object? Setup as a public for serialization purposes.</summary>
		// Token: 0x04004ED7 RID: 20183
		public bool CreateObject = true;

		/// <summary>Sound file to play when attacking.</summary>
		// Token: 0x04004ED8 RID: 20184
		public string Sound = "Sounds/Abilities/sfx_ability_mutation_flamingRay_attack";

		// Token: 0x04004ED9 RID: 20185
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
