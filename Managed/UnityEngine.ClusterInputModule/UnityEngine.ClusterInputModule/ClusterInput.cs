using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[Obsolete("This type is deprecated and will be removed in a future release.", false)]
	[NativeConditional("ENABLE_CLUSTERINPUT")]
	[NativeHeader("Modules/ClusterInput/ClusterInput.h")]
	public class ClusterInput
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public unsafe static float GetAxis(string name)
		{
			float axis_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				axis_Injected = ClusterInput.GetAxis_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return axis_Injected;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public unsafe static bool GetButton(string name)
		{
			bool button_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				button_Injected = ClusterInput.GetButton_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return button_Injected;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		[NativeConditional("ENABLE_CLUSTERINPUT", "Vector3f(0.0f, 0.0f, 0.0f)")]
		public unsafe static Vector3 GetTrackerPosition(string name)
		{
			Vector3 vector2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Vector3 vector;
				ClusterInput.GetTrackerPosition_Injected(ref managedSpanWrapper, out vector);
			}
			finally
			{
				char* ptr = null;
				Vector3 vector;
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		[NativeConditional("ENABLE_CLUSTERINPUT", "Quartenion::identity")]
		public unsafe static Quaternion GetTrackerRotation(string name)
		{
			Quaternion quaternion2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Quaternion quaternion;
				ClusterInput.GetTrackerRotation_Injected(ref managedSpanWrapper, out quaternion);
			}
			finally
			{
				char* ptr = null;
				Quaternion quaternion;
				quaternion2 = quaternion;
			}
			return quaternion2;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B8 File Offset: 0x000003B8
		public unsafe static void SetAxis(string name, float value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ClusterInput.SetAxis_Injected(ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002210 File Offset: 0x00000410
		public unsafe static void SetButton(string name, bool value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ClusterInput.SetButton_Injected(ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002268 File Offset: 0x00000468
		public unsafe static void SetTrackerPosition(string name, Vector3 value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ClusterInput.SetTrackerPosition_Injected(ref managedSpanWrapper, ref value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000004C0
		public unsafe static void SetTrackerRotation(string name, Quaternion value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ClusterInput.SetTrackerRotation_Injected(ref managedSpanWrapper, ref value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002318 File Offset: 0x00000518
		public unsafe static bool AddInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(deviceName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = deviceName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(serverUrl, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = serverUrl.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				flag = ClusterInput.AddInput_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref managedSpanWrapper3, index, type);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return flag;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023D8 File Offset: 0x000005D8
		public unsafe static bool EditInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(deviceName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = deviceName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(serverUrl, ref managedSpanWrapper3))
				{
					ReadOnlySpan<char> readOnlySpan3 = serverUrl.AsSpan();
					fixed (char* ptr3 = readOnlySpan3.GetPinnableReference())
					{
						managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, readOnlySpan3.Length);
					}
				}
				flag = ClusterInput.EditInput_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref managedSpanWrapper3, index, type);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				char* ptr3 = null;
			}
			return flag;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002498 File Offset: 0x00000698
		public unsafe static bool CheckConnectionToServer(string name)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ClusterInput.CheckConnectionToServer_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAxis_Injected(ref ManagedSpanWrapper name);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetButton_Injected(ref ManagedSpanWrapper name);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerPosition_Injected(ref ManagedSpanWrapper name, out Vector3 ret);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerRotation_Injected(ref ManagedSpanWrapper name, out Quaternion ret);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAxis_Injected(ref ManagedSpanWrapper name, float value);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetButton_Injected(ref ManagedSpanWrapper name, bool value);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerPosition_Injected(ref ManagedSpanWrapper name, [In] ref Vector3 value);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerRotation_Injected(ref ManagedSpanWrapper name, [In] ref Quaternion value);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddInput_Injected(ref ManagedSpanWrapper name, ref ManagedSpanWrapper deviceName, ref ManagedSpanWrapper serverUrl, int index, ClusterInputType type);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool EditInput_Injected(ref ManagedSpanWrapper name, ref ManagedSpanWrapper deviceName, ref ManagedSpanWrapper serverUrl, int index, ClusterInputType type);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckConnectionToServer_Injected(ref ManagedSpanWrapper name);
	}
}
