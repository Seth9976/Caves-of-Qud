using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TerrainUtils;

namespace UnityEngine.TerrainTools
{
	// Token: 0x0200003B RID: 59
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public class PaintContext
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000075DC File Offset: 0x000057DC
		public Terrain originTerrain { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000075E4 File Offset: 0x000057E4
		public RectInt pixelRect { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000075EC File Offset: 0x000057EC
		public int targetTextureWidth { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000075F4 File Offset: 0x000057F4
		public int targetTextureHeight { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000075FC File Offset: 0x000057FC
		public Vector2 pixelSize { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00007604 File Offset: 0x00005804
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000760C File Offset: 0x0000580C
		public RenderTexture sourceRenderTexture { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00007615 File Offset: 0x00005815
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000761D File Offset: 0x0000581D
		public RenderTexture destinationRenderTexture { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00007626 File Offset: 0x00005826
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000762E File Offset: 0x0000582E
		public RenderTexture oldRenderTexture { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00007638 File Offset: 0x00005838
		public int terrainCount
		{
			get
			{
				return this.m_TerrainTiles.Count;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007658 File Offset: 0x00005858
		public Terrain GetTerrain(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].terrain;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000767C File Offset: 0x0000587C
		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000076A0 File Offset: 0x000058A0
		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000076C3 File Offset: 0x000058C3
		public float heightWorldSpaceMin
		{
			get
			{
				return this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000076CB File Offset: 0x000058CB
		public float heightWorldSpaceSize
		{
			get
			{
				return this.m_HeightWorldSpaceMax - this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000076DA File Offset: 0x000058DA
		public static float kNormalizedHeightScale
		{
			get
			{
				return 0.4999771f;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002A3 RID: 675 RVA: 0x000076E4 File Offset: 0x000058E4
		// (remove) Token: 0x060002A4 RID: 676 RVA: 0x00007718 File Offset: 0x00005918
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint;

		// Token: 0x060002A5 RID: 677 RVA: 0x0000774C File Offset: 0x0000594C
		internal static int ClampContextResolution(int resolution)
		{
			return Mathf.Clamp(resolution, 1, 8192);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000776C File Offset: 0x0000596C
		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			this.originTerrain = terrain;
			this.pixelRect = pixelRect;
			this.targetTextureWidth = targetTextureWidth;
			this.targetTextureHeight = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			this.pixelSize = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (sharedBoundaryTexel ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (sharedBoundaryTexel ? 1f : 0f)));
			this.FindTerrainTilesUnlimited(sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000077FC File Offset: 0x000059FC
		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, sharedBoundaryTexel), inputTextureWidth, inputTextureHeight, sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007828 File Offset: 0x00005A28
		private void FindTerrainTilesUnlimited(bool sharedBoundaryTexel, bool fillOutsideTerrain)
		{
			float minX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)this.pixelRect.xMin;
			float minZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)this.pixelRect.yMin;
			float maxX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)(this.pixelRect.xMax - 1);
			float maxZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)(this.pixelRect.yMax - 1);
			this.m_HeightWorldSpaceMin = this.originTerrain.GetPosition().y;
			this.m_HeightWorldSpaceMax = this.m_HeightWorldSpaceMin + this.originTerrain.terrainData.size.y;
			Predicate<Terrain> predicate = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num4 = t.transform.position.x + t.terrainData.size.x;
				float num5 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num4 >= minX && z <= maxZ && num5 >= minZ;
			};
			TerrainMap terrainMap = TerrainMap.CreateFromConnectedNeighbors(this.originTerrain, predicate, false);
			this.m_TerrainTiles = new List<PaintContext.TerrainTile>();
			bool flag = terrainMap != null;
			if (flag)
			{
				foreach (KeyValuePair<TerrainTileCoord, Terrain> keyValuePair in terrainMap.terrainTiles)
				{
					TerrainTileCoord key = keyValuePair.Key;
					Terrain value = keyValuePair.Value;
					int num = key.tileX * (this.targetTextureWidth - (sharedBoundaryTexel ? 1 : 0));
					int num2 = key.tileZ * (this.targetTextureHeight - (sharedBoundaryTexel ? 1 : 0));
					RectInt rectInt = new RectInt(num, num2, this.targetTextureWidth, this.targetTextureHeight);
					bool flag2 = this.pixelRect.Overlaps(rectInt);
					if (flag2)
					{
						int num3 = (fillOutsideTerrain ? Mathf.Max(this.targetTextureWidth, this.targetTextureHeight) : 0);
						this.m_TerrainTiles.Add(PaintContext.TerrainTile.Make(value, num, num2, this.pixelRect, this.targetTextureWidth, this.targetTextureHeight, num3));
						this.m_HeightWorldSpaceMin = Mathf.Min(this.m_HeightWorldSpaceMin, value.GetPosition().y);
						this.m_HeightWorldSpaceMax = Mathf.Max(this.m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
					}
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			int num = PaintContext.ClampContextResolution(this.pixelRect.width);
			int num2 = PaintContext.ClampContextResolution(this.pixelRect.height);
			bool flag = num != this.pixelRect.width || num2 != this.pixelRect.height;
			if (flag)
			{
				Debug.LogWarning(string.Format("\nTERRAIN EDITOR INTERNAL ERROR: An attempt to create a PaintContext with dimensions of {0}x{1} was made,\nwhereas the maximum supported resolution is {2}. The size has been clamped to {3}.", new object[]
				{
					this.pixelRect.width,
					this.pixelRect.height,
					8192,
					8192
				}));
			}
			this.sourceRenderTexture = RenderTexture.GetTemporary(num, num2, 16, colorFormat, RenderTextureReadWrite.Linear);
			this.destinationRenderTexture = RenderTexture.GetTemporary(num, num2, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			this.sourceRenderTexture.filterMode = FilterMode.Point;
			this.oldRenderTexture = RenderTexture.active;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = this.oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(this.sourceRenderTexture);
			RenderTexture.ReleaseTemporary(this.destinationRenderTexture);
			this.sourceRenderTexture = null;
			this.destinationRenderTexture = null;
			this.oldRenderTexture = null;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007C40 File Offset: 0x00005E40
		private void GatherInternal(Func<PaintContext.ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = this.sourceRenderTexture;
			GL.Clear(true, true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, (float)this.pixelRect.width, 0f, (float)this.pixelRect.height);
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.gatherEnable;
				if (!flag2)
				{
					Texture texture = terrainToTexture(terrainTile);
					bool flag3 = texture == null || !terrainTile.gatherEnable;
					if (!flag3)
					{
						bool flag4 = texture.width != this.targetTextureWidth || texture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.gatherEnable;
							if (!flag5)
							{
								FilterMode filterMode = texture.filterMode;
								texture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", texture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuadPadded(terrainTile.clippedPCPixels, terrainTile.paddedPCPixels, terrainTile.clippedTerrainPixels, terrainTile.paddedTerrainPixels, texture);
								texture.filterMode = filterMode;
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			GL.PopMatrix();
			RenderTexture.active = this.oldRenderTexture;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private void ScatterInternal(Func<PaintContext.ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.scatterEnable;
				if (!flag2)
				{
					RenderTexture renderTexture = terrainToRT(terrainTile);
					bool flag3 = renderTexture == null || !terrainTile.scatterEnable;
					if (!flag3)
					{
						bool flag4 = renderTexture.width != this.targetTextureWidth || renderTexture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.scatterEnable;
							if (!flag5)
							{
								RenderTexture.active = renderTexture;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)renderTexture.width, 0f, (float)renderTexture.height);
								FilterMode filterMode = this.destinationRenderTexture.filterMode;
								this.destinationRenderTexture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, this.destinationRenderTexture);
								this.destinationRenderTexture.filterMode = filterMode;
								GL.PopMatrix();
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			RenderTexture.active = active;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007FA0 File Offset: 0x000061A0
		public void Gather(Func<PaintContext.ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainSource != null;
			if (flag)
			{
				this.GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007FCC File Offset: 0x000061CC
		public void Scatter(Func<PaintContext.ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainDest != null;
			if (flag)
			{
				this.ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007FF8 File Offset: 0x000061F8
		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.heightWorldSpaceMin) / this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.heightWorldSpaceSize);
			}, null);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000080A4 File Offset: 0x000062A4
		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.ScatterInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, "PaintContext.ScatterHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action(t, PaintContext.ToolAction.PaintHeightmap, editorUndoName);
				}
				blitMaterial.SetFloat("_Height_Offset", (this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}, delegate(PaintContext.ITerrainInfo t)
			{
				TerrainHeightmapSyncControl terrainHeightmapSyncControl = (t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightAndLod);
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, terrainHeightmapSyncControl);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			});
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000815C File Offset: 0x0000635C
		public void GatherHoles()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.holesTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles", null, 0, null, null);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000081B4 File Offset: 0x000063B4
		public void ScatterHoles(string editorUndoName)
		{
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action(t, PaintContext.ToolAction.PaintHoles, editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}, "PaintContext.ScatterHoles", null, 0, null, null);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000081EC File Offset: 0x000063EC
		public void GatherNormals()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.normalmapTexture, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals", null, 0, null, null);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008244 File Offset: 0x00006444
		private PaintContext.SplatmapUserData GetTerrainLayerUserData(PaintContext.ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			PaintContext.SplatmapUserData splatmapUserData = context.userData as PaintContext.SplatmapUserData;
			bool flag = splatmapUserData != null;
			if (flag)
			{
				bool flag2 = terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer;
				if (flag2)
				{
					return splatmapUserData;
				}
				splatmapUserData = null;
			}
			bool flag3 = splatmapUserData == null;
			if (flag3)
			{
				int num = -1;
				bool flag4 = terrainLayer != null;
				if (flag4)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					bool flag5 = num == -1 && addLayerIfDoesntExist;
					if (flag5)
					{
						Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
						if (action != null)
						{
							action(context, PaintContext.ToolAction.AddTerrainLayer, "Adding Terrain Layer");
						}
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					splatmapUserData = new PaintContext.SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = num & 3;
				}
				context.userData = splatmapUserData;
			}
			return splatmapUserData;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008338 File Offset: 0x00006538
		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			bool flag = inputLayer == null;
			if (!flag)
			{
				Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
				Vector4[] layerMasks = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 0f),
					new Vector4(0f, 1f, 0f, 0f),
					new Vector4(0f, 0f, 1f, 0f),
					new Vector4(0f, 0f, 0f, 1f)
				};
				this.GatherInternal(delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
					bool flag2 = terrainLayerUserData != null;
					Texture texture;
					if (flag2)
					{
						texture = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
					}
					else
					{
						texture = null;
					}
					return texture;
				}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData2 = this.GetTerrainLayerUserData(t, null, false);
					bool flag3 = terrainLayerUserData2 == null;
					if (!flag3)
					{
						copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData2.channelIndex]);
					}
				}, null);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008454 File Offset: 0x00006654
		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTexture tempTarget = RenderTexture.GetTemporary(new RenderTextureDescriptor(this.destinationRenderTexture.width, this.destinationRenderTexture.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None)
			{
				sRGB = false,
				useMipMap = false,
				autoGenerateMips = false
			});
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
					if (action != null)
					{
						action(t, PaintContext.ToolAction.PaintTexture, editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D texture2D = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = ((i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i);
							Texture2D texture2D2 = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D2.width != this.targetTextureWidth || texture2D2.height != this.targetTextureHeight;
							if (flag3)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)tempTarget.width, 0f, (float)tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", texture2D);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D2);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.destinationRenderTexture, t.clippedTerrainPixels, texture2D2);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial, 0, null, null);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008580 File Offset: 0x00006780
		private static void OnTerrainPainted(PaintContext.ITerrainInfo tile, PaintContext.ToolAction action)
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				bool flag = tile.terrain == PaintContext.s_PaintedTerrain[i].terrain;
				if (flag)
				{
					PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
					paintedTerrain.action |= action;
					PaintContext.s_PaintedTerrain[i] = paintedTerrain;
					return;
				}
			}
			PaintContext.s_PaintedTerrain.Add(new PaintContext.PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000861C File Offset: 0x0000681C
		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				bool flag = terrainData == null;
				if (!flag)
				{
					bool flag2 = (paintedTerrain.action & PaintContext.ToolAction.PaintHeightmap) > PaintContext.ToolAction.None;
					if (flag2)
					{
						terrainData.SyncHeightmap();
					}
					bool flag3 = (paintedTerrain.action & PaintContext.ToolAction.PaintHoles) > PaintContext.ToolAction.None;
					if (flag3)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					bool flag4 = (paintedTerrain.action & PaintContext.ToolAction.PaintTexture) > PaintContext.ToolAction.None;
					if (flag4)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			PaintContext.s_PaintedTerrain.Clear();
		}

		// Token: 0x040000F8 RID: 248
		private List<PaintContext.TerrainTile> m_TerrainTiles;

		// Token: 0x040000F9 RID: 249
		private float m_HeightWorldSpaceMin;

		// Token: 0x040000FA RID: 250
		private float m_HeightWorldSpaceMax;

		// Token: 0x040000FC RID: 252
		internal const int k_MinimumResolution = 1;

		// Token: 0x040000FD RID: 253
		internal const int k_MaximumResolution = 8192;

		// Token: 0x040000FE RID: 254
		private static List<PaintContext.PaintedTerrain> s_PaintedTerrain = new List<PaintContext.PaintedTerrain>();

		// Token: 0x0200003C RID: 60
		public interface ITerrainInfo
		{
			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060002BA RID: 698
			Terrain terrain { get; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060002BB RID: 699
			RectInt clippedTerrainPixels { get; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060002BC RID: 700
			RectInt clippedPCPixels { get; }

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060002BD RID: 701
			RectInt paddedTerrainPixels { get; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060002BE RID: 702
			RectInt paddedPCPixels { get; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060002BF RID: 703
			// (set) Token: 0x060002C0 RID: 704
			bool gatherEnable { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060002C1 RID: 705
			// (set) Token: 0x060002C2 RID: 706
			bool scatterEnable { get; set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060002C3 RID: 707
			// (set) Token: 0x060002C4 RID: 708
			object userData { get; set; }
		}

		// Token: 0x0200003D RID: 61
		private class TerrainTile : PaintContext.ITerrainInfo
		{
			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x000086F8 File Offset: 0x000068F8
			Terrain PaintContext.ITerrainInfo.terrain
			{
				get
				{
					return this.terrain;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x00008710 File Offset: 0x00006910
			RectInt PaintContext.ITerrainInfo.clippedTerrainPixels
			{
				get
				{
					return this.clippedTerrainPixels;
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008728 File Offset: 0x00006928
			RectInt PaintContext.ITerrainInfo.clippedPCPixels
			{
				get
				{
					return this.clippedPCPixels;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008740 File Offset: 0x00006940
			RectInt PaintContext.ITerrainInfo.paddedTerrainPixels
			{
				get
				{
					return this.paddedTerrainPixels;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008758 File Offset: 0x00006958
			RectInt PaintContext.ITerrainInfo.paddedPCPixels
			{
				get
				{
					return this.paddedPCPixels;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060002CA RID: 714 RVA: 0x00008770 File Offset: 0x00006970
			// (set) Token: 0x060002CB RID: 715 RVA: 0x00008788 File Offset: 0x00006988
			bool PaintContext.ITerrainInfo.gatherEnable
			{
				get
				{
					return this.gatherEnable;
				}
				set
				{
					this.gatherEnable = value;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002CC RID: 716 RVA: 0x00008794 File Offset: 0x00006994
			// (set) Token: 0x060002CD RID: 717 RVA: 0x000087AC File Offset: 0x000069AC
			bool PaintContext.ITerrainInfo.scatterEnable
			{
				get
				{
					return this.scatterEnable;
				}
				set
				{
					this.scatterEnable = value;
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002CE RID: 718 RVA: 0x000087B8 File Offset: 0x000069B8
			// (set) Token: 0x060002CF RID: 719 RVA: 0x000087D0 File Offset: 0x000069D0
			object PaintContext.ITerrainInfo.userData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x000087DC File Offset: 0x000069DC
			public static PaintContext.TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, int edgePad = 0)
			{
				PaintContext.TerrainTile terrainTile = new PaintContext.TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				int num = ((terrain.leftNeighbor == null) ? edgePad : 0);
				int num2 = ((terrain.rightNeighbor == null) ? edgePad : 0);
				int num3 = ((terrain.bottomNeighbor == null) ? edgePad : 0);
				int num4 = ((terrain.topNeighbor == null) ? edgePad : 0);
				terrainTile.paddedTerrainPixels = new RectInt
				{
					x = Mathf.Max(-num, pixelRect.x - tileOriginPixelsX - num),
					y = Mathf.Max(-num3, pixelRect.y - tileOriginPixelsY - num3),
					xMax = Mathf.Min(targetTextureWidth + num2, pixelRect.xMax - tileOriginPixelsX + num2),
					yMax = Mathf.Min(targetTextureHeight + num4, pixelRect.yMax - tileOriginPixelsY + num4)
				};
				terrainTile.paddedPCPixels = new RectInt(terrainTile.clippedPCPixels.min + (terrainTile.paddedTerrainPixels.min - terrainTile.clippedTerrainPixels.min), terrainTile.clippedPCPixels.size + (terrainTile.paddedTerrainPixels.size - terrainTile.clippedTerrainPixels.size));
				bool flag = terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0;
				if (flag)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}

			// Token: 0x040000FF RID: 255
			public Terrain terrain;

			// Token: 0x04000100 RID: 256
			public Vector2Int tileOriginPixels;

			// Token: 0x04000101 RID: 257
			public RectInt clippedTerrainPixels;

			// Token: 0x04000102 RID: 258
			public RectInt clippedPCPixels;

			// Token: 0x04000103 RID: 259
			public RectInt paddedTerrainPixels;

			// Token: 0x04000104 RID: 260
			public RectInt paddedPCPixels;

			// Token: 0x04000105 RID: 261
			public object userData;

			// Token: 0x04000106 RID: 262
			public bool gatherEnable;

			// Token: 0x04000107 RID: 263
			public bool scatterEnable;
		}

		// Token: 0x0200003E RID: 62
		private class SplatmapUserData
		{
			// Token: 0x04000108 RID: 264
			public TerrainLayer terrainLayer;

			// Token: 0x04000109 RID: 265
			public int terrainLayerIndex;

			// Token: 0x0400010A RID: 266
			public int mapIndex;

			// Token: 0x0400010B RID: 267
			public int channelIndex;
		}

		// Token: 0x0200003F RID: 63
		[Flags]
		internal enum ToolAction
		{
			// Token: 0x0400010D RID: 269
			None = 0,
			// Token: 0x0400010E RID: 270
			PaintHeightmap = 1,
			// Token: 0x0400010F RID: 271
			PaintTexture = 2,
			// Token: 0x04000110 RID: 272
			PaintHoles = 4,
			// Token: 0x04000111 RID: 273
			AddTerrainLayer = 8
		}

		// Token: 0x02000040 RID: 64
		private struct PaintedTerrain
		{
			// Token: 0x04000112 RID: 274
			public Terrain terrain;

			// Token: 0x04000113 RID: 275
			public PaintContext.ToolAction action;
		}
	}
}
