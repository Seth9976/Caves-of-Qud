using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200029A RID: 666
	[ExcludeFromPreset]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h")]
	public sealed class GameObject : Object
	{
		// Token: 0x060020A8 RID: 8360 RVA: 0x0003F66C File Offset: 0x0003D86C
		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		public static GameObject CreatePrimitive(PrimitiveType type)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(GameObject.CreatePrimitive_Injected(type));
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0003F684 File Offset: 0x0003D884
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0003F6C4 File Offset: 0x0003D8C4
		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponent_Injected(intPtr, type));
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		internal void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.GetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0003F710 File Offset: 0x0003D910
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		internal unsafe Component GetComponentByName(string type)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr componentByName_Injected = GameObject.GetComponentByName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr componentByName_Injected;
				component = Unmarshal.UnmarshalUnityObject<Component>(componentByName_Injected);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0003F780 File Offset: 0x0003D980
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObjectWithCase", HasExplicitThis = true)]
		internal unsafe Component GetComponentByNameWithCase(string type, bool caseSensitive)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(type, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = type.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr componentByNameWithCase_Injected = GameObject.GetComponentByNameWithCase_Injected(intPtr, ref managedSpanWrapper, caseSensitive);
			}
			finally
			{
				IntPtr componentByNameWithCase_Injected;
				component = Unmarshal.UnmarshalUnityObject<Component>(componentByNameWithCase_Injected);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0003F80C File Offset: 0x0003DA0C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true)]
		public Component GetComponentInChildren(Type type, bool includeInactive)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponentInChildren_Injected(intPtr, type, includeInactive));
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0003F838 File Offset: 0x0003DA38
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return this.GetComponentInChildren(type, false);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0003F854 File Offset: 0x0003DA54
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool flag = false;
			return this.GetComponentInChildren<T>(flag);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0003F870 File Offset: 0x0003DA70
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0003F898 File Offset: 0x0003DA98
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true)]
		public Component GetComponentInParent(Type type, bool includeInactive)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.GetComponentInParent_Injected(intPtr, type, includeInactive));
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0003F8C4 File Offset: 0x0003DAC4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			return this.GetComponentInParent(type, false);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0003F8E0 File Offset: 0x0003DAE0
		[ExcludeFromDocs]
		public T GetComponentInParent<T>()
		{
			bool flag = false;
			return this.GetComponentInParent<T>(flag);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0003F8FC File Offset: 0x0003DAFC
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0003F924 File Offset: 0x0003DB24
		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		private Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentsInternal_Injected(intPtr, type, useSearchTypeAsArrayReturnType, recursive, includeInactive, reverse, resultList);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0003F950 File Offset: 0x0003DB50
		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0003F974 File Offset: 0x0003DB74
		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0003F9A0 File Offset: 0x0003DBA0
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0003F9B0 File Offset: 0x0003DBB0
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, false, true, false, results);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0003F9CC File Offset: 0x0003DBCC
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool flag = false;
			return this.GetComponentsInChildren(type, flag);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0003FA0C File Offset: 0x0003DC0C
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0003FA38 File Offset: 0x0003DC38
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0003FA54 File Offset: 0x0003DC54
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0003FA6D File Offset: 0x0003DC6D
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0003FA7C File Offset: 0x0003DC7C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool flag = false;
			return this.GetComponentsInParent(type, flag);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0003FA98 File Offset: 0x0003DC98
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0003FABB File Offset: 0x0003DCBB
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0003FAD4 File Offset: 0x0003DCD4
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0003FB00 File Offset: 0x0003DD00
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0003FB1C File Offset: 0x0003DD1C
		[SecuritySafeCritical]
		public unsafe bool TryGetComponent<T>(out T component)
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.TryGetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			component = castHelper.t;
			return castHelper.t != null;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0003FB70 File Offset: 0x0003DD70
		public bool TryGetComponent(Type type, out Component component)
		{
			component = this.TryGetComponentInternal(type);
			return component != null;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0003FB94 File Offset: 0x0003DD94
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		internal Component TryGetComponentInternal(Type type)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.TryGetComponentInternal_Injected(intPtr, type));
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		internal void TryGetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.TryGetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTagForListInternal", ThrowsException = true)]
		private unsafe static void FindGameObjectsWithTagForListInternal(string tag, object results)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.FindGameObjectsWithTagForListInternal_Injected(ref managedSpanWrapper, results);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0003FC50 File Offset: 0x0003DE50
		public static void FindGameObjectsWithTag(string tag, List<GameObject> results)
		{
			GameObject.FindGameObjectsWithTagForListInternal(tag, results);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0003FC5B File Offset: 0x0003DE5B
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0003FC68 File Offset: 0x0003DE68
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0003FC75 File Offset: 0x0003DE75
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0003FC84 File Offset: 0x0003DE84
		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		internal unsafe Component AddComponentInternal(string className)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = GameObject.AddComponentInternal_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr2;
				component = Unmarshal.UnmarshalUnityObject<Component>(intPtr2);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0003FCF4 File Offset: 0x0003DEF4
		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		private Component Internal_AddComponentWithType(Type componentType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.Internal_AddComponentWithType_Injected(intPtr, componentType));
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0003FD1C File Offset: 0x0003DF1C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0003FD38 File Offset: 0x0003DF38
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0003FD64 File Offset: 0x0003DF64
		public int GetComponentCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentCount_Injected(intPtr);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0003FD88 File Offset: 0x0003DF88
		[NativeName("QueryComponentAtIndex<Unity::Component>")]
		internal Component QueryComponentAtIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Component>(GameObject.QueryComponentAtIndex_Injected(intPtr, index));
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		public Component GetComponentAtIndex(int index)
		{
			bool flag = index < 0 || index >= this.GetComponentCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", "Valid range is 0 to GetComponentCount() - 1.");
			}
			return this.QueryComponentAtIndex(index);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		public T GetComponentAtIndex<T>(int index) where T : Component
		{
			T t = (T)((object)this.GetComponentAtIndex(index));
			bool flag = t == null;
			if (flag)
			{
				throw new InvalidCastException();
			}
			return t;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0003FE28 File Offset: 0x0003E028
		public int GetComponentIndex(Component component)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.GetComponentIndex_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Component>(component));
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0003FE50 File Offset: 0x0003E050
		public Transform transform
		{
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(GameObject.get_transform_Injected(intPtr));
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0003FE78 File Offset: 0x0003E078
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0003FE9C File Offset: 0x0003E09C
		public int layer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_layer_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_layer_Injected(intPtr, value);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0003FEE4 File Offset: 0x0003E0E4
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public bool active
		{
			[NativeMethod(Name = "IsActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_active_Injected(intPtr);
			}
			[NativeMethod(Name = "SetSelfActive")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_active_Injected(intPtr, value);
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0003FF08 File Offset: 0x0003E108
		[NativeMethod(Name = "SetSelfActive")]
		public void SetActive(bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.SetActive_Injected(intPtr, value);
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0003FF2C File Offset: 0x0003E12C
		public bool activeSelf
		{
			[NativeMethod(Name = "IsSelfActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_activeSelf_Injected(intPtr);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0003FF50 File Offset: 0x0003E150
		public bool activeInHierarchy
		{
			[NativeMethod(Name = "IsActive")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_activeInHierarchy_Injected(intPtr);
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0003FF74 File Offset: 0x0003E174
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		public void SetActiveRecursively(bool state)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GameObject.SetActiveRecursively_Injected(intPtr, state);
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x0003FF98 File Offset: 0x0003E198
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x0003FFBC File Offset: 0x0003E1BC
		public bool isStatic
		{
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_isStatic_Injected(intPtr);
			}
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GameObject.set_isStatic_Injected(intPtr, value);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0003FFE0 File Offset: 0x0003E1E0
		internal bool isStaticBatchable
		{
			[NativeMethod(Name = "IsStaticBatchable")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_isStaticBatchable_Injected(intPtr);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00040004 File Offset: 0x0003E204
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x00040044 File Offset: 0x0003E244
		public unsafe string tag
		{
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true)]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					GameObject.get_tag_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GameObject.set_tag_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000400A8 File Offset: 0x0003E2A8
		public bool CompareTag(string tag)
		{
			return this.CompareTag_Internal(tag);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000400B1 File Offset: 0x0003E2B1
		public bool CompareTag(TagHandle tag)
		{
			return this.CompareTagHandle_Internal(tag);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000400BC File Offset: 0x0003E2BC
		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		private unsafe bool CompareTag_Internal(string tag)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GameObject.CompareTag_Internal_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00040124 File Offset: 0x0003E324
		[FreeFunction(Name = "GameObjectBindings::CompareTagHandle", HasExplicitThis = true)]
		private bool CompareTagHandle_Internal(TagHandle tag)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GameObject.CompareTagHandle_Internal_Injected(intPtr, ref tag);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00040148 File Offset: 0x0003E348
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		public unsafe static GameObject FindGameObjectWithTag(string tag)
		{
			GameObject gameObject;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GameObject.FindGameObjectWithTag_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				gameObject = Unmarshal.UnmarshalUnityObject<GameObject>(intPtr);
				char* ptr = null;
			}
			return gameObject;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000401A8 File Offset: 0x0003E3A8
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		public unsafe static GameObject[] FindGameObjectsWithTag(string tag)
		{
			GameObject[] array;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				array = GameObject.FindGameObjectsWithTag_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00040200 File Offset: 0x0003E400
		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		public unsafe void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.SendMessageUpwards_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00040268 File Offset: 0x0003E468
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00040284 File Offset: 0x0003E484
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000402A0 File Offset: 0x0003E4A0
		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		public unsafe void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.SendMessage_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00040308 File Offset: 0x0003E508
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00040324 File Offset: 0x0003E524
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00040340 File Offset: 0x0003E540
		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		public unsafe void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.BroadcastMessage_Injected(intPtr, ref managedSpanWrapper, parameter, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000403A8 File Offset: 0x0003E5A8
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000403C4 File Offset: 0x0003E5C4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000403E0 File Offset: 0x0003E5E0
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000403F2 File Offset: 0x0003E5F2
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00040404 File Offset: 0x0003E604
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type type in components)
			{
				this.AddComponent(type);
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00040440 File Offset: 0x0003E640
		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		private unsafe static void Internal_CreateGameObject([Writable] GameObject self, string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GameObject.Internal_CreateGameObject_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00040498 File Offset: 0x0003E698
		[FreeFunction(Name = "GameObjectBindings::Find")]
		public unsafe static GameObject Find(string name)
		{
			GameObject gameObject;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = GameObject.Find_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				gameObject = Unmarshal.UnmarshalUnityObject<GameObject>(intPtr);
				char* ptr = null;
			}
			return gameObject;
		}

		// Token: 0x060020FC RID: 8444
		[FreeFunction(Name = "GameObjectBindings::SetGameObjectsActiveByInstanceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGameObjectsActive(IntPtr instanceIds, int instanceCount, bool active);

		// Token: 0x060020FD RID: 8445 RVA: 0x000404F8 File Offset: 0x0003E6F8
		public static void SetGameObjectsActive(NativeArray<int> instanceIDs, bool active)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = instanceIDs.Length == 0;
			if (!flag2)
			{
				GameObject.SetGameObjectsActive((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, active);
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00040550 File Offset: 0x0003E750
		public unsafe static void SetGameObjectsActive(ReadOnlySpan<int> instanceIDs, bool active)
		{
			bool flag = instanceIDs.Length == 0;
			if (!flag)
			{
				fixed (int* pinnableReference = instanceIDs.GetPinnableReference())
				{
					int* ptr = pinnableReference;
					GameObject.SetGameObjectsActive((IntPtr)((void*)ptr), instanceIDs.Length, active);
				}
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00040594 File Offset: 0x0003E794
		[FreeFunction("GameObjectBindings::InstantiateGameObjectsByInstanceID")]
		private static void InstantiateGameObjects(int sourceInstanceID, IntPtr newInstanceIDs, IntPtr newTransformInstanceIDs, int count, Scene destinationScene)
		{
			GameObject.InstantiateGameObjects_Injected(sourceInstanceID, newInstanceIDs, newTransformInstanceIDs, count, ref destinationScene);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000405AC File Offset: 0x0003E7AC
		public static void InstantiateGameObjects(int sourceInstanceID, int count, NativeArray<int> newInstanceIDs, NativeArray<int> newTransformInstanceIDs, Scene destinationScene = default(Scene))
		{
			bool flag = !newInstanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "newInstanceIDs");
			}
			bool flag2 = !newTransformInstanceIDs.IsCreated;
			if (flag2)
			{
				throw new ArgumentException("NativeArray is uninitialized", "newTransformInstanceIDs");
			}
			bool flag3 = count == 0;
			if (!flag3)
			{
				bool flag4 = count != newInstanceIDs.Length || count != newTransformInstanceIDs.Length;
				if (flag4)
				{
					throw new ArgumentException("Size mismatch! Both arrays must already be the size of count.");
				}
				GameObject.InstantiateGameObjects(sourceInstanceID, (IntPtr)newInstanceIDs.GetUnsafeReadOnlyPtr<int>(), (IntPtr)newTransformInstanceIDs.GetUnsafeReadOnlyPtr<int>(), newInstanceIDs.Length, destinationScene);
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00040650 File Offset: 0x0003E850
		[FreeFunction(Name = "GameObjectBindings::GetSceneByInstanceID")]
		public static Scene GetScene(int instanceID)
		{
			Scene scene;
			GameObject.GetScene_Injected(instanceID, out scene);
			return scene;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00040668 File Offset: 0x0003E868
		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Scene scene;
				GameObject.get_scene_Injected(intPtr, out scene);
				return scene;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00040690 File Offset: 0x0003E890
		public ulong sceneCullingMask
		{
			[FreeFunction(Name = "GameObjectBindings::GetSceneCullingMask", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GameObject.get_sceneCullingMask_Injected(intPtr);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x000406B4 File Offset: 0x0003E8B4
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002105 RID: 8453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePrimitive_Injected(PrimitiveType type);

		// Token: 0x06002106 RID: 8454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponent_Injected(IntPtr _unity_self, Type type);

		// Token: 0x06002107 RID: 8455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06002108 RID: 8456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentByName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper type);

		// Token: 0x06002109 RID: 8457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentByNameWithCase_Injected(IntPtr _unity_self, ref ManagedSpanWrapper type, bool caseSensitive);

		// Token: 0x0600210A RID: 8458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentInChildren_Injected(IntPtr _unity_self, Type type, bool includeInactive);

		// Token: 0x0600210B RID: 8459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponentInParent_Injected(IntPtr _unity_self, Type type, bool includeInactive);

		// Token: 0x0600210C RID: 8460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array GetComponentsInternal_Injected(IntPtr _unity_self, Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x0600210D RID: 8461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr TryGetComponentInternal_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600210E RID: 8462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TryGetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x0600210F RID: 8463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FindGameObjectsWithTagForListInternal_Injected(ref ManagedSpanWrapper tag, object results);

		// Token: 0x06002110 RID: 8464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AddComponentInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper className);

		// Token: 0x06002111 RID: 8465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_AddComponentWithType_Injected(IntPtr _unity_self, Type componentType);

		// Token: 0x06002112 RID: 8466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComponentCount_Injected(IntPtr _unity_self);

		// Token: 0x06002113 RID: 8467
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr QueryComponentAtIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x06002114 RID: 8468
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComponentIndex_Injected(IntPtr _unity_self, IntPtr component);

		// Token: 0x06002115 RID: 8469
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_transform_Injected(IntPtr _unity_self);

		// Token: 0x06002116 RID: 8470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layer_Injected(IntPtr _unity_self);

		// Token: 0x06002117 RID: 8471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layer_Injected(IntPtr _unity_self, int value);

		// Token: 0x06002118 RID: 8472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_active_Injected(IntPtr _unity_self);

		// Token: 0x06002119 RID: 8473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_active_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600211A RID: 8474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600211B RID: 8475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_activeSelf_Injected(IntPtr _unity_self);

		// Token: 0x0600211C RID: 8476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_activeInHierarchy_Injected(IntPtr _unity_self);

		// Token: 0x0600211D RID: 8477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActiveRecursively_Injected(IntPtr _unity_self, bool state);

		// Token: 0x0600211E RID: 8478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStatic_Injected(IntPtr _unity_self);

		// Token: 0x0600211F RID: 8479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isStatic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06002120 RID: 8480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStaticBatchable_Injected(IntPtr _unity_self);

		// Token: 0x06002121 RID: 8481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tag_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06002122 RID: 8482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tag_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06002123 RID: 8483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareTag_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag);

		// Token: 0x06002124 RID: 8484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareTagHandle_Internal_Injected(IntPtr _unity_self, [In] ref TagHandle tag);

		// Token: 0x06002125 RID: 8485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindGameObjectWithTag_Injected(ref ManagedSpanWrapper tag);

		// Token: 0x06002126 RID: 8486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject[] FindGameObjectsWithTag_Injected(ref ManagedSpanWrapper tag);

		// Token: 0x06002127 RID: 8487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessageUpwards_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06002128 RID: 8488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06002129 RID: 8489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BroadcastMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600212A RID: 8490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject_Injected([Writable] GameObject self, ref ManagedSpanWrapper name);

		// Token: 0x0600212B RID: 8491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Find_Injected(ref ManagedSpanWrapper name);

		// Token: 0x0600212C RID: 8492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstantiateGameObjects_Injected(int sourceInstanceID, IntPtr newInstanceIDs, IntPtr newTransformInstanceIDs, int count, [In] ref Scene destinationScene);

		// Token: 0x0600212D RID: 8493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetScene_Injected(int instanceID, out Scene ret);

		// Token: 0x0600212E RID: 8494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scene_Injected(IntPtr _unity_self, out Scene ret);

		// Token: 0x0600212F RID: 8495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_sceneCullingMask_Injected(IntPtr _unity_self);
	}
}
