using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020007F4 RID: 2036
	internal sealed class BufferBuilder
	{
		// Token: 0x06004107 RID: 16647 RVA: 0x000DEE1B File Offset: 0x000DD01B
		internal BufferBuilder()
			: this(256)
		{
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000DEE28 File Offset: 0x000DD028
		internal BufferBuilder(int initialSize)
		{
			this._buffer = new byte[initialSize];
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000DEE3C File Offset: 0x000DD03C
		private void EnsureBuffer(int count)
		{
			if (count > this._buffer.Length - this._offset)
			{
				byte[] array = new byte[(this._buffer.Length * 2 > this._buffer.Length + count) ? (this._buffer.Length * 2) : (this._buffer.Length + count)];
				Buffer.BlockCopy(this._buffer, 0, array, 0, this._offset);
				this._buffer = array;
			}
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000DEEA8 File Offset: 0x000DD0A8
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			this._offset = offset + 1;
			buffer[offset] = value;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x000DEED5 File Offset: 0x000DD0D5
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000DEEE2 File Offset: 0x000DD0E2
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this._buffer, this._offset, count);
			this._offset += count;
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x000DEF0D File Offset: 0x000DD10D
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000DEF17 File Offset: 0x000DD117
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000DEF34 File Offset: 0x000DD134
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				int byteCount = Encoding.UTF8.GetByteCount(value, offset, count);
				this.EnsureBuffer(byteCount);
				Encoding.UTF8.GetBytes(value, offset, count, this._buffer, this._offset);
				this._offset += byteCount;
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000DEF8C File Offset: 0x000DD18C
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", c));
				}
				this._buffer[this._offset + i] = (byte)c;
			}
			this._offset += count;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000DEFF3 File Offset: 0x000DD1F3
		internal int Length
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000DEFFB File Offset: 0x000DD1FB
		internal byte[] GetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000DF003 File Offset: 0x000DD203
		internal void Reset()
		{
			this._offset = 0;
		}

		// Token: 0x0400270C RID: 9996
		private byte[] _buffer;

		// Token: 0x0400270D RID: 9997
		private int _offset;
	}
}
