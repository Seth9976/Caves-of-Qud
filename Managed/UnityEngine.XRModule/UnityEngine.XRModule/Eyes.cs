using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Eyes : IEquatable<Eyes>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003AC4 File Offset: 0x00001CC4
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003ADC File Offset: 0x00001CDC
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public bool TryGetLeftEyePosition(out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition(this, EyeSide.Left, out position);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003B14 File Offset: 0x00001D14
		public bool TryGetRightEyePosition(out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition(this, EyeSide.Right, out position);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B34 File Offset: 0x00001D34
		public bool TryGetLeftEyeRotation(out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation(this, EyeSide.Left, out rotation);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B54 File Offset: 0x00001D54
		public bool TryGetRightEyeRotation(out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation(this, EyeSide.Right, out rotation);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B74 File Offset: 0x00001D74
		private static bool Eyes_TryGetEyePosition(Eyes eyes, EyeSide chirality, out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition_Injected(ref eyes, chirality, out position);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B8C File Offset: 0x00001D8C
		private static bool Eyes_TryGetEyeRotation(Eyes eyes, EyeSide chirality, out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation_Injected(ref eyes, chirality, out rotation);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public bool TryGetFixationPoint(out Vector3 fixationPoint)
		{
			return Eyes.Eyes_TryGetFixationPoint(this, out fixationPoint);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003BC4 File Offset: 0x00001DC4
		private static bool Eyes_TryGetFixationPoint(Eyes eyes, out Vector3 fixationPoint)
		{
			return Eyes.Eyes_TryGetFixationPoint_Injected(ref eyes, out fixationPoint);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003BDC File Offset: 0x00001DDC
		public bool TryGetLeftEyeOpenAmount(out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount(this, EyeSide.Left, out openAmount);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003BFC File Offset: 0x00001DFC
		public bool TryGetRightEyeOpenAmount(out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount(this, EyeSide.Right, out openAmount);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C1C File Offset: 0x00001E1C
		private static bool Eyes_TryGetEyeOpenAmount(Eyes eyes, EyeSide chirality, out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount_Injected(ref eyes, chirality, out openAmount);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C34 File Offset: 0x00001E34
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Eyes);
			return !flag && this.Equals((Eyes)obj);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003C68 File Offset: 0x00001E68
		public bool Equals(Eyes other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C9C File Offset: 0x00001E9C
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public static bool operator ==(Eyes a, Eyes b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static bool operator !=(Eyes a, Eyes b)
		{
			return !(a == b);
		}

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyePosition_Injected([In] ref Eyes eyes, EyeSide chirality, out Vector3 position);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyeRotation_Injected([In] ref Eyes eyes, EyeSide chirality, out Quaternion rotation);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetFixationPoint_Injected([In] ref Eyes eyes, out Vector3 fixationPoint);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyeOpenAmount_Injected([In] ref Eyes eyes, EyeSide chirality, out float openAmount);

		// Token: 0x040000A8 RID: 168
		private ulong m_DeviceId;

		// Token: 0x040000A9 RID: 169
		private uint m_FeatureIndex;
	}
}
