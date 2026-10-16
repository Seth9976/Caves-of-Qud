using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200067B RID: 1659
	internal class TextureBlitter : IDisposable
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000B6FF5 File Offset: 0x000B51F5
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000B6FFD File Offset: 0x000B51FD
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003058 RID: 12376 RVA: 0x000B7006 File Offset: 0x000B5206
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000B7018 File Offset: 0x000B5218
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_BlitMaterial);
					this.m_BlitMaterial = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000B7058 File Offset: 0x000B5258
		static TextureBlitter()
		{
			for (int i = 0; i < 8; i++)
			{
				TextureBlitter.k_TextureIds[i] = Shader.PropertyToID("_MainTex" + i.ToString());
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000B70AD File Offset: 0x000B52AD
		public TextureBlitter(int capacity = 512)
		{
			this.m_PendingBlits = new List<TextureBlitter.BlitInfo>(capacity);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000B70D0 File Offset: 0x000B52D0
		public void QueueBlit(Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_PendingBlits.Add(new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				});
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000B7138 File Offset: 0x000B5338
		public void BlitOneNow(RenderTexture dst, Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_SingleBlit[0] = new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				};
				this.BeginBlit(dst);
				this.DoBlit(this.m_SingleBlit, 0);
				this.EndBlit();
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000B71BD File Offset: 0x000B53BD
		public int queueLength
		{
			get
			{
				return this.m_PendingBlits.Count;
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000B71CC File Offset: 0x000B53CC
		public void Commit(RenderTexture dst)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_PendingBlits.Count == 0;
				if (!flag)
				{
					this.BeginBlit(dst);
					for (int i = 0; i < this.m_PendingBlits.Count; i += 8)
					{
						this.DoBlit(this.m_PendingBlits, i);
					}
					this.EndBlit();
					this.m_PendingBlits.Clear();
				}
			}
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000B7245 File Offset: 0x000B5445
		public void Reset()
		{
			this.m_PendingBlits.Clear();
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000B7254 File Offset: 0x000B5454
		private void BeginBlit(RenderTexture dst)
		{
			bool flag = this.m_BlitMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find(Shaders.k_AtlasBlit);
				this.m_BlitMaterial = new Material(shader);
				this.m_BlitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			}
			bool flag2 = this.m_Properties == null;
			if (flag2)
			{
				this.m_Properties = new MaterialPropertyBlock();
			}
			this.m_Viewport = Utility.GetActiveViewport();
			this.m_PrevRT = RenderTexture.active;
			GL.LoadPixelMatrix(0f, (float)dst.width, 0f, (float)dst.height);
			Graphics.SetRenderTarget(dst);
			this.m_BlitMaterial.SetPass(0);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000B7300 File Offset: 0x000B5500
		private void DoBlit(IList<TextureBlitter.BlitInfo> blitInfos, int startIndex)
		{
			int num = Mathf.Min(blitInfos.Count - startIndex, 8);
			int num2 = startIndex + num;
			int i = startIndex;
			int num3 = 0;
			while (i < num2)
			{
				Texture src = blitInfos[i].src;
				bool flag = src != null;
				if (flag)
				{
					this.m_Properties.SetTexture(TextureBlitter.k_TextureIds[num3], src);
				}
				i++;
				num3++;
			}
			Utility.SetPropertyBlock(this.m_Properties);
			GL.Begin(7);
			int j = startIndex;
			int num4 = 0;
			while (j < num2)
			{
				TextureBlitter.BlitInfo blitInfo = blitInfos[j];
				float num5 = 1f / (float)blitInfo.src.width;
				float num6 = 1f / (float)blitInfo.src.height;
				float num7 = (float)(blitInfo.dstPos.x - blitInfo.border);
				float num8 = (float)(blitInfo.dstPos.y - blitInfo.border);
				float num9 = (float)(blitInfo.dstPos.x + blitInfo.srcRect.width + blitInfo.border);
				float num10 = (float)(blitInfo.dstPos.y + blitInfo.srcRect.height + blitInfo.border);
				float num11 = (float)(blitInfo.srcRect.x - blitInfo.border) * num5;
				float num12 = (float)(blitInfo.srcRect.y - blitInfo.border) * num6;
				float num13 = (float)(blitInfo.srcRect.xMax + blitInfo.border) * num5;
				float num14 = (float)(blitInfo.srcRect.yMax + blitInfo.border) * num6;
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num11, num12, (float)num4);
				GL.Vertex3(num7, num8, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num11, num14, (float)num4);
				GL.Vertex3(num7, num10, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num13, num14, (float)num4);
				GL.Vertex3(num9, num10, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num13, num12, (float)num4);
				GL.Vertex3(num9, num8, 0f);
				j++;
				num4++;
			}
			GL.End();
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000B7564 File Offset: 0x000B5764
		private void EndBlit()
		{
			Graphics.SetRenderTarget(this.m_PrevRT);
			GL.Viewport(new Rect((float)this.m_Viewport.x, (float)this.m_Viewport.y, (float)this.m_Viewport.width, (float)this.m_Viewport.height));
		}

		// Token: 0x040017AF RID: 6063
		private const int k_TextureSlotCount = 8;

		// Token: 0x040017B0 RID: 6064
		private static readonly int[] k_TextureIds = new int[8];

		// Token: 0x040017B1 RID: 6065
		private static ProfilerMarker s_CommitSampler = new ProfilerMarker("UIR.TextureBlitter.Commit");

		// Token: 0x040017B2 RID: 6066
		private TextureBlitter.BlitInfo[] m_SingleBlit = new TextureBlitter.BlitInfo[1];

		// Token: 0x040017B3 RID: 6067
		private Material m_BlitMaterial;

		// Token: 0x040017B4 RID: 6068
		private MaterialPropertyBlock m_Properties;

		// Token: 0x040017B5 RID: 6069
		private RectInt m_Viewport;

		// Token: 0x040017B6 RID: 6070
		private RenderTexture m_PrevRT;

		// Token: 0x040017B7 RID: 6071
		private List<TextureBlitter.BlitInfo> m_PendingBlits;

		// Token: 0x0200067C RID: 1660
		private struct BlitInfo
		{
			// Token: 0x040017B9 RID: 6073
			public Texture src;

			// Token: 0x040017BA RID: 6074
			public RectInt srcRect;

			// Token: 0x040017BB RID: 6075
			public Vector2Int dstPos;

			// Token: 0x040017BC RID: 6076
			public int border;

			// Token: 0x040017BD RID: 6077
			public Color tint;
		}
	}
}
