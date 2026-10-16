using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D0 RID: 5072
	[Serializable]
	public class EvilTwin : BaseMutation
	{
		// Token: 0x0600C3F6 RID: 50166 RVA: 0x00476E36 File Offset: 0x00475036
		public EvilTwin()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C3F7 RID: 50167 RVA: 0x00476E54 File Offset: 0x00475054
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3F8 RID: 50168 RVA: 0x00476E57 File Offset: 0x00475057
		public override string GetDescription()
		{
			return "Acting on some inscrutable impulse, a parallel version of yourself travels through space and time to destroy you.\n\nEach time you embark on a new location, there's a small chance your evil twin has tracked you there and attempts to kill you.";
		}

		// Token: 0x0600C3F9 RID: 50169 RVA: 0x00476E5E File Offset: 0x0047505E
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3FA RID: 50170 RVA: 0x00476E68 File Offset: 0x00475068
		private static void ApplyHostility(GameObject Actor, Brain Brain, int Depth)
		{
			if (Actor == null)
			{
				return;
			}
			if (Depth >= 100)
			{
				return;
			}
			Brain.AddOpinion<OpinionInscrutable>(Actor, 1f);
			EvilTwin.ApplyHostility(Actor.PartyLeader, Brain, Depth + 1);
			Dominated dominated;
			if (Actor.TryGetEffect<Dominated>(out dominated))
			{
				EvilTwin.ApplyHostility(dominated.Dominator, Brain, Depth + 1);
			}
		}

		// Token: 0x0600C3FB RID: 50171 RVA: 0x00476EB4 File Offset: 0x004750B4
		public static bool CreateEvilTwin(GameObject Original, string Prefix, Cell TargetCell = null, string Message = null, string ColorString = "&K", GameObject Actor = null, string MessageForActor = null, bool MakeExtras = true, string Description = null, string Factions = null)
		{
			if (Actor == null)
			{
				Actor = Original;
			}
			string text = "Twin";
			if (!Prefix.IsNullOrEmpty())
			{
				text = Prefix + (Prefix.EndsWith("-") ? "" : " ") + text;
			}
			if (!Original.CanBeReplicated(Actor, text, true))
			{
				return false;
			}
			if (Factions == null)
			{
				Factions = (Original.IsPlayer() ? "Entropic-100,Mean-100,Playerhater-99" : "Entropic-100,Mean-100");
			}
			GameObject gameObject = Original.DeepCopy(false, false, null);
			if (gameObject.HasPart<EvilTwin>())
			{
				Mutations part = gameObject.GetPart<Mutations>();
				part.RemoveMutation(part.GetMutation("EvilTwin"), true);
				gameObject.RemovePart<EvilTwin>();
			}
			if (Original.IsPlayer())
			{
				gameObject.SetStringProperty("PlayerCopy", "true", false);
			}
			gameObject.SetStringProperty("EvilTwin", "true", false);
			gameObject.SetIntProperty("Entropic", 1, false);
			Brain brain = gameObject.Brain;
			brain.PartyLeader = null;
			brain.Hibernating = false;
			brain.Staying = false;
			brain.Passive = false;
			brain.Factions = Factions;
			brain.Allegiance.Hostile = true;
			brain.Allegiance.Calm = false;
			if (Actor != null)
			{
				brain.AddOpinion<OpinionMollify>(Actor, 1f);
				Actor.AddOpinion<OpinionMollify>(gameObject, 1f);
			}
			if (!Prefix.IsNullOrEmpty())
			{
				gameObject.DisplayName = Prefix + (Prefix.EndsWith("-") ? "" : " ") + gameObject.DisplayNameOnlyDirect;
			}
			Description description;
			if (!Description.IsNullOrEmpty() && gameObject.TryGetPart<Description>(out description) && description._Short == "It's you.")
			{
				description.Short = Description;
			}
			Event @event = Event.New("EvilTwinAttitudeSetup", 0, 0, 0);
			@event.SetParameter("Original", Original);
			@event.SetParameter("Twin", gameObject);
			@event.SetParameter("Actor", Actor);
			if (Original.FireEvent(@event))
			{
				brain.PushGoal(new Kill(Original, false));
				EvilTwin.ApplyHostility(Original, brain, 0);
			}
			Zone currentZone = Original.CurrentZone;
			gameObject.Render.ColorString = ColorString;
			Temporary.AddHierarchically(gameObject, -1, null, null, false);
			Brain brain2 = gameObject.Brain;
			if (brain2 != null)
			{
				brain2.PerformEquip(false, true, null);
			}
			if (TargetCell == null)
			{
				int i = 1000;
				while (i > 0)
				{
					i--;
					int num = Stat.Random(0, currentZone.Width - 1);
					int num2 = Stat.Random(0, currentZone.Height - 1);
					Cell cell = currentZone.GetCell(num, num2);
					if (cell.IsEmpty())
					{
						TargetCell = cell;
						break;
					}
				}
			}
			if (TargetCell == null)
			{
				gameObject.Obliterate(null, false, null);
				return false;
			}
			if (Original.IsPlayer())
			{
				IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_mutation_evilTwin_spawn", 1f, false, SoundRequest.SoundEffectType.None);
			}
			if (gameObject != null)
			{
				gameObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_evilTwin_spawn", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			TargetCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject.MakeActive(false);
			if (MakeExtras)
			{
				if (Actor.GetIntProperty("MentalCloneMultiplier", 0) > 0)
				{
					int num3 = 0;
					for (;;)
					{
						int num4 = num3;
						int? num5 = ((Actor != null) ? new int?(Actor.GetIntProperty("MentalCloneMultiplier", 0) - 1) : null);
						if (!((num4 < num5.GetValueOrDefault()) & (num5 != null)))
						{
							break;
						}
						EvilTwin.CreateEvilTwin(Original, Prefix, TargetCell, null, ColorString, Actor, null, false, Description, null);
						num3++;
					}
				}
				if (Original.IsPlayer())
				{
					IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_mutation_evilTwin_spawn", 1f, false, SoundRequest.SoundEffectType.None);
				}
				if (gameObject != null)
				{
					gameObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_evilTwin_spawn", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				WasReplicatedEvent.Send(Original, Actor, gameObject, text, true);
				ReplicaCreatedEvent.Send(gameObject, Actor, Original, text, true);
				if (!Message.IsNullOrEmpty() && Original.IsPlayer())
				{
					Popup.Show(Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (!MessageForActor.IsNullOrEmpty() && Actor.IsPlayer())
				{
					Popup.Show(MessageForActor, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return true;
		}

		// Token: 0x0600C3FC RID: 50172 RVA: 0x004772B3 File Offset: 0x004754B3
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C3FD RID: 50173 RVA: 0x004772D4 File Offset: 0x004754D4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!E.Object.OnWorldMap())
			{
				Zone currentZone = E.Object.CurrentZone;
				string text = ((currentZone != null) ? currentZone.ZoneID : null);
				if (!text.IsNullOrEmpty() && !this.Visited.ContainsKey(text))
				{
					this.Visited[text] = true;
					if (!E.Object.DisplayName.Contains("[Creature]"))
					{
						if (GlobalConfig.GetBoolSetting("EvilTwinLevelBased", false))
						{
							if (this.Due > 0 && EvilTwin.CHANCE_PER_ZONE_LEVEL_BASED.in100() && EvilTwin.CreateEvilTwin(this.ParentObject, EvilTwin.DEFECT_PREFIX, null, EvilTwin.DEFECT_MESSAGE, "&K", null, null, true, EvilTwin.DEFECT_DESCRIPTION, null))
							{
								this.Due--;
							}
						}
						else if (EvilTwin.CHANCE_PER_ZONE_NON_LEVEL_BASED.in100())
						{
							EvilTwin.CreateEvilTwin(this.ParentObject, EvilTwin.DEFECT_PREFIX, null, EvilTwin.DEFECT_MESSAGE, "&K", null, null, true, EvilTwin.DEFECT_DESCRIPTION, null);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3FE RID: 50174 RVA: 0x004773DC File Offset: 0x004755DC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("glass", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3FF RID: 50175 RVA: 0x00477404 File Offset: 0x00475604
		public override bool HandleEvent(AfterLevelGainedEvent E)
		{
			if (E.Actor.IsPlayer() && GlobalConfig.GetBoolSetting("EvilTwinLevelBased", false) && (EvilTwin.BASE_CHANCE_PER_LEVEL_LEVEL_BASED + EvilTwin.CHANCE_PER_LEVEL_LEVEL_BASED * this.ParentObject.Stat("Level", 0)).in100())
			{
				this.Due++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C400 RID: 50176 RVA: 0x00477464 File Offset: 0x00475664
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, PooledEvent<AfterLevelGainedEvent>.ID, 0, false);
		}

		// Token: 0x04004DC3 RID: 19907
		public static readonly int CHANCE_PER_ZONE_NON_LEVEL_BASED = 5;

		// Token: 0x04004DC4 RID: 19908
		public static readonly int CHANCE_PER_ZONE_LEVEL_BASED = 25;

		// Token: 0x04004DC5 RID: 19909
		public static readonly int BASE_CHANCE_PER_LEVEL_LEVEL_BASED = 20;

		// Token: 0x04004DC6 RID: 19910
		public static readonly int CHANCE_PER_LEVEL_LEVEL_BASED = 2;

		// Token: 0x04004DC7 RID: 19911
		public static readonly string DEFECT_PREFIX = "Evil";

		// Token: 0x04004DC8 RID: 19912
		public static readonly string DEFECT_MESSAGE = "{{c|You sense a sinister presence nearby.}}";

		// Token: 0x04004DC9 RID: 19913
		public static readonly string DEFECT_DESCRIPTION = "It's evil you.";

		// Token: 0x04004DCA RID: 19914
		public int Due;

		// Token: 0x04004DCB RID: 19915
		public Dictionary<string, bool> Visited = new Dictionary<string, bool>();
	}
}
