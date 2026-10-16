using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200022D RID: 557
	[NativeHeader("Editor/Src/Properties/DrivenPropertyManager.h")]
	internal class DrivenPropertyManager
	{
		// Token: 0x06001E4C RID: 7756 RVA: 0x0003A55C File Offset: 0x0003875C
		[Conditional("UNITY_EDITOR")]
		public static void RegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.RegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0003A568 File Offset: 0x00038768
		[Conditional("UNITY_EDITOR")]
		public static void TryRegisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.TryRegisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0003A574 File Offset: 0x00038774
		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperty(Object driver, Object target, string propertyPath)
		{
			DrivenPropertyManager.UnregisterPropertyPartial(driver, target, propertyPath);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0003A580 File Offset: 0x00038780
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperties([NotNull] Object driver)
		{
			if (driver == null)
			{
				ThrowHelper.ThrowArgumentNullException(driver, "driver");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(driver);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(driver, "driver");
			}
			DrivenPropertyManager.UnregisterProperties_Injected(intPtr);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0003A5B8 File Offset: 0x000387B8
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		private unsafe static void RegisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath)
		{
			if (driver == null)
			{
				ThrowHelper.ThrowArgumentNullException(driver, "driver");
			}
			if (target == null)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			if (propertyPath == null)
			{
				ThrowHelper.ThrowArgumentNullException(propertyPath, "propertyPath");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(driver);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(driver, "driver");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Object>(target);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(target, "target");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				DrivenPropertyManager.RegisterPropertyPartial_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0003A664 File Offset: 0x00038864
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		private unsafe static void TryRegisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath)
		{
			if (driver == null)
			{
				ThrowHelper.ThrowArgumentNullException(driver, "driver");
			}
			if (target == null)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			if (propertyPath == null)
			{
				ThrowHelper.ThrowArgumentNullException(propertyPath, "propertyPath");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(driver);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(driver, "driver");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Object>(target);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(target, "target");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				DrivenPropertyManager.TryRegisterPropertyPartial_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0003A710 File Offset: 0x00038910
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		private unsafe static void UnregisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath)
		{
			if (driver == null)
			{
				ThrowHelper.ThrowArgumentNullException(driver, "driver");
			}
			if (target == null)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			if (propertyPath == null)
			{
				ThrowHelper.ThrowArgumentNullException(propertyPath, "propertyPath");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(driver);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(driver, "driver");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Object>(target);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(target, "target");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				DrivenPropertyManager.UnregisterPropertyPartial_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E54 RID: 7764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterProperties_Injected(IntPtr driver);

		// Token: 0x06001E55 RID: 7765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterPropertyPartial_Injected(IntPtr driver, IntPtr target, ref ManagedSpanWrapper propertyPath);

		// Token: 0x06001E56 RID: 7766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryRegisterPropertyPartial_Injected(IntPtr driver, IntPtr target, ref ManagedSpanWrapper propertyPath);

		// Token: 0x06001E57 RID: 7767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterPropertyPartial_Injected(IntPtr driver, IntPtr target, ref ManagedSpanWrapper propertyPath);
	}
}
