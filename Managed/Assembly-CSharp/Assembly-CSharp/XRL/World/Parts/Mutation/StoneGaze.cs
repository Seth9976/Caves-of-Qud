using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001427 RID: 5159
	[Serializable]
	public class StoneGaze : IDelayedLineMutation
	{
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600C90A RID: 51466 RVA: 0x00493F7D File Offset: 0x0049217D
		public override string Command
		{
			get
			{
				return "CommandStoneGaze";
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600C90B RID: 51467 RVA: 0x00493F84 File Offset: 0x00492184
		public override bool CanRefract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600C90C RID: 51468 RVA: 0x00493F87 File Offset: 0x00492187
		public override GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref StoneGaze._Projectile))
				{
					StoneGaze._Projectile = GameObject.CreateUnmodified("ProjectileStoneGaze");
				}
				return StoneGaze._Projectile;
			}
		}

		// Token: 0x0600C90D RID: 51469 RVA: 0x00493FAC File Offset: 0x004921AC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", this.GetRange(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C90F RID: 51471 RVA: 0x00494004 File Offset: 0x00492204
		public static GameObject CreateStatueOf(GameObject source)
		{
			GameObject gameObject = GameObject.Create("Random Stone Statue");
			gameObject.RequirePart<RandomStatue>(false).SetCreature(source.DeepCopy(false, false, null));
			if (Stat.Random(1, 100) <= 50)
			{
				gameObject.SetStringProperty("QuestVerb", "pray at", false);
				gameObject.SetStringProperty("QuestEvent", "Prayed", false);
			}
			else
			{
				gameObject.SetStringProperty("QuestVerb", "desecrate", false);
				gameObject.SetStringProperty("QuestEvent", "Desecrated", false);
			}
			gameObject.RequirePart<Shrine>(false);
			return gameObject;
		}

		// Token: 0x0600C910 RID: 51472 RVA: 0x0049408C File Offset: 0x0049228C
		public override void FireLine(List<Cell> Path)
		{
			base.PlayWorldSound("burn_blast", 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			List<GameObject> list = Event.NewGameObjectList();
			bool flag = false;
			int i = 0;
			int count = Path.Count;
			while (i < count)
			{
				Cell cell = Path[i];
				if (cell.IsBlackedOut())
				{
					break;
				}
				foreach (GameObject gameObject in cell.Objects)
				{
					if (this.IsValidTarget(gameObject) && !gameObject.MakeSave("Toughness", 20, this.ParentObject, "Ego", "Lithofex Stoning Gaze Beam", false, false, false, false, null))
					{
						list.Add(gameObject);
					}
				}
				int j = 0;
				while (j < list.Count)
				{
					GameObject gameObject2 = list[j];
					if (gameObject2.IsPlayer())
					{
						Achievement.TURNED_STONE.Unlock();
					}
					gameObject2.SetIntProperty("SuppressCorpseDrops", 1, false);
					if (gameObject2.Die(this.ParentObject, null, "You were turned to stone by the gaze of " + this.ParentObject.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", string.Concat(new string[]
					{
						gameObject2.It,
						gameObject2.GetVerb("were", true, true, false, true, false),
						" @@turned to stone by the gaze of ",
						this.ParentObject.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), false, null, null, false, false, null, null, null) && gameObject2.IsValid())
					{
						flag = gameObject2.IsPlayer();
						cell.AddObject(StoneGaze.CreateStatueOf(gameObject2), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					list.RemoveAt(0);
				}
				if (cell.IsVisible())
				{
					scrapBuffer.RenderBase();
					if (i > 0)
					{
						scrapBuffer.WriteAt(Path[i - 1], "&b*", true);
					}
					if (i > 1)
					{
						scrapBuffer.WriteAt(Path[i - 2], "&K*", true);
					}
					scrapBuffer.WriteAt(cell, "&B*", true);
					scrapBuffer.Draw();
					Thread.Sleep(10);
				}
				i++;
			}
			if (flag)
			{
				The.Core.RenderDelay(3000, true, true, false);
			}
		}

		// Token: 0x0600C911 RID: 51473 RVA: 0x00494324 File Offset: 0x00492524
		public override string GetDescription()
		{
			return "You turn things to stone with your gaze.";
		}

		// Token: 0x0600C912 RID: 51474 RVA: 0x0049432B File Offset: 0x0049252B
		public override int GetRange(int Level)
		{
			return 7 + Level;
		}

		// Token: 0x0600C913 RID: 51475 RVA: 0x00494330 File Offset: 0x00492530
		public override int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x0600C914 RID: 51476 RVA: 0x00494334 File Offset: 0x00492534
		public override int GetDelay(int Level)
		{
			return 3;
		}

		// Token: 0x0600C915 RID: 51477 RVA: 0x00494338 File Offset: 0x00492538
		public override string GetLevelText(int Level)
		{
			return string.Concat(new string[]
			{
				"You can gaze {{rules|",
				this.GetRange(Level).ToString(),
				"}} squares after a ",
				Grammar.Cardinal(this.GetDelay(Level)),
				"-turn warmup and turn targets to stone.\nCooldown: {{rules|",
				this.GetCooldown(Level).ToString(),
				"}} rounds"
			});
		}

		// Token: 0x04004EAC RID: 20140
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
