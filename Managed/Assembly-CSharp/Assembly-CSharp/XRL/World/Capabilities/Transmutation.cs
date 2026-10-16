using System;
using System.Collections.Generic;
using Qud.API;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World.Capabilities
{
	// Token: 0x020009FA RID: 2554
	[HasWishCommand]
	public static class Transmutation
	{
		/// <summary>
		/// Transmute an object into another.
		/// </summary>
		/// <param name="Object">The object to transmute.</param>
		/// <param name="Actor">
		/// The actor responsible for the transmutation.
		/// If the <see cref="!:Object" /> dies as part of the transmutation process, this will be its killer.
		/// </param>
		/// <param name="Weapon">The weapon used by the <see cref="!:Actor" />.</param>
		/// <param name="Projectile">The projectile fired by the <see cref="!:Weapon" />.</param>
		/// <param name="Blueprint">The blueprint to transmute into.</param>
		/// <param name="Message">
		/// A message displayed in the log or death reason.
		/// Parsed using <see cref="M:XRL.GameText.VariableReplace(System.String,System.String,System.Boolean,System.String,System.Boolean,System.Boolean)" /> with the old and new objects as subject and object respectively.
		/// </param>
		/// <param name="Context">The context to send to methods and events that receive one.</param>
		/// <param name="Animate">If a sentient <see cref="!:Object" /> is transmuting into a non-sentient one, animate it. If false, it will instead die.</param>
		/// <param name="MakePermanent">If a temporary or existence-supported object is being transmuted, make the result permanent.</param>
		/// <returns><c>true</c> if the <see cref="!:Object" /> was successfully transmuted; otherwise, <c>false</c>.</returns>
		// Token: 0x06006CDF RID: 27871 RVA: 0x00236E54 File Offset: 0x00235054
		public static bool TransmuteObject(GameObject Object, GameObject Actor = null, GameObject Weapon = null, GameObject Projectile = null, string Blueprint = null, string Message = null, string Context = "Transmute", string Sound = "sfx_statusEffect_spacetimeWeirdness", bool Animate = false, bool MakePermanent = false)
		{
			if (!Object.IsReal || Object.IsNowhere())
			{
				return false;
			}
			if (Object.HasTagOrProperty("SuppressTransmutation"))
			{
				return false;
			}
			if (Blueprint.IsNullOrEmpty())
			{
				Blueprint = EncountersAPI.GetATransmutationBlueprintFor(Object);
			}
			if (Blueprint.IsNullOrEmpty() || Blueprint == Object.Blueprint)
			{
				return false;
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (!GameObjectFactory.Factory.Blueprints.TryGetValue(Blueprint, out gameObjectBlueprint))
			{
				MetricsManager.LogError("No blueprint by name '" + Blueprint + "' found to transmute into.");
				return false;
			}
			bool flag = Object.IsPlayer();
			if (flag && gameObjectBlueprint.DescendsFrom("Gemstone"))
			{
				Achievement.TRANSMUTED_GEM.Unlock();
			}
			GameObject gameObject = GameObject.Create(gameObjectBlueprint, 0, 0, null, null, null, Context, null);
			Cell currentCell = Object.CurrentCell;
			currentCell.PlayWorldSound(Sound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			bool flag2;
			using (new IntPropertyMod(gameObject, "SuppressTransmutation", 1))
			{
				Object.SplitFromStack();
				if (gameObject.HasTag("Item") && Object.IsGiganticCreature && ItemModding.ModificationApplicable("ModGigantic", gameObject, null, null))
				{
					gameObject.ApplyModification("ModGigantic", true, null, false);
				}
				string text = (Message.IsNullOrEmpty() ? null : GameText.VariableReplace(Message, Object, gameObject, false));
				if (Object.Brain != null && gameObject.Brain == null)
				{
					if (Animate)
					{
						AnimateObject.Animate(gameObject, null, null);
					}
					else if (!Object.Die(Actor, null, text, null, false, Weapon, Projectile, false, false, null, null, null))
					{
						return false;
					}
				}
				if (!text.IsNullOrEmpty())
				{
					Object.Physics.EmitMessage(text, 'W', false, false, false, null, null);
				}
				if (flag)
				{
					gameObject.RemovePart(typeof(GivesRep));
					gameObject.RequirePart<Inventory>(false);
					if (gameObject.Body == null)
					{
						gameObject.AddPart<Body>(new Body(), true, false).Anatomy = gameObject.GetTag("BodyType", "Humanoid");
					}
					Brain brain = gameObject.RequirePart<Brain>(false);
					brain.Allegiance.Clear();
					brain.Allegiance["Player"] = 100;
					brain.FactionFeelings.Clear();
					if (!gameObject.HasStat("Energy"))
					{
						try
						{
							Statistic statistic = new Statistic(GameObjectFactory.Factory.Blueprints["Creature"].Stats["Energy"]);
							statistic.Owner = gameObject;
							gameObject.Statistics["Energy"] = statistic;
						}
						catch
						{
						}
					}
				}
				gameObject.StripContents(true, true);
				Transmutation.TransmuteBody(Object, gameObject);
				Transmutation.TransmuteInventory(Object, gameObject);
				Transmutation.TransmuteBrain(Object, gameObject);
				Transmutation.TransmuteName(Object, gameObject);
				if (flag)
				{
					gameObject.SystemMoveTo(currentCell, null, false, true, false, false);
					gameObject.MakeActive(false);
					if (flag)
					{
						The.Game.Player.Body = gameObject;
					}
				}
				Object.ReplaceWith(gameObject);
				Object.RemoveFromContext(null);
				if (!flag)
				{
					gameObject.SystemMoveTo(currentCell, null, false, true, false, false);
					gameObject.MakeActive(false);
				}
				if (MakePermanent)
				{
					gameObject.RemovePart(typeof(global::Temporary));
					gameObject.RemovePart(typeof(ExistenceSupport));
				}
				if (currentCell.IsVisible() && Options.UseParticleVFX && !Object.Render.Tile.IsNullOrEmpty() && !gameObject.Render.Tile.IsNullOrEmpty())
				{
					ParticleVFXTransmuted.Play(Object, gameObject);
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x002371F8 File Offset: 0x002353F8
		public static void TransmuteBody(GameObject Object, GameObject Target)
		{
			if (Object.Body == null)
			{
				return;
			}
			foreach (BodyPart bodyPart in Object.Body.LoopParts())
			{
				GameObject equipped = bodyPart.Equipped;
				if (equipped != null && !equipped.IsNatural() && bodyPart.TryUnequip(true, true, false, false))
				{
					Target.ReceiveObject(equipped, true, null);
					if (Target.Body != null)
					{
						BodyPart firstPart = Target.Body.GetFirstPart(bodyPart.Type, bodyPart.Laterality, (BodyPart x) => x.Equipped == null);
						if (firstPart != null)
						{
							Target.EquipObject(equipped, firstPart, true, new int?(0));
							continue;
						}
					}
					Transmutation.TransferObject(equipped, Object, Target);
				}
			}
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x002372D8 File Offset: 0x002354D8
		private static void TransferObject(GameObject Item, GameObject Object, GameObject Target)
		{
			if (Target.Inventory != null && Target.ReceiveObject(Item, false, null))
			{
				return;
			}
			string text = "CommandDropObject";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			bool flag5 = true;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			GameObject gameObject = null;
			Cell cell = null;
			Cell cell2 = null;
			IInventory inventory;
			if ((inventory = Object.GetDropInventory()) == null)
			{
				inventory = Target.GetDropInventory() ?? Item.GetDropInventory();
			}
			InventoryActionEvent.Check(Object, Object, Item, text, flag, flag2, flag3, flag4, flag5, num, num2, num3, gameObject, cell, cell2, inventory);
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x0023732C File Offset: 0x0023552C
		public static void TransmuteInventory(GameObject Object, GameObject Target)
		{
			if (Object.Inventory == null)
			{
				return;
			}
			foreach (GameObject gameObject in new List<GameObject>(Object.Inventory.Objects))
			{
				Transmutation.TransferObject(gameObject, Object, Target);
			}
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x00237394 File Offset: 0x00235594
		public static void TransmuteBrain(GameObject Object, GameObject Target)
		{
			if (Object.Brain == null || Target.Brain == null)
			{
				return;
			}
			Target.PartyLeader = Object.PartyLeader;
			if (!Object.Brain.PartyMembers.IsNullOrEmpty<KeyValuePair<int, PartyMember>>() && Target.Brain != null)
			{
				foreach (KeyValuePair<int, PartyMember> keyValuePair in Object.Brain.PartyMembers)
				{
					Target.Brain.PartyMembers[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			AllegianceSet allegiance = Object.Brain.Allegiance;
			if (!allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				Object.Brain.Allegiance = Target.Brain.Allegiance;
				Target.Brain.Allegiance = allegiance;
			}
			foreach (GameObject gameObject in Object.CurrentZone.YieldObjects())
			{
				if (gameObject.PartyLeader == Object)
				{
					Proselytized proselytized;
					if (gameObject.TryGetEffect<Proselytized>(out proselytized))
					{
						if (!Target.HasPart<Persuasion_Proselytize>())
						{
							continue;
						}
						proselytized.Proselytizer = Target;
					}
					Beguiled beguiled;
					if (gameObject.TryGetEffect<Beguiled>(out beguiled))
					{
						if (!Target.HasPart<Beguiling>())
						{
							continue;
						}
						beguiled.Beguiler = Target;
					}
					gameObject.PartyLeader = Target;
					gameObject.Brain.Goals.Clear();
				}
			}
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x00237504 File Offset: 0x00235704
		public static void TransmuteName(GameObject Object, GameObject Target)
		{
			if (!Object.HasProperName || !Object.IsCombatObject())
			{
				return;
			}
			string text = Object.Render.DisplayName;
			GameObjectBlueprint blueprint = Object.GetBlueprint(true);
			string text2 = (blueprint.HasProperName() ? Object.GetSpecies() : blueprint.CachedDisplayNameStripped);
			GameObjectBlueprint blueprint2 = Target.GetBlueprint(true);
			string text3 = (blueprint2.HasProperName() ? Target.GetSpecies() : blueprint2.CachedDisplayNameStripped);
			text = text.Replace(text2, text3);
			Target.Render.DisplayName = text;
			Target.HasProperName = true;
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x0023758A File Offset: 0x0023578A
		[WishCommand("transmute", null)]
		public static void WishTransmute()
		{
			Transmutation.WishTransmute(null);
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x00237594 File Offset: 0x00235794
		[WishCommand("transmute", null)]
		public static void WishTransmute(string Blueprint)
		{
			Transmutation.TransmuteObject(The.Player, null, null, null, Blueprint, "=subject.T= =verb:were= transmuted into =object.an=.", "Transmute", "sfx_statusEffect_spacetimeWeirdness", false, false);
		}

		// Token: 0x040031DC RID: 12764
		public const string TAG_SUPPRESS = "SuppressTransmutation";
	}
}
