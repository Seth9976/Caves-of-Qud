using System;
using ConsoleLib.Console;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140C RID: 5132
	[Serializable]
	public class IceBreather : BreatherBase
	{
		// Token: 0x0600C75F RID: 51039 RVA: 0x0048A634 File Offset: 0x00488834
		public override string GetFaceObject()
		{
			return "Icy Vapor";
		}

		// Token: 0x0600C760 RID: 51040 RVA: 0x0048A63B File Offset: 0x0048883B
		public override string GetCommandDisplayName()
		{
			return "Breathe Ice";
		}

		// Token: 0x0600C761 RID: 51041 RVA: 0x0048A642 File Offset: 0x00488842
		public override string GetDescription()
		{
			return "You breathe ice.";
		}

		// Token: 0x0600C762 RID: 51042 RVA: 0x0048A649 File Offset: 0x00488849
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			base.CollectStats(stats, Level);
			stats.Set("Damage", this.ComputeDamage(Level), false, 0);
		}

		// Token: 0x0600C763 RID: 51043 RVA: 0x0048A667 File Offset: 0x00488867
		public override string GetBreathName()
		{
			return "ice";
		}

		// Token: 0x0600C764 RID: 51044 RVA: 0x0048A670 File Offset: 0x00488870
		public override string GetLevelText(int Level)
		{
			string text = "Breathes ice in a cone.\n";
			text = text + "Damage: " + this.ComputeDamage(Level) + "\n";
			text = text + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n";
			text = text + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n";
			text += "Cooldown: 15 rounds\n";
			if (Level != base.Level)
			{
				text += "Decreased temperature.";
			}
			return text;
		}

		// Token: 0x0600C765 RID: 51045 RVA: 0x0048A6FD File Offset: 0x004888FD
		public override bool ChangeLevel(int NewLevel)
		{
			base.StatShifter.SetStatShift("ColdResistance", base.Level * 2, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C766 RID: 51046 RVA: 0x0048A720 File Offset: 0x00488920
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.Unmutate(GO);
		}

		// Token: 0x0600C767 RID: 51047 RVA: 0x0048A734 File Offset: 0x00488934
		public string ComputeDamage(int UseLevel)
		{
			string text = UseLevel.ToString() + "d4";
			if (this.ParentObject != null)
			{
				int partCount = this.ParentObject.Body.GetPartCount(this.BodyPartType);
				if (partCount > 0)
				{
					text += partCount.Signed(false);
				}
			}
			else
			{
				text += "+1";
			}
			return text;
		}

		// Token: 0x0600C768 RID: 51048 RVA: 0x0048A793 File Offset: 0x00488993
		public string ComputeDamage()
		{
			return this.ComputeDamage(base.Level);
		}

		// Token: 0x0600C769 RID: 51049 RVA: 0x0048A7A4 File Offset: 0x004889A4
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			string text = this.ComputeDamage();
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjectsInCell())
				{
					if (gameObject.PhaseMatches(this.ParentObject))
					{
						gameObject.TemperatureChange(-120 - 7 * base.Level, this.ParentObject, false, false, false, false, 0, null, null);
						for (int i = 0; i < 5; i++)
						{
							gameObject.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int j = 0; j < 5; j++)
						{
							gameObject.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int k = 0; k < 5; k++)
						{
							gameObject.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
					}
				}
				foreach (GameObject gameObject2 in C.GetObjectsWithPart("Combat"))
				{
					if (gameObject2.PhaseMatches(this.ParentObject))
					{
						gameObject2.TakeDamage(text.RollCached(), "from %t freezing effect!", "Cold", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					}
				}
			}
			base.DrawBreathInCell(C, Buffer, "C", "B", "Y");
		}
	}
}
