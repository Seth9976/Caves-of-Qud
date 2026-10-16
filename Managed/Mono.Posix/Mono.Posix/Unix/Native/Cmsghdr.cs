using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000064 RID: 100
	[Map("struct cmsghdr")]
	[CLSCompliant(false)]
	public struct Cmsghdr
	{
		// Token: 0x0600042E RID: 1070
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Cmsghdr_getsize", SetLastError = true)]
		private static extern int getsize();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		public static int Size
		{
			get
			{
				return Cmsghdr.size;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		public unsafe static Cmsghdr ReadFromBuffer(Msghdr msgh, long cmsg)
		{
			if (msgh == null)
			{
				throw new ArgumentNullException("msgh");
			}
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] array;
			byte* ptr;
			if ((array = msgh.msg_control) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Cmsghdr cmsghdr;
			if (!NativeConvert.TryCopy((IntPtr)((void*)(ptr + cmsg)), out cmsghdr))
			{
				throw new ArgumentException("Failed to convert from native struct", "buffer");
			}
			array = null;
			if (NativeConvert.FromUnixSocketProtocol(cmsghdr.cmsg_level) == NativeConvert.FromUnixSocketProtocol(UnixSocketProtocol.SOL_SOCKET))
			{
				cmsghdr.cmsg_level = UnixSocketProtocol.SOL_SOCKET;
			}
			return cmsghdr;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000AD98 File Offset: 0x00008F98
		public unsafe void WriteToBuffer(Msghdr msgh, long cmsg)
		{
			if (msgh == null)
			{
				throw new ArgumentNullException("msgh");
			}
			if (msgh.msg_control == null || msgh.msg_controllen > (long)msgh.msg_control.Length)
			{
				throw new ArgumentException("msgh.msg_control == null || msgh.msg_controllen > msgh.msg_control.Length", "msgh");
			}
			if (cmsg < 0L || cmsg + (long)Cmsghdr.Size > msgh.msg_controllen)
			{
				throw new ArgumentException("cmsg offset pointing out of buffer", "cmsg");
			}
			byte[] array;
			byte* ptr;
			if ((array = msgh.msg_control) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy(ref this, (IntPtr)((void*)(ptr + cmsg))))
			{
				throw new ArgumentException("Failed to convert to native struct", "buffer");
			}
			array = null;
		}

		// Token: 0x0400046A RID: 1130
		public long cmsg_len;

		// Token: 0x0400046B RID: 1131
		public UnixSocketProtocol cmsg_level;

		// Token: 0x0400046C RID: 1132
		public UnixSocketControlMessage cmsg_type;

		// Token: 0x0400046D RID: 1133
		private static readonly int size = Cmsghdr.getsize();
	}
}
