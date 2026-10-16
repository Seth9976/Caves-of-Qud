using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWind.h")]
	[ExcludeFromPreset]
	public class SpeedTreeWindAsset : Object
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003218 File Offset: 0x00001418
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000323C File Offset: 0x0000143C
		public int Version
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpeedTreeWindAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpeedTreeWindAsset.get_Version_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpeedTreeWindAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpeedTreeWindAsset.set_Version_Injected(intPtr, value);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000325F File Offset: 0x0000145F
		internal SpeedTreeWindAsset(int version, SpeedTreeWindConfig9 config)
		{
			SpeedTreeWindAsset.Internal_Create(this, version, SpeedTreeWindConfig9.Serialize(config));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003278 File Offset: 0x00001478
		[NativeThrows]
		private unsafe static void Internal_Create([Writable] SpeedTreeWindAsset notSelf, int version, byte[] data)
		{
			Span<byte> span = new Span<byte>(data);
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				SpeedTreeWindAsset.Internal_Create_Injected(notSelf, version, ref managedSpanWrapper);
			}
		}

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Version_Injected(IntPtr _unity_self);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Version_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create_Injected([Writable] SpeedTreeWindAsset notSelf, int version, ref ManagedSpanWrapper data);
	}
}
