using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000320 RID: 800
	internal class DynamicAtlasPage : IDisposable
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00058FDF File Offset: 0x000571DF
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x00058FE7 File Offset: 0x000571E7
		public TextureId textureId { get; private set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00058FF0 File Offset: 0x000571F0
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x00058FF8 File Offset: 0x000571F8
		public RenderTexture atlas { get; private set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00059001 File Offset: 0x00057201
		public RenderTextureFormat format { get; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x00059009 File Offset: 0x00057209
		public FilterMode filterMode { get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00059011 File Offset: 0x00057211
		public Vector2Int minSize { get; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00059019 File Offset: 0x00057219
		public Vector2Int maxSize { get; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00059021 File Offset: 0x00057221
		public Vector2Int currentSize
		{
			get
			{
				return this.m_CurrentSize;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00059029 File Offset: 0x00057229
		internal Allocator2D allocator
		{
			get
			{
				return this.m_Allocator;
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00059034 File Offset: 0x00057234
		public DynamicAtlasPage(RenderTextureFormat format, FilterMode filterMode, Vector2Int minSize, Vector2Int maxSize)
		{
			this.textureId = TextureRegistry.instance.AllocAndAcquireDynamic();
			this.format = format;
			this.filterMode = filterMode;
			this.minSize = minSize;
			this.maxSize = maxSize;
			this.m_Allocator = new Allocator2D(minSize, maxSize, this.m_2Padding);
			this.m_Blitter = new TextureBlitter(64);
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x000590A6 File Offset: 0x000572A6
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x000590AE File Offset: 0x000572AE
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001712 RID: 5906 RVA: 0x000590B7 File Offset: 0x000572B7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000590CC File Offset: 0x000572CC
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.atlas != null;
					if (flag)
					{
						UIRUtility.Destroy(this.atlas);
						this.atlas = null;
					}
					bool flag2 = this.m_Allocator != null;
					if (flag2)
					{
						this.m_Allocator = null;
					}
					bool flag3 = this.m_Blitter != null;
					if (flag3)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag4 = this.textureId != TextureId.invalid;
					if (flag4)
					{
						TextureRegistry.instance.Release(this.textureId);
						this.textureId = TextureId.invalid;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00059194 File Offset: 0x00057394
		public bool TryAdd(Texture2D image, out Allocator2D.Alloc2D alloc, out RectInt rect)
		{
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Allocator2D.Alloc2D);
				rect = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = !this.m_Allocator.TryAllocate(image.width + this.m_2Padding, image.height + this.m_2Padding, out alloc);
				if (flag2)
				{
					rect = default(RectInt);
					flag = false;
				}
				else
				{
					this.m_CurrentSize.x = Mathf.Max(this.m_CurrentSize.x, UIRUtility.GetNextPow2(alloc.rect.xMax));
					this.m_CurrentSize.y = Mathf.Max(this.m_CurrentSize.y, UIRUtility.GetNextPow2(alloc.rect.yMax));
					rect = new RectInt(alloc.rect.xMin + this.m_1Padding, alloc.rect.yMin + this.m_1Padding, image.width, image.height);
					this.Update(image, rect);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000592AC File Offset: 0x000574AC
		public void Update(Texture2D image, RectInt rect)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(image != null && rect.width > 0 && rect.height > 0);
				this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rect.x, rect.y), true, Color.white);
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00059330 File Offset: 0x00057530
		public void Remove(Allocator2D.Alloc2D alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(alloc.rect.width > 0 && alloc.rect.height > 0);
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00059388 File Offset: 0x00057588
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.UpdateAtlasTexture();
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000593C4 File Offset: 0x000575C4
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag2 = this.atlas.width != this.m_CurrentSize.x || this.atlas.height != this.m_CurrentSize.y;
				if (flag2)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag3 = renderTexture == null;
					if (flag3)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_CurrentSize.x,
							this.m_CurrentSize.y
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

		// Token: 0x06001719 RID: 5913 RVA: 0x000594F8 File Offset: 0x000576F8
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_CurrentSize.x == 0 || this.m_CurrentSize.y == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_CurrentSize.x, this.m_CurrentSize.y, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas Page " + DynamicAtlasPage.s_TextureCounter++.ToString(),
					filterMode = this.filterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x04000C9D RID: 3229
		private readonly int m_1Padding = 1;

		// Token: 0x04000C9E RID: 3230
		private readonly int m_2Padding = 2;

		// Token: 0x04000C9F RID: 3231
		private Allocator2D m_Allocator;

		// Token: 0x04000CA0 RID: 3232
		private TextureBlitter m_Blitter;

		// Token: 0x04000CA1 RID: 3233
		private Vector2Int m_CurrentSize;

		// Token: 0x04000CA2 RID: 3234
		private static int s_TextureCounter;
	}
}
