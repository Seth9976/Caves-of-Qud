using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001454 RID: 5204
	[Serializable]
	public class SlimeGlands : BaseMutation
	{
		// Token: 0x0600CB8F RID: 52111 RVA: 0x004A0674 File Offset: 0x0049E874
		public SlimeGlands()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600CB90 RID: 52112 RVA: 0x004A0687 File Offset: 0x0049E887
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CB91 RID: 52113 RVA: 0x004A068C File Offset: 0x0049E88C
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", SlimeGlands.RANGE, false, 0);
			stats.Set("Area", "3x3", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600CB92 RID: 52114 RVA: 0x004A06D8 File Offset: 0x0049E8D8
		public int GetCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CB93 RID: 52115 RVA: 0x004A06DC File Offset: 0x0049E8DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600CB94 RID: 52116 RVA: 0x004A06FC File Offset: 0x0049E8FC
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= SlimeGlands.RANGE && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !E.Actor.IsFrozen() && !E.Actor.OnWorldMap() && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add(SlimeGlands.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB95 RID: 52117 RVA: 0x004A077C File Offset: 0x0049E97C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == SlimeGlands.COMMAND_NAME)
			{
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return false;
				}
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				List<Cell> list = base.PickBurst(SlimeGlands.RADIUS, SlimeGlands.RANGE, false, AllowVis.OnlyVisible, "Spit Slime");
				if (list == null || list.Count == 0)
				{
					return false;
				}
				if (this.ParentObject.DistanceTo(list[0]) > SlimeGlands.RANGE)
				{
					return this.ParentObject.Fail("That is out of range! (" + SlimeGlands.RANGE.Things("square", null) + ")");
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_spitSlime_spit", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				SlimeGlands.SlimeAnimation("&g", this.ParentObject.CurrentCell, list[0]);
				int num = 0;
				foreach (Cell cell in list)
				{
					if (num == 0 || 80.in100())
					{
						cell.AddObject("SlimePuddle", null, null, null, null);
					}
					num++;
				}
				base.UseEnergy(1000, "Physical Mutation SlimeGlands");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				base.DidX("spit", "a pool of slime", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB96 RID: 52118 RVA: 0x004A093C File Offset: 0x0049EB3C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CB97 RID: 52119 RVA: 0x004A0940 File Offset: 0x0049EB40
		public override string GetDescription()
		{
			return "" + "You produce a viscous slime that you can spit at things.\n\n" + "Covers an area with slime\n" + "Range: 8\n" + "Area: 3x3\n" + "Cooldown: 40 rounds\n" + "You can walk over slime without slipping.";
		}

		// Token: 0x0600CB98 RID: 52120 RVA: 0x004A098E File Offset: 0x0049EB8E
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600CB99 RID: 52121 RVA: 0x004A0998 File Offset: 0x0049EB98
		public static void SlimeAnimation(string Color, Cell StartCell, Cell EndCell)
		{
			if (StartCell == null)
			{
				return;
			}
			if (EndCell == null)
			{
				return;
			}
			if (StartCell.ParentZone == null)
			{
				return;
			}
			List<Point> list = Zone.Line(StartCell.X, StartCell.Y, EndCell.X, EndCell.Y, false);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
			TextConsole textConsole = Popup._TextConsole;
			CleanQueue<Point> cleanQueue = new CleanQueue<Point>();
			Point point = null;
			foreach (Point point2 in list)
			{
				point = point2;
				bool flag = false;
				cleanQueue.Enqueue(point2);
				while (cleanQueue.Count > 3)
				{
					cleanQueue.Dequeue();
				}
				int num = 0;
				for (int i = 0; i < cleanQueue.Items.Count; i++)
				{
					Point point3 = cleanQueue.Items[i];
					if (StartCell.ParentZone.GetCell(point3.X, point3.Y).IsVisible())
					{
						if (!flag)
						{
							flag = true;
							XRLCore.Core.RenderBaseToBuffer(scrapBuffer);
						}
						scrapBuffer.Goto(point3.X, point3.Y);
						if (cleanQueue.Count == 1)
						{
							scrapBuffer.Write(Color + "\a", true, false, false, null, -1);
						}
						else if (cleanQueue.Count == 2)
						{
							if (num == 0)
							{
								scrapBuffer.Write(Color + "ú", true, false, false, null, -1);
							}
							if (num == 1)
							{
								scrapBuffer.Write(Color + "\a", true, false, false, null, -1);
							}
						}
						else
						{
							if (num == 0)
							{
								scrapBuffer.Write(Color + "ù", true, false, false, null, -1);
							}
							if (num == 1)
							{
								scrapBuffer.Write(Color + "ú", true, false, false, null, -1);
							}
							if (num == 2)
							{
								scrapBuffer.Write(Color + "\a", true, false, false, null, -1);
							}
						}
					}
					num++;
				}
				if (flag)
				{
					textConsole.DrawBuffer(scrapBuffer, null, false);
					Thread.Sleep(30);
				}
			}
			if (point != null)
			{
				for (int j = 0; j < 5; j++)
				{
					float num2 = (float)Stat.Random(0, 359) / 58f;
					float num3 = (float)Math.Sin((double)num2) / 2f;
					float num4 = (float)Math.Cos((double)num2) / 2f;
					XRLCore.ParticleManager.Add(Color + ".", (float)point.X, (float)point.Y, num3, num4, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x0600CB9A RID: 52122 RVA: 0x004A0C30 File Offset: 0x0049EE30
		public override bool Mutate(GameObject GO, int Level)
		{
			GO.Slimewalking = true;
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spit Slime", SlimeGlands.COMMAND_NAME, "Physical Mutations", null, "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB9B RID: 52123 RVA: 0x004A0C80 File Offset: 0x0049EE80
		public override bool Unmutate(GameObject GO)
		{
			GO.Slimewalking = false;
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004F22 RID: 20258
		public static readonly string COMMAND_NAME = "CommandSpitSlime";

		// Token: 0x04004F23 RID: 20259
		public static readonly int RADIUS = 1;

		// Token: 0x04004F24 RID: 20260
		public static readonly int RANGE = 8;
	}
}
