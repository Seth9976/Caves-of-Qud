using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	[NativeHeader("Runtime/Camera/Projector.h")]
	public sealed class Projector : Behaviour
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000221D8 File Offset: 0x000203D8
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000221FC File Offset: 0x000203FC
		public float nearClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_nearClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_nearClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00022220 File Offset: 0x00020420
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x00022244 File Offset: 0x00020444
		public float farClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_farClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_farClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00022268 File Offset: 0x00020468
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0002228C File Offset: 0x0002048C
		public float fieldOfView
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_fieldOfView_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_fieldOfView_Injected(intPtr, value);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000222B0 File Offset: 0x000204B0
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x000222D4 File Offset: 0x000204D4
		public float aspectRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_aspectRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_aspectRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000222F8 File Offset: 0x000204F8
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0002231C File Offset: 0x0002051C
		public bool orthographic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_orthographic_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_orthographic_Injected(intPtr, value);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00022340 File Offset: 0x00020540
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x00022364 File Offset: 0x00020564
		public float orthographicSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_orthographicSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_orthographicSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00022388 File Offset: 0x00020588
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x000223AC File Offset: 0x000205AC
		public int ignoreLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Projector.get_ignoreLayers_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_ignoreLayers_Injected(intPtr, value);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x000223D0 File Offset: 0x000205D0
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x000223F8 File Offset: 0x000205F8
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Projector.get_material_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Projector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Projector.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x060013D2 RID: 5074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_nearClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x060013D3 RID: 5075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nearClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013D4 RID: 5076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_farClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x060013D5 RID: 5077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_farClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013D6 RID: 5078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fieldOfView_Injected(IntPtr _unity_self);

		// Token: 0x060013D7 RID: 5079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fieldOfView_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013D8 RID: 5080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_aspectRatio_Injected(IntPtr _unity_self);

		// Token: 0x060013D9 RID: 5081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aspectRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013DA RID: 5082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_orthographic_Injected(IntPtr _unity_self);

		// Token: 0x060013DB RID: 5083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060013DC RID: 5084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_orthographicSize_Injected(IntPtr _unity_self);

		// Token: 0x060013DD RID: 5085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographicSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013DE RID: 5086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_ignoreLayers_Injected(IntPtr _unity_self);

		// Token: 0x060013DF RID: 5087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreLayers_Injected(IntPtr _unity_self, int value);

		// Token: 0x060013E0 RID: 5088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x060013E1 RID: 5089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);
	}
}
