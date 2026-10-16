using System;
using ConsoleLib.Console;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140B RID: 5131
	[Serializable]
	public class FireBreather : BreatherBase
	{
		// Token: 0x0600C752 RID: 51026 RVA: 0x0048A252 File Offset: 0x00488452
		public override string GetFaceObject()
		{
			return "Ghostly Flames";
		}

		// Token: 0x0600C753 RID: 51027 RVA: 0x0048A259 File Offset: 0x00488459
		public override string GetCommandDisplayName()
		{
			return "Breathe Fire";
		}

		// Token: 0x0600C754 RID: 51028 RVA: 0x0048A260 File Offset: 0x00488460
		public override string GetDescription()
		{
			return "You breathe fire.";
		}

		// Token: 0x0600C755 RID: 51029 RVA: 0x0048A268 File Offset: 0x00488468
		public override string GetLevelText(int Level)
		{
			string text = "Breathes fire in a cone.\n";
			text = text + "Damage: " + this.ComputeDamage(Level) + "\n";
			text = text + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n";
			text = text + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n";
			text += "Cooldown: 15 rounds\n";
			if (Level != base.Level)
			{
				text += "{{rules|Increased temperature}}";
			}
			return text;
		}

		// Token: 0x0600C756 RID: 51030 RVA: 0x0048A2F5 File Offset: 0x004884F5
		public override string GetBreathName()
		{
			return "fire";
		}

		// Token: 0x0600C757 RID: 51031 RVA: 0x0048A2FC File Offset: 0x004884FC
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

		// Token: 0x0600C758 RID: 51032 RVA: 0x0048A35B File Offset: 0x0048855B
		public string ComputeDamage()
		{
			return this.ComputeDamage(base.Level);
		}

		// Token: 0x0600C759 RID: 51033 RVA: 0x0048A369 File Offset: 0x00488569
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			base.CollectStats(stats, Level);
			stats.Set("Damage", this.ComputeDamage(Level), false, 0);
		}

		// Token: 0x0600C75A RID: 51034 RVA: 0x0048A387 File Offset: 0x00488587
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C75B RID: 51035 RVA: 0x0048A391 File Offset: 0x00488591
		public override bool ChangeLevel(int NewLevel)
		{
			base.StatShifter.SetStatShift("HeatResistance", base.Level * 2, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C75C RID: 51036 RVA: 0x0048A3B4 File Offset: 0x004885B4
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.Unmutate(GO);
		}

		// Token: 0x0600C75D RID: 51037 RVA: 0x0048A3C8 File Offset: 0x004885C8
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			string text = this.ComputeDamage();
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjectsInCell())
				{
					if (gameObject.PhaseMatches(this.ParentObject))
					{
						gameObject.TemperatureChange(310 + 25 * base.Level, this.ParentObject, false, false, false, false, 0, null, null);
						if (doEffect)
						{
							for (int i = 0; i < 5; i++)
							{
								gameObject.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
							for (int j = 0; j < 5; j++)
							{
								gameObject.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
							for (int k = 0; k < 5; k++)
							{
								gameObject.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
							}
						}
					}
				}
				foreach (GameObject gameObject2 in C.GetObjectsWithPart("Combat"))
				{
					if (gameObject2.PhaseMatches(this.ParentObject))
					{
						Damage damage = new Damage(Stat.Roll(text, null));
						damage.AddAttribute("Fire");
						damage.AddAttribute("Heat");
						Event @event = Event.New("TakeDamage", 0, 0, 0);
						@event.AddParameter("Damage", damage);
						@event.AddParameter("Owner", this.ParentObject);
						@event.AddParameter("Attacker", this.ParentObject);
						@event.AddParameter("Message", "from %t flames!");
						gameObject2.FireEvent(@event);
					}
				}
			}
			if (doEffect)
			{
				C.Flameburst(Buffer);
			}
		}
	}
}
