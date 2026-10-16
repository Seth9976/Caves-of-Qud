using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E6 RID: 5094
	[Serializable]
	public class Disintegration : BaseMutation
	{
		// Token: 0x0600C4EE RID: 50414 RVA: 0x0047B05C File Offset: 0x0047925C
		public Disintegration()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C4EF RID: 50415 RVA: 0x0047B06F File Offset: 0x0047926F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C4F0 RID: 50416 RVA: 0x0047B098 File Offset: 0x00479298
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 3 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				Cell currentCell = E.Actor.CurrentCell;
				if (currentCell != null)
				{
					bool flag = false;
					bool flag2 = true;
					if (this.ParentObject.Brain != null)
					{
						foreach (GameObject gameObject in currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 4, "Combat", E.Actor, false, false, null))
						{
							if (gameObject != E.Actor)
							{
								if (E.Actor.Brain.GetFeeling(gameObject) >= 0)
								{
									flag2 = false;
									break;
								}
								flag = true;
							}
						}
					}
					if (flag && flag2)
					{
						E.Add(Disintegration.COMMAND_NAME, 1, null, false, false, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4F1 RID: 50417 RVA: 0x0047B188 File Offset: 0x00479388
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Disintegration.COMMAND_NAME)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell == null)
				{
					return false;
				}
				Disintegration.Disintegrate(currentCell, 3, base.Level, this.ParentObject, null, null, false, false);
				IComponent<GameObject>.XDidYToZ("the air", "vibrates", "destructively around", this.ParentObject, null, "!", this.ParentObject, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				base.UseEnergy(1000, "Mental Mutation Disintegration");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				this.ParentObject.ApplyEffect(new Exhausted(3), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4F2 RID: 50418 RVA: 0x0047B254 File Offset: 0x00479454
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4F3 RID: 50419 RVA: 0x0047B277 File Offset: 0x00479477
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C4F4 RID: 50420 RVA: 0x0047B27A File Offset: 0x0047947A
		public override string GetDescription()
		{
			return "You disintegrate nearby matter.";
		}

		// Token: 0x0600C4F5 RID: 50421 RVA: 0x0047B284 File Offset: 0x00479484
		public override string GetLevelText(int Level)
		{
			return "" + "Area: 7x7 around self\n" + "Damage to non-structural objects: {{rules|" + Disintegration.GetNonStructuralDamage(Level) + "}}\n" + "Damage to structural objects: {{rules|" + Disintegration.GetStructuralDamage(Level) + "}}\n" + "You are exhausted for 3 rounds after using this power.\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C4F6 RID: 50422 RVA: 0x0047B2F4 File Offset: 0x004794F4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "7x7 around self", false, 0);
			stats.Set("DamageNonStructural", Disintegration.GetNonStructuralDamage(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("DamageStructural", Disintegration.GetStructuralDamage(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C4F7 RID: 50423 RVA: 0x0047B378 File Offset: 0x00479578
		public static string GetNonStructuralDamage(int Level)
		{
			return Level.ToString() + "d10+" + (2 * Level).ToString();
		}

		// Token: 0x0600C4F8 RID: 50424 RVA: 0x0047B3A1 File Offset: 0x004795A1
		public static string GetStructuralDamage(int Level)
		{
			return Level.ToString() + "d100+20";
		}

		// Token: 0x0600C4F9 RID: 50425 RVA: 0x0047B3B4 File Offset: 0x004795B4
		public static void Disintegrate(Cell C, int Radius, int Level, GameObject immunity, GameObject owner = null, GameObject source = null, bool lowPrecision = false, bool indirect = false)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			if (owner == null)
			{
				owner = immunity;
			}
			int phase = Phase.getPhase(source ?? owner);
			List<Cell> list = new List<Cell>();
			C.GetAdjacentCells(Radius, list, false, true);
			bool flag = false;
			if (C.ParentZone != null && C.ParentZone.IsActive())
			{
				for (int i = 0; i < Radius; i++)
				{
					foreach (Cell cell in list)
					{
						if (cell.ParentZone == C.ParentZone && cell.IsVisible())
						{
							flag = true;
							if (Radius < 3 || (cell.PathDistanceTo(C) <= i - Stat.Random(0, 1) && cell.PathDistanceTo(C) > i - Stat.Random(2, 3)))
							{
								scrapBuffer.Goto(cell.X, cell.Y);
								scrapBuffer.Write("&" + Phase.getRandomDisintegrationColor(phase).ToString() + ((char)Stat.Random(191, 198)).ToString(), true, false, false, null, -1);
							}
						}
					}
					if (flag)
					{
						textConsole.DrawBuffer(scrapBuffer, null, false);
						Thread.Sleep(75);
					}
				}
			}
			if (list.Count > 0 && owner != null && owner.Physics != null)
			{
				owner.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_disintegration_disintegrate", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
			}
			string nonStructuralDamage = Disintegration.GetNonStructuralDamage(Level);
			string structuralDamage = Disintegration.GetStructuralDamage(Level);
			bool flag2 = lowPrecision;
			if (!flag2 && owner != null)
			{
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasObject(new Predicate<GameObject>(owner.IsRegardedWithHostilityBy)))
						{
							flag2 = true;
							break;
						}
					}
				}
			}
			foreach (Cell cell2 in list)
			{
				foreach (GameObject gameObject in cell2.GetObjectsInCell())
				{
					if (gameObject != immunity && gameObject.PhaseMatches(phase) && gameObject.GetMatterPhase() <= 3 && gameObject.GetIntProperty("Electromagnetic", 0) <= 0)
					{
						string text = (gameObject.HasPart<Inorganic>() ? structuralDamage : nonStructuralDamage);
						GameObject gameObject2 = gameObject;
						int num = text.RollCached();
						string text2 = "from %t disintegration!";
						string text3 = "Disintegration";
						string text4 = null;
						string text5 = null;
						bool flag3 = flag2 && owner != null && !gameObject.IsHostileTowards(owner);
						gameObject2.TakeDamage(num, text2, text3, text4, text5, owner, null, source, null, null, flag3, false, indirect, false, false, false, false, false, false, 5, null);
					}
				}
			}
		}

		// Token: 0x0600C4FA RID: 50426 RVA: 0x0047B6D4 File Offset: 0x004798D4
		public int GetCooldown(int Level)
		{
			return 75;
		}

		// Token: 0x0600C4FB RID: 50427 RVA: 0x0047B6D8 File Offset: 0x004798D8
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C4FC RID: 50428 RVA: 0x0047B6E4 File Offset: 0x004798E4
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Disintegration", Disintegration.COMMAND_NAME, "Mental Mutations", null, "ê", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4FD RID: 50429 RVA: 0x0047B72D File Offset: 0x0047992D
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DEA RID: 19946
		public static readonly string COMMAND_NAME = "CommandDisintegration";
	}
}
