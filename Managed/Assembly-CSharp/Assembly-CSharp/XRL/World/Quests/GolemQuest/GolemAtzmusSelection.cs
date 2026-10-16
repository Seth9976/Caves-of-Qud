using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XRL.Rules;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2B RID: 2859
	[HasWishCommand]
	[Serializable]
	public class GolemAtzmusSelection : GolemGameObjectSelection
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060072D5 RID: 29397 RVA: 0x002B6F98 File Offset: 0x002B5198
		public override string ID
		{
			get
			{
				return "Atzmus";
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060072D6 RID: 29398 RVA: 0x002B6F9F File Offset: 0x002B519F
		public override string DisplayName
		{
			get
			{
				return "atzmus";
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x002B6FA6 File Offset: 0x002B51A6
		public override char Key
		{
			get
			{
				return 'a';
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060072D8 RID: 29400 RVA: 0x002B6FAA File Offset: 0x002B51AA
		public override bool IsCarried
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060072D9 RID: 29401 RVA: 0x002B6FAD File Offset: 0x002B51AD
		public override int Consumed
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x002B6FB0 File Offset: 0x002B51B0
		public override bool IsValid(GameObject Object)
		{
			return base.IsValid(Object) && (Object.HasStringProperty("LimbSourceGameObjectBlueprint") || Object.HasPart(typeof(DismemberedProperties)));
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x002B6FDC File Offset: 0x002B51DC
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			GameObject sourceOf = this.GetSourceOf(this.Material);
			if (GameObject.Validate(sourceOf))
			{
				Object.SetStringProperty("AtzmusSourceBlueprint", sourceOf.Blueprint, false);
				Object.SetStringProperty("AtzmusSourceDisplayName", sourceOf.Render.DisplayName, false);
				if (!sourceOf.HasProperName)
				{
					Object.SetStringProperty("AtzmusSourceIndefiniteArticle", sourceOf.a, false);
				}
			}
			Mutations mutations = Object.RequirePart<Mutations>(false);
			foreach (string text in GolemAtzmusSelection.Blocklist)
			{
				if (text != "DarkVision")
				{
					BaseMutation baseMutation = Object.GetPart(text) as BaseMutation;
					if (baseMutation != null)
					{
						mutations.RemoveMutation(baseMutation, true);
					}
				}
			}
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x002B7094 File Offset: 0x002B5294
		private GameObject GetSourceOf(GameObject Object)
		{
			if (!GameObject.Validate(Object))
			{
				return null;
			}
			DismemberedProperties part = Object.GetPart<DismemberedProperties>();
			string text = part.SourceID ?? Object.GetStringProperty("LimbSourceGameObjectID", null);
			if (!text.IsNullOrEmpty())
			{
				GameObject gameObject = The.ZoneManager.FindObjectByID(text);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			string text2;
			if ((text2 = part.SourceBlueprint) == null && (text2 = Object.GetStringProperty("LimbSourceGameObjectBlueprint", null)) == null)
			{
				RandomFigurine part2 = Object.GetPart<RandomFigurine>();
				text2 = ((part2 != null) ? part2.Creature : null);
			}
			string text3 = text2;
			GameObjectBlueprint gameObjectBlueprint;
			if (!text3.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(text3, out gameObjectBlueprint))
			{
				Stat.PushState(text.Coalesce(Object.ID), false);
				try
				{
					return GameObjectFactory.Factory.CreateSampleObject(gameObjectBlueprint, null);
				}
				finally
				{
					Stat.PopState();
				}
				return Object;
			}
			return Object;
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x002B716C File Offset: 0x002B536C
		public override IEnumerable<GameObjectUnit> YieldAllEffects()
		{
			foreach (MutationEntry mutationEntry in MutationFactory.AllMutationEntries())
			{
				if (!mutationEntry.IsDefect() && !GolemAtzmusSelection.Blocklist.Contains(mutationEntry.Class))
				{
					yield return new GameObjectMutationUnit
					{
						Name = mutationEntry.Name,
						Level = 5
					};
				}
			}
			IEnumerator<MutationEntry> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x002B7175 File Offset: 0x002B5375
		public override IEnumerable<GameObjectUnit> YieldRandomEffects()
		{
			List<MutationEntry> list = (from x in MutationFactory.AllMutationEntries()
				where !x.IsDefect()
				select x).ToList<MutationEntry>();
			yield return new GameObjectMutationUnit
			{
				Name = list.GetRandomElement<MutationEntry>(null).Name,
				Level = 5
			};
			yield break;
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x002B717E File Offset: 0x002B537E
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(GameObject Object)
		{
			GameObject obj = this.GetSourceOf(Object);
			if (obj == null)
			{
				yield break;
			}
			Mutations part = obj.GetPart<Mutations>();
			List<BaseMutation> list = ((part != null) ? part.MutationList : null);
			if (!list.IsNullOrEmpty<BaseMutation>())
			{
				bool yielded = false;
				foreach (BaseMutation baseMutation in list)
				{
					if (!baseMutation.IsDefect() && !GolemAtzmusSelection.Blocklist.Contains(baseMutation.Name))
					{
						yielded = true;
						yield return new GameObjectMutationUnit
						{
							Name = baseMutation.GetDisplayName(true),
							Class = baseMutation.Name,
							Level = Mathf.Max(baseMutation.BaseLevel, 1),
							ShouldShowLevel = baseMutation.ShouldShowLevel()
						};
					}
				}
				List<BaseMutation>.Enumerator enumerator = default(List<BaseMutation>.Enumerator);
				if (yielded)
				{
					yield break;
				}
			}
			List<string> list2 = new List<string> { "Strength" };
			int num = -1;
			foreach (string text in Statistic.Attributes)
			{
				int statValue = obj.GetStatValue(text, 0);
				if (statValue > num)
				{
					list2.Clear();
					list2.Add(text);
					num = statValue;
				}
				else if (statValue == num)
				{
					list2.Add(text);
				}
			}
			yield return new GameObjectAttributeUnit
			{
				Attribute = ((list2.Count == 1) ? list2[0] : list2.GetRandomElement<string>(Stat.GetSeededRandomGenerator(Object.ID))),
				Value = 5
			};
			yield break;
			yield break;
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x002B7195 File Offset: 0x002B5395
		[WishCommand("golemquest:atzmus", null)]
		private static void Wish()
		{
			GolemAtzmusSelection.ReceiveMaterials(The.Player);
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x002B71A4 File Offset: 0x002B53A4
		public static void ReceiveMaterials(GameObject Object)
		{
			GameObjectFactory factory = GameObjectFactory.Factory;
			Dictionary<string, ValueTuple<GameObjectBlueprint, int>> dictionary = new Dictionary<string, ValueTuple<GameObjectBlueprint, int>>();
			foreach (GameObjectBlueprint gameObjectBlueprint in factory.BlueprintList)
			{
				foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair in gameObjectBlueprint.Mutations)
				{
					if (gameObjectBlueprint.IsBaseBlueprint() || gameObjectBlueprint.HasTag("Golem", false))
					{
						break;
					}
					if (gameObjectBlueprint.DescendsFrom("BaseNephal"))
					{
						break;
					}
					if (Array.IndexOf<string>(GolemAtzmusSelection.Blocklist, keyValuePair.Key) < 0)
					{
						int num;
						if (!keyValuePair.Value.TryGetParameter<int>("Level", out num))
						{
							num = 1;
						}
						ValueTuple<GameObjectBlueprint, int> valueTuple;
						if (!dictionary.TryGetValue(keyValuePair.Key, out valueTuple) || num > valueTuple.Item2 || (num == valueTuple.Item2 && gameObjectBlueprint.Mutations.Count < valueTuple.Item1.Mutations.Count))
						{
							dictionary[keyValuePair.Key] = new ValueTuple<GameObjectBlueprint, int>(gameObjectBlueprint, num);
						}
					}
				}
			}
			GameObject gameObject = null;
			foreach (KeyValuePair<string, ValueTuple<GameObjectBlueprint, int>> keyValuePair2 in dictionary)
			{
				try
				{
					gameObject = factory.CreateObject(keyValuePair2.Value.Item1, 0, 0, null, null, null, null, null);
					BodyPart dismemberableBodyPart = Axe_Dismember.GetDismemberableBodyPart(gameObject, null, null, false, false);
					if (dismemberableBodyPart != null)
					{
						GameObject gameObject2 = gameObject.Body.Dismember(dismemberableBodyPart, null, null, false, false, null);
						if (gameObject2 != null)
						{
							Object.ReceiveObject(gameObject2, false, null);
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("golemquest:atzmus", ex, "game_exception");
				}
				finally
				{
					if (gameObject != null)
					{
						gameObject.Pool();
					}
				}
			}
		}

		// Token: 0x0400352D RID: 13613
		public const string BASE_ID = "Atzmus";

		// Token: 0x0400352E RID: 13614
		public static readonly string[] Blocklist = new string[] { "NightVision", "DarkVision", "Invisibility", "OldElectricalGeneration", "WallWalker", "Metamorphosis" };
	}
}
