using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.ZoneBuilders;

namespace XRL.World.Biomes
{
	// Token: 0x02000A1E RID: 2590
	[HasGameBasedStaticCache]
	[HasWishCommand]
	public class PsychicBiome : IBiome
	{
		// Token: 0x06006D4F RID: 27983 RVA: 0x00249C7C File Offset: 0x00247E7C
		public override int GetBiomeValue(string ZoneID)
		{
			string text;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			if (!ZoneID.Parse(ZoneID, out text, out num, out num2, out num3, out num4, out num5) || num5 < 10 || num < 72 || num > 78 || num2 < 2 || num2 > 17)
			{
				return 0;
			}
			if (PsychicBiome.SeedMap == null)
			{
				PsychicBiome.SeedMap = this.CreateSeedMap();
			}
			ushort[] array = PsychicBiome.SeedMap[num3 + (num - 72) * Definitions.Width, num4 + (num2 - 2) * Definitions.Height];
			ushort num6 = array[(num5 - 10) % array.Length];
			if (num6 == 0)
			{
				return 0;
			}
			GameObject terrainObjectForZone = ZoneManager.GetTerrainObjectForZone(num, num2, text);
			if (terrainObjectForZone == null || !terrainObjectForZone.GetBlueprint(true).DescendsFrom("TerrainMoonStair"))
			{
				return 0;
			}
			return (int)num6;
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x00249D2C File Offset: 0x00247F2C
		private ushort[,][] CreateSeedMap()
		{
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			Cell cell;
			if (zone == null)
			{
				cell = null;
			}
			else
			{
				GameObject gameObject = zone.FindFirstObject("TerrainEynRoj");
				cell = ((gameObject != null) ? gameObject.CurrentCell : null);
			}
			Cell cell2 = cell;
			if (cell2 != null)
			{
				PsychicBiome.CX = cell2.X * 3 + 1;
				PsychicBiome.CY = cell2.Y * 3 + 1;
			}
			ushort[,][] array = new ushort[21, 48][];
			for (int i = 0; i < 48; i++)
			{
				for (int j = 0; j < 21; j++)
				{
					array[j, i] = new ushort[(j == PsychicBiome.CX - 216 && i == PsychicBiome.CY - 6) ? 40 : 10];
				}
			}
			FastNoise fastNoise = new FastNoise(The.Game.GetWorldSeed("PsychicBiome1"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			fastNoise.SetFrequency(0.3f);
			fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			fastNoise.SetFractalOctaves(2);
			fastNoise.SetFractalLacunarity(8f);
			fastNoise.SetFractalGain(0.5f);
			global::System.Random random = new global::System.Random(The.Game.GetWorldSeed("PsychicBiomeValue"));
			for (int k = 0; k < 10; k++)
			{
				for (int l = 0; l < 48; l++)
				{
					for (int m = 0; m < 21; m++)
					{
						if (fastNoise.GetValue((float)m, (float)l, (float)k) >= 0.85f && !this.TryAssignNeighbour(array, m, l, k, 65535))
						{
							array[m, l][k] = (ushort)random.Next(255);
						}
					}
				}
			}
			fastNoise.SetSeed(The.Game.GetWorldSeed("PsychicBiome2"));
			for (int n = 0; n < 10; n++)
			{
				for (int num = 0; num < 48; num++)
				{
					for (int num2 = 0; num2 < 21; num2++)
					{
						if (fastNoise.GetValue((float)num2, (float)num, (float)n) >= 0.85f && !this.TryAssignNeighbour(array, num2, num, n, 65280))
						{
							ushort[] array2 = array[num2, num];
							int num3 = n;
							array2[num3] |= (ushort)(random.Next(255) << 8);
						}
					}
				}
			}
			if (cell2 != null)
			{
				int num4 = PsychicBiome.CX - 216;
				int num5 = PsychicBiome.CY - 6;
				int num6 = 0;
				ushort num7 = 0;
				array[num4, num5][0] = 0;
				int num8 = 1;
				while (num8 < 40)
				{
					if (num6 > 0)
					{
						num6--;
						goto IL_02B2;
					}
					if (random.NextDouble() >= 0.5)
					{
						num7 = 0;
						goto IL_02B2;
					}
					ushort num9 = num7;
					num7 = (ushort)random.Next(255);
					num6 = random.Next(1, 3);
					if (num9 == 0 || random.NextDouble() >= 0.3)
					{
						goto IL_02B2;
					}
					array[num4, num5][num8] = (ushort)((int)num9 | ((int)num7 << 8));
					IL_02C1:
					num8++;
					continue;
					IL_02B2:
					array[num4, num5][num8] = num7;
					goto IL_02C1;
				}
			}
			return array;
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x0024A00C File Offset: 0x0024820C
		private bool TryAssignNeighbour(ushort[,][] Map, int X, int Y, int Z, ushort Mask = 65535)
		{
			ushort num = ((X > 0) ? (Map[X - 1, Y][Z] & Mask) : 0);
			ushort num2 = ((Y > 0) ? (Map[X, Y - 1][Z] & Mask) : 0);
			if (num2 != 0)
			{
				ushort[] array = Map[X, Y];
				array[Z] |= num2;
				if (num > 0 && num != num2)
				{
					ushort num3 = ~Mask;
					int num4 = X - 1;
					while (num4 >= 0 && (Map[num4, Y][Z] & Mask) == num)
					{
						ushort[] array2 = Map[num4, Y];
						array2[Z] &= num3;
						ushort[] array3 = Map[num4, Y];
						array3[Z] |= num2;
						num4--;
					}
				}
				return true;
			}
			if (num != 0)
			{
				ushort[] array4 = Map[X, Y];
				array4[Z] |= num;
				return true;
			}
			return false;
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x0024A0D8 File Offset: 0x002482D8
		public override void MutateZone(Zone Z)
		{
			Z.BuildReachabilityFromEdges();
			if (Z.wX * 3 + Z.X != PsychicBiome.CX || Z.wY * 3 + Z.Y != PsychicBiome.CY)
			{
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, Z.area, "BiomePsychic_Generic", null, null);
			}
			int biomeValue = this.GetBiomeValue(Z.ZoneID);
			if (biomeValue >= 1)
			{
				Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_psionic");
			}
			foreach (MutationEntry mutationEntry in this.GetMutationsFor(biomeValue))
			{
				if (!mutationEntry.BiomeTable.IsNullOrEmpty())
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, Z.area, mutationEntry.BiomeTable, null, null);
				}
			}
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x0024A1B0 File Offset: 0x002483B0
		public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			int biomeValue = this.GetBiomeValue(ZoneID);
			if (biomeValue == 0)
			{
				return Name;
			}
			foreach (MutationEntry mutationEntry in this.GetMutationsFor(biomeValue))
			{
				Name = IBiome.MutateZoneNameWith(Name, mutationEntry.BiomeEpithet, mutationEntry.BiomeAdjective);
			}
			return Name;
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x0024A220 File Offset: 0x00248420
		public List<MutationEntry> GetMutationsFor(int Value)
		{
			PsychicBiome.Result.Clear();
			if (Value == 0)
			{
				return PsychicBiome.Result;
			}
			if (PsychicBiome.Pool == null)
			{
				PsychicBiome.Pool = MutationFactory.GetMutationsOfCategory("Mental", false).FindAll((MutationEntry x) => !x.BiomeAdjective.IsNullOrEmpty() || !x.BiomeEpithet.IsNullOrEmpty());
			}
			int num = Value & 255;
			if (num != 0)
			{
				PsychicBiome.Result.Add(PsychicBiome.Pool.GetRandomElement<MutationEntry>(new global::System.Random(num)));
			}
			int num2 = Value >> 8;
			if (num2 != 0 && num2 != num)
			{
				MutationEntry randomElement = PsychicBiome.Pool.GetRandomElement<MutationEntry>(new global::System.Random(num2));
				if (!PsychicBiome.Result.Contains(randomElement))
				{
					PsychicBiome.Result.Add(randomElement);
				}
			}
			return PsychicBiome.Result;
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x0024A2DC File Offset: 0x002484DC
		public override GameObject MutateGameObject(GameObject Object, string ZoneID)
		{
			if (!Object.IsCombatObject())
			{
				return Object;
			}
			if (!Object.IsMutant())
			{
				return Object;
			}
			string text;
			global::System.Random random = (Object.Property.TryGetValue("Batch", out text) ? new global::System.Random(The.Game.GetWorldSeed(text)) : Stat.Rnd);
			int biomeValue = this.GetBiomeValue(ZoneID);
			Mutations mutations = Object.RequirePart<Mutations>(false);
			foreach (MutationEntry mutationEntry in this.GetMutationsFor(biomeValue))
			{
				if (!Object.HasPart(mutationEntry.Class) && mutations.IncludedInMutatePool(mutationEntry, false) && random.NextDouble() >= 0.25)
				{
					int num = Mathf.Clamp(Object.Level / 4, 1, 10);
					if (mutationEntry.MaxLevel > 0 && num > mutationEntry.MaxLevel)
					{
						num = mutationEntry.MaxLevel;
					}
					mutations.AddMutation(mutationEntry, num);
				}
			}
			return Object;
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x0024A3E4 File Offset: 0x002485E4
		public override bool IsNotable(string ZoneID)
		{
			return false;
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x0024A3E8 File Offset: 0x002485E8
		[WishCommand("chavvah:biomes", null)]
		public static void WishRootBiomes()
		{
			PsychicBiome psychicBiome = (PsychicBiome)BiomeManager.Biomes["Psychic"];
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			for (int i = 1; i < 40; i++)
			{
				string text = ZoneID.Assemble("JoppaWorld", PsychicBiome.CX / 3, PsychicBiome.CY / 3, 1, 1, 10 + i);
				int biomeValue = psychicBiome.GetBiomeValue(text);
				stringBuilder.Compound('[', '\n').Append(10 + i).Append("]: ");
				stringBuilder.AppendJoin<string>(", ", from x in psychicBiome.GetMutationsFor(biomeValue)
					select x.Name);
			}
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x040031EE RID: 12782
		public const int X1 = 72;

		// Token: 0x040031EF RID: 12783
		public const int Y1 = 2;

		// Token: 0x040031F0 RID: 12784
		public const int X2 = 78;

		// Token: 0x040031F1 RID: 12785
		public const int Y2 = 17;

		// Token: 0x040031F2 RID: 12786
		public const int Width = 21;

		// Token: 0x040031F3 RID: 12787
		public const int Height = 48;

		// Token: 0x040031F4 RID: 12788
		public const int Depth = 10;

		// Token: 0x040031F5 RID: 12789
		public const int RootDepth = 40;

		// Token: 0x040031F6 RID: 12790
		public const float Cutoff = 0.85f;

		// Token: 0x040031F7 RID: 12791
		public const string Table = "BiomePsychic_Generic";

		// Token: 0x040031F8 RID: 12792
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static ushort[,][] SeedMap = null;

		// Token: 0x040031F9 RID: 12793
		public static int CX = 229;

		// Token: 0x040031FA RID: 12794
		public static int CY = 16;

		// Token: 0x040031FB RID: 12795
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		private static List<MutationEntry> Pool;

		// Token: 0x040031FC RID: 12796
		private static List<MutationEntry> Result = new List<MutationEntry>(2);
	}
}
