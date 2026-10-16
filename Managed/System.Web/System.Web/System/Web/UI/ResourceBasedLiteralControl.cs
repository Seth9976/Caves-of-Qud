using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x0200021C RID: 540
	internal class ResourceBasedLiteralControl : LiteralControl
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x000390DB File Offset: 0x000372DB
		public ResourceBasedLiteralControl(IntPtr ptr, int length)
		{
			this.EnableViewState = false;
			base.AutoID = false;
			this.ptr = ptr;
			this.length = length;
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00039100 File Offset: 0x00037300
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00039147 File Offset: 0x00037347
		public override string Text
		{
			get
			{
				if (this.length == -1)
				{
					return base.Text;
				}
				byte[] array = new byte[this.length];
				Marshal.Copy(this.ptr, array, 0, this.length);
				return Encoding.UTF8.GetString(array);
			}
			set
			{
				this.length = -1;
				base.Text = value;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00039158 File Offset: 0x00037358
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.length == -1)
			{
				writer.Write(base.Text);
				return;
			}
			HttpWriter httpWriter = writer.GetHttpWriter();
			if (httpWriter == null || httpWriter.Response.ContentEncoding.CodePage != 65001)
			{
				byte[] array = new byte[this.length];
				Marshal.Copy(this.ptr, array, 0, this.length);
				writer.Write(Encoding.UTF8.GetString(array));
				return;
			}
			httpWriter.WriteUTF8Ptr(this.ptr, this.length);
		}

		// Token: 0x0400155B RID: 5467
		private IntPtr ptr;

		// Token: 0x0400155C RID: 5468
		private int length;
	}
}
