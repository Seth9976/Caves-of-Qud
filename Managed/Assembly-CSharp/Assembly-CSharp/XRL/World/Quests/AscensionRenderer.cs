using System;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Quests
{
	// Token: 0x02000B1B RID: 2843
	public class AscensionRenderer : IPart
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06007223 RID: 29219 RVA: 0x002B1D7C File Offset: 0x002AFF7C
		private bool ascending
		{
			get
			{
				if (this.system == null)
				{
					this.system = The.Game.GetSystem<AscensionSystem>();
				}
				AscensionSystem ascensionSystem = this.system;
				return ascensionSystem != null && ascensionSystem.Stage == 2;
			}
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x002B1DAA File Offset: 0x002AFFAA
		public override bool FinalRender(RenderEvent E)
		{
			E.WantsToPaint = true;
			return true;
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x002B1DB4 File Offset: 0x002AFFB4
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x002B1DB7 File Offset: 0x002AFFB7
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.ascending && GameObject.Validate(this.ParentObject))
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x002B1DE0 File Offset: 0x002AFFE0
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.fastNoise == null)
			{
				this.fastNoise = new FastNoise(1337);
				this.fastNoise.SetSeed(XRLCore.Core.Game.GetWorldSeed("ASCENTCOUNDS"));
				this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
				this.fastNoise.SetFrequency(0.02f);
				this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
				this.fastNoise.SetFractalOctaves(5);
				this.fastNoise.SetFractalLacunarity(2f);
				this.fastNoise.SetFractalGain(0.5f);
				this.fastNoise2 = new FastNoise(1337);
				this.fastNoise2.SetSeed(XRLCore.Core.Game.GetWorldSeed("ASCENTCOUNDS2"));
				this.fastNoise2.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
				this.fastNoise2.SetFrequency(0.02f);
				this.fastNoise2.SetFractalType(FastNoise.FractalType.FBM);
				this.fastNoise2.SetFractalOctaves(5);
				this.fastNoise2.SetFractalLacunarity(2f);
				this.fastNoise2.SetFractalGain(0.5f);
			}
			if (this.lastElapsed == -1.0)
			{
				this.lastElapsed = (double)XRLCore.FrameTimer.ElapsedMilliseconds;
			}
			double num = ((double)XRLCore.FrameTimer.ElapsedMilliseconds - this.lastElapsed) / 1000.0;
			this.lastElapsed = (double)XRLCore.FrameTimer.ElapsedMilliseconds;
			double num2 = 5.0;
			double num3 = 1.0;
			double num4 = 2200.0;
			double num5 = -1400.0;
			this.accel = 0.0;
			double num6 = -8.0;
			int num7 = 1;
			int num8 = 2;
			int num9 = num8;
			if (this.ascending)
			{
				if (num9 == num7)
				{
					if (this.climbTime <= 0.0)
					{
						this.chargeTime += num;
						if (this.chargeTime > num2)
						{
							this.climbTime = num3;
							this.chargeTime = 0.0;
							this.accel = num4;
							SoundManager.PlaySound("Sounds/Interact/sfx_interact_artifact_finish_whir", 0f, 0.1f, 1f, SoundRequest.SoundEffectType.None, 0f);
						}
					}
					else
					{
						this.climbTime -= num;
						this.accel += num5 * num;
						this.altitude += this.accel * num;
						if (this.climbTime <= 0.0)
						{
							this.chargeTime = 0.0;
							SoundManager.PlaySound("Sounds/Grenade/sfx_grenade_highExplosive_explode", 0f, 0.1f, 1f, SoundRequest.SoundEffectType.None, 0f);
							CombatJuice.cameraShake(0.1f, false);
						}
					}
				}
				else if (num9 == num8)
				{
					this.altitude += num6 * num;
				}
				int num10 = 46;
				int num11 = 6;
				for (int i = num10; i < num10 + num11; i++)
				{
					for (int j = 0; j < 25; j++)
					{
						buffer[i, j].Char = '|';
						if (Options.UseTiles)
						{
							buffer[i, j].Tile = "Furniture/spindle-ribbon-middle.png";
						}
						buffer[i, j].SetForeground('K');
						buffer[i, j].SetDetail('k');
					}
				}
				for (int k = 0; k < 80; k++)
				{
					for (int l = 0; l < 25; l++)
					{
						if (buffer[k, l].Tile != null)
						{
							buffer[k, l].Tile == "Furniture/spindle-ribbon-middle.png";
						}
						float noise = this.fastNoise.GetNoise((float)(k / 4) - (float)(this.t / 200.0), (float)((double)l + this.altitude));
						float noise2 = this.fastNoise2.GetNoise((float)(k / 4) - (float)(this.t / 3000.0), (float)((double)l + this.altitude));
						if (buffer[k, l].Tile == null && (double)noise2 > 0.3)
						{
							buffer[k, l].Tile = (((double)noise2 > 0.8) ? "Tiles2/gas_0.png" : (((double)noise2 > 0.6) ? "Tiles2/gas_0.png" : "Tiles2/gas_0.png"));
							buffer[k, l].SetForeground('K');
							buffer[k, l].SetBackground('k');
						}
						if ((double)noise > 0.5)
						{
							buffer[k, l].Tile = (((double)noise > 0.8) ? "Tiles2/gas_0.png" : (((double)noise > 0.6) ? "Tiles2/gas_0.png" : "Tiles2/gas_0.png"));
							buffer[k, l].SetForeground('K');
							buffer[k, l].SetBackground('k');
						}
					}
				}
			}
		}

		// Token: 0x040034F7 RID: 13559
		[NonSerialized]
		private AscensionSystem system;

		// Token: 0x040034F8 RID: 13560
		private FastNoise fastNoise;

		// Token: 0x040034F9 RID: 13561
		private FastNoise fastNoise2;

		// Token: 0x040034FA RID: 13562
		public double t;

		// Token: 0x040034FB RID: 13563
		public double lastElapsed = -1.0;

		// Token: 0x040034FC RID: 13564
		public double chargeTime;

		// Token: 0x040034FD RID: 13565
		public double climbTime;

		// Token: 0x040034FE RID: 13566
		public double altitude;

		// Token: 0x040034FF RID: 13567
		public double accel;
	}
}
