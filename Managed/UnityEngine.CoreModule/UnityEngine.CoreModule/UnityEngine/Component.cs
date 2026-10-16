using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200028B RID: 651
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/Component.bindings.h")]
	[NativeClass("Unity::Component")]
	public class Component : Object
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0003E554 File Offset: 0x0003C754
		public Transform transform
		{
			[FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(Component.get_transform_Injected(intPtr));
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0003E57C File Offset: 0x0003C77C
		public GameObject gameObject
		{
			[FreeFunction("GetGameObject", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<GameObject>(Component.get_gameObject_Injected(intPtr));
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		internal void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Component.GetComponentFastPath_Injected(intPtr, type, oneFurtherThanResultValue);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0003E628 File Offset: 0x0003C828
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public bool TryGetComponent(Type type, out Component component)
		{
			return this.gameObject.TryGetComponent(type, out component);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0003E648 File Offset: 0x0003C848
		[SecuritySafeCritical]
		public bool TryGetComponent<T>(out T component)
		{
			return this.gameObject.TryGetComponent<T>(out component);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0003E668 File Offset: 0x0003C868
		[FreeFunction(HasExplicitThis = true)]
		public unsafe Component GetComponent(string type)
		{
			Component component;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				IntPtr component_Injected = Component.GetComponent_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr component_Injected;
				component = Unmarshal.UnmarshalUnityObject<Component>(component_Injected);
				char* ptr = null;
			}
			return component;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.GetComponentInChildren(t, false);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0003E714 File Offset: 0x0003C914
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0003E73C File Offset: 0x0003C93C
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), false));
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0003E764 File Offset: 0x0003C964
		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0003E784 File Offset: 0x0003C984
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			return this.gameObject.GetComponentsInChildren(t, false);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0003E7A4 File Offset: 0x0003C9A4
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0003E7C2 File Offset: 0x0003C9C2
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0003E7ED File Offset: 0x0003C9ED
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0003E7FC File Offset: 0x0003C9FC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInParent(t, includeInactive);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0003E81C File Offset: 0x0003CA1C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t, false);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0003E83C File Offset: 0x0003CA3C
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0003E864 File Offset: 0x0003CA64
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T), false));
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0003E88C File Offset: 0x0003CA8C
		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0003E8AC File Offset: 0x0003CAAC
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			return this.GetComponentsInParent(t, false);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0003E8E6 File Offset: 0x0003CAE6
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0003E914 File Offset: 0x0003CB14
		public Component[] GetComponents(Type type)
		{
			return this.gameObject.GetComponents(type);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0003E934 File Offset: 0x0003CB34
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		private void GetComponentsForListInternal(Type searchType, object resultList)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Component.GetComponentsForListInternal_Injected(intPtr, searchType, resultList);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0003E958 File Offset: 0x0003CB58
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0003E964 File Offset: 0x0003CB64
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0003E979 File Offset: 0x0003CB79
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0003E986 File Offset: 0x0003CB86
		public string tag
		{
			get
			{
				return this.gameObject.tag;
			}
			set
			{
				this.gameObject.tag = value;
			}
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0003E998 File Offset: 0x0003CB98
		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		public int GetComponentIndex()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Component.GetComponentIndex_Injected(intPtr);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0003E9DC File Offset: 0x0003CBDC
		public bool CompareTag(string tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0003E9FC File Offset: 0x0003CBFC
		public bool CompareTag(TagHandle tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0003EA1C File Offset: 0x0003CC1C
		[FreeFunction(HasExplicitThis = true)]
		public unsafe void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				Component.SendMessageUpwards_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0003EA84 File Offset: 0x0003CC84
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			this.SendMessageUpwards(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0003EA91 File Offset: 0x0003CC91
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			this.SendMessageUpwards(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0003EA9E File Offset: 0x0003CC9E
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0003EAAB File Offset: 0x0003CCAB
		public void SendMessage(string methodName, object value)
		{
			this.SendMessage(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0003EAB8 File Offset: 0x0003CCB8
		public void SendMessage(string methodName)
		{
			this.SendMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0003EAC8 File Offset: 0x0003CCC8
		[FreeFunction("SendMessage", HasExplicitThis = true)]
		public unsafe void SendMessage(string methodName, object value, SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				Component.SendMessage_Injected(intPtr, ref managedSpanWrapper, value, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0003EB30 File Offset: 0x0003CD30
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0003EB40 File Offset: 0x0003CD40
		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		public unsafe void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Component>(this);
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
				Component.BroadcastMessage_Injected(intPtr, ref managedSpanWrapper, parameter, options);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0003EBA8 File Offset: 0x0003CDA8
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			this.BroadcastMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0003EBB5 File Offset: 0x0003CDB5
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			this.BroadcastMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0003EBC2 File Offset: 0x0003CDC2
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x0600207B RID: 8315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_transform_Injected(IntPtr _unity_self);

		// Token: 0x0600207C RID: 8316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_gameObject_Injected(IntPtr _unity_self);

		// Token: 0x0600207D RID: 8317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentFastPath_Injected(IntPtr _unity_self, Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x0600207E RID: 8318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComponent_Injected(IntPtr _unity_self, ref ManagedSpanWrapper type);

		// Token: 0x0600207F RID: 8319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComponentsForListInternal_Injected(IntPtr _unity_self, Type searchType, object resultList);

		// Token: 0x06002080 RID: 8320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComponentIndex_Injected(IntPtr _unity_self);

		// Token: 0x06002081 RID: 8321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessageUpwards_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06002082 RID: 8322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, object value, SendMessageOptions options);

		// Token: 0x06002083 RID: 8323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BroadcastMessage_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
	}
}
