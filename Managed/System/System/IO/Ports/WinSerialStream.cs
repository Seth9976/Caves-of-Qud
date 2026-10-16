using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Ports
{
	// Token: 0x0200053D RID: 1341
	internal class WinSerialStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x06002B38 RID: 11064
		[DllImport("kernel32", SetLastError = true)]
		private static extern int CreateFile(string port_name, uint desired_access, uint share_mode, uint security_attrs, uint creation, uint flags, uint template);

		// Token: 0x06002B39 RID: 11065
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetupComm(int handle, int read_buffer_size, int write_buffer_size);

		// Token: 0x06002B3A RID: 11066
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool PurgeComm(int handle, uint flags);

		// Token: 0x06002B3B RID: 11067
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommTimeouts(int handle, Timeouts timeouts);

		// Token: 0x06002B3C RID: 11068 RVA: 0x00093ED8 File Offset: 0x000920D8
		public WinSerialStream(string port_name, int baud_rate, int data_bits, Parity parity, StopBits sb, bool dtr_enable, bool rts_enable, Handshake hs, int read_timeout, int write_timeout, int read_buffer_size, int write_buffer_size)
		{
			this.handle = WinSerialStream.CreateFile((port_name != null && !port_name.StartsWith("\\\\.\\")) ? ("\\\\.\\" + port_name) : port_name, 3221225472U, 0U, 0U, 3U, 1073741824U, 0U);
			if (this.handle == -1)
			{
				this.ReportIOError(port_name);
			}
			this.SetAttributes(baud_rate, parity, data_bits, sb, hs);
			if (!WinSerialStream.PurgeComm(this.handle, 12U) || !WinSerialStream.SetupComm(this.handle, read_buffer_size, write_buffer_size))
			{
				this.ReportIOError(null);
			}
			this.read_timeout = read_timeout;
			this.write_timeout = write_timeout;
			this.timeouts = new Timeouts(read_timeout, write_timeout);
			if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
			{
				this.ReportIOError(null);
			}
			this.SetSignal(SerialSignal.Dtr, dtr_enable);
			if (hs != Handshake.RequestToSend && hs != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rts_enable);
			}
			NativeOverlapped nativeOverlapped = default(NativeOverlapped);
			this.write_event = new ManualResetEvent(false);
			nativeOverlapped.EventHandle = this.write_event.Handle;
			this.write_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(nativeOverlapped, this.write_overlapped, true);
			NativeOverlapped nativeOverlapped2 = default(NativeOverlapped);
			this.read_event = new ManualResetEvent(false);
			nativeOverlapped2.EventHandle = this.read_event.Handle;
			this.read_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(nativeOverlapped2, this.read_overlapped, true);
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x00094057 File Offset: 0x00092257
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x00094060 File Offset: 0x00092260
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(value, this.write_timeout);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000940B3 File Offset: 0x000922B3
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x000940BC File Offset: 0x000922BC
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(this.read_timeout, value);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002B48 RID: 11080
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool CloseHandle(int handle);

		// Token: 0x06002B49 RID: 11081 RVA: 0x0009410F File Offset: 0x0009230F
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			WinSerialStream.CloseHandle(this.handle);
			Marshal.FreeHGlobal(this.write_overlapped);
			Marshal.FreeHGlobal(this.read_overlapped);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00093D41 File Offset: 0x00091F41
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0000BC79 File Offset: 0x00009E79
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00094144 File Offset: 0x00092344
		~WinSerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x00094174 File Offset: 0x00092374
		public override void Flush()
		{
			this.CheckDisposed();
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B50 RID: 11088
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool ReadFile(int handle, byte* buffer, int bytes_to_read, out int bytes_read, IntPtr overlapped);

		// Token: 0x06002B51 RID: 11089
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetOverlappedResult(int handle, IntPtr overlapped, ref int bytes_transfered, bool wait);

		// Token: 0x06002B52 RID: 11090 RVA: 0x0009417C File Offset: 0x0009237C
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.ReadFile(this.handle, ptr + offset, count, out num, this.read_overlapped))
				{
					return num;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.read_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x06002B53 RID: 11091
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool WriteFile(int handle, byte* buffer, int bytes_to_write, out int bytes_written, IntPtr overlapped);

		// Token: 0x06002B54 RID: 11092 RVA: 0x0009423C File Offset: 0x0009243C
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num = 0;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.WriteFile(this.handle, ptr + offset, count, out num, this.write_overlapped))
				{
					return;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.write_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num < count)
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x06002B55 RID: 11093
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommState(int handle, [Out] DCB dcb);

		// Token: 0x06002B56 RID: 11094
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommState(int handle, DCB dcb);

		// Token: 0x06002B57 RID: 11095 RVA: 0x000942F8 File Offset: 0x000924F8
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits bits, Handshake hs)
		{
			DCB dcb = new DCB();
			if (!WinSerialStream.GetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
			dcb.SetValues(baud_rate, parity, data_bits, bits, hs);
			if (!WinSerialStream.SetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00094344 File Offset: 0x00092544
		private void ReportIOError(string optional_arg)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			string text;
			if (lastWin32Error - 2 > 1)
			{
				if (lastWin32Error != 87)
				{
					text = new Win32Exception().Message;
				}
				else
				{
					text = "Parameter is incorrect.";
				}
			}
			else
			{
				text = "The port `" + optional_arg + "' does not exist.";
			}
			throw new IOException(text);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00094390 File Offset: 0x00092590
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000943AB File Offset: 0x000925AB
		public void DiscardInBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 8U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000943C2 File Offset: 0x000925C2
		public void DiscardOutBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 4U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B5C RID: 11100
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool ClearCommError(int handle, out uint errors, out CommStat stat);

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000943DC File Offset: 0x000925DC
		public int BytesToRead
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesIn;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x00094408 File Offset: 0x00092608
		public int BytesToWrite
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesOut;
			}
		}

		// Token: 0x06002B5F RID: 11103
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommModemStatus(int handle, out uint flags);

		// Token: 0x06002B60 RID: 11104 RVA: 0x00094434 File Offset: 0x00092634
		public SerialSignal GetSignals()
		{
			uint num;
			if (!WinSerialStream.GetCommModemStatus(this.handle, out num))
			{
				this.ReportIOError(null);
			}
			SerialSignal serialSignal = SerialSignal.None;
			if ((num & 128U) != 0U)
			{
				serialSignal |= SerialSignal.Cd;
			}
			if ((num & 16U) != 0U)
			{
				serialSignal |= SerialSignal.Cts;
			}
			if ((num & 32U) != 0U)
			{
				serialSignal |= SerialSignal.Dsr;
			}
			return serialSignal;
		}

		// Token: 0x06002B61 RID: 11105
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool EscapeCommFunction(int handle, uint flags);

		// Token: 0x06002B62 RID: 11106 RVA: 0x0009447C File Offset: 0x0009267C
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal != SerialSignal.Rts && signal != SerialSignal.Dtr)
			{
				throw new Exception("Wrong internal value");
			}
			uint num;
			if (signal == SerialSignal.Rts)
			{
				if (value)
				{
					num = 3U;
				}
				else
				{
					num = 4U;
				}
			}
			else if (value)
			{
				num = 5U;
			}
			else
			{
				num = 6U;
			}
			if (!WinSerialStream.EscapeCommFunction(this.handle, num))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000944CB File Offset: 0x000926CB
		public void SetBreakState(bool value)
		{
			if (!WinSerialStream.EscapeCommFunction(this.handle, value ? 8U : 9U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x0400176E RID: 5998
		private const uint GenericRead = 2147483648U;

		// Token: 0x0400176F RID: 5999
		private const uint GenericWrite = 1073741824U;

		// Token: 0x04001770 RID: 6000
		private const uint OpenExisting = 3U;

		// Token: 0x04001771 RID: 6001
		private const uint FileFlagOverlapped = 1073741824U;

		// Token: 0x04001772 RID: 6002
		private const uint PurgeRxClear = 8U;

		// Token: 0x04001773 RID: 6003
		private const uint PurgeTxClear = 4U;

		// Token: 0x04001774 RID: 6004
		private const uint WinInfiniteTimeout = 4294967295U;

		// Token: 0x04001775 RID: 6005
		private const uint FileIOPending = 997U;

		// Token: 0x04001776 RID: 6006
		private const uint SetRts = 3U;

		// Token: 0x04001777 RID: 6007
		private const uint ClearRts = 4U;

		// Token: 0x04001778 RID: 6008
		private const uint SetDtr = 5U;

		// Token: 0x04001779 RID: 6009
		private const uint ClearDtr = 6U;

		// Token: 0x0400177A RID: 6010
		private const uint SetBreak = 8U;

		// Token: 0x0400177B RID: 6011
		private const uint ClearBreak = 9U;

		// Token: 0x0400177C RID: 6012
		private const uint CtsOn = 16U;

		// Token: 0x0400177D RID: 6013
		private const uint DsrOn = 32U;

		// Token: 0x0400177E RID: 6014
		private const uint RsldOn = 128U;

		// Token: 0x0400177F RID: 6015
		private const uint EvRxChar = 1U;

		// Token: 0x04001780 RID: 6016
		private const uint EvCts = 8U;

		// Token: 0x04001781 RID: 6017
		private const uint EvDsr = 16U;

		// Token: 0x04001782 RID: 6018
		private const uint EvRlsd = 32U;

		// Token: 0x04001783 RID: 6019
		private const uint EvBreak = 64U;

		// Token: 0x04001784 RID: 6020
		private const uint EvErr = 128U;

		// Token: 0x04001785 RID: 6021
		private const uint EvRing = 256U;

		// Token: 0x04001786 RID: 6022
		private int handle;

		// Token: 0x04001787 RID: 6023
		private int read_timeout;

		// Token: 0x04001788 RID: 6024
		private int write_timeout;

		// Token: 0x04001789 RID: 6025
		private bool disposed;

		// Token: 0x0400178A RID: 6026
		private IntPtr write_overlapped;

		// Token: 0x0400178B RID: 6027
		private IntPtr read_overlapped;

		// Token: 0x0400178C RID: 6028
		private ManualResetEvent read_event;

		// Token: 0x0400178D RID: 6029
		private ManualResetEvent write_event;

		// Token: 0x0400178E RID: 6030
		private Timeouts timeouts;
	}
}
