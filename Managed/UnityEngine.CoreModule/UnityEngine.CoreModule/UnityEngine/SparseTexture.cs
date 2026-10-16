using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001F8 RID: 504
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		public int tileWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SparseTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SparseTexture.get_tileWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0002D114 File Offset: 0x0002B314
		public int tileHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SparseTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SparseTexture.get_tileHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0002D138 File Offset: 0x0002B338
		public bool isCreated
		{
			[NativeName("IsInitialized")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SparseTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SparseTexture.get_isCreated_Injected(intPtr);
			}
		}

		// Token: 0x060019B9 RID: 6585
		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, GraphicsFormat format, TextureColorSpace colorSpace, int mipCount);

		// Token: 0x060019BA RID: 6586 RVA: 0x0002D15C File Offset: 0x0002B35C
		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		public unsafe void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SparseTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(data);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				SparseTexture.UpdateTile_Injected(intPtr, tileX, tileY, miplevel, ref managedSpanWrapper);
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		public unsafe void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SparseTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<byte> span = new Span<byte>(data);
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				SparseTexture.UpdateTileRaw_Injected(intPtr, tileX, tileY, miplevel, ref managedSpanWrapper);
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			this.UpdateTileRaw(tileX, tileY, miplevel, null);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0002D204 File Offset: 0x0002B404
		internal bool ValidateFormat(TextureFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0002D270 File Offset: 0x0002B470
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, GraphicsFormatUsage.Sparse);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
		internal bool ValidateSize(int width, int height, GraphicsFormat format)
		{
			bool flag = (ulong)GraphicsFormatUtility.GetBlockSize(format) * (ulong)((long)width / (long)((ulong)GraphicsFormatUtility.GetBlockWidth(format))) * (ulong)((long)height / (long)((ulong)GraphicsFormatUtility.GetBlockHeight(format))) < 65536UL;
			bool flag2;
			if (flag)
			{
				Debug.LogError("SparseTexture creation failed. The minimum size in bytes of a SparseTexture is 64KB.", this);
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0002D324 File Offset: 0x0002B524
		private static void ValidateIsNotCrunched(TextureFormat textureFormat)
		{
			bool flag = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
			if (flag)
			{
				throw new ArgumentException("Crunched SparseTexture is not supported.");
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0002D347 File Offset: 0x0002B547
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, DefaultFormat format, int mipCount)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), mipCount)
		{
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0002D35C File Offset: 0x0002B55C
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width, height);
			if (!flag)
			{
				bool flag2 = !this.ValidateSize(width, height, format);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, format, base.GetTextureColorSpace(format), mipCount);
				}
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount)
			: this(width, height, textureFormat, mipCount, false)
		{
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0002D3B8 File Offset: 0x0002B5B8
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount, [DefaultValue("false")] bool linear)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				SparseTexture.ValidateIsNotCrunched(textureFormat);
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				bool flag2 = !SystemInfo.IsFormatSupported(graphicsFormat, GraphicsFormatUsage.Sparse);
				if (flag2)
				{
					Debug.LogError(string.Format("Creation of a SparseTexture with '{0}' is not supported on this platform.", textureFormat));
				}
				else
				{
					bool flag3 = !this.ValidateSize(width, height, graphicsFormat);
					if (!flag3)
					{
						SparseTexture.Internal_Create(this, width, height, graphicsFormat, base.GetTextureColorSpace(linear), mipCount);
					}
				}
			}
		}

		// Token: 0x060019C5 RID: 6597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_tileWidth_Injected(IntPtr _unity_self);

		// Token: 0x060019C6 RID: 6598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_tileHeight_Injected(IntPtr _unity_self);

		// Token: 0x060019C7 RID: 6599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isCreated_Injected(IntPtr _unity_self);

		// Token: 0x060019C8 RID: 6600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateTile_Injected(IntPtr _unity_self, int tileX, int tileY, int miplevel, ref ManagedSpanWrapper data);

		// Token: 0x060019C9 RID: 6601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateTileRaw_Injected(IntPtr _unity_self, int tileX, int tileY, int miplevel, ref ManagedSpanWrapper data);
	}
}
