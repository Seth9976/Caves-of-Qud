using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000070B8 File Offset: 0x000052B8
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarInternal(go, humanDescription);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000070E4 File Offset: 0x000052E4
		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return Unmarshal.UnmarshalUnityObject<Avatar>(AvatarBuilder.BuildHumanAvatarInternal_Injected(Object.MarshalledUnityObject.Marshal<GameObject>(go), ref humanDescription));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00007104 File Offset: 0x00005304
		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		public unsafe static Avatar BuildGenericAvatar([NotNull] GameObject go, [NotNull] string rootMotionTransformName)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			if (rootMotionTransformName == null)
			{
				ThrowHelper.ThrowArgumentNullException(rootMotionTransformName, "rootMotionTransformName");
			}
			Avatar avatar;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(go, "go");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rootMotionTransformName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rootMotionTransformName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AvatarBuilder.BuildGenericAvatar_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr2;
				avatar = Unmarshal.UnmarshalUnityObject<Avatar>(intPtr2);
				char* ptr = null;
			}
			return avatar;
		}

		// Token: 0x06000337 RID: 823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BuildHumanAvatarInternal_Injected(IntPtr go, [In] ref HumanDescription humanDescription);

		// Token: 0x06000338 RID: 824
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BuildGenericAvatar_Injected(IntPtr go, ref ManagedSpanWrapper rootMotionTransformName);
	}
}
