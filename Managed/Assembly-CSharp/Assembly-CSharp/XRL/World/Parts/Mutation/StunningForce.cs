using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013FB RID: 5115
	[Serializable]
	public class StunningForce : BaseMutation
	{
		// Token: 0x0600C648 RID: 50760 RVA: 0x00483055 File Offset: 0x00481255
		public StunningForce()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C649 RID: 50761 RVA: 0x00483068 File Offset: 0x00481268
		public static void CollectProxyStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "7x7", false, 0);
			stats.Set("Range", StunningForce.RANGE, false, 0);
			stats.Set("DamageIncrement", StunningForce.GetDamageIncrement(Level), false, 0);
		}

		// Token: 0x0600C64A RID: 50762 RVA: 0x004830A4 File Offset: 0x004812A4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "7x7", false, 0);
			stats.Set("Range", StunningForce.RANGE, false, 0);
			stats.Set("DamageIncrement", StunningForce.GetDamageIncrement(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C64B RID: 50763 RVA: 0x00483115 File Offset: 0x00481315
		public int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x0600C64C RID: 50764 RVA: 0x00483119 File Offset: 0x00481319
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C64D RID: 50765 RVA: 0x00483138 File Offset: 0x00481338
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= StunningForce.RANGE && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.HasLOSTo(E.Target, false, false, false, true, null))
			{
				E.Add("CommandStunningForce", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C64E RID: 50766 RVA: 0x00483191 File Offset: 0x00481391
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C64F RID: 50767 RVA: 0x004831B9 File Offset: 0x004813B9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C650 RID: 50768 RVA: 0x004831BC File Offset: 0x004813BC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandStunningForce");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C651 RID: 50769 RVA: 0x004831D4 File Offset: 0x004813D4
		public static string GetDamageIncrement(int Level)
		{
			if (Level < 3)
			{
				return "1d3";
			}
			return "1d3+" + ((Level - 1) / 2).ToString();
		}

		// Token: 0x0600C652 RID: 50770 RVA: 0x00483202 File Offset: 0x00481402
		public override string GetDescription()
		{
			return "You invoke a concussive force in a nearby area, throwing enemies back and stunning them.";
		}

		// Token: 0x0600C653 RID: 50771 RVA: 0x0048320C File Offset: 0x0048140C
		public static void ShockwaveAnimation(Cell StartCell, Cell EndCell)
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
			foreach (Point point in list)
			{
				bool flag = false;
				cleanQueue.Enqueue(point);
				while (cleanQueue.Count > 3)
				{
					cleanQueue.Dequeue();
				}
				int num = 0;
				for (int i = 0; i < cleanQueue.Items.Count; i++)
				{
					Point point2 = cleanQueue.Items[i];
					if (StartCell.ParentZone.GetCell(point2.X, point2.Y).IsVisible())
					{
						if (!flag)
						{
							flag = true;
							XRLCore.Core.RenderBaseToBuffer(scrapBuffer);
						}
						int num2 = point2.X + Stat.Random(-1, 1);
						int num3 = point2.Y + Stat.Random(-1, 1);
						scrapBuffer.Constrain(ref num2, ref num3);
						scrapBuffer[point2.X, point2.Y].Copy(scrapBuffer[num2, num3]);
						if (num == 0)
						{
							scrapBuffer[point2.X, point2.Y].Char = '\u00af';
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
		}

		// Token: 0x0600C654 RID: 50772 RVA: 0x004833B8 File Offset: 0x004815B8
		public override string GetLevelText(int Level)
		{
			string text = "";
			text = text + "Range: " + StunningForce.RANGE.ToString() + "\n";
			text += "Area: 7x7\n";
			if (Level == base.Level)
			{
				text += "Creatures are pushed away from center of blast, stunned, and dealt crushing damage in up to 3 increments.\n";
			}
			else
			{
				text += "{{rules|Increased stun save difficulty}}\n";
			}
			text = text + "Damage increment: {{rules|" + StunningForce.GetDamageIncrement(Level) + "}}\n";
			return text + "Cooldown: 50 rounds";
		}

		// Token: 0x0600C655 RID: 50773 RVA: 0x0048343C File Offset: 0x0048163C
		public static void Concussion(Cell StartCell, GameObject ParentObject, int Level, int Distance, int Phase = 1, GameObject Target = null, bool Stun = true, bool Damage = true)
		{
			StartCell.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_stunning_force", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			StunningForce.VisitedCells.Clear();
			StunningForce.CellQueue.Clear();
			StunningForce.Pushed.Clear();
			StunningForce.Stunned.Clear();
			StunningForce.CellQueue.Enqueue(new ConcussionEntry(StartCell, Distance, "-"));
			while (StunningForce.CellQueue.Count > 0)
			{
				ConcussionEntry concussionEntry = StunningForce.CellQueue.Dequeue();
				if (concussionEntry.Distance > 0)
				{
					Algorithms.RandomShuffleInPlace<string>(StunningForce.Dirs);
					foreach (string text in StunningForce.Dirs)
					{
						Cell localCellFromDirection = concussionEntry.C.GetLocalCellFromDirection(text, true);
						if (localCellFromDirection != null && !StunningForce.VisitedCells.ContainsKey(localCellFromDirection))
						{
							StunningForce.CellQueue.Enqueue(new ConcussionEntry(localCellFromDirection, concussionEntry.Distance - 1, text));
							StunningForce.VisitedCells.Add(localCellFromDirection, true);
						}
					}
				}
				foreach (GameObject gameObject in concussionEntry.C.GetObjectsWithPart("Physics"))
				{
					if (gameObject != ParentObject && !StunningForce.Pushed.Contains(gameObject) && gameObject.PhaseMatches(Phase) && !gameObject.IsOpenLiquidVolume() && gameObject.Render != null && gameObject.Render.RenderLayer >= 1 && gameObject.IsReal)
					{
						string text2 = concussionEntry.Direction;
						if (text2 == "-")
						{
							text2 = Directions.GetRandomDirection();
						}
						StunningForce.Pushed.Add(gameObject);
						int num = 3;
						int num2 = 4;
						Cell currentCell = gameObject.CurrentCell;
						int num3 = Math.Min(num, num2 - ((currentCell != null) ? currentCell.ManhattanDistanceTo(StartCell) : 4));
						gameObject.Push(text2, Level * 1000, 4, false, true);
						if (Damage && num3 > 0)
						{
							int num4 = 0;
							for (int j = 0; j < num3; j++)
							{
								num4 += StunningForce.GetDamageIncrement(Level).RollCached();
							}
							GameObject gameObject2 = gameObject;
							int num5 = num4;
							string text3 = "from %t stunning force!";
							string text4 = "Concussion";
							string text5 = null;
							string text6 = null;
							GameObject gameObject3 = null;
							bool flag = gameObject != Target;
							gameObject2.TakeDamage(num5, text3, text4, text5, text6, gameObject3, ParentObject, null, null, null, flag, false, false, false, false, false, false, false, false, 5, null);
						}
						if (Stun && gameObject.IsValid() && !StunningForce.Stunned.Contains(gameObject) && gameObject.IsCombat)
						{
							StunningForce.Stunned.Add(gameObject);
						}
					}
				}
			}
			if (StartCell != null && StartCell.IsVisible())
			{
				float num6 = (float)(Distance * 2 + 1) / 7f;
				int num7 = Mathf.RoundToInt(4f / num6);
				for (int k = 0; k < 360; k += num7)
				{
					for (int l = 5; l < 30; l += Stat.RandomCosmetic(2, 6))
					{
						if (Stat.RandomCosmetic(1, 120 - Distance * 10) == 1)
						{
							XRLCore.ParticleManager.Add("&" + Phase.getRandomStunningForceColor(Phase).ToString() + "±", (float)StartCell.X, (float)StartCell.Y, (float)Math.Sin((double)((float)k) * 0.04 * (double)num6) / (float)l, (float)Math.Cos((double)((float)k) * 0.04 * (double)num6) / (float)l, Mathf.RoundToInt((float)(40 + Stat.RandomCosmetic(-10, 10)) * num6));
						}
					}
					XRLCore.ParticleManager.Add("@", (float)StartCell.X, (float)StartCell.Y, (float)Math.Sin((double)((float)k) * 0.06) / 4f, (float)Math.Cos((double)((float)k) * 0.06) / 4f, Mathf.RoundToInt((float)(40 + Stat.RandomCosmetic(-30, 30)) * num6));
				}
				if (ParentObject.IsPlayer())
				{
					if (ParentObject.CurrentCell == StartCell)
					{
						IComponent<GameObject>.XDidY(ParentObject, "invoke", "a concussive blast around you", "!", null, null, ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
					else
					{
						IComponent<GameObject>.XDidY(ParentObject, "invoke", "a concussive blast to the " + Directions.GetDirectionDescription(ParentObject.CurrentCell.GetDirectionFromCell(StartCell, false)), "!", null, null, ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
				}
				else if (ParentObject.CurrentCell == StartCell)
				{
					IComponent<GameObject>.XDidY(ParentObject, "feel", "a concussive blast around " + ParentObject.them, "!", null, null, ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
				else
				{
					IComponent<GameObject>.XDidY(ParentObject, "feel", "a concussive blast from the " + Directions.GetDirectionDescription(ParentObject.CurrentCell.GetDirectionFromCell(StartCell, false)), "!", null, null, ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			foreach (GameObject gameObject4 in StunningForce.Stunned)
			{
				if (gameObject4.IsValid() && !gameObject4.MakeSave("Toughness", 15 + Level, null, null, "StunningForce Stun", false, false, false, false, ParentObject))
				{
					gameObject4.ApplyEffect(new Stun(3, 15 + Level, false), null);
				}
			}
			StunningForce.VisitedCells.Clear();
			StunningForce.CellQueue.Clear();
			StunningForce.Pushed.Clear();
			StunningForce.Stunned.Clear();
		}

		// Token: 0x0600C656 RID: 50774 RVA: 0x004839E0 File Offset: 0x00481BE0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandStunningForce")
			{
				if (this.ParentObject.OnWorldMap())
				{
					this.ParentObject.Fail("You cannot use Stunning Force on the world map.");
					return false;
				}
				List<Cell> list = base.PickBurst(3, StunningForce.RANGE, false, AllowVis.OnlyVisible, "Stunning Force");
				if (list == null || list.Count <= 0)
				{
					return false;
				}
				if (50.in100())
				{
					Cell randomLocalAdjacentCell = list[0].GetRandomLocalAdjacentCell();
					if (randomLocalAdjacentCell != null)
					{
						list[0] = randomLocalAdjacentCell;
					}
				}
				if (this.ParentObject.CurrentCell != null && list[0] != null)
				{
					StunningForce.ShockwaveAnimation(this.ParentObject.CurrentCell, list[0]);
				}
				StunningForce.Concussion(list[0], this.ParentObject, base.Level, 3, this.ParentObject.GetPhase(), list[0].GetCombatTarget(this.ParentObject, true, false, false, 0, null, null, null, null, null, true, false, null), true, true);
				base.UseEnergy(1000, "Mental Mutation StunningForce");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 50, null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C657 RID: 50775 RVA: 0x00483AFF File Offset: 0x00481CFF
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C658 RID: 50776 RVA: 0x00483B08 File Offset: 0x00481D08
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Stunning Force", "CommandStunningForce", "Mental Mutations", null, "#", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C659 RID: 50777 RVA: 0x00483B51 File Offset: 0x00481D51
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E33 RID: 20019
		public static readonly int RANGE = 8;

		// Token: 0x04004E34 RID: 20020
		[NonSerialized]
		private static Dictionary<Cell, bool> VisitedCells = new Dictionary<Cell, bool>();

		// Token: 0x04004E35 RID: 20021
		[NonSerialized]
		private static CleanQueue<ConcussionEntry> CellQueue = new CleanQueue<ConcussionEntry>();

		// Token: 0x04004E36 RID: 20022
		[NonSerialized]
		private static List<GameObject> Pushed = new List<GameObject>();

		// Token: 0x04004E37 RID: 20023
		[NonSerialized]
		private static List<GameObject> Stunned = new List<GameObject>();

		// Token: 0x04004E38 RID: 20024
		[NonSerialized]
		private static string[] Dirs = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };
	}
}
