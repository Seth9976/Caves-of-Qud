using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F6 RID: 5110
	[Serializable]
	public class Pyrokinesis : BaseMutation
	{
		// Token: 0x0600C610 RID: 50704 RVA: 0x00481E30 File Offset: 0x00480030
		public Pyrokinesis()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C611 RID: 50705 RVA: 0x00481E64 File Offset: 0x00480064
		public static void CollectProxyStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			stats.Set("DamageRound1", string.Format("{0}d3 / 2", Level), !flag, 0);
			stats.Set("DamageRound2", string.Format("{0}d4 / 2", Level), !flag, 0);
			stats.Set("DamageRound3", string.Format("{0}d6 / 2", Level), !flag, 0);
			stats.Set("Area", "3x3", false, 0);
			stats.Set("Range", Pyrokinesis.DEFAULT_RANGE, false, 0);
		}

		// Token: 0x0600C612 RID: 50706 RVA: 0x00481F08 File Offset: 0x00480108
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			base.CollectStats(stats, Level);
			Pyrokinesis.CollectProxyStats(stats, Level);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 50, 0);
			stats.Set("Intro", (base.Level == Level) ? "Toasts affected area over 3 rounds, dealing damage and setting things on fire\n" : "{{rules|Increased toast temperature intensity}}\n", false, 0);
		}

		// Token: 0x0600C613 RID: 50707 RVA: 0x00481F5C File Offset: 0x0048015C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C614 RID: 50708 RVA: 0x00481F7C File Offset: 0x0048017C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.Range && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && !E.Target.IsInStasis())
			{
				E.Add(Pyrokinesis.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C615 RID: 50709 RVA: 0x00481FD8 File Offset: 0x004801D8
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Pyrokinesis.COMMAND_NAME) || Pyrokinesis.Cast(this, "5-6", null, null, null, 0)) && base.HandleEvent(E);
		}

		// Token: 0x0600C616 RID: 50710 RVA: 0x00482021 File Offset: 0x00480221
		public override string GetDescription()
		{
			return "You heat a nearby area with your mind.";
		}

		// Token: 0x0600C617 RID: 50711 RVA: 0x00482028 File Offset: 0x00480228
		public override string GetLevelText(int Level)
		{
			string text;
			if (Level == base.Level)
			{
				text = "Toasts affected area over " + this.Duration.Things("round", null) + "\n";
			}
			else if (Level > base.Level)
			{
				text = "{{rules|Increased toast temperature intensity}}\n";
			}
			else
			{
				text = "{{rules|Decreased toast temperature intensity}}\n";
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
					Pyrokinesis.GetDamageDieSize(i).ToString(),
					"}} divided by 2\n"
				});
			}
			return text + "Cooldown: 50 rounds";
		}

		// Token: 0x0600C618 RID: 50712 RVA: 0x00482154 File Offset: 0x00480354
		public static int GetDamageDieSize(int Round)
		{
			int num;
			if (Round != 1)
			{
				if (Round != 2)
				{
					num = 6;
				}
				else
				{
					num = 4;
				}
			}
			else
			{
				num = 3;
			}
			return num;
		}

		// Token: 0x0600C619 RID: 50713 RVA: 0x00482178 File Offset: 0x00480378
		public static List<GameObject> Pyro(GameObject Actor, int Level, List<Cell> Cells, int? Duration = null, int Phase = 0, bool Dependent = false)
		{
			int num = Duration ?? Pyrokinesis.DEFAULT_DURATION;
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			if (Actor != null)
			{
				Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_pyrokinesis_active", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Cells.ShuffleInPlace<Cell>(null).Sort((Cell a, Cell b) => b.GetTotalDistance(Cells).CompareTo(a.GetTotalDistance(Cells)));
			if (Phase == 0)
			{
				Phase = ((Actor != null) ? Actor.GetPhase() : 1);
			}
			List<GameObject> list = new List<GameObject>(Cells.Count);
			int nextGroupID = Pyrokinesis.GetNextGroupID();
			PyroZone pyroZone = null;
			foreach (Cell cell in Cells)
			{
				GameObject gameObject = GameObject.Create(Pyrokinesis.FIELD_BLUEPRINT);
				PyroZone pyroZone2;
				if (gameObject.TryGetPart<PyroZone>(out pyroZone2))
				{
					pyroZone2.Level = Level;
					pyroZone2.Owner = Actor;
					pyroZone2.Duration = num;
					pyroZone2.GroupID = nextGroupID;
					if (Dependent)
					{
						pyroZone2.DependsOn = ((Actor != null) ? Actor.ID : null);
					}
					if (pyroZone == null)
					{
						pyroZone2.Control = true;
						pyroZone = pyroZone2;
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
				scrapBuffer.Write("&" + Phase.getRandomHeatColor(Phase).ToString() + "*", true, false, false, null, -1);
				textConsole.DrawBuffer(scrapBuffer, null, false);
				Thread.Sleep(25);
			}
			if (pyroZone != null)
			{
				pyroZone.Started();
			}
			return list;
		}

		// Token: 0x0600C61A RID: 50714 RVA: 0x004823B0 File Offset: 0x004805B0
		public static bool Cast(Pyrokinesis Mutation = null, string Level = "5-6", GameObject Actor = null, int? Range = null, int? Radius = null, int Phase = 0)
		{
			if (Actor == null)
			{
				Actor = ((Mutation != null) ? Mutation.ParentObject : null) ?? The.Player;
			}
			bool flag = false;
			if (Mutation == null)
			{
				flag = true;
				Mutation = new Pyrokinesis();
				Mutation.Level = Level.RollCached();
				Mutation.Range = Range ?? Pyrokinesis.DEFAULT_RANGE;
				Mutation.Radius = Radius ?? Pyrokinesis.DEFAULT_RADIUS;
				Mutation.ParentObject = Actor;
			}
			if (Actor.OnWorldMap())
			{
				if (!flag)
				{
					Actor.Fail("You cannot do that on the world map.");
				}
				return false;
			}
			List<Cell> list = Mutation.PickBurst(Mutation.Radius, Mutation.Range, false, AllowVis.OnlyVisible, "Toast");
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
			Mutation.UseEnergy(1000, "Mental Mutation Pyrokinesis");
			Mutation.CooldownMyActivatedAbility(Mutation.ActivatedAbilityID, 50, null, null, false);
			Pyrokinesis.Pyro(Actor, Mutation.Level, list, new int?(Mutation.Duration), 0, false);
			return true;
		}

		// Token: 0x0600C61B RID: 50715 RVA: 0x00482530 File Offset: 0x00480730
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Toast", Pyrokinesis.COMMAND_NAME, "Mental Mutations", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C61C RID: 50716 RVA: 0x00482579 File Offset: 0x00480779
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C61D RID: 50717 RVA: 0x00482590 File Offset: 0x00480790
		public static int GetNextGroupID()
		{
			if (Pyrokinesis.GroupID == 0)
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
									PyroZone pyroZone;
									if (gameObject.Blueprint == Pyrokinesis.FIELD_BLUEPRINT && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID >= Pyrokinesis.GroupID)
									{
										Pyrokinesis.GroupID = pyroZone.GroupID;
									}
									k++;
								}
							}
						}
					}
				}
			}
			return ++Pyrokinesis.GroupID;
		}

		// Token: 0x04004E21 RID: 20001
		public static readonly int DEFAULT_RANGE = 8;

		// Token: 0x04004E22 RID: 20002
		public static readonly int DEFAULT_DURATION = 3;

		// Token: 0x04004E23 RID: 20003
		public static readonly int DEFAULT_RADIUS = 1;

		// Token: 0x04004E24 RID: 20004
		public static readonly string COMMAND_NAME = "CommandPyrokinesis";

		// Token: 0x04004E25 RID: 20005
		public static readonly string FIELD_BLUEPRINT = "Shimmering Heat";

		// Token: 0x04004E26 RID: 20006
		public int Duration = Pyrokinesis.DEFAULT_DURATION;

		// Token: 0x04004E27 RID: 20007
		public int Range = Pyrokinesis.DEFAULT_RANGE;

		// Token: 0x04004E28 RID: 20008
		public int Radius = Pyrokinesis.DEFAULT_RADIUS;

		// Token: 0x04004E29 RID: 20009
		public static int GroupID;
	}
}
