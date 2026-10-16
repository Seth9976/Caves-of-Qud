using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F1 RID: 241
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00008760 File Offset: 0x00006960
		public void RegisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent == null;
			if (flag)
			{
				this.m_SpriteChangeEvent = new UnityEvent<SpriteRenderer>();
			}
			this.m_SpriteChangeEvent.AddListener(callback);
			this.hasSpriteChangeEvents = true;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000879C File Offset: 0x0000699C
		public void UnregisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent != null;
			if (flag)
			{
				this.m_SpriteChangeEvent.RemoveListener(callback);
				bool flag2 = this.m_SpriteChangeEvent.GetCallsCount() == 0;
				if (flag2)
				{
					this.hasSpriteChangeEvents = false;
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000087E0 File Offset: 0x000069E0
		[RequiredByNativeCode]
		private void InvokeSpriteChanged()
		{
			try
			{
				UnityEvent<SpriteRenderer> spriteChangeEvent = this.m_SpriteChangeEvent;
				if (spriteChangeEvent != null)
				{
					spriteChangeEvent.Invoke(this);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00008824 File Offset: 0x00006A24
		internal bool shouldSupportTiling
		{
			[NativeMethod("ShouldSupportTiling")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_shouldSupportTiling_Injected(intPtr);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00008848 File Offset: 0x00006A48
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000886C File Offset: 0x00006A6C
		internal bool hasSpriteChangeEvents
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_hasSpriteChangeEvents_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_hasSpriteChangeEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00008890 File Offset: 0x00006A90
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000088B8 File Offset: 0x00006AB8
		public Sprite sprite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Sprite>(SpriteRenderer.get_sprite_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_sprite_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Sprite>(value));
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000088E0 File Offset: 0x00006AE0
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00008904 File Offset: 0x00006B04
		public SpriteDrawMode drawMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_drawMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_drawMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00008928 File Offset: 0x00006B28
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00008950 File Offset: 0x00006B50
		public Vector2 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				SpriteRenderer.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00008974 File Offset: 0x00006B74
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00008998 File Offset: 0x00006B98
		public float adaptiveModeThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_adaptiveModeThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_adaptiveModeThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000089BC File Offset: 0x00006BBC
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000089E0 File Offset: 0x00006BE0
		public SpriteTileMode tileMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_tileMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_tileMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00008A04 File Offset: 0x00006C04
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00008A2C File Offset: 0x00006C2C
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				SpriteRenderer.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00008A50 File Offset: 0x00006C50
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00008A74 File Offset: 0x00006C74
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00008A98 File Offset: 0x00006C98
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00008ABC File Offset: 0x00006CBC
		public bool flipX
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_flipX_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_flipX_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00008AE0 File Offset: 0x00006CE0
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00008B04 File Offset: 0x00006D04
		public bool flipY
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_flipY_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_flipY_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00008B28 File Offset: 0x00006D28
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00008B4C File Offset: 0x00006D4C
		public SpriteSortPoint spriteSortPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpriteRenderer.get_spriteSortPoint_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpriteRenderer.set_spriteSortPoint_Injected(intPtr, value);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00008B70 File Offset: 0x00006D70
		private IntPtr GetCurrentMeshDataPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SpriteRenderer.GetCurrentMeshDataPtr_Injected(intPtr);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00008B94 File Offset: 0x00006D94
		internal unsafe Mesh.MeshDataArray GetCurrentMeshData()
		{
			IntPtr currentMeshDataPtr = this.GetCurrentMeshDataPtr();
			bool flag = currentMeshDataPtr == IntPtr.Zero;
			Mesh.MeshDataArray meshDataArray;
			if (flag)
			{
				meshDataArray = new Mesh.MeshDataArray(0);
			}
			else
			{
				Mesh.MeshDataArray meshDataArray2 = new Mesh.MeshDataArray(1);
				*meshDataArray2.m_Ptrs = currentMeshDataPtr;
				meshDataArray = meshDataArray2;
			}
			return meshDataArray;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00008BD8 File Offset: 0x00006DD8
		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			SpriteRenderer.Internal_GetSpriteBounds_Injected(intPtr, mode, out bounds);
			return bounds;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008C00 File Offset: 0x00006E00
		internal void GetSecondaryTextureProperties([NotNull] MaterialPropertyBlock mbp)
		{
			if (mbp == null)
			{
				ThrowHelper.ThrowArgumentNullException(mbp, "mbp");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpriteRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(mbp);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mbp, "mbp");
			}
			SpriteRenderer.GetSecondaryTextureProperties_Injected(intPtr, intPtr2);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00008C48 File Offset: 0x00006E48
		internal Bounds GetSpriteBounds()
		{
			return this.Internal_GetSpriteBounds(this.drawMode);
		}

		// Token: 0x060004A2 RID: 1186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_shouldSupportTiling_Injected(IntPtr _unity_self);

		// Token: 0x060004A3 RID: 1187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasSpriteChangeEvents_Injected(IntPtr _unity_self);

		// Token: 0x060004A4 RID: 1188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasSpriteChangeEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004A5 RID: 1189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sprite_Injected(IntPtr _unity_self);

		// Token: 0x060004A6 RID: 1190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sprite_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060004A7 RID: 1191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteDrawMode get_drawMode_Injected(IntPtr _unity_self);

		// Token: 0x060004A8 RID: 1192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drawMode_Injected(IntPtr _unity_self, SpriteDrawMode value);

		// Token: 0x060004A9 RID: 1193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004AA RID: 1194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060004AB RID: 1195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_adaptiveModeThreshold_Injected(IntPtr _unity_self);

		// Token: 0x060004AC RID: 1196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_adaptiveModeThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004AD RID: 1197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteTileMode get_tileMode_Injected(IntPtr _unity_self);

		// Token: 0x060004AE RID: 1198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tileMode_Injected(IntPtr _unity_self, SpriteTileMode value);

		// Token: 0x060004AF RID: 1199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060004B0 RID: 1200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060004B1 RID: 1201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x060004B2 RID: 1202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x060004B3 RID: 1203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_flipX_Injected(IntPtr _unity_self);

		// Token: 0x060004B4 RID: 1204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flipX_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004B5 RID: 1205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_flipY_Injected(IntPtr _unity_self);

		// Token: 0x060004B6 RID: 1206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flipY_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004B7 RID: 1207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteSortPoint get_spriteSortPoint_Injected(IntPtr _unity_self);

		// Token: 0x060004B8 RID: 1208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spriteSortPoint_Injected(IntPtr _unity_self, SpriteSortPoint value);

		// Token: 0x060004B9 RID: 1209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCurrentMeshDataPtr_Injected(IntPtr _unity_self);

		// Token: 0x060004BA RID: 1210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetSpriteBounds_Injected(IntPtr _unity_self, SpriteDrawMode mode, out Bounds ret);

		// Token: 0x060004BB RID: 1211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSecondaryTextureProperties_Injected(IntPtr _unity_self, IntPtr mbp);

		// Token: 0x040002C9 RID: 713
		private UnityEvent<SpriteRenderer> m_SpriteChangeEvent;
	}
}
