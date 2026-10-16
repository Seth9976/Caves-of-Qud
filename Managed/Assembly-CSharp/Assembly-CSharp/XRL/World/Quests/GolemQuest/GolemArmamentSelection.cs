using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2A RID: 2858
	[HasWishCommand]
	[Serializable]
	public class GolemArmamentSelection : GolemGameObjectSelection
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x002B6D40 File Offset: 0x002B4F40
		public override string ID
		{
			get
			{
				return "Armament";
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x002B6D47 File Offset: 0x002B4F47
		public override string DisplayName
		{
			get
			{
				return "armament";
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060072C6 RID: 29382 RVA: 0x002B6D4E File Offset: 0x002B4F4E
		public override char Key
		{
			get
			{
				return 'r';
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x002B6D52 File Offset: 0x002B4F52
		public override bool IsCarried
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060072C8 RID: 29384 RVA: 0x002B6D55 File Offset: 0x002B4F55
		public override int Consumed
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x002B6D58 File Offset: 0x002B4F58
		public override bool IsValid(GameObject Object)
		{
			return base.IsValid(Object) && (Object.HasTag("MeleeWeapon") || Object.HasTag("MissileWeapon")) && !Object.HasTag("UndesirableWeapon") && Object.HasPart<Zetachrome>();
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x002B6D92 File Offset: 0x002B4F92
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(GameObject Object)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			if (part != null && part.Skill != null)
			{
				yield return new GameObjectUnitAggregate(new GameObjectUnit[]
				{
					new GameObjectSkillUnit
					{
						Skill = part.Skill,
						Power = "*"
					},
					new GameObjectMetachromeUnit
					{
						Skill = part.Skill
					}
				});
			}
			MissileWeapon part2 = Object.GetPart<MissileWeapon>();
			if (part2 != null && part2.Skill != null)
			{
				yield return new GameObjectUnitAggregate(new GameObjectUnit[]
				{
					new GameObjectSkillUnit
					{
						Skill = part2.Skill,
						Power = "*"
					},
					new GameObjectMetachromeUnit
					{
						Skill = part2.Skill
					}
				});
			}
			yield break;
		}

		// Token: 0x060072CB RID: 29387 RVA: 0x002B6DA2 File Offset: 0x002B4FA2
		public override IEnumerable<GameObjectUnit> YieldAllEffects()
		{
			foreach (string text in GolemArmamentSelection.Skills)
			{
				yield return new GameObjectSkillUnit
				{
					Skill = text,
					Power = "*"
				};
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield return new GameObjectMetachromeUnit
			{
				Skill = GolemArmamentSelection.Skills.GetRandomElement<string>(null)
			};
			yield break;
			yield break;
		}

		// Token: 0x060072CC RID: 29388 RVA: 0x002B6DAB File Offset: 0x002B4FAB
		public override IEnumerable<GameObjectUnit> YieldRandomEffects()
		{
			string randomElement = GolemArmamentSelection.Skills.GetRandomElement<string>(null);
			yield return new GameObjectUnitAggregate(new GameObjectUnit[]
			{
				new GameObjectSkillUnit
				{
					Skill = randomElement,
					Power = "*"
				},
				new GameObjectMetachromeUnit
				{
					Skill = randomElement
				}
			});
			yield break;
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x002B6DB4 File Offset: 0x002B4FB4
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			GameObject material = this.Material;
			object obj;
			if (material == null)
			{
				obj = null;
			}
			else
			{
				MeleeWeapon part = material.GetPart<MeleeWeapon>();
				obj = ((part != null) ? part.Skill : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				GameObject material2 = this.Material;
				if (material2 == null)
				{
					obj2 = null;
				}
				else
				{
					MissileWeapon part2 = material2.GetPart<MissileWeapon>();
					obj2 = ((part2 != null) ? part2.Skill : null);
				}
			}
			string text = obj2;
			if (!text.IsNullOrEmpty())
			{
				Object.SetStringProperty("ArmamentSkill", text, false);
			}
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x002B6E1E File Offset: 0x002B501E
		public string GetFirstSkill()
		{
			return this.GetFirstSkill(this.Material);
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x002B6E2C File Offset: 0x002B502C
		public string GetFirstSkill(GameObject Object)
		{
			if (Object != null)
			{
				MeleeWeapon part = Object.GetPart<MeleeWeapon>();
				string text;
				if ((text = ((part != null) ? part.Skill : null)) == null)
				{
					MissileWeapon part2 = this.Material.GetPart<MissileWeapon>();
					if (part2 == null)
					{
						return null;
					}
					text = part2.Skill;
				}
				return text;
			}
			return null;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060072D0 RID: 29392 RVA: 0x002B6E60 File Offset: 0x002B5060
		public static List<string> Skills
		{
			get
			{
				if (GolemArmamentSelection._Skills == null)
				{
					GolemArmamentSelection._Skills = new List<string>();
					foreach (GameObjectBlueprint gameObjectBlueprint in GolemArmamentSelection.ZetachromeWeapons)
					{
						string text = gameObjectBlueprint.GetPartParameter<string>("MeleeWeapon", "Skill", null) ?? gameObjectBlueprint.GetPartParameter<string>("MissileWeapon", "Skill", null);
						if (!text.IsNullOrEmpty() && !GolemArmamentSelection._Skills.Contains(text))
						{
							GolemArmamentSelection._Skills.Add(text);
						}
					}
					GolemArmamentSelection._Skills.TrimExcess();
				}
				return GolemArmamentSelection._Skills;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060072D1 RID: 29393 RVA: 0x002B6F10 File Offset: 0x002B5110
		private static IEnumerable<GameObjectBlueprint> ZetachromeWeapons
		{
			get
			{
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if ((gameObjectBlueprint.HasPart("MeleeWeapon") || gameObjectBlueprint.HasPart("MissileWeapon")) && gameObjectBlueprint.HasPart("Zetachrome") && !gameObjectBlueprint.HasTag("UndesirableWeapon", false))
					{
						yield return gameObjectBlueprint;
					}
				}
				List<GameObjectBlueprint>.Enumerator enumerator = default(List<GameObjectBlueprint>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x002B6F19 File Offset: 0x002B5119
		[WishCommand("golemquest:armament", null)]
		private static void Wish()
		{
			GolemArmamentSelection.ReceiveMaterials(The.Player);
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x002B6F28 File Offset: 0x002B5128
		public static void ReceiveMaterials(GameObject Object)
		{
			foreach (GameObjectBlueprint gameObjectBlueprint in GolemArmamentSelection.ZetachromeWeapons)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint))
				{
					GameObject gameObject = GameObjectFactory.Factory.CreateUnmodifiedObject(gameObjectBlueprint, null, null);
					Object.ReceiveObject(gameObject, false, null);
				}
			}
		}

		// Token: 0x0400352B RID: 13611
		public const string BASE_ID = "Armament";

		// Token: 0x0400352C RID: 13612
		[NonSerialized]
		private static List<string> _Skills;
	}
}
