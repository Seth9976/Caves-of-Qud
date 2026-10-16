using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x02000539 RID: 1337
	internal class SerialPortStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x06002B08 RID: 11016
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int open_serial(string portName);

		// Token: 0x06002B09 RID: 11017 RVA: 0x00093B68 File Offset: 0x00091D68
		public SerialPortStream(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, bool dtrEnable, bool rtsEnable, Handshake handshake, int readTimeout, int writeTimeout, int readBufferSize, int writeBufferSize)
		{
			this.fd = SerialPortStream.open_serial(portName);
			if (this.fd == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			this.TryBaudRate(baudRate);
			if (!SerialPortStream.set_attributes(this.fd, baudRate, parity, dataBits, stopBits, handshake))
			{
				SerialPortStream.ThrowIOException();
			}
			this.read_timeout = readTimeout;
			this.write_timeout = writeTimeout;
			this.SetSignal(SerialSignal.Dtr, dtrEnable);
			if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rtsEnable);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x00093BE3 File Offset: 0x00091DE3
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x00093BEB File Offset: 0x00091DEB
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
				this.read_timeout = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x00093C07 File Offset: 0x00091E07
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x00093C0F File Offset: 0x00091E0F
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
				this.write_timeout = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x06002B15 RID: 11029 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x06002B16 RID: 11030
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int read_serial(int fd, byte[] buffer, int offset, int count);

		// Token: 0x06002B17 RID: 11031
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool poll_serial(int fd, out int error, int timeout);

		// Token: 0x06002B18 RID: 11032 RVA: 0x00093C2C File Offset: 0x00091E2C
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
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
			bool flag = SerialPortStream.poll_serial(this.fd, out num, this.read_timeout);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			if (!flag)
			{
				throw new TimeoutException();
			}
			int num2 = SerialPortStream.read_serial(this.fd, buffer, offset, count);
			if (num2 == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return num2;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B1B RID: 11035
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int write_serial(int fd, byte[] buffer, int offset, int count, int timeout);

		// Token: 0x06002B1C RID: 11036 RVA: 0x00093CB4 File Offset: 0x00091EB4
		public override void Write(byte[] buffer, int offset, int count)
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
			if (SerialPortStream.write_serial(this.fd, buffer, offset, count, this.write_timeout) < 0)
			{
				throw new TimeoutException("The operation has timed-out");
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x00093D1D File Offset: 0x00091F1D
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (SerialPortStream.close_serial(this.fd) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B1E RID: 11038
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int close_serial(int fd);

		// Token: 0x06002B1F RID: 11039 RVA: 0x0000BC79 File Offset: 0x00009E79
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x00093D41 File Offset: 0x00091F41
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x00093D50 File Offset: 0x00091F50
		~SerialPortStream()
		{
			try
			{
				this.Dispose(false);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x00093D8C File Offset: 0x00091F8C
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06002B23 RID: 11043
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool set_attributes(int fd, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake);

		// Token: 0x06002B24 RID: 11044 RVA: 0x00093DA7 File Offset: 0x00091FA7
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs)
		{
			if (!SerialPortStream.set_attributes(this.fd, baud_rate, parity, data_bits, sb, hs))
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B25 RID: 11045
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int get_bytes_in_buffer(int fd, int input);

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x00093DC2 File Offset: 0x00091FC2
		public int BytesToRead
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 1);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x00093DD9 File Offset: 0x00091FD9
		public int BytesToWrite
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 0);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x06002B28 RID: 11048
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int discard_buffer(int fd, bool inputBuffer);

		// Token: 0x06002B29 RID: 11049 RVA: 0x00093DF0 File Offset: 0x00091FF0
		public void DiscardInBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, true) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00093E05 File Offset: 0x00092005
		public void DiscardOutBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, false) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B2B RID: 11051
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern SerialSignal get_signals(int fd, out int error);

		// Token: 0x06002B2C RID: 11052 RVA: 0x00093E1C File Offset: 0x0009201C
		public SerialSignal GetSignals()
		{
			int num;
			SerialSignal serialSignal = SerialPortStream.get_signals(this.fd, out num);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return serialSignal;
		}

		// Token: 0x06002B2D RID: 11053
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int set_signal(int fd, SerialSignal signal, bool value);

		// Token: 0x06002B2E RID: 11054 RVA: 0x00093E3F File Offset: 0x0009203F
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal < SerialSignal.Cd || signal > SerialSignal.Rts || signal == SerialSignal.Cd || signal == SerialSignal.Cts || signal == SerialSignal.Dsr)
			{
				throw new Exception("Invalid internal value");
			}
			if (SerialPortStream.set_signal(this.fd, signal, value) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B2F RID: 11055
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int breakprop(int fd);

		// Token: 0x06002B30 RID: 11056 RVA: 0x00093E76 File Offset: 0x00092076
		public void SetBreakState(bool value)
		{
			if (value && SerialPortStream.breakprop(this.fd) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06002B31 RID: 11057
		[DllImport("libc")]
		private static extern IntPtr strerror(int errnum);

		// Token: 0x06002B32 RID: 11058 RVA: 0x00093E8E File Offset: 0x0009208E
		private static void ThrowIOException()
		{
			throw new IOException(Marshal.PtrToStringAnsi(SerialPortStream.strerror(Marshal.GetLastWin32Error())));
		}

		// Token: 0x06002B33 RID: 11059
		[DllImport("MonoPosixHelper")]
		private static extern bool is_baud_rate_legal(int baud_rate);

		// Token: 0x06002B34 RID: 11060 RVA: 0x00093EA4 File Offset: 0x000920A4
		private void TryBaudRate(int baudRate)
		{
			if (!SerialPortStream.is_baud_rate_legal(baudRate))
			{
				throw new ArgumentOutOfRangeException("baudRate", "Given baud rate is not supported on this platform.");
			}
		}

		// Token: 0x0400175D RID: 5981
		private int fd;

		// Token: 0x0400175E RID: 5982
		private int read_timeout;

		// Token: 0x0400175F RID: 5983
		private int write_timeout;

		// Token: 0x04001760 RID: 5984
		private bool disposed;
	}
}
