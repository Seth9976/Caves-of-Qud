using System;
using System.Runtime.InteropServices;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000E RID: 14
	internal class NativeStr : IDisposable
	{
		// Token: 0x1700002C RID: 44
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002674 File Offset: 0x00000874
		public string Str
		{
			set
			{
				this.m_Str = value;
				this.Dispose();
				bool flag = value != null;
				if (flag)
				{
					this.m_MarshalledString = Marshal.StringToHGlobalUni(this.m_Str);
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000026AC File Offset: 0x000008AC
		public IntPtr Ptr
		{
			get
			{
				return this.m_MarshalledString;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026C4 File Offset: 0x000008C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000026D8 File Offset: 0x000008D8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_MarshalledString != IntPtr.Zero;
			if (flag)
			{
				Marshal.FreeHGlobal(this.m_MarshalledString);
				this.m_MarshalledString = IntPtr.Zero;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002714 File Offset: 0x00000914
		~NativeStr()
		{
			this.Dispose(false);
		}

		// Token: 0x04000045 RID: 69
		private string m_Str = null;

		// Token: 0x04000046 RID: 70
		private IntPtr m_MarshalledString = IntPtr.Zero;
	}
}
