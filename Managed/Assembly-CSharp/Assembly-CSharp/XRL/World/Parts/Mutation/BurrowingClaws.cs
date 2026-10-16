using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142D RID: 5165
	[Serializable]
	public class BurrowingClaws : BaseDefaultEquipmentMutation
	{
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600C94B RID: 51531 RVA: 0x00494FAD File Offset: 0x004931AD
		public GameObjectBlueprint Blueprint
		{
			get
			{
				if (this._Blueprint == null)
				{
					this._Blueprint = GameObjectFactory.Factory.GetBlueprint(this.GetBlueprintName());
				}
				return this._Blueprint;
			}
		}

		// Token: 0x0600C94D RID: 51533 RVA: 0x0049500E File Offset: 0x0049320E
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C94E RID: 51534 RVA: 0x00495014 File Offset: 0x00493214
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			int wallBonusPenetration = BurrowingClaws.GetWallBonusPenetration(Level);
			if (wallBonusPenetration < 0)
			{
				stats.Set("WallPenetration", "-" + wallBonusPenetration.ToString(), !stats.mode.Contains("ability"), 0);
				return;
			}
			stats.Set("WallPenetration", "+" + wallBonusPenetration.ToString(), !stats.mode.Contains("ability"), 0);
		}

		// Token: 0x0600C94F RID: 51535 RVA: 0x0049508D File Offset: 0x0049328D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<AfterGameLoadedEvent>.ID || ID == PooledEvent<PartSupportEvent>.ID || ID == PooledEvent<PathAsBurrowerEvent>.ID || ID == PooledEvent<PreferDefaultBehaviorEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C950 RID: 51536 RVA: 0x004950C3 File Offset: 0x004932C3
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.EnableActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C951 RID: 51537 RVA: 0x004950E7 File Offset: 0x004932E7
		public override bool HandleEvent(PathAsBurrowerEvent E)
		{
			return (!this.PathAsBurrower || !base.IsMyActivatedAbilityToggledOn(this.EnableActivatedAbilityID, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600C952 RID: 51538 RVA: 0x00495109 File Offset: 0x00493309
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "Digging") || !base.IsMyActivatedAbilityToggledOn(this.EnableActivatedAbilityID, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600C953 RID: 51539 RVA: 0x0049513E File Offset: 0x0049333E
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			NeedPartSupportEvent.Send(this.ParentObject, "Digging", null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C954 RID: 51540 RVA: 0x00495158 File Offset: 0x00493358
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandDigDown");
			Registrar.Register("CommandDigUp");
			Registrar.Register("CommandToggleBurrowingClaws");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C955 RID: 51541 RVA: 0x00495183 File Offset: 0x00493383
		public string GetBlueprintName()
		{
			return this.Variant.Coalesce(BurrowingClaws.OBJECT_BLUEPRINT_NAME);
		}

		// Token: 0x0600C956 RID: 51542 RVA: 0x00495195 File Offset: 0x00493395
		public bool CheckDig()
		{
			if (this.ParentObject.AreHostilesNearby())
			{
				Popup.ShowFail("You can't excavate with hostiles nearby.", true, true, true);
				return false;
			}
			return true;
		}

		// Token: 0x0600C957 RID: 51543 RVA: 0x004951B4 File Offset: 0x004933B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandToggleBurrowingClaws")
			{
				base.ToggleMyActivatedAbility(this.EnableActivatedAbilityID, null, false, null);
				if (base.IsMyActivatedAbilityToggledOn(this.EnableActivatedAbilityID, null))
				{
					this.ParentObject.RequirePart<Digging>(false);
				}
				else
				{
					NeedPartSupportEvent.Send(this.ParentObject, "Digging", null);
				}
				this.ParentObject.RemoveStringProperty("Burrowing");
			}
			else if (E.ID == "CommandDigDown")
			{
				if (this.CheckDig())
				{
					Cell cellFromDirection = this.ParentObject.CurrentCell.GetCellFromDirection("D", false);
					this.ParentObject.CurrentCell.AddObject("StairsDown", null, null, null, null);
					cellFromDirection.AddObject("StairsUp", null, null, null, null);
					this.ParentObject.PlayWorldSound("sfx_ability_mutation_burrowingClaws_burrow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					base.DidX("dig", "a passage up", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			else if (E.ID == "CommandDigUp")
			{
				if (this.ParentObject.CurrentZone.IsOutside())
				{
					Popup.ShowFail("You can't excavate the sky!", true, true, true);
					return true;
				}
				if (this.CheckDig())
				{
					Cell cellFromDirection2 = this.ParentObject.CurrentCell.GetCellFromDirection("U", false);
					this.ParentObject.CurrentCell.AddObject("StairsUp", null, null, null, null);
					cellFromDirection2.AddObject("StairsDown", null, null, null, null);
					GameObject parentObject = this.ParentObject;
					if (parentObject != null)
					{
						parentObject.PlayWorldSound("sfx_ability_mutation_burrowingClaws_burrow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					base.DidX("dig", "a passage down", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C958 RID: 51544 RVA: 0x004953C6 File Offset: 0x004935C6
		public string GetPenetration()
		{
			return BurrowingClaws.GetPenetration(base.Level);
		}

		// Token: 0x0600C959 RID: 51545 RVA: 0x004953D4 File Offset: 0x004935D4
		public static string GetPenetration(int Level)
		{
			return "1d6+" + (3 * Level).ToString();
		}

		// Token: 0x0600C95A RID: 51546 RVA: 0x004953F6 File Offset: 0x004935F6
		public int GetWallBonusPenetration()
		{
			return BurrowingClaws.GetWallBonusPenetration(base.Level);
		}

		// Token: 0x0600C95B RID: 51547 RVA: 0x00495403 File Offset: 0x00493603
		public static int GetWallBonusPenetration(int Level)
		{
			return Level * 3;
		}

		// Token: 0x0600C95C RID: 51548 RVA: 0x00495408 File Offset: 0x00493608
		public double GetWallBonusPercentage()
		{
			return BurrowingClaws.GetWallBonusPercentage(base.Level, this.ParentObject);
		}

		// Token: 0x0600C95D RID: 51549 RVA: 0x0049541C File Offset: 0x0049361C
		public static double GetWallBonusPercentage(int Level, GameObject Mutant = null)
		{
			int num = 25;
			if (Mutant != null && Mutant.IsGiganticCreature)
			{
				num *= 2;
			}
			return (double)num;
		}

		// Token: 0x0600C95E RID: 51550 RVA: 0x0049543D File Offset: 0x0049363D
		public int GetWallHitsRequired()
		{
			return BurrowingClaws.GetWallHitsRequired(base.Level, this.ParentObject);
		}

		// Token: 0x0600C95F RID: 51551 RVA: 0x00495450 File Offset: 0x00493650
		public static int GetWallHitsRequired(int Level, GameObject Mutant = null)
		{
			return Drill.GetWallHitsRequired(BurrowingClaws.GetWallBonusPercentage(Level, Mutant));
		}

		// Token: 0x0600C960 RID: 51552 RVA: 0x0049545E File Offset: 0x0049365E
		public int GetAV(int Level)
		{
			if (Level < 5)
			{
				return 1;
			}
			if (Level < 9)
			{
				return 2;
			}
			return 3;
		}

		// Token: 0x0600C961 RID: 51553 RVA: 0x0049546E File Offset: 0x0049366E
		public override string GetDescription()
		{
			return this.Blueprint.GetTag("VariantDescription", "").Coalesce("You bear spade-like claws that can burrow through the earth.");
		}

		// Token: 0x0600C962 RID: 51554 RVA: 0x00495490 File Offset: 0x00493690
		public override string GetLevelText(int Level)
		{
			string cachedDisplayNameStrippedTitleCase = this.Blueprint.CachedDisplayNameStrippedTitleCase;
			string text = Grammar.Pluralize(cachedDisplayNameStrippedTitleCase);
			int wallBonusPenetration = BurrowingClaws.GetWallBonusPenetration(Level);
			StringBuilder stringBuilder = Event.NewStringBuilder(null).Append(cachedDisplayNameStrippedTitleCase).Append(" penetration vs. walls: {{rules|")
				.Append(wallBonusPenetration.Signed(false))
				.Append("}}\n");
			int wallHitsRequired = BurrowingClaws.GetWallHitsRequired(Level, this.ParentObject);
			if (wallHitsRequired > 0)
			{
				stringBuilder.Append(text).Append(" destroy walls after ").Append(wallHitsRequired)
					.Append(" penetrating hits.\n");
			}
			if (Options.EnablePrereleaseContent)
			{
				stringBuilder.Append("Can dig passages up or down when outside of combat\n");
			}
			stringBuilder.Append(text).Append(" are also a ").Append(this.GetWeaponClass())
				.Append(" class natural weapon that deal {{rules|")
				.Append(this.GetClawsDamage(Level))
				.Append("}} base damage to non-walls.");
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x0600C963 RID: 51555 RVA: 0x00495570 File Offset: 0x00493770
		public string GetWeaponClass()
		{
			string partParameter = this.Blueprint.GetPartParameter<string>("MeleeWeapon", "Skill", "ShortBlades");
			string text;
			if (Skills.WeaponClassName.TryGetValue(partParameter, out text))
			{
				return text;
			}
			return "short-blade";
		}

		// Token: 0x0600C964 RID: 51556 RVA: 0x004955B0 File Offset: 0x004937B0
		public string GetClawsDamage(int Level)
		{
			ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
			foreach (ReadOnlySpan<char> readOnlySpan2 in this.Blueprint.GetTag("VariantDamage", "").DelimitedBy(','))
			{
				int num = readOnlySpan2.IndexOf(':');
				int num2;
				if (int.TryParse(readOnlySpan2.Slice(0, num), out num2) && num2 <= Level)
				{
					readOnlySpan = readOnlySpan2.Slice(num + 1);
				}
			}
			if (readOnlySpan.Length == 0)
			{
				return "1d2";
			}
			return new string(readOnlySpan);
		}

		// Token: 0x0600C965 RID: 51557 RVA: 0x00495640 File Offset: 0x00493840
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			GameObjectBlueprint blueprint = this.Blueprint;
			string partParameter = blueprint.GetPartParameter<string>("MeleeWeapon", "Slot", "Hand");
			List<BodyPart> part = body.GetPart(partParameter);
			int level = base.Level;
			int num = 0;
			while (num < part.Count && num < 2)
			{
				BodyPart bodyPart = part[num];
				if (bodyPart.DefaultBehavior == null || bodyPart.DefaultBehavior.GetBlueprint(true) != blueprint)
				{
					bodyPart.DefaultBehavior = GameObject.Create(blueprint, 0, 0, null, null, null, null, null);
					bodyPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "BurrowingClaws", false);
				}
				MeleeWeapon meleeWeapon;
				if (bodyPart.DefaultBehavior.TryGetPart<MeleeWeapon>(out meleeWeapon))
				{
					meleeWeapon.BaseDamage = this.GetClawsDamage(level);
				}
				BurrowingClawsProperties burrowingClawsProperties;
				if (bodyPart.DefaultBehavior.TryGetPart<BurrowingClawsProperties>(out burrowingClawsProperties))
				{
					burrowingClawsProperties.WallBonusPenetration = BurrowingClaws.GetWallBonusPenetration(level);
					burrowingClawsProperties.WallBonusPercentage = BurrowingClaws.GetWallBonusPercentage(level, null);
				}
				num++;
			}
		}

		// Token: 0x0600C966 RID: 51558 RVA: 0x00495730 File Offset: 0x00493930
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			this._Blueprint = null;
		}

		// Token: 0x0600C967 RID: 51559 RVA: 0x00495740 File Offset: 0x00493940
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C968 RID: 51560 RVA: 0x0049574C File Offset: 0x0049394C
		public override bool Mutate(GameObject GO, int Level)
		{
			if (Options.EnablePrereleaseContent)
			{
				this.DigUpActivatedAbilityID = base.AddMyActivatedAbility("Excavate up", "CommandDigUp", "Physical Mutations", null, "\u0018", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
				this.DigDownActivatedAbilityID = base.AddMyActivatedAbility("Excavate down", "CommandDigDown", "Physical Mutations", null, "\u0019", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			this.EnableActivatedAbilityID = base.AddMyActivatedAbility(base.GetDisplayName(true), "CommandToggleBurrowingClaws", "Physical Mutations", null, "ë", null, true, true, true, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			if (base.IsMyActivatedAbilityToggledOn(this.EnableActivatedAbilityID, null))
			{
				GO.RequirePart<Digging>(false);
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C969 RID: 51561 RVA: 0x0049581D File Offset: 0x00493A1D
		public override bool Unmutate(GameObject GO)
		{
			NeedPartSupportEvent.Send(GO, "Digging", this);
			base.RemoveMyActivatedAbility(ref this.DigUpActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.DigDownActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.EnableActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EB1 RID: 20145
		public static readonly string OBJECT_BLUEPRINT_NAME = "Burrowing Claws Claw";

		// Token: 0x04004EB2 RID: 20146
		public string BodyPartType = "Hands";

		// Token: 0x04004EB3 RID: 20147
		public Guid DigUpActivatedAbilityID = Guid.Empty;

		// Token: 0x04004EB4 RID: 20148
		public Guid DigDownActivatedAbilityID = Guid.Empty;

		// Token: 0x04004EB5 RID: 20149
		public Guid EnableActivatedAbilityID = Guid.Empty;

		// Token: 0x04004EB6 RID: 20150
		public bool PathAsBurrower = true;

		// Token: 0x04004EB7 RID: 20151
		[NonSerialized]
		protected GameObjectBlueprint _Blueprint;
	}
}
