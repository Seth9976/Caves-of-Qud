using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001437 RID: 5175
	[Serializable]
	public class FreezingRay : BaseDefaultEquipmentMutation
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x0600CA19 RID: 51737 RVA: 0x004992FA File Offset: 0x004974FA
		private static GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref FreezingRay._Projectile))
				{
					FreezingRay._Projectile = GameObject.CreateUnmodified(FreezingRay.PROJECTILE_BLUEPRINT);
				}
				return FreezingRay._Projectile;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600CA1A RID: 51738 RVA: 0x0049931C File Offset: 0x0049751C
		public override bool UseVariantName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600CA1B RID: 51739 RVA: 0x0049931F File Offset: 0x0049751F
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CA1C RID: 51740 RVA: 0x00499322 File Offset: 0x00497522
		public override string GetCreateCharacterDisplayName()
		{
			if (this.BodyPartType != null)
			{
				return base.GetDisplayName(true) + " (" + this.BodyPartType + ")";
			}
			return base.GetDisplayName(true);
		}

		// Token: 0x0600CA1E RID: 51742 RVA: 0x00499378 File Offset: 0x00497578
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CA1F RID: 51743 RVA: 0x004993A0 File Offset: 0x004975A0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.CheckObjectProperlyEquipped() && E.Distance <= FreezingRay.RANGE && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add(FreezingRay.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA20 RID: 51744 RVA: 0x00499410 File Offset: 0x00497610
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == FreezingRay.COMMAND_NAME)
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
				else if (!FreezingRay.Cast(this, "5-6"))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA21 RID: 51745 RVA: 0x004994F4 File Offset: 0x004976F4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("EmitText", this.GetDescription(), false, 0);
			stats.Set("Range", FreezingRay.RANGE, false, 0);
			stats.Set("Damage", this.ComputeDamage(), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 20, 0);
		}

		// Token: 0x0600CA22 RID: 51746 RVA: 0x00499561 File Offset: 0x00497761
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA23 RID: 51747 RVA: 0x00499589 File Offset: 0x00497789
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA24 RID: 51748 RVA: 0x004995A0 File Offset: 0x004977A0
		public override string GetDescription()
		{
			BodyPart registeredSlot = base.GetRegisteredSlot(this.BodyPartType, true);
			if (registeredSlot != null)
			{
				return "You emit a ray of frost from your " + registeredSlot.GetOrdinalName() + ".";
			}
			return "You emit a ray of frost.";
		}

		// Token: 0x0600CA25 RID: 51749 RVA: 0x004995DC File Offset: 0x004977DC
		public override string GetLevelText(int Level)
		{
			return "Emits a " + FreezingRay.RANGE.ToString() + "-square ray of frost in the direction of your choice.\n" + "Damage: {{rules|" + this.ComputeDamage(Level) + "}}\n" + "Cooldown: 20 rounds\n" + "Melee attacks cool opponents by {{rules|" + this.GetCoolOnHitAmount(Level) + "}} degrees";
		}

		// Token: 0x0600CA26 RID: 51750 RVA: 0x0049963B File Offset: 0x0049783B
		public string GetCoolOnHitAmount(int Level)
		{
			return "-" + Level.ToString() + "d4";
		}

		// Token: 0x0600CA27 RID: 51751 RVA: 0x00499654 File Offset: 0x00497854
		public string ComputeDamage(int UseLevel)
		{
			string text = UseLevel.ToString() + "d3";
			if (this.ParentObject != null)
			{
				int partCount = this.ParentObject.Body.GetPartCount(this.BodyPartType);
				if (partCount > 0)
				{
					text += partCount.Signed(false);
				}
			}
			else
			{
				text += "+1";
			}
			return text;
		}

		// Token: 0x0600CA28 RID: 51752 RVA: 0x004996B3 File Offset: 0x004978B3
		public string ComputeDamage()
		{
			return this.ComputeDamage(base.Level);
		}

		// Token: 0x0600CA29 RID: 51753 RVA: 0x004996C4 File Offset: 0x004978C4
		public void Freeze(Cell C, ref ScreenBuffer Buffer)
		{
			string text = this.ComputeDamage();
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjectsInCell())
				{
					if (gameObject.PhaseMatches(this.ParentObject) && gameObject.TemperatureChange(-120 - 7 * base.Level, this.ParentObject, false, false, false, false, 0, null, null))
					{
						for (int i = 0; i < 5; i++)
						{
							gameObject.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int j = 0; j < 5; j++)
						{
							gameObject.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int k = 0; k < 5; k++)
						{
							gameObject.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
					}
				}
				foreach (GameObject gameObject2 in C.GetObjectsWithPart("Combat"))
				{
					if (gameObject2.PhaseMatches(this.ParentObject))
					{
						gameObject2.TakeDamage(text.RollCached(), "from %t freezing effect!", "Cold", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					}
				}
			}
			if (C.IsVisible())
			{
				if (Buffer == null)
				{
					Buffer = ScreenBuffer.GetScrapBuffer1(false);
					XRLCore.Core.RenderMapToBuffer(Buffer);
				}
				Buffer.Goto(C.X, C.Y);
				string text2 = "&C";
				int num = Stat.Random(1, 3);
				if (num == 1)
				{
					text2 = "&C";
				}
				if (num == 2)
				{
					text2 = "&B";
				}
				if (num == 3)
				{
					text2 = "&Y";
				}
				int num2 = Stat.Random(1, 3);
				if (num2 == 1)
				{
					text2 += "^C";
				}
				if (num2 == 2)
				{
					text2 += "^B";
				}
				if (num2 == 3)
				{
					text2 += "^Y";
				}
				Buffer.Write(text2 + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(Buffer, null, false);
				Thread.Sleep(10);
			}
		}

		// Token: 0x0600CA2A RID: 51754 RVA: 0x00499998 File Offset: 0x00497B98
		public static bool Cast(FreezingRay mutation = null, string level = "5-6")
		{
			if (mutation == null)
			{
				mutation = new FreezingRay();
				mutation.Level = level.RollCached();
				mutation.ParentObject = The.Player;
			}
			ScreenBuffer screenBuffer = null;
			List<Cell> list = mutation.PickLine(FreezingRay.RANGE, AllowVis.Any, null, false, true, true, false, null, null, "Freezing Ray", true, true);
			if (list == null || list.Count <= 0)
			{
				return false;
			}
			if (list.Count == 1 && mutation.ParentObject.IsPlayer() && Popup.ShowYesNoCancel("Are you sure you want to target " + mutation.ParentObject.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
			{
				return false;
			}
			if (!TutorialManager.AllowTargetPick((mutation != null) ? mutation.ParentObject : null, typeof(FreezingRay), list))
			{
				return false;
			}
			GameObject parentObject = mutation.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_freezingRay_attack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			mutation.UseEnergy(1000, "Physical Mutation Freezing Hands");
			mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, 20, null, null, false);
			mutation.PlayWorldSound(mutation.Sound, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			int i = 0;
			int num = Math.Min(list.Count, 10);
			while (i < num)
			{
				if (list.Count == 1 || list[i] != mutation.ParentObject.CurrentCell)
				{
					mutation.Freeze(list[i], ref screenBuffer);
				}
				if (i < num - 1 && list[i].IsSolidForProjectile(FreezingRay.Projectile, mutation.ParentObject, null, mutation.ParentObject.Target, null, null, false))
				{
					break;
				}
				i++;
			}
			BodyPart registeredSlot = mutation.GetRegisteredSlot(mutation.BodyPartType, false);
			IComponent<GameObject>.XDidY(mutation.ParentObject, "emit", "a freezing ray" + ((registeredSlot != null) ? (" from " + mutation.ParentObject.its + " " + registeredSlot.GetOrdinalName()) : ""), "!", null, null, mutation.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x0600CA2B RID: 51755 RVA: 0x00499BC8 File Offset: 0x00497DC8
		public bool CheckObjectProperlyEquipped()
		{
			return !this.CreateObject || (base.HasRegisteredSlot(this.BodyPartType) && base.GetRegisteredSlot(this.BodyPartType, false) != null);
		}

		// Token: 0x0600CA2C RID: 51756 RVA: 0x00499BF4 File Offset: 0x00497DF4
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
					string coolOnHitAmount = this.GetCoolOnHitAmount(base.Level);
					int num = -10 * base.Level;
					int num2 = coolOnHitAmount.RollMaxCached();
					if ((num2 > 0 && gameObjectParameter.Physics.Temperature < num) || (num2 < 0 && gameObjectParameter.Physics.Temperature > num))
					{
						gameObjectParameter.TemperatureChange(coolOnHitAmount.RollCached(), E.GetGameObjectParameter("Attacker"), false, false, false, false, this.ParentObject.GetPhase(), null, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CA2D RID: 51757 RVA: 0x00499CB3 File Offset: 0x00497EB3
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Physics.BrittleTemperature = -600 + -300 * base.Level;
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA2E RID: 51758 RVA: 0x00499CE0 File Offset: 0x00497EE0
		private void AddAbility()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Freezing Ray", FreezingRay.COMMAND_NAME, "Physical Mutations", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x0600CA2F RID: 51759 RVA: 0x00499D24 File Offset: 0x00497F24
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			GameObjectBlueprint value = GameObjectFactory.Factory.Blueprints.GetValue(Variant, null);
			this.BodyPartType = ((value != null) ? value.GetPartParameter<string>("Armor", "WornOn", "Hands") : null);
		}

		// Token: 0x0600CA30 RID: 51760 RVA: 0x00499D6B File Offset: 0x00497F6B
		public override string GetVariantName(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return null;
			}
			return Blueprint.GetPartParameter<string>("Armor", "WornOn", "Hands");
		}

		// Token: 0x0600CA31 RID: 51761 RVA: 0x00499D88 File Offset: 0x00497F88
		public override IRenderable GetIcon()
		{
			MutationEntry mutationEntry;
			if (!MutationFactory.TryGetMutationEntry(this, out mutationEntry))
			{
				return null;
			}
			return mutationEntry.GetRenderable();
		}

		// Token: 0x0600CA32 RID: 51762 RVA: 0x00499DA8 File Offset: 0x00497FA8
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

		// Token: 0x0600CA33 RID: 51763 RVA: 0x00499E1F File Offset: 0x0049801F
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			base.OnRegenerateDefaultEquipment(body);
		}

		// Token: 0x0600CA34 RID: 51764 RVA: 0x00499E28 File Offset: 0x00498028
		public void MakeFreezing(BodyPart part)
		{
			if (part == null)
			{
				return;
			}
			if (part.DefaultBehavior != null && part.DefaultBehavior.Blueprint != this.Variant)
			{
				part.DefaultBehavior.RequirePart<Icy>(false);
			}
			if (part.Parts != null)
			{
				for (int i = 0; i < part.Parts.Count; i++)
				{
					this.MakeFreezing(part.Parts[i]);
				}
			}
		}

		// Token: 0x0600CA35 RID: 51765 RVA: 0x00499E98 File Offset: 0x00498098
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
					firstPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "FreezingRay", false);
				}
				this.MakeFreezing(firstPart);
				if (this.BodyPartType == "Hands")
				{
					foreach (BodyPart bodyPart in Body.GetPart("Hand"))
					{
						this.MakeFreezing(bodyPart);
					}
				}
			}
			base.OnDecorateDefaultEquipment(Body);
		}

		// Token: 0x0600CA36 RID: 51766 RVA: 0x00499FC0 File Offset: 0x004981C0
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
			if (GO.Physics != null)
			{
				this.OldFreeze = GO.Physics.FreezeTemperature;
				this.OldBrittle = GO.Physics.BrittleTemperature;
			}
			this.AddAbility();
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA37 RID: 51767 RVA: 0x0049A0A8 File Offset: 0x004982A8
		public override bool Unmutate(GameObject GO)
		{
			if (GO.Physics != null)
			{
				if (this.OldFreeze != -1)
				{
					GO.Physics.FreezeTemperature = this.OldFreeze;
				}
				if (this.OldBrittle != -1)
				{
					GO.Physics.BrittleTemperature = this.OldBrittle;
				}
				this.OldFreeze = -1;
				this.OldBrittle = -1;
				GO.Physics.Temperature = 25;
			}
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EE5 RID: 20197
		public static readonly string COMMAND_NAME = "CommandFreezingRay";

		// Token: 0x04004EE6 RID: 20198
		public static readonly string PROJECTILE_BLUEPRINT = "ProjectileFreezingRay";

		// Token: 0x04004EE7 RID: 20199
		public static readonly int RANGE = 9;

		// Token: 0x04004EE8 RID: 20200
		public string BodyPartType;

		// Token: 0x04004EE9 RID: 20201
		public bool CreateObject = true;

		// Token: 0x04004EEA RID: 20202
		public int OldFreeze = -1;

		// Token: 0x04004EEB RID: 20203
		public int OldBrittle = -1;

		// Token: 0x04004EEC RID: 20204
		public string Sound = "Sounds/Abilities/sfx_ability_mutation_freezingRay_attack";

		// Token: 0x04004EED RID: 20205
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
