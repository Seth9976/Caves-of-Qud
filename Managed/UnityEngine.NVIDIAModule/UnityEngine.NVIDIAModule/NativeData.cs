using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000D RID: 13
	internal class NativeData<T> : IDisposable where T : struct
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002584 File Offset: 0x00000784
		public IntPtr Ptr
		{
			get
			{
				UnsafeUtility.CopyStructureToPtr<T>(ref this.Value, this.m_MarshalledValue.ToPointer());
				return this.m_MarshalledValue;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000025B5 File Offset: 0x000007B5
		public NativeData()
		{
			this.m_MarshalledValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000025EF File Offset: 0x000007EF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002604 File Offset: 0x00000804
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_MarshalledValue != IntPtr.Zero;
			if (flag)
			{
				Marshal.FreeHGlobal(this.m_MarshalledValue);
				this.m_MarshalledValue = IntPtr.Zero;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002640 File Offset: 0x00000840
		~NativeData()
		{
			this.Dispose(false);
		}

		// Token: 0x04000043 RID: 67
		private IntPtr m_MarshalledValue = IntPtr.Zero;

		// Token: 0x04000044 RID: 68
		public T Value = new T();
	}
}
