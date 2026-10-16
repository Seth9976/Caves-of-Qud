using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000069 RID: 105
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[StaticAccessor("AnimatorJobExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorJobExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	public static class AnimatorJobExtensions
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0000BE11 File Offset: 0x0000A011
		public static void AddJobDependency(this Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency(animator, jobHandle);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public static TransformStreamHandle BindStreamTransform(this Animator animator, Transform transform)
		{
			TransformStreamHandle transformStreamHandle = default(TransformStreamHandle);
			AnimatorJobExtensions.InternalBindStreamTransform(animator, transform, out transformStreamHandle);
			return transformStreamHandle;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000BE44 File Offset: 0x0000A044
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindStreamProperty(transform, type, property, false);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000BE60 File Offset: 0x0000A060
		public static PropertyStreamHandle BindCustomStreamProperty(this Animator animator, string property, CustomStreamPropertyType type)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindCustomStreamProperty(animator, property, type, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000BE88 File Offset: 0x0000A088
		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindStreamProperty(animator, transform, type, property, isObjectReference, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public static TransformSceneHandle BindSceneTransform(this Animator animator, Transform transform)
		{
			TransformSceneHandle transformSceneHandle = default(TransformSceneHandle);
			AnimatorJobExtensions.InternalBindSceneTransform(animator, transform, out transformSceneHandle);
			return transformSceneHandle;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindSceneProperty(transform, type, property, false);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertySceneHandle propertySceneHandle = default(PropertySceneHandle);
			AnimatorJobExtensions.InternalBindSceneProperty(animator, transform, type, property, isObjectReference, out propertySceneHandle);
			return propertySceneHandle;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000BF24 File Offset: 0x0000A124
		public static bool OpenAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			return AnimatorJobExtensions.InternalOpenAnimationStream(animator, ref stream);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000BF3D File Offset: 0x0000A13D
		public static void CloseAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			AnimatorJobExtensions.InternalCloseAnimationStream(animator, ref stream);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000BF48 File Offset: 0x0000A148
		public static void ResolveAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllStreamHandles(animator);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000BF52 File Offset: 0x0000A152
		public static void ResolveAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllSceneHandles(animator);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000BF5C File Offset: 0x0000A15C
		internal static void UnbindAllHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllStreamHandles(animator);
			AnimatorJobExtensions.InternalUnbindAllSceneHandles(animator);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000BF6D File Offset: 0x0000A16D
		public static void UnbindAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllStreamHandles(animator);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000BF77 File Offset: 0x0000A177
		public static void UnbindAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllSceneHandles(animator);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000BF84 File Offset: 0x0000A184
		private static void InternalAddJobDependency([NotNull] Animator animator, JobHandle jobHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalAddJobDependency_Injected(intPtr, ref jobHandle);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private static void InternalBindStreamTransform([NotNull] Animator animator, [NotNull] Transform transform, out TransformStreamHandle transformStreamHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			AnimatorJobExtensions.InternalBindStreamTransform_Injected(intPtr, intPtr2, out transformStreamHandle);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000C018 File Offset: 0x0000A218
		private unsafe static void InternalBindStreamProperty([NotNull] Animator animator, [NotNull] Transform transform, [NotNull] Type type, [NotNull] string property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			if (property == null)
			{
				ThrowHelper.ThrowArgumentNullException(property, "property");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(animator, "animator");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(transform, "transform");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(property, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = property.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AnimatorJobExtensions.InternalBindStreamProperty_Injected(intPtr, intPtr2, type, ref managedSpanWrapper, isObjectReference, out propertyStreamHandle);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		private unsafe static void InternalBindCustomStreamProperty([NotNull] Animator animator, [NotNull] string property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			if (property == null)
			{
				ThrowHelper.ThrowArgumentNullException(property, "property");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(animator, "animator");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(property, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = property.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AnimatorJobExtensions.InternalBindCustomStreamProperty_Injected(intPtr, ref managedSpanWrapper, propertyType, out propertyStreamHandle);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000C164 File Offset: 0x0000A364
		private static void InternalBindSceneTransform([NotNull] Animator animator, [NotNull] Transform transform, out TransformSceneHandle transformSceneHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			AnimatorJobExtensions.InternalBindSceneTransform_Injected(intPtr, intPtr2, out transformSceneHandle);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		private unsafe static void InternalBindSceneProperty([NotNull] Animator animator, [NotNull] Transform transform, [NotNull] Type type, [NotNull] string property, bool isObjectReference, out PropertySceneHandle propertySceneHandle)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			if (property == null)
			{
				ThrowHelper.ThrowArgumentNullException(property, "property");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(animator, "animator");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(transform, "transform");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(property, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = property.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AnimatorJobExtensions.InternalBindSceneProperty_Injected(intPtr, intPtr2, type, ref managedSpanWrapper, isObjectReference, out propertySceneHandle);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000C280 File Offset: 0x0000A480
		private static bool InternalOpenAnimationStream([NotNull] Animator animator, ref AnimationStream stream)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			return AnimatorJobExtensions.InternalOpenAnimationStream_Injected(intPtr, ref stream);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		private static void InternalCloseAnimationStream([NotNull] Animator animator, ref AnimationStream stream)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalCloseAnimationStream_Injected(intPtr, ref stream);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		private static void InternalResolveAllStreamHandles([NotNull] Animator animator)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalResolveAllStreamHandles_Injected(intPtr);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000C328 File Offset: 0x0000A528
		private static void InternalResolveAllSceneHandles([NotNull] Animator animator)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalResolveAllSceneHandles_Injected(intPtr);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000C360 File Offset: 0x0000A560
		private static void InternalUnbindAllStreamHandles([NotNull] Animator animator)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalUnbindAllStreamHandles_Injected(intPtr);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000C398 File Offset: 0x0000A598
		private static void InternalUnbindAllSceneHandles([NotNull] Animator animator)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimatorJobExtensions.InternalUnbindAllSceneHandles_Injected(intPtr);
		}

		// Token: 0x0600068F RID: 1679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalAddJobDependency_Injected(IntPtr animator, [In] ref JobHandle jobHandle);

		// Token: 0x06000690 RID: 1680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamTransform_Injected(IntPtr animator, IntPtr transform, out TransformStreamHandle transformStreamHandle);

		// Token: 0x06000691 RID: 1681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamProperty_Injected(IntPtr animator, IntPtr transform, Type type, ref ManagedSpanWrapper property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x06000692 RID: 1682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindCustomStreamProperty_Injected(IntPtr animator, ref ManagedSpanWrapper property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle);

		// Token: 0x06000693 RID: 1683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneTransform_Injected(IntPtr animator, IntPtr transform, out TransformSceneHandle transformSceneHandle);

		// Token: 0x06000694 RID: 1684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneProperty_Injected(IntPtr animator, IntPtr transform, Type type, ref ManagedSpanWrapper property, bool isObjectReference, out PropertySceneHandle propertySceneHandle);

		// Token: 0x06000695 RID: 1685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalOpenAnimationStream_Injected(IntPtr animator, ref AnimationStream stream);

		// Token: 0x06000696 RID: 1686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCloseAnimationStream_Injected(IntPtr animator, ref AnimationStream stream);

		// Token: 0x06000697 RID: 1687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllStreamHandles_Injected(IntPtr animator);

		// Token: 0x06000698 RID: 1688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllSceneHandles_Injected(IntPtr animator);

		// Token: 0x06000699 RID: 1689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllStreamHandles_Injected(IntPtr animator);

		// Token: 0x0600069A RID: 1690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllSceneHandles_Injected(IntPtr animator);
	}
}
