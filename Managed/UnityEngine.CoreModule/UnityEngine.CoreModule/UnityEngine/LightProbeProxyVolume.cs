using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001D0 RID: 464
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060014AA RID: 5290
		public static extern bool isFeatureSupported
		{
			[NativeName("IsFeatureSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000232E8 File Offset: 0x000214E8
		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				LightProbeProxyVolume.get_boundsGlobal_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00023310 File Offset: 0x00021510
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x00023338 File Offset: 0x00021538
		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LightProbeProxyVolume.get_sizeCustom_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_sizeCustom_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0002335C File Offset: 0x0002155C
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x00023384 File Offset: 0x00021584
		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LightProbeProxyVolume.get_originCustom_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_originCustom_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000233A8 File Offset: 0x000215A8
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x000233CC File Offset: 0x000215CC
		public float probeDensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_probeDensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_probeDensity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000233F0 File Offset: 0x000215F0
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x00023414 File Offset: 0x00021614
		public int gridResolutionX
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_gridResolutionX_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_gridResolutionX_Injected(intPtr, value);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00023438 File Offset: 0x00021638
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0002345C File Offset: 0x0002165C
		public int gridResolutionY
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_gridResolutionY_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_gridResolutionY_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00023480 File Offset: 0x00021680
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x000234A4 File Offset: 0x000216A4
		public int gridResolutionZ
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_gridResolutionZ_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_gridResolutionZ_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000234C8 File Offset: 0x000216C8
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x000234EC File Offset: 0x000216EC
		public LightProbeProxyVolume.BoundingBoxMode boundingBoxMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_boundingBoxMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_boundingBoxMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00023510 File Offset: 0x00021710
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00023534 File Offset: 0x00021734
		public LightProbeProxyVolume.ResolutionMode resolutionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_resolutionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_resolutionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00023558 File Offset: 0x00021758
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x0002357C File Offset: 0x0002177C
		public LightProbeProxyVolume.ProbePositionMode probePositionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_probePositionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_probePositionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x000235A0 File Offset: 0x000217A0
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x000235C4 File Offset: 0x000217C4
		public LightProbeProxyVolume.RefreshMode refreshMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_refreshMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_refreshMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x000235E8 File Offset: 0x000217E8
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0002360C File Offset: 0x0002180C
		public LightProbeProxyVolume.QualityMode qualityMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_qualityMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_qualityMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00023630 File Offset: 0x00021830
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00023654 File Offset: 0x00021854
		public LightProbeProxyVolume.DataFormat dataFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbeProxyVolume.get_dataFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightProbeProxyVolume.set_dataFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00023677 File Offset: 0x00021877
		public void Update()
		{
			this.SetDirtyFlag(true);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00023684 File Offset: 0x00021884
		private void SetDirtyFlag(bool flag)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbeProxyVolume>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LightProbeProxyVolume.SetDirtyFlag_Injected(intPtr, flag);
		}

		// Token: 0x060014C7 RID: 5319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_boundsGlobal_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060014C8 RID: 5320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sizeCustom_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060014C9 RID: 5321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sizeCustom_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060014CA RID: 5322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_originCustom_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060014CB RID: 5323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_originCustom_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060014CC RID: 5324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_probeDensity_Injected(IntPtr _unity_self);

		// Token: 0x060014CD RID: 5325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_probeDensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x060014CE RID: 5326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_gridResolutionX_Injected(IntPtr _unity_self);

		// Token: 0x060014CF RID: 5327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gridResolutionX_Injected(IntPtr _unity_self, int value);

		// Token: 0x060014D0 RID: 5328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_gridResolutionY_Injected(IntPtr _unity_self);

		// Token: 0x060014D1 RID: 5329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gridResolutionY_Injected(IntPtr _unity_self, int value);

		// Token: 0x060014D2 RID: 5330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_gridResolutionZ_Injected(IntPtr _unity_self);

		// Token: 0x060014D3 RID: 5331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gridResolutionZ_Injected(IntPtr _unity_self, int value);

		// Token: 0x060014D4 RID: 5332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.BoundingBoxMode get_boundingBoxMode_Injected(IntPtr _unity_self);

		// Token: 0x060014D5 RID: 5333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_boundingBoxMode_Injected(IntPtr _unity_self, LightProbeProxyVolume.BoundingBoxMode value);

		// Token: 0x060014D6 RID: 5334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.ResolutionMode get_resolutionMode_Injected(IntPtr _unity_self);

		// Token: 0x060014D7 RID: 5335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_resolutionMode_Injected(IntPtr _unity_self, LightProbeProxyVolume.ResolutionMode value);

		// Token: 0x060014D8 RID: 5336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.ProbePositionMode get_probePositionMode_Injected(IntPtr _unity_self);

		// Token: 0x060014D9 RID: 5337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_probePositionMode_Injected(IntPtr _unity_self, LightProbeProxyVolume.ProbePositionMode value);

		// Token: 0x060014DA RID: 5338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.RefreshMode get_refreshMode_Injected(IntPtr _unity_self);

		// Token: 0x060014DB RID: 5339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_refreshMode_Injected(IntPtr _unity_self, LightProbeProxyVolume.RefreshMode value);

		// Token: 0x060014DC RID: 5340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.QualityMode get_qualityMode_Injected(IntPtr _unity_self);

		// Token: 0x060014DD RID: 5341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_qualityMode_Injected(IntPtr _unity_self, LightProbeProxyVolume.QualityMode value);

		// Token: 0x060014DE RID: 5342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeProxyVolume.DataFormat get_dataFormat_Injected(IntPtr _unity_self);

		// Token: 0x060014DF RID: 5343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dataFormat_Injected(IntPtr _unity_self, LightProbeProxyVolume.DataFormat value);

		// Token: 0x060014E0 RID: 5344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirtyFlag_Injected(IntPtr _unity_self, bool flag);

		// Token: 0x020001D1 RID: 465
		public enum ResolutionMode
		{
			// Token: 0x04000683 RID: 1667
			Automatic,
			// Token: 0x04000684 RID: 1668
			Custom
		}

		// Token: 0x020001D2 RID: 466
		public enum BoundingBoxMode
		{
			// Token: 0x04000686 RID: 1670
			AutomaticLocal,
			// Token: 0x04000687 RID: 1671
			AutomaticWorld,
			// Token: 0x04000688 RID: 1672
			Custom
		}

		// Token: 0x020001D3 RID: 467
		public enum ProbePositionMode
		{
			// Token: 0x0400068A RID: 1674
			CellCorner,
			// Token: 0x0400068B RID: 1675
			CellCenter
		}

		// Token: 0x020001D4 RID: 468
		public enum RefreshMode
		{
			// Token: 0x0400068D RID: 1677
			Automatic,
			// Token: 0x0400068E RID: 1678
			EveryFrame,
			// Token: 0x0400068F RID: 1679
			ViaScripting
		}

		// Token: 0x020001D5 RID: 469
		public enum QualityMode
		{
			// Token: 0x04000691 RID: 1681
			Low,
			// Token: 0x04000692 RID: 1682
			Normal
		}

		// Token: 0x020001D6 RID: 470
		public enum DataFormat
		{
			// Token: 0x04000694 RID: 1684
			HalfFloat,
			// Token: 0x04000695 RID: 1685
			Float
		}
	}
}
