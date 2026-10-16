using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Genkit;
using Occult.Engine.CodeGeneration;
using Unity.Profiling;
using XRL.Collections;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.AI.Pathfinding;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000DBD RID: 3517
	[HasWishCommand]
	[GenerateSerializationPartial(PostWrite = "PostWrite", PostRead = "PostRead")]
	[GeneratePoolingPartial(Capacity = 64)]
	[Serializable]
	public class Brain : IPart
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06008C4A RID: 35914 RVA: 0x0031C3AA File Offset: 0x0031A5AA
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Brain.BrainPool;
			}
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x0031C3B4 File Offset: 0x0031A5B4
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.Flags = 17792;
			this.MaxMissileRange = 80;
			this.MinKillRadius = 5;
			this.MaxKillRadius = 15;
			this.HostileWalkRadius = 84;
			this.MaxWanderRadius = 12;
			GameObjectReference leaderReference = this.LeaderReference;
			if (leaderReference != null)
			{
				leaderReference.Clear();
			}
			GlobalLocation startingCell = this.StartingCell;
			if (startingCell != null)
			{
				startingCell.Clear();
			}
			this.LastThought = null;
			this.Allegiance.Clear();
			this.FactionFeelings.Clear();
			this.PartyMembers.Clear();
			this.Opinions.Clear();
			this.Goals.Clear();
			Dictionary<GameObject, int> friendlyFire = this.FriendlyFire;
			if (friendlyFire == null)
			{
				return;
			}
			friendlyFire.Clear();
		}

		// Token: 0x06008C4C RID: 35916 RVA: 0x0031C46C File Offset: 0x0031A66C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.MaxMissileRange);
			Writer.WriteOptimized(this.MinKillRadius);
			Writer.WriteOptimized(this.MaxKillRadius);
			Writer.WriteOptimized(this.HostileWalkRadius);
			Writer.WriteOptimized(this.MaxWanderRadius);
			Writer.Write(this.StartingCell);
			Writer.WriteComposite<AllegianceSet>(this.Allegiance);
			Writer.WriteComposite<PartyCollection>(this.PartyMembers);
			Writer.WriteComposite<OpinionMap>(this.Opinions);
			this.PostWrite(Writer);
		}

		// Token: 0x06008C4D RID: 35917 RVA: 0x0031C4F8 File Offset: 0x0031A6F8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Flags = Reader.ReadOptimizedInt32();
			this.MaxMissileRange = Reader.ReadOptimizedInt32();
			this.MinKillRadius = Reader.ReadOptimizedInt32();
			this.MaxKillRadius = Reader.ReadOptimizedInt32();
			this.HostileWalkRadius = Reader.ReadOptimizedInt32();
			this.MaxWanderRadius = Reader.ReadOptimizedInt32();
			this.StartingCell = (GlobalLocation)Reader.ReadComposite();
			this.Allegiance = Reader.ReadComposite<AllegianceSet>();
			this.PartyMembers = Reader.ReadComposite<PartyCollection>();
			this.Opinions = Reader.ReadComposite<OpinionMap>();
			this.PostRead(Reader);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06008C4E RID: 35918 RVA: 0x0031C589 File Offset: 0x0031A789
		// (set) Token: 0x06008C4F RID: 35919 RVA: 0x0031C59B File Offset: 0x0031A79B
		public bool Hostile
		{
			get
			{
				return (this.Allegiance.Flags & 1) == 1;
			}
			[Obsolete("Hostile is part of allegiance set, use Brain.Allegiance.Hostile = value")]
			set
			{
				this.Allegiance.Flags = (value ? (this.Allegiance.Flags | 1) : (this.Allegiance.Flags & -2));
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06008C50 RID: 35920 RVA: 0x0031C5C8 File Offset: 0x0031A7C8
		// (set) Token: 0x06008C51 RID: 35921 RVA: 0x0031C5DA File Offset: 0x0031A7DA
		public bool Calm
		{
			get
			{
				return (this.Allegiance.Flags & 2) == 2;
			}
			[Obsolete("Calm is part of allegiance set, use Brain.Allegiance.Calm = value")]
			set
			{
				this.Allegiance.Flags = (value ? (this.Allegiance.Flags | 2) : (this.Allegiance.Flags & -3));
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06008C52 RID: 35922 RVA: 0x0031C607 File Offset: 0x0031A807
		// (set) Token: 0x06008C53 RID: 35923 RVA: 0x0031C614 File Offset: 0x0031A814
		public bool Wanders
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06008C54 RID: 35924 RVA: 0x0031C632 File Offset: 0x0031A832
		// (set) Token: 0x06008C55 RID: 35925 RVA: 0x0031C63F File Offset: 0x0031A83F
		public bool WandersRandomly
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06008C56 RID: 35926 RVA: 0x0031C65D File Offset: 0x0031A85D
		// (set) Token: 0x06008C57 RID: 35927 RVA: 0x0031C66C File Offset: 0x0031A86C
		public bool Aquatic
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06008C58 RID: 35928 RVA: 0x0031C68B File Offset: 0x0031A88B
		// (set) Token: 0x06008C59 RID: 35929 RVA: 0x0031C69A File Offset: 0x0031A89A
		public bool LivesOnWalls
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06008C5A RID: 35930 RVA: 0x0031C6B9 File Offset: 0x0031A8B9
		// (set) Token: 0x06008C5B RID: 35931 RVA: 0x0031C6C8 File Offset: 0x0031A8C8
		public bool WallWalker
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06008C5C RID: 35932 RVA: 0x0031C6E7 File Offset: 0x0031A8E7
		// (set) Token: 0x06008C5D RID: 35933 RVA: 0x0031C6FC File Offset: 0x0031A8FC
		public bool Mobile
		{
			get
			{
				return (this.Flags & 128) == 128;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 128) : (this.Flags & -129));
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06008C5E RID: 35934 RVA: 0x0031C721 File Offset: 0x0031A921
		// (set) Token: 0x06008C5F RID: 35935 RVA: 0x0031C736 File Offset: 0x0031A936
		public bool Hibernating
		{
			get
			{
				return (this.Flags & 256) == 256;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 256) : (this.Flags & -257));
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06008C60 RID: 35936 RVA: 0x0031C75B File Offset: 0x0031A95B
		// (set) Token: 0x06008C61 RID: 35937 RVA: 0x0031C770 File Offset: 0x0031A970
		public bool PointBlankRange
		{
			get
			{
				return (this.Flags & 512) == 512;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 512) : (this.Flags & -513));
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06008C62 RID: 35938 RVA: 0x0031C795 File Offset: 0x0031A995
		// (set) Token: 0x06008C63 RID: 35939 RVA: 0x0031C7AA File Offset: 0x0031A9AA
		public bool DoReequip
		{
			get
			{
				return (this.Flags & 1024) == 1024;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1024) : (this.Flags & -1025));
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06008C64 RID: 35940 RVA: 0x0031C7CF File Offset: 0x0031A9CF
		// (set) Token: 0x06008C65 RID: 35941 RVA: 0x0031C7E4 File Offset: 0x0031A9E4
		public bool NeedToReload
		{
			get
			{
				return (this.Flags & 2048) == 2048;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2048) : (this.Flags & -2049));
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06008C66 RID: 35942 RVA: 0x0031C809 File Offset: 0x0031AA09
		// (set) Token: 0x06008C67 RID: 35943 RVA: 0x0031C81E File Offset: 0x0031AA1E
		public bool Staying
		{
			get
			{
				return (this.Flags & 4096) == 4096;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4096) : (this.Flags & -4097));
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06008C68 RID: 35944 RVA: 0x0031C843 File Offset: 0x0031AA43
		// (set) Token: 0x06008C69 RID: 35945 RVA: 0x0031C858 File Offset: 0x0031AA58
		public bool Passive
		{
			get
			{
				return (this.Flags & 8192) == 8192;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8192) : (this.Flags & -8193));
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06008C6A RID: 35946 RVA: 0x0031C87D File Offset: 0x0031AA7D
		// (set) Token: 0x06008C6B RID: 35947 RVA: 0x0031C892 File Offset: 0x0031AA92
		public bool DoPrimaryChoiceOnReequip
		{
			get
			{
				return (this.Flags & 16384) == 16384;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16384) : (this.Flags & -16385));
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06008C6C RID: 35948 RVA: 0x0031C8B7 File Offset: 0x0031AAB7
		// (set) Token: 0x06008C6D RID: 35949 RVA: 0x0031C8CA File Offset: 0x0031AACA
		public GameObject PartyLeader
		{
			get
			{
				GameObjectReference leaderReference = this.LeaderReference;
				if (leaderReference == null)
				{
					return null;
				}
				return leaderReference.Object;
			}
			set
			{
				this.SetPartyLeader(value, 0, false, false);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (set) Token: 0x06008C6E RID: 35950 RVA: 0x0031C8D6 File Offset: 0x0031AAD6
		public string Factions
		{
			set
			{
				Brain.FillFactionMembership(this.FindAllegiance(0), value);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (set) Token: 0x06008C6F RID: 35951 RVA: 0x0031C8E8 File Offset: 0x0031AAE8
		public string Feelings
		{
			set
			{
				if (value.IsNullOrEmpty())
				{
					return;
				}
				foreach (ReadOnlySpan<char> readOnlySpan in value.DelimitedBy(','))
				{
					int num = readOnlySpan.IndexOf(':');
					if (num != -1)
					{
						ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(0, num);
						int num2;
						if (int.TryParse(readOnlySpan.Slice(num + 1), out num2))
						{
							if (readOnlySpan2.Length == 3 && readOnlySpan2.SequenceEqual<char>("All"))
							{
								using (IEnumerator<Faction> enumerator2 = XRL.World.Factions.Loop().GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Faction faction = enumerator2.Current;
										this.FactionFeelings[faction.Name] = num2;
									}
									continue;
								}
							}
							this.FactionFeelings[new string(readOnlySpan2)] = num2;
						}
					}
				}
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06008C70 RID: 35952 RVA: 0x0031C9DC File Offset: 0x0031ABDC
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x0031C9E4 File Offset: 0x0031ABE4
		public bool StepTowards(Cell targetCell, bool Global = false)
		{
			if (targetCell == null)
			{
				return true;
			}
			this.Think("I'm going to move towards my target.");
			if (targetCell.ParentZone.IsWorldMap())
			{
				this.Think("Target's on the world map, can't follow!");
				return false;
			}
			FindPath findPath = new FindPath(this.ParentObject.CurrentCell, targetCell, Global, true, this.ParentObject, 94, false, false, false, false, false, null);
			if (findPath.Usable)
			{
				if (findPath.Directions.Count > 0)
				{
					this.PushGoal(new Step(findPath.Directions[0], false, false, false, false, null, false));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x0031CA74 File Offset: 0x0031AC74
		public int RemoveGoalsDescendedFrom<T>()
		{
			int num = 0;
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				if (this.Goals.Items[i] is T)
				{
					this.Goals.Items.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x0031CAD0 File Offset: 0x0031ACD0
		public GoalHandler FindGoal(string Name)
		{
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				if (this.Goals.Items[i].GetType().Name == Name)
				{
					return this.Goals.Items[i];
				}
			}
			return null;
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x0031CB30 File Offset: 0x0031AD30
		public bool HasGoal(string Name)
		{
			using (List<GoalHandler>.Enumerator enumerator = this.Goals.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().Name == Name)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		///   True unless the <see cref="F:XRL.World.Parts.Brain.Goals" /> are empty or only contain the "Bored" goal.
		/// </summary>
		// Token: 0x06008C75 RID: 35957 RVA: 0x0031CB9C File Offset: 0x0031AD9C
		public bool HasGoal()
		{
			int count = this.Goals.Count;
			return count != 0 && (count != 1 || this.Goals.Peek().GetType().Name != "Bored");
		}

		// Token: 0x06008C76 RID: 35958 RVA: 0x0031CBE8 File Offset: 0x0031ADE8
		public bool HasGoalOtherThan(string What)
		{
			int count = this.Goals.Count;
			if (count == 0)
			{
				return false;
			}
			if (count != 1)
			{
				foreach (GoalHandler goalHandler in this.Goals.Items)
				{
					if (goalHandler.GetType().Name != What && goalHandler.GetType().Name != "Bored")
					{
						return true;
					}
				}
				return false;
			}
			return this.Goals.Peek().GetType().Name != What && this.Goals.Peek().GetType().Name != "Bored";
		}

		// Token: 0x06008C77 RID: 35959 RVA: 0x0031CCC0 File Offset: 0x0031AEC0
		public AllegianceSet FindAllegiance(int ID)
		{
			for (AllegianceSet allegianceSet = this.Allegiance; allegianceSet != null; allegianceSet = allegianceSet.Previous)
			{
				if (allegianceSet.SourceID == ID)
				{
					return allegianceSet;
				}
			}
			return null;
		}

		// Token: 0x06008C78 RID: 35960 RVA: 0x0031CCEC File Offset: 0x0031AEEC
		public AllegianceSet FindAllegiance<T>(int ID = 0) where T : IAllyReason
		{
			return this.FindAllegiance(typeof(T), ID);
		}

		// Token: 0x06008C79 RID: 35961 RVA: 0x0031CD00 File Offset: 0x0031AF00
		public AllegianceSet FindAllegiance(Type Type, int ID = 0)
		{
			for (AllegianceSet allegianceSet = this.Allegiance; allegianceSet != null; allegianceSet = allegianceSet.Previous)
			{
				if (ID == 0 || allegianceSet.SourceID == ID)
				{
					IAllyReason reason = allegianceSet.Reason;
					if (((reason != null) ? reason.GetType() : null) == Type)
					{
						return allegianceSet;
					}
				}
			}
			return null;
		}

		// Token: 0x06008C7A RID: 35962 RVA: 0x0031CD44 File Offset: 0x0031AF44
		public AllegianceSet GetBaseAllegiance()
		{
			return this.FindAllegiance(0);
		}

		// Token: 0x06008C7B RID: 35963 RVA: 0x0031CD4D File Offset: 0x0031AF4D
		public void RemoveAllegiance(AllegianceSet Set)
		{
			this.RemoveAllegiance(Set.SourceID);
		}

		// Token: 0x06008C7C RID: 35964 RVA: 0x0031CD5B File Offset: 0x0031AF5B
		public void RemoveAllegiance(GameObject Object)
		{
			this.RemoveAllegiance(Object.BaseID);
		}

		// Token: 0x06008C7D RID: 35965 RVA: 0x0031CD69 File Offset: 0x0031AF69
		public void RemoveAllegiance<T>(GameObject Object) where T : IAllyReason
		{
			if (Object != null && Object.HasID)
			{
				this.RemoveAllegiance(typeof(T), Object.BaseID);
			}
		}

		// Token: 0x06008C7E RID: 35966 RVA: 0x0031CD8C File Offset: 0x0031AF8C
		public void RemoveAllegiance<T>(int ID = 0) where T : IAllyReason
		{
			this.RemoveAllegiance(typeof(T), ID);
		}

		// Token: 0x06008C7F RID: 35967 RVA: 0x0031CDA0 File Offset: 0x0031AFA0
		public void RemoveAllegiance(Type Type, int ID = 0)
		{
			AllegianceSet allegianceSet = this.Allegiance;
			AllegianceSet allegianceSet2 = allegianceSet.Previous;
			if (ID == 0 || allegianceSet.SourceID == ID)
			{
				IAllyReason reason = allegianceSet.Reason;
				if (((reason != null) ? reason.GetType() : null) == Type)
				{
					this.Allegiance = allegianceSet2;
					allegianceSet.Previous = null;
					return;
				}
			}
			while (allegianceSet2 != null)
			{
				if (ID == 0 || allegianceSet2.SourceID == ID)
				{
					IAllyReason reason2 = allegianceSet2.Reason;
					if (((reason2 != null) ? reason2.GetType() : null) == Type)
					{
						allegianceSet.Previous = allegianceSet2.Previous;
						return;
					}
				}
				allegianceSet = allegianceSet2;
				allegianceSet2 = allegianceSet.Previous;
			}
		}

		// Token: 0x06008C80 RID: 35968 RVA: 0x0031CE28 File Offset: 0x0031B028
		public void RemoveAllegiance(int ID)
		{
			AllegianceSet allegianceSet = this.Allegiance;
			AllegianceSet allegianceSet2 = allegianceSet.Previous;
			if (allegianceSet.SourceID == ID)
			{
				this.Allegiance = allegianceSet2;
				allegianceSet.Previous = null;
				return;
			}
			while (allegianceSet2 != null)
			{
				if (allegianceSet2.SourceID == ID)
				{
					allegianceSet.Previous = allegianceSet2.Previous;
					return;
				}
				allegianceSet = allegianceSet2;
				allegianceSet2 = allegianceSet.Previous;
			}
		}

		// Token: 0x06008C81 RID: 35969 RVA: 0x0031CE7D File Offset: 0x0031B07D
		public void PushAllegiance(AllegianceSet Set)
		{
			if (this.Allegiance == Set)
			{
				return;
			}
			Set.Previous = this.Allegiance;
			this.Allegiance = Set;
		}

		// Token: 0x06008C82 RID: 35970 RVA: 0x0031CE9C File Offset: 0x0031B09C
		public void PopAllegiance()
		{
			if (this.Allegiance.Previous == null)
			{
				return;
			}
			AllegianceSet allegiance = this.Allegiance;
			this.Allegiance = allegiance.Previous;
			allegiance.Previous = null;
		}

		// Token: 0x06008C83 RID: 35971 RVA: 0x0031CED1 File Offset: 0x0031B0D1
		public void TakeBaseAllegiance(Brain Brain)
		{
			this.FindAllegiance(0).Copy(Brain.Allegiance);
		}

		// Token: 0x06008C84 RID: 35972 RVA: 0x0031CEE5 File Offset: 0x0031B0E5
		public void TakeBaseAllegiance(GameObject Object)
		{
			this.TakeBaseAllegiance(Object.Brain);
		}

		// Token: 0x06008C85 RID: 35973 RVA: 0x0031CEF4 File Offset: 0x0031B0F4
		public void TakeAllegiance(GameObject Object, IAllyReason Reason)
		{
			Brain brain = ((Object != null) ? Object.Brain : null);
			if (brain == null)
			{
				return;
			}
			AllegianceSet allegiance = brain.Allegiance;
			Type type = Reason.GetType();
			if (Reason.Replace != IAllyReason.ReplaceTarget.None)
			{
				this.RemoveAllegiance(type, (Reason.Replace == IAllyReason.ReplaceTarget.Source) ? Object.BaseID : 0);
			}
			AllegianceSet allegianceSet = new AllegianceSet();
			allegianceSet.Copy(allegiance);
			allegianceSet.SourceID = Object.BaseID;
			allegianceSet.Reason = Reason;
			XRLGame game = The.Game;
			Reason.Time = ((game != null) ? game.TimeTicks : 0L);
			this.PushAllegiance(allegianceSet);
			Reason.Initialize(this.ParentObject, Object, allegianceSet);
			CleanStack<GoalHandler> goals = this.Goals;
			if (goals == null)
			{
				return;
			}
			goals.Clear();
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x0031CF9E File Offset: 0x0031B19E
		public void TakeAllegiance<T>(GameObject Object) where T : IAllyReason, new()
		{
			this.TakeAllegiance(Object, new T());
		}

		// Token: 0x06008C87 RID: 35975 RVA: 0x0031CFB1 File Offset: 0x0031B1B1
		public void TakeDemeanor(GameObject Object)
		{
			this.Allegiance.Flags = Object.Brain.Allegiance.Flags;
		}

		// Token: 0x06008C88 RID: 35976 RVA: 0x0031CFCE File Offset: 0x0031B1CE
		public bool TakeOnAttitudesOf(GameObject Object, bool CopyLeader = false, bool CopyTarget = false)
		{
			this.TakeBaseAllegiance(Object);
			return true;
		}

		// Token: 0x06008C89 RID: 35977 RVA: 0x0031CFD8 File Offset: 0x0031B1D8
		public bool TakeOnAttitudesOf(Brain o, bool CopyLeader = false, bool CopyTarget = false)
		{
			this.TakeBaseAllegiance(o);
			return true;
		}

		// Token: 0x06008C8A RID: 35978 RVA: 0x0031CFE2 File Offset: 0x0031B1E2
		public void SetAlliedLeader<T>(GameObject Object, int Flags = 0, bool Silent = false) where T : IAllyReason, new()
		{
			this.TakeAllegiance<T>(Object);
			this.SetPartyLeader(Object, Flags, false, Silent);
		}

		// Token: 0x06008C8B RID: 35979 RVA: 0x0031CFF8 File Offset: 0x0031B1F8
		public void SetPartyLeader(GameObject Object, int Flags = 0, bool Transient = false, bool Silent = false)
		{
			GameObject gameObject = null;
			if (this.LeaderReference != null)
			{
				gameObject = this.LeaderReference.Object;
				this.LeaderReference.Set(Object);
				if (gameObject != null)
				{
					Brain brain = gameObject.Brain;
					if (brain != null)
					{
						brain.PartyMembers.Remove(this.ParentObject.BaseID);
					}
					if (!Silent && gameObject.IsPlayer())
					{
						gameObject.PlayWorldSound("sfx_characterMod_follower_lose", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
				}
			}
			else if (Object != null)
			{
				this.LeaderReference = new GameObjectReference(Object);
			}
			if (Object != null)
			{
				if (((Object != null) ? Object.Brain : null) == null && this.ParentObject != null)
				{
					string text = "Object without brain ";
					string blueprint = Object.Blueprint;
					string text2 = " trying to be set as partyleader for ";
					GameObject parentObject = this.ParentObject;
					MetricsManager.LogWarning(text + blueprint + text2 + ((parentObject != null) ? parentObject.Blueprint : null));
				}
				if (!Transient)
				{
					Brain brain2 = Object.Brain;
					if (brain2 != null)
					{
						brain2.PartyMembers.TryAdd(this.ParentObject, Flags);
					}
					this.Forgive(Object);
					Physics physics = this.ParentObject.Physics;
					if (((physics != null) ? physics.LastDamagedBy : null) == Object)
					{
						this.ParentObject.Physics.LastDamagedBy = null;
					}
					Physics physics2 = this.ParentObject.Physics;
					if (((physics2 != null) ? physics2.InflamedBy : null) == Object)
					{
						this.ParentObject.Physics.InflamedBy = null;
					}
				}
				if (Object.IsLedBy(this.ParentObject))
				{
					MetricsManager.LogError(string.Format("Potential leader cycle: {0} + {1}", Object, this.ParentObject));
				}
				if (!Silent && Object.IsPlayer())
				{
					Object.PlayWorldSound("sfx_characterMod_follower_gain", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			AfterChangePartyLeaderEvent.Send(this.ParentObject, Object, gameObject, Transient);
			if (Object != null && Object != gameObject)
			{
				Object.FireEvent(Event.New("GainedNewFollower", "Object", this.ParentObject));
			}
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x0031D1E4 File Offset: 0x0031B3E4
		public bool IsMobile()
		{
			return this.Mobile || ((this.ParentObject != null) & this.ParentObject.IsFlying);
		}

		// Token: 0x06008C8D RID: 35981 RVA: 0x0031D20A File Offset: 0x0031B40A
		[Obsolete("use IsMobile(), will be removed after Q2 2024")]
		public bool isMobile()
		{
			return this.IsMobile();
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x0031D212 File Offset: 0x0031B412
		public bool LimitToAquatic()
		{
			return this.Aquatic && (this.ParentObject == null || !this.ParentObject.IsFlying);
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x0031D236 File Offset: 0x0031B436
		[Obsolete("use LimitToAquatic(), will be removed after Q2 2024")]
		public bool limitToAquatic()
		{
			return this.LimitToAquatic();
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x0031D240 File Offset: 0x0031B440
		public void CheckMobility(out bool Immobile, out bool Waterbound, out bool WallWalker)
		{
			Immobile = !this.Mobile;
			Waterbound = this.Aquatic;
			WallWalker = this.WallWalker;
			if ((Immobile | Waterbound | WallWalker) && this.ParentObject != null && this.ParentObject.IsFlying)
			{
				Immobile = false;
				Waterbound = false;
				WallWalker = false;
			}
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x0031D290 File Offset: 0x0031B490
		[Obsolete("use CheckMobility(), will be removed after Q2 2024")]
		public void checkMobility(out bool Immobile, out bool Waterbound, out bool WallWalker)
		{
			this.CheckMobility(out Immobile, out Waterbound, out WallWalker);
		}

		// Token: 0x06008C92 RID: 35986 RVA: 0x0031D29B File Offset: 0x0031B49B
		public void SetFactionFeeling(string Faction, int Feeling)
		{
			this.FactionFeelings[Faction] = Feeling;
		}

		// Token: 0x06008C93 RID: 35987 RVA: 0x0031D2AA File Offset: 0x0031B4AA
		[Obsolete("use SetFactionFeeling(), will be removed after Q2 2024")]
		public void setFactionFeeling(string Faction, int Feeling)
		{
			this.SetFactionFeeling(Faction, Feeling);
		}

		// Token: 0x06008C94 RID: 35988 RVA: 0x0031D2B4 File Offset: 0x0031B4B4
		public void SetFactionMembership(string Faction, int Feeling)
		{
			this.Allegiance[Faction] = Feeling;
		}

		// Token: 0x06008C95 RID: 35989 RVA: 0x0031D2C3 File Offset: 0x0031B4C3
		[Obsolete("use SetFactionMembership(), will be removed after Q2 2024")]
		public void setFactionMembership(string Faction, int Membership)
		{
			this.SetFactionMembership(Faction, Membership);
		}

		// Token: 0x06008C96 RID: 35990 RVA: 0x0031D2D0 File Offset: 0x0031B4D0
		public bool CanFight()
		{
			if (!this.ParentObject.IsCombatObject())
			{
				return false;
			}
			int i = 0;
			int count = this.Goals.Count;
			while (i < count)
			{
				if (!this.Goals.Items[i].CanFight())
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06008C97 RID: 35991 RVA: 0x0031D320 File Offset: 0x0031B520
		public bool IsNonAggressive()
		{
			int i = 0;
			int count = this.Goals.Count;
			while (i < count)
			{
				if (this.Goals.Items[i].IsNonAggressive())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06008C98 RID: 35992 RVA: 0x0031D360 File Offset: 0x0031B560
		public bool IsBusy()
		{
			int i = 0;
			int count = this.Goals.Count;
			while (i < count)
			{
				if (this.Goals.Items[i].IsBusy())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06008C99 RID: 35993 RVA: 0x0031D3A0 File Offset: 0x0031B5A0
		public void WantToKill(GameObject Subject, string Because = null, bool Directed = false)
		{
			if (The.Core.IgnoreMe && Subject != null && Subject.IsPlayer())
			{
				return;
			}
			if (!this.ParentObject.IsCombatObject())
			{
				return;
			}
			this.ThinkOfKilling(Subject, Because);
			int i = 0;
			int count = this.Goals.Items.Count;
			while (i < count)
			{
				Kill kill = this.Goals.Items[i] as Kill;
				if (kill != null && kill._Target == Subject)
				{
					return;
				}
				i++;
			}
			if (this.Goals.Count == 0)
			{
				new Bored().Push(this);
			}
			this.Goals.Peek().PushChildGoal(new Kill(Subject, Directed));
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06008C9A RID: 35994 RVA: 0x0031D44C File Offset: 0x0031B64C
		// (set) Token: 0x06008C9B RID: 35995 RVA: 0x0031D4C0 File Offset: 0x0031B6C0
		public GameObject Target
		{
			get
			{
				if (this.ParentObject.IsPlayer())
				{
					return Sidebar.CurrentTarget;
				}
				for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
				{
					Kill kill = this.Goals.Items[i] as Kill;
					if (kill != null && kill.Target != null && kill.Target.IsValid())
					{
						return kill.Target;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (this.ParentObject.IsPlayer())
					{
						Sidebar.CurrentTarget = null;
					}
					for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
					{
						if (this.Goals.Items[i] is Kill)
						{
							for (int j = this.Goals.Items.Count - 1; j >= i; j--)
							{
								this.Goals.Pop();
							}
							i = this.Goals.Items.Count - 1;
						}
					}
					return;
				}
				if (this.Target == null && value != this.ParentObject)
				{
					if (this.ParentObject.IsPlayer())
					{
						Sidebar.CurrentTarget = value;
						return;
					}
					this.WantToKill(value, null, false);
				}
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06008C9C RID: 35996 RVA: 0x0031D586 File Offset: 0x0031B786
		[Obsolete("use pPhysics on parent object")]
		public Physics pPhysics
		{
			get
			{
				return this.ParentObject.Physics;
			}
		}

		// Token: 0x06008C9D RID: 35997 RVA: 0x0031D594 File Offset: 0x0031B794
		public void Mindwipe()
		{
			this.Goals.Clear();
			this.LeaderReference = null;
			this.StartingCell = null;
			this.Staying = false;
			this.Passive = false;
			this.Allegiance.Clear();
			this.Opinions.Clear();
			this.FactionFeelings.Clear();
			this.PartyMembers.Clear();
			if (this.FriendlyFire != null)
			{
				this.FriendlyFire.Clear();
			}
			GameObjectBlueprint blueprint = this.ParentObject.GetBlueprint(true);
			if (blueprint != null)
			{
				blueprint.ReinitializePart(this);
			}
		}

		// Token: 0x06008C9E RID: 35998 RVA: 0x0031D620 File Offset: 0x0031B820
		public void StopFighting(bool Involuntary = false)
		{
			this.Target = null;
			this.ClearHostileMemory();
			this.ClearHostileFactionFeelings();
			base.FlushNavigationCaches();
			if (this.ParentObject.HasRegisteredEvent("StopFighting"))
			{
				this.ParentObject.FireEvent(Event.New("StopFighting", "Target", null, "Involuntary", Involuntary ? 1 : 0));
			}
		}

		// Token: 0x06008C9F RID: 35999 RVA: 0x0031D680 File Offset: 0x0031B880
		public void StopFighting(GameObject Target, bool Involuntary = false)
		{
			if (this.ParentObject.IsPlayer() && Sidebar.CurrentTarget == Target)
			{
				Sidebar.CurrentTarget = null;
			}
			this.ForgiveCombat();
			bool flag = false;
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				Kill kill = this.Goals.Items[i] as Kill;
				if (kill != null && kill.Target == Target)
				{
					for (int j = this.Goals.Items.Count - 1; j >= i; j--)
					{
						this.Goals.Pop();
					}
					i = this.Goals.Items.Count;
					flag = true;
				}
			}
			base.FlushNavigationCaches();
			if (this.ParentObject.HasRegisteredEvent("StopFighting"))
			{
				Event @event = Event.New("StopFighting", 0, 0, 0);
				@event.SetParameter("Target", Target);
				if (Involuntary)
				{
					@event.SetFlag("Involuntary", true);
				}
				if (flag)
				{
					@event.SetFlag("GoalRemoved", true);
				}
				this.ParentObject.FireEvent(@event);
			}
		}

		// Token: 0x06008CA0 RID: 36000 RVA: 0x0031D794 File Offset: 0x0031B994
		public void StopFight(bool Involuntary = false)
		{
			List<GameObject> list = Event.NewGameObjectList();
			list.Add(this.ParentObject);
			The.ZoneManager.FindObjects(list, delegate(GameObject o)
			{
				if (o == this.ParentObject)
				{
					return false;
				}
				if (!o.InSamePartyAs(this.ParentObject))
				{
					GameObject target = o.Target;
					return target != null && target.InSamePartyAs(this.ParentObject);
				}
				return true;
			});
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				for (int j = 0; j < count; j++)
				{
					if (i != j)
					{
						list[i].StopFighting(list[j], Involuntary);
						list[j].StopFighting(list[i], Involuntary);
					}
				}
				i++;
			}
		}

		// Token: 0x06008CA1 RID: 36001 RVA: 0x0031D818 File Offset: 0x0031BA18
		public void StopFight(GameObject Object, bool Involuntary = false, bool Reciprocal = false)
		{
			if (Reciprocal)
			{
				List<GameObject> list = Event.NewGameObjectList();
				list.Add(this.ParentObject);
				list.Add(Object);
				The.ZoneManager.FindObjects(list, (GameObject o) => o != this.ParentObject && o != Object && (o.InSamePartyAs(this.ParentObject) || o.InSamePartyAs(Object)));
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					for (int j = 0; j < count; j++)
					{
						if (i != j)
						{
							list[i].StopFighting(list[j], Involuntary);
							list[j].StopFighting(list[i], Involuntary);
						}
					}
					i++;
				}
				return;
			}
			List<GameObject> list2 = Event.NewGameObjectList();
			List<GameObject> list3 = Event.NewGameObjectList();
			list2.Add(this.ParentObject);
			list3.Add(Object);
			The.ZoneManager.FindObjects(list2, (GameObject o) => o != this.ParentObject && o.InSamePartyAs(this.ParentObject));
			The.ZoneManager.FindObjects(list3, (GameObject o) => o != Object && o.InSamePartyAs(Object));
			int k = 0;
			int count2 = list2.Count;
			while (k < count2)
			{
				int l = 0;
				int count3 = list3.Count;
				while (l < count3)
				{
					list2[k].StopFighting(list3[l], Involuntary);
					l++;
				}
				k++;
			}
		}

		// Token: 0x06008CA2 RID: 36002 RVA: 0x0031D96B File Offset: 0x0031BB6B
		public void ClearHostileMemory()
		{
			this.ForgiveCombat();
		}

		// Token: 0x06008CA3 RID: 36003 RVA: 0x0031D974 File Offset: 0x0031BB74
		public void ClearHostileFactionFeelings()
		{
			if (this.FactionFeelings.Count <= 0)
			{
				return;
			}
			if (this.FactionFeelings.Count == 1)
			{
				if (this.FactionFeelings.First<KeyValuePair<string, int>>().Value < 0)
				{
					this.FactionFeelings.Clear();
				}
				return;
			}
			List<string> list = null;
			foreach (KeyValuePair<string, int> keyValuePair in this.FactionFeelings)
			{
				if (keyValuePair.Value < 0)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(keyValuePair.Key);
				}
			}
			if (list != null)
			{
				foreach (string text in list)
				{
					this.FactionFeelings.Remove(text);
				}
			}
		}

		// Token: 0x06008CA4 RID: 36004 RVA: 0x0031DA6C File Offset: 0x0031BC6C
		public bool ShouldRemember(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				return true;
			}
			Zone currentZone = Object.CurrentZone;
			return currentZone != null && currentZone.IsActive() && currentZone == this.ParentObject.CurrentZone;
		}

		// Token: 0x06008CA5 RID: 36005 RVA: 0x0031DAB8 File Offset: 0x0031BCB8
		public void PostWrite(SerializationWriter Writer)
		{
			int num = 0;
			if (this.FriendlyFire != null)
			{
				foreach (KeyValuePair<GameObject, int> keyValuePair in this.FriendlyFire)
				{
					GameObject gameObject;
					int num2;
					keyValuePair.Deconstruct(out gameObject, out num2);
					GameObject gameObject2 = gameObject;
					if (gameObject2 != null && this.ShouldRemember(gameObject2))
					{
						num++;
					}
				}
			}
			Writer.WriteOptimized(num);
			if (num > 0)
			{
				foreach (KeyValuePair<GameObject, int> keyValuePair in this.FriendlyFire)
				{
					GameObject gameObject;
					int num2;
					keyValuePair.Deconstruct(out gameObject, out num2);
					GameObject gameObject3 = gameObject;
					int num3 = num2;
					if (gameObject3 != null && this.ShouldRemember(gameObject3))
					{
						Writer.WriteGameObject(gameObject3);
						Writer.WriteOptimized(num3);
					}
				}
			}
			Writer.Write(this.LeaderReference);
			Writer.WriteComposite<StringMap<int>>(this.FactionFeelings);
		}

		// Token: 0x06008CA6 RID: 36006 RVA: 0x0031DBBC File Offset: 0x0031BDBC
		public void PostRead(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num == 0)
			{
				this.FriendlyFire = null;
			}
			else
			{
				if (this.FriendlyFire == null)
				{
					this.FriendlyFire = new Dictionary<GameObject, int>(num);
				}
				else
				{
					this.FriendlyFire.Clear();
					this.FriendlyFire.EnsureCapacity(num);
				}
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Reader.ReadGameObject("friendlyfire");
					this.FriendlyFire.TryAdd(gameObject, Reader.ReadOptimizedInt32());
				}
			}
			this.LeaderReference = Reader.ReadGameObjectReference();
			this.FactionFeelings = Reader.ReadComposite<StringMap<int>>();
			this.Opinions.ClearExpired();
		}

		// Token: 0x06008CA7 RID: 36007 RVA: 0x0031DC58 File Offset: 0x0031BE58
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Brain brain = base.DeepCopy(Parent, MapInv) as Brain;
			brain.Allegiance = new AllegianceSet();
			brain.Allegiance.Copy(this.Allegiance);
			brain.PartyMembers = new PartyCollection();
			brain.Opinions = new OpinionMap();
			return brain;
		}

		// Token: 0x06008CA8 RID: 36008 RVA: 0x0031DCA4 File Offset: 0x0031BEA4
		public override void Attach()
		{
			this.ParentObject.Brain = this;
			if (this.ParentObject.HasPart<Combat>())
			{
				(ref this.ParentObject.Flags).SetBit(2, true);
			}
		}

		// Token: 0x06008CA9 RID: 36009 RVA: 0x0031DCD1 File Offset: 0x0031BED1
		public override void Remove()
		{
			(ref this.ParentObject.Flags).SetBit(2, false);
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Brain : null) == this)
			{
				this.ParentObject.Brain = null;
			}
		}

		// Token: 0x06008CAA RID: 36010 RVA: 0x0031DD06 File Offset: 0x0031BF06
		public GoalHandler PushGoal(GoalHandler Goal)
		{
			Goal.Push(this);
			return Goal;
		}

		// Token: 0x06008CAB RID: 36011 RVA: 0x0031DD10 File Offset: 0x0031BF10
		public void Think(string Hrm)
		{
			this.LastThought = Hrm;
			int num;
			if (this.ParentObject.TryGetIntProperty("ThinkOutLoud", out num) && num > 0)
			{
				MessageQueue.AddPlayerMessage(this.ParentObject.DebugName + " thinks: '" + Hrm + "'", null, true);
			}
		}

		// Token: 0x06008CAC RID: 36012 RVA: 0x0031DD60 File Offset: 0x0031BF60
		public void ThinkOfKilling(GameObject who, string because = null)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("I'm going to kill ").Append(who.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
			if (!because.IsNullOrEmpty())
			{
				stringBuilder.Append(" because ").Append(because);
			}
			stringBuilder.Append('!');
			this.Think(stringBuilder.ToString());
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x0031DDD8 File Offset: 0x0031BFD8
		public void ForgiveCombat()
		{
			foreach (KeyValuePair<int, OpinionList> keyValuePair in this.Opinions)
			{
				int num;
				OpinionList opinionList;
				keyValuePair.Deconstruct(out num, out opinionList);
				OpinionList opinionList2 = opinionList;
				for (int i = opinionList2.Count - 1; i >= 0; i--)
				{
					if (opinionList2[i] is IOpinionCombat && opinionList2[i].BaseValue < 0)
					{
						opinionList2.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x0031DE70 File Offset: 0x0031C070
		public void Forgive(GameObject Object)
		{
			if (Object == null || !Object.HasID)
			{
				return;
			}
			OpinionList opinionList;
			if (this.Opinions.TryGetValue(Object.BaseID, out opinionList))
			{
				for (int i = opinionList.Count - 1; i >= 0; i--)
				{
					if (opinionList[i].BaseValue < 0)
					{
						opinionList.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x0031DEC7 File Offset: 0x0031C0C7
		public void BecomeCompanionOf(GameObject Object, bool Trifling = false, bool Involuntary = false)
		{
			this.SetAlliedLeader<AllyDefault>(Object, 0, false);
			this.StopFight(Object, Involuntary, false);
		}

		// Token: 0x06008CB0 RID: 36016 RVA: 0x0031DEDC File Offset: 0x0031C0DC
		public bool IsLedBy(GameObject Object)
		{
			GameObject partyLeader;
			for (GameObject gameObject = this.PartyLeader; gameObject != null; gameObject = partyLeader)
			{
				if (gameObject == Object)
				{
					return true;
				}
				partyLeader = gameObject.PartyLeader;
				if (partyLeader == this.ParentObject)
				{
					MetricsManager.LogError("leader cycle in " + this.ParentObject.DebugName + " + " + gameObject.DebugName);
					break;
				}
			}
			return false;
		}

		// Token: 0x06008CB1 RID: 36017 RVA: 0x0031DF38 File Offset: 0x0031C138
		public bool IsLedBy(string Blueprint)
		{
			GameObject partyLeader;
			for (GameObject gameObject = this.PartyLeader; gameObject != null; gameObject = partyLeader)
			{
				if (gameObject.Blueprint == Blueprint)
				{
					return true;
				}
				partyLeader = gameObject.PartyLeader;
				if (partyLeader == this.ParentObject)
				{
					MetricsManager.LogError("leader cycle in " + this.ParentObject.DebugName + " + " + gameObject.DebugName);
					break;
				}
			}
			return false;
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x0031DF9C File Offset: 0x0031C19C
		public bool IsPlayerLed()
		{
			GameObject partyLeader;
			for (GameObject gameObject = this.PartyLeader; gameObject != null; gameObject = partyLeader)
			{
				if (gameObject.IsPlayer() || gameObject.LeftBehindByPlayer())
				{
					return true;
				}
				partyLeader = gameObject.PartyLeader;
				if (partyLeader == this.ParentObject)
				{
					MetricsManager.LogError("leader cycle in " + this.ParentObject.DebugName + " + " + gameObject.DebugName);
					break;
				}
			}
			return false;
		}

		// Token: 0x06008CB3 RID: 36019 RVA: 0x0031E004 File Offset: 0x0031C204
		public void AddOpinion<T>(GameObject Subject, float Magnitude = 1f) where T : IOpinionSubject, new()
		{
			OpinionList opinionList;
			if (!this.TryGetOpinions(Subject, out opinionList))
			{
				return;
			}
			XRLGame game = The.Game;
			long num = ((game != null) ? game.TimeTicks : 0L);
			for (int i = opinionList.Count - 1; i >= 0; i--)
			{
				IOpinion opinion = opinionList[i];
				if (opinion.GetType() == typeof(T))
				{
					IOpinionSubject opinionSubject = (IOpinionSubject)opinion;
					if (num - opinionSubject.Time >= (long)opinionSubject.Cooldown)
					{
						opinionList.RemoveAt(i);
						opinionSubject.Time = num;
						opinionSubject.Magnitude = Math.Min(opinionSubject.Limit, opinionSubject.Magnitude + Magnitude);
						opinionSubject.Initialize(this.ParentObject, Subject);
						opinionList.Add(opinionSubject);
						this.AfterAddOpinion(opinionSubject, this.ParentObject, Subject, null, true);
					}
					return;
				}
			}
			T t = new T();
			t.Time = num;
			t.Magnitude = Magnitude;
			t.Initialize(this.ParentObject, Subject);
			opinionList.Add(t);
			this.AfterAddOpinion(t, this.ParentObject, Subject, null, false);
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x0031E128 File Offset: 0x0031C328
		public void AddOpinion<T>(GameObject Subject, GameObject Object, float Magnitude = 1f) where T : IOpinionObject, new()
		{
			OpinionList opinionList;
			if (!this.TryGetOpinions(Subject, out opinionList))
			{
				return;
			}
			XRLGame game = The.Game;
			long num = ((game != null) ? game.TimeTicks : 0L);
			for (int i = opinionList.Count - 1; i >= 0; i--)
			{
				IOpinion opinion = opinionList[i];
				if (opinion.GetType() == typeof(T))
				{
					IOpinionObject opinionObject = (IOpinionObject)opinion;
					if (num - opinionObject.Time >= (long)opinionObject.Cooldown)
					{
						opinionList.RemoveAt(i);
						opinionObject.Time = num;
						opinionObject.Magnitude = Math.Min(opinionObject.Limit, opinionObject.Magnitude + Magnitude);
						opinionObject.Initialize(this.ParentObject, Subject, Object);
						opinionList.Add(opinionObject);
						this.AfterAddOpinion(opinionObject, this.ParentObject, Subject, Object, true);
					}
					return;
				}
			}
			T t = new T();
			t.Time = num;
			t.Magnitude = Magnitude;
			t.Initialize(this.ParentObject, Subject, Object);
			opinionList.Add(t);
			this.AfterAddOpinion(t, this.ParentObject, Subject, Object, false);
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x0031E24C File Offset: 0x0031C44C
		private void AfterAddOpinion(IOpinion Opinion, GameObject Actor, GameObject Subject, GameObject Object = null, bool Renew = false)
		{
			if (!Renew && Opinion.Value < 0)
			{
				int feeling = Actor.Brain.GetFeeling(Subject);
				if (feeling < -10 && feeling - Opinion.Value >= -10)
				{
					base.PlayWorldSound("sfx_creature_angered", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			AfterAddOpinionEvent.Send(Opinion, Actor, Subject, Object, Renew);
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x0031E2BC File Offset: 0x0031C4BC
		public bool TryGetOpinions(GameObject Subject, out OpinionList List)
		{
			if (base.IsPlayer() || Subject == this.ParentObject)
			{
				List = null;
				return false;
			}
			if (!this.Opinions.TryGetValue(Subject.BaseID, out List))
			{
				Dictionary<int, OpinionList> opinions = this.Opinions;
				int baseID = Subject.BaseID;
				OpinionList opinionList;
				List = (opinionList = new OpinionList());
				opinions[baseID] = opinionList;
			}
			return true;
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x0031E310 File Offset: 0x0031C510
		public bool RemoveOpinion<T>(GameObject Object) where T : IOpinion
		{
			OpinionList opinionList;
			if (!this.Opinions.TryGetValue(Object.BaseID, out opinionList))
			{
				return false;
			}
			bool flag = false;
			for (int i = opinionList.Count - 1; i >= 0; i--)
			{
				if (opinionList[i].GetType() == typeof(T))
				{
					opinionList.RemoveAt(i);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x0031E36B File Offset: 0x0031C56B
		public void AdjustFeeling(GameObject GO, int FeelingDelta)
		{
		}

		// Token: 0x06008CB9 RID: 36025 RVA: 0x0031E36D File Offset: 0x0031C56D
		public bool GetAngryAt(GameObject GO, int amount = -50)
		{
			return true;
		}

		// Token: 0x06008CBA RID: 36026 RVA: 0x0031E370 File Offset: 0x0031C570
		public bool LikeBetter(GameObject GO, int amount = 50)
		{
			return true;
		}

		// Token: 0x06008CBB RID: 36027 RVA: 0x0031E374 File Offset: 0x0031C574
		public GameObject GetFinalLeader()
		{
			GameObject gameObject;
			GameObject partyLeader;
			for (gameObject = this.PartyLeader; gameObject != null; gameObject = partyLeader)
			{
				partyLeader = gameObject.PartyLeader;
				if (partyLeader == null)
				{
					break;
				}
				if (partyLeader == this.ParentObject)
				{
					MetricsManager.LogError("leader cycle in " + this.ParentObject.DebugName + " + " + gameObject.DebugName);
					break;
				}
			}
			return gameObject;
		}

		// Token: 0x06008CBC RID: 36028 RVA: 0x0031E3CC File Offset: 0x0031C5CC
		public Brain GetFinalLeaderBrain()
		{
			GameObject gameObject = this.PartyLeader;
			GameObject gameObject2 = gameObject;
			while (gameObject != null)
			{
				GameObject partyLeader = gameObject.PartyLeader;
				if (partyLeader == null)
				{
					break;
				}
				if (partyLeader == this.ParentObject)
				{
					MetricsManager.LogError("leader cycle in " + this.ParentObject.DebugName + " + " + gameObject.DebugName);
					break;
				}
				gameObject = partyLeader;
				if (gameObject.Brain != null)
				{
					gameObject2 = gameObject;
				}
			}
			Brain brain;
			if ((brain = ((gameObject != null) ? gameObject.Brain : null)) == null)
			{
				if (gameObject2 == null)
				{
					return null;
				}
				brain = gameObject2.Brain;
			}
			return brain;
		}

		// Token: 0x06008CBD RID: 36029 RVA: 0x0031E448 File Offset: 0x0031C648
		public bool InSamePartyAs(GameObject who)
		{
			if (who == null)
			{
				return false;
			}
			GameObject finalLeader = this.GetFinalLeader();
			if (finalLeader == null)
			{
				return who.IsLedBy(this.ParentObject);
			}
			return who == finalLeader || who.IsLedBy(finalLeader);
		}

		// Token: 0x06008CBE RID: 36030 RVA: 0x0031E484 File Offset: 0x0031C684
		public bool InSameFactionAs(GameObject Object, Brain.AllegianceLevel Minimum = Brain.AllegianceLevel.Member)
		{
			Brain brain = Object.Brain;
			if (brain == null)
			{
				return false;
			}
			foreach (KeyValuePair<string, int> keyValuePair in this.Allegiance)
			{
				int num;
				if (Brain.GetAllegianceLevel(new int?(keyValuePair.Value)) >= Minimum && brain.Allegiance.TryGetValue(keyValuePair.Key, out num) && Brain.GetAllegianceLevel(new int?(num)) >= Minimum)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008CBF RID: 36031 RVA: 0x0031E520 File Offset: 0x0031C720
		public int? GetPersonalFeeling(GameObject Target)
		{
			OpinionList opinionList;
			if (Target.HasID && this.Opinions.TryGetValue(Target.BaseID, out opinionList))
			{
				return new int?(GetFeelingEvent.GetFor(this.ParentObject, Target, opinionList.Total, null, null, false, true, false));
			}
			return null;
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x0031E570 File Offset: 0x0031C770
		public int GetFeeling(GameObject Target)
		{
			if (Target == null)
			{
				return 0;
			}
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return 0;
			}
			if (parentObject == Target)
			{
				return 100;
			}
			Brain brain = Target.Brain;
			if (brain == null)
			{
				return 0;
			}
			Brain finalLeaderBrain = this.GetFinalLeaderBrain();
			if (finalLeaderBrain != null)
			{
				return GetFeelingEvent.GetFor(parentObject, Target, finalLeaderBrain.GetFeeling(Target), finalLeaderBrain.ParentObject, null, false, false, false);
			}
			if (parentObject.IsPlayer())
			{
				GameObject target = brain.Target;
				if (target != null && (target == parentObject || target.IsPlayerLed()))
				{
					return GetFeelingEvent.GetFor(parentObject, Target, -25, null, null, true, false, false);
				}
			}
			GameObject finalLeader = brain.GetFinalLeader();
			if (finalLeader != null)
			{
				return GetFeelingEvent.GetFor(parentObject, Target, this.GetFeeling(finalLeader), null, finalLeader, false, false, false);
			}
			int num = 0;
			OpinionList opinionList;
			if (Target.HasID && this.Opinions.TryGetValue(Target.BaseID, out opinionList))
			{
				num = GetFeelingEvent.GetFor(parentObject, Target, opinionList.Total, null, null, false, true, false);
			}
			int num2 = this.GetBaseFactionFeeling(Target);
			num2 = GetFeelingEvent.GetFor(parentObject, Target, num2, null, null, false, false, true);
			int num3 = num + num2;
			if (this.Hostile && num3 < 50)
			{
				num3 = Math.Min(num3, -50);
			}
			if (this.Calm && num3 >= -50 && num3 < 0)
			{
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x0031E699 File Offset: 0x0031C899
		public int GetBaseFactionFeeling(GameObject Object)
		{
			return this.Allegiance.GetBaseFeeling(Object, this.FactionFeelings);
		}

		// Token: 0x06008CC2 RID: 36034 RVA: 0x0031E6B0 File Offset: 0x0031C8B0
		public static string GetPrimaryFaction(string FactionSpec)
		{
			int num = -999;
			string text = null;
			Brain.FillFactionMembership(Brain.ProcFactions, FactionSpec);
			foreach (KeyValuePair<string, int> keyValuePair in Brain.ProcFactions)
			{
				if (keyValuePair.Value > num)
				{
					text = keyValuePair.Key;
					num = keyValuePair.Value;
				}
			}
			if (text != null)
			{
				return text;
			}
			if (FactionSpec.IsNullOrEmpty())
			{
				return "Beasts";
			}
			if (FactionSpec.Contains(","))
			{
				return Brain.ExtractFaction(FactionSpec.CachedCommaExpansion()[0]);
			}
			return Brain.ExtractFaction(FactionSpec);
		}

		// Token: 0x06008CC3 RID: 36035 RVA: 0x0031E760 File Offset: 0x0031C960
		public string GetPrimaryFaction(bool Base = false)
		{
			if (this.ParentObject.IsPlayer())
			{
				return "Player";
			}
			StringMap<int> stringMap = (Base ? this.GetBaseAllegiance() : this.Allegiance);
			int num = int.MinValue;
			string text = "Beasts";
			foreach (KeyValuePair<string, int> keyValuePair in stringMap)
			{
				string text2;
				int num2;
				keyValuePair.Deconstruct(out text2, out num2);
				string text3 = text2;
				int num3 = num2;
				if (num3 > num)
				{
					text = text3;
					num = num3;
				}
			}
			return text;
		}

		// Token: 0x06008CC4 RID: 36036 RVA: 0x0031E7F4 File Offset: 0x0031C9F4
		public string GetPrimaryFactionName(bool VisibleOnly = true, bool Formatted = true, bool Base = false)
		{
			StringMap<int> stringMap = (Base ? this.GetBaseAllegiance() : this.Allegiance);
			int num = int.MinValue;
			Faction faction = null;
			foreach (KeyValuePair<string, int> keyValuePair in stringMap)
			{
				string text;
				int num2;
				keyValuePair.Deconstruct(out text, out num2);
				string text2 = text;
				int num3 = num2;
				if (num3 > num)
				{
					Faction ifExists = XRL.World.Factions.GetIfExists(text2);
					if (ifExists != null && (!VisibleOnly || ifExists.Visible))
					{
						faction = ifExists;
						num = num3;
					}
				}
			}
			if (!Formatted)
			{
				if (faction == null)
				{
					return null;
				}
				return faction.Name;
			}
			else
			{
				if (faction == null)
				{
					return null;
				}
				return faction.GetFormattedName();
			}
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x0031E8A4 File Offset: 0x0031CAA4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x0031E8A7 File Offset: 0x0031CAA7
		public bool IsHostileTowards(GameObject Object)
		{
			return this.GetFeelingLevel(Object) == Brain.FeelingLevel.Hostile;
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x0031E8B3 File Offset: 0x0031CAB3
		public bool IsAlliedTowards(GameObject Object)
		{
			return this.GetFeelingLevel(Object) == Brain.FeelingLevel.Allied;
		}

		// Token: 0x06008CC8 RID: 36040 RVA: 0x0031E8BF File Offset: 0x0031CABF
		public bool IsNeutralTowards(GameObject Object)
		{
			return this.GetFeelingLevel(Object) == Brain.FeelingLevel.Neutral;
		}

		// Token: 0x06008CC9 RID: 36041 RVA: 0x0031E8CB File Offset: 0x0031CACB
		public static Brain.FeelingLevel GetFeelingLevel(int Feeling)
		{
			if (Feeling < -10)
			{
				return Brain.FeelingLevel.Hostile;
			}
			if (Feeling >= 50)
			{
				return Brain.FeelingLevel.Allied;
			}
			return Brain.FeelingLevel.Neutral;
		}

		// Token: 0x06008CCA RID: 36042 RVA: 0x0031E8DC File Offset: 0x0031CADC
		public Brain.FeelingLevel GetFeelingLevel(GameObject Object)
		{
			if (Object == null)
			{
				return Brain.FeelingLevel.Neutral;
			}
			int feeling = this.GetFeeling(Object);
			if (feeling < -10)
			{
				return Brain.FeelingLevel.Hostile;
			}
			if (feeling >= 50)
			{
				return Brain.FeelingLevel.Allied;
			}
			return Brain.FeelingLevel.Neutral;
		}

		// Token: 0x06008CCB RID: 36043 RVA: 0x0031E908 File Offset: 0x0031CB08
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<CommandTakeActionEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<GetTradePerformanceEvent>.ID || ID == PooledEvent<GetWaterRitualLiquidEvent>.ID || ID == GetZoneSuspendabilityEvent.ID || ID == GetZoneFreezabilityEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == SuspendingEvent.ID || ID == TookDamageEvent.ID || ID == BeforeDeathRemovalEvent.ID || (ID == TookEnvironmentalDamageEvent.ID && !base.IsPlayer());
		}

		// Token: 0x06008CCC RID: 36044 RVA: 0x0031E9C8 File Offset: 0x0031CBC8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (Options.DebugInternals || Options.DebugAttitude)
			{
				E.AddAction("Show Attitude", "show attitude", "ShowAttitude", null, 'A', false, -1, 500, false, true, false, false, true, null, false);
			}
			if (Options.DebugInternals)
			{
				int num;
				string text = ((this.ParentObject.TryGetIntProperty("ThinkOutLoud", out num) && num != 0) ? "Disable" : "Enable");
				E.AddAction(text + " think out loud", text.ToLower() + " think out loud", "ToggleThinkOutLoud", null, '/', false, -1, 500, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CCD RID: 36045 RVA: 0x0031EA7C File Offset: 0x0031CC7C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ShowAttitude")
			{
				Popup.Show(this.BuildChronology(), null, "Sounds/UI/ui_notification", true, true, true, false, null);
			}
			else if (E.Command == "ToggleThinkOutLoud")
			{
				int num;
				bool flag = this.ParentObject.TryGetIntProperty("ThinkOutLoud", out num) && num != 0;
				this.ParentObject.SetIntProperty("ThinkOutLoud", flag ? 0 : 1, false);
				string text = (flag ? "disable" : "enable");
				base.DidX(text, "thinking out loud", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CCE RID: 36046 RVA: 0x0031EB2C File Offset: 0x0031CD2C
		public string BuildChronology()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ValueTuple<long, string>> list = new List<ValueTuple<long, string>>();
			for (AllegianceSet allegianceSet = this.Allegiance; allegianceSet != null; allegianceSet = allegianceSet.Previous)
			{
				stringBuilder.Clear();
				if (allegianceSet.SourceID == 0)
				{
					stringBuilder.Append("Base allegiance ");
				}
				else
				{
					The.ZoneManager.FindObjectByID(allegianceSet.SourceID);
					stringBuilder.Append(allegianceSet.Reason.GetText(this.ParentObject)).Append(' ');
				}
				allegianceSet.AppendTo(stringBuilder);
				List<ValueTuple<long, string>> list2 = list;
				IAllyReason reason = allegianceSet.Reason;
				list2.Add(new ValueTuple<long, string>((reason != null) ? reason.Time : 0L, stringBuilder.ToString()));
			}
			list.Sort((ValueTuple<long, string> a, ValueTuple<long, string> b) => a.Item1.CompareTo(b.Item1));
			stringBuilder.Clear();
			if (this.PartyLeader.IsValid())
			{
				stringBuilder.Append("---- Leader ---- ");
				stringBuilder.Append("\n    -- ").Append(this.PartyLeader.BaseDisplayNameStripped);
				stringBuilder.Append("\n Feeling (").Append(this.PartyLeader.Brain.GetFeeling(The.Player)).Append(')');
			}
			stringBuilder.Compound("---- Allegiances ---- ", "\n\n");
			foreach (ValueTuple<long, string> valueTuple in list)
			{
				if (valueTuple.Item1 > 0L)
				{
					stringBuilder.Append("\nOn the ").Append(Calendar.GetDay(valueTuple.Item1)).Append(" of ")
						.Append(Calendar.GetMonth(valueTuple.Item1))
						.Append(' ');
				}
				else
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(valueTuple.Item2);
			}
			stringBuilder.Append("\n\n---- Opinions ---- ");
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (KeyValuePair<int, OpinionList> keyValuePair in this.Opinions)
			{
				int num;
				OpinionList opinionList;
				keyValuePair.Deconstruct(out num, out opinionList);
				int num2 = num;
				Container<IOpinion> container = opinionList;
				List<ValueTuple<long, string>> list3 = new List<ValueTuple<long, string>>();
				GameObject gameObject = The.ZoneManager.FindObjectByID(num2);
				if (gameObject != null)
				{
					stringBuilder.Append("\n    -- ").Append(gameObject.BaseDisplayNameStripped).Append('\n');
					stringBuilder.Append("    Reputation (").Append(this.GetBaseFactionFeeling(gameObject)).Append(")");
				}
				else
				{
					stringBuilder.Append("\n    -- ID #").Append(num2);
				}
				foreach (IOpinion opinion in container)
				{
					stringBuilder2.Clear();
					stringBuilder2.Append(opinion.GetText(this.ParentObject)).Append(" (").Append(opinion.Value)
						.Append(')');
					list3.Add(new ValueTuple<long, string>(opinion.Time, stringBuilder2.ToString()));
				}
				list3.Sort((ValueTuple<long, string> a, ValueTuple<long, string> b) => a.Item1.CompareTo(b.Item1));
				foreach (ValueTuple<long, string> valueTuple2 in list3)
				{
					stringBuilder.Append("\n    [").Append(valueTuple2.Item1).Append("] ")
						.Append(valueTuple2.Item2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008CCF RID: 36047 RVA: 0x0031EF34 File Offset: 0x0031D134
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Actor.IsPlayer() && Options.AutoexploreAttackIgnoredAdjacentEnemies && !AutoAct.IsGathering(E.Setting, null) && this.ParentObject.isAdjacentTo(E.Actor) && this.ParentObject.IsReal && this.ParentObject.HasStat("Hitpoints") && E.Actor.IsIrrelevantHostile(this.ParentObject) && E.Actor.PhaseAndFlightMatches(this.ParentObject) && (FungalVisionary.VisionLevel > 0 || !this.ParentObject.HasPart<FungalVision>() || E.Actor.HasPart<FungalVision>()))
			{
				E.Command = "Attack";
				E.AllowRetry = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD0 RID: 36048 RVA: 0x0031F000 File Offset: 0x0031D200
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!The.ActionManager.ActionQueue.Contains(this.ParentObject) && E.Cell.ParentZone.IsActive())
			{
				this.ParentObject.MakeActive(false);
			}
			if (!this.Wanders && !this.WandersRandomly && this.StartingCell.IsNullOrEmpty() && this.IsMobile() && !this.ParentObject.HasPropertyOrTag("NoStay"))
			{
				if (this.StartingCell == null)
				{
					this.StartingCell = new GlobalLocation();
				}
				this.StartingCell.SetCell(this.ParentObject.CurrentCell);
			}
			if (this.DoReequip)
			{
				this.DoReequip = false;
				this.PerformEquip(true, this.DoPrimaryChoiceOnReequip, null);
				this.DoPrimaryChoiceOnReequip = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD1 RID: 36049 RVA: 0x0031F0D0 File Offset: 0x0031D2D0
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (GameObject.Validate(E.ForLeader))
				{
					E.AddStatus(E.ForLeader.DistanceTo(this.ParentObject).Things("square", null) + " away", -100);
				}
				if (this.Staying)
				{
					E.AddStatus("staying put", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD2 RID: 36050 RVA: 0x0031F144 File Offset: 0x0031D344
		public override bool HandleEvent(GetTradePerformanceEvent E)
		{
			if (E.Actor != null && E.Actor.IsPlayer() && E.Trader != null)
			{
				Faction ifExists = XRL.World.Factions.GetIfExists(E.Trader.GetPrimaryFaction(false));
				if (ifExists != null)
				{
					E.LinearAdjustment += (double)The.Game.PlayerReputation.GetTradePerformance(ifExists);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD3 RID: 36051 RVA: 0x0031F1A8 File Offset: 0x0031D3A8
		public override bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			Faction ifExists = XRL.World.Factions.GetIfExists(this.GetPrimaryFaction(true));
			string text = ((ifExists != null) ? ifExists.GetWaterRitualLiquid(E.Target) : null);
			if (!text.IsNullOrEmpty() && (!(text == "water") || E.Liquid.IsNullOrEmpty()))
			{
				E.Liquid = text;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD4 RID: 36052 RVA: 0x0031F204 File Offset: 0x0031D404
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			if (!this.WallWalker && this.IsTryingToJoinPartyLeader())
			{
				E.Suspendability = Suspendability.CompanionTryingToJoinPartyLeader;
				return false;
			}
			if (this.IsPlayerLed() && The.Player.OnWorldMap())
			{
				E.Suspendability = Suspendability.CompanionWhilePlayerIsOnWorldMap;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD5 RID: 36053 RVA: 0x0031F243 File Offset: 0x0031D443
		public override bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			if (!this.WallWalker && this.IsTryingToJoinPartyLeader())
			{
				E.Freezability = Freezability.CompanionTryingToJoinPartyLeader;
				return false;
			}
			if (this.IsPlayerLed() && The.Player.OnWorldMap())
			{
				E.Freezability = Freezability.CompanionWhilePlayerIsOnWorldMap;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD6 RID: 36054 RVA: 0x0031F284 File Offset: 0x0031D484
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object != this.ParentObject || E.Object.IsNowhere())
			{
				return base.HandleEvent(E);
			}
			this.Wake();
			if (E.Actor == null || E.Actor == this.ParentObject || this.InSamePartyAs(E.Actor))
			{
				return base.HandleEvent(E);
			}
			if (this.ParentObject.HasRegisteredEvent("CanBeAngeredByDamage") && !this.ParentObject.FireEventDirect(Event.New("CanBeAngeredByDamage", "Attacker", E.Actor, "Damage", E.Damage)))
			{
				return base.HandleEvent(E);
			}
			if (E.Damage != null && E.Damage.HasAttribute("Accidental"))
			{
				bool flag = E.Damage != null && E.Damage.Amount > 0;
				if (!this.FriendlyFireIncident(E.Actor, flag))
				{
					return base.HandleEvent(E);
				}
			}
			this.Attacked(E.Actor, E.Weapon, 1f);
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD7 RID: 36055 RVA: 0x0031F39C File Offset: 0x0031D59C
		public void Attacked(GameObject Attacker, GameObject Weapon = null, float Magnitude = 1f)
		{
			if (base.IsPlayer())
			{
				AIHelpBroadcastEvent.Send(this.ParentObject, Attacker, null, null, 20, 1f, HelpCause.Assault);
				return;
			}
			if (!this.IsHostileTowards(Attacker))
			{
				this.AddOpinion<OpinionAttack>(Attacker, Weapon, Magnitude);
				AIHelpBroadcastEvent.Send(this.ParentObject, Attacker, null, null, 20, 1f, HelpCause.Assault);
			}
			if (this.Target == null && this.CanFight() && this.IsHostileTowards(Attacker))
			{
				this.WantToKill(Attacker, "because I was attacked", false);
			}
		}

		// Token: 0x06008CD8 RID: 36056 RVA: 0x0031F418 File Offset: 0x0031D618
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (E.Killer != null && E.Killer != this.ParentObject && !E.Killer.InSamePartyAs(this.ParentObject))
			{
				OpinionList opinionList;
				if (this.ParentObject.Brain.TryGetOpinions(E.Killer, out opinionList))
				{
					if (opinionList.Find((IOpinion x) => x is OpinionAttack) != null)
					{
						AIHelpBroadcastEvent.Send(this.ParentObject, E.Killer, null, null, 20, 1f, HelpCause.Murder);
						goto IL_00A8;
					}
				}
				AIHelpBroadcastEvent.Send(this.ParentObject, E.Killer, null, null, 20, 1f, HelpCause.Killed);
			}
			IL_00A8:
			return base.HandleEvent(E);
		}

		// Token: 0x06008CD9 RID: 36057 RVA: 0x0031F4D4 File Offset: 0x0031D6D4
		public override bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			if (!base.IsPlayer() && this.MovingTo() == null && (E.Damage.Amount > 5 || Stat.Random(1, 5) <= E.Damage.Amount || this.ParentObject.hitpoints <= 10 || this.ParentObject.isDamaged(0.1, false)) && (!this.IsHostileTowards(The.Player) || Stat.Random(15, 45) < this.ParentObject.Stat("Intelligence", 0) + E.Damage.Amount))
			{
				this.PushGoal(new FleeLocation(this.ParentObject.CurrentCell, 1, false, false));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDA RID: 36058 RVA: 0x0031F595 File Offset: 0x0031D795
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.StopFighting(false);
			this.FriendlyFire = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDB RID: 36059 RVA: 0x0031F5AC File Offset: 0x0031D7AC
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			E.AddEntry(this, "PartyLeader", this.PartyLeader);
			E.AddEntry(this, "Wanders", this.Wanders);
			E.AddEntry(this, "WandersRandomly", this.WandersRandomly);
			if (this.Goals.Count > 0)
			{
				stringBuilder.Clear();
				for (int i = this.Goals.Count - 1; i > 0; i--)
				{
					int num = 1;
					string description = this.Goals.Items[i].GetDescription();
					while (i > 0 && this.Goals.Items[i - 1].GetDescription() == description)
					{
						num++;
						i--;
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('\n');
					}
					stringBuilder.Append(description);
					if (num > 1)
					{
						stringBuilder.Append(" x").Append(num);
					}
				}
				E.AddEntry(this, "Goals", stringBuilder.ToString());
			}
			else
			{
				E.AddEntry(this, "Goals", "none");
			}
			E.AddEntry(this, "MinKillRadius", this.MinKillRadius);
			E.AddEntry(this, "MaxKillRadius", this.MaxKillRadius);
			E.AddEntry(this, "NeedToReload", this.NeedToReload);
			E.AddEntry(this, "Hostile walk radius to player", this.ParentObject.GetHostileWalkRadius(The.Player));
			E.AddEntry(this, "Last thought", this.LastThought.IsNullOrEmpty() ? "none" : this.LastThought);
			if (this.Allegiance.Count > 0)
			{
				stringBuilder.Clear();
				List<string> list = new List<string>(this.Allegiance.Count);
				foreach (KeyValuePair<string, int> keyValuePair in this.Allegiance)
				{
					list.Add(keyValuePair.Key);
				}
				list.Sort();
				int j = 0;
				int count = list.Count;
				while (j < count)
				{
					if (j > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(list[j]).Append('-').Append(this.Allegiance[list[j]]);
					j++;
				}
				E.AddEntry(this, "Faction membership", stringBuilder.ToString());
			}
			else
			{
				E.AddEntry(this, "Faction membership", "none");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDC RID: 36060 RVA: 0x0031F844 File Offset: 0x0031DA44
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Hostile)
			{
				E.Postfix.Append("\nBase demeanor: {{r|aggressive}}");
			}
			if (this.Calm)
			{
				E.Postfix.Append("\nBase demeanor: {{g|docile}}");
			}
			if (this.Passive)
			{
				E.Postfix.Append("\nEngagement style: {{g|defensive}}");
			}
			else if (this.IsPlayerLed())
			{
				E.Postfix.Append("\nEngagement style: {{r|aggressive}}");
			}
			if (this.Target != null && this.IsPlayerLed())
			{
				E.Postfix.Append("\nFighting {{r|" + this.Target.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDD RID: 36061 RVA: 0x0031F914 File Offset: 0x0031DB14
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Hibernating && this.ParentObject.GetIntProperty("StartNotHibernatingChanceIn100", 50).in100())
			{
				this.Hibernating = false;
			}
			this.ParentObject.FireEvent(Brain.eFactionsAdded);
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDE RID: 36062 RVA: 0x0031F964 File Offset: 0x0031DB64
		public override bool HandleEvent(SuspendingEvent E)
		{
			this.FriendlyFire = null;
			if (this.IsMobile() && this.PartyLeader != null && this.PartyLeader.IsPlayer())
			{
				if (!this.PartyLeader.InSameZone(this.ParentObject))
				{
					this.Goals.Clear();
				}
			}
			else
			{
				this.Goals.Clear();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CDF RID: 36063 RVA: 0x0031F9C8 File Offset: 0x0031DBC8
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			bool flag;
			using (Brain.s_BrainCommandTakeActionEvent.Auto())
			{
				Brain.objectsToRemove.Clear();
				if (this.FriendlyFire != null)
				{
					foreach (GameObject gameObject in this.FriendlyFire.Keys)
					{
						Zone currentZone = gameObject.CurrentZone;
						if (currentZone == null || !currentZone.IsActive())
						{
							Brain.objectsToRemove.Add(gameObject);
						}
					}
					if (Brain.objectsToRemove.Count > 0)
					{
						foreach (GameObject gameObject2 in Brain.objectsToRemove)
						{
							this.FriendlyFire.Remove(gameObject2);
						}
						Brain.objectsToRemove.Clear();
						if (this.FriendlyFire.Count == 0)
						{
							this.FriendlyFire = null;
						}
					}
				}
				if (this.IsPlayerLed())
				{
					this.ParentObject.ModIntProperty("TurnsAsPlayerMinion", 1, false);
				}
				if (this.ParentObject.IsInvalid())
				{
					flag = true;
				}
				else if (this.ParentObject.IsNowhere())
				{
					this.ParentObject.UseEnergy(1000);
					flag = true;
				}
				else if (!BeforeAITakingActionEvent.Check(this.ParentObject))
				{
					flag = true;
				}
				else
				{
					if (this.ParentObject.IsPlayer())
					{
						if (Options.DisablePlayerbrain)
						{
							return true;
						}
					}
					else if (The.Core.Calm)
					{
						this.ParentObject.UseEnergy(1000);
						return true;
					}
					if (this.PartyLeader != null && this.PartyLeader.HasRegisteredEvent("MinionTakingAction"))
					{
						this.PartyLeader.FireEvent(Event.New("MinionTakingAction", "Object", this.ParentObject));
					}
					if (!this.ParentObject.IsPlayer())
					{
						if (this.GoToPartyLeader())
						{
							return true;
						}
						if (this.Hibernating)
						{
							this.ParentObject.UseEnergy(1000);
							return true;
						}
						if (this.ParentObject.IsFrozen())
						{
							this.Think("I'm frozen!");
							this.ParentObject.UseEnergy(1000);
							return true;
						}
						this.ParentObject.FireEvent(Brain.eAITakingAction);
						Cell currentCell = this.ParentObject.CurrentCell;
						if (this.ParentObject.HasEffect<XRL.World.Effects.Confused>())
						{
							string randomDirection = Directions.GetRandomDirection();
							Cell localCellFromDirection = currentCell.GetLocalCellFromDirection(randomDirection, true);
							if (localCellFromDirection != null)
							{
								if (this.LimitToAquatic() && !localCellFromDirection.HasAquaticSupportFor(this.ParentObject))
								{
									this.ParentObject.UseEnergy(1000);
									return true;
								}
								if (!localCellFromDirection.IsEmpty())
								{
									this.ParentObject.UseEnergy(1000);
									return true;
								}
							}
							this.ParentObject.Move(randomDirection, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
							return true;
						}
						if (this.DoReequip)
						{
							this.DoReequip = false;
							this.PerformReequip(false, this.DoPrimaryChoiceOnReequip, false, null);
							this.DoPrimaryChoiceOnReequip = true;
						}
						while (this.Goals.Count > 0 && this.Goals.Peek().Finished())
						{
							this.Goals.Pop();
						}
						if (this.Target == null && this.PartyLeader != null && this.PartyLeader.IsPlayerControlled() && this.CanAcquireTarget())
						{
							GameObject target = this.PartyLeader.Target;
							if (target != null && this.CheckPerceptionOf(target))
							{
								this.WantToKill(target, "to aid my leader", false);
							}
						}
						if (this.Target == null && currentCell != null)
						{
							bool flag2 = !this.IsPlayerLed();
							GameObject gameObject3 = this.FindProspectiveTarget(null, flag2);
							if (gameObject3 != null)
							{
								this.WantToKill(gameObject3, "out of hostility", false);
							}
							else if (flag2)
							{
								this.Think("I looked for the player to target but didn't find them.");
							}
							else
							{
								this.Think("I looked for a target but didn't find one.");
							}
						}
						if (this.Goals.Count == 0)
						{
							new Bored().Push(this);
						}
					}
					this.ParentObject.FireEvent(Brain.eTakingAction);
					while (this.Goals.Count > 0 && this.Goals.Peek().Finished())
					{
						this.Goals.Pop();
					}
					if (this.Goals.Count > 0)
					{
						this.Goals.Peek().TakeAction();
						GameObject parentObject = this.ParentObject;
						if (parentObject != null && parentObject.IsPlayer())
						{
							The.Core.RenderDelay(200, true, true, false);
						}
					}
					flag = base.HandleEvent(E);
				}
			}
			return flag;
		}

		// Token: 0x06008CE0 RID: 36064 RVA: 0x0031FED0 File Offset: 0x0031E0D0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06008CE1 RID: 36065 RVA: 0x0031FED3 File Offset: 0x0031E0D3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIMessage");
			Registrar.Register("AIWakeupBroadcast");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008CE2 RID: 36066 RVA: 0x0031FEF4 File Offset: 0x0031E0F4
		public void Stay(Cell C)
		{
			if (C == null)
			{
				this.Staying = false;
				return;
			}
			if (this.IsMobile() && !this.Wanders && !this.WandersRandomly)
			{
				if (this.StartingCell == null)
				{
					this.StartingCell = new GlobalLocation();
				}
				this.StartingCell.SetCell(C);
			}
			this.Staying = true;
		}

		// Token: 0x06008CE3 RID: 36067 RVA: 0x0031FF4A File Offset: 0x0031E14A
		public void FleeTo(Cell Cell, int Duration = 3)
		{
			this.PushGoal(new FleeLocation(Cell, Duration, false, false));
		}

		// Token: 0x06008CE4 RID: 36068 RVA: 0x0031FF5C File Offset: 0x0031E15C
		public void MoveTo(Cell Cell, bool ClearFirst = true)
		{
			this.MoveTo(Cell.ParentZone, Cell.Location, ClearFirst);
		}

		// Token: 0x06008CE5 RID: 36069 RVA: 0x0031FF74 File Offset: 0x0031E174
		public void MoveTo(Zone Zone, Location2D Location, bool ClearFirst = true)
		{
			if (ClearFirst)
			{
				this.Goals.Clear();
			}
			this.PushGoal(new MoveTo(Zone.GetCell(Location), false, false, 0, false, false, false, -1, -1, 95));
		}

		// Token: 0x06008CE6 RID: 36070 RVA: 0x0031FFAC File Offset: 0x0031E1AC
		public void MoveTo(GameObject Object, bool ClearFirst = true)
		{
			if (ClearFirst)
			{
				this.Goals.Clear();
			}
			this.PushGoal(new MoveTo(Object, false, false, 0, false, false, false, -1, -1, 95));
		}

		// Token: 0x06008CE7 RID: 36071 RVA: 0x0031FFDE File Offset: 0x0031E1DE
		public void MoveToGlobal(string Zone, int X, int Y)
		{
			this.Goals.Clear();
			this.PushGoal(new MoveToGlobal(Zone, X, Y));
		}

		// Token: 0x06008CE8 RID: 36072 RVA: 0x0031FFFC File Offset: 0x0031E1FC
		public void StopMoving()
		{
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				GoalHandler goalHandler = this.Goals.Items[i] as IMovementGoal;
				if (goalHandler != null)
				{
					int count = this.Goals.Items.Count;
					goalHandler.FailToParent();
					if (count != this.Goals.Items.Count)
					{
						i = this.Goals.Items.Count;
					}
				}
			}
		}

		// Token: 0x06008CE9 RID: 36073 RVA: 0x0032007C File Offset: 0x0031E27C
		public bool IsTryingToJoinPartyLeader()
		{
			GameObject partyLeader = this.PartyLeader;
			if (partyLeader == null || partyLeader.IsNowhere())
			{
				return false;
			}
			if (this.Staying)
			{
				return false;
			}
			if (this.ParentObject.HasTagOrProperty("DoesNotJoinPartyLeader") || this.ParentObject.IsPlayer())
			{
				return false;
			}
			Cell currentCell = partyLeader.CurrentCell;
			Cell currentCell2 = this.ParentObject.CurrentCell;
			bool flag;
			return currentCell != null && currentCell2 != null && currentCell.ParentZone != null && currentCell2.ParentZone != null && currentCell.ParentZone != currentCell2.ParentZone && !currentCell.ParentZone.IsWorldMap() && (!The.ZoneManager.TryGetZoneProperty<bool>(currentCell.ParentZone.ZoneID, "JoinPartyLeaderPossible", out flag) || flag) && JoinPartyLeaderPossibleEvent.Check(this.ParentObject, partyLeader, currentCell2, ref currentCell, this.ParentObject.IsPotentiallyMobile());
		}

		// Token: 0x06008CEA RID: 36074 RVA: 0x00320154 File Offset: 0x0031E354
		public bool GoToPartyLeader()
		{
			GameObject partyLeader = this.PartyLeader;
			if (partyLeader == null || partyLeader.IsNowhere())
			{
				return false;
			}
			if (this.Staying)
			{
				return false;
			}
			if (this.ParentObject.HasEffect<Dominated>() || this.ParentObject.IsPlayer())
			{
				return false;
			}
			Cell currentCell = partyLeader.CurrentCell;
			Cell currentCell2 = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null || (currentCell2 != null && currentCell.ParentZone == currentCell2.ParentZone))
			{
				return false;
			}
			if (currentCell.ParentZone.IsWorldMap())
			{
				return false;
			}
			bool flag;
			if (The.ZoneManager.TryGetZoneProperty<bool>(currentCell.ParentZone.ZoneID, "JoinPartyLeaderPossible", out flag) && !flag)
			{
				return false;
			}
			bool flag2 = this.ParentObject.HasTagOrProperty("DoesNotJoinPartyLeader");
			bool flag3 = false;
			if (!flag2 && JoinPartyLeaderPossibleEvent.Check(this.ParentObject, partyLeader, currentCell2, ref currentCell, this.ParentObject.IsMobile()))
			{
				this.Goals.Clear();
				if (!currentCell.ParentZone.IsWorldMap())
				{
					List<Cell> list = currentCell.GetPassableConnectedAdjacentCellsFor(this.ParentObject, 3, null, true, true).ShuffleInPlace<Cell>(null);
					if (currentCell.IsPassable(this.ParentObject, true))
					{
						list.Insert(0, currentCell);
					}
					Cell cell = null;
					int num = int.MaxValue;
					int num2 = 0;
					int num3 = 0;
					foreach (Cell cell2 in list)
					{
						if (cell2.GetNavigationWeightFor(this.ParentObject, false, false, false, false, false) < 10 && !cell2.HasCombatObject(false) && !cell2.IsSolidFor(this.ParentObject, false))
						{
							int num4 = ((currentCell2 != null) ? currentCell2.PathDistanceTo(cell2) : 0);
							int num5 = cell2.PathDistanceTo(currentCell);
							int num6 = num4 + num5;
							if (num6 < num && CanJoinPartyLeaderEvent.Check(this.ParentObject, partyLeader, currentCell2, cell2, num4, num5))
							{
								cell = cell2;
								num = num6;
								num2 = num4;
								num3 = num5;
							}
						}
					}
					if (cell != null)
					{
						this.Think("I'm going to join my leader.");
						this.ParentObject.SystemLongDistanceMoveTo(cell, null, false, true);
						this.ParentObject.UseEnergy(1000, "Move Join Leader", null, null, false);
						JoinedPartyLeaderEvent.Send(this.ParentObject, partyLeader, currentCell2, cell, num2, num3);
						flag3 = true;
					}
				}
			}
			return flag3;
		}

		// Token: 0x06008CEB RID: 36075 RVA: 0x003203B4 File Offset: 0x0031E5B4
		public void BroadcastForHelp(GameObject Target)
		{
			AIHelpBroadcastEvent.Send(this.ParentObject, Target, null, null, 20, 1f, HelpCause.General);
		}

		// Token: 0x06008CEC RID: 36076 RVA: 0x003203CC File Offset: 0x0031E5CC
		public override bool Render(RenderEvent E)
		{
			if (this.Hibernating && !E.NoWake)
			{
				this.Wake();
			}
			return true;
		}

		// Token: 0x06008CED RID: 36077 RVA: 0x003203E8 File Offset: 0x0031E5E8
		public void Wake()
		{
			if (this.Hibernating)
			{
				this.Hibernating = false;
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && currentCell.ParentZone != null)
				{
					Event @event = Event.New("AIWakeupBroadcast", 0, 0, 0);
					List<GameObject> list = currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 5, "Brain", this.ParentObject, null, null);
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						list[i].FireEvent(@event);
						i++;
					}
				}
			}
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x00320474 File Offset: 0x0031E674
		public static Brain.AllegianceLevel GetAllegianceLevel(int? Membership)
		{
			if (Membership != null)
			{
				int? num = Membership;
				int num2 = 75;
				if ((num.GetValueOrDefault() >= num2) & (num != null))
				{
					return Brain.AllegianceLevel.Member;
				}
				num = Membership;
				num2 = 50;
				if ((num.GetValueOrDefault() >= num2) & (num != null))
				{
					return Brain.AllegianceLevel.Affiliated;
				}
				num = Membership;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					return Brain.AllegianceLevel.Associated;
				}
			}
			return Brain.AllegianceLevel.None;
		}

		// Token: 0x06008CEF RID: 36079 RVA: 0x003204E4 File Offset: 0x0031E6E4
		public static Brain.AllegianceLevel GetAllegianceLevel(IDictionary<string, int> Membership, string Faction)
		{
			int num;
			if (Membership.TryGetValue(Faction, out num))
			{
				return Brain.GetAllegianceLevel(new int?(num));
			}
			return Brain.AllegianceLevel.None;
		}

		// Token: 0x06008CF0 RID: 36080 RVA: 0x00320509 File Offset: 0x0031E709
		public Brain.AllegianceLevel GetAllegianceLevel(string Faction)
		{
			return Brain.GetAllegianceLevel(this.Allegiance, Faction);
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x00320518 File Offset: 0x0031E718
		public bool CheckVisibilityOf(GameObject who)
		{
			if (this.ParentObject.IsPlayer())
			{
				if (who.IsVisible())
				{
					return true;
				}
			}
			else
			{
				if (this.ParentObject.HasPart<Clairvoyance>() && this.ParentObject.Stat("Intelligence", 0).in100())
				{
					return true;
				}
				if (this.ParentObject.HasLOSTo(who, true, false, true, true, null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008CF2 RID: 36082 RVA: 0x00320579 File Offset: 0x0031E779
		public bool CheckPerceptionOf(GameObject Object)
		{
			return this.CheckVisibilityOf(Object) || (Object.IsInActiveZone() && Object.IsAudible(this.ParentObject, 20)) || (Object.IsInActiveZone() && Object.IsSmellable(this.ParentObject));
		}

		// Token: 0x06008CF3 RID: 36083 RVA: 0x003205BC File Offset: 0x0031E7BC
		public bool IsSuitableTarget(GameObject Object)
		{
			if (this.ParentObject.IsPlayerControlled())
			{
				if (Object.IsPlayerControlled())
				{
					return false;
				}
				if (Object.HasEffect<Asleep>())
				{
					return false;
				}
				if (Object.IsHostileTowards(The.Player) && this.CheckPerceptionOf(Object))
				{
					return true;
				}
			}
			else if (this.IsHostileTowards(Object) && this.CheckPerceptionOf(Object))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06008CF4 RID: 36084 RVA: 0x00320617 File Offset: 0x0031E817
		public bool IsSuitablePlayerControlledTarget(GameObject Object)
		{
			return Object.IsPlayerControlled() && (this.IsHostileTowards(Object) && this.CheckPerceptionOf(Object));
		}

		// Token: 0x06008CF5 RID: 36085 RVA: 0x00320638 File Offset: 0x0031E838
		public int TargetSort(GameObject Target1, GameObject Target2)
		{
			int num = PreferTargetEvent.Check(this.ParentObject, Target1, Target2);
			if (num != 0)
			{
				return -num;
			}
			int num2 = Target1.IsPlayer().CompareTo(Target2.IsPlayer());
			if (num2 != 0)
			{
				return -num2;
			}
			int num3 = Target1.PhaseMatches(this.ParentObject).CompareTo(Target2.PhaseMatches(this.ParentObject));
			if (num3 != 0)
			{
				return -num3;
			}
			int num4 = Target1.FlightMatches(this.ParentObject).CompareTo(Target2.FlightMatches(this.ParentObject));
			if (num4 != 0)
			{
				return -num4;
			}
			int num5 = Target1.DistanceTo(this.ParentObject).CompareTo(Target2.DistanceTo(this.ParentObject));
			if (num5 != 0)
			{
				return num5;
			}
			return 0;
		}

		// Token: 0x06008CF6 RID: 36086 RVA: 0x003206F0 File Offset: 0x0031E8F0
		public static int ExtractFactionMembership(ref string spec)
		{
			int num = spec.LastIndexOf('-');
			if (num == -1)
			{
				MetricsManager.LogError("Invalid faction membership specification: " + spec);
				return 0;
			}
			int num2;
			if (!int.TryParse(spec.Substring(num + 1), out num2) || num2 <= 0)
			{
				MetricsManager.LogError("Invalid faction membership specification: " + spec);
				return 0;
			}
			spec = spec.Substring(0, num);
			return num2;
		}

		// Token: 0x06008CF7 RID: 36087 RVA: 0x00320754 File Offset: 0x0031E954
		public static int ExtractFactionMembership(string spec)
		{
			return Brain.ExtractFactionMembership(ref spec);
		}

		// Token: 0x06008CF8 RID: 36088 RVA: 0x0032075D File Offset: 0x0031E95D
		public static string ExtractFaction(string spec)
		{
			Brain.ExtractFactionMembership(ref spec);
			return spec;
		}

		// Token: 0x06008CF9 RID: 36089 RVA: 0x00320768 File Offset: 0x0031E968
		public static void FillFactionMembership(IDictionary<string, int> Map, string Spec)
		{
			AllegianceSet allegianceSet = Map as AllegianceSet;
			if (allegianceSet != null)
			{
				allegianceSet.ClearSlots();
			}
			else
			{
				Map.Clear();
			}
			if (!Spec.IsNullOrEmpty())
			{
				if (Spec.Contains(","))
				{
					using (List<string>.Enumerator enumerator = Spec.CachedCommaExpansion().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							int num = Brain.ExtractFactionMembership(ref text);
							if (num > 0)
							{
								int num2;
								if (Map.TryGetValue(text, out num2))
								{
									if (num > num2)
									{
										Map[text] = num;
									}
								}
								else
								{
									Map.Add(text, num);
								}
							}
						}
						return;
					}
				}
				int num3 = Brain.ExtractFactionMembership(ref Spec);
				if (num3 > 0)
				{
					int num4;
					if (Map.TryGetValue(Spec, out num4))
					{
						if (num3 > num4)
						{
							Map[Spec] = num3;
							return;
						}
					}
					else
					{
						Map.Add(Spec, num3);
					}
				}
			}
		}

		// Token: 0x06008CFA RID: 36090 RVA: 0x00320844 File Offset: 0x0031EA44
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AIWakeupBroadcast")
			{
				this.Hibernating = false;
			}
			else if (E.ID == "AIMessage")
			{
				if (this.ParentObject.IsNowhere())
				{
					return true;
				}
				if (this.ParentObject.IsPlayer())
				{
					return true;
				}
				this.Wake();
				if (E.GetStringParameter("Message", null) == "Attacked")
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("By");
					if (gameObjectParameter != null && gameObjectParameter != this.ParentObject && !this.InSamePartyAs(gameObjectParameter) && (gameObjectParameter.IsCreature || 3.in100()) && this.ParentObject.FireEvent(Event.New("CanBeAngeredByBeingAttacked", "Attacker", gameObjectParameter)))
					{
						bool flag = E.HasFlag("Accidental");
						if (flag && !this.FriendlyFireIncident(gameObjectParameter, false))
						{
							return true;
						}
						if (this.IsPlayerLed())
						{
							if (!gameObjectParameter.IsPlayerControlled() && this.Target == null)
							{
								if (flag)
								{
									this.WantToKill(gameObjectParameter, "because I was accidentally attacked", false);
								}
								else
								{
									this.WantToKill(gameObjectParameter, "because I was attacked", false);
								}
							}
						}
						else
						{
							this.Attacked(gameObjectParameter, null, 1f);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008CFB RID: 36091 RVA: 0x00320984 File Offset: 0x0031EB84
		public override bool HandleEvent(AIHelpBroadcastEvent E)
		{
			if (!GameObject.Validate(this.ParentObject))
			{
				return true;
			}
			if (this.ParentObject.IsPlayer() || this.ParentObject == E.Actor)
			{
				return true;
			}
			if (E.Target == null || this.Target != null)
			{
				return true;
			}
			Asleep asleep;
			if (this.ParentObject.TryGetEffect<Asleep>(out asleep) && !this.ParentObject.HasTag("NoHelpBroadcastWake"))
			{
				this.ParentObject.FireEvent("WakeUp");
				return true;
			}
			if (E.Target.Brain != null)
			{
				if (this.InSamePartyAs(E.Target))
				{
					return true;
				}
				if (this.InSameFactionAs(E.Target, Brain.AllegianceLevel.Member))
				{
					return true;
				}
			}
			bool flag = this.ParentObject.DistanceTo(E.Target) <= this.MaxKillRadius && this.Target == null && this.CanFight() && (!this.Passive || !this.HasGoal());
			int feeling = this.GetFeeling(E.Actor);
			if (E.Cause == HelpCause.Assault)
			{
				if (feeling >= 50 && feeling >= this.GetFeeling(E.Target))
				{
					if (!this.IsHostileTowards(E.Target))
					{
						this.AddOpinion<OpinionAttackAlly>(E.Target, E.Actor, E.Magnitude);
					}
					if (flag && this.IsHostileTowards(E.Target))
					{
						this.WantToKill(E.Target, "to help " + E.Actor.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false), false);
						return true;
					}
				}
			}
			else if (E.Cause == HelpCause.Murder || E.Cause == HelpCause.Killed)
			{
				if (feeling >= 50 && ((E.Cause == HelpCause.Murder && feeling >= this.GetFeeling(E.Target)) || this.InSamePartyAs(E.Actor) || this.InSameFactionAs(E.Actor, Brain.AllegianceLevel.Member)))
				{
					if (!this.IsHostileTowards(E.Target))
					{
						this.AddOpinion<OpinionKilledAlly>(E.Target, E.Actor, E.Magnitude);
					}
					if (flag && this.IsHostileTowards(E.Target))
					{
						this.WantToKill(E.Target, "to avenge " + E.Actor.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false), false);
						return true;
					}
				}
			}
			else
			{
				foreach (string text in E.Factions)
				{
					if (this.GetAllegianceLevel(text) >= Brain.AllegianceLevel.Affiliated)
					{
						if (this.ParentObject.HasRegisteredEvent("CanBeAngeredByPropertyCrime"))
						{
							Event @event = Event.New("CanBeAngeredByPropertyCrime", "Attacker", E.Target, "Object", E.Actor, "Faction", text);
							if (!this.ParentObject.FireEventDirect(@event))
							{
								return false;
							}
						}
						if (E.Cause == HelpCause.Theft)
						{
							this.AddOpinion<OpinionThief>(E.Target, E.Item, E.Magnitude);
						}
						else if (E.Cause == HelpCause.Trespass)
						{
							this.AddOpinion<OpinionTrespass>(E.Target, E.Magnitude);
						}
						if (flag && this.IsHostileTowards(E.Target))
						{
							this.WantToKill(E.Target, (E.Actor != null) ? ("to protect " + E.Actor.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false)) : ("for " + Faction.GetFormattedName(text)), false);
							return true;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008CFC RID: 36092 RVA: 0x00320D64 File Offset: 0x0031EF64
		public bool CanAcquireTarget()
		{
			return !this.Passive && this.CanFight() && this.ParentObject.FireEvent("AILookForTarget");
		}

		// Token: 0x06008CFD RID: 36093 RVA: 0x00320D90 File Offset: 0x0031EF90
		public GameObject FindProspectiveTarget(Cell FromCell = null, bool WantPlayer = false)
		{
			if (!this.CanAcquireTarget())
			{
				return null;
			}
			if (FromCell == null)
			{
				FromCell = this.ParentObject.CurrentCell;
				if (FromCell == null)
				{
					return null;
				}
			}
			List<GameObject> list = FromCell.ParentZone.FastCombatSquareVisibility(FromCell.X, FromCell.Y, Stat.Random(this.MinKillRadius, this.MaxKillRadius), this.ParentObject, null, false, true, false, true, null);
			if (list.Count > 0)
			{
				List<GameObject> list2 = Event.NewGameObjectList();
				if (WantPlayer)
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (this.IsSuitablePlayerControlledTarget(list[i]))
						{
							list2.Add(list[i]);
						}
					}
				}
				else
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (this.IsSuitableTarget(list[j]))
						{
							list2.Add(list[j]);
						}
					}
				}
				if (list2 != null && list2.Count > 0)
				{
					if (list2.Count > 1)
					{
						list2.Sort(new Comparison<GameObject>(this.TargetSort));
						int num = Stat.Roll(1, 10);
						if (num == 1)
						{
							return list2.GetRandomElement<GameObject>(null);
						}
						if (num == 2)
						{
							List<GameObject> list3 = list2;
							return list3[list3.Count - 1];
						}
					}
					return list2[0];
				}
			}
			return null;
		}

		// Token: 0x06008CFE RID: 36094 RVA: 0x00320EC0 File Offset: 0x0031F0C0
		public bool FriendlyFireIncident(GameObject GO, bool TookDamage = false)
		{
			Brain.FeelingLevel feelingLevel = this.GetFeelingLevel(GO);
			int num = (TookDamage ? 1 : 0);
			if (feelingLevel == Brain.FeelingLevel.Allied || feelingLevel == Brain.FeelingLevel.Neutral)
			{
				if (!this.ParentObject.FireEvent(Event.New("CanBeAngeredByFriendlyFire", "Attacker", GO)))
				{
					return false;
				}
				if (this.ParentObject.Stat("Intelligence", 0) < 7)
				{
					return true;
				}
				if (this.FriendlyFire == null)
				{
					if (num > 0)
					{
						this.FriendlyFire = new Dictionary<GameObject, int> { { GO, num } };
					}
				}
				else if (this.FriendlyFire.TryGetValue(GO, out num))
				{
					if (TookDamage)
					{
						num++;
						this.FriendlyFire[GO] = num;
					}
				}
				else if (num > 0)
				{
					this.FriendlyFire.Add(GO, num);
				}
				if (feelingLevel == Brain.FeelingLevel.Allied)
				{
					if (GO.IsPlayerControlled())
					{
						if (num < 5 && Stat.Random(0, 400) > num)
						{
							return false;
						}
					}
					else if (num < 20 && Stat.Random(0, 1000) > num)
					{
						return false;
					}
				}
				else if (feelingLevel == Brain.FeelingLevel.Neutral)
				{
					if (GO.IsPlayerControlled())
					{
						if (num < 3 && Stat.Random(0, 200) > num)
						{
							return false;
						}
					}
					else if (num < 5 && Stat.Random(0, 400) > num)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06008CFF RID: 36095 RVA: 0x00320FE0 File Offset: 0x0031F1E0
		public static double PreciseWeaponScore(GameObject obj, GameObject who = null)
		{
			if (obj == null)
			{
				return 0.0;
			}
			string inventoryCategory = obj.GetInventoryCategory(false);
			if (inventoryCategory == "Ammo")
			{
				return 0.0;
			}
			MeleeWeapon part = obj.GetPart<MeleeWeapon>();
			if (part == null)
			{
				return 0.0;
			}
			double num = (double)part.BaseDamage.RollMinCached();
			int num2 = part.BaseDamage.RollMaxCached();
			double num3 = num * (double)2 + (double)num2;
			int num4 = 0;
			int num5;
			int num6;
			part.GetNormalPenetration(who, out num5, out num6);
			int num10;
			if (who != null && IsAdaptivePenetrationActiveEvent.Check(obj, ref num4))
			{
				int maxStrengthBonus = part.MaxStrengthBonus;
				int num7 = 0;
				int num8 = 0;
				int num9 = (Statistic.IsMental(part.Stat) ? Stats.GetCombatMA(who) : Stats.GetCombatAV(who));
				num10 = num5 + num4 + 1;
				GetWeaponMeleePenetrationEvent.Process(ref num8, ref num10, ref maxStrengthBonus, ref num6, ref num7, num9, false, "", "Primary", who, who, obj);
			}
			else
			{
				num10 = num5 + num6 - 1;
			}
			double num11 = ((num10 < 1) ? (num3 / 2.0) : (num3 * (double)num10));
			double num12 = 0.0;
			double num13 = (double)(50 + part.HitBonus * 5);
			if (who != null)
			{
				num13 += (double)(who.StatMod("Agility", 0) * 5);
				if (who.HasSkill(part.Skill))
				{
					num13 += (double)((part.Skill == "ShortBlades") ? 5 : 10);
					num13 += 10.0;
				}
				if ((part.Skill == "LongBlades" || part.Skill == "ShortBlades") && who.HasPart<LongBladesCore>())
				{
					if (part.Skill == "LongBlades")
					{
						if (who.HasPart(typeof(LongBladesLunge)))
						{
							num12 += 1.0;
						}
						if (who.HasPart(typeof(LongBladesSwipe)))
						{
							num12 += 1.0;
						}
						if (who.HasPart(typeof(LongBladesDeathblow)))
						{
							num12 += 1.0;
						}
					}
					if (who.HasEffect(typeof(LongbladeStance_Aggressive)))
					{
						if (who.HasPart(typeof(LongBladesImprovedAggressiveStance)))
						{
							num13 -= 15.0;
						}
						else
						{
							num13 -= 10.0;
						}
					}
					else if (who.HasEffect(typeof(LongbladeStance_Dueling)))
					{
						if (who.HasPart(typeof(LongBladesImprovedDuelistStance)))
						{
							num13 += 15.0;
						}
						else
						{
							num13 += 10.0;
						}
					}
				}
			}
			num13 = Math.Min(Math.Max(num13, 5.0), 100.0);
			num12 += num11 * num13 / 50.0;
			if (obj.HasTag("Storied"))
			{
				num12 += 5.0;
			}
			if (obj.HasTag("NaturalGear"))
			{
				num12 += 1.0;
			}
			string usesSlots = obj.UsesSlots;
			if (!usesSlots.IsNullOrEmpty())
			{
				num12 = num12 * 2.0 / (double)(usesSlots.CachedCommaExpansion().Count + 1);
			}
			else
			{
				int slotsRequiredFor = obj.GetSlotsRequiredFor(who, part.Slot, true);
				if (slotsRequiredFor > 1)
				{
					num12 = num12 * 2.0 / (double)(slotsRequiredFor + 1);
				}
			}
			if (part.Ego != 0)
			{
				num12 += (double)part.Ego;
			}
			if (inventoryCategory == "Melee Weapons" || inventoryCategory == "Natural Weapons")
			{
				num12 += 1.0;
			}
			if (num13 != 50.0)
			{
				num12 += (num13 - 50.0) / 5.0;
			}
			string text = obj.GetTag("AdjustWeaponScore", null);
			if (text != null)
			{
				num12 += Convert.ToDouble(text);
			}
			text = ((who != null) ? who.GetTagOrStringProperty("AdjustWieldedScore", null) : null);
			int num14;
			if (!text.IsNullOrEmpty() && text.CachedNumericDictionaryExpansion().TryGetValue(obj.Blueprint, out num14))
			{
				num12 += (double)num14;
			}
			if (obj.HasRegisteredEvent("AdjustWeaponScore"))
			{
				int num15 = (int)Math.Round(num12, MidpointRounding.AwayFromZero);
				Event @event = Event.New("AdjustWeaponScore", "Score", num15, "OriginalScore", num12, "User", who);
				obj.FireEvent(@event);
				int intParameter = @event.GetIntParameter("Score", 0);
				if (num15 != intParameter)
				{
					num12 += (double)(intParameter - num15);
				}
			}
			return num12;
		}

		// Token: 0x06008D00 RID: 36096 RVA: 0x0032145C File Offset: 0x0031F65C
		public static int WeaponScore(GameObject Object, GameObject Actor = null)
		{
			return (int)Math.Round(Brain.PreciseWeaponScore(Object, Actor), MidpointRounding.AwayFromZero);
		}

		// Token: 0x06008D01 RID: 36097 RVA: 0x0032146C File Offset: 0x0031F66C
		public static bool TreatAsThrownWeapon(GameObject Object)
		{
			return Object.IsThrownWeapon && !Object.HasTagOrProperty("NoAIEquipAsThrownWeapon");
		}

		// Token: 0x06008D02 RID: 36098 RVA: 0x00321488 File Offset: 0x0031F688
		public static int CompareWeapons(GameObject Weapon1, GameObject Weapon2, GameObject POV)
		{
			if (Weapon1 == Weapon2)
			{
				return 0;
			}
			if (Weapon1 == null)
			{
				return 1;
			}
			if (Weapon2 == null)
			{
				return -1;
			}
			bool flag = Weapon1.HasTagOrProperty("AlwaysEquipAsWeapon");
			int num = flag.CompareTo(Weapon2.HasTagOrProperty("AlwaysEquipAsWeapon"));
			if (num != 0)
			{
				return -num;
			}
			flag = Weapon1.HasPart<MissileWeapon>();
			int num2 = flag.CompareTo(Weapon2.HasPart<MissileWeapon>());
			if (num2 != 0)
			{
				return num2;
			}
			flag = Brain.TreatAsThrownWeapon(Weapon1);
			int num3 = flag.CompareTo(Brain.TreatAsThrownWeapon(Weapon2));
			if (num3 != 0)
			{
				return num3;
			}
			flag = Weapon1.HasPart<Food>();
			int num4 = flag.CompareTo(Weapon2.HasPart<Food>());
			if (num4 != 0)
			{
				return num4;
			}
			flag = Weapon1.HasPart<Armor>();
			int num5 = flag.CompareTo(Weapon2.HasPart<Armor>());
			if (num5 != 0)
			{
				return num5;
			}
			flag = Weapon1.HasEffectDescendedFrom<IBusted>();
			int num6 = flag.CompareTo(Weapon2.HasEffectDescendedFrom<IBusted>());
			if (num6 != 0)
			{
				return num6;
			}
			flag = Weapon1.HasTag("MeleeWeapon") || Weapon1.HasTag("NaturalGear");
			int num7 = flag.CompareTo(Weapon2.HasTag("MeleeWeapon") || Weapon2.HasTag("NaturalGear"));
			if (num7 != 0)
			{
				return -num7;
			}
			flag = Weapon1.HasTag("UndesirableWeapon");
			int num8 = flag.CompareTo(Weapon2.HasTag("UndesirableWeapon"));
			if (num8 != 0)
			{
				return num8;
			}
			double num9 = Brain.PreciseWeaponScore(Weapon1, POV);
			double num10 = Brain.PreciseWeaponScore(Weapon2, POV);
			return -num9.CompareTo(num10);
		}

		// Token: 0x06008D03 RID: 36099 RVA: 0x003215E5 File Offset: 0x0031F7E5
		public static bool IsNewWeaponBetter(GameObject NewWeapon, GameObject OldWeapon, GameObject POV)
		{
			return Brain.CompareWeapons(NewWeapon, OldWeapon, POV) < 0;
		}

		// Token: 0x06008D04 RID: 36100 RVA: 0x003215F2 File Offset: 0x0031F7F2
		public bool IsNewWeaponBetter(GameObject NewWeapon, GameObject OldWeapon)
		{
			return Brain.IsNewWeaponBetter(NewWeapon, OldWeapon, this.ParentObject);
		}

		// Token: 0x06008D05 RID: 36101 RVA: 0x00321604 File Offset: 0x0031F804
		public static double PreciseMissileWeaponScore(GameObject obj, GameObject who = null)
		{
			if (obj == null)
			{
				return 0.0;
			}
			MissileWeapon part = obj.GetPart<MissileWeapon>();
			if (part == null)
			{
				return 0.0;
			}
			GetMissileWeaponPerformanceEvent @for = GetMissileWeaponPerformanceEvent.GetFor(who, obj, null, null, null, null, null, null, null, null, null, null, null, false);
			if (@for.BaseDamage.IsNullOrEmpty())
			{
				return 0.0;
			}
			int num = @for.BaseDamage.RollMinCached();
			int num2 = @for.BaseDamage.RollMaxCached();
			double num3 = (double)(num * 2 + num2);
			int num4 = @for.Penetration - 1;
			double num5 = ((num4 < 1) ? (num3 / 2.0) : (num3 * (double)num4));
			double num6 = (double)(50 - part.WeaponAccuracy);
			if (who != null)
			{
				num6 += (double)(who.StatMod(part.Modifier, 0) * 3);
				if (part.IsSkilled(who))
				{
					num6 += 6.0;
				}
			}
			num6 = Math.Min(Math.Max(num6, 5.0), 100.0);
			double num7 = num5 * (double)part.ShotsPerAction * num6 / 50.0;
			if (obj.HasTag("Storied"))
			{
				num7 += 5.0;
			}
			if (obj.HasTag("NaturalGear"))
			{
				num7 += 1.0;
			}
			string usesSlots = obj.UsesSlots;
			if (CanFireAllMissileWeaponsEvent.Check(who, null))
			{
				if (!usesSlots.IsNullOrEmpty())
				{
					num7 = num7 * 2.0 / (double)(usesSlots.CachedCommaExpansion().Count + 1);
				}
				else
				{
					int slotsRequiredFor = obj.GetSlotsRequiredFor(who, part.SlotType, true);
					if (slotsRequiredFor > 1)
					{
						num7 = num7 * 2.0 / (double)(slotsRequiredFor + 1);
					}
				}
			}
			else if (!usesSlots.IsNullOrEmpty())
			{
				num7 = num7 * 3.0 / (double)(usesSlots.CachedCommaExpansion().Count + 2);
			}
			else
			{
				int slotsRequiredFor2 = obj.GetSlotsRequiredFor(who, part.SlotType, true);
				if (slotsRequiredFor2 > 1)
				{
					num7 = num7 * 4.0 / (double)(slotsRequiredFor2 + 3);
				}
			}
			if (num6 != 50.0)
			{
				num7 += (num6 - 50.0) / 5.0;
			}
			string tag = obj.GetTag("AdjustMissileWeaponScore", null);
			if (tag != null)
			{
				num7 += Convert.ToDouble(tag);
			}
			if (obj.HasRegisteredEvent("AdjustMissileWeaponScore"))
			{
				int num8 = (int)Math.Round(num7, MidpointRounding.AwayFromZero);
				Event @event = Event.New("AdjustMissileWeaponScore", "Score", num8, "OriginalScore", num7, "User", who);
				obj.FireEvent(@event);
				int intParameter = @event.GetIntParameter("Score", 0);
				if (num8 != intParameter)
				{
					num7 += (double)(intParameter - num8);
				}
			}
			return num7;
		}

		// Token: 0x06008D06 RID: 36102 RVA: 0x003218DA File Offset: 0x0031FADA
		public static int MissileWeaponScore(GameObject obj, GameObject who = null)
		{
			return (int)Math.Round(Brain.PreciseMissileWeaponScore(obj, who), MidpointRounding.AwayFromZero);
		}

		// Token: 0x06008D07 RID: 36103 RVA: 0x003218EC File Offset: 0x0031FAEC
		public static int CompareMissileWeapons(GameObject Weapon1, GameObject Weapon2, GameObject POV)
		{
			if (Weapon1 == Weapon2)
			{
				return 0;
			}
			if (Weapon1 == null)
			{
				return 1;
			}
			if (Weapon2 == null)
			{
				return -1;
			}
			int num = Weapon1.HasTagOrProperty("AlwaysEquipAsMissileWeapon").CompareTo(Weapon2.HasTagOrProperty("AlwaysEquipAsMissileWeapon"));
			if (num != 0)
			{
				return -num;
			}
			return -Brain.PreciseMissileWeaponScore(Weapon1, POV).CompareTo(Brain.PreciseMissileWeaponScore(Weapon2, POV));
		}

		// Token: 0x06008D08 RID: 36104 RVA: 0x00321945 File Offset: 0x0031FB45
		public static bool IsNewMissileWeaponBetter(GameObject NewWeapon, GameObject OldWeapon, GameObject POV)
		{
			return Brain.CompareMissileWeapons(NewWeapon, OldWeapon, POV) < 0;
		}

		// Token: 0x06008D09 RID: 36105 RVA: 0x00321952 File Offset: 0x0031FB52
		public bool IsNewMissileWeaponBetter(GameObject NewWeapon, GameObject OldWeapon)
		{
			return Brain.IsNewMissileWeaponBetter(NewWeapon, OldWeapon, this.ParentObject);
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x00321964 File Offset: 0x0031FB64
		public static double PreciseArmorScore(GameObject obj, GameObject who = null)
		{
			if (obj == null)
			{
				return 0.0;
			}
			Armor part = obj.GetPart<Armor>();
			if (part == null)
			{
				return 0.0;
			}
			double num = (double)(part.Acid + part.Elec + part.Cold + part.Heat + part.Strength * 5 + part.Intelligence + part.Ego * 2 + part.ToHit * 10 - part.SpeedPenalty * 2 + part.CarryBonus / 5);
			double num2 = (double)part.AV;
			double num3 = (double)part.DV;
			if (who != null && part.WornOn != "*")
			{
				Body body = who.Body;
				if (body != null)
				{
					int partCount = body.GetPartCount(part.WornOn);
					if (partCount > 1)
					{
						num2 /= (double)partCount;
						num3 /= (double)partCount;
					}
				}
			}
			if (part.Agility != 0)
			{
				num += (double)part.Agility;
				num3 += (double)part.Agility * 0.5;
			}
			if (num2 > 0.0)
			{
				num += num2 * num2 * 20.0;
			}
			else if (num2 < 0.0)
			{
				num += num2 * 40.0;
			}
			if (num3 > 0.0)
			{
				num += num3 * num3 * 10.0;
			}
			else if (num3 < 0.0)
			{
				num += num3 * 20.0;
			}
			MoveCostMultiplier part2 = obj.GetPart<MoveCostMultiplier>();
			if (part2 != null)
			{
				num += (double)(-(double)part2.Amount * 2);
			}
			EquipStatBoost part3 = obj.GetPart<EquipStatBoost>();
			if (part3 != null)
			{
				int num4 = 0;
				foreach (KeyValuePair<string, int> keyValuePair in part3.GetBonusList(false, false))
				{
					int num5 = (Statistic.IsInverseBenefit(keyValuePair.Key) ? (-keyValuePair.Value) : keyValuePair.Value);
					if (!keyValuePair.Key.Contains("Resist"))
					{
						num5 *= 10;
					}
					num4 += num5;
				}
				if (num4 != 0)
				{
					if (part3.ChargeUse > 0)
					{
						num += (double)num4;
					}
					else
					{
						num += (double)(num4 * 2);
					}
				}
			}
			if (obj.HasTag("Storied"))
			{
				num += 5.0;
			}
			if (obj.HasTag("NaturalGear"))
			{
				num += 1.0;
			}
			string usesSlots = obj.UsesSlots;
			if (!usesSlots.IsNullOrEmpty())
			{
				num = num * 2.0 / (double)(usesSlots.CachedCommaExpansion().Count + 1);
			}
			if (obj.HasPart<Metal>())
			{
				num -= Math.Abs(num / 20.0);
			}
			return num;
		}

		// Token: 0x06008D0B RID: 36107 RVA: 0x00321C18 File Offset: 0x0031FE18
		public static int ArmorScore(GameObject obj, GameObject who = null)
		{
			return (int)Math.Round(Brain.PreciseArmorScore(obj, who), MidpointRounding.AwayFromZero);
		}

		// Token: 0x06008D0C RID: 36108 RVA: 0x00321C28 File Offset: 0x0031FE28
		public static int CompareArmors(GameObject Armor1, GameObject Armor2, GameObject POV)
		{
			if (Armor1 == Armor2)
			{
				return 0;
			}
			if (Armor1 == null)
			{
				return 1;
			}
			if (Armor2 == null)
			{
				return -1;
			}
			int num = Armor1.HasTagOrProperty("AlwaysEquipAsArmor").CompareTo(Armor2.HasTagOrProperty("AlwaysEquipAsArmor"));
			if (num != 0)
			{
				return -num;
			}
			return -Brain.PreciseArmorScore(Armor1, POV).CompareTo(Brain.PreciseArmorScore(Armor2, POV));
		}

		// Token: 0x06008D0D RID: 36109 RVA: 0x00321C81 File Offset: 0x0031FE81
		public static bool IsNewArmorBetter(GameObject NewArmor, GameObject OldArmor, GameObject POV)
		{
			return Brain.CompareArmors(NewArmor, OldArmor, POV) < 0;
		}

		// Token: 0x06008D0E RID: 36110 RVA: 0x00321C8E File Offset: 0x0031FE8E
		public bool IsNewArmorBetter(GameObject NewArmor, GameObject OldArmor)
		{
			return Brain.IsNewArmorBetter(NewArmor, OldArmor, this.ParentObject);
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x00321CA0 File Offset: 0x0031FEA0
		public static double PreciseShieldScore(GameObject obj, GameObject who = null)
		{
			if (obj == null)
			{
				return 0.0;
			}
			Shield part = obj.GetPart<Shield>();
			if (part == null)
			{
				return 0.0;
			}
			double num = (double)(-(double)part.SpeedPenalty);
			if (part.AV > 0)
			{
				num += (double)(part.AV * part.AV);
			}
			if (part.DV < 0)
			{
				num += (double)(part.DV * 2);
			}
			else if (part.DV > 0)
			{
				num += (double)(part.DV * part.DV);
			}
			if (obj.HasTag("Storied"))
			{
				num += 5.0;
			}
			if (obj.HasTag("NaturalGear"))
			{
				num += 1.0;
			}
			string usesSlots = obj.UsesSlots;
			if (!usesSlots.IsNullOrEmpty())
			{
				num = num * 2.0 / (double)(usesSlots.CachedCommaExpansion().Count + 1);
			}
			if (part.WornOn != "Hand")
			{
				num = num * 5.0 / 4.0;
			}
			if (obj.HasPart<Metal>() && num > 0.0)
			{
				num -= 1.0;
			}
			return num;
		}

		// Token: 0x06008D10 RID: 36112 RVA: 0x00321DC7 File Offset: 0x0031FFC7
		public static int ShieldScore(GameObject obj, GameObject who = null)
		{
			return (int)Math.Round(Brain.PreciseShieldScore(obj, who), MidpointRounding.AwayFromZero);
		}

		// Token: 0x06008D11 RID: 36113 RVA: 0x00321DD8 File Offset: 0x0031FFD8
		public static int CompareShields(GameObject Shield1, GameObject Shield2, GameObject POV)
		{
			if (Shield1 == Shield2)
			{
				return 0;
			}
			if (Shield1 == null)
			{
				return 1;
			}
			if (Shield2 == null)
			{
				return -1;
			}
			return -Brain.PreciseShieldScore(Shield1, POV).CompareTo(Brain.PreciseShieldScore(Shield2, POV));
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x00321E0C File Offset: 0x0032000C
		public static bool IsNewShieldBetter(GameObject NewShield, GameObject OldShield, GameObject POV)
		{
			return Brain.CompareShields(NewShield, OldShield, POV) < 0;
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x00321E19 File Offset: 0x00320019
		public bool IsNewShieldBetter(GameObject NewShield, GameObject OldShield)
		{
			return Brain.IsNewShieldBetter(NewShield, OldShield, this.ParentObject);
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x00321E28 File Offset: 0x00320028
		public static int CompareGear(GameObject obj1, GameObject obj2, GameObject POV)
		{
			if (obj1 == obj2)
			{
				return 0;
			}
			if (obj1 == null)
			{
				return 1;
			}
			if (obj2 == null)
			{
				return -1;
			}
			int num = obj1.HasEffectDescendedFrom<IBusted>().CompareTo(obj2.HasEffectDescendedFrom<IBusted>());
			if (num != 0)
			{
				return num;
			}
			int num2 = Brain.CompareArmors(obj1, obj2, POV);
			if (num2 != 0)
			{
				return num2;
			}
			int num3 = Brain.CompareMissileWeapons(obj1, obj2, POV);
			if (num3 != 0)
			{
				return num3;
			}
			int num4 = Brain.CompareShields(obj1, obj2, POV);
			if (num4 != 0)
			{
				return num4;
			}
			int num5 = Brain.CompareWeapons(obj1, obj2, POV);
			if (num5 != 0)
			{
				return num5;
			}
			int num6 = obj1.HasPartDescendedFrom<ILightSource>().CompareTo(obj2.HasPartDescendedFrom<ILightSource>());
			if (num6 != 0)
			{
				return -num6;
			}
			if (obj1.HasPart<Commerce>() && obj2.HasPart<Commerce>())
			{
				int num7 = obj1.ValueEach.CompareTo(obj2.ValueEach);
				if (num7 != 0)
				{
					return -num7;
				}
			}
			int num8 = obj1.GetTier().CompareTo(obj2.GetTier());
			if (num8 != 0)
			{
				return -num8;
			}
			if (!obj1.HasIntProperty("SortFudge"))
			{
				obj1.SetIntProperty("SortFudge", Stat.Random(0, 32768), false);
			}
			if (!obj2.HasIntProperty("SortFudge"))
			{
				obj2.SetIntProperty("SortFudge", Stat.Random(0, 32768), false);
			}
			return obj1.GetIntProperty("SortFudge", 0).CompareTo(obj2.GetIntProperty("SortFudge", 0));
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x00321F73 File Offset: 0x00320173
		public static bool IsNewGearBetter(GameObject NewGear, GameObject OldGear, GameObject POV)
		{
			return Brain.CompareGear(NewGear, OldGear, POV) < 0;
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x00321F80 File Offset: 0x00320180
		public bool IsNewGearBetter(GameObject NewGear, GameObject OldGear)
		{
			return Brain.IsNewGearBetter(NewGear, OldGear, this.ParentObject);
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x00321F90 File Offset: 0x00320190
		public void CleanNaturalGear(Inventory Inventory = null)
		{
			if (Inventory == null)
			{
				Inventory = this.ParentObject.Inventory;
				if (Inventory == null)
				{
					return;
				}
			}
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in Inventory.GetObjectsDirect())
			{
				if (gameObject.HasTag("NaturalGear"))
				{
					list.Add(gameObject);
				}
			}
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				list[i].Obliterate(null, false, null);
				i++;
			}
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x00322030 File Offset: 0x00320230
		public void PerformEquip(bool Silent = false, bool DoPrimaryChoice = true, GameObject Avoid = null)
		{
			this.PerformReequip(Silent, DoPrimaryChoice, true, Avoid);
		}

		// Token: 0x06008D19 RID: 36121 RVA: 0x0032203C File Offset: 0x0032023C
		public void PerformReequip(bool Silent = false, bool DoPrimaryChoice = true, bool Initial = false, GameObject Avoid = null)
		{
			try
			{
				if (this.ParentObject.CurrentCell == null || this.ParentObject.CanMoveExtremities(null, false, false, false))
				{
					Brain.SharedGearSorter.Setup(this.ParentObject, false);
					Brain.SharedWeaponSorter.Setup(this.ParentObject, false);
					bool flag = this.ParentObject.IsMerchant();
					Body body = this.ParentObject.Body;
					Inventory inventory = this.ParentObject.Inventory;
					if (body != null && inventory != null)
					{
						try
						{
							if (The.Player != null && !this.ParentObject.HasTag("ExcludeGrenadeHack") && The.Player.Stat("Level", 0) < 3 && this.IsHostileTowards(The.Player))
							{
								foreach (GameObject gameObject in inventory.GetObjectsWithTag("Grenade"))
								{
									gameObject.Destroy(null, false, false, null);
								}
							}
						}
						catch (Exception ex)
						{
							base.LogErrorInEditor(ex.ToString());
						}
						List<BodyPart> parts = body.GetParts();
						int num = 0;
						GameObject gameObject2 = null;
						foreach (BodyPart bodyPart in parts)
						{
							if (bodyPart.Equipped != null && bodyPart.Equipped.HasPartDescendedFrom<ILightSource>())
							{
								num++;
							}
						}
						foreach (BodyPart bodyPart2 in parts)
						{
							if (bodyPart2.Equipped != null && bodyPart2.Equipped.HasPart<Shield>())
							{
								gameObject2 = bodyPart2.Equipped;
								break;
							}
						}
						List<GameObject> list = Event.NewGameObjectList();
						inventory.GetObjects(list);
						if (Avoid != null)
						{
							list.Remove(Avoid);
						}
						List<GameObject> list2 = Event.NewGameObjectList();
						foreach (GameObject gameObject3 in list)
						{
							if (gameObject3.EquipAsDefaultBehavior())
							{
								list2.Add(gameObject3);
							}
						}
						foreach (GameObject gameObject4 in list2)
						{
							list.Remove(gameObject4);
						}
						if (list2.Count > 0)
						{
							foreach (BodyPart bodyPart3 in parts)
							{
								List<GameObject> equipmentListForSlot = inventory.GetEquipmentListForSlot(bodyPart3.Type, list2, true, true, true);
								if (equipmentListForSlot != null && equipmentListForSlot.Count > 0)
								{
									if (equipmentListForSlot.Count > 1)
									{
										equipmentListForSlot.Sort(Brain.SharedGearSorter);
									}
									GameObject gameObject5 = equipmentListForSlot[0];
									if (!this.Equip(gameObject5, bodyPart3, list2, null, null, ref num, ref gameObject2, true, true) && GameObject.Validate(ref gameObject5))
									{
										string[] array = new string[11];
										array[0] = "failed to equip default gear ";
										array[1] = gameObject5.DebugName;
										array[2] = " on ";
										array[3] = this.ParentObject.DebugName;
										array[4] = "'s ";
										array[5] = bodyPart3.Description;
										array[6] = " (EQ:";
										int num2 = 7;
										GameObject equipped = bodyPart3.Equipped;
										array[num2] = ((equipped != null) ? equipped.DebugName : null);
										array[8] = ",DF:";
										int num3 = 9;
										GameObject defaultBehavior = bodyPart3.DefaultBehavior;
										array[num3] = ((defaultBehavior != null) ? defaultBehavior.DebugName : null);
										array[10] = "), destructing";
										MetricsManager.LogError(string.Concat(array));
										gameObject5.Obliterate(null, false, null);
									}
								}
							}
						}
						string propertyOrTag = this.ParentObject.GetPropertyOrTag("NoEquip", null);
						List<string> list3 = (propertyOrTag.IsNullOrEmpty() ? null : new List<string>(propertyOrTag.CachedCommaExpansion()));
						List<GameObject> list4 = inventory.GetEquipmentListForSlot("Hand", list, true, true, true) ?? Event.NewGameObjectList();
						List<GameObject> list5 = null;
						for (int i = list4.Count - 1; i >= 0; i--)
						{
							GameObject gameObject6 = list4[i];
							if (gameObject6.HasPart<Shield>())
							{
								if (list5 == null)
								{
									list5 = Event.NewGameObjectList(list4);
								}
								list4.Remove(gameObject6);
							}
							else if (gameObject6.HasPart<MissileWeapon>() || Brain.TreatAsThrownWeapon(gameObject6))
							{
								list4.Remove(gameObject6);
								if (list5 != null)
								{
									list5.Remove(gameObject6);
								}
							}
							else if (!gameObject6.HasPropertyOrTag("ShowMeleeWeaponStats") && !gameObject6.HasTag("MeleeWeapon") && !gameObject6.HasPropertyOrTag("AlwaysEquipAsWeapon") && gameObject6.GetInventoryCategory(false) != "Melee Weapons" && !gameObject6.HasPartDescendedFrom<ILightSource>())
							{
								list4.Remove(gameObject6);
								if (list5 != null)
								{
									list5.Remove(gameObject6);
								}
							}
						}
						BodyPart bodyPart4 = null;
						if (list4.Count > 1)
						{
							list4.Sort(Brain.SharedWeaponSorter);
						}
						if (list5 != null && list5.Count > 1)
						{
							list5.Sort(Brain.SharedGearSorter);
						}
						BodyPart bodyPart5 = null;
						foreach (BodyPart bodyPart6 in parts)
						{
							if (bodyPart6.Primary && bodyPart6.Type == "Hand")
							{
								if (bodyPart6.Equipped == null || bodyPart6.Equipped.CanBeUnequipped(null, null, false, false))
								{
									bodyPart5 = bodyPart6;
									GameObject gameObject7 = bodyPart6.Equipped ?? bodyPart6.DefaultBehavior;
									foreach (GameObject gameObject8 in list4)
									{
										if ((list3 == null || !list3.Contains(gameObject8.Blueprint)) && (this.ParentObject.IsPlayer() || !gameObject8.HasPropertyOrTag("NoAIEquip")) && (!flag || !gameObject8.HasProperty("_stock")) && this.IsNewWeaponBetter(gameObject8, gameObject7))
										{
											gameObject7 = gameObject8;
											break;
										}
									}
									this.Equip(gameObject7, bodyPart6, list4, list5, list, ref num, ref gameObject2, Silent, Initial);
									break;
								}
								break;
							}
						}
						if (list4.Count > 0 && this.ParentObject.HasSkill("Multiweapon_Fighting"))
						{
							foreach (BodyPart bodyPart7 in parts)
							{
								if (bodyPart7 != bodyPart5 && bodyPart7.Type == "Hand")
								{
									bodyPart4 = bodyPart7;
									if (bodyPart7.Equipped == null || bodyPart7.Equipped.CanBeUnequipped(null, null, false, false))
									{
										GameObject gameObject9 = bodyPart7.Equipped ?? bodyPart7.DefaultBehavior;
										foreach (GameObject gameObject10 in list4)
										{
											if ((list3 == null || !list3.Contains(gameObject10.Blueprint)) && (this.ParentObject.IsPlayer() || !gameObject10.HasPropertyOrTag("NoAIEquip")) && (!flag || !gameObject10.HasProperty("_stock")) && this.IsNewWeaponBetter(gameObject10, gameObject9))
											{
												gameObject9 = gameObject10;
												break;
											}
										}
										this.Equip(gameObject9, bodyPart7, list4, list5, list, ref num, ref gameObject2, Silent, Initial);
										break;
									}
									break;
								}
							}
						}
						BodyPart bodyPart8 = null;
						foreach (BodyPart bodyPart9 in parts)
						{
							if (bodyPart9.Type == "Hand" && !bodyPart9.Primary)
							{
								bodyPart8 = bodyPart9;
							}
						}
						foreach (BodyPart bodyPart10 in parts)
						{
							GameObject gameObject11 = null;
							GameObject gameObject12 = bodyPart10.Equipped ?? bodyPart10.DefaultBehavior;
							List<GameObject> list6 = null;
							List<GameObject> list7 = null;
							List<GameObject> list8 = null;
							List<GameObject> list9;
							if (bodyPart10.Type == "Hand")
							{
								if (bodyPart10.Primary || bodyPart10 == bodyPart4)
								{
									continue;
								}
								list9 = list5 ?? list4;
								list6 = list4;
								list7 = list5;
								list8 = list;
							}
							else
							{
								list9 = inventory.GetEquipmentListForSlot(bodyPart10.Type, list, true, true, true);
								if (list9 != null && list9.Count > 1)
								{
									list9.Sort(Brain.SharedGearSorter);
								}
								list6 = list;
							}
							if (list9 != null && list9.Count != 0)
							{
								foreach (GameObject gameObject13 in list9)
								{
									if ((list3 == null || !list3.Contains(gameObject13.Blueprint)) && (this.ParentObject.IsPlayer() || !gameObject13.HasPropertyOrTag("NoAIEquip")) && (!flag || !gameObject13.HasProperty("_stock")) && !gameObject13.HasPart<Food>() && (!gameObject13.HasPart<Shield>() || (this.ParentObject.HasSkill("Shield_Block") && (gameObject2 == null || (gameObject12 == gameObject2 && this.IsNewShieldBetter(gameObject13, gameObject2) && this.IsNewGearBetter(gameObject13, gameObject12))))))
									{
										if (bodyPart10.Type == "Hand")
										{
											if (bodyPart10 == bodyPart8 && num <= 0 && gameObject13.HasPartDescendedFrom<ILightSource>() && bodyPart10.Equipped == null)
											{
												gameObject11 = gameObject13;
												break;
											}
											if (!this.ParentObject.IsPlayer() && this.IsNewGearBetter(gameObject13, gameObject12))
											{
												gameObject11 = gameObject13;
												break;
											}
										}
										else if (bodyPart10.Type == "Thrown Weapon")
										{
											if (gameObject13.Physics != null && gameObject13.Physics.Category == "Grenades" && this.IsNewGearBetter(gameObject13, gameObject12))
											{
												gameObject11 = gameObject13;
												break;
											}
											if (Brain.TreatAsThrownWeapon(gameObject13) && !gameObject13.HasTagOrProperty("NoAIEquip") && (list3 == null || !list3.Contains(gameObject13.Blueprint)) && this.IsNewGearBetter(gameObject13, gameObject12))
											{
												gameObject11 = gameObject13;
												break;
											}
										}
										else
										{
											MissileWeapon part = gameObject13.GetPart<MissileWeapon>();
											if (part != null)
											{
												if ((!part.FiresManually || part.ValidSlotType(bodyPart10.Type, false)) && this.IsNewMissileWeaponBetter(gameObject13, gameObject12))
												{
													gameObject11 = gameObject13;
													break;
												}
											}
											else if (bodyPart10.Type != "Missile Weapon" && this.IsNewGearBetter(gameObject13, gameObject12))
											{
												gameObject11 = gameObject13;
												break;
											}
										}
									}
								}
								this.Equip(gameObject11, bodyPart10, list6, list7, list8, ref num, ref gameObject2, Silent, Initial);
							}
						}
					}
					this.CleanNaturalGear(inventory);
					CommandReloadEvent.Execute(this.ParentObject, false, false, 0);
					try
					{
						if (DoPrimaryChoice && this.ParentObject != null && body != null && !this.ParentObject.IsPlayer())
						{
							BodyPart bestPart = null;
							GameObject bestWeapon = null;
							body.ForeachPart(delegate(BodyPart p)
							{
								if (p.Equipped != null)
								{
									if (this.IsNewWeaponBetter(p.Equipped, bestWeapon))
									{
										bestPart = p;
										bestWeapon = p.Equipped;
										return;
									}
								}
								else if (p.DefaultBehavior != null && this.IsNewWeaponBetter(p.DefaultBehavior, bestWeapon))
								{
									bestPart = p;
									bestWeapon = p.DefaultBehavior;
								}
							});
							if (bestPart != null)
							{
								bestPart.SetAsPreferredDefault(false);
							}
						}
					}
					catch (Exception ex2)
					{
						MetricsManager.LogException("Select primary limb", ex2, "game_exception");
					}
					if (Initial)
					{
						DidInitialEquipEvent.Send(this.ParentObject);
					}
					else
					{
						DidReequipEvent.Send(this.ParentObject);
					}
				}
			}
			catch (Exception ex3)
			{
				MetricsManager.LogException("Exception during Brain::PerformReequip", ex3, "game_exception");
			}
		}

		// Token: 0x06008D1A RID: 36122 RVA: 0x00322D30 File Offset: 0x00320F30
		private bool Equip(GameObject Object, BodyPart Part, List<GameObject> PrimaryActiveList, List<GameObject> SecondaryActiveList, List<GameObject> TertiaryActiveList, ref int LightSources, ref GameObject Shield, bool Silent = false, bool Initial = true)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			bool flag = Object.EquipAsDefaultBehavior();
			GameObject gameObject;
			if (flag)
			{
				gameObject = Part.DefaultBehavior;
			}
			else
			{
				gameObject = Part.Equipped;
				if (gameObject != null)
				{
					if (gameObject == Object)
					{
						return false;
					}
					if (gameObject.SameAs(Object))
					{
						return false;
					}
					Part.TryUnequip(Silent, false, false, false);
				}
				if (Part.Equipped != null)
				{
					return false;
				}
			}
			if (Part.DefaultBehavior != null)
			{
				if (Part.DefaultBehavior == Object)
				{
					return false;
				}
				if (Part.DefaultBehavior.SameAs(Object))
				{
					return false;
				}
			}
			Event @event = (Initial ? Brain.eCommandEquipObjectFree : Brain.eCommandEquipObject);
			@event.SetParameter("Object", Object);
			@event.SetParameter("BodyPart", Part);
			@event.SetSilent(Silent);
			bool flag2 = this.ParentObject.FireEvent(@event);
			if (flag2)
			{
				GameObject gameObject2 = (flag ? Part.DefaultBehavior : Part.Equipped);
				if (gameObject2 != null && gameObject2 != gameObject)
				{
					if (gameObject2.HasPartDescendedFrom<ILightSource>())
					{
						LightSources++;
					}
					if (gameObject2.HasPart<Shield>())
					{
						Shield = gameObject2;
					}
					if (Object == gameObject2)
					{
						if (PrimaryActiveList != null)
						{
							PrimaryActiveList.Remove(Object);
						}
						if (SecondaryActiveList != null)
						{
							SecondaryActiveList.Remove(Object);
						}
						if (TertiaryActiveList != null)
						{
							TertiaryActiveList.Remove(Object);
						}
					}
					if (gameObject != null)
					{
						if (flag)
						{
							if (GameObject.Validate(ref gameObject))
							{
								gameObject.Obliterate(null, false, null);
								return flag2;
							}
						}
						else if (GameObject.Validate(ref gameObject))
						{
							if (gameObject.InInventory == this.ParentObject)
							{
								if (PrimaryActiveList != null)
								{
									PrimaryActiveList.Add(gameObject);
								}
								if (SecondaryActiveList != null)
								{
									SecondaryActiveList.Add(gameObject);
								}
								if (TertiaryActiveList != null)
								{
									TertiaryActiveList.Add(gameObject);
								}
							}
							if (gameObject.HasPartDescendedFrom<ILightSource>())
							{
								LightSources--;
							}
							if (gameObject.HasPart<Shield>() && gameObject == Shield)
							{
								Shield = null;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06008D1B RID: 36123 RVA: 0x00322ED0 File Offset: 0x003210D0
		public Cell MovingTo()
		{
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				MoveTo moveTo = this.Goals.Items[i] as MoveTo;
				if (moveTo != null)
				{
					Cell destinationCell = moveTo.GetDestinationCell();
					if (destinationCell != null)
					{
						return destinationCell;
					}
				}
			}
			for (int j = this.Goals.Items.Count - 1; j >= 0; j--)
			{
				Step step = this.Goals.Items[j] as Step;
				if (step != null)
				{
					Cell destinationCell2 = step.GetDestinationCell();
					if (destinationCell2 != null)
					{
						return destinationCell2;
					}
				}
			}
			return null;
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x00322F6C File Offset: 0x0032116C
		public bool IsFleeing()
		{
			for (int i = this.Goals.Items.Count - 1; i >= 0; i--)
			{
				if (this.Goals.Items[i].IsFleeing())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x00322FB4 File Offset: 0x003211B4
		public bool IsFactionMember(string Faction)
		{
			int num;
			return this.Allegiance.TryGetValue(Faction, out num) && num > 0;
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x00322FD8 File Offset: 0x003211D8
		public void WantToReequip()
		{
			if (!this.ParentObject.IsPlayer())
			{
				this.DoReequip = true;
			}
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x00322FF0 File Offset: 0x003211F0
		public static bool PartyMemberOrder(KeyValuePair<int, PartyMember> KV)
		{
			GameObject gameObject = KV.Value.Reference.Object ?? GameObject.FindByID(KV.Key);
			return !GameObject.Validate(ref gameObject) || KV.Value.Flags.HasBit(8388608);
		}

		// Token: 0x06008D20 RID: 36128 RVA: 0x00323045 File Offset: 0x00321245
		[WishCommand("allfeelings", null)]
		public static void WishAllFeelings()
		{
			Brain.WriteFeelingSamples(false);
		}

		// Token: 0x06008D21 RID: 36129 RVA: 0x0032304D File Offset: 0x0032124D
		[WishCommand("allfeelinglevels", null)]
		public static void WishAllFeelingLevels()
		{
			Brain.WriteFeelingSamples(true);
		}

		// Token: 0x06008D22 RID: 36130 RVA: 0x00323058 File Offset: 0x00321258
		private static void WriteFeelingSamples(bool Level = false)
		{
			try
			{
				List<GameObjectBlueprint> blueprintList = GameObjectFactory.Factory.BlueprintList;
				List<ValueTuple<GameObjectBlueprint, string>> list = new List<ValueTuple<GameObjectBlueprint, string>>();
				foreach (GameObjectBlueprint gameObjectBlueprint in blueprintList)
				{
					if (gameObjectBlueprint.HasPart("Brain") && !gameObjectBlueprint.IsBaseBlueprint())
					{
						Loading.SetLoadingStatus("Sampling " + gameObjectBlueprint.Name + "...", false);
						GameObject gameObject = null;
						string text;
						try
						{
							gameObject = gameObjectBlueprint.createSample();
							text = (Level ? Enum.GetName(typeof(Brain.FeelingLevel), gameObject.Brain.GetFeelingLevel(The.Player)) : gameObject.Brain.GetFeeling(The.Player).ToString());
						}
						catch (Exception)
						{
							text = "invalid";
						}
						list.Add(new ValueTuple<GameObjectBlueprint, string>(gameObjectBlueprint, text));
						if (gameObject != null)
						{
							gameObject.Pool();
						}
					}
				}
				list.Sort(([TupleElementNames(new string[] { "Blueprint", "Value" })] ValueTuple<GameObjectBlueprint, string> a, [TupleElementNames(new string[] { "Blueprint", "Value" })] ValueTuple<GameObjectBlueprint, string> b) => string.Compare(a.Item1.Name, b.Item1.Name, StringComparison.Ordinal));
				string text2 = (Level ? "AllFeelingLevels.txt" : "AllFeelings.txt");
				string text3 = DataManager.SavePath("Trash/Text/");
				Directory.CreateDirectory(text3);
				using (StreamWriter streamWriter = new StreamWriter(text3 + text2))
				{
					foreach (ValueTuple<GameObjectBlueprint, string> valueTuple in list)
					{
						streamWriter.WriteLine(valueTuple.Item1.Name + ": " + valueTuple.Item2);
					}
				}
				Popup.Show(string.Concat(new string[]
				{
					list.Count.ToString(),
					" feelings written to ",
					text2,
					" in ",
					text3,
					"!"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			finally
			{
				Loading.SetLoadingStatus(null, false);
			}
		}

		// Token: 0x04003AFD RID: 15101
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool BrainPool = new IPartPool(64);

		/// <todo>Tweak thresholds to have a better feeling spread.</todo>
		// Token: 0x04003AFE RID: 15102
		public const int FEELING_HOSTILE_THRESHOLD = -10;

		// Token: 0x04003AFF RID: 15103
		public const int FEELING_ALLIED_THRESHOLD = 50;

		// Token: 0x04003B00 RID: 15104
		public static readonly int DEFAULT_MIN_KILL_RADIUS = 5;

		// Token: 0x04003B01 RID: 15105
		public static readonly int DEFAULT_MAX_KILL_RADIUS = 15;

		// Token: 0x04003B02 RID: 15106
		public static readonly int DEFAULT_HOSTILE_WALK_RADIUS = 84;

		// Token: 0x04003B03 RID: 15107
		public static readonly int DEFAULT_MAX_WANDER_RADIUS = 12;

		// Token: 0x04003B04 RID: 15108
		public const int PARTY_PROSELYTIZED = 1;

		// Token: 0x04003B05 RID: 15109
		public const int PARTY_BEGUILED = 2;

		// Token: 0x04003B06 RID: 15110
		public const int PARTY_BONDED = 4;

		// Token: 0x04003B07 RID: 15111
		public const int PARTY_REBUKED = 8;

		// Token: 0x04003B08 RID: 15112
		public const int PARTY_INDEPENDENT = 8388608;

		// Token: 0x04003B09 RID: 15113
		public const int PARTY_SUPPORT = 15;

		// Token: 0x04003B0A RID: 15114
		public const int BRAIN_FLAG_HOSTILE = 1;

		// Token: 0x04003B0B RID: 15115
		public const int BRAIN_FLAG_CALM = 2;

		// Token: 0x04003B0C RID: 15116
		public const int BRAIN_FLAG_WANDERS = 4;

		// Token: 0x04003B0D RID: 15117
		public const int BRAIN_FLAG_WANDERS_RANDOMLY = 8;

		// Token: 0x04003B0E RID: 15118
		public const int BRAIN_FLAG_AQUATIC = 16;

		// Token: 0x04003B0F RID: 15119
		public const int BRAIN_FLAG_LIVES_ON_WALLS = 32;

		// Token: 0x04003B10 RID: 15120
		public const int BRAIN_FLAG_WALL_WALKER = 64;

		// Token: 0x04003B11 RID: 15121
		public const int BRAIN_FLAG_MOBILE = 128;

		// Token: 0x04003B12 RID: 15122
		public const int BRAIN_FLAG_HIBERNATING = 256;

		// Token: 0x04003B13 RID: 15123
		public const int BRAIN_FLAG_POINT_BLANK_RANGE = 512;

		// Token: 0x04003B14 RID: 15124
		public const int BRAIN_FLAG_DO_REEQUIP = 1024;

		// Token: 0x04003B15 RID: 15125
		public const int BRAIN_FLAG_NEED_TO_RELOAD = 2048;

		// Token: 0x04003B16 RID: 15126
		public const int BRAIN_FLAG_STAYING = 4096;

		// Token: 0x04003B17 RID: 15127
		public const int BRAIN_FLAG_PASSIVE = 8192;

		// Token: 0x04003B18 RID: 15128
		public const int BRAIN_FLAG_DO_PRIMARY_CHOICE_ON_REEQUIP = 16384;

		// Token: 0x04003B19 RID: 15129
		public int Flags = 17792;

		// Token: 0x04003B1A RID: 15130
		public int MaxMissileRange = 80;

		// Token: 0x04003B1B RID: 15131
		public int MinKillRadius = Brain.DEFAULT_MIN_KILL_RADIUS;

		// Token: 0x04003B1C RID: 15132
		public int MaxKillRadius = Brain.DEFAULT_MAX_KILL_RADIUS;

		// Token: 0x04003B1D RID: 15133
		public int HostileWalkRadius = Brain.DEFAULT_HOSTILE_WALK_RADIUS;

		// Token: 0x04003B1E RID: 15134
		public int MaxWanderRadius = Brain.DEFAULT_MAX_WANDER_RADIUS;

		// Token: 0x04003B1F RID: 15135
		private GameObjectReference LeaderReference;

		// Token: 0x04003B20 RID: 15136
		public GlobalLocation StartingCell;

		// Token: 0x04003B21 RID: 15137
		[NonSerialized]
		public string LastThought;

		// Token: 0x04003B22 RID: 15138
		public AllegianceSet Allegiance = new AllegianceSet();

		// Token: 0x04003B23 RID: 15139
		[NonSerialized]
		public StringMap<int> FactionFeelings = new StringMap<int>();

		// Token: 0x04003B24 RID: 15140
		public PartyCollection PartyMembers = new PartyCollection();

		// Token: 0x04003B25 RID: 15141
		public OpinionMap Opinions = new OpinionMap();

		// Token: 0x04003B26 RID: 15142
		[NonSerialized]
		public CleanStack<GoalHandler> Goals = new CleanStack<GoalHandler>();

		// Token: 0x04003B27 RID: 15143
		[NonSerialized]
		public Dictionary<GameObject, int> FriendlyFire;

		// Token: 0x04003B28 RID: 15144
		[NonSerialized]
		private static Event eAITakingAction = new ImmutableEvent("AITakingAction");

		// Token: 0x04003B29 RID: 15145
		[NonSerialized]
		private static Event eCommandEquipObject = new Event("CommandEquipObject", "Object", null, "BodyPart", null);

		// Token: 0x04003B2A RID: 15146
		[NonSerialized]
		private static Event eCommandEquipObjectFree = new Event("CommandEquipObject", "Object", null, "BodyPart", null, "EnergyCost", 0);

		// Token: 0x04003B2B RID: 15147
		[NonSerialized]
		private static Event eFactionsAdded = new ImmutableEvent("FactionsAdded");

		// Token: 0x04003B2C RID: 15148
		[NonSerialized]
		private static Event eTakingAction = new ImmutableEvent("TakingAction");

		// Token: 0x04003B2D RID: 15149
		private static Dictionary<string, int> ProcFactions = new Dictionary<string, int>();

		// Token: 0x04003B2E RID: 15150
		private static readonly ProfilerMarker s_BrainCommandTakeActionEvent = new ProfilerMarker("s_BrainCommandTakeActionEvent");

		// Token: 0x04003B2F RID: 15151
		[NonSerialized]
		public static List<GameObject> objectsToRemove = new List<GameObject>();

		// Token: 0x04003B30 RID: 15152
		[NonSerialized]
		private static Brain.GearSorter SharedGearSorter = new Brain.GearSorter();

		// Token: 0x04003B31 RID: 15153
		[NonSerialized]
		private static Brain.WeaponSorter SharedWeaponSorter = new Brain.WeaponSorter();

		// Token: 0x02001C3E RID: 7230
		public enum FeelingLevel
		{
			// Token: 0x04006F98 RID: 28568
			Hostile,
			// Token: 0x04006F99 RID: 28569
			Neutral,
			// Token: 0x04006F9A RID: 28570
			Allied
		}

		// Token: 0x02001C3F RID: 7231
		public enum AllegianceLevel
		{
			// Token: 0x04006F9C RID: 28572
			None,
			// Token: 0x04006F9D RID: 28573
			Associated,
			// Token: 0x04006F9E RID: 28574
			Affiliated,
			// Token: 0x04006F9F RID: 28575
			Member
		}

		// Token: 0x02001C40 RID: 7232
		public class WeaponSorter : Comparer<GameObject>
		{
			// Token: 0x0600F091 RID: 61585 RVA: 0x0054A741 File Offset: 0x00548941
			public bool IsValid()
			{
				return GameObject.Validate(ref this.POV);
			}

			// Token: 0x0600F092 RID: 61586 RVA: 0x0054A74E File Offset: 0x0054894E
			public WeaponSorter()
			{
			}

			// Token: 0x0600F093 RID: 61587 RVA: 0x0054A756 File Offset: 0x00548956
			public WeaponSorter(GameObject POV)
				: this()
			{
				this.Setup(POV, false);
			}

			// Token: 0x0600F094 RID: 61588 RVA: 0x0054A766 File Offset: 0x00548966
			public WeaponSorter(GameObject POV, bool Reverse)
				: this()
			{
				this.Setup(POV, Reverse);
			}

			// Token: 0x0600F095 RID: 61589 RVA: 0x0054A776 File Offset: 0x00548976
			public void Setup(GameObject POV = null, bool Reverse = false)
			{
				this.POV = POV;
				this.Reverse = Reverse;
			}

			// Token: 0x0600F096 RID: 61590 RVA: 0x0054A786 File Offset: 0x00548986
			public override int Compare(GameObject Object1, GameObject Object2)
			{
				return Brain.CompareWeapons(Object1, Object2, this.POV) * (this.Reverse ? (-1) : 1);
			}

			// Token: 0x04006FA0 RID: 28576
			private GameObject POV;

			// Token: 0x04006FA1 RID: 28577
			private bool Reverse;
		}

		// Token: 0x02001C41 RID: 7233
		public class MissileWeaponSorter : Comparer<GameObject>
		{
			// Token: 0x0600F097 RID: 61591 RVA: 0x0054A7A2 File Offset: 0x005489A2
			public MissileWeaponSorter(GameObject POV)
			{
				this.POV = POV;
			}

			// Token: 0x0600F098 RID: 61592 RVA: 0x0054A7B1 File Offset: 0x005489B1
			public MissileWeaponSorter(GameObject POV, bool Reverse)
				: this(POV)
			{
				this.Reverse = Reverse;
			}

			// Token: 0x0600F099 RID: 61593 RVA: 0x0054A7C1 File Offset: 0x005489C1
			public override int Compare(GameObject o1, GameObject o2)
			{
				return Brain.CompareMissileWeapons(o1, o2, this.POV) * (this.Reverse ? (-1) : 1);
			}

			// Token: 0x04006FA2 RID: 28578
			private GameObject POV;

			// Token: 0x04006FA3 RID: 28579
			private bool Reverse;
		}

		// Token: 0x02001C42 RID: 7234
		public class GearSorter : Comparer<GameObject>
		{
			// Token: 0x0600F09A RID: 61594 RVA: 0x0054A7DD File Offset: 0x005489DD
			public bool IsValid()
			{
				return GameObject.Validate(ref this.POV);
			}

			// Token: 0x0600F09B RID: 61595 RVA: 0x0054A7EA File Offset: 0x005489EA
			public GearSorter()
			{
			}

			// Token: 0x0600F09C RID: 61596 RVA: 0x0054A7F2 File Offset: 0x005489F2
			public GearSorter(GameObject POV)
				: this()
			{
				this.Setup(POV, false);
			}

			// Token: 0x0600F09D RID: 61597 RVA: 0x0054A802 File Offset: 0x00548A02
			public GearSorter(GameObject POV, bool Reverse)
				: this()
			{
				this.Setup(POV, Reverse);
			}

			// Token: 0x0600F09E RID: 61598 RVA: 0x0054A812 File Offset: 0x00548A12
			public void Setup(GameObject POV = null, bool Reverse = false)
			{
				this.POV = POV;
				this.Reverse = Reverse;
			}

			// Token: 0x0600F09F RID: 61599 RVA: 0x0054A822 File Offset: 0x00548A22
			public override int Compare(GameObject Object1, GameObject Object2)
			{
				return Brain.CompareGear(Object1, Object2, this.POV) * (this.Reverse ? (-1) : 1);
			}

			// Token: 0x04006FA4 RID: 28580
			private GameObject POV;

			// Token: 0x04006FA5 RID: 28581
			private bool Reverse;
		}
	}
}
