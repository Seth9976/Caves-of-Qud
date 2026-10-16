using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Web
{
	// Token: 0x02000043 RID: 67
	internal sealed class HttpChannelBindingToken : ChannelBinding
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000722A File Offset: 0x0000542A
		internal HttpChannelBindingToken(IntPtr token, int tokenSize)
		{
			base.SetHandle(token);
			this._size = tokenSize;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00007240 File Offset: 0x00005440
		protected override bool ReleaseHandle()
		{
			base.SetHandle(IntPtr.Zero);
			this._size = 0;
			return true;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00007255 File Offset: 0x00005455
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private int _size;
	}
}
