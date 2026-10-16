using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125A RID: 4698
	[Serializable]
	public class CrystalGrassy : IPart
	{
		// Token: 0x0600BA7E RID: 47742 RVA: 0x00441BC7 File Offset: 0x0043FDC7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA7F RID: 47743 RVA: 0x00441BCA File Offset: 0x0043FDCA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA80 RID: 47744 RVA: 0x00441BE0 File Offset: 0x0043FDE0
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
							CrystalGrassy.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA81 RID: 47745 RVA: 0x00441C4C File Offset: 0x0043FE4C
		private static int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x0600BA82 RID: 47746 RVA: 0x00441C60 File Offset: 0x0043FE60
		public static double sampleSimplexNoise(string type, int x, int y, int z, int amplitude, float frequencyMultiplier = 1f)
		{
			CrystalGrassy.fastNoise.SetSeed(CrystalGrassy.getSeed(type));
			CrystalGrassy.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			CrystalGrassy.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			CrystalGrassy.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			CrystalGrassy.fastNoise.SetFractalOctaves(3);
			CrystalGrassy.fastNoise.SetFractalLacunarity(0.7f);
			CrystalGrassy.fastNoise.SetFractalGain(1f);
			return Math.Ceiling((double)CrystalGrassy.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x0600BA83 RID: 47747 RVA: 0x00441CE8 File Offset: 0x0043FEE8
		public static void PaintCell(Cell C)
		{
			if (CrystalGrassy.sampleSimplexNoise(C.ParentZone.ZoneID, C.X, C.Y, C.ParentZone.Z, 5, 1f) <= 0.5)
			{
				int num = Stat.Random(1, 16);
				if (num <= 2)
				{
					C.PaintColorString = "&y";
					C.PaintTile = "Tiles/tile-dirt1.png";
					C.PaintDetailColor = "y";
				}
				else if (num <= 13)
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
			else if (CrystalGrassy.sampleSimplexNoise(C.ParentZone.ZoneID, C.X, C.Y, C.ParentZone.Z, 5, 1f) < 5.0)
			{
				C.PaintColorString = "&K";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
		}

		// Token: 0x04004AEA RID: 19178
		[NonSerialized]
		private static FastNoise fastNoise = new FastNoise(1337);
	}
}
