using System;
using System.Diagnostics;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainCallbacks
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000E6 RID: 230 RVA: 0x000032B4 File Offset: 0x000014B4
		// (remove) Token: 0x060000E7 RID: 231 RVA: 0x000032E8 File Offset: 0x000014E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000E8 RID: 232 RVA: 0x0000331C File Offset: 0x0000151C
		// (remove) Token: 0x060000E9 RID: 233 RVA: 0x00003350 File Offset: 0x00001550
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event TerrainCallbacks.TextureChangedCallback textureChanged;

		// Token: 0x060000EA RID: 234 RVA: 0x00003384 File Offset: 0x00001584
		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000033C8 File Offset: 0x000015C8
		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x060000ED RID: 237
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x060000F1 RID: 241
		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);
	}
}
