using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E4 RID: 5092
	[Serializable]
	public class Confusion : BaseMutation
	{
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600C4C6 RID: 50374 RVA: 0x0047A02F File Offset: 0x0047822F
		public static int CurrentConfusionLevel
		{
			get
			{
				if (The.Player == null)
				{
					return 0;
				}
				return The.Player.GetConfusion();
			}
		}

		// Token: 0x0600C4C7 RID: 50375 RVA: 0x0047A044 File Offset: 0x00478244
		public Confusion()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C4C8 RID: 50376 RVA: 0x0047A05E File Offset: 0x0047825E
		public override string GetDescription()
		{
			return "You confuse nearby enemies.";
		}

		// Token: 0x0600C4C9 RID: 50377 RVA: 0x0047A065 File Offset: 0x00478265
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C4CA RID: 50378 RVA: 0x0047A084 File Offset: 0x00478284
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.Range && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.DistanceTo(E.Target) < Confusion.GetConeLength(base.Level) && E.Target.Brain != null && !E.Target.HasEffect<Confused>() && E.Target.FireEvent("CanApplyConfusion") && CanApplyEffectEvent.Check(E.Target, "Confusion", 0) && !E.Target.IsInStasis())
			{
				E.Add("CommandConfuse", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4CB RID: 50379 RVA: 0x0047A143 File Offset: 0x00478343
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4CC RID: 50380 RVA: 0x0047A166 File Offset: 0x00478366
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandConfuse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C4CD RID: 50381 RVA: 0x0047A17C File Offset: 0x0047837C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandConfuse")
			{
				List<Cell> list = base.PickCone(Confusion.GetConeLength(base.Level), Confusion.GetConeAngle(base.Level), AllowVis.Any, null, "Confusion");
				if (list == null)
				{
					return false;
				}
				base.UseEnergy(1000, "Mental Mutation Confusion");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Confusion.GetCooldown(base.Level), null, null, false);
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_confusion", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				this.Confuse(list);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C4CE RID: 50382 RVA: 0x0047A22F File Offset: 0x0047842F
		public static int GetCooldown(int Level)
		{
			return 75;
		}

		// Token: 0x0600C4CF RID: 50383 RVA: 0x0047A233 File Offset: 0x00478433
		public static int GetConeAngle(int Level)
		{
			return 29 + Level;
		}

		// Token: 0x0600C4D0 RID: 50384 RVA: 0x0047A239 File Offset: 0x00478439
		public static int GetConeLength(int Level)
		{
			return 4 + Level / 3;
		}

		// Token: 0x0600C4D1 RID: 50385 RVA: 0x0047A240 File Offset: 0x00478440
		public static int GetConeAnimationDelay(int Level)
		{
			return Math.Max(10, 26 - Level);
		}

		// Token: 0x0600C4D2 RID: 50386 RVA: 0x0047A24D File Offset: 0x0047844D
		public static int GetLowDuration(int Level)
		{
			return (int)((double)(Level / 2 + 10) * 0.8);
		}

		// Token: 0x0600C4D3 RID: 50387 RVA: 0x0047A261 File Offset: 0x00478461
		public static int GetHighDuration(int Level)
		{
			return (int)((double)(Level / 2 + 10) * 1.2);
		}

		// Token: 0x0600C4D4 RID: 50388 RVA: 0x0047A278 File Offset: 0x00478478
		public static string GetDuration(int Level)
		{
			return Confusion.GetLowDuration(Level).ToString() + "-" + Confusion.GetHighDuration(Level).ToString();
		}

		// Token: 0x0600C4D5 RID: 50389 RVA: 0x0047A2AB File Offset: 0x004784AB
		public static int GetMentalPenalty(int Level)
		{
			return (int)Math.Min(10.0, Math.Floor((double)(Level - 1) / 2.0 + 3.0));
		}

		// Token: 0x0600C4D6 RID: 50390 RVA: 0x0047A2D9 File Offset: 0x004784D9
		public static int GetConfusionLevel(int Level)
		{
			return (int)Math.Min(10.0, Math.Floor((double)(Level - 1) / 2.0 + 3.0));
		}

		// Token: 0x0600C4D7 RID: 50391 RVA: 0x0047A308 File Offset: 0x00478508
		public override string GetLevelText(int Level)
		{
			return "Affected creatures act semi-randomly and receive a {{rules|-" + Confusion.GetMentalPenalty(Level).ToString() + "}} penalty to their mental abilities.\n" + "Cone angle: {{rules|" + Confusion.GetConeAngle(Level).ToString() + "}} degrees\n" + "Cone length: {{rules|" + Confusion.GetConeLength(Level).ToString() + "}}\n" + "Duration: {{rules|" + Confusion.GetDuration(Level) + "}} rounds\n" + "Cooldown: " + Confusion.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C4D8 RID: 50392 RVA: 0x0047A3A0 File Offset: 0x004785A0
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Penalty", Confusion.GetMentalPenalty(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("ConeAngle", Confusion.GetConeAngle(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("ConeLength", Confusion.GetConeLength(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("Duration", Confusion.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Confusion.GetCooldown(Level), 0);
		}

		// Token: 0x0600C4D9 RID: 50393 RVA: 0x0047A45C File Offset: 0x0047865C
		public static bool Confuse(MentalAttackEvent E, bool Attack, int Level, int Penalty, bool Silent = false)
		{
			GameObject defender = E.Defender;
			GameObject attacker = E.Attacker;
			if (Attack && !defender.IsHostileTowards(attacker))
			{
				defender.AddOpinion<OpinionAttack>(attacker, null, 1f);
			}
			if (E.Penetrations > 0)
			{
				int magnitude = E.Magnitude;
				if (magnitude > 0 && defender.FireEvent("CanApplyConfusion") && CanApplyEffectEvent.Check(defender, "Confusion", magnitude) && (!Attack || defender.FireEvent(Event.New("ApplyAttackConfusion", "Duration", magnitude))) && defender.FireEvent(Event.New("ApplyConfusion", "Duration", magnitude)) && defender.ApplyEffect(new Confused(magnitude, Level, Penalty, "Confusion"), null))
				{
					for (int i = 0; i < E.Penetrations; i++)
					{
						defender.ParticleText("&W?", false);
					}
					return true;
				}
				if (!Silent && attacker != null && attacker.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{R|Your attack does not affect " + defender.the + defender.ShortDisplayName + ".}}", null, true);
				}
			}
			else if (!Silent)
			{
				IComponent<GameObject>.XDidY(defender, "resist", "being confused", null, null, null, defender, null, false, false, null, null, false, false, false, false, false, null);
			}
			return false;
		}

		// Token: 0x0600C4DA RID: 50394 RVA: 0x0047A584 File Offset: 0x00478784
		public void Confuse(List<Cell> Cells)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			string duration = Confusion.GetDuration(base.Level);
			int coneAnimationDelay = Confusion.GetConeAnimationDelay(base.Level);
			foreach (Cell cell in Cells)
			{
				foreach (GameObject gameObject in cell.GetObjectsInCell())
				{
					if (gameObject != this.ParentObject && gameObject.Brain != null)
					{
						int level = Confusion.GetConfusionLevel(base.Level);
						int penalty = Confusion.GetMentalPenalty(base.Level);
						base.PerformMentalAttack((MentalAttackEvent E) => Confusion.Confuse(E, true, level, penalty, false), this.ParentObject, gameObject, null, "Confuse", "1d8", 8388609, duration.RollCached(), int.MinValue, Math.Max(this.ParentObject.StatMod("Ego", 0), base.Level), 0);
					}
				}
				if (cell.IsVisible())
				{
					scrapBuffer.Goto(cell.X, cell.Y);
					scrapBuffer.Write("&B?", true, false, false, null, -1);
					textConsole.DrawBuffer(scrapBuffer, null, false);
					Thread.Sleep(coneAnimationDelay);
				}
			}
		}

		// Token: 0x0600C4DB RID: 50395 RVA: 0x0047A734 File Offset: 0x00478934
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C4DC RID: 50396 RVA: 0x0047A740 File Offset: 0x00478940
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Confusion", "CommandConfuse", "Mental Mutations", null, "?", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4DD RID: 50397 RVA: 0x0047A789 File Offset: 0x00478989
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DE0 RID: 19936
		public int Range = 8;
	}
}
