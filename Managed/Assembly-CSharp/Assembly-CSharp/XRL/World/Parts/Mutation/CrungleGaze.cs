using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001413 RID: 5139
	[Serializable]
	public class CrungleGaze : IDelayedLineMutation
	{
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600C7A1 RID: 51105 RVA: 0x0048B10C File Offset: 0x0048930C
		public override string Command
		{
			get
			{
				return "CommandCrungleGaze";
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600C7A2 RID: 51106 RVA: 0x0048B113 File Offset: 0x00489313
		public override bool CanRefract
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600C7A3 RID: 51107 RVA: 0x0048B116 File Offset: 0x00489316
		public override GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref CrungleGaze._Projectile))
				{
					CrungleGaze._Projectile = GameObject.CreateUnmodified("ProjectileCrungleGaze");
				}
				return CrungleGaze._Projectile;
			}
		}

		// Token: 0x0600C7A4 RID: 51108 RVA: 0x0048B138 File Offset: 0x00489338
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", this.GetRange(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C7A6 RID: 51110 RVA: 0x0048B190 File Offset: 0x00489390
		public override void FireLine(List<Cell> Path)
		{
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			bool flag = false;
			int i = 0;
			int count = Path.Count;
			while (i < count)
			{
				Cell cell = Path[i];
				foreach (GameObject gameObject in cell.Objects)
				{
					if (this.IsValidTarget(gameObject))
					{
						if (gameObject.IsPlayer())
						{
							SoundManager.PlaySound("Sounds/StatusEffects/sfx_statusEffect_spacetimeWeirdness", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
							Popup.Show("You feel drowsy.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							flag = true;
						}
						else
						{
							gameObject.ApplyEffect(new Asleep(20, false, false, false, true), null);
						}
					}
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
				The.Player.ApplyEffect(new DeepDream(this.ParentObject), null);
			}
		}

		// Token: 0x0600C7A7 RID: 51111 RVA: 0x0048B2E4 File Offset: 0x004894E4
		public override string GetDescription()
		{
			return "You provoke waking dreams with your gaze.";
		}

		// Token: 0x0600C7A8 RID: 51112 RVA: 0x0048B2EB File Offset: 0x004894EB
		public override int GetRange(int Level)
		{
			return 7 + Level;
		}

		// Token: 0x0600C7A9 RID: 51113 RVA: 0x0048B2F0 File Offset: 0x004894F0
		public override int GetCooldown(int Level)
		{
			return 50;
		}

		// Token: 0x0600C7AA RID: 51114 RVA: 0x0048B2F4 File Offset: 0x004894F4
		public override int GetDelay(int Level)
		{
			return 3;
		}

		// Token: 0x0600C7AB RID: 51115 RVA: 0x0048B2F8 File Offset: 0x004894F8
		public override string GetLevelText(int Level)
		{
			return string.Concat(new string[]
			{
				"You can gaze {{rules|",
				this.GetRange(Level).ToString(),
				"}} squares after a ",
				Grammar.Cardinal(this.GetDelay(Level)),
				"-turn warmup and send your target to a waking dream.\nCooldown: {{rules|",
				this.GetCooldown(Level).ToString(),
				"}} rounds"
			});
		}

		// Token: 0x04004E61 RID: 20065
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
