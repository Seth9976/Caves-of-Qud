using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000027 RID: 39
	[NativeConditional("ENABLE_XR")]
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h")]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00005514 File Offset: 0x00003714
		internal uint GetIndex()
		{
			IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRInputSubsystem.GetIndex_Injected(intPtr);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005538 File Offset: 0x00003738
		public bool TryRecenter()
		{
			IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRInputSubsystem.TryRecenter_Injected(intPtr);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000555C File Offset: 0x0000375C
		public bool TryGetInputDevices(List<InputDevice> devices)
		{
			bool flag = devices == null;
			if (flag)
			{
				throw new ArgumentNullException("devices");
			}
			devices.Clear();
			bool flag2 = this.m_DeviceIdsCache == null;
			if (flag2)
			{
				this.m_DeviceIdsCache = new List<ulong>();
			}
			this.m_DeviceIdsCache.Clear();
			this.TryGetDeviceIds_AsList(this.m_DeviceIdsCache);
			for (int i = 0; i < this.m_DeviceIdsCache.Count; i++)
			{
				devices.Add(new InputDevice(this.m_DeviceIdsCache[i]));
			}
			return true;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000055F0 File Offset: 0x000037F0
		public bool TrySetTrackingOriginMode(TrackingOriginModeFlags origin)
		{
			IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRInputSubsystem.TrySetTrackingOriginMode_Injected(intPtr, origin);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005614 File Offset: 0x00003814
		public TrackingOriginModeFlags GetTrackingOriginMode()
		{
			IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRInputSubsystem.GetTrackingOriginMode_Injected(intPtr);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005638 File Offset: 0x00003838
		public TrackingOriginModeFlags GetSupportedTrackingOriginModes()
		{
			IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRInputSubsystem.GetSupportedTrackingOriginModes_Injected(intPtr);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000565C File Offset: 0x0000385C
		public bool TryGetBoundaryPoints(List<Vector3> boundaryPoints)
		{
			bool flag = boundaryPoints == null;
			if (flag)
			{
				throw new ArgumentNullException("boundaryPoints");
			}
			return this.TryGetBoundaryPoints_AsList(boundaryPoints);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005688 File Offset: 0x00003888
		private unsafe bool TryGetBoundaryPoints_AsList(List<Vector3> boundaryPoints)
		{
			bool flag;
			try
			{
				IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (boundaryPoints != null)
				{
					fixed (Vector3[] array = NoAllocHelpers.ExtractArrayFromList<Vector3>(boundaryPoints))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, boundaryPoints.Count);
					}
				}
				flag = XRInputSubsystem.TryGetBoundaryPoints_AsList_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector3>(boundaryPoints);
			}
			return flag;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000179 RID: 377 RVA: 0x00005700 File Offset: 0x00003900
		// (remove) Token: 0x0600017A RID: 378 RVA: 0x00005738 File Offset: 0x00003938
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<XRInputSubsystem> trackingOriginUpdated;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600017B RID: 379 RVA: 0x00005770 File Offset: 0x00003970
		// (remove) Token: 0x0600017C RID: 380 RVA: 0x000057A8 File Offset: 0x000039A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<XRInputSubsystem> boundaryChanged;

		// Token: 0x0600017D RID: 381 RVA: 0x000057E0 File Offset: 0x000039E0
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.trackingOriginUpdated != null;
			if (flag)
			{
				xrinputSubsystem.trackingOriginUpdated(xrinputSubsystem);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000581C File Offset: 0x00003A1C
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.boundaryChanged != null;
			if (flag)
			{
				xrinputSubsystem.boundaryChanged(xrinputSubsystem);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005858 File Offset: 0x00003A58
		internal unsafe void TryGetDeviceIds_AsList(List<ulong> deviceIds)
		{
			try
			{
				IntPtr intPtr = XRInputSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (deviceIds != null)
				{
					fixed (ulong[] array = NoAllocHelpers.ExtractArrayFromList<ulong>(deviceIds))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, deviceIds.Count);
					}
				}
				XRInputSubsystem.TryGetDeviceIds_AsList_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ulong>(deviceIds);
			}
		}

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetIndex_Injected(IntPtr _unity_self);

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryRecenter_Injected(IntPtr _unity_self);

		// Token: 0x06000183 RID: 387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetTrackingOriginMode_Injected(IntPtr _unity_self, TrackingOriginModeFlags origin);

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TrackingOriginModeFlags GetTrackingOriginMode_Injected(IntPtr _unity_self);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TrackingOriginModeFlags GetSupportedTrackingOriginModes_Injected(IntPtr _unity_self);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetBoundaryPoints_AsList_Injected(IntPtr _unity_self, ref BlittableListWrapper boundaryPoints);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryGetDeviceIds_AsList_Injected(IntPtr _unity_self, ref BlittableListWrapper deviceIds);

		// Token: 0x040000F2 RID: 242
		private List<ulong> m_DeviceIdsCache;

		// Token: 0x02000028 RID: 40
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000188 RID: 392 RVA: 0x00005467 File Offset: 0x00003667
			public static IntPtr ConvertToNative(XRInputSubsystem xrInputSubsystem)
			{
				return xrInputSubsystem.m_Ptr;
			}
		}
	}
}
