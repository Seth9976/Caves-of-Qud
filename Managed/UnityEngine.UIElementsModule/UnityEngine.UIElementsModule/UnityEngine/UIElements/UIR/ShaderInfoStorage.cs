using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200066E RID: 1646
	internal class ShaderInfoStorage<T> : BaseShaderInfoStorage where T : struct
	{
		// Token: 0x06003027 RID: 12327 RVA: 0x000B610C File Offset: 0x000B430C
		public ShaderInfoStorage(TextureFormat format, Func<Color, T> convert, int initialSize = 64, int maxSize = 4096)
		{
			Debug.Assert(maxSize <= SystemInfo.maxTextureSize);
			Debug.Assert(initialSize <= maxSize);
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxSize));
			Debug.Assert(convert != null);
			this.m_InitialSize = initialSize;
			this.m_MaxSize = maxSize;
			this.m_Format = format;
			this.m_Convert = convert;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000B6184 File Offset: 0x000B4384
		protected override void Dispose(bool disposing)
		{
			bool flag = !base.disposed && disposing;
			if (flag)
			{
				UIRUtility.Destroy(this.m_Texture);
				this.m_Texture = null;
				this.m_Texels = default(NativeArray<T>);
				UIRAtlasAllocator allocator = this.m_Allocator;
				if (allocator != null)
				{
					allocator.Dispose();
				}
				this.m_Allocator = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x000B61E3 File Offset: 0x000B43E3
		public override Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000B61EC File Offset: 0x000B43EC
		public override bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool disposed = base.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				uvs = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = this.m_Allocator == null;
				if (flag2)
				{
					this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxSize, 0);
				}
				bool flag3 = !this.m_Allocator.TryAllocate(width, height, out uvs);
				if (flag3)
				{
					flag = false;
				}
				else
				{
					uvs = new RectInt(uvs.x, uvs.y, width, height);
					this.CreateOrExpandTexture();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000B627C File Offset: 0x000B447C
		public override void SetTexel(int x, int y, Color color)
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.m_Texels.IsCreated;
				if (flag)
				{
					this.m_Texels = this.m_Texture.GetRawTextureData<T>();
				}
				this.m_Texels[x + y * this.m_Texture.width] = this.m_Convert(color);
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000B62EC File Offset: 0x000B44EC
		public override void UpdateTexture()
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_Texture == null || !this.m_Texels.IsCreated;
				if (!flag)
				{
					this.m_Texture.Apply(false, false);
					this.m_Texels = default(NativeArray<T>);
				}
			}
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000B6350 File Offset: 0x000B4550
		private void CreateOrExpandTexture()
		{
			int physicalWidth = this.m_Allocator.physicalWidth;
			int physicalHeight = this.m_Allocator.physicalHeight;
			bool flag = false;
			bool flag2 = this.m_Texture != null;
			if (flag2)
			{
				bool flag3 = this.m_Texture.width == physicalWidth && this.m_Texture.height == physicalHeight;
				if (flag3)
				{
					return;
				}
				flag = true;
			}
			Texture2D texture2D = new Texture2D(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, this.m_Format, false)
			{
				name = "UIR Shader Info " + BaseShaderInfoStorage.s_TextureCounter++.ToString(),
				hideFlags = HideFlags.HideAndDontSave,
				filterMode = FilterMode.Point
			};
			bool flag4 = flag;
			if (flag4)
			{
				NativeArray<T> nativeArray = (this.m_Texels.IsCreated ? this.m_Texels : this.m_Texture.GetRawTextureData<T>());
				NativeArray<T> rawTextureData = texture2D.GetRawTextureData<T>();
				ShaderInfoStorage<T>.CpuBlit(nativeArray, this.m_Texture.width, this.m_Texture.height, rawTextureData, texture2D.width, texture2D.height);
				this.m_Texels = rawTextureData;
			}
			else
			{
				this.m_Texels = default(NativeArray<T>);
			}
			UIRUtility.Destroy(this.m_Texture);
			this.m_Texture = texture2D;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000B649C File Offset: 0x000B469C
		private static void CpuBlit(NativeArray<T> src, int srcWidth, int srcHeight, NativeArray<T> dst, int dstWidth, int dstHeight)
		{
			Debug.Assert(dstWidth >= srcWidth && dstHeight >= srcHeight);
			int num = dstWidth - srcWidth;
			int num2 = dstHeight - srcHeight;
			int num3 = srcWidth * srcHeight;
			int i = 0;
			int num4 = 0;
			int num5 = srcWidth;
			while (i < num3)
			{
				while (i < num5)
				{
					dst[num4] = src[i];
					num4++;
					i++;
				}
				num5 += srcWidth;
				num4 += num;
			}
		}

		// Token: 0x04001782 RID: 6018
		private readonly int m_InitialSize;

		// Token: 0x04001783 RID: 6019
		private readonly int m_MaxSize;

		// Token: 0x04001784 RID: 6020
		private readonly TextureFormat m_Format;

		// Token: 0x04001785 RID: 6021
		private readonly Func<Color, T> m_Convert;

		// Token: 0x04001786 RID: 6022
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x04001787 RID: 6023
		private Texture2D m_Texture;

		// Token: 0x04001788 RID: 6024
		private NativeArray<T> m_Texels;
	}
}
