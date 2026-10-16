using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x0200084D RID: 2125
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x000EB9B8 File Offset: 0x000E9BB8
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x000EB9C0 File Offset: 0x000E9BC0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(0) || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00013B79 File Offset: 0x00011D79
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000EB9E8 File Offset: 0x000E9BE8
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle()
		{
			return new SafeFreeContextBufferChannelBinding_SECURITY();
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000EB9F0 File Offset: 0x000E9BF0
		public unsafe static int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, SecPkgContext_Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			int num = -2146893055;
			if (contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ENDPOINT_BINDINGS && contextAttribute != global::Interop.SspiCli.ContextAttribute.SECPKG_ATTR_UNIQUE_BINDINGS)
			{
				return num;
			}
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			if (num == 0 && refHandle != null)
			{
				refHandle.Set(buffer->Bindings);
				refHandle._size = buffer->BindingsLength;
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000EBA6C File Offset: 0x000E9C6C
		public override string ToString()
		{
			if (this.IsInvalid)
			{
				return null;
			}
			byte[] array = new byte[this._size];
			Marshal.Copy(this.handle, array, 0, array.Length);
			return BitConverter.ToString(array).Replace('-', ' ');
		}

		// Token: 0x040028F1 RID: 10481
		private int _size;
	}
}
