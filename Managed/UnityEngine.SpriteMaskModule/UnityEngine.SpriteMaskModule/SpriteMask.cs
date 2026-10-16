using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeType(Header = "Modules/SpriteMask/Public/SpriteMask.h")]
	[RejectDragAndDropMaterial]
	public sealed class SpriteMask : Renderer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		public int frontSortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_frontSortingLayerID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_frontSortingLayerID_Injected(intPtr, value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020BC File Offset: 0x000002BC
		public int frontSortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_frontSortingOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_frontSortingOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002104 File Offset: 0x00000304
		public int backSortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_backSortingLayerID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_backSortingLayerID_Injected(intPtr, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000214C File Offset: 0x0000034C
		public int backSortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_backSortingOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_backSortingOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00000394
		public float alphaCutoff
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_alphaCutoff_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_alphaCutoff_Injected(intPtr, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
		public Sprite sprite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Sprite>(SpriteMask.get_sprite_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_sprite_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Sprite>(value));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000222C File Offset: 0x0000042C
		public bool isCustomRangeActive
		{
			[NativeMethod("IsCustomRangeActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_isCustomRangeActive_Injected(intPtr);
			}
			[NativeMethod("SetCustomRangeActive")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_isCustomRangeActive_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
		public SpriteSortPoint spriteSortPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_spriteSortPoint_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_spriteSortPoint_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		public SpriteMask.MaskSource maskSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteMask.get_maskSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteMask.set_maskSource_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		internal Renderer cachedSupportedRenderer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Renderer>(SpriteMask.get_cachedSupportedRenderer_Injected(intPtr));
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002308 File Offset: 0x00000508
		internal Bounds GetSpriteBounds()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			SpriteMask.GetSpriteBounds_Injected(intPtr, out bounds);
			return bounds;
		}

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_frontSortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frontSortingLayerID_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_frontSortingOrder_Injected(IntPtr _unity_self);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frontSortingOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_backSortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backSortingLayerID_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_backSortingOrder_Injected(IntPtr _unity_self);

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backSortingOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_alphaCutoff_Injected(IntPtr _unity_self);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alphaCutoff_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sprite_Injected(IntPtr _unity_self);

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sprite_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isCustomRangeActive_Injected(IntPtr _unity_self);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isCustomRangeActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteSortPoint get_spriteSortPoint_Injected(IntPtr _unity_self);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spriteSortPoint_Injected(IntPtr _unity_self, SpriteSortPoint value);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMask.MaskSource get_maskSource_Injected(IntPtr _unity_self);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskSource_Injected(IntPtr _unity_self, SpriteMask.MaskSource value);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_cachedSupportedRenderer_Injected(IntPtr _unity_self);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpriteBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x02000003 RID: 3
		public enum MaskSource
		{
			// Token: 0x04000002 RID: 2
			Sprite,
			// Token: 0x04000003 RID: 3
			SupportedRenderers
		}
	}
}
