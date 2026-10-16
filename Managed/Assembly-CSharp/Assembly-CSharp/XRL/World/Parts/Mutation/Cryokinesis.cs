using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E5 RID: 5093
	[Serializable]
	public class Cryokinesis : BaseMutation
	{
		// Token: 0x0600C4DE RID: 50398 RVA: 0x0047A7A0 File Offset: 0x004789A0
		public Cryokinesis()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C4DF RID: 50399 RVA: 0x0047A7D4 File Offset: 0x004789D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C4E0 RID: 50400 RVA: 0x0047A7FC File Offset: 0x004789FC
		public static void CollectProxyStats(Templates.StatCollector stats, int Level, int Radius, int Range)
		{
			bool flag = stats.mode.Contains("ability");
			stats.Set("DamageRound1", string.Format("{0}d{1} / 2", Level, Cryokinesis.GetDamageDieSize(1)), !flag, 0);
			stats.Set("DamageRound2", string.Format("{0}d{1} / 2", Level, Cryokinesis.GetDamageDieSize(2)), !flag, 0);
			stats.Set("DamageRound3", string.Format("{0}d{1} / 2", Level, Cryokinesis.GetDamageDieSize(3)), !flag, 0);
			int num = Radius * 2 + 1;
			stats.Set("Area", string.Format("{0}x{1}", num, num), false, 0);
			stats.Set("Range", Range, false, 0);
		}

		// Token: 0x0600C4E1 RID: 50401 RVA: 0x0047A8D2 File Offset: 0x00478AD2
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			base.CollectStats(stats, Level);
			Cryokinesis.CollectProxyStats(stats, Level, this.Radius, this.Range);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 50, 0);
		}

		// Token: 0x0600C4E2 RID: 50402 RVA: 0x0047A905 File Offset: 0x00478B05
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.Range && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(Cryokinesis.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4E3 RID: 50403 RVA: 0x0047A93C File Offset: 0x00478B3C
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Cryokinesis.COMMAND_NAME) || Cryokinesis.Cast(this, "5-6", null, null, null, 0)) && base.HandleEvent(E);
		}

		// Token: 0x0600C4E4 RID: 50404 RVA: 0x0047A985 File Offset: 0x00478B85
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4E5 RID: 50405 RVA: 0x0047A9AD File Offset: 0x00478BAD
		public override string GetDescription()
		{
			return "You chill a nearby area with your mind.";
		}

		// Token: 0x0600C4E6 RID: 50406 RVA: 0x0047A9B4 File Offset: 0x00478BB4
		public override string GetLevelText(int Level)
		{
			string text;
			if (Level == base.Level)
			{
				text = "Chills affected area over " + this.Duration.Things("round", null) + ", dealing damage and freezing things\n";
			}
			else if (Level > base.Level)
			{
				text = "{{rules|Increased chill temperature intensity}}\n";
			}
			else
			{
				text = "{{rules|Decreased chill temperature intensity}}\n";
			}
			text = text + "Range: " + this.Range.ToString() + "\n";
			int num = this.Radius * 2 + 1;
			text = string.Concat(new string[]
			{
				text,
				"Area: ",
				num.ToString(),
				"x",
				num.ToString(),
				"\n"
			});
			for (int i = 1; i <= this.Duration; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					"Round ",
					i.ToString(),
					" damage: {{rules|",
					Level.ToString(),
					"d",
					Cryokinesis.GetDamageDieSize(i).ToString(),
					"}} divided by 2\n"
				});
			}
			return text + "Cooldown: 50 rounds";
		}

		// Token: 0x0600C4E7 RID: 50407 RVA: 0x0047AAE0 File Offset: 0x00478CE0
		public static int GetDamageDieSize(int Round)
		{
			int num;
			if (Round != 1)
			{
				if (Round != 2)
				{
					num = 4;
				}
				else
				{
					num = 3;
				}
			}
			else
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x0600C4E8 RID: 50408 RVA: 0x0047AB04 File Offset: 0x00478D04
		public static List<GameObject> Cryo(GameObject Actor, int Level, List<Cell> Cells, int? Duration = null, int Phase = 0, bool Dependent = false)
		{
			int num = Duration ?? Cryokinesis.DEFAULT_DURATION;
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			if (Actor != null)
			{
				Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_cryokinesis_active", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Cells.ShuffleInPlace<Cell>(null).Sort((Cell a, Cell b) => b.GetTotalDistance(Cells).CompareTo(a.GetTotalDistance(Cells)));
			if (Phase == 0)
			{
				Phase = ((Actor != null) ? Actor.GetPhase() : 1);
			}
			List<GameObject> list = new List<GameObject>(Cells.Count);
			int nextGroupID = Cryokinesis.GetNextGroupID();
			CryoZone cryoZone = null;
			foreach (Cell cell in Cells)
			{
				GameObject gameObject = GameObject.Create(Cryokinesis.FIELD_BLUEPRINT);
				CryoZone cryoZone2;
				if (gameObject.TryGetPart<CryoZone>(out cryoZone2))
				{
					cryoZone2.Level = Level;
					cryoZone2.Owner = Actor;
					cryoZone2.Duration = num;
					cryoZone2.GroupID = nextGroupID;
					if (Dependent)
					{
						cryoZone2.DependsOn = ((Actor != null) ? Actor.ID : null);
					}
					if (cryoZone == null)
					{
						cryoZone2.Control = true;
						cryoZone = cryoZone2;
					}
				}
				else if (Dependent && Actor != null)
				{
					gameObject.RequirePart<ExistenceSupport>(false).SupportedBy = Actor;
				}
				Phase.carryOver(Phase, gameObject, 9999);
				list.Add(cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null));
				if (!cell.IsVisible())
				{
					GameObject player = The.Player;
					if (player == null || !player.WithinSensePsychicRange(cell))
					{
						continue;
					}
				}
				scrapBuffer.Goto(cell.X, cell.Y);
				scrapBuffer.Write("&" + Phase.getRandomColdColor(Phase).ToString() + "*", true, false, false, null, -1);
				textConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(25);
			}
			if (cryoZone != null)
			{
				cryoZone.Started();
			}
			return list;
		}

		// Token: 0x0600C4E9 RID: 50409 RVA: 0x0047AD3C File Offset: 0x00478F3C
		public static bool Cast(Cryokinesis Mutation = null, string Level = "5-6", GameObject Actor = null, int? Range = null, int? Radius = null, int Phase = 0)
		{
			if (Actor == null)
			{
				Actor = ((Mutation != null) ? Mutation.ParentObject : null) ?? The.Player;
			}
			bool flag = false;
			if (Mutation == null)
			{
				flag = true;
				Mutation = new Cryokinesis();
				Mutation.Level = Level.RollCached();
				Mutation.Range = Range ?? Cryokinesis.DEFAULT_RANGE;
				Mutation.Radius = Radius ?? Cryokinesis.DEFAULT_RADIUS;
				Mutation.ParentObject = Actor;
			}
			if (Mutation.ParentObject.OnWorldMap())
			{
				if (!flag)
				{
					Actor.Fail("You cannot do that on the world map.");
				}
				return false;
			}
			List<Cell> list = Mutation.PickBurst(Mutation.Radius, Mutation.Range, false, AllowVis.OnlyVisible, "Chill");
			if (list == null || list.Count == 0)
			{
				return false;
			}
			using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.DistanceTo(Actor) - 1 > Mutation.Range)
					{
						Actor.Fail("That is out of range! (" + Mutation.Range.Things("square", null) + ")");
						return false;
					}
				}
			}
			Mutation.UseEnergy(1000, "Mental Mutation Cryokinesis");
			Mutation.CooldownMyActivatedAbility(Mutation.ActivatedAbilityID, 50, null, null, false);
			Cryokinesis.Cryo(Actor, Mutation.Level, list, new int?(Mutation.Duration), Phase, false);
			return true;
		}

		// Token: 0x0600C4EA RID: 50410 RVA: 0x0047AEC0 File Offset: 0x004790C0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Chill", Cryokinesis.COMMAND_NAME, "Mental Mutations", null, "\u000f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4EB RID: 50411 RVA: 0x0047AF09 File Offset: 0x00479109
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C4EC RID: 50412 RVA: 0x0047AF20 File Offset: 0x00479120
		public static int GetNextGroupID()
		{
			if (Cryokinesis.GroupID == 0)
			{
				foreach (Zone zone in The.ZoneManager.CachedZones.Values)
				{
					for (int i = 0; i < zone.Height; i++)
					{
						for (int j = 0; j < zone.Width; j++)
						{
							Cell cell = zone.GetCell(j, i);
							if (cell != null)
							{
								int k = 0;
								int count = cell.Objects.Count;
								while (k < count)
								{
									GameObject gameObject = cell.Objects[k];
									CryoZone cryoZone;
									if (gameObject.Blueprint == Cryokinesis.FIELD_BLUEPRINT && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID >= Cryokinesis.GroupID)
									{
										Cryokinesis.GroupID = cryoZone.GroupID;
									}
									k++;
								}
							}
						}
					}
				}
			}
			return ++Cryokinesis.GroupID;
		}

		// Token: 0x04004DE1 RID: 19937
		public static readonly int DEFAULT_RANGE = 8;

		// Token: 0x04004DE2 RID: 19938
		public static readonly int DEFAULT_DURATION = 3;

		// Token: 0x04004DE3 RID: 19939
		public static readonly int DEFAULT_RADIUS = 1;

		// Token: 0x04004DE4 RID: 19940
		public static readonly string COMMAND_NAME = "CommandCryokinesis";

		// Token: 0x04004DE5 RID: 19941
		public static readonly string FIELD_BLUEPRINT = "Frigid Mist";

		// Token: 0x04004DE6 RID: 19942
		public int Duration = Cryokinesis.DEFAULT_DURATION;

		// Token: 0x04004DE7 RID: 19943
		public int Range = Cryokinesis.DEFAULT_RANGE;

		// Token: 0x04004DE8 RID: 19944
		public int Radius = Cryokinesis.DEFAULT_RADIUS;

		// Token: 0x04004DE9 RID: 19945
		public static int GroupID;
	}
}
