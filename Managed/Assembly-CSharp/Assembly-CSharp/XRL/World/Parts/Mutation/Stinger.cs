using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001456 RID: 5206
	[Serializable]
	public class Stinger : BaseDefaultEquipmentMutation
	{
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600CBAF RID: 52143 RVA: 0x004A118C File Offset: 0x0049F38C
		public IStingerProperties StingerProperties
		{
			get
			{
				if (this._StingerProperties == null)
				{
					GameObject stingerObject = this.StingerObject;
					this._StingerProperties = ((stingerObject != null) ? stingerObject.GetPartDescendedFrom<IStingerProperties>() : null);
					GameObjectBlueprint gameObjectBlueprint;
					if (this._StingerProperties == null && !this.Variant.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(this.Variant, out gameObjectBlueprint))
					{
						foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair in gameObjectBlueprint.Parts)
						{
							GamePartBlueprint value = keyValuePair.Value;
							if (typeof(IStingerProperties).IsAssignableFrom(value.T))
							{
								this._StingerProperties = (IStingerProperties)value.Reflector.GetNewInstance();
								value.InitializePartInstance(this._StingerProperties);
								break;
							}
						}
					}
					if (this._StingerProperties == null)
					{
						this._StingerProperties = new StingerPoisonProperties();
					}
				}
				return this._StingerProperties;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600CBB0 RID: 52144 RVA: 0x004A1290 File Offset: 0x0049F490
		public override bool CanSelectVariant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600CBB1 RID: 52145 RVA: 0x004A1293 File Offset: 0x0049F493
		public Stinger()
		{
		}

		// Token: 0x0600CBB2 RID: 52146 RVA: 0x004A12A6 File Offset: 0x0049F4A6
		[Obsolete("mod compat")]
		public Stinger(string VenomType)
			: this()
		{
		}

		// Token: 0x0600CBB3 RID: 52147 RVA: 0x004A12AE File Offset: 0x0049F4AE
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Stinger stinger = base.DeepCopy(Parent, MapInv) as Stinger;
			stinger.StingerObject = null;
			stinger._StingerProperties = null;
			return stinger;
		}

		// Token: 0x0600CBB4 RID: 52148 RVA: 0x004A12CB File Offset: 0x0049F4CB
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CBB5 RID: 52149 RVA: 0x004A12CE File Offset: 0x0049F4CE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600CBB6 RID: 52150 RVA: 0x004A12F4 File Offset: 0x0049F4F4
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.StingActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBB7 RID: 52151 RVA: 0x004A1318 File Offset: 0x0049F518
		public override bool HandleEvent(CommandEvent E)
		{
			if (!(E.Command == "CommandSting"))
			{
				return base.HandleEvent(E);
			}
			if (this.StingerObject == null || !this.StingerObject.IsEquippedProperly(null))
			{
				return this.ParentObject.ShowFailure("You don't have a stinger.");
			}
			if (!this.ParentObject.CanMoveExtremities("Sting", true, false, false))
			{
				return false;
			}
			Cell cell = base.PickDirection("Sting", null);
			if (cell == null)
			{
				return false;
			}
			GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
			if (combatTarget == null || combatTarget == this.ParentObject)
			{
				if (this.ParentObject.IsPlayer())
				{
					if (cell.HasObjectWithPart("Combat"))
					{
						Popup.ShowFail("There is no one there you can sting.", true, true, true);
					}
					else
					{
						Popup.ShowFail("There is no one there to sting.", true, true, true);
					}
				}
				return false;
			}
			base.UseEnergy(1000, "Physical Mutation Stinger");
			base.CooldownMyActivatedAbility(this.StingActivatedAbilityID, 25, null, null, false);
			if (this.ParentObject.IsPlayer())
			{
				base.DidX("strike", combatTarget.the + combatTarget.ShortDisplayName + " with your stinger", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			else
			{
				base.DidX("strike", string.Concat(new string[]
				{
					combatTarget.a,
					combatTarget.ShortDisplayName,
					" with ",
					this.ParentObject.its,
					" stinger"
				}), "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			this.Strike(this.StingerObject, combatTarget, true);
			return false;
		}

		// Token: 0x0600CBB8 RID: 52152 RVA: 0x004A14C8 File Offset: 0x0049F6C8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && GameObject.Validate(E.Target) && !E.Target.IsWall() && base.IsMyActivatedAbilityAIUsable(this.StingActivatedAbilityID, null) && E.Actor.CanMoveExtremities("Sting", false, false, false))
			{
				E.Add("CommandSting", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBB9 RID: 52153 RVA: 0x004A1534 File Offset: 0x0049F734
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CBBA RID: 52154 RVA: 0x004A1537 File Offset: 0x0049F737
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LungedTarget");
		}

		// Token: 0x0600CBBB RID: 52155 RVA: 0x004A1544 File Offset: 0x0049F744
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LungedTarget" && this.StingerObject != null && this.StingerObject.IsEquippedProperly(null) && !this.ParentObject.Body.IsPrimaryWeapon(this.StingerObject))
			{
				this.Strike(this.StingerObject, E.GetGameObjectParameter("Defender"), false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CBBC RID: 52156 RVA: 0x004A15B0 File Offset: 0x0049F7B0
		public override string GetDescription()
		{
			return this.StingerProperties.GetDescription();
		}

		// Token: 0x0600CBBD RID: 52157 RVA: 0x004A15C0 File Offset: 0x0049F7C0
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("StingerPen", this.GetPenetration(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("StingerDamage", this.GetDamage(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("Duration", this.StingerProperties.GetDuration(Level), false, 0);
			stats.Set("VenomType", this.StingerProperties.GetAdjective(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600CBBE RID: 52158 RVA: 0x004A1668 File Offset: 0x0049F868
		public void Strike(GameObject Stinger, GameObject Defender, bool Auto = false)
		{
			Combat.MeleeAttackWithWeapon(this.ParentObject, Defender, Stinger, this.ParentObject.GetBodyPartByManager(this.ManagerID, false), Auto ? "Autohit,Autopen,Stinging" : "Stinging", 0, 0, 0, 0, 0, false, true);
		}

		// Token: 0x0600CBBF RID: 52159 RVA: 0x004A16AB File Offset: 0x0049F8AB
		public int GetSave(int Level)
		{
			return 14 + Level * 2;
		}

		// Token: 0x0600CBC0 RID: 52160 RVA: 0x004A16B3 File Offset: 0x0049F8B3
		public int GetCooldown(int Level)
		{
			return this.StingerProperties.GetCooldown(Level);
		}

		// Token: 0x0600CBC1 RID: 52161 RVA: 0x004A16C1 File Offset: 0x0049F8C1
		public string GetDamage(int Level)
		{
			return this.StingerProperties.GetDamage(Level);
		}

		// Token: 0x0600CBC2 RID: 52162 RVA: 0x004A16CF File Offset: 0x0049F8CF
		public int GetPenetration(int Level)
		{
			return this.StingerProperties.GetPenetration(Level);
		}

		// Token: 0x0600CBC3 RID: 52163 RVA: 0x004A16E0 File Offset: 0x0049F8E0
		public override IRenderable GetIcon()
		{
			MutationEntry mutationEntry;
			if (!MutationFactory.TryGetMutationEntry(this, out mutationEntry))
			{
				return null;
			}
			return mutationEntry.GetRenderable();
		}

		// Token: 0x0600CBC4 RID: 52164 RVA: 0x004A1700 File Offset: 0x0049F900
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder("20% chance on melee attack to sting your opponent ({{c|\u001a}}{{rules|").Append(this.GetPenetration(Level) + RuleSettings.VISUAL_PENETRATION_BONUS).Append("}} {{r|\u0003}}{{rules|")
				.Append(this.GetDamage(Level))
				.Append("}})\n")
				.Append("Stinger is a long blade and can only penetrate once.\nAlways sting on charge or lunge.\nStinger applies venom on damage (only 20% chance if Stinger is your primary weapon).\nMay use Sting activated ability to strike with your stinger and automatically hit and penetrate.\nSting cooldown: ")
				.Append(this.GetCooldown(Level))
				.Append('\n');
			this.StingerProperties.AppendLevelText(stringBuilder, Level);
			stringBuilder.Append("+200 reputation with {{w|arachnids}}");
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x0600CBC5 RID: 52165 RVA: 0x004A1786 File Offset: 0x0049F986
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600CBC6 RID: 52166 RVA: 0x004A178F File Offset: 0x0049F98F
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::Stinger";
			}
		}

		// Token: 0x0600CBC7 RID: 52167 RVA: 0x004A17A8 File Offset: 0x0049F9A8
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			BodyPart partByManager = body.GetPartByManager(this.ManagerID, false);
			if (partByManager != null)
			{
				this.AddStingerTo(partByManager);
			}
		}

		// Token: 0x0600CBC8 RID: 52168 RVA: 0x004A17CD File Offset: 0x0049F9CD
		public static bool IsUnmanagedPart(BodyPart Part)
		{
			return Part.Manager.IsNullOrEmpty();
		}

		// Token: 0x0600CBC9 RID: 52169 RVA: 0x004A17DC File Offset: 0x0049F9DC
		public static BodyPart AddTail(GameObject Object, string ManagerID, bool UseUnmanaged = false, bool DoUpdate = true)
		{
			BodyPart bodyPart;
			if (Object == null)
			{
				bodyPart = null;
			}
			else
			{
				Body body = Object.Body;
				bodyPart = ((body != null) ? body.GetBody() : null);
			}
			BodyPart bodyPart2 = bodyPart;
			if (bodyPart2 == null)
			{
				return null;
			}
			if (UseUnmanaged)
			{
				BodyPart firstPart = bodyPart2.GetFirstPart("Tail", new Predicate<BodyPart>(Stinger.IsUnmanagedPart));
				if (firstPart != null)
				{
					firstPart.Manager = ManagerID;
					return firstPart;
				}
			}
			Object.WantToReequip();
			BodyPart firstAttachedPart = bodyPart2.GetFirstAttachedPart("Tail", 0, Object.Body, true);
			int? num;
			if (firstAttachedPart != null)
			{
				firstAttachedPart.ChangeLaterality(2, false);
				BodyPart bodyPart3 = bodyPart2;
				num = new int?(bodyPart2.Category);
				return bodyPart3.AddPartAt(firstAttachedPart, "Tail", 1, null, null, null, null, ManagerID, num, null, null, null, null, null, null, null, null, null, null, null, null, DoUpdate);
			}
			BodyPart bodyPart4 = bodyPart2;
			string text = "Tail";
			int num2 = 0;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			num = new int?(bodyPart2.Category);
			int? num3 = num;
			string[] array = new string[] { "Roots", "Thrown Weapon", "Floating Nearby" };
			return bodyPart4.AddPartAt(text, num2, text2, text3, text4, text5, ManagerID, num3, null, null, null, null, null, null, null, null, null, null, null, null, "Feet", array, DoUpdate);
		}

		// Token: 0x0600CBCA RID: 52170 RVA: 0x004A19BC File Offset: 0x0049FBBC
		public static void RemoveTail(GameObject Object, string ManagerID)
		{
			BodyPart bodyPart = Object.GetBodyPartByManager(ManagerID, false);
			if (bodyPart == null)
			{
				return;
			}
			int laterality = bodyPart.Laterality;
			int num = ((bodyPart.Laterality == 1) ? 2 : 1);
			Object.RemoveBodyPartsByManager(ManagerID, true);
			bodyPart = Object.GetFirstBodyPart("Tail", num);
			if (bodyPart != null && bodyPart.IsLateralityConsistent(null) && !Object.HasBodyPart("Tail", laterality))
			{
				bodyPart.ChangeLaterality(0, false);
			}
			Object.WantToReequip();
		}

		// Token: 0x0600CBCB RID: 52171 RVA: 0x004A1A2C File Offset: 0x0049FC2C
		public void AddStingerTo(BodyPart Limb)
		{
			this._StingerProperties = null;
			if (this.StingerObject == null)
			{
				this.StingerObject = GameObject.Create(this.Variant ?? "Stinger");
			}
			int level = base.Level;
			bool flag = this.StingerObject.EquipAsDefaultBehavior();
			MeleeWeapon part = this.StingerObject.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.BaseDamage = this.GetDamage(level);
				part.PenBonus = this.GetPenetration(level);
				part.Slot = Limb.Type;
			}
			if (flag && Limb.DefaultBehavior != null)
			{
				if (Limb.DefaultBehavior == this.StingerObject)
				{
					return;
				}
				Limb.DefaultBehavior = null;
			}
			if (!flag && Limb.Equipped != null)
			{
				if (Limb.Equipped == this.StingerObject)
				{
					return;
				}
				if (Limb.Equipped.CanBeUnequipped(null, null, false, true))
				{
					Limb.ForceUnequip(true, false, false);
				}
			}
			if (!Limb.Equip(this.StingerObject, new int?(0), true, false, false, true))
			{
				base.CleanUpMutationEquipment(this.ParentObject, ref this.StingerObject);
			}
		}

		// Token: 0x0600CBCC RID: 52172 RVA: 0x004A1B2C File Offset: 0x0049FD2C
		public override bool Mutate(GameObject GO, int Level)
		{
			BodyPart bodyPart = Stinger.AddTail(GO, this.ManagerID, true, true);
			if (bodyPart != null)
			{
				this.AddStingerTo(bodyPart);
				this.StingActivatedAbilityID = base.AddMyActivatedAbility("Sting", "CommandSting", "Physical Mutations", null, "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CBCD RID: 52173 RVA: 0x004A1B8E File Offset: 0x0049FD8E
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.StingActivatedAbilityID, null);
			base.CleanUpMutationEquipment(GO, ref this.StingerObject);
			Stinger.RemoveTail(GO, this.ManagerID);
			this._StingerProperties = null;
			return base.Unmutate(GO);
		}

		// Token: 0x04004F29 RID: 20265
		public const string COMMAND_NAME = "CommandSting";

		// Token: 0x04004F2A RID: 20266
		public GameObject StingerObject;

		// Token: 0x04004F2B RID: 20267
		public Guid StingActivatedAbilityID = Guid.Empty;

		// Token: 0x04004F2C RID: 20268
		[NonSerialized]
		private IStingerProperties _StingerProperties;
	}
}
