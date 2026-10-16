using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001259 RID: 4697
	[Serializable]
	public class CrystalDirty : IPart
	{
		// Token: 0x0600BA76 RID: 47734 RVA: 0x00441913 File Offset: 0x0043FB13
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA77 RID: 47735 RVA: 0x00441916 File Offset: 0x0043FB16
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA78 RID: 47736 RVA: 0x0044192C File Offset: 0x0043FB2C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				if (!Options.DisableFloorTextureObjects)
				{
					Zone parentZone = E.Cell.ParentZone;
					for (int i = 0; i < parentZone.Height; i++)
					{
						for (int j = 0; j < parentZone.Width; j++)
						{
							CrystalDirty.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA79 RID: 47737 RVA: 0x00441998 File Offset: 0x0043FB98
		private static int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x0600BA7A RID: 47738 RVA: 0x004419AC File Offset: 0x0043FBAC
		public static double sampleSimplexNoise(string type, int x, int y, int z, int amplitude, float frequencyMultiplier = 1f)
		{
			CrystalDirty.fastNoise.SetSeed(CrystalDirty.getSeed(type));
			CrystalDirty.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			CrystalDirty.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			CrystalDirty.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			CrystalDirty.fastNoise.SetFractalOctaves(3);
			CrystalDirty.fastNoise.SetFractalLacunarity(0.7f);
			CrystalDirty.fastNoise.SetFractalGain(1f);
			return Math.Ceiling((double)CrystalDirty.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x0600BA7B RID: 47739 RVA: 0x00441A34 File Offset: 0x0043FC34
		public static void PaintCell(Cell C)
		{
			if (CrystalDirty.sampleSimplexNoise(C.ParentZone.ZoneID, C.X, C.Y, C.ParentZone.Z, 5, 1f) <= 0.5)
			{
				int num = Stat.Random(1, 16);
				if (num <= 10)
				{
					C.PaintColorString = "&y";
					C.PaintTile = "Tiles/tile-dirt1.png";
					C.PaintDetailColor = "y";
				}
				else if (num == 11 || num == 12 || num == 13)
				{
					C.PaintColorString = "&y";
					C.PaintTile = DirtPicker.GetRandomGrassTile();
				}
				else if (num == 14)
				{
					C.PaintColorString = "&K";
					C.PaintTile = DirtPicker.GetRandomGrassTile();
				}
				else
				{
					C.PaintColorString = "&Y";
					C.PaintTile = "Tiles/tile-dirt1.png";
					C.PaintDetailColor = "y";
				}
				int num2 = Stat.Random(1, 5);
				if (num2 == 1)
				{
					C.PaintRenderString = ".";
				}
				if (num2 == 2)
				{
					C.PaintRenderString = ",";
				}
				if (num2 == 3)
				{
					C.PaintRenderString = "`";
				}
				if (num2 == 4)
				{
					C.PaintRenderString = "'";
					return;
				}
			}
			else if (CrystalDirty.sampleSimplexNoise(C.ParentZone.ZoneID, C.X, C.Y, C.ParentZone.Z, 5, 1f) < 5.0)
			{
				C.PaintColorString = "&K";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
		}

		// Token: 0x04004AE9 RID: 19177
		[NonSerialized]
		private static FastNoise fastNoise = new FastNoise(1337);
	}
}
