using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200031F RID: 799
	internal class DynamicAtlasCore : IDisposable
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00058908 File Offset: 0x00056B08
		public int maxImageSize { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00058910 File Offset: 0x00056B10
		public RenderTextureFormat format { get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00058918 File Offset: 0x00056B18
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00058920 File Offset: 0x00056B20
		public RenderTexture atlas { get; private set; }

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005892C File Offset: 0x00056B2C
		public DynamicAtlasCore(RenderTextureFormat format = RenderTextureFormat.ARGB32, FilterMode filterMode = FilterMode.Bilinear, int maxImageSize = 64, int initialSize = 64, int maxAtlasSize = 4096)
		{
			Debug.Assert(filterMode == FilterMode.Bilinear || filterMode == FilterMode.Point);
			Debug.Assert(maxAtlasSize <= SystemInfo.maxRenderTextureSize);
			Debug.Assert(initialSize <= maxAtlasSize);
			Debug.Assert(Mathf.IsPowerOfTwo(maxImageSize));
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxAtlasSize));
			this.m_MaxAtlasSize = maxAtlasSize;
			this.format = format;
			this.maxImageSize = maxImageSize;
			this.m_FilterMode = filterMode;
			this.m_UVs = new Dictionary<Texture2D, RectInt>(64);
			this.m_Blitter = new TextureBlitter(64);
			this.m_InitialSize = initialSize;
			this.m_2SidePadding = ((filterMode == FilterMode.Point) ? 0 : 2);
			this.m_1SidePadding = ((filterMode == FilterMode.Point) ? 0 : 1);
			this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxAtlasSize, this.m_1SidePadding);
			this.m_ColorSpace = QualitySettings.activeColorSpace;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00058A1A File Offset: 0x00056C1A
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00058A22 File Offset: 0x00056C22
		private protected bool disposed { protected get; private set; }

		// Token: 0x060016F9 RID: 5881 RVA: 0x00058A2B File Offset: 0x00056C2B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00058A40 File Offset: 0x00056C40
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.atlas);
					this.atlas = null;
					bool flag = this.m_Allocator != null;
					if (flag)
					{
						this.m_Allocator.Dispose();
						this.m_Allocator = null;
					}
					bool flag2 = this.m_Blitter != null;
					if (flag2)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00058AC3 File Offset: 0x00056CC3
		private static void LogDisposeError()
		{
			Debug.LogError("An attempt to use a disposed atlas manager has been detected.");
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00058AD4 File Offset: 0x00056CD4
		public bool IsReleased()
		{
			return this.atlas != null && !this.atlas.IsCreated();
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00058B08 File Offset: 0x00056D08
		public bool TryGetRect(Texture2D image, out RectInt uvs, Func<Texture2D, bool> filter = null)
		{
			uvs = default(RectInt);
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
				flag = false;
			}
			else
			{
				bool flag2 = image == null;
				if (flag2)
				{
					flag = false;
				}
				else
				{
					bool flag3 = this.m_UVs.TryGetValue(image, out uvs);
					if (flag3)
					{
						flag = true;
					}
					else
					{
						bool flag4 = filter != null && !filter(image);
						if (flag4)
						{
							flag = false;
						}
						else
						{
							bool flag5 = !this.AllocateRect(image.width, image.height, out uvs);
							if (flag5)
							{
								flag = false;
							}
							else
							{
								this.m_UVs[image] = uvs;
								this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(uvs.x, uvs.y), true, Color.white);
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00058BE8 File Offset: 0x00056DE8
		public void UpdateTexture(Texture2D image)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				RectInt rectInt;
				bool flag = !this.m_UVs.TryGetValue(image, out rectInt);
				if (!flag)
				{
					this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rectInt.x, rectInt.y), true, Color.white);
				}
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00058C5C File Offset: 0x00056E5C
		public bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool flag = !this.m_Allocator.TryAllocate(width + this.m_2SidePadding, height + this.m_2SidePadding, out uvs);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				uvs = new RectInt(uvs.x + this.m_1SidePadding, uvs.y + this.m_1SidePadding, width, height);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00058CBD File Offset: 0x00056EBD
		public void EnqueueBlit(Texture image, RectInt srcRect, int x, int y, bool addBorder, Color tint)
		{
			this.m_Blitter.QueueBlit(image, srcRect, new Vector2Int(x, y), addBorder, tint);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00058CDC File Offset: 0x00056EDC
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DynamicAtlasCore.LogDisposeError();
			}
			else
			{
				this.UpdateAtlasTexture();
				bool forceReblitAll = this.m_ForceReblitAll;
				if (forceReblitAll)
				{
					this.m_ForceReblitAll = false;
					this.m_Blitter.Reset();
					foreach (KeyValuePair<Texture2D, RectInt> keyValuePair in this.m_UVs)
					{
						this.m_Blitter.QueueBlit(keyValuePair.Key, new RectInt(0, 0, keyValuePair.Key.width, keyValuePair.Key.height), new Vector2Int(keyValuePair.Value.x, keyValuePair.Value.y), true, Color.white);
					}
				}
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00058DDC File Offset: 0x00056FDC
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				bool flag2 = this.m_UVs.Count > this.m_Blitter.queueLength;
				if (flag2)
				{
					this.m_ForceReblitAll = true;
				}
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag3 = this.atlas.width != this.m_Allocator.physicalWidth || this.atlas.height != this.m_Allocator.physicalHeight;
				if (flag3)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag4 = renderTexture == null;
					if (flag4)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_Allocator.physicalWidth,
							this.m_Allocator.physicalHeight
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00058F38 File Offset: 0x00057138
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_Allocator.physicalWidth == 0 || this.m_Allocator.physicalHeight == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas " + DynamicAtlasCore.s_TextureCounter++.ToString(),
					filterMode = this.m_FilterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x04000C87 RID: 3207
		private int m_InitialSize;

		// Token: 0x04000C88 RID: 3208
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x04000C89 RID: 3209
		private Dictionary<Texture2D, RectInt> m_UVs;

		// Token: 0x04000C8A RID: 3210
		private bool m_ForceReblitAll;

		// Token: 0x04000C8B RID: 3211
		private FilterMode m_FilterMode;

		// Token: 0x04000C8C RID: 3212
		private ColorSpace m_ColorSpace;

		// Token: 0x04000C8D RID: 3213
		private TextureBlitter m_Blitter;

		// Token: 0x04000C8E RID: 3214
		private int m_2SidePadding;

		// Token: 0x04000C8F RID: 3215
		private int m_1SidePadding;

		// Token: 0x04000C90 RID: 3216
		private int m_MaxAtlasSize;

		// Token: 0x04000C91 RID: 3217
		private static ProfilerMarker s_MarkerReset = new ProfilerMarker("UIR.AtlasManager.Reset");

		// Token: 0x04000C94 RID: 3220
		private static int s_TextureCounter;
	}
}
