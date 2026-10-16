using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct Hand : IEquatable<Hand>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000038E0 File Offset: 0x00001AE0
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003910 File Offset: 0x00001B10
		public bool TryGetRootBone(out Bone boneOut)
		{
			return Hand.Hand_TryGetRootBone(this, out boneOut);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003930 File Offset: 0x00001B30
		private static bool Hand_TryGetRootBone(Hand hand, out Bone boneOut)
		{
			return Hand.Hand_TryGetRootBone_Injected(ref hand, out boneOut);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003948 File Offset: 0x00001B48
		public bool TryGetFingerBones(HandFinger finger, List<Bone> bonesOut)
		{
			bool flag = bonesOut == null;
			if (flag)
			{
				throw new ArgumentNullException("bonesOut");
			}
			return Hand.Hand_TryGetFingerBonesAsList(this, finger, bonesOut);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000397C File Offset: 0x00001B7C
		private unsafe static bool Hand_TryGetFingerBonesAsList(Hand hand, HandFinger finger, [NotNull] List<Bone> bonesOut)
		{
			if (bonesOut == null)
			{
				ThrowHelper.ThrowArgumentNullException(bonesOut, "bonesOut");
			}
			bool flag;
			try
			{
				fixed (Bone[] array = NoAllocHelpers.ExtractArrayFromList<Bone>(bonesOut))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, bonesOut.Count);
					flag = Hand.Hand_TryGetFingerBonesAsList_Injected(ref hand, finger, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Bone>(bonesOut);
			}
			return flag;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000039F0 File Offset: 0x00001BF0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Hand);
			return !flag && this.Equals((Hand)obj);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003A24 File Offset: 0x00001C24
		public bool Equals(Hand other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003A58 File Offset: 0x00001C58
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A8C File Offset: 0x00001C8C
		public static bool operator ==(Hand a, Hand b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public static bool operator !=(Hand a, Hand b)
		{
			return !(a == b);
		}

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Hand_TryGetRootBone_Injected([In] ref Hand hand, out Bone boneOut);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Hand_TryGetFingerBonesAsList_Injected([In] ref Hand hand, HandFinger finger, ref BlittableListWrapper bonesOut);

		// Token: 0x040000A3 RID: 163
		private ulong m_DeviceId;

		// Token: 0x040000A4 RID: 164
		private uint m_FeatureIndex;
	}
}
