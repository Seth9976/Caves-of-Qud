using System;
using System.Windows.Forms;

namespace SFB
{
	// Token: 0x0200018C RID: 396
	public class WindowWrapper : IWin32Window
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x0005FBDB File Offset: 0x0005DDDB
		public WindowWrapper(IntPtr handle)
		{
			this._hwnd = handle;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0005FBEA File Offset: 0x0005DDEA
		public IntPtr Handle
		{
			get
			{
				return this._hwnd;
			}
		}

		// Token: 0x04000A6E RID: 2670
		private IntPtr _hwnd;
	}
}
