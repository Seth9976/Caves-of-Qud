using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Language;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Units
{
	// Token: 0x020009AD RID: 2477
	[Serializable]
	public class GameObjectMetachromeUnit : GameObjectUnit
	{
		// Token: 0x06006AD8 RID: 27352 RVA: 0x00226914 File Offset: 0x00224B14
		public override void Apply(GameObject Object)
		{
			GolemQuestSystem golemQuestSystem = GolemQuestSystem.Get();
			Body body;
			if (golemQuestSystem == null)
			{
				body = null;
			}
			else
			{
				GameObject material = golemQuestSystem.Body.Material;
				body = ((material != null) ? material.Body : null);
			}
			Body body2 = body ?? Object.Body;
			Dictionary<string, List<BodyPart>> dictionary = new Dictionary<string, List<BodyPart>>();
			foreach (BodyPart bodyPart in Object.Body.LoopParts())
			{
				if (bodyPart.Type != null && !bodyPart.Abstract && !bodyPart.Extrinsic && !GameObjectMetachromeUnit.ExcludedTypes.Contains(bodyPart.Type) && (bodyPart.VariantType == null || !GameObjectMetachromeUnit.ExcludedTypes.Contains(bodyPart.VariantType)))
				{
					if (bodyPart.DefaultBehavior != null)
					{
						if (!this.IsDefaultWeapon(bodyPart.DefaultBehavior))
						{
							continue;
						}
						bodyPart.DefaultBehavior = null;
						bodyPart.DefaultBehaviorBlueprint = null;
					}
					if (this.IsCursed(bodyPart.Equipped))
					{
						if (this.IsDefaultWeapon(bodyPart.Equipped))
						{
							MeleeWeapon part = bodyPart.Equipped.GetPart<MeleeWeapon>();
							if (part != null)
							{
								part.Skill = this.Skill;
							}
						}
					}
					else
					{
						if (this.IsDefaultWeapon(bodyPart.Equipped))
						{
							bodyPart.Unequip();
						}
						List<BodyPart> list;
						if (!dictionary.TryGetValue(bodyPart.Type, out list))
						{
							list = (dictionary[bodyPart.Type] = new List<BodyPart>());
						}
						list.Add(bodyPart);
					}
				}
			}
			List<string> list2 = new List<string>();
			foreach (BodyPart bodyPart2 in body2.LoopParts())
			{
				if (dictionary.ContainsKey(bodyPart2.Type) && !list2.Contains(bodyPart2.Type) && (this.IsDefaultWeapon(bodyPart2.Equipped) || this.IsDefaultWeapon(bodyPart2.DefaultBehavior)))
				{
					list2.Add(bodyPart2.Type);
				}
			}
			if (list2.IsNullOrEmpty<string>())
			{
				BodyPart bodyPart3 = body2.LoopParts().FirstOrDefault<BodyPart>((BodyPart x) => x.Primary);
				string text = ((bodyPart3 != null) ? bodyPart3.Type : null);
				if (!text.IsNullOrEmpty() && dictionary.ContainsKey(text))
				{
					list2.Add(text);
				}
				else
				{
					text = body2.GetPrimaryLimbType();
					if (dictionary.ContainsKey(text))
					{
						list2.Add(text);
					}
					else
					{
						text = Object.Body.GetPrimaryLimbType();
						list2.Add(dictionary.ContainsKey(text) ? text : dictionary.Keys.GetRandomElement<string>(null));
					}
				}
			}
			foreach (string text2 in list2)
			{
				List<BodyPart> list3;
				GameObjectBlueprint gameObjectBlueprint;
				if (!dictionary.TryGetValue(text2, out list3))
				{
					MetricsManager.LogError("No limbs by slot '" + text2 + "' found for " + Object.ShortDisplayNameStripped);
				}
				else if (GameObjectMetachromeUnit.TryGetBlueprintFor(text2, this.Skill, out gameObjectBlueprint))
				{
					foreach (BodyPart bodyPart4 in list3)
					{
						if (bodyPart4.DefaultBehavior == null)
						{
							GameObjectMetachromeUnit.SetDefaultBehavior(bodyPart4, gameObjectBlueprint);
						}
					}
				}
			}
			Object.Body.RecalculateFirsts();
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x00226CD0 File Offset: 0x00224ED0
		public static void SetDefaultBehavior(BodyPart Limb, GameObjectBlueprint Blueprint)
		{
			Limb.DefaultBehaviorBlueprint = Blueprint.Name;
			Limb.DefaultBehavior = GameObjectFactory.Factory.CreateUnmodifiedObject(Blueprint, null, null);
			MeleeWeapon part = Limb.DefaultBehavior.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.Slot = Limb.Type;
			}
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x00226D18 File Offset: 0x00224F18
		public static bool TryGetBlueprintFor(string Type, string Skill, out GameObjectBlueprint Blueprint)
		{
			GameObjectFactory factory = GameObjectFactory.Factory;
			if (!factory.Blueprints.TryGetValue("Metachrome " + Type + " " + Skill, out Blueprint))
			{
				MetricsManager.LogError(string.Concat(new string[] { "No blueprint by id 'Metachrome ", Type, " ", Skill, "' found, defaulting to 'Metachrome ", Skill, "'" }));
				if (!factory.Blueprints.TryGetValue("Metachrome " + Skill, out Blueprint))
				{
					MetricsManager.LogError(string.Concat(new string[] { "No blueprint by id 'Metachrome ", Skill, "' found, skipping '", Type, "' slot" }));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x00226DD5 File Offset: 0x00224FD5
		private bool IsCursed(GameObject Object)
		{
			return Object != null && !Object.CanBeUnequipped(null, null, false, false);
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x00226DE9 File Offset: 0x00224FE9
		private bool IsDefaultWeapon(GameObject Object)
		{
			return Object != null && Object.IsNatural() && Object.GetBlueprint(true).DescendsFrom("NaturalWeapon");
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x00226E09 File Offset: 0x00225009
		public override void Reset()
		{
			base.Reset();
			this.Skill = null;
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x00226E18 File Offset: 0x00225018
		public override bool CanInscribe()
		{
			return false;
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x00226E1C File Offset: 0x0022501C
		public override string GetDescription(bool Inscription = false)
		{
			string text = "Metachrome " + this.Skill;
			GameObjectBlueprint gameObjectBlueprint;
			if (!GameObjectFactory.Factory.Blueprints.TryGetValue(text, out gameObjectBlueprint))
			{
				return "[GameObjectMetachromeUnit Error: Unknown blueprint '" + text + "']";
			}
			return "Equipped with " + Grammar.Pluralize(gameObjectBlueprint.DisplayName());
		}

		// Token: 0x040030EE RID: 12526
		public string Skill;

		// Token: 0x040030EF RID: 12527
		private static readonly string[] ExcludedTypes = new string[] { "Missile Weapon", "Thrown Weapon", "Ammo" };
	}
}
