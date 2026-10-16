using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000AD8 RID: 2776
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x060062EA RID: 25322 RVA: 0x0014ABD8 File Offset: 0x00148DD8
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060062EB RID: 25323 RVA: 0x0014ABEF File Offset: 0x00148DEF
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x0014ABF7 File Offset: 0x00148DF7
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04003A45 RID: 14917
		private unsafe void* _pointer;

		// Token: 0x04003A46 RID: 14918
		private GCHandle _handle;

		// Token: 0x04003A47 RID: 14919
		private IPinnable _pinnable;
	}
}
