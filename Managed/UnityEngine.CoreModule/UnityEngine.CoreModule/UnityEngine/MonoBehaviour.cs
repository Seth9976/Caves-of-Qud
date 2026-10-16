using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002A1 RID: 673
	[ExtensionOfNativeClass]
	[NativeHeader("Runtime/Scripting/DelayedCallUtility.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00040AF8 File Offset: 0x0003ECF8
		public CancellationToken destroyCancellationToken
		{
			get
			{
				bool flag = this == null;
				if (flag)
				{
					throw new MissingReferenceException("DestroyCancellation token should be called atleast once before destroying the monobehaviour object");
				}
				bool flag2 = this.m_CancellationTokenSource == null;
				if (flag2)
				{
					this.m_CancellationTokenSource = new CancellationTokenSource();
					this.OnCancellationTokenCreated();
				}
				return this.m_CancellationTokenSource.Token;
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00040B4C File Offset: 0x0003ED4C
		[RequiredByNativeCode]
		private void RaiseCancellation()
		{
			CancellationTokenSource cancellationTokenSource = this.m_CancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00040B64 File Offset: 0x0003ED64
		public bool IsInvoking()
		{
			return MonoBehaviour.Internal_IsInvokingAll(this);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00040B7C File Offset: 0x0003ED7C
		public void CancelInvoke()
		{
			MonoBehaviour.Internal_CancelInvokeAll(this);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00040B86 File Offset: 0x0003ED86
		public void Invoke(string methodName, float time)
		{
			MonoBehaviour.InvokeDelayed(this, methodName, time, 0f);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00040B98 File Offset: 0x0003ED98
		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			bool flag = repeatRate <= 1E-05f && repeatRate != 0f;
			if (flag)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F");
			}
			MonoBehaviour.InvokeDelayed(this, methodName, time, repeatRate);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00040BD5 File Offset: 0x0003EDD5
		public void CancelInvoke(string methodName)
		{
			MonoBehaviour.CancelInvoke(this, methodName);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00040BE0 File Offset: 0x0003EDE0
		public bool IsInvoking(string methodName)
		{
			return MonoBehaviour.IsInvoking(this, methodName);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00040BFC File Offset: 0x0003EDFC
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object obj = null;
			return this.StartCoroutine(methodName, obj);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00040C18 File Offset: 0x0003EE18
		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			bool flag = string.IsNullOrEmpty(methodName);
			if (flag)
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged(methodName, value);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00040C60 File Offset: 0x0003EE60
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged2(routine);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00040CA4 File Offset: 0x0003EEA4
		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return this.StartCoroutine(routine);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00040CC0 File Offset: 0x0003EEC0
		public void StopCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineFromEnumeratorManaged(routine);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00040D04 File Offset: 0x0003EF04
		public void StopCoroutine(Coroutine routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineManaged(routine);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00040D48 File Offset: 0x0003EF48
		public unsafe void StopCoroutine(string methodName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
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
				MonoBehaviour.StopCoroutine_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00040DAC File Offset: 0x0003EFAC
		public void StopAllCoroutines()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopAllCoroutines_Injected(intPtr);
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		public bool useGUILayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_useGUILayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MonoBehaviour.set_useGUILayout_Injected(intPtr, value);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x00040E18 File Offset: 0x0003F018
		public bool didStart
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_didStart_Injected(intPtr);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x00040E3C File Offset: 0x0003F03C
		public bool didAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_didAwake_Injected(intPtr);
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00040E5E File Offset: 0x0003F05E
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00040E68 File Offset: 0x0003F068
		[FreeFunction("CancelInvoke")]
		private static void Internal_CancelInvokeAll([NotNull] MonoBehaviour self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			MonoBehaviour.Internal_CancelInvokeAll_Injected(intPtr);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		[FreeFunction("IsInvoking")]
		private static bool Internal_IsInvokingAll([NotNull] MonoBehaviour self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return MonoBehaviour.Internal_IsInvokingAll_Injected(intPtr);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		[FreeFunction]
		private unsafe static void InvokeDelayed([NotNull] MonoBehaviour self, string methodName, float time, float repeatRate)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
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
				MonoBehaviour.InvokeDelayed_Injected(intPtr, ref managedSpanWrapper, time, repeatRate);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00040F54 File Offset: 0x0003F154
		[FreeFunction]
		private unsafe static void CancelInvoke([NotNull] MonoBehaviour self, string methodName)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
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
				MonoBehaviour.CancelInvoke_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00040FCC File Offset: 0x0003F1CC
		[FreeFunction]
		private unsafe static bool IsInvoking([NotNull] MonoBehaviour self, string methodName)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
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
				flag = MonoBehaviour.IsInvoking_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00041048 File Offset: 0x0003F248
		[FreeFunction]
		private static bool IsObjectMonoBehaviour([NotNull] Object obj)
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
			return MonoBehaviour.IsObjectMonoBehaviour_Injected(intPtr);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00041080 File Offset: 0x0003F280
		[return: Unmarshalled]
		private unsafe Coroutine StartCoroutineManaged(string methodName, object value)
		{
			Coroutine coroutine;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
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
				coroutine = MonoBehaviour.StartCoroutineManaged_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return coroutine;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000410E8 File Offset: 0x0003F2E8
		[return: Unmarshalled]
		private Coroutine StartCoroutineManaged2(IEnumerator enumerator)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MonoBehaviour.StartCoroutineManaged2_Injected(intPtr, enumerator);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0004110C File Offset: 0x0003F30C
		private void StopCoroutineManaged(Coroutine routine)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopCoroutineManaged_Injected(intPtr, (routine == null) ? ((IntPtr)0) : Coroutine.BindingsMarshaller.ConvertToNative(routine));
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00041140 File Offset: 0x0003F340
		private void StopCoroutineFromEnumeratorManaged(IEnumerator routine)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopCoroutineFromEnumeratorManaged_Injected(intPtr, routine);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00041164 File Offset: 0x0003F364
		internal string GetScriptClassName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				MonoBehaviour.GetScriptClassName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000411A4 File Offset: 0x0003F3A4
		private void OnCancellationTokenCreated()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.OnCancellationTokenCreated_Injected(intPtr);
		}

		// Token: 0x0600216E RID: 8558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutine_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName);

		// Token: 0x0600216F RID: 8559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopAllCoroutines_Injected(IntPtr _unity_self);

		// Token: 0x06002170 RID: 8560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGUILayout_Injected(IntPtr _unity_self);

		// Token: 0x06002171 RID: 8561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGUILayout_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06002172 RID: 8562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_didStart_Injected(IntPtr _unity_self);

		// Token: 0x06002173 RID: 8563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_didAwake_Injected(IntPtr _unity_self);

		// Token: 0x06002174 RID: 8564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CancelInvokeAll_Injected(IntPtr self);

		// Token: 0x06002175 RID: 8565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsInvokingAll_Injected(IntPtr self);

		// Token: 0x06002176 RID: 8566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeDelayed_Injected(IntPtr self, ref ManagedSpanWrapper methodName, float time, float repeatRate);

		// Token: 0x06002177 RID: 8567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInvoke_Injected(IntPtr self, ref ManagedSpanWrapper methodName);

		// Token: 0x06002178 RID: 8568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInvoking_Injected(IntPtr self, ref ManagedSpanWrapper methodName);

		// Token: 0x06002179 RID: 8569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsObjectMonoBehaviour_Injected(IntPtr obj);

		// Token: 0x0600217A RID: 8570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Coroutine StartCoroutineManaged_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, object value);

		// Token: 0x0600217B RID: 8571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Coroutine StartCoroutineManaged2_Injected(IntPtr _unity_self, IEnumerator enumerator);

		// Token: 0x0600217C RID: 8572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutineManaged_Injected(IntPtr _unity_self, IntPtr routine);

		// Token: 0x0600217D RID: 8573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutineFromEnumeratorManaged_Injected(IntPtr _unity_self, IEnumerator routine);

		// Token: 0x0600217E RID: 8574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetScriptClassName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600217F RID: 8575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnCancellationTokenCreated_Injected(IntPtr _unity_self);

		// Token: 0x040009C5 RID: 2501
		private CancellationTokenSource m_CancellationTokenSource;
	}
}
