using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.AI.Pathfinding;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011B1 RID: 4529
	[Serializable]
	public class PetEitherOr : IPart
	{
		// Token: 0x0600B3DC RID: 46044 RVA: 0x004197EF File Offset: 0x004179EF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600B3DD RID: 46045 RVA: 0x00419810 File Offset: 0x00417A10
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Cooldown > 0)
			{
				this.Cooldown--;
				if (this.Cooldown <= 0)
				{
					int num = 0;
					while (this.ParentObject.HasPart<AnimatedMaterialGeneric>() && ++num <= 100)
					{
						this.ParentObject.RemovePart<AnimatedMaterialGeneric>();
					}
					this.explode();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3DE RID: 46046 RVA: 0x00419870 File Offset: 0x00417A70
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			if (IComponent<GameObject>.ThePlayer != null)
			{
				if (!IComponent<GameObject>.ThePlayer.HasPart<PetEitherOrRespawner>())
				{
					IComponent<GameObject>.ThePlayer.AddPart<PetEitherOrRespawner>(new PetEitherOrRespawner(), true, false);
				}
				PetEitherOrRespawner part = IComponent<GameObject>.ThePlayer.GetPart<PetEitherOrRespawner>();
				if (this.Either)
				{
					part.respawnEither = true;
				}
				if (!this.Either)
				{
					part.respawnOr = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3DF RID: 46047 RVA: 0x004198D4 File Offset: 0x00417AD4
		public override bool HandleEvent(AfterLevelGainedEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Leveler leveler;
				if (this.ParentObject.TryGetPart<Leveler>(out leveler))
				{
					leveler.LevelUp(null, null, null, E);
				}
				this.trigger();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3E0 RID: 46048 RVA: 0x00419915 File Offset: 0x00417B15
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, PooledEvent<AfterLevelGainedEvent>.ID, 0, false);
			Registrar.Register("AccomplishmentAdded");
		}

		// Token: 0x0600B3E1 RID: 46049 RVA: 0x00419934 File Offset: 0x00417B34
		public bool testChirality()
		{
			return this.triggerChirality == this.Either;
		}

		// Token: 0x0600B3E2 RID: 46050 RVA: 0x00419944 File Offset: 0x00417B44
		public void trigger()
		{
			IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " starts to flicker.", null, true);
			if (!this.ParentObject.HasPart<AnimatedMaterialGeneric>())
			{
				this.ParentObject.AddPart<AnimatedMaterialGeneric>(new AnimatedMaterialGeneric(), true, false);
				AnimatedMaterialGeneric part = this.ParentObject.GetPart<AnimatedMaterialGeneric>();
				part.AnimationLength = 20;
				part.ColorStringAnimationFrames = "0=" + this.ParentObject.Render.ColorString + ",10=&" + this.ParentObject.Render.DetailColor;
				part.DetailColorAnimationFrames = "0=" + this.ParentObject.Render.DetailColor + ",10=" + this.ParentObject.Render.ColorString.Substring(1);
			}
			this.Cooldown = Stat.SeededRandom(XRLCore.Core.Game.Turns.ToString(), 8, 20);
			this.type = Stat.SeededRandom(XRLCore.Core.Game.Turns.ToString(), 1, 11);
			this.ParentObject.Brain.PushGoal(new WanderDuration((long)this.Cooldown));
			this.triggerChirality = XRLCore.Core.Game.Turns % 2L == 0L;
			if (this.ParentObject.Physics.CurrentCell.ParentZone.CountObjects((GameObject o) => o.HasTag("EitherOrPet")) <= 1)
			{
				this.triggerChirality = this.Either;
			}
		}

		// Token: 0x0600B3E3 RID: 46051 RVA: 0x00419AD7 File Offset: 0x00417CD7
		public void boom()
		{
			this.ParentObject.DustPuff("&y", 15);
			this.ParentObject.PsychicPulse();
			this.ParentObject.Destroy(null, false, false, null);
		}

		// Token: 0x0600B3E4 RID: 46052 RVA: 0x00419B08 File Offset: 0x00417D08
		public void wall(string blueprint)
		{
			List<GameObject> objectsWithTag = this.ParentObject.Physics.CurrentCell.ParentZone.GetObjectsWithTag("EitherOrPet");
			if (objectsWithTag.Count < 2)
			{
				return;
			}
			FindPath findPath = new FindPath(objectsWithTag[0].Physics.CurrentCell.ParentZone, objectsWithTag[0].Physics.CurrentCell.X, objectsWithTag[0].Physics.CurrentCell.Y, objectsWithTag[1].Physics.CurrentCell.ParentZone, objectsWithTag[1].Physics.CurrentCell.X, objectsWithTag[1].Physics.CurrentCell.Y, true, true, GameObject.Create("Drillbot"), false, true, false, false, false, false);
			if (findPath.Usable)
			{
				for (int i = 0; i < findPath.Steps.Count; i++)
				{
					GameObject gameObject;
					if (blueprint[0] == '*')
					{
						gameObject = findPath.Steps[i].AddObject(PopulationManager.RollOneFrom(blueprint.Substring(1), null, null).Blueprint, null, null, null, null);
					}
					else
					{
						gameObject = findPath.Steps[i].AddObject(blueprint, null, null, null, null);
					}
					if (gameObject != null && gameObject.IsCombat)
					{
						gameObject.SetActive();
					}
				}
			}
		}

		// Token: 0x0600B3E5 RID: 46053 RVA: 0x00419C58 File Offset: 0x00417E58
		public void explode()
		{
			this.ParentObject.Brain.Goals.Clear();
			if (this.ParentObject.Physics.CurrentCell != null)
			{
				if (this.type == 1 && this.testChirality())
				{
					this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocation(true).AddObject("Space-Time Vortex", null, null, null, null);
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " implodes.", null, true);
					this.boom();
				}
				if (this.type == 2 && this.testChirality())
				{
					this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocation(true).AddObject(PopulationManager.RollOneFrom("Village_RandomBaseStatue_*Default", null, null).Blueprint, null, null, null, null);
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is smeared into stone by the rasp of time.", null, true);
					this.boom();
				}
				if (this.type == 3 && this.testChirality())
				{
					int i = 0;
					int num = Stat.Random(4, 6);
					while (i < num)
					{
						GameObject gameObject = this.ParentObject.CurrentCell.GetConnectedSpawnLocation(true).AddObject("ClockworkBeetle", null, null, null, null);
						gameObject.Brain.AddOpinion<OpinionSummon>(The.Player, 1f);
						gameObject.MakeActive(false);
						i++;
					}
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("crumble", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " into beetles.", null, true);
					this.boom();
				}
				if (this.type == 4 && this.testChirality())
				{
					int num2 = 0;
					foreach (Cell cell in this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocations(3))
					{
						GameObject anObject = EncountersAPI.GetAnObject(null);
						cell.AddObject(anObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (anObject.IsCombat)
						{
							anObject.SetActive();
						}
						num2++;
						if (num2 >= 3)
						{
							break;
						}
					}
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is vacuumed to another place and time. The void that remains is filled with three important objects from one of your side lives.", null, true);
					this.boom();
				}
				if (this.type == 5 && this.testChirality())
				{
					Cell connectedSpawnLocation = this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocation(true);
					if (connectedSpawnLocation != null)
					{
						int zoneTier = this.ParentObject.Physics.CurrentCell.ParentZone.NewTier;
						GameObject acreature = EncountersAPI.GetACreature((GameObjectBlueprint o) => o.Tier == zoneTier || o.Tier == zoneTier + 1);
						connectedSpawnLocation.AddObject(acreature, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						acreature.SetActive();
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.Does("atomize", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" and",
							this.ParentObject.GetVerb("recombine", true, false, false, true, false),
							" into ",
							acreature.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"."
						}), null, true);
					}
					this.boom();
				}
				if (this.type == 6 && this.testChirality())
				{
					List<string> list = new List<string>();
					foreach (Cell cell2 in this.ParentObject.Physics.CurrentCell.GetAdjacentCells(3, true, true))
					{
						foreach (GameObject gameObject2 in cell2.Objects)
						{
							if (AnimateObject.CanAnimate(gameObject2) && 50.in100())
							{
								AnimateObject.Animate(gameObject2, null, null);
								gameObject2.SetActive();
								list.Add(gameObject2.DisplayNameOnly);
							}
						}
					}
					if (list.Count == 0)
					{
						IComponent<GameObject>.AddPlayerMessage(Grammar.MakePossessive(this.ParentObject.DisplayNameOnly) + " consciousness dissipates.", null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(Grammar.MakePossessive(this.ParentObject.DisplayNameOnly) + " consciousness dissipates into " + Grammar.MakeAndList(list, true) + ".", null, true);
					}
					this.boom();
				}
				if (this.type == 7 && this.testChirality())
				{
					List<string> list2 = new List<string>();
					for (int j = 0; j < 3; j++)
					{
						list2.Add(PopulationManager.RollOneFrom("RandomLiquid", null, null).Blueprint);
					}
					foreach (Cell cell3 in this.ParentObject.Physics.CurrentCell.GetAdjacentCells(2, true, true))
					{
						cell3.AddObject(LiquidVolume.create(list2, 1000), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					GameObject gameObject3 = LiquidVolume.create(list2, 1000);
					string text = (gameObject3.DisplayName.Contains("pool of ") ? gameObject3.DisplayName.Substring(gameObject3.DisplayName.IndexOf("of", gameObject3.DisplayName.IndexOf("of") + 1) + 2) : "liquid");
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " liquifies into several pools of" + text + "&y.", null, true);
					this.boom();
				}
				if (this.type == 8)
				{
					if (this.testChirality())
					{
						foreach (GameObject gameObject4 in this.ParentObject.Physics.CurrentCell.ParentZone.GetObjectsWithPart("Combat"))
						{
							gameObject4.ApplyEffect(new BlinkingTicSickness(1200), null);
						}
					}
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is folded a trillion times by the pressure of the nether, causing the local region of spacetime to lose contiguity.", null, true);
					this.boom();
				}
				if (this.type == 9)
				{
					this.wall("Forcefield");
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is vectorized into a line of force.", null, true);
					this.boom();
				}
				if (this.type == 10)
				{
					this.wall("RealityStabilizationField");
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is vectorized into a line of normality.", null, true);
					this.boom();
				}
				if (this.type == 11)
				{
					this.wall("*PlantSummoning" + this.ParentObject.Physics.CurrentCell.ParentZone.NewTier.ToString());
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayNameOnly + " is vectorized into a line of plants.", null, true);
					this.boom();
				}
			}
		}

		// Token: 0x0600B3E6 RID: 46054 RVA: 0x0041A3C4 File Offset: 0x004185C4
		public static bool ContainsAccomplishmentExclusion(string accomplishment)
		{
			return accomplishment.StartsWith("You read ") || accomplishment.Contains("arrived at Joppa") || accomplishment.Contains(", you arrived at ") || accomplishment.StartsWith("Sheba Hagadias");
		}

		// Token: 0x0600B3E7 RID: 46055 RVA: 0x0041A3FC File Offset: 0x004185FC
		public static string GetEitherOrAccomplishment()
		{
			JournalAccomplishment randomElement = (from c in JournalAPI.Accomplishments
				select (c) into c
				where !PetEitherOr.ContainsAccomplishmentExclusion(c.Text)
				select c).GetRandomElement<JournalAccomplishment>(null);
			if (randomElement == null)
			{
				return Grammar.Weirdify("You've done nothing.", 100);
			}
			return ColorUtility.StripFormatting(Grammar.Weirdify(randomElement.Text, 75));
		}

		// Token: 0x0600B3E8 RID: 46056 RVA: 0x0041A480 File Offset: 0x00418680
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AccomplishmentAdded")
			{
				string stringParameter = E.GetStringParameter("Text", null);
				if (!string.IsNullOrEmpty(stringParameter) && !PetEitherOr.ContainsAccomplishmentExclusion(stringParameter))
				{
					this.trigger();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B3E9 RID: 46057 RVA: 0x0041A4CC File Offset: 0x004186CC
		public override bool Render(RenderEvent E)
		{
			if (IComponent<GameObject>.ThePlayer != null && IComponent<GameObject>.ThePlayer.Render != null)
			{
				this.ParentObject.Render.Tile = IComponent<GameObject>.ThePlayer.Render.Tile;
				E.RenderString = IComponent<GameObject>.ThePlayer.Render.RenderString;
				E.Tile = IComponent<GameObject>.ThePlayer.Render.Tile;
			}
			if (this.Cooldown <= 0)
			{
				if (this.ParentObject.Physics != null && this.ParentObject.Physics.CurrentCell != null && this.ParentObject.Physics.CurrentCell.CountObjectsWithTag("EitherOrPet") > 1)
				{
					E.ColorString = "&Y";
					E.DetailColor = "y";
				}
				else if (this.Either)
				{
					E.ColorString = "&c";
					E.DetailColor = "C";
				}
				else
				{
					E.ColorString = "&r";
					E.DetailColor = "R";
				}
			}
			return true;
		}

		// Token: 0x0400488A RID: 18570
		public bool triggerChirality;

		// Token: 0x0400488B RID: 18571
		public bool Either;

		// Token: 0x0400488C RID: 18572
		public int Cooldown;

		// Token: 0x0400488D RID: 18573
		private int type;
	}
}
