using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("XRScriptingClasses.h")]
	public struct Bone : IEquatable<Bone>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003D08 File Offset: 0x00001F08
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003D20 File Offset: 0x00001F20
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003D38 File Offset: 0x00001F38
		public bool TryGetPosition(out Vector3 position)
		{
			return Bone.Bone_TryGetPosition(this, out position);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D58 File Offset: 0x00001F58
		private static bool Bone_TryGetPosition(Bone bone, out Vector3 position)
		{
			return Bone.Bone_TryGetPosition_Injected(ref bone, out position);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003D70 File Offset: 0x00001F70
		public bool TryGetRotation(out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation(this, out rotation);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D90 File Offset: 0x00001F90
		private static bool Bone_TryGetRotation(Bone bone, out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation_Injected(ref bone, out rotation);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public bool TryGetParentBone(out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone(this, out parentBone);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003DC8 File Offset: 0x00001FC8
		private static bool Bone_TryGetParentBone(Bone bone, out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone_Injected(ref bone, out parentBone);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public bool TryGetChildBones(List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones(this, childBones);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E00 File Offset: 0x00002000
		private unsafe static bool Bone_TryGetChildBones(Bone bone, [NotNull] List<Bone> childBones)
		{
			if (childBones == null)
			{
				ThrowHelper.ThrowArgumentNullException(childBones, "childBones");
			}
			bool flag;
			try
			{
				fixed (Bone[] array = NoAllocHelpers.ExtractArrayFromList<Bone>(childBones))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, childBones.Count);
					flag = Bone.Bone_TryGetChildBones_Injected(ref bone, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Bone>(childBones);
			}
			return flag;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E74 File Offset: 0x00002074
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Bone);
			return !flag && this.Equals((Bone)obj);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EA8 File Offset: 0x000020A8
		public bool Equals(Bone other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EDC File Offset: 0x000020DC
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F10 File Offset: 0x00002110
		public static bool operator ==(Bone a, Bone b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F2C File Offset: 0x0000212C
		public static bool operator !=(Bone a, Bone b)
		{
			return !(a == b);
		}

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetPosition_Injected([In] ref Bone bone, out Vector3 position);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetRotation_Injected([In] ref Bone bone, out Quaternion rotation);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetParentBone_Injected([In] ref Bone bone, out Bone parentBone);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetChildBones_Injected([In] ref Bone bone, ref BlittableListWrapper childBones);

		// Token: 0x040000AA RID: 170
		private ulong m_DeviceId;

		// Token: 0x040000AB RID: 171
		private uint m_FeatureIndex;
	}
}
