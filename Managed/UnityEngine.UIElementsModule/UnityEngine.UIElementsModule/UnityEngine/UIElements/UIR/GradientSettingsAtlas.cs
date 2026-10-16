using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000640 RID: 1600
	internal class GradientSettingsAtlas : IDisposable
	{
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000AB734 File Offset: 0x000A9934
		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000AB74C File Offset: 0x000A994C
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000AB754 File Offset: 0x000A9954
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F00 RID: 12032 RVA: 0x000AB75D File Offset: 0x000A995D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000AB770 File Offset: 0x000A9970
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000AB7A7 File Offset: 0x000A99A7
		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000AB7C8 File Offset: 0x000A99C8
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000AB81C File Offset: 0x000A9A1C
		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000AB834 File Offset: 0x000A9A34
		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc alloc;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Alloc);
			}
			else
			{
				Alloc alloc2 = this.m_Allocator.Allocate((uint)count);
				alloc = alloc2;
			}
			return alloc;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000AB87C File Offset: 0x000A9A7C
		public void Remove(Alloc alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000AB8AC File Offset: 0x000A9AAC
		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int num = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < num; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				int num2 = (int)alloc.start;
				int j = 0;
				int num3 = settings.Length;
				while (j < num3)
				{
					int num4 = 0;
					GradientSettings gradientSettings = settings[j];
					Debug.Assert(remap == null || num2 == remap.destIndex);
					bool flag2 = gradientSettings.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 vector = gradientSettings.radialFocus;
						vector += Vector2.one;
						vector /= 2f;
						vector.y = 1f - vector.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)gradientSettings.addressMode / 255f, vector.x, vector.y, num4++, num2);
					}
					else
					{
						bool flag3 = gradientSettings.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num4++, num2);
						}
					}
					Vector2Int vector2Int = new Vector2Int(gradientSettings.location.x, gradientSettings.location.y);
					Vector2 vector2 = new Vector2((float)(gradientSettings.location.width - 1), (float)(gradientSettings.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						vector2Int = new Vector2Int(remap.location.x, remap.location.y);
						vector2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(vector2Int.x, vector2Int.y, num4++, num2);
					this.m_RawAtlas.WriteRawInt2Packed((int)vector2.x, (int)vector2.y, num4++, num2);
					remap = ((remap != null) ? remap.next : null);
					num2++;
					j++;
				}
				this.MustCommit = true;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000ABB6D File Offset: 0x000A9D6D
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000ABB75 File Offset: 0x000A9D75
		public bool MustCommit { get; private set; }

		// Token: 0x06002F0A RID: 12042 RVA: 0x000ABB80 File Offset: 0x000A9D80
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					this.MustCommit = false;
				}
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000ABBE4 File Offset: 0x000A9DE4
		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + GradientSettingsAtlas.s_TextureCounter++.ToString(),
					filterMode = FilterMode.Point
				};
			}
		}

		// Token: 0x0400161A RID: 5658
		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		// Token: 0x0400161B RID: 5659
		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		// Token: 0x0400161C RID: 5660
		private readonly int m_Length;

		// Token: 0x0400161D RID: 5661
		private readonly int m_ElemWidth;

		// Token: 0x0400161E RID: 5662
		private BestFitAllocator m_Allocator;

		// Token: 0x0400161F RID: 5663
		private Texture2D m_Atlas;

		// Token: 0x04001620 RID: 5664
		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		// Token: 0x04001621 RID: 5665
		private static int s_TextureCounter;

		// Token: 0x02000641 RID: 1601
		private struct RawTexture
		{
			// Token: 0x06002F0D RID: 12045 RVA: 0x000ABC78 File Offset: 0x000A9E78
			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte b = (byte)(v0 / 255);
				byte b2 = (byte)(v0 - (int)(b * byte.MaxValue));
				byte b3 = (byte)(v1 / 255);
				byte b4 = (byte)(v1 - (int)(b3 * byte.MaxValue));
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x06002F0E RID: 12046 RVA: 0x000ABCD4 File Offset: 0x000A9ED4
			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte b = (byte)(f0 * 255f + 0.5f);
				byte b2 = (byte)(f1 * 255f + 0.5f);
				byte b3 = (byte)(f2 * 255f + 0.5f);
				byte b4 = (byte)(f3 * 255f + 0.5f);
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x04001624 RID: 5668
			public Color32[] rgba;

			// Token: 0x04001625 RID: 5669
			public int width;

			// Token: 0x04001626 RID: 5670
			public int height;
		}
	}
}
