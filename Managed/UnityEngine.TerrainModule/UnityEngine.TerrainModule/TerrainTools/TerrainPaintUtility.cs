using System;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000049 RID: 73
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainPaintUtility
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00009030 File Offset: 0x00007230
		public static Material GetBuiltinPaintMaterial()
		{
			bool flag = TerrainPaintUtility.s_BuiltinPaintMaterial == null;
			if (flag)
			{
				TerrainPaintUtility.s_BuiltinPaintMaterial = new Material(Shader.Find("Hidden/TerrainEngine/PaintHeight"));
			}
			return TerrainPaintUtility.s_BuiltinPaintMaterial;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000906C File Offset: 0x0000726C
		public static void GetBrushWorldSizeLimits(out float minBrushWorldSize, out float maxBrushWorldSize, float terrainTileWorldSize, int terrainTileTextureResolutionPixels, int minBrushResolutionPixels = 1, int maxBrushResolutionPixels = 8192)
		{
			bool flag = terrainTileTextureResolutionPixels <= 0;
			if (flag)
			{
				minBrushWorldSize = terrainTileWorldSize;
				maxBrushWorldSize = terrainTileWorldSize;
			}
			else
			{
				float num = terrainTileWorldSize / (float)terrainTileTextureResolutionPixels;
				minBrushWorldSize = (float)minBrushResolutionPixels * num;
				float num2 = (float)Mathf.Min(maxBrushResolutionPixels, SystemInfo.maxTextureSize);
				maxBrushWorldSize = num2 * num;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000090B0 File Offset: 0x000072B0
		public static BrushTransform CalculateBrushTransform(Terrain terrain, Vector2 brushCenterTerrainUV, float brushSize, float brushRotationDegrees)
		{
			float num = brushRotationDegrees * 0.017453292f;
			float num2 = Mathf.Cos(num);
			float num3 = Mathf.Sin(num);
			Vector2 vector = new Vector2(num2, -num3) * brushSize;
			Vector2 vector2 = new Vector2(num3, num2) * brushSize;
			Vector3 size = terrain.terrainData.size;
			Vector2 vector3 = brushCenterTerrainUV * new Vector2(size.x, size.z);
			Vector2 vector4 = vector3 - 0.5f * vector - 0.5f * vector2;
			BrushTransform brushTransform = new BrushTransform(vector4, vector, vector2);
			return brushTransform;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009154 File Offset: 0x00007354
		public static void BuildTransformPaintContextUVToPaintContextUV(PaintContext src, PaintContext dst, out Vector4 scaleOffset)
		{
			float num = ((float)src.pixelRect.xMin - 0.5f) * src.pixelSize.x;
			float num2 = ((float)src.pixelRect.yMin - 0.5f) * src.pixelSize.y;
			float num3 = (float)src.pixelRect.width * src.pixelSize.x;
			float num4 = (float)src.pixelRect.height * src.pixelSize.y;
			float num5 = ((float)dst.pixelRect.xMin - 0.5f) * dst.pixelSize.x;
			float num6 = ((float)dst.pixelRect.yMin - 0.5f) * dst.pixelSize.y;
			float num7 = (float)dst.pixelRect.width * dst.pixelSize.x;
			float num8 = (float)dst.pixelRect.height * dst.pixelSize.y;
			scaleOffset = new Vector4(num3 / num7, num4 / num8, (num - num5) / num7, (num2 - num6) / num8);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009288 File Offset: 0x00007488
		public static void SetupTerrainToolMaterialProperties(PaintContext paintContext, in BrushTransform brushXform, Material material)
		{
			float num = ((float)paintContext.pixelRect.xMin - 0.5f) * paintContext.pixelSize.x;
			float num2 = ((float)paintContext.pixelRect.yMin - 0.5f) * paintContext.pixelSize.y;
			float num3 = (float)paintContext.pixelRect.width * paintContext.pixelSize.x;
			float num4 = (float)paintContext.pixelRect.height * paintContext.pixelSize.y;
			Vector2 vector = num3 * brushXform.targetX;
			Vector2 vector2 = num4 * brushXform.targetY;
			Vector2 vector3 = brushXform.targetOrigin + num * brushXform.targetX + num2 * brushXform.targetY;
			material.SetVector("_PCUVToBrushUVScales", new Vector4(vector.x, vector.y, vector2.x, vector2.y));
			material.SetVector("_PCUVToBrushUVOffset", new Vector4(vector3.x, vector3.y, 0f, 0f));
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000093B4 File Offset: 0x000075B4
		internal static bool paintTextureUsesCopyTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000093D4 File Offset: 0x000075D4
		internal static PaintContext InitializePaintContext(Terrain terrain, int targetWidth, int targetHeight, RenderTextureFormat pcFormat, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			PaintContext paintContext = PaintContext.CreateFromBounds(terrain, boundsInTerrainSpace, targetWidth, targetHeight, extraBorderPixels, sharedBoundaryTexel, fillOutsideTerrain);
			paintContext.CreateRenderTargets(pcFormat);
			return paintContext;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009400 File Offset: 0x00007600
		public static void ReleaseContextResources(PaintContext ctx)
		{
			ctx.Cleanup(true);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000940C File Offset: 0x0000760C
		public static PaintContext BeginPaintHeightmap(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.heightmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
			paintContext.GatherHeightmap();
			return paintContext;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009444 File Offset: 0x00007644
		public static void EndPaintHeightmap(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHeightmap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009458 File Offset: 0x00007658
		public static PaintContext BeginPaintHoles(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int holesResolution = terrain.terrainData.holesResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, holesResolution, holesResolution, Terrain.holesRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, false, fillOutsideTerrain);
			paintContext.GatherHoles();
			return paintContext;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009490 File Offset: 0x00007690
		public static void EndPaintHoles(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHoles(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000094A4 File Offset: 0x000076A4
		public static PaintContext CollectNormals(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.normalmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
			paintContext.GatherNormals();
			return paintContext;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000094DC File Offset: 0x000076DC
		public static PaintContext BeginPaintTexture(Terrain terrain, Rect boundsInTerrainSpace, TerrainLayer inputLayer, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			bool flag = inputLayer == null;
			PaintContext paintContext;
			if (flag)
			{
				paintContext = null;
			}
			else
			{
				int alphamapResolution = terrain.terrainData.alphamapResolution;
				PaintContext paintContext2 = TerrainPaintUtility.InitializePaintContext(terrain, alphamapResolution, alphamapResolution, RenderTextureFormat.R8, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
				paintContext2.GatherAlphamap(inputLayer, true);
				paintContext = paintContext2;
			}
			return paintContext;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009523 File Offset: 0x00007723
		public static void EndPaintTexture(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterAlphamap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009538 File Offset: 0x00007738
		public static Material GetBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_BlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_BlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainBlitCopyZWrite"));
			}
			return TerrainPaintUtility.s_BlitMaterial;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009574 File Offset: 0x00007774
		public static Material GetHeightBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_HeightBlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_HeightBlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/HeightBlitCopy"));
			}
			return TerrainPaintUtility.s_HeightBlitMaterial;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000095B0 File Offset: 0x000077B0
		public static Material GetCopyTerrainLayerMaterial()
		{
			bool flag = !TerrainPaintUtility.s_CopyTerrainLayerMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_CopyTerrainLayerMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainLayerUtils"));
			}
			return TerrainPaintUtility.s_CopyTerrainLayerMaterial;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000095EC File Offset: 0x000077EC
		internal static void DrawQuad(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture)
		{
			TerrainPaintUtility.DrawQuad2(destinationPixels, sourcePixels, sourceTexture, sourcePixels, sourceTexture);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000095FC File Offset: 0x000077FC
		internal static void DrawQuad2(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture, RectInt sourcePixels2, Texture sourceTexture2)
		{
			bool flag = destinationPixels.width > 0 && destinationPixels.height > 0;
			if (flag)
			{
				Rect rect = new Rect((float)sourcePixels.x / (float)sourceTexture.width, (float)sourcePixels.y / (float)sourceTexture.height, (float)sourcePixels.width / (float)sourceTexture.width, (float)sourcePixels.height / (float)sourceTexture.height);
				Rect rect2 = new Rect((float)sourcePixels2.x / (float)sourceTexture2.width, (float)sourcePixels2.y / (float)sourceTexture2.height, (float)sourcePixels2.width / (float)sourceTexture2.width, (float)sourcePixels2.height / (float)sourceTexture2.height);
				GL.Begin(7);
				GL.Color(new Color(1f, 1f, 1f, 1f));
				GL.MultiTexCoord2(0, rect.x, rect.y);
				GL.MultiTexCoord2(1, rect2.x, rect2.y);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.y, 0f);
				GL.MultiTexCoord2(0, rect.x, rect.yMax);
				GL.MultiTexCoord2(1, rect2.x, rect2.yMax);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.yMax);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.yMax);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.y);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.y);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.y, 0f);
				GL.End();
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000097FC File Offset: 0x000079FC
		internal static void DrawQuadPadded(RectInt destinationPixels, RectInt destinationPixelsPadded, RectInt sourcePixels, RectInt sourcePixelsPadded, Texture sourceTexture)
		{
			GL.Begin(7);
			GL.Color(new Color(1f, 1f, 1f, 1f));
			for (int i = 0; i < 3; i++)
			{
				bool flag = i == 0;
				Vector2Int vector2Int;
				Vector2Int vector2Int2;
				Vector2 vector;
				if (flag)
				{
					vector2Int = new Vector2Int(sourcePixelsPadded.yMin, sourcePixels.yMin);
					vector2Int2 = new Vector2Int(destinationPixelsPadded.yMin, destinationPixels.yMin);
					vector = new Vector2(-1f, 0f);
				}
				else
				{
					bool flag2 = i == 1;
					if (flag2)
					{
						vector2Int = new Vector2Int(sourcePixels.yMin, sourcePixels.yMax);
						vector2Int2 = new Vector2Int(destinationPixels.yMin, destinationPixels.yMax);
						vector = new Vector2(0f, 0f);
					}
					else
					{
						vector2Int = new Vector2Int(sourcePixels.yMax, sourcePixelsPadded.yMax);
						vector2Int2 = new Vector2Int(destinationPixels.yMax, destinationPixelsPadded.yMax);
						vector = new Vector2(0f, -1f);
					}
				}
				bool flag3 = vector2Int[0] >= vector2Int[1];
				if (!flag3)
				{
					for (int j = 0; j < 3; j++)
					{
						bool flag4 = j == 0;
						Vector2Int vector2Int3;
						Vector2Int vector2Int4;
						Vector2 vector2;
						if (flag4)
						{
							vector2Int3 = new Vector2Int(sourcePixelsPadded.xMin, sourcePixels.xMin);
							vector2Int4 = new Vector2Int(destinationPixelsPadded.xMin, destinationPixels.xMin);
							vector2 = new Vector2(-1f, 0f);
						}
						else
						{
							bool flag5 = j == 1;
							if (flag5)
							{
								vector2Int3 = new Vector2Int(sourcePixels.xMin, sourcePixels.xMax);
								vector2Int4 = new Vector2Int(destinationPixels.xMin, destinationPixels.xMax);
								vector2 = new Vector2(0f, 0f);
							}
							else
							{
								vector2Int3 = new Vector2Int(sourcePixels.xMax, sourcePixelsPadded.xMax);
								vector2Int4 = new Vector2Int(destinationPixels.xMax, destinationPixelsPadded.xMax);
								vector2 = new Vector2(0f, -1f);
							}
						}
						bool flag6 = vector2Int3[0] >= vector2Int3[1];
						if (!flag6)
						{
							Rect rect = new Rect((float)vector2Int3[0] / (float)sourceTexture.width, (float)vector2Int[0] / (float)sourceTexture.height, (float)(vector2Int3[1] - vector2Int3[0]) / (float)sourceTexture.width, (float)(vector2Int[1] - vector2Int[0]) / (float)sourceTexture.height);
							GL.TexCoord2(rect.x, rect.y);
							GL.Vertex3((float)vector2Int4[0], (float)vector2Int2[0], 0.5f * (vector2[0] + vector[0]));
							GL.TexCoord2(rect.x, rect.yMax);
							GL.Vertex3((float)vector2Int4[0], (float)vector2Int2[1], 0.5f * (vector2[0] + vector[1]));
							GL.TexCoord2(rect.xMax, rect.yMax);
							GL.Vertex3((float)vector2Int4[1], (float)vector2Int2[1], 0.5f * (vector2[1] + vector[1]));
							GL.TexCoord2(rect.xMax, rect.y);
							GL.Vertex3((float)vector2Int4[1], (float)vector2Int2[0], 0.5f * (vector2[1] + vector[0]));
						}
					}
				}
			}
			GL.End();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009BC4 File Offset: 0x00007DC4
		internal static RectInt CalcPixelRectFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int textureWidth, int textureHeight, int extraBorderPixels, bool sharedBoundaryTexel)
		{
			float num = ((float)textureWidth - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.x;
			float num2 = ((float)textureHeight - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.z;
			int num3 = Mathf.FloorToInt(boundsInTerrainSpace.xMin * num) - extraBorderPixels;
			int num4 = Mathf.FloorToInt(boundsInTerrainSpace.yMin * num2) - extraBorderPixels;
			int num5 = Mathf.CeilToInt(boundsInTerrainSpace.xMax * num) + extraBorderPixels;
			int num6 = Mathf.CeilToInt(boundsInTerrainSpace.yMax * num2) + extraBorderPixels;
			int num7 = PaintContext.ClampContextResolution(num5 - num3 + 1);
			int num8 = PaintContext.ClampContextResolution(num6 - num4 + 1);
			return new RectInt(num3, num4, num7, num8);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009C94 File Offset: 0x00007E94
		public static Texture2D GetTerrainAlphaMapChecked(Terrain terrain, int mapIndex)
		{
			bool flag = mapIndex >= terrain.terrainData.alphamapTextureCount;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain alphamap information.");
			}
			return terrain.terrainData.GetAlphamapTexture(mapIndex);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public static int FindTerrainLayerIndex(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			for (int i = 0; i < terrainLayers.Length; i++)
			{
				bool flag = terrainLayers[i] == inputLayer;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009D1C File Offset: 0x00007F1C
		internal static int AddTerrainLayer(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			int num = terrainLayers.Length;
			TerrainLayer[] array = new TerrainLayer[num + 1];
			Array.Copy(terrainLayers, 0, array, 0, num);
			array[num] = inputLayer;
			terrain.terrainData.terrainLayers = array;
			return num;
		}

		// Token: 0x04000135 RID: 309
		private static Material s_BuiltinPaintMaterial;

		// Token: 0x04000136 RID: 310
		private static Material s_BlitMaterial;

		// Token: 0x04000137 RID: 311
		private static Material s_HeightBlitMaterial;

		// Token: 0x04000138 RID: 312
		private static Material s_CopyTerrainLayerMaterial;
	}
}
