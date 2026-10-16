using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.AMD
{
	// Token: 0x02000009 RID: 9
	internal class NativeData<T> : IDisposable where T : struct
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002130 File Offset: 0x00000330
		public IntPtr Ptr
		{
			get
			{
				UnsafeUtility.CopyStructureToPtr<T>(ref this.Value, this.m_MarshalledValue.ToPointer());
				return this.m_MarshalledValue;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002161 File Offset: 0x00000361
		public NativeData()
		{
			this.m_MarshalledValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000219B File Offset: 0x0000039B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021B0 File Offset: 0x000003B0
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_MarshalledValue != IntPtr.Zero;
			if (flag)
			{
				Marshal.FreeHGlobal(this.m_MarshalledValue);
				this.m_MarshalledValue = IntPtr.Zero;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021EC File Offset: 0x000003EC
		~NativeData()
		{
			this.Dispose(false);
		}

		// Token: 0x04000035 RID: 53
		private IntPtr m_MarshalledValue = IntPtr.Zero;

		// Token: 0x04000036 RID: 54
		public T Value = new T();
	}
}
