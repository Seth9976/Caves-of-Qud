using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Animations
{
	// Token: 0x02000074 RID: 116
	[NativeHeader("Modules/Animation/ScriptBindings/GenericBinding.bindings.h")]
	[StaticAccessor("UnityEngine::Animation::GenericBindingUtility", StaticAccessorType.DoubleColon)]
	public static class GenericBindingUtility
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0000D178 File Offset: 0x0000B378
		public static bool CreateGenericBinding(Object targetObject, string property, GameObject root, bool isObjectReference, out GenericBinding genericBinding)
		{
			bool flag = targetObject == null;
			if (flag)
			{
				throw new ArgumentNullException("targetObject");
			}
			bool flag2 = typeof(Transform).IsAssignableFrom(targetObject.GetType());
			if (flag2)
			{
				throw new ArgumentException("Unsupported type for targetObject. Cannot create a generic binding from a Transform component.");
			}
			Component component = targetObject as Component;
			bool flag3 = component != null;
			bool flag4;
			if (flag3)
			{
				flag4 = GenericBindingUtility.CreateGenericBindingForComponent(component, property, root, isObjectReference, out genericBinding);
			}
			else
			{
				GameObject gameObject = targetObject as GameObject;
				bool flag5 = gameObject != null;
				if (!flag5)
				{
					throw new ArgumentException(string.Format("Type {0} for {1} is unsupported. Expecting either a GameObject or a Component", targetObject.GetType(), "targetObject"));
				}
				flag4 = GenericBindingUtility.CreateGenericBindingForGameObject(gameObject, property, root, out genericBinding);
			}
			return flag4;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000D220 File Offset: 0x0000B420
		[NativeMethod(IsThreadSafe = false)]
		private unsafe static bool CreateGenericBindingForGameObject([NotNull] GameObject gameObject, string property, [NotNull] GameObject root, out GenericBinding genericBinding)
		{
			if (gameObject == null)
			{
				ThrowHelper.ThrowArgumentNullException(gameObject, "gameObject");
			}
			if (root == null)
			{
				ThrowHelper.ThrowArgumentNullException(root, "root");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(gameObject);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(gameObject, "gameObject");
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
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(root);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(root, "root");
				}
				flag = GenericBindingUtility.CreateGenericBindingForGameObject_Injected(intPtr, ref managedSpanWrapper, intPtr2, out genericBinding);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		[NativeMethod(IsThreadSafe = false)]
		private unsafe static bool CreateGenericBindingForComponent([NotNull] Component component, string property, [NotNull] GameObject root, bool isObjectReference, out GenericBinding genericBinding)
		{
			if (component == null)
			{
				ThrowHelper.ThrowArgumentNullException(component, "component");
			}
			if (root == null)
			{
				ThrowHelper.ThrowArgumentNullException(root, "root");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(component);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(component, "component");
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
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(root);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(root, "root");
				}
				flag = GenericBindingUtility.CreateGenericBindingForComponent_Injected(intPtr, ref managedSpanWrapper, intPtr2, isObjectReference, out genericBinding);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000D364 File Offset: 0x0000B564
		[NativeMethod(IsThreadSafe = false)]
		public static GenericBinding[] GetAnimatableBindings([NotNull] GameObject targetObject, [NotNull] GameObject root)
		{
			if (targetObject == null)
			{
				ThrowHelper.ThrowArgumentNullException(targetObject, "targetObject");
			}
			if (root == null)
			{
				ThrowHelper.ThrowArgumentNullException(root, "root");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(targetObject);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(targetObject, "targetObject");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(root);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(root, "root");
			}
			return GenericBindingUtility.GetAnimatableBindings_Injected(intPtr, intPtr2);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		[NativeMethod(IsThreadSafe = false)]
		public static GenericBinding[] GetCurveBindings([NotNull] AnimationClip clip)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(clip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			return GenericBindingUtility.GetCurveBindings_Injected(intPtr);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		public unsafe static void BindProperties(GameObject rootGameObject, NativeArray<GenericBinding> genericBindings, out NativeArray<BoundProperty> floatProperties, out NativeArray<BoundProperty> discreteProperties, Allocator allocator)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < genericBindings.Length; i++)
			{
				bool flag = genericBindings[i].typeID == 4;
				if (!flag)
				{
					bool flag2 = genericBindings[i].isDiscrete || genericBindings[i].isObjectReference;
					if (flag2)
					{
						num2++;
					}
					else
					{
						num++;
					}
				}
			}
			floatProperties = new NativeArray<BoundProperty>(num, allocator, NativeArrayOptions.ClearMemory);
			discreteProperties = new NativeArray<BoundProperty>(num2, allocator, NativeArrayOptions.ClearMemory);
			void* unsafePtr = genericBindings.GetUnsafePtr<GenericBinding>();
			void* unsafePtr2 = floatProperties.GetUnsafePtr<BoundProperty>();
			void* unsafePtr3 = discreteProperties.GetUnsafePtr<BoundProperty>();
			GenericBindingUtility.Internal_BindProperties(rootGameObject, unsafePtr, genericBindings.Length, unsafePtr2, unsafePtr3);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		[NativeMethod(IsThreadSafe = false)]
		internal unsafe static void Internal_BindProperties([NotNull] GameObject gameObject, void* genericBindings, int genericBindingsCount, void* floatProperties, void* discreteProperties)
		{
			if (gameObject == null)
			{
				ThrowHelper.ThrowArgumentNullException(gameObject, "gameObject");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(gameObject);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(gameObject, "gameObject");
			}
			GenericBindingUtility.Internal_BindProperties_Injected(intPtr, genericBindings, genericBindingsCount, floatProperties, discreteProperties);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000D50C File Offset: 0x0000B70C
		public unsafe static void UnbindProperties(NativeArray<BoundProperty> boundProperties)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			GenericBindingUtility.Internal_UnbindProperties(unsafePtr, boundProperties.Length);
		}

		// Token: 0x0600074F RID: 1871
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Internal_UnbindProperties(void* boundProperties, int boundPropertiesCount);

		// Token: 0x06000750 RID: 1872 RVA: 0x0000D530 File Offset: 0x0000B730
		public unsafe static void SetValues(NativeArray<BoundProperty> boundProperties, NativeArray<float> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<float>(values);
			GenericBindingUtility.SetFloatValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, values.Length);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000D564 File Offset: 0x0000B764
		public unsafe static void SetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> indices, NativeArray<float> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(indices);
			void* unsafeBufferPointerWithoutChecks2 = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<float>(values);
			GenericBindingUtility.SetScatterFloatValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, indices.Length, unsafeBufferPointerWithoutChecks2, values.Length);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public unsafe static void SetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(values);
			GenericBindingUtility.SetDiscreteValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, values.Length);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public unsafe static void SetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> indices, NativeArray<int> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(indices);
			void* unsafeBufferPointerWithoutChecks2 = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(values);
			GenericBindingUtility.SetScatterDiscreteValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, indices.Length, unsafeBufferPointerWithoutChecks2, values.Length);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000D620 File Offset: 0x0000B820
		public unsafe static void GetValues(NativeArray<BoundProperty> boundProperties, NativeArray<float> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<float>(values);
			GenericBindingUtility.GetFloatValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, values.Length);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000D654 File Offset: 0x0000B854
		public unsafe static void GetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> indices, NativeArray<float> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(indices);
			void* unsafeBufferPointerWithoutChecks2 = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<float>(values);
			GenericBindingUtility.GetScatterFloatValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, indices.Length, unsafeBufferPointerWithoutChecks2, values.Length);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000D698 File Offset: 0x0000B898
		public unsafe static void GetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(values);
			GenericBindingUtility.GetDiscreteValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, values.Length);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public unsafe static void GetValues(NativeArray<BoundProperty> boundProperties, NativeArray<int> indices, NativeArray<int> values)
		{
			void* unsafePtr = boundProperties.GetUnsafePtr<BoundProperty>();
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(indices);
			void* unsafeBufferPointerWithoutChecks2 = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<int>(values);
			GenericBindingUtility.GetScatterDiscreteValues(unsafePtr, boundProperties.Length, unsafeBufferPointerWithoutChecks, indices.Length, unsafeBufferPointerWithoutChecks2, values.Length);
		}

		// Token: 0x06000758 RID: 1880
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetFloatValues(void* boundProperties, int boundPropertiesCount, void* values, int valuesCount);

		// Token: 0x06000759 RID: 1881
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetScatterFloatValues(void* boundProperties, int boundPropertiesCount, void* indices, int indicesCount, void* values, int valuesCount);

		// Token: 0x0600075A RID: 1882
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetDiscreteValues(void* boundProperties, int boundPropertiesCount, void* values, int valuesCount);

		// Token: 0x0600075B RID: 1883
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetScatterDiscreteValues(void* boundProperties, int boundPropertiesCount, void* indices, int indicesCount, void* values, int valuesCount);

		// Token: 0x0600075C RID: 1884
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void GetFloatValues(void* boundProperties, int boundPropertiesCount, void* values, int valuesCount);

		// Token: 0x0600075D RID: 1885
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void GetScatterFloatValues(void* boundProperties, int boundPropertiesCount, void* indices, int indicesCount, void* values, int valuesCount);

		// Token: 0x0600075E RID: 1886
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void GetDiscreteValues(void* boundProperties, int boundPropertiesCount, void* values, int valuesCount);

		// Token: 0x0600075F RID: 1887
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void GetScatterDiscreteValues(void* boundProperties, int boundPropertiesCount, void* indices, int indicesCount, void* values, int valuesCount);

		// Token: 0x06000760 RID: 1888 RVA: 0x0000D710 File Offset: 0x0000B910
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void ValidateIsCreated<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> array) where T : struct, ValueType
		{
			bool flag = !array.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray of " + typeof(T).Name + " is not created.");
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000D750 File Offset: 0x0000B950
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void ValidateIndicesAreInRange(NativeArray<int> indices, int maxValue)
		{
			for (int i = 0; i < indices.Length; i++)
			{
				bool flag = indices[i] < 0 || indices[i] >= maxValue;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("NativeArray of indices contain element out of range at index '{0}': value '{1}' is not in the range 0 to {2}.", i, indices[i], maxValue));
				}
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void ValidateLengthMatch<[global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(NativeArray<T1> array1, NativeArray<T2> array2) where T1 : struct, ValueType where T2 : struct, ValueType
		{
			bool flag = array1.Length != array2.Length;
			if (flag)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Length must be equals for NativeArray<",
					typeof(T1).Name,
					"> and NativeArray<",
					typeof(T2).Name,
					">."
				}));
			}
		}

		// Token: 0x06000763 RID: 1891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateGenericBindingForGameObject_Injected(IntPtr gameObject, ref ManagedSpanWrapper property, IntPtr root, out GenericBinding genericBinding);

		// Token: 0x06000764 RID: 1892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateGenericBindingForComponent_Injected(IntPtr component, ref ManagedSpanWrapper property, IntPtr root, bool isObjectReference, out GenericBinding genericBinding);

		// Token: 0x06000765 RID: 1893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GenericBinding[] GetAnimatableBindings_Injected(IntPtr targetObject, IntPtr root);

		// Token: 0x06000766 RID: 1894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GenericBinding[] GetCurveBindings_Injected(IntPtr clip);

		// Token: 0x06000767 RID: 1895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_BindProperties_Injected(IntPtr gameObject, void* genericBindings, int genericBindingsCount, void* floatProperties, void* discreteProperties);
	}
}
