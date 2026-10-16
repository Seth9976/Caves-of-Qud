using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x0200003C RID: 60
	[NativeHeader("Modules/Audio/Public/AudioContainerElement.h")]
	internal sealed class AudioContainerElement : Object
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000065B9 File Offset: 0x000047B9
		internal AudioContainerElement()
		{
			AudioContainerElement.Internal_Create(this);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000065CC File Offset: 0x000047CC
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000065F4 File Offset: 0x000047F4
		internal AudioClip audioClip
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioClip>(AudioContainerElement.get_audioClip_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioContainerElement.set_audioClip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioClip>(value));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000661C File Offset: 0x0000481C
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00006640 File Offset: 0x00004840
		internal float volume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioContainerElement.get_volume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioContainerElement.set_volume_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00006664 File Offset: 0x00004864
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00006688 File Offset: 0x00004888
		internal bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioContainerElement.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioContainerElement>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioContainerElement.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x0600032B RID: 811
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AudioContainerElement self);

		// Token: 0x0600032C RID: 812
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_audioClip_Injected(IntPtr _unity_self);

		// Token: 0x0600032D RID: 813
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_audioClip_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600032E RID: 814
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_volume_Injected(IntPtr _unity_self);

		// Token: 0x0600032F RID: 815
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volume_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000330 RID: 816
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06000331 RID: 817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);
	}
}
