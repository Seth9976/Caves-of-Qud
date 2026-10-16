using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000196 RID: 406
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class LensFlare : Behaviour
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000220AC File Offset: 0x000202AC
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x000220D0 File Offset: 0x000202D0
		public float brightness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LensFlare.get_brightness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LensFlare.set_brightness_Injected(intPtr, value);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000220F4 File Offset: 0x000202F4
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x00022118 File Offset: 0x00020318
		public float fadeSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LensFlare.get_fadeSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LensFlare.set_fadeSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0002213C File Offset: 0x0002033C
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00022164 File Offset: 0x00020364
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				LensFlare.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LensFlare.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00022188 File Offset: 0x00020388
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000221B0 File Offset: 0x000203B0
		public Flare flare
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Flare>(LensFlare.get_flare_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LensFlare>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LensFlare.set_flare_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Flare>(value));
			}
		}

		// Token: 0x060013B9 RID: 5049
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_brightness_Injected(IntPtr _unity_self);

		// Token: 0x060013BA RID: 5050
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_brightness_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013BB RID: 5051
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fadeSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060013BC RID: 5052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fadeSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060013BD RID: 5053
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060013BE RID: 5054
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060013BF RID: 5055
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_flare_Injected(IntPtr _unity_self);

		// Token: 0x060013C0 RID: 5056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flare_Injected(IntPtr _unity_self, IntPtr value);
	}
}
