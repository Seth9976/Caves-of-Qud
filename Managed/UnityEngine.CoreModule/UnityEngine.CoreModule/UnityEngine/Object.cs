using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020002BD RID: 701
	[NativeHeader("Runtime/GameCode/CloneObject.h")]
	[NativeHeader("Runtime/Export/Scripting/UnityEngineObject.bindings.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x000421D0 File Offset: 0x000403D0
		[SecuritySafeCritical]
		public unsafe int GetInstanceID()
		{
			bool flag = this.m_CachedPtr == IntPtr.Zero;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = *(int*)((byte*)(void*)this.m_CachedPtr + Object.OffsetOfInstanceIDInCPlusPlusObject);
			}
			return num;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00042210 File Offset: 0x00040410
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00042228 File Offset: 0x00040428
		public override bool Equals(object other)
		{
			Object @object = other as Object;
			bool flag = @object == null && other != null && !(other is Object);
			return !flag && Object.CompareBaseObjects(this, @object);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0004226C File Offset: 0x0004046C
		public static implicit operator bool([MaybeNullWhen(false)] [NotNullWhen(true)] Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00042288 File Offset: 0x00040488
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = lhs == null;
			bool flag2 = rhs == null;
			bool flag3 = flag2 && flag;
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					flag4 = !Object.IsNativeObjectAlive(lhs);
				}
				else
				{
					bool flag6 = flag;
					if (flag6)
					{
						flag4 = !Object.IsNativeObjectAlive(rhs);
					}
					else
					{
						flag4 = lhs == rhs;
					}
				}
			}
			return flag4;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000422DC File Offset: 0x000404DC
		private void EnsureRunningOnMainThread()
		{
			bool flag = !Object.CurrentThreadIsMainThread();
			if (flag)
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00042304 File Offset: 0x00040504
		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00042328 File Offset: 0x00040528
		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x00042340 File Offset: 0x00040540
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x00042358 File Offset: 0x00040558
		public string name
		{
			get
			{
				return this.GetName();
			}
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00042364 File Offset: 0x00040564
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original) where T : Object
		{
			return Object.InstantiateAsync<T>(original, new InstantiateParameters
			{
				worldSpace = true
			}, default(CancellationToken));
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00042398 File Offset: 0x00040598
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent) where T : Object
		{
			return Object.InstantiateAsync<T>(original, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, default(CancellationToken));
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000423D4 File Offset: 0x000405D4
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, position, rotation, new InstantiateParameters
			{
				worldSpace = true
			}, default(CancellationToken));
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00042408 File Offset: 0x00040608
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, position, rotation, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, default(CancellationToken));
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00042444 File Offset: 0x00040644
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, new InstantiateParameters
			{
				worldSpace = true
			}, default(CancellationToken));
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00042478 File Offset: 0x00040678
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, default(CancellationToken));
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000424B4 File Offset: 0x000406B4
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, position, rotation, new InstantiateParameters
			{
				worldSpace = true
			}, default(CancellationToken));
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000424EC File Offset: 0x000406EC
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, positions, rotations, new InstantiateParameters
			{
				worldSpace = true
			}, default(CancellationToken));
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00042524 File Offset: 0x00040724
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, position, rotation, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, default(CancellationToken));
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00042564 File Offset: 0x00040764
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, CancellationToken cancellationToken) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, position, rotation, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, cancellationToken);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0004259C File Offset: 0x0004079C
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, positions, rotations, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, default(CancellationToken));
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000425DC File Offset: 0x000407DC
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, CancellationToken cancellationToken) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, positions, rotations, new InstantiateParameters
			{
				worldSpace = true,
				parent = parent
			}, cancellationToken);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00042614 File Offset: 0x00040814
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, InstantiateParameters parameters, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, parameters, cancellationToken);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00042630 File Offset: 0x00040830
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, InstantiateParameters parameters, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Quaternion>.Empty, parameters, cancellationToken);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00042658 File Offset: 0x00040858
		public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Vector3 position, Quaternion rotation, InstantiateParameters parameters, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
		{
			return Object.InstantiateAsync<T>(original, 1, position, rotation, parameters, cancellationToken);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00042678 File Offset: 0x00040878
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation, InstantiateParameters parameters, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
		{
			return Object.InstantiateAsync<T>(original, count, new ReadOnlySpan<Vector3>((void*)(&position), 1), new ReadOnlySpan<Quaternion>((void*)(&rotation), 1), parameters, cancellationToken);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000426A8 File Offset: 0x000408A8
		[MethodImpl((MethodImplOptions)768)]
		public unsafe static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, InstantiateParameters parameters, CancellationToken cancellationToken = default(CancellationToken)) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Cannot call instantiate multiple with count less or equal to zero");
			}
			fixed (Vector3* pinnableReference = positions.GetPinnableReference())
			{
				Vector3* ptr = pinnableReference;
				fixed (Quaternion* pinnableReference2 = rotations.GetPinnableReference())
				{
					Quaternion* ptr2 = pinnableReference2;
					return new AsyncInstantiateOperation<T>(Object.Internal_InstantiateAsyncWithParams(original, count, parameters, (IntPtr)((void*)ptr), positions.Length, (IntPtr)((void*)ptr2), rotations.Length), cancellationToken);
				}
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00042730 File Offset: 0x00040930
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = original is ScriptableObject;
			if (flag)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object @object = Object.Internal_InstantiateSingle(original, position, rotation);
			bool flag2 = @object == null;
			if (flag2)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00042788 File Offset: 0x00040988
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original, position, rotation);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object object2 = Object.Internal_InstantiateSingleWithParent(original, parent, position, rotation);
				bool flag2 = object2 == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = object2;
			}
			return @object;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000427E0 File Offset: 0x000409E0
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Object.Internal_CloneSingle(original);
			bool flag = @object == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0004281C File Offset: 0x00040A1C
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Scene scene)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Object.Internal_CloneSingleWithScene(original, scene);
			bool flag = @object == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0004285C File Offset: 0x00040A5C
		public static T Instantiate<T>(T original, InstantiateParameters parameters) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_CloneSingleWithParams(original, parameters));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000428B0 File Offset: 0x00040AB0
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, InstantiateParameters parameters) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_InstantiateSingleWithParams(original, position, rotation, parameters));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00042904 File Offset: 0x00040B04
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Object.Instantiate(original, parent, false);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00042920 File Offset: 0x00040B20
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object object2 = Object.Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
				bool flag2 = object2 == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = object2;
			}
			return @object;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00042974 File Offset: 0x00040B74
		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_CloneSingle(original));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000429C4 File Offset: 0x00040BC4
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation));
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000429E8 File Offset: 0x00040BE8
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation, parent));
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00042A10 File Offset: 0x00040C10
		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(original, parent, false);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00042A2C File Offset: 0x00040C2C
		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)((object)Object.Instantiate(original, parent, worldPositionStays));
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00042A50 File Offset: 0x00040C50
		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		public static void Destroy(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), t);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00042A6C File Offset: 0x00040C6C
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00042A88 File Offset: 0x00040C88
		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		public static void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets)
		{
			Object.DestroyImmediate_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), allowDestroyingAssets);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00042AA4 File Offset: 0x00040CA4
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool flag = false;
			Object.DestroyImmediate(obj, flag);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00042ABC File Offset: 0x00040CBC
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID, but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static Object[] FindObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type, false);
		}

		// Token: 0x06002213 RID: 8723
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type, bool includeInactive);

		// Token: 0x06002214 RID: 8724 RVA: 0x00042AD8 File Offset: 0x00040CD8
		public static Object[] FindObjectsByType(Type type, FindObjectsSortMode sortMode)
		{
			return Object.FindObjectsByType(type, FindObjectsInactive.Exclude, sortMode);
		}

		// Token: 0x06002215 RID: 8725
		[FreeFunction("UnityEngineObjectBindings::FindObjectsByType")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsByType(Type type, FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode);

		// Token: 0x06002216 RID: 8726 RVA: 0x00042AF4 File Offset: 0x00040CF4
		[FreeFunction("GetSceneManager().DontDestroyOnLoad", ThrowsException = true)]
		public static void DontDestroyOnLoad([NotNull] Object target)
		{
			if (target == null)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(target);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(target, "target");
			}
			Object.DontDestroyOnLoad_Injected(intPtr);
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00042B2C File Offset: 0x00040D2C
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x00042B50 File Offset: 0x00040D50
		public HideFlags hideFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Object.get_hideFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Object.set_hideFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00042B73 File Offset: 0x00040D73
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy(obj, t);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00042B80 File Offset: 0x00040D80
		[Obsolete("use Object.Destroy instead.")]
		[ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00042B9C File Offset: 0x00040D9C
		[Obsolete("Object.FindSceneObjectsOfType has been deprecated, Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindSceneObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static Object[] FindSceneObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type);
		}

		// Token: 0x0600221C RID: 8732
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x0600221D RID: 8733 RVA: 0x00042BB4 File Offset: 0x00040DB4
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), false));
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00042BDC File Offset: 0x00040DDC
		public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), FindObjectsInactive.Exclude, sortMode));
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00042C04 File Offset: 0x00040E04
		[Obsolete("Object.FindObjectsOfType has been deprecated. Use Object.FindObjectsByType instead which lets you decide whether you need the results sorted or not.  FindObjectsOfType sorts the results by InstanceID but if you do not need this using FindObjectSortMode.None is considerably faster.", false)]
		public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), includeInactive));
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00042C2C File Offset: 0x00040E2C
		public static T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive, FindObjectsSortMode sortMode) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsByType(typeof(T), findObjectsInactive, sortMode));
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00042C54 File Offset: 0x00040E54
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), false));
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00042C7C File Offset: 0x00040E7C
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		public static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), includeInactive));
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00042CA4 File Offset: 0x00040EA4
		public static T FindFirstObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00042CCC File Offset: 0x00040ECC
		public static T FindAnyObjectByType<T>() where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), FindObjectsInactive.Exclude));
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00042CF4 File Offset: 0x00040EF4
		public static T FindFirstObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindFirstObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00042D1C File Offset: 0x00040F1C
		public static T FindAnyObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
		{
			return (T)((object)Object.FindAnyObjectByType(typeof(T), findObjectsInactive));
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00042D44 File Offset: 0x00040F44
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00042D5C File Offset: 0x00040F5C
		private static void CheckNullArgument(object arg, string message)
		{
			bool flag = arg == null;
			if (flag)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00042D7C File Offset: 0x00040F7C
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type, false);
			bool flag = array.Length != 0;
			Object @object;
			if (flag)
			{
				@object = array[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00042DA8 File Offset: 0x00040FA8
		public static Object FindFirstObjectByType(Type type)
		{
			Object[] array = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00042DD0 File Offset: 0x00040FD0
		public static Object FindAnyObjectByType(Type type)
		{
			Object[] array = Object.FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00042DF8 File Offset: 0x00040FF8
		[Obsolete("Object.FindObjectOfType has been deprecated. Use Object.FindFirstObjectByType instead or if finding any instance is acceptable the faster Object.FindAnyObjectByType", false)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type, bool includeInactive)
		{
			Object[] array = Object.FindObjectsOfType(type, includeInactive);
			bool flag = array.Length != 0;
			Object @object;
			if (flag)
			{
				@object = array[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00042E24 File Offset: 0x00041024
		public static Object FindFirstObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] array = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.InstanceID);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00042E4C File Offset: 0x0004104C
		public static Object FindAnyObjectByType(Type type, FindObjectsInactive findObjectsInactive)
		{
			Object[] array = Object.FindObjectsByType(type, findObjectsInactive, FindObjectsSortMode.None);
			return (array.Length != 0) ? array[0] : null;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00042E74 File Offset: 0x00041074
		public override string ToString()
		{
			return Object.ToString(this);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00042E8C File Offset: 0x0004108C
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00042EA8 File Offset: 0x000410A8
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06002232 RID: 8754
		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x06002233 RID: 8755
		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CurrentThreadIsMainThread();

		// Token: 0x06002234 RID: 8756 RVA: 0x00042EC4 File Offset: 0x000410C4
		[NativeMethod(Name = "CloneObject", IsFreeFunction = true, ThrowsException = true)]
		private static Object Internal_CloneSingle([NotNull] Object data)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingle_Injected(intPtr));
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00042F00 File Offset: 0x00041100
		[FreeFunction("CloneObjectToScene")]
		private static Object Internal_CloneSingleWithScene([NotNull] Object data, Scene scene)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingleWithScene_Injected(intPtr, ref scene));
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00042F40 File Offset: 0x00041140
		[FreeFunction("CloneObjectWithParams")]
		private static Object Internal_CloneSingleWithParams([NotNull] Object data, InstantiateParameters parameters)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingleWithParams_Injected(intPtr, ref parameters));
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00042F80 File Offset: 0x00041180
		[FreeFunction("InstantiateObjectWithParams")]
		private static Object Internal_InstantiateSingleWithParams([NotNull] Object data, Vector3 position, Quaternion rotation, InstantiateParameters parameters)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_InstantiateSingleWithParams_Injected(intPtr, ref position, ref rotation, ref parameters));
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00042FC4 File Offset: 0x000411C4
		[FreeFunction("CloneObject")]
		private static Object Internal_CloneSingleWithParent([NotNull] Object data, [NotNull] Transform parent, bool worldPositionStays)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_CloneSingleWithParent_Injected(intPtr, intPtr2, worldPositionStays));
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00043024 File Offset: 0x00041224
		[FreeFunction("InstantiateAsyncObjects")]
		private static IntPtr Internal_InstantiateAsyncWithParams([NotNull] Object original, int count, InstantiateParameters parameters, IntPtr positions, int positionsCount, IntPtr rotations, int rotationsCount)
		{
			if (original == null)
			{
				ThrowHelper.ThrowArgumentNullException(original, "original");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(original);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(original, "original");
			}
			return Object.Internal_InstantiateAsyncWithParams_Injected(intPtr, count, ref parameters, positions, positionsCount, rotations, rotationsCount);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00043064 File Offset: 0x00041264
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle([NotNull] Object data, Vector3 pos, Quaternion rot)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_InstantiateSingle_Injected(intPtr, ref pos, ref rot));
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000430A4 File Offset: 0x000412A4
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent([NotNull] Object data, [NotNull] Transform parent, Vector3 pos, Quaternion rot)
		{
			if (data == null)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(data);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(data, "data");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Object.Internal_InstantiateSingleWithParent_Injected(intPtr, intPtr2, ref pos, ref rot));
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00043108 File Offset: 0x00041308
		[FreeFunction("UnityEngineObjectBindings::ToString")]
		private static string ToString(Object obj)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Object.ToString_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00043140 File Offset: 0x00041340
		[FreeFunction("UnityEngineObjectBindings::GetName", HasExplicitThis = true)]
		private string GetName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Object.GetName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00043180 File Offset: 0x00041380
		[FreeFunction("UnityEngineObjectBindings::IsPersistent")]
		internal static bool IsPersistent([NotNull] Object obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(obj);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Object.IsPersistent_Injected(intPtr);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000431B8 File Offset: 0x000413B8
		[FreeFunction("UnityEngineObjectBindings::SetName", HasExplicitThis = true)]
		private unsafe void SetName(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Object.SetName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002240 RID: 8768
		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		// Token: 0x06002241 RID: 8769 RVA: 0x0004321C File Offset: 0x0004141C
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID")]
		internal static Object FindObjectFromInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(Object.FindObjectFromInstanceID_Injected(instanceID));
		}

		// Token: 0x06002242 RID: 8770
		[FreeFunction("UnityEngineObjectBindings::GetPtrFromInstanceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetPtrFromInstanceID(int instanceID, Type objectType, out bool isMonoBehaviour);

		// Token: 0x06002243 RID: 8771 RVA: 0x00043234 File Offset: 0x00041434
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::ForceLoadFromInstanceID")]
		internal static Object ForceLoadFromInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(Object.ForceLoadFromInstanceID_Injected(instanceID));
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0004324C File Offset: 0x0004144C
		[FreeFunction("UnityEngineObjectBindings::MarkObjectDirty", HasExplicitThis = true)]
		internal void MarkDirty()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Object.MarkDirty_Injected(intPtr);
		}

		// Token: 0x06002247 RID: 8775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy_Injected(IntPtr obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06002248 RID: 8776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImmediate_Injected(IntPtr obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x06002249 RID: 8777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DontDestroyOnLoad_Injected(IntPtr target);

		// Token: 0x0600224A RID: 8778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HideFlags get_hideFlags_Injected(IntPtr _unity_self);

		// Token: 0x0600224B RID: 8779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hideFlags_Injected(IntPtr _unity_self, HideFlags value);

		// Token: 0x0600224C RID: 8780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingle_Injected(IntPtr data);

		// Token: 0x0600224D RID: 8781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingleWithScene_Injected(IntPtr data, [In] ref Scene scene);

		// Token: 0x0600224E RID: 8782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingleWithParams_Injected(IntPtr data, [In] ref InstantiateParameters parameters);

		// Token: 0x0600224F RID: 8783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateSingleWithParams_Injected(IntPtr data, [In] ref Vector3 position, [In] ref Quaternion rotation, [In] ref InstantiateParameters parameters);

		// Token: 0x06002250 RID: 8784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CloneSingleWithParent_Injected(IntPtr data, IntPtr parent, bool worldPositionStays);

		// Token: 0x06002251 RID: 8785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateAsyncWithParams_Injected(IntPtr original, int count, [In] ref InstantiateParameters parameters, IntPtr positions, int positionsCount, IntPtr rotations, int rotationsCount);

		// Token: 0x06002252 RID: 8786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateSingle_Injected(IntPtr data, [In] ref Vector3 pos, [In] ref Quaternion rot);

		// Token: 0x06002253 RID: 8787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_InstantiateSingleWithParent_Injected(IntPtr data, IntPtr parent, [In] ref Vector3 pos, [In] ref Quaternion rot);

		// Token: 0x06002254 RID: 8788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ToString_Injected(IntPtr obj, out ManagedSpanWrapper ret);

		// Token: 0x06002255 RID: 8789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06002256 RID: 8790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPersistent_Injected(IntPtr obj);

		// Token: 0x06002257 RID: 8791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06002258 RID: 8792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindObjectFromInstanceID_Injected(int instanceID);

		// Token: 0x06002259 RID: 8793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ForceLoadFromInstanceID_Injected(int instanceID);

		// Token: 0x0600225A RID: 8794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkDirty_Injected(IntPtr _unity_self);

		// Token: 0x040009FB RID: 2555
		private const int kInstanceID_None = 0;

		// Token: 0x040009FC RID: 2556
		private IntPtr m_CachedPtr;

		// Token: 0x040009FD RID: 2557
		internal static readonly int OffsetOfInstanceIDInCPlusPlusObject = Object.GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x040009FE RID: 2558
		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		// Token: 0x040009FF RID: 2559
		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";

		// Token: 0x020002BE RID: 702
		[VisibleToOtherModules]
		internal static class MarshalledUnityObject
		{
			// Token: 0x0600225B RID: 8795 RVA: 0x0004327C File Offset: 0x0004147C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static IntPtr Marshal<T>(T obj) where T : Object
			{
				bool flag = obj == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = Object.MarshalledUnityObject.MarshalNotNull<T>(obj);
				}
				return intPtr;
			}

			// Token: 0x0600225C RID: 8796 RVA: 0x000432AC File Offset: 0x000414AC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static IntPtr MarshalNotNull<T>(T obj) where T : Object
			{
				return obj.m_CachedPtr;
			}

			// Token: 0x0600225D RID: 8797 RVA: 0x00002925 File Offset: 0x00000B25
			public static void TryThrowEditorNullExceptionObject(Object unityObj, string paramterName)
			{
			}
		}
	}
}
