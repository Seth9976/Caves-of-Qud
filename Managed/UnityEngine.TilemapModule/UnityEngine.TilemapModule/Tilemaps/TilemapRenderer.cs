using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.U2D;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/Tilemap/TilemapRendererJobs.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h")]
	[RequireComponent(typeof(Tilemap))]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	public sealed class TilemapRenderer : Renderer
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003998 File Offset: 0x00001B98
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000039C0 File Offset: 0x00001BC0
		public Vector3Int chunkSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3Int vector3Int;
				TilemapRenderer.get_chunkSize_Injected(intPtr, out vector3Int);
				return vector3Int;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_chunkSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000039E4 File Offset: 0x00001BE4
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00003A0C File Offset: 0x00001C0C
		public Vector3 chunkCullingBounds
		{
			[FreeFunction("TilemapRendererBindings::GetChunkCullingBounds", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				TilemapRenderer.get_chunkCullingBounds_Injected(intPtr, out vector);
				return vector;
			}
			[FreeFunction("TilemapRendererBindings::SetChunkCullingBounds", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_chunkCullingBounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003A30 File Offset: 0x00001C30
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00003A54 File Offset: 0x00001C54
		public int maxChunkCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_maxChunkCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_maxChunkCount_Injected(intPtr, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003A78 File Offset: 0x00001C78
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003A9C File Offset: 0x00001C9C
		public int maxFrameAge
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_maxFrameAge_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_maxFrameAge_Injected(intPtr, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003AC0 File Offset: 0x00001CC0
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public TilemapRenderer.SortOrder sortOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_sortOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_sortOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003B08 File Offset: 0x00001D08
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00003B2C File Offset: 0x00001D2C
		[NativeProperty("RenderMode")]
		public TilemapRenderer.Mode mode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_mode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_mode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003B50 File Offset: 0x00001D50
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00003B74 File Offset: 0x00001D74
		public TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_detectChunkCullingBounds_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_detectChunkCullingBounds_Injected(intPtr, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003B98 File Offset: 0x00001D98
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003BBC File Offset: 0x00001DBC
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003BDF File Offset: 0x00001DDF
		[RequiredByNativeCode]
		internal void RegisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered += this.OnSpriteAtlasRegistered;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[RequiredByNativeCode]
		internal void UnregisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered -= this.OnSpriteAtlasRegistered;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003C0C File Offset: 0x00001E0C
		internal void OnSpriteAtlasRegistered(SpriteAtlas atlas)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TilemapRenderer.OnSpriteAtlasRegistered_Injected(intPtr, Object.MarshalledUnityObject.Marshal<SpriteAtlas>(atlas));
		}

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_chunkSize_Injected(IntPtr _unity_self, out Vector3Int ret);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_chunkSize_Injected(IntPtr _unity_self, [In] ref Vector3Int value);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_chunkCullingBounds_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_chunkCullingBounds_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_maxChunkCount_Injected(IntPtr _unity_self);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxChunkCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_maxFrameAge_Injected(IntPtr _unity_self);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxFrameAge_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TilemapRenderer.SortOrder get_sortOrder_Injected(IntPtr _unity_self);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortOrder_Injected(IntPtr _unity_self, TilemapRenderer.SortOrder value);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TilemapRenderer.Mode get_mode_Injected(IntPtr _unity_self);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mode_Injected(IntPtr _unity_self, TilemapRenderer.Mode value);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TilemapRenderer.DetectChunkCullingBounds get_detectChunkCullingBounds_Injected(IntPtr _unity_self);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detectChunkCullingBounds_Injected(IntPtr _unity_self, TilemapRenderer.DetectChunkCullingBounds value);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnSpriteAtlasRegistered_Injected(IntPtr _unity_self, IntPtr atlas);

		// Token: 0x02000012 RID: 18
		public enum SortOrder
		{
			// Token: 0x04000044 RID: 68
			BottomLeft,
			// Token: 0x04000045 RID: 69
			BottomRight,
			// Token: 0x04000046 RID: 70
			TopLeft,
			// Token: 0x04000047 RID: 71
			TopRight
		}

		// Token: 0x02000013 RID: 19
		public enum Mode
		{
			// Token: 0x04000049 RID: 73
			Chunk,
			// Token: 0x0400004A RID: 74
			Individual,
			// Token: 0x0400004B RID: 75
			SRPBatch
		}

		// Token: 0x02000014 RID: 20
		public enum DetectChunkCullingBounds
		{
			// Token: 0x0400004D RID: 77
			Auto,
			// Token: 0x0400004E RID: 78
			Manual
		}
	}
}
