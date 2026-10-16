using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007E8 RID: 2024
	internal class QuotedPrintableStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x060040BD RID: 16573 RVA: 0x000DD6AC File Offset: 0x000DB8AC
		internal QuotedPrintableStream(Stream stream, int lineLength)
			: base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this._lineLength = lineLength;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000DD6CB File Offset: 0x000DB8CB
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF)
			: this(stream, 70)
		{
			this._encodeCRLF = encodeCRLF;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000DD6E0 File Offset: 0x000DB8E0
		private QuotedPrintableStream.ReadStateInfo ReadState
		{
			get
			{
				QuotedPrintableStream.ReadStateInfo readStateInfo;
				if ((readStateInfo = this._readState) == null)
				{
					readStateInfo = (this._readState = new QuotedPrintableStream.ReadStateInfo());
				}
				return readStateInfo;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000DD708 File Offset: 0x000DB908
		internal WriteStateInfoBase WriteState
		{
			get
			{
				WriteStateInfoBase writeStateInfoBase;
				if ((writeStateInfoBase = this._writeState) == null)
				{
					writeStateInfoBase = (this._writeState = new WriteStateInfoBase(1024, null, null, this._lineLength));
				}
				return writeStateInfoBase;
			}
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000DD73C File Offset: 0x000DB93C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			QuotedPrintableStream.WriteAsyncResult writeAsyncResult = new QuotedPrintableStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000DD792 File Offset: 0x000DB992
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000DD7A0 File Offset: 0x000DB9A0
		public unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			byte* ptr2 = ptr + offset;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + count;
			if (this.ReadState.IsEscaped)
			{
				if (this.ReadState.Byte == -1)
				{
					if (count == 1)
					{
						this.ReadState.Byte = (short)(*ptr3);
						return 0;
					}
					if (*ptr3 != 13 || ptr3[1] != 10)
					{
						byte b = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
						byte b2 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
						if (b == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b));
						}
						if (b2 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b2));
						}
						*(ptr4++) = (byte)(((int)b << 4) + (int)b2);
					}
					ptr3 += 2;
				}
				else
				{
					if (this.ReadState.Byte != 13 || *ptr3 != 10)
					{
						byte b3 = QuotedPrintableStream.s_hexDecodeMap[(int)this.ReadState.Byte];
						byte b4 = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
						if (b3 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b3));
						}
						if (b4 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b4));
						}
						*(ptr4++) = (byte)(((int)b3 << 4) + (int)b4);
					}
					ptr3++;
				}
				this.ReadState.IsEscaped = false;
				this.ReadState.Byte = -1;
			}
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 61)
				{
					long num = (long)(ptr5 - ptr3);
					if (num != 1L)
					{
						if (num != 2L)
						{
							if (ptr3[1] != 13 || ptr3[2] != 10)
							{
								byte b5 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
								byte b6 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[2]];
								if (b5 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b5));
								}
								if (b6 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b6));
								}
								*(ptr4++) = (byte)(((int)b5 << 4) + (int)b6);
							}
							ptr3 += 3;
							continue;
						}
						this.ReadState.Byte = (short)ptr3[1];
					}
					this.ReadState.IsEscaped = true;
					break;
				}
				*(ptr4++) = *(ptr3++);
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x000DDA04 File Offset: 0x000DBC04
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this._lineLength != -1 && this.WriteState.CurrentLineLength + 3 + 2 >= this._lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this._writeState.CurrentLineLength + 3 + 2 >= 70)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.AppendCRLF(false);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < (this._encodeCRLF ? 6 : 2))
					{
						return i - offset;
					}
					i++;
					if (this._encodeCRLF)
					{
						this.WriteState.Append(new byte[] { 61, 48, 68, 61, 48, 65 });
					}
					else
					{
						this.WriteState.AppendCRLF(false);
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					if ((buffer[i] == 9 || buffer[i] == 32) && i + 1 >= count + offset)
					{
						if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
						{
							return i - offset;
						}
						this.WriteState.Append(61);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			return i - offset;
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x000DDC4C File Offset: 0x000DBE4C
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x000DDC6F File Offset: 0x000DBE6F
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x000DDC77 File Offset: 0x000DBE77
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x000DDC85 File Offset: 0x000DBE85
		private void FlushInternal()
		{
			if (this._writeState != null && this._writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
			}
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x000DDCC8 File Offset: 0x000DBEC8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			for (;;)
			{
				num += this.EncodeBytes(buffer, offset + num, count - num);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x040026DD RID: 9949
		private bool _encodeCRLF;

		// Token: 0x040026DE RID: 9950
		private const int SizeOfSoftCRLF = 3;

		// Token: 0x040026DF RID: 9951
		private const int SizeOfEncodedChar = 3;

		// Token: 0x040026E0 RID: 9952
		private const int SizeOfEncodedCRLF = 6;

		// Token: 0x040026E1 RID: 9953
		private const int SizeOfNonEncodedCRLF = 2;

		// Token: 0x040026E2 RID: 9954
		private static readonly byte[] s_hexDecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1,
			2, 3, 4, 5, 6, 7, 8, 9, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12, 13, 14,
			15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12,
			13, 14, 15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x040026E3 RID: 9955
		private static readonly byte[] s_hexEncodeMap = new byte[]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};

		// Token: 0x040026E4 RID: 9956
		private int _lineLength;

		// Token: 0x040026E5 RID: 9957
		private QuotedPrintableStream.ReadStateInfo _readState;

		// Token: 0x040026E6 RID: 9958
		private WriteStateInfoBase _writeState;

		// Token: 0x020007E9 RID: 2025
		private sealed class ReadStateInfo
		{
			// Token: 0x17000E97 RID: 3735
			// (get) Token: 0x060040CC RID: 16588 RVA: 0x000DDD5C File Offset: 0x000DBF5C
			// (set) Token: 0x060040CD RID: 16589 RVA: 0x000DDD64 File Offset: 0x000DBF64
			internal bool IsEscaped { get; set; }

			// Token: 0x17000E98 RID: 3736
			// (get) Token: 0x060040CE RID: 16590 RVA: 0x000DDD6D File Offset: 0x000DBF6D
			// (set) Token: 0x060040CF RID: 16591 RVA: 0x000DDD75 File Offset: 0x000DBF75
			internal short Byte { get; set; } = -1;
		}

		// Token: 0x020007EA RID: 2026
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x060040D1 RID: 16593 RVA: 0x000DDD8D File Offset: 0x000DBF8D
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x060040D2 RID: 16594 RVA: 0x000DDDB7 File Offset: 0x000DBFB7
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.BufferFlushed();
			}

			// Token: 0x060040D3 RID: 16595 RVA: 0x000DDDDA File Offset: 0x000DBFDA
			internal static void End(IAsyncResult result)
			{
				((QuotedPrintableStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x060040D4 RID: 16596 RVA: 0x000DDDE8 File Offset: 0x000DBFE8
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060040D5 RID: 16597 RVA: 0x000DDE34 File Offset: 0x000DC034
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x040026E9 RID: 9961
			private readonly QuotedPrintableStream _parent;

			// Token: 0x040026EA RID: 9962
			private readonly byte[] _buffer;

			// Token: 0x040026EB RID: 9963
			private readonly int _offset;

			// Token: 0x040026EC RID: 9964
			private readonly int _count;

			// Token: 0x040026ED RID: 9965
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x040026EE RID: 9966
			private int _written;
		}
	}
}
