using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200016C RID: 364
	internal sealed class PinnedObject : CriticalDisposableObject
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x000228C8 File Offset: 0x00020AC8
		[NullableContext(1)]
		public PinnedObject(object obj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._handle = GCHandle.Alloc(obj, 3);
				this._isValid = 1;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00022908 File Offset: 0x00020B08
		protected override void Release()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._handle.Free();
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00022948 File Offset: 0x00020B48
		public unsafe byte* Pointer
		{
			get
			{
				return (byte*)(void*)this._handle.AddrOfPinnedObject();
			}
		}

		// Token: 0x04000940 RID: 2368
		private GCHandle _handle;

		// Token: 0x04000941 RID: 2369
		private int _isValid;
	}
}
