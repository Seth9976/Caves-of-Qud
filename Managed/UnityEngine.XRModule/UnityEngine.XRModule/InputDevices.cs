using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000017 RID: 23
	[UsedByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[StructLayout(LayoutKind.Sequential)]
	public class InputDevices
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003F48 File Offset: 0x00002148
		public static InputDevice GetDeviceAtXRNode(XRNode node)
		{
			ulong deviceIdAtXRNode = InputTracking.GetDeviceIdAtXRNode(node);
			return new InputDevice(deviceIdAtXRNode);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F68 File Offset: 0x00002168
		public static void GetDevicesAtXRNode(XRNode node, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			List<ulong> list = new List<ulong>();
			InputTracking.GetDeviceIdsAtXRNode_Internal(node, list);
			inputDevices.Clear();
			foreach (ulong num in list)
			{
				InputDevice inputDevice = new InputDevice(num);
				bool isValid = inputDevice.isValid;
				if (isValid)
				{
					inputDevices.Add(inputDevice);
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FFC File Offset: 0x000021FC
		public static void GetDevices(List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			inputDevices.Clear();
			InputDevices.GetDevices_Internal(inputDevices);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000402C File Offset: 0x0000222C
		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevices.GetDevicesWithCharacteristics instead.")]
		public static void GetDevicesWithRole(InputDeviceRole role, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			bool flag2 = InputDevices.s_InputDeviceList == null;
			if (flag2)
			{
				InputDevices.s_InputDeviceList = new List<InputDevice>();
			}
			InputDevices.GetDevices_Internal(InputDevices.s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice inputDevice in InputDevices.s_InputDeviceList)
			{
				bool flag3 = inputDevice.role == role;
				if (flag3)
				{
					inputDevices.Add(inputDevice);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000040D0 File Offset: 0x000022D0
		public static void GetDevicesWithCharacteristics(InputDeviceCharacteristics desiredCharacteristics, List<InputDevice> inputDevices)
		{
			bool flag = inputDevices == null;
			if (flag)
			{
				throw new ArgumentNullException("inputDevices");
			}
			bool flag2 = InputDevices.s_InputDeviceList == null;
			if (flag2)
			{
				InputDevices.s_InputDeviceList = new List<InputDevice>();
			}
			InputDevices.GetDevices_Internal(InputDevices.s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice inputDevice in InputDevices.s_InputDeviceList)
			{
				bool flag3 = (inputDevice.characteristics & desiredCharacteristics) == desiredCharacteristics;
				if (flag3)
				{
					inputDevices.Add(inputDevice);
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000BB RID: 187 RVA: 0x00004174 File Offset: 0x00002374
		// (remove) Token: 0x060000BC RID: 188 RVA: 0x000041A8 File Offset: 0x000023A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<InputDevice> deviceConnected;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000BD RID: 189 RVA: 0x000041DC File Offset: 0x000023DC
		// (remove) Token: 0x060000BE RID: 190 RVA: 0x00004210 File Offset: 0x00002410
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<InputDevice> deviceDisconnected;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000BF RID: 191 RVA: 0x00004244 File Offset: 0x00002444
		// (remove) Token: 0x060000C0 RID: 192 RVA: 0x00004278 File Offset: 0x00002478
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<InputDevice> deviceConfigChanged;

		// Token: 0x060000C1 RID: 193 RVA: 0x000042AC File Offset: 0x000024AC
		[RequiredByNativeCode]
		private static void InvokeConnectionEvent(ulong deviceId, ConnectionChangeType change)
		{
			switch (change)
			{
			case ConnectionChangeType.Connected:
			{
				bool flag = InputDevices.deviceConnected != null;
				if (flag)
				{
					InputDevices.deviceConnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.Disconnected:
			{
				bool flag2 = InputDevices.deviceDisconnected != null;
				if (flag2)
				{
					InputDevices.deviceDisconnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.ConfigChange:
			{
				bool flag3 = InputDevices.deviceConfigChanged != null;
				if (flag3)
				{
					InputDevices.deviceConfigChanged(new InputDevice(deviceId));
				}
				break;
			}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004334 File Offset: 0x00002534
		private unsafe static void GetDevices_Internal([NotNull] List<InputDevice> inputDevices)
		{
			if (inputDevices == null)
			{
				ThrowHelper.ThrowArgumentNullException(inputDevices, "inputDevices");
			}
			try
			{
				fixed (InputDevice[] array = NoAllocHelpers.ExtractArrayFromList<InputDevice>(inputDevices))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, inputDevices.Count);
					InputDevices.GetDevices_Internal_Injected(ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<InputDevice>(inputDevices);
			}
		}

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SendHapticImpulse(ulong deviceId, uint channel, float amplitude, float duration);

		// Token: 0x060000C4 RID: 196 RVA: 0x000043A0 File Offset: 0x000025A0
		internal unsafe static bool SendHapticBuffer(ulong deviceId, uint channel, [NotNull] byte[] buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Span<byte> span = new Span<byte>(buffer);
			bool flag;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = InputDevices.SendHapticBuffer_Injected(deviceId, channel, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetHapticCapabilities(ulong deviceId, out HapticCapabilities capabilities);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StopHaptics(ulong deviceId);

		// Token: 0x060000C7 RID: 199 RVA: 0x000043EC File Offset: 0x000025EC
		internal static bool TryGetFeatureUsages(ulong deviceId, [NotNull] List<InputFeatureUsage> featureUsages)
		{
			if (featureUsages == null)
			{
				ThrowHelper.ThrowArgumentNullException(featureUsages, "featureUsages");
			}
			return InputDevices.TryGetFeatureUsages_Injected(deviceId, featureUsages);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004410 File Offset: 0x00002610
		internal unsafe static bool TryGetFeatureValue_bool(ulong deviceId, string usage, out bool value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_bool_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004468 File Offset: 0x00002668
		internal unsafe static bool TryGetFeatureValue_UInt32(ulong deviceId, string usage, out uint value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_UInt32_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000044C0 File Offset: 0x000026C0
		internal unsafe static bool TryGetFeatureValue_float(ulong deviceId, string usage, out float value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_float_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004518 File Offset: 0x00002718
		internal unsafe static bool TryGetFeatureValue_Vector2f(ulong deviceId, string usage, out Vector2 value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_Vector2f_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004570 File Offset: 0x00002770
		internal unsafe static bool TryGetFeatureValue_Vector3f(ulong deviceId, string usage, out Vector3 value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_Vector3f_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000045C8 File Offset: 0x000027C8
		internal unsafe static bool TryGetFeatureValue_Quaternionf(ulong deviceId, string usage, out Quaternion value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_Quaternionf_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004620 File Offset: 0x00002820
		internal unsafe static bool TryGetFeatureValue_Custom(ulong deviceId, string usage, [Out] byte[] value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (value != null)
				{
					fixed (byte[] array = value)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				flag = InputDevices.TryGetFeatureValue_Custom_Injected(deviceId, ref managedSpanWrapper, out blittableArrayWrapper);
			}
			finally
			{
				char* ptr = null;
				byte[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
			}
			return flag;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000046A8 File Offset: 0x000028A8
		internal unsafe static bool TryGetFeatureValueAtTime_bool(ulong deviceId, string usage, long time, out bool value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_bool_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004704 File Offset: 0x00002904
		internal unsafe static bool TryGetFeatureValueAtTime_UInt32(ulong deviceId, string usage, long time, out uint value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_UInt32_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004760 File Offset: 0x00002960
		internal unsafe static bool TryGetFeatureValueAtTime_float(ulong deviceId, string usage, long time, out float value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_float_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000047BC File Offset: 0x000029BC
		internal unsafe static bool TryGetFeatureValueAtTime_Vector2f(ulong deviceId, string usage, long time, out Vector2 value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_Vector2f_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004818 File Offset: 0x00002A18
		internal unsafe static bool TryGetFeatureValueAtTime_Vector3f(ulong deviceId, string usage, long time, out Vector3 value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_Vector3f_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004874 File Offset: 0x00002A74
		internal unsafe static bool TryGetFeatureValueAtTime_Quaternionf(ulong deviceId, string usage, long time, out Quaternion value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValueAtTime_Quaternionf_Injected(deviceId, ref managedSpanWrapper, time, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000048D0 File Offset: 0x00002AD0
		internal unsafe static bool TryGetFeatureValue_XRHand(ulong deviceId, string usage, out Hand value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_XRHand_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004928 File Offset: 0x00002B28
		internal unsafe static bool TryGetFeatureValue_XRBone(ulong deviceId, string usage, out Bone value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_XRBone_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004980 File Offset: 0x00002B80
		internal unsafe static bool TryGetFeatureValue_XREyes(ulong deviceId, string usage, out Eyes value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(usage, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = usage.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = InputDevices.TryGetFeatureValue_XREyes_Injected(deviceId, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDeviceValid(ulong deviceId);

		// Token: 0x060000D9 RID: 217 RVA: 0x000049D8 File Offset: 0x00002BD8
		internal static string GetDeviceName(ulong deviceId)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				InputDevices.GetDeviceName_Injected(deviceId, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004A08 File Offset: 0x00002C08
		internal static string GetDeviceManufacturer(ulong deviceId)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				InputDevices.GetDeviceManufacturer_Injected(deviceId, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004A38 File Offset: 0x00002C38
		internal static string GetDeviceSerialNumber(ulong deviceId)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				InputDevices.GetDeviceSerialNumber_Injected(deviceId, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern InputDeviceCharacteristics GetDeviceCharacteristics(ulong deviceId);

		// Token: 0x060000DD RID: 221 RVA: 0x00004A68 File Offset: 0x00002C68
		internal static InputDeviceRole GetDeviceRole(ulong deviceId)
		{
			InputDeviceCharacteristics deviceCharacteristics = InputDevices.GetDeviceCharacteristics(deviceId);
			bool flag = (deviceCharacteristics & (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice)) == (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice);
			InputDeviceRole inputDeviceRole;
			if (flag)
			{
				inputDeviceRole = InputDeviceRole.Generic;
			}
			else
			{
				bool flag2 = (deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left);
				if (flag2)
				{
					inputDeviceRole = InputDeviceRole.LeftHanded;
				}
				else
				{
					bool flag3 = (deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right);
					if (flag3)
					{
						inputDeviceRole = InputDeviceRole.RightHanded;
					}
					else
					{
						bool flag4 = (deviceCharacteristics & InputDeviceCharacteristics.Controller) == InputDeviceCharacteristics.Controller;
						if (flag4)
						{
							inputDeviceRole = InputDeviceRole.GameController;
						}
						else
						{
							bool flag5 = (deviceCharacteristics & (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference)) == (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference);
							if (flag5)
							{
								inputDeviceRole = InputDeviceRole.TrackingReference;
							}
							else
							{
								bool flag6 = (deviceCharacteristics & InputDeviceCharacteristics.TrackedDevice) == InputDeviceCharacteristics.TrackedDevice;
								if (flag6)
								{
									inputDeviceRole = InputDeviceRole.HardwareTracker;
								}
								else
								{
									inputDeviceRole = InputDeviceRole.Unknown;
								}
							}
						}
					}
				}
			}
			return inputDeviceRole;
		}

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDevices_Internal_Injected(ref BlittableListWrapper inputDevices);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendHapticBuffer_Injected(ulong deviceId, uint channel, ref ManagedSpanWrapper buffer);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureUsages_Injected(ulong deviceId, List<InputFeatureUsage> featureUsages);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_bool_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out bool value);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_UInt32_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out uint value);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_float_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out float value);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_Vector2f_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Vector2 value);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_Vector3f_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Vector3 value);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_Quaternionf_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Quaternion value);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_Custom_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out BlittableArrayWrapper value);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_bool_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out bool value);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_UInt32_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out uint value);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_float_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out float value);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_Vector2f_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out Vector2 value);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_Vector3f_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out Vector3 value);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValueAtTime_Quaternionf_Injected(ulong deviceId, ref ManagedSpanWrapper usage, long time, out Quaternion value);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_XRHand_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Hand value);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_XRBone_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Bone value);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFeatureValue_XREyes_Injected(ulong deviceId, ref ManagedSpanWrapper usage, out Eyes value);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceName_Injected(ulong deviceId, out ManagedSpanWrapper ret);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceManufacturer_Injected(ulong deviceId, out ManagedSpanWrapper ret);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceSerialNumber_Injected(ulong deviceId, out ManagedSpanWrapper ret);

		// Token: 0x040000AC RID: 172
		private static List<InputDevice> s_InputDeviceList;
	}
}
