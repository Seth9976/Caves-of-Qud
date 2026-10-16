using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public static class TerrainExtensions
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00002ECC File Offset: 0x000010CC
		public static void UpdateGIMaterials(this Terrain terrain)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect(0f, 0f, 1f, 1f));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002F1C File Offset: 0x0000111C
		public static void UpdateGIMaterials(this Terrain terrain, int x, int y, int width, int height)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			float num = (float)terrain.terrainData.alphamapWidth;
			float num2 = (float)terrain.terrainData.alphamapHeight;
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect((float)x / num, (float)y / num2, (float)width / num, (float)height / num2));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002F84 File Offset: 0x00001184
		[FreeFunction]
		[NativeConditional("INCLUDE_DYNAMIC_GI && ENABLE_RUNTIME_GI")]
		internal static void UpdateGIMaterialsForTerrain(int terrainInstanceID, Rect uvBounds)
		{
			TerrainExtensions.UpdateGIMaterialsForTerrain_Injected(terrainInstanceID, ref uvBounds);
		}

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateGIMaterialsForTerrain_Injected(int terrainInstanceID, [In] ref Rect uvBounds);
	}
}
