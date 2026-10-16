using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000257 RID: 599
	[NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h")]
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06001F36 RID: 7990
		[NativeMethod(IsThreadSafe = true)]
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x06001F37 RID: 7991
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetManagedObject(IntPtr ptr, [Unmarshalled] AsyncOperation self);

		// Token: 0x06001F38 RID: 7992 RVA: 0x0003BEE9 File Offset: 0x0003A0E9
		public AsyncOperation()
		{
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		protected AsyncOperation(IntPtr ptr)
		{
			bool flag = ptr == IntPtr.Zero;
			if (!flag)
			{
				AsyncOperation.InternalSetManagedObject(ptr, this);
				this.m_Ptr = ptr;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0003BF2C File Offset: 0x0003A12C
		public bool isDone
		{
			[NativeMethod("IsDone")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_isDone_Injected(intPtr);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0003BF50 File Offset: 0x0003A150
		public float progress
		{
			[NativeMethod("GetProgress")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_progress_Injected(intPtr);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0003BF74 File Offset: 0x0003A174
		// (set) Token: 0x06001F3D RID: 7997 RVA: 0x0003BF98 File Offset: 0x0003A198
		public int priority
		{
			[NativeMethod("GetPriority")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_priority_Injected(intPtr);
			}
			[NativeMethod("SetPriority")]
			set
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AsyncOperation.set_priority_Injected(intPtr, value);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0003BFBC File Offset: 0x0003A1BC
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
		public bool allowSceneActivation
		{
			[NativeMethod("GetAllowSceneActivation")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_allowSceneActivation_Injected(intPtr);
			}
			[NativeMethod("SetAllowSceneActivation")]
			set
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AsyncOperation.set_allowSceneActivation_Injected(intPtr, value);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0003C004 File Offset: 0x0003A204
		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0003C03C File Offset: 0x0003A23C
		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback(this);
				this.m_completeCallback = null;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001F42 RID: 8002 RVA: 0x0003C070 File Offset: 0x0003A270
		// (remove) Token: 0x06001F43 RID: 8003 RVA: 0x0003C0AD File Offset: 0x0003A2AD
		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		// Token: 0x06001F44 RID: 8004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isDone_Injected(IntPtr _unity_self);

		// Token: 0x06001F45 RID: 8005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_progress_Injected(IntPtr _unity_self);

		// Token: 0x06001F46 RID: 8006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_priority_Injected(IntPtr _unity_self);

		// Token: 0x06001F47 RID: 8007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_priority_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001F48 RID: 8008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowSceneActivation_Injected(IntPtr _unity_self);

		// Token: 0x06001F49 RID: 8009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowSceneActivation_Injected(IntPtr _unity_self, bool value);

		// Token: 0x04000935 RID: 2357
		[VisibleToOtherModules(new string[] { "UnityEngine.AssetBundleModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000936 RID: 2358
		private Action<AsyncOperation> m_completeCallback;

		// Token: 0x02000258 RID: 600
		internal static class BindingsMarshaller
		{
			// Token: 0x06001F4A RID: 8010 RVA: 0x0003C0C7 File Offset: 0x0003A2C7
			public static AsyncOperation ConvertToManaged(IntPtr ptr)
			{
				return new AsyncOperation(ptr);
			}

			// Token: 0x06001F4B RID: 8011 RVA: 0x0003BDC9 File Offset: 0x00039FC9
			public static IntPtr ConvertToNative(AsyncOperation asyncOperation)
			{
				return asyncOperation.m_Ptr;
			}
		}
	}
}
