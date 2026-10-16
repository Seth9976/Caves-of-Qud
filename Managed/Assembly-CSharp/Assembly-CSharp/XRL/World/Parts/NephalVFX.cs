using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000EE1 RID: 3809
	[Serializable]
	public class NephalVFX : IPart
	{
		// Token: 0x06009872 RID: 39026 RVA: 0x00375EB8 File Offset: 0x003740B8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06009873 RID: 39027 RVA: 0x00375ECE File Offset: 0x003740CE
		public override bool FinalRender(RenderEvent E)
		{
			E.WantsToPaint = true;
			return true;
		}

		// Token: 0x06009874 RID: 39028 RVA: 0x00375ED8 File Offset: 0x003740D8
		public override void OnPaint(ScreenBuffer buffer)
		{
			double num = (double)((float)XRLCore.FrameTimer.ElapsedMilliseconds / 1000f);
			float num2 = (float)(num - this.lastStep);
			this.lastStep = num;
			if (!GameManager.dirtyFocused)
			{
				return;
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null && currentZone.IsActive())
			{
				if (num2 > 0f)
				{
					this.nextZoomie -= num2;
					if (this.nextZoomie <= 0f)
					{
						float num3 = 0.1f;
						this.nextZoomie = num3 * (float)Stat.RandomCosmetic(5, 20);
						RadialTextParticle radialTextParticle = new RadialTextParticle();
						radialTextParticle.x = (float)this.ParentObject.CurrentCell.X;
						radialTextParticle.y = (float)this.ParentObject.CurrentCell.Y;
						radialTextParticle.d = (float)Math.Max(Math.Max(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y), Math.Max(80 - this.ParentObject.CurrentCell.X, 25 - this.ParentObject.CurrentCell.Y));
						radialTextParticle.r = (float)Stat.Random(1, 1000) * 2f * 3.1415927f / 1000f;
						radialTextParticle.Life = 1;
						this.zoomies.Add(radialTextParticle);
					}
				}
				for (int i = 0; i < this.zoomies.Count; i++)
				{
					RadialTextParticle radialTextParticle2 = this.zoomies[i];
					int num4 = Convert.ToInt32(this.ParentObject.CurrentCell.X) + (int)((double)radialTextParticle2.d * Math.Cos((double)radialTextParticle2.r));
					int num5 = Convert.ToInt32(this.ParentObject.CurrentCell.Y) + (int)((double)radialTextParticle2.d * Math.Sin((double)radialTextParticle2.r) * 0.6660000085830688);
					Location2D location2D = Location2D.Get(num4, num5);
					if (location2D != null)
					{
						this.drips[location2D] = new NephalVFX.ColorDrip(The.Color[Crayons.GetRandomColorAll(null)[0]]);
					}
					ConsoleChar consoleChar = buffer.get(num4, num5);
					if (consoleChar != null && this.ParentObject.CurrentCell.Location != Location2D.Get(num4, num5))
					{
						consoleChar.Foreground = The.Color[Crayons.GetRandomColorAll(null)[0]];
						consoleChar.TileForeground = The.Color[Crayons.GetRandomColorAll(null)[0]];
						consoleChar.Background = The.Color.k;
						consoleChar.TileBackground = The.Color[Crayons.GetRandomColorAll(null)[0]];
						consoleChar.Detail = The.Color[Crayons.GetRandomColorAll(null)[0]];
					}
					radialTextParticle2.d -= num2 * 4f;
					if (radialTextParticle2.d < 0f)
					{
						radialTextParticle2.d = 0f;
						radialTextParticle2.Life = 0;
					}
					else
					{
						radialTextParticle2.r += num2;
						num4 = Convert.ToInt32(this.ParentObject.CurrentCell.X) + (int)((double)radialTextParticle2.d * Math.Cos((double)radialTextParticle2.r));
						num5 = Convert.ToInt32(this.ParentObject.CurrentCell.Y) + (int)((double)radialTextParticle2.d * Math.Sin((double)radialTextParticle2.r) * 0.6660000085830688);
						consoleChar = buffer.get(num4, num5);
						if (consoleChar != null && this.ParentObject.CurrentCell.Location != Location2D.Get(num4, num5))
						{
							consoleChar.Foreground = The.Color[Crayons.GetRandomColorAll(null)[0]];
							consoleChar.TileForeground = The.Color[Crayons.GetRandomColorAll(null)[0]];
							consoleChar.Background = The.Color.k;
							consoleChar.TileBackground = The.Color[Crayons.GetRandomColorAll(null)[0]];
							consoleChar.Detail = The.Color[Crayons.GetRandomColorAll(null)[0]];
						}
					}
				}
				foreach (KeyValuePair<Location2D, NephalVFX.ColorDrip> keyValuePair in this.drips)
				{
					keyValuePair.Value.life -= num2;
					ConsoleChar consoleChar2 = buffer.get(keyValuePair.Key.X, keyValuePair.Key.Y);
					if (consoleChar2 != null && this.ParentObject.CurrentCell.Location != Location2D.Get(keyValuePair.Key.X, keyValuePair.Key.Y))
					{
						consoleChar2.Foreground = keyValuePair.Value.C;
						consoleChar2.TileForeground = keyValuePair.Value.C;
						consoleChar2.Detail = keyValuePair.Value.C;
					}
				}
				this.drips.RemoveAll<Location2D, NephalVFX.ColorDrip>((KeyValuePair<Location2D, NephalVFX.ColorDrip> d) => d.Value.life <= 0f);
				this.zoomies.RemoveAll((RadialTextParticle z) => z.Life <= 0);
			}
			base.OnPaint(buffer);
		}

		// Token: 0x06009875 RID: 39029 RVA: 0x0037648C File Offset: 0x0037468C
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04003F81 RID: 16257
		[NonSerialized]
		private List<RadialTextParticle> zoomies = new List<RadialTextParticle>();

		// Token: 0x04003F82 RID: 16258
		private float nextZoomie;

		// Token: 0x04003F83 RID: 16259
		[NonSerialized]
		private Dictionary<Location2D, NephalVFX.ColorDrip> drips = new Dictionary<Location2D, NephalVFX.ColorDrip>();

		// Token: 0x04003F84 RID: 16260
		private double lastStep;

		// Token: 0x02001C84 RID: 7300
		private class ColorDrip
		{
			// Token: 0x0600F188 RID: 61832 RVA: 0x0054C861 File Offset: 0x0054AA61
			public ColorDrip(Color C)
			{
				this.C = C;
				this.life = 0.2f;
			}

			// Token: 0x04007066 RID: 28774
			public Color C;

			// Token: 0x04007067 RID: 28775
			public float life;
		}
	}
}
