using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003F RID: 63
	[NativeHeader("Modules/Animation/HumanTrait.h")]
	public class HumanTrait
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000373 RID: 883
		public static extern int MuscleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000374 RID: 884
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexFromMono(int humanId);

		// Token: 0x06000375 RID: 885
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexToMono(int boneIndex);

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000376 RID: 886
		public static extern string[] MuscleName
		{
			[NativeMethod("GetMuscleNames")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000377 RID: 887
		public static extern int BoneCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000378 RID: 888
		public static extern string[] BoneName
		{
			[NativeMethod("MonoBoneNames")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00007A7C File Offset: 0x00005C7C
		public static int MuscleFromBone(int i, int dofIndex)
		{
			return HumanTrait.Internal_MuscleFromBone(HumanTrait.GetBoneIndexFromMono(i), dofIndex);
		}

		// Token: 0x0600037A RID: 890
		[NativeMethod("MuscleFromBone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_MuscleFromBone(int i, int dofIndex);

		// Token: 0x0600037B RID: 891 RVA: 0x00007A9C File Offset: 0x00005C9C
		public static int BoneFromMuscle(int i)
		{
			return HumanTrait.GetBoneIndexToMono(HumanTrait.Internal_BoneFromMuscle(i));
		}

		// Token: 0x0600037C RID: 892
		[NativeMethod("BoneFromMuscle")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_BoneFromMuscle(int i);

		// Token: 0x0600037D RID: 893 RVA: 0x00007ABC File Offset: 0x00005CBC
		public static bool RequiredBone(int i)
		{
			return HumanTrait.Internal_RequiredBone(HumanTrait.GetBoneIndexFromMono(i));
		}

		// Token: 0x0600037E RID: 894
		[NativeMethod("RequiredBone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RequiredBone(int i);

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600037F RID: 895
		public static extern int RequiredBoneCount
		{
			[NativeMethod("RequiredBoneCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000380 RID: 896
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMin(int i);

		// Token: 0x06000381 RID: 897
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMax(int i);

		// Token: 0x06000382 RID: 898 RVA: 0x00007ADC File Offset: 0x00005CDC
		public static float GetBoneDefaultHierarchyMass(int i)
		{
			return HumanTrait.Internal_GetBoneHierarchyMass(HumanTrait.GetBoneIndexFromMono(i));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00007AFC File Offset: 0x00005CFC
		public static int GetParentBone(int i)
		{
			int num = HumanTrait.Internal_GetParent(HumanTrait.GetBoneIndexFromMono(i));
			return (num != -1) ? HumanTrait.GetBoneIndexToMono(num) : (-1);
		}

		// Token: 0x06000384 RID: 900
		[NativeMethod("GetBoneHierarchyMass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetBoneHierarchyMass(int i);

		// Token: 0x06000385 RID: 901
		[NativeMethod("GetParent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetParent(int i);
	}
}
