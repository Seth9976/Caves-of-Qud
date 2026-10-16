using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net
{
	// Token: 0x02000574 RID: 1396
	internal sealed class Base64Stream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x0009A2B9 File Offset: 0x000984B9
		internal Base64Stream(Stream stream, Base64WriteStateInfo writeStateInfo)
			: base(stream)
		{
			this._writeState = new Base64WriteStateInfo();
			this._lineLength = writeStateInfo.MaxLineLength;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0009A2D9 File Offset: 0x000984D9
		internal Base64Stream(Base64WriteStateInfo writeStateInfo)
			: base(new MemoryStream())
		{
			this._lineLength = writeStateInfo.MaxLineLength;
			this._writeState = writeStateInfo;
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0009A2FC File Offset: 0x000984FC
		private Base64Stream.ReadStateInfo ReadState
		{
			get
			{
				Base64Stream.ReadStateInfo readStateInfo;
				if ((readStateInfo = this._readState) == null)
				{
					readStateInfo = (this._readState = new Base64Stream.ReadStateInfo());
				}
				return readStateInfo;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x0009A321 File Offset: 0x00098521
		internal Base64WriteStateInfo WriteState
		{
			get
			{
				return this._writeState;
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0009A32C File Offset: 0x0009852C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
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
			Base64Stream.ReadAsyncResult readAsyncResult = new Base64Stream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			readAsyncResult.Read();
			return readAsyncResult;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0009A384 File Offset: 0x00098584
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
			Base64Stream.WriteAsyncResult writeAsyncResult = new Base64Stream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0009A3DC File Offset: 0x000985DC
		public override void Close()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				int padding = this.WriteState.Padding;
				if (padding != 1)
				{
					if (padding == 2)
					{
						this.WriteState.Append(new byte[]
						{
							Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
							Base64Stream.s_base64EncodeMap[64],
							Base64Stream.s_base64EncodeMap[64]
						});
					}
				}
				else
				{
					this.WriteState.Append(new byte[]
					{
						Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
						Base64Stream.s_base64EncodeMap[64]
					});
				}
				this.WriteState.Padding = 0;
				this.FlushInternal();
			}
			base.Close();
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0009A4A0 File Offset: 0x000986A0
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
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 13 || *ptr3 == 10 || *ptr3 == 61 || *ptr3 == 32 || *ptr3 == 9)
				{
					ptr3++;
				}
				else
				{
					byte b = Base64Stream.s_base64DecodeMap[(int)(*ptr3)];
					if (b == 255)
					{
						throw new FormatException("An invalid character was found in the Base-64 stream.");
					}
					switch (this.ReadState.Pos)
					{
					case 0:
					{
						this.ReadState.Val = (byte)(b << 2);
						Base64Stream.ReadStateInfo readState = this.ReadState;
						byte b2 = readState.Pos;
						readState.Pos = b2 + 1;
						break;
					}
					case 1:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 4));
						this.ReadState.Val = (byte)(b << 4);
						Base64Stream.ReadStateInfo readState2 = this.ReadState;
						byte b2 = readState2.Pos;
						readState2.Pos = b2 + 1;
						break;
					}
					case 2:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 2));
						this.ReadState.Val = (byte)(b << 6);
						Base64Stream.ReadStateInfo readState3 = this.ReadState;
						byte b2 = readState3.Pos;
						readState3.Pos = b2 + 1;
						break;
					}
					case 3:
						*(ptr4++) = this.ReadState.Val + b;
						this.ReadState.Pos = 0;
						break;
					}
					ptr3++;
				}
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0009A630 File Offset: 0x00098830
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			return this.EncodeBytes(buffer, offset, count, true, true);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0009A640 File Offset: 0x00098840
		internal int EncodeBytes(byte[] buffer, int offset, int count, bool dontDeferFinalBytes, bool shouldAppendSpaceToCRLF)
		{
			this.WriteState.AppendHeader();
			int i = offset;
			int num = this.WriteState.Padding;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 240) >> 4)]);
					if (count == 1)
					{
						this.WriteState.LastBits = (byte)((buffer[i] & 15) << 2);
						this.WriteState.Padding = 1;
						i++;
						return i - offset;
					}
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 15) << 2) | ((buffer[i + 1] & 192) >> 6)]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 63)]);
					i += 2;
					count -= 2;
					this.WriteState.Padding = 0;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 63)]);
				i++;
				count--;
				this.WriteState.Padding = 0;
			}
			int num2 = i + (count - count % 3);
			while (i < num2)
			{
				if (this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength > this._lineLength)
				{
					this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
				}
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i + 1] & 15) << 2) | ((buffer[i + 2] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 2] & 63)]);
				i += 3;
			}
			i = num2;
			if (count % 3 != 0 && this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength >= this._lineLength)
			{
				this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
			}
			num = count % 3;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
					if (dontDeferFinalBytes)
					{
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 15) << 2]);
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
						this.WriteState.Padding = 0;
					}
					else
					{
						this.WriteState.LastBits = (byte)((buffer[i + 1] & 15) << 2);
						this.WriteState.Padding = 1;
					}
					i += 2;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				if (dontDeferFinalBytes)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)((byte)((buffer[i] & 3) << 4))]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Padding = 0;
				}
				else
				{
					this.WriteState.LastBits = (byte)((buffer[i] & 3) << 4);
					this.WriteState.Padding = 2;
				}
				i++;
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0009A9E1 File Offset: 0x00098BE1
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0009AA04 File Offset: 0x00098C04
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return Base64Stream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0009AA1A File Offset: 0x00098C1A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Base64Stream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0009AA30 File Offset: 0x00098C30
		public override void Flush()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				this.FlushInternal();
			}
			base.Flush();
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0009AA54 File Offset: 0x00098C54
		private void FlushInternal()
		{
			base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
			this.WriteState.Reset();
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0009AA80 File Offset: 0x00098C80
		public override int Read(byte[] buffer, int offset, int count)
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
			for (;;)
			{
				int num = base.Read(buffer, offset, count);
				if (num == 0)
				{
					break;
				}
				num = this.DecodeBytes(buffer, offset, num);
				if (num > 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0009AAE4 File Offset: 0x00098CE4
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
				num += this.EncodeBytes(buffer, offset + num, count - num, false, false);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x040018A1 RID: 6305
		private static readonly byte[] s_base64DecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, 62, byte.MaxValue, byte.MaxValue, byte.MaxValue, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
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

		// Token: 0x040018A2 RID: 6306
		private static readonly byte[] s_base64EncodeMap = new byte[]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 83, 84,
			85, 86, 87, 88, 89, 90, 97, 98, 99, 100,
			101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
			111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
			121, 122, 48, 49, 50, 51, 52, 53, 54, 55,
			56, 57, 43, 47, 61
		};

		// Token: 0x040018A3 RID: 6307
		private readonly int _lineLength;

		// Token: 0x040018A4 RID: 6308
		private readonly Base64WriteStateInfo _writeState;

		// Token: 0x040018A5 RID: 6309
		private Base64Stream.ReadStateInfo _readState;

		// Token: 0x040018A6 RID: 6310
		private const int SizeOfBase64EncodedChar = 4;

		// Token: 0x040018A7 RID: 6311
		private const byte InvalidBase64Value = 255;

		// Token: 0x02000575 RID: 1397
		private sealed class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D1B RID: 11547 RVA: 0x0009AB7A File Offset: 0x00098D7A
			internal ReadAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06002D1C RID: 11548 RVA: 0x0009ABA4 File Offset: 0x00098DA4
			private bool CompleteRead(IAsyncResult result)
			{
				this._read = this._parent.BaseStream.EndRead(result);
				if (this._read == 0)
				{
					base.InvokeCallback();
					return true;
				}
				this._read = this._parent.DecodeBytes(this._buffer, this._offset, this._read);
				if (this._read > 0)
				{
					base.InvokeCallback();
					return true;
				}
				return false;
			}

			// Token: 0x06002D1D RID: 11549 RVA: 0x0009AC10 File Offset: 0x00098E10
			internal void Read()
			{
				IAsyncResult asyncResult;
				do
				{
					asyncResult = this._parent.BaseStream.BeginRead(this._buffer, this._offset, this._count, Base64Stream.ReadAsyncResult.s_onRead, this);
				}
				while (asyncResult.CompletedSynchronously && !this.CompleteRead(asyncResult));
			}

			// Token: 0x06002D1E RID: 11550 RVA: 0x0009AC58 File Offset: 0x00098E58
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result.AsyncState;
					try
					{
						if (!readAsyncResult.CompleteRead(result))
						{
							readAsyncResult.Read();
						}
					}
					catch (Exception ex)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06002D1F RID: 11551 RVA: 0x0009ACB0 File Offset: 0x00098EB0
			internal static int End(IAsyncResult result)
			{
				Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult._read;
			}

			// Token: 0x040018A8 RID: 6312
			private readonly Base64Stream _parent;

			// Token: 0x040018A9 RID: 6313
			private readonly byte[] _buffer;

			// Token: 0x040018AA RID: 6314
			private readonly int _offset;

			// Token: 0x040018AB RID: 6315
			private readonly int _count;

			// Token: 0x040018AC RID: 6316
			private int _read;

			// Token: 0x040018AD RID: 6317
			private static readonly AsyncCallback s_onRead = new AsyncCallback(Base64Stream.ReadAsyncResult.OnRead);
		}

		// Token: 0x02000576 RID: 1398
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D21 RID: 11553 RVA: 0x0009ACD7 File Offset: 0x00098ED7
			internal WriteAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06002D22 RID: 11554 RVA: 0x0009AD04 File Offset: 0x00098F04
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written, false, false);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, Base64Stream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x06002D23 RID: 11555 RVA: 0x0009ADAB File Offset: 0x00098FAB
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.Reset();
			}

			// Token: 0x06002D24 RID: 11556 RVA: 0x0009ADD0 File Offset: 0x00098FD0
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						if (writeAsyncResult.IsCompleted)
						{
							throw;
						}
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06002D25 RID: 11557 RVA: 0x0009AE24 File Offset: 0x00099024
			internal static void End(IAsyncResult result)
			{
				((Base64Stream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x040018AE RID: 6318
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(Base64Stream.WriteAsyncResult.OnWrite);

			// Token: 0x040018AF RID: 6319
			private readonly Base64Stream _parent;

			// Token: 0x040018B0 RID: 6320
			private readonly byte[] _buffer;

			// Token: 0x040018B1 RID: 6321
			private readonly int _offset;

			// Token: 0x040018B2 RID: 6322
			private readonly int _count;

			// Token: 0x040018B3 RID: 6323
			private int _written;
		}

		// Token: 0x02000577 RID: 1399
		private sealed class ReadStateInfo
		{
			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06002D27 RID: 11559 RVA: 0x0009AE45 File Offset: 0x00099045
			// (set) Token: 0x06002D28 RID: 11560 RVA: 0x0009AE4D File Offset: 0x0009904D
			internal byte Val { get; set; }

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06002D29 RID: 11561 RVA: 0x0009AE56 File Offset: 0x00099056
			// (set) Token: 0x06002D2A RID: 11562 RVA: 0x0009AE5E File Offset: 0x0009905E
			internal byte Pos { get; set; }
		}
	}
}
