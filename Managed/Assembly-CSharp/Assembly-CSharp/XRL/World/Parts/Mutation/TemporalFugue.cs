using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001401 RID: 5121
	[Serializable]
	public class TemporalFugue : BaseMutation
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600C6BD RID: 50877 RVA: 0x00487370 File Offset: 0x00485570
		public virtual string CommandID
		{
			get
			{
				return "CommandTemporalFugue";
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600C6BE RID: 50878 RVA: 0x00487377 File Offset: 0x00485577
		public virtual string AbilityClass
		{
			get
			{
				return "Mental Mutations";
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600C6BF RID: 50879 RVA: 0x0048737E File Offset: 0x0048557E
		public virtual bool IsRealityDistortionBased
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600C6C0 RID: 50880 RVA: 0x00487381 File Offset: 0x00485581
		public virtual bool AffectedByWillpower
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600C6C1 RID: 50881 RVA: 0x00487384 File Offset: 0x00485584
		public virtual int AIMaxDistance
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600C6C2 RID: 50882 RVA: 0x00487387 File Offset: 0x00485587
		public TemporalFugue()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C6C3 RID: 50883 RVA: 0x0048739A File Offset: 0x0048559A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C6C4 RID: 50884 RVA: 0x0048739D File Offset: 0x0048559D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C6C5 RID: 50885 RVA: 0x004873C4 File Offset: 0x004855C4
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.AIMaxDistance && GameObject.Validate(E.Target) && !E.Actor.HasStringProperty("FugueCopy") && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && (!this.IsRealityDistortionBased || base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0)) && E.Actor.CanBeReplicated(E.Actor, null, true))
			{
				if (!E.Actor.HasTagOrProperty("AIAbilityIgnoreCon"))
				{
					int? num = E.Target.Con(E.Actor, false);
					int num2 = Stat.Random(-10, -5);
					if (!((num.GetValueOrDefault() > num2) & (num != null)))
					{
						goto IL_00D1;
					}
				}
				E.Add(this.CommandID, 1, null, false, false, null, null);
			}
			IL_00D1:
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6C6 RID: 50886 RVA: 0x004874AC File Offset: 0x004856AC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.CommandID)
			{
				if (!this.PerformTemporalFugue(E))
				{
					return false;
				}
				base.UseEnergy(1000, "Mental Mutation TemporalFugue");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6C7 RID: 50887 RVA: 0x00487504 File Offset: 0x00485704
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("time", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6C8 RID: 50888 RVA: 0x0048752C File Offset: 0x0048572C
		public override string GetDescription()
		{
			return "You quickly pass back and forth through time creating multiple copies of yourself.";
		}

		// Token: 0x0600C6C9 RID: 50889 RVA: 0x00487533 File Offset: 0x00485733
		public static int GetTemporalFugueDuration(int Level)
		{
			return 20 + 2 * (Level / 2);
		}

		// Token: 0x0600C6CA RID: 50890 RVA: 0x0048753D File Offset: 0x0048573D
		public int GetTemporalFugueDuration()
		{
			return TemporalFugue.GetTemporalFugueDuration(base.Level);
		}

		// Token: 0x0600C6CB RID: 50891 RVA: 0x0048754A File Offset: 0x0048574A
		public static int GetTemporalFugueCopies(int Level)
		{
			return (Level - 1) / 2 + 1;
		}

		// Token: 0x0600C6CC RID: 50892 RVA: 0x00487553 File Offset: 0x00485753
		public int GetTemporalFugueCopies()
		{
			return TemporalFugue.GetTemporalFugueCopies(base.Level);
		}

		// Token: 0x0600C6CD RID: 50893 RVA: 0x00487560 File Offset: 0x00485760
		public int GetCooldown()
		{
			return this.GetCooldown(base.Level);
		}

		// Token: 0x0600C6CE RID: 50894 RVA: 0x0048756E File Offset: 0x0048576E
		public virtual int GetCooldown(int Level)
		{
			return 200;
		}

		// Token: 0x0600C6CF RID: 50895 RVA: 0x00487578 File Offset: 0x00485778
		public override string GetLevelText(int Level)
		{
			return "" + "Duration: {{rules|" + TemporalFugue.GetTemporalFugueDuration(Level).ToString() + "}} rounds\n" + "Copies: {{rules|" + TemporalFugue.GetTemporalFugueCopies(Level).ToString() + "}}\n" + "Cooldown: {{rules|" + this.GetCooldown(Level).ToString() + "}} rounds";
		}

		// Token: 0x0600C6D0 RID: 50896 RVA: 0x004875E4 File Offset: 0x004857E4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			stats.Set("Duration", TemporalFugue.GetTemporalFugueDuration(Level), !flag, 0);
			stats.Set("Copies", TemporalFugue.GetTemporalFugueCopies(Level), !flag, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C6D1 RID: 50897 RVA: 0x0048764C File Offset: 0x0048584C
		public static bool PerformTemporalFugue(GameObject Actor, GameObject Subject = null, GameObject Source = null, TemporalFugue Mutation = null, IEvent TriggeringEvent = null, bool Involuntary = false, bool IsRealityDistortionBased = true, int? Duration = null, int? Copies = null, int HostileCopyChance = 0, string Context = "Temporal Fugue", string FriendlyCopyColorString = null, string HostileCopyColorString = null, string FriendlyCopyPrefix = null, string HostileCopyPrefix = null)
		{
			if (Subject == null)
			{
				Subject = Actor;
			}
			if (Source == null)
			{
				Source = Actor;
			}
			if (Subject.HasStringProperty("FugueCopy"))
			{
				Actor.Fail(Subject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " too tenuously anchored in this time to be temporally duplicated in it.");
				return false;
			}
			Cell currentCell = Subject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (currentCell.OnWorldMap())
			{
				Actor.Fail("You may not perform temporal fugue on the world map.");
				return false;
			}
			if (!Subject.CanBeReplicated(Actor, Context, true))
			{
				Actor.Fail("It is impossible to duplicate " + Subject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			if (IsRealityDistortionBased)
			{
				Event @event;
				if (Mutation != null)
				{
					@event = Event.New("InitiateRealityDistortionLocal", "Actor", Actor, "Object", Subject, "Mutation", Mutation);
				}
				else
				{
					@event = Event.New("InitiateRealityDistortionLocal", "Actor", Actor, "Object", Subject, "Source", Source ?? Actor);
				}
				if (!Actor.FireEvent(@event, TriggeringEvent))
				{
					return false;
				}
			}
			if (Subject != null)
			{
				Subject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_temporalFugue_copy", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
			}
			int num = 0;
			int num2;
			if (Duration != null)
			{
				num2 = Duration.Value;
			}
			else if (Mutation != null)
			{
				num2 = Mutation.GetTemporalFugueDuration() + 1;
			}
			else
			{
				num2 = Stat.Random(20, 40);
			}
			int num3;
			if (Copies != null)
			{
				num3 = Copies.Value;
			}
			else if (Mutation != null)
			{
				num3 = Mutation.GetTemporalFugueCopies();
			}
			else
			{
				num3 = Stat.Random(2, 4);
			}
			if (Mutation != null)
			{
				Mutation.CooldownMyActivatedAbility(Mutation.ActivatedAbilityID, Mutation.GetCooldown(), null, null, false);
			}
			int intProperty = Actor.GetIntProperty("MentalCloneMultiplier", 0);
			if (intProperty > 0)
			{
				num3 *= intProperty;
			}
			List<Cell> adjacentCells = currentCell.GetAdjacentCells(2, true, true);
			List<Cell> list = null;
			List<Cell> list2 = null;
			foreach (Cell cell in adjacentCells.ShuffleInPlace<Cell>(null))
			{
				if (cell.IsSpawnable())
				{
					int navigationWeightFor = cell.GetNavigationWeightFor(Subject, false, false, false, false, false);
					if (navigationWeightFor < 95)
					{
						if (navigationWeightFor >= 70)
						{
							if (list2 == null)
							{
								list2 = new List<Cell>();
							}
							list2.Add(cell);
						}
						else if (navigationWeightFor >= 30 || !cell.IsEmpty())
						{
							if (list == null)
							{
								list = new List<Cell>();
							}
							list.Add(cell);
						}
						else
						{
							GameObject gameObject = Subject;
							GameObject gameObject2 = Source;
							if (TemporalFugue.CreateFugueCopyOf(Actor, gameObject, cell, gameObject2, IsRealityDistortionBased, num2, HostileCopyChance, Context, FriendlyCopyColorString, HostileCopyColorString, FriendlyCopyPrefix, HostileCopyPrefix, Mutation) != null && ++num >= num3)
							{
								list = null;
								list2 = null;
								break;
							}
						}
					}
				}
			}
			if (list != null)
			{
				foreach (Cell cell2 in list)
				{
					GameObject gameObject3 = Subject;
					GameObject gameObject2 = Source;
					if (TemporalFugue.CreateFugueCopyOf(Actor, gameObject3, cell2, gameObject2, IsRealityDistortionBased, num2, HostileCopyChance, Context, FriendlyCopyColorString, HostileCopyColorString, FriendlyCopyPrefix, HostileCopyPrefix, Mutation) != null && ++num >= num3)
					{
						list2 = null;
						break;
					}
				}
			}
			if (list2 != null)
			{
				foreach (Cell cell3 in list2)
				{
					GameObject gameObject4 = Subject;
					GameObject gameObject2 = Source;
					Cell cell4 = cell3;
					GameObject gameObject5 = gameObject2;
					int num4 = num2;
					if (TemporalFugue.CreateFugueCopyOf(Actor, gameObject4, cell4, gameObject5, IsRealityDistortionBased, num4, HostileCopyChance, Context, FriendlyCopyColorString, HostileCopyColorString, FriendlyCopyPrefix, HostileCopyPrefix, Mutation) != null && ++num >= num3)
					{
						break;
					}
				}
			}
			if (num < num3)
			{
				foreach (Cell cell5 in Subject.CurrentCell.GetConnectedSpawnLocations(num3 - num))
				{
					GameObject gameObject6 = Subject;
					GameObject gameObject2 = Source;
					if (TemporalFugue.CreateFugueCopyOf(Actor, gameObject6, cell5, gameObject2, IsRealityDistortionBased, num2, HostileCopyChance, Context, FriendlyCopyColorString, HostileCopyColorString, FriendlyCopyPrefix, HostileCopyPrefix, Mutation) != null && ++num >= num3)
					{
						break;
					}
				}
			}
			IComponent<GameObject>.XDidY(Actor, "blur", "through spacetime", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			IComponent<GameObject>.XDidY(Actor, "multiply", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x0600C6D2 RID: 50898 RVA: 0x00487A94 File Offset: 0x00485C94
		public virtual bool PerformTemporalFugue(IEvent TriggeringEvent = null)
		{
			return TemporalFugue.PerformTemporalFugue(this.ParentObject, this.ParentObject, null, this, TriggeringEvent, false, true, null, null, 0, "Temporal Fugue", null, null, null, null);
		}

		// Token: 0x0600C6D3 RID: 50899 RVA: 0x00487AD4 File Offset: 0x00485CD4
		public static GameObject CreateFugueCopyOf(GameObject Actor, GameObject Object, Cell TargetCell, GameObject Source = null, bool IsRealityDistortionBased = true, int Duration = 20, int HostileCopyChance = 0, string Context = "Temporal Fugue", string FriendlyCopyColorString = null, string HostileCopyColorString = null, string FriendlyCopyPrefix = null, string HostileCopyPrefix = null, IPart Mutation = null)
		{
			if (Source == null)
			{
				Source = Actor;
			}
			if (Object.HasStringProperty("FugueCopy"))
			{
				return null;
			}
			if (IsRealityDistortionBased && !IComponent<GameObject>.CheckRealityDistortionAccessibility(null, TargetCell, Actor, Source, Mutation, 0))
			{
				return null;
			}
			if (!Object.CanBeReplicated(Actor, Context, true))
			{
				return null;
			}
			GameObject gameObject = Object.DeepCopy(false, false, null);
			if (gameObject.HasStat("XPValue"))
			{
				gameObject.GetStat("XPValue").BaseValue = 0;
			}
			if (Object.IsPlayer())
			{
				gameObject.SetStringProperty("PlayerCopy", "true", false);
				gameObject.SetStringProperty("PlayerCopyDescription", "one of your " + Context + " clones", false);
				if (Object.IsOriginalPlayerBody())
				{
					if (Object.HasPart<ConversationScript>())
					{
						gameObject.RemovePart<Chat>();
						gameObject.AddPart<Chat>(new Chat("*You make small talk with =player.reflexive=."), true, false);
					}
					gameObject.SetStringProperty("OriginalPlayerCopy", "true", false);
				}
			}
			gameObject.SetStringProperty("FugueCopy", Object.ID, false);
			gameObject.SetStringProperty("CloneOf", Object.ID, false);
			gameObject.RemoveStringProperty("OriginalPlayerBody");
			if (HostileCopyChance.in100())
			{
				if (gameObject.Brain != null)
				{
					gameObject.Brain.PartyLeader = null;
					gameObject.Brain.Staying = false;
					gameObject.Brain.Passive = false;
					gameObject.Brain.Goals.Clear();
					gameObject.Brain.Target = Object;
				}
				if (!HostileCopyColorString.IsNullOrEmpty())
				{
					gameObject.Render.ColorString = HostileCopyColorString;
				}
				if (!HostileCopyPrefix.IsNullOrEmpty())
				{
					gameObject.Render.DisplayName = HostileCopyPrefix + " " + gameObject.Render.DisplayName;
				}
			}
			else
			{
				AIReplica aireplica = gameObject.RequirePart<AIReplica>(false);
				aireplica.OriginalID = Object.ID;
				aireplica.ForceAllied = true;
				if (gameObject.Brain != null)
				{
					gameObject.Brain.Goals.Clear();
					gameObject.IsTrifling = true;
					gameObject.Brain.SetPartyLeader(Object, 0, false, true);
				}
				if (!FriendlyCopyColorString.IsNullOrEmpty())
				{
					gameObject.Render.ColorString = FriendlyCopyColorString;
				}
				if (!FriendlyCopyPrefix.IsNullOrEmpty())
				{
					gameObject.Render.DisplayName = FriendlyCopyPrefix + " " + gameObject.Render.DisplayName;
				}
			}
			Temporary.AddHierarchically(gameObject, Duration, "*fugue", null, false);
			gameObject.FireEvent("TemporalFugueCopied");
			TargetCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject.MakeActive(false);
			gameObject.FugueVFX();
			WasReplicatedEvent.Send(Object, Actor, gameObject, Context, true);
			ReplicaCreatedEvent.Send(gameObject, Actor, Object, Context, true);
			if (!Achievement.CLONES_30.Achieved && Object.IsPlayer())
			{
				Cloning.QueueAchievementCheck();
			}
			return gameObject;
		}

		// Token: 0x0600C6D4 RID: 50900 RVA: 0x00487D78 File Offset: 0x00485F78
		public bool PerformTemporalFugue(Event TriggeringEvent = null)
		{
			return TemporalFugue.PerformTemporalFugue(this.ParentObject, null, null, this, TriggeringEvent, false, true, null, null, 0, "Temporal Fugue", null, null, null, null);
		}

		// Token: 0x0600C6D5 RID: 50901 RVA: 0x00487DB4 File Offset: 0x00485FB4
		public override bool FireEvent(Event E)
		{
			if (E.ID == this.CommandID)
			{
				if (!this.PerformTemporalFugue(E))
				{
					return false;
				}
				base.UseEnergy(1000, "Mental Mutation TemporalFugue");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C6D6 RID: 50902 RVA: 0x00487E0C File Offset: 0x0048600C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C6D7 RID: 50903 RVA: 0x00487E18 File Offset: 0x00486018
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(base.GetDisplayName(true), this.CommandID, this.AbilityClass, null, "\u0013", null, false, false, false, false, this.IsRealityDistortionBased, false, false, false, true, this.AffectedByWillpower, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C6D8 RID: 50904 RVA: 0x00487E6F File Offset: 0x0048606F
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
