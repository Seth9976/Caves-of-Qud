using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000066 RID: 102
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public static class AnimationStreamHandleUtility
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x0000B02C File Offset: 0x0000922C
		public static void WriteInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num, useMask);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000B06C File Offset: 0x0000926C
		public static void WriteFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num, useMask);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000B0AC File Offset: 0x000092AC
		public static void ReadInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000B0EC File Offset: 0x000092EC
		public static void ReadFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num);
			}
		}

		// Token: 0x060005EB RID: 1515
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count);

		// Token: 0x060005EC RID: 1516
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count);

		// Token: 0x060005ED RID: 1517
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count, bool useMask);

		// Token: 0x060005EE RID: 1518
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count, bool useMask);
	}
}
