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
	// Token: 0x02001433 RID: 5171
	[Serializable]
	public class ElectromagneticPulse : BaseMutation
	{
		// Token: 0x0600C9D2 RID: 51666 RVA: 0x00497567 File Offset: 0x00495767
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C9D3 RID: 51667 RVA: 0x0049758D File Offset: 0x0049578D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("circuitry", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9D4 RID: 51668 RVA: 0x004975B5 File Offset: 0x004957B5
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.DischargeActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9D5 RID: 51669 RVA: 0x004975D9 File Offset: 0x004957D9
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= base.Level && base.IsMyActivatedAbilityAIUsable(this.DischargeActivatedAbilityID, null))
			{
				E.Add("CommandElectromagneticPulse", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9D6 RID: 51670 RVA: 0x00497610 File Offset: 0x00495810
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandElectromagneticPulse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C9D7 RID: 51671 RVA: 0x00497625 File Offset: 0x00495825
		public override string GetDescription()
		{
			return "You generate an electromagnetic pulse that disables nearby artifacts and machines.";
		}

		// Token: 0x0600C9D8 RID: 51672 RVA: 0x0049762C File Offset: 0x0049582C
		public static int GetRadius(int Level)
		{
			if (Level < 5)
			{
				return 2;
			}
			if (Level < 9)
			{
				return 5;
			}
			return 9;
		}

		// Token: 0x0600C9D9 RID: 51673 RVA: 0x0049763D File Offset: 0x0049583D
		public static int GetCooldown(int Level)
		{
			return 200;
		}

		// Token: 0x0600C9DA RID: 51674 RVA: 0x00497644 File Offset: 0x00495844
		public static string GetDuration(int Level)
		{
			return (4 + Level * 2).ToString() + "-" + (13 + Level * 2).ToString();
		}

		// Token: 0x0600C9DB RID: 51675 RVA: 0x00497678 File Offset: 0x00495878
		public override string GetLevelText(int Level)
		{
			int num = ElectromagneticPulse.GetRadius(Level) * 2 + 1;
			string text = string.Concat(new string[]
			{
				"Area: {{rules|",
				num.ToString(),
				"x",
				num.ToString(),
				"}} centered around yourself\n"
			});
			text = string.Concat(new string[]
			{
				text,
				"Duration: {{rules|",
				(4 + Level * 2).ToString(),
				"-",
				(13 + Level * 2).ToString(),
				"}} rounds\n"
			});
			return text + "Cooldown: " + ElectromagneticPulse.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C9DC RID: 51676 RVA: 0x00497734 File Offset: 0x00495934
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", ElectromagneticPulse.GetRadius(Level).ToString() + "x" + ElectromagneticPulse.GetRadius(Level).ToString(), !stats.mode.Contains("ability"), 0);
			stats.Set("Duration", ElectromagneticPulse.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), ElectromagneticPulse.GetCooldown(Level), 0);
		}

		// Token: 0x0600C9DD RID: 51677 RVA: 0x004977C8 File Offset: 0x004959C8
		public static void EMP(Cell C, int Radius, int Duration, bool IncludeBaseCell = true, int Phase = 1)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			List<Cell> list = new List<Cell>();
			C.GetAdjacentCells(Radius, list, false, true);
			foreach (Cell cell in list)
			{
				if (cell != C || IncludeBaseCell)
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPart("Metal"))
					{
						if (gameObject.PhaseMatches(Phase))
						{
							gameObject.ForceApplyEffect(new ElectromagneticPulsed(Duration), null);
						}
					}
					foreach (GameObject gameObject2 in cell.GetObjectsWithPart("Combat"))
					{
						if (gameObject2.PhaseMatches(Phase))
						{
							gameObject2.ApplyEffect(new ElectromagneticPulsed(Duration), null);
						}
					}
				}
			}
			if (C != null)
			{
				C.PlayWorldSound("Sounds/Abilities/sfx_ability_electromagnetic_pulse", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			bool flag = false;
			for (int i = 0; i < Radius; i++)
			{
				XRLCore.Core.RenderMapToBuffer(scrapBuffer);
				foreach (Cell cell2 in list)
				{
					if (cell2.ParentZone == C.ParentZone && (Radius < 3 || (cell2.PathDistanceTo(C) <= i - Stat.Random(0, 1) && cell2.PathDistanceTo(C) > i - Stat.Random(2, 3))) && cell2.IsVisible())
					{
						flag = true;
						scrapBuffer.Goto(cell2.X, cell2.Y);
						scrapBuffer.Write("&" + Phase.getRandomElectromagneticPulseColor(Phase).ToString() + ((char)Stat.Random(191, 198)).ToString(), true, false, false, null, -1);
					}
				}
				if (flag)
				{
					textConsole.DrawBuffer(scrapBuffer, null, false);
					Thread.Sleep(25);
				}
			}
		}

		// Token: 0x0600C9DE RID: 51678 RVA: 0x00497A48 File Offset: 0x00495C48
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandElectromagneticPulse")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell == null)
				{
					return false;
				}
				base.UseEnergy(1000, "Physical Mutation Electromagnetic Pulse");
				ElectromagneticPulse.EMP(currentCell, ElectromagneticPulse.GetRadius(base.Level), Stat.Random(1, 10) + 5 + base.Level, false, this.ParentObject.GetPhase());
				base.CooldownMyActivatedAbility(this.DischargeActivatedAbilityID, ElectromagneticPulse.GetCooldown(base.Level), null, null, false);
				base.DidX("emit", "an electromagnetic pulse", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C9DF RID: 51679 RVA: 0x00497AFF File Offset: 0x00495CFF
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C9E0 RID: 51680 RVA: 0x00497B08 File Offset: 0x00495D08
		public override bool Mutate(GameObject GO, int Level)
		{
			this.DischargeActivatedAbilityID = base.AddMyActivatedAbility("Emit Pulse", "CommandElectromagneticPulse", "Physical Mutations", null, "î", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C9E1 RID: 51681 RVA: 0x00497B51 File Offset: 0x00495D51
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.DischargeActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004ED2 RID: 20178
		public Guid DischargeActivatedAbilityID = Guid.Empty;
	}
}
