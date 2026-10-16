using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000D4 RID: 212
	internal class MonoBtlsBioMono : MonoBtlsBio
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0000CE04 File Offset: 0x0000B004
		public MonoBtlsBioMono(IMonoBtlsBioMono backend)
			: base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMono.mono_btls_bio_mono_new()))
		{
			this.backend = backend;
			this.handle = GCHandle.Alloc(this);
			this.instance = GCHandle.ToIntPtr(this.handle);
			this.readFunc = new MonoBtlsBioMono.BioReadFunc(MonoBtlsBioMono.OnRead);
			this.writeFunc = new MonoBtlsBioMono.BioWriteFunc(MonoBtlsBioMono.OnWrite);
			this.controlFunc = new MonoBtlsBioMono.BioControlFunc(MonoBtlsBioMono.Control);
			this.readFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioReadFunc>(this.readFunc);
			this.writeFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioWriteFunc>(this.writeFunc);
			this.controlFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsBioMono.BioControlFunc>(this.controlFunc);
			MonoBtlsBioMono.mono_btls_bio_mono_initialize(base.Handle.DangerousGetHandle(), this.instance, this.readFuncPtr, this.writeFuncPtr, this.controlFuncPtr);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000CED6 File Offset: 0x0000B0D6
		public static MonoBtlsBioMono CreateStream(Stream stream, bool ownsStream)
		{
			return new MonoBtlsBioMono(new MonoBtlsBioMono.StreamBackend(stream, ownsStream));
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		public static MonoBtlsBioMono CreateString(StringWriter writer)
		{
			return new MonoBtlsBioMono(new MonoBtlsBioMono.StringBackend(writer));
		}

		// Token: 0x0600042B RID: 1067
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mono_new();

		// Token: 0x0600042C RID: 1068
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_mono_initialize(IntPtr handle, IntPtr instance, IntPtr readFunc, IntPtr writeFunc, IntPtr controlFunc);

		// Token: 0x0600042D RID: 1069 RVA: 0x0000CEF1 File Offset: 0x0000B0F1
		private long Control(MonoBtlsBioMono.ControlCommand command, long arg)
		{
			if (command == MonoBtlsBioMono.ControlCommand.Flush)
			{
				this.backend.Flush();
				return 1L;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000CF0C File Offset: 0x0000B10C
		private int OnRead(IntPtr data, int dataLength, out int wantMore)
		{
			byte[] array = new byte[dataLength];
			bool flag;
			int num = this.backend.Read(array, 0, dataLength, out flag);
			wantMore = (flag ? 1 : 0);
			if (num <= 0)
			{
				return num;
			}
			Marshal.Copy(array, 0, data, num);
			return num;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000CF4C File Offset: 0x0000B14C
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioReadFunc))]
		private static int OnRead(IntPtr instance, IntPtr data, int dataLength, out int wantMore)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsBioMono.OnRead(data, dataLength, out wantMore);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				wantMore = 0;
				num = -1;
			}
			return num;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000CF9C File Offset: 0x0000B19C
		private int OnWrite(IntPtr data, int dataLength)
		{
			byte[] array = new byte[dataLength];
			Marshal.Copy(data, array, 0, dataLength);
			if (!this.backend.Write(array, 0, dataLength))
			{
				return -1;
			}
			return dataLength;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioWriteFunc))]
		private static int OnWrite(IntPtr instance, IntPtr data, int dataLength)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsBioMono.OnWrite(data, dataLength);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				num = -1;
			}
			return num;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000D018 File Offset: 0x0000B218
		[MonoPInvokeCallback(typeof(MonoBtlsBioMono.BioControlFunc))]
		private static long Control(IntPtr instance, MonoBtlsBioMono.ControlCommand command, long arg)
		{
			MonoBtlsBioMono monoBtlsBioMono = (MonoBtlsBioMono)GCHandle.FromIntPtr(instance).Target;
			long num;
			try
			{
				num = monoBtlsBioMono.Control(command, arg);
			}
			catch (Exception ex)
			{
				monoBtlsBioMono.SetException(ex);
				num = -1L;
			}
			return num;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000D064 File Offset: 0x0000B264
		protected override void Close()
		{
			try
			{
				if (this.backend != null)
				{
					this.backend.Close();
					this.backend = null;
				}
				if (this.handle.IsAllocated)
				{
					this.handle.Free();
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x040003B6 RID: 950
		private GCHandle handle;

		// Token: 0x040003B7 RID: 951
		private IntPtr instance;

		// Token: 0x040003B8 RID: 952
		private MonoBtlsBioMono.BioReadFunc readFunc;

		// Token: 0x040003B9 RID: 953
		private MonoBtlsBioMono.BioWriteFunc writeFunc;

		// Token: 0x040003BA RID: 954
		private MonoBtlsBioMono.BioControlFunc controlFunc;

		// Token: 0x040003BB RID: 955
		private IntPtr readFuncPtr;

		// Token: 0x040003BC RID: 956
		private IntPtr writeFuncPtr;

		// Token: 0x040003BD RID: 957
		private IntPtr controlFuncPtr;

		// Token: 0x040003BE RID: 958
		private IMonoBtlsBioMono backend;

		// Token: 0x020000D5 RID: 213
		private enum ControlCommand
		{
			// Token: 0x040003C0 RID: 960
			Flush = 1
		}

		// Token: 0x020000D6 RID: 214
		// (Invoke) Token: 0x06000435 RID: 1077
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BioReadFunc(IntPtr bio, IntPtr data, int dataLength, out int wantMore);

		// Token: 0x020000D7 RID: 215
		// (Invoke) Token: 0x06000439 RID: 1081
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BioWriteFunc(IntPtr bio, IntPtr data, int dataLength);

		// Token: 0x020000D8 RID: 216
		// (Invoke) Token: 0x0600043D RID: 1085
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate long BioControlFunc(IntPtr bio, MonoBtlsBioMono.ControlCommand command, long arg);

		// Token: 0x020000D9 RID: 217
		private class StreamBackend : IMonoBtlsBioMono
		{
			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000D0BC File Offset: 0x0000B2BC
			public Stream InnerStream
			{
				get
				{
					return this.stream;
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
			public StreamBackend(Stream stream, bool ownsStream)
			{
				this.stream = stream;
				this.ownsStream = ownsStream;
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x0000D0DA File Offset: 0x0000B2DA
			public int Read(byte[] buffer, int offset, int size, out bool wantMore)
			{
				wantMore = false;
				return this.stream.Read(buffer, offset, size);
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x0000D0EE File Offset: 0x0000B2EE
			public bool Write(byte[] buffer, int offset, int size)
			{
				this.stream.Write(buffer, offset, size);
				return true;
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x0000D0FF File Offset: 0x0000B2FF
			public void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x0000D10C File Offset: 0x0000B30C
			public void Close()
			{
				if (this.ownsStream && this.stream != null)
				{
					this.stream.Dispose();
				}
				this.stream = null;
			}

			// Token: 0x040003C1 RID: 961
			private Stream stream;

			// Token: 0x040003C2 RID: 962
			private bool ownsStream;
		}

		// Token: 0x020000DA RID: 218
		private class StringBackend : IMonoBtlsBioMono
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x0000D130 File Offset: 0x0000B330
			public StringBackend(StringWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x0000D14A File Offset: 0x0000B34A
			public int Read(byte[] buffer, int offset, int size, out bool wantMore)
			{
				wantMore = false;
				return -1;
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x0000D154 File Offset: 0x0000B354
			public bool Write(byte[] buffer, int offset, int size)
			{
				string @string = this.encoding.GetString(buffer, offset, size);
				this.writer.Write(@string);
				return true;
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00003917 File Offset: 0x00001B17
			public void Flush()
			{
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00003917 File Offset: 0x00001B17
			public void Close()
			{
			}

			// Token: 0x040003C3 RID: 963
			private StringWriter writer;

			// Token: 0x040003C4 RID: 964
			private Encoding encoding = new UTF8Encoding();
		}
	}
}
