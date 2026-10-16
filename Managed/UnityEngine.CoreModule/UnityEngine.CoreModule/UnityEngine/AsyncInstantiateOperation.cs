using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000251 RID: 593
	[NativeHeader("Runtime/GameCode/AsyncInstantiate/AsyncInstantiateOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncInstantiateOperation : AsyncOperation
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0003BC50 File Offset: 0x00039E50
		public Object[] Result
		{
			get
			{
				return this.m_Result;
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0003BC68 File Offset: 0x00039E68
		[NativeMethod("IsWaitingForSceneActivation")]
		public bool IsWaitingForSceneActivation()
		{
			IntPtr intPtr = AsyncInstantiateOperation.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AsyncInstantiateOperation.IsWaitingForSceneActivation_Injected(intPtr);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0003BC8C File Offset: 0x00039E8C
		[NativeMethod("WaitForCompletion")]
		public void WaitForCompletion()
		{
			IntPtr intPtr = AsyncInstantiateOperation.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AsyncInstantiateOperation.WaitForCompletion_Injected(intPtr);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		[NativeMethod("Cancel")]
		public void Cancel()
		{
			IntPtr intPtr = AsyncInstantiateOperation.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AsyncInstantiateOperation.Cancel_Injected(intPtr);
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001F1C RID: 7964
		// (set) Token: 0x06001F1D RID: 7965
		[StaticAccessor("GetAsyncInstantiateManager()", StaticAccessorType.Dot)]
		internal static extern float IntegrationTimeMS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		public AsyncInstantiateOperation()
			: this(IntPtr.Zero, default(CancellationToken))
		{
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0003BCF7 File Offset: 0x00039EF7
		protected AsyncInstantiateOperation(IntPtr ptr, CancellationToken cancellationToken)
			: base(ptr)
		{
			this.m_CancellationToken = CancellationTokenSource.CreateLinkedTokenSource(AsyncInstantiateOperation.s_GlobalCancellation.Token, cancellationToken).Token;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0003BD20 File Offset: 0x00039F20
		public static float GetIntegrationTimeMS()
		{
			return AsyncInstantiateOperation.IntegrationTimeMS;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0003BD38 File Offset: 0x00039F38
		public static void SetIntegrationTimeMS(float integrationTimeMS)
		{
			bool flag = integrationTimeMS <= 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("integrationTimeMS", "integrationTimeMS was out of range. Must be greater than zero.");
			}
			AsyncInstantiateOperation.IntegrationTimeMS = integrationTimeMS;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0003BD6C File Offset: 0x00039F6C
		[RequiredByNativeCode(GenerateProxy = true)]
		private bool IsCancellationRequested()
		{
			return this.m_CancellationToken.IsCancellationRequested;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0003BD8C File Offset: 0x00039F8C
		internal virtual Object[] CreateResultArray(int size)
		{
			this.m_Result = new Object[size];
			return this.m_Result;
		}

		// Token: 0x06001F25 RID: 7973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsWaitingForSceneActivation_Injected(IntPtr _unity_self);

		// Token: 0x06001F26 RID: 7974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitForCompletion_Injected(IntPtr _unity_self);

		// Token: 0x06001F27 RID: 7975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cancel_Injected(IntPtr _unity_self);

		// Token: 0x04000930 RID: 2352
		internal static CancellationTokenSource s_GlobalCancellation = new CancellationTokenSource();

		// Token: 0x04000931 RID: 2353
		internal Object[] m_Result;

		// Token: 0x04000932 RID: 2354
		private CancellationToken m_CancellationToken;

		// Token: 0x02000252 RID: 594
		internal new static class BindingsMarshaller
		{
			// Token: 0x06001F28 RID: 7976 RVA: 0x0003BDBC File Offset: 0x00039FBC
			public static AsyncInstantiateOperation ConvertToManaged(IntPtr ptr)
			{
				return new AsyncInstantiateOperation(ptr, CancellationToken.None);
			}

			// Token: 0x06001F29 RID: 7977 RVA: 0x0003BDC9 File Offset: 0x00039FC9
			public static IntPtr ConvertToNative(AsyncInstantiateOperation obj)
			{
				return obj.m_Ptr;
			}
		}
	}
}
