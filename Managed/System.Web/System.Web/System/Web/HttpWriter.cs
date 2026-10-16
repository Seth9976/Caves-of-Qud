using System;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Unity;

namespace System.Web
{
	/// <summary>Provides a <see cref="T:System.IO.TextWriter" /> object that is accessed through the intrinsic <see cref="T:System.Web.HttpResponse" /> object.</summary>
	// Token: 0x020000BD RID: 189
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpWriter : TextWriter
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x000190BC File Offset: 0x000172BC
		static HttpWriter()
		{
			int num;
			int num2;
			ThreadPool.GetMinThreads(out num, out num2);
			num *= 3;
			uint num3 = (uint)(4194304L / (long)num);
			HttpWriter.byteBufferSize = Math.Min(131072U, num3);
			if (HttpWriter.byteBufferSize < 32768U)
			{
				HttpWriter.byteBufferSize = 32768U;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001911C File Offset: 0x0001731C
		internal HttpWriter(HttpResponse response)
		{
			this.chars = new char[1];
			base..ctor();
			this.response = response;
			this.encoding = response.ContentEncoding;
			this.output_stream = response.output_stream;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001914F File Offset: 0x0001734F
		private byte[] GetByteBuffer(int length)
		{
			if (HttpWriter._bytebuffer == null)
			{
				HttpWriter._bytebuffer = new byte[HttpWriter.byteBufferSize];
			}
			if ((ulong)HttpWriter.byteBufferSize >= (ulong)((long)length))
			{
				return HttpWriter._bytebuffer;
			}
			return new byte[length];
		}

		/// <summary>Gets an <see cref="T:System.Text.Encoding" /> object for the <see cref="T:System.IO.TextWriter" />.</summary>
		/// <returns>An instance of the <see cref="T:System.Text.Encoding" /> class indicating the character set of the current response.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001917D File Offset: 0x0001737D
		public override Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00019185 File Offset: 0x00017385
		internal void SetEncoding(Encoding new_encoding)
		{
			this.encoding = new_encoding;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to enable HTTP output directly from the <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class containing the data to send to the client </returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001918E File Offset: 0x0001738E
		public Stream OutputStream
		{
			get
			{
				return this.output_stream;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00019196 File Offset: 0x00017396
		internal HttpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Sends all buffered output to the HTTP output stream and closes the socket connection.</summary>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001919E File Offset: 0x0001739E
		public override void Close()
		{
			this.output_stream.Close();
		}

		/// <summary>Sends all buffered output to the HTTP output stream.</summary>
		// Token: 0x06000A85 RID: 2693 RVA: 0x000191AB File Offset: 0x000173AB
		public override void Flush()
		{
			this.output_stream.Flush();
		}

		/// <summary>Sends a single character to the HTTP output stream.</summary>
		/// <param name="ch">The character to send to the HTTP output stream. </param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x000191B8 File Offset: 0x000173B8
		public override void Write(char ch)
		{
			this.chars[0] = ch;
			this.Write(this.chars, 0, 1);
		}

		/// <summary>Sends an <see cref="T:System.Object" /> to the HTTP output stream.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to send to the HTTP output stream. </param>
		// Token: 0x06000A87 RID: 2695 RVA: 0x000191D1 File Offset: 0x000173D1
		public override void Write(object obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Write(obj.ToString());
		}

		/// <summary>Sends a string to the HTTP output stream.</summary>
		/// <param name="s">The string to send to the HTTP output stream. </param>
		// Token: 0x06000A88 RID: 2696 RVA: 0x000191E3 File Offset: 0x000173E3
		public override void Write(string s)
		{
			if (s != null)
			{
				this.WriteString(s, 0, s.Length);
			}
		}

		/// <summary>Sends a stream of characters with the specified starting position and number of characters to the HTTP output stream.</summary>
		/// <param name="buffer">The memory buffer containing the characters to send to the HTTP output stream </param>
		/// <param name="index">The buffer position of the first character to send. </param>
		/// <param name="count">The number of characters to send beginning at the position specified by <paramref name="index" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffer" />, is null.- or -<paramref name="index" /> is less than zero.- or - <paramref name="count" /> is less than zero.- or -<paramref name="buffer" /> length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06000A89 RID: 2697 RVA: 0x000191F8 File Offset: 0x000173F8
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null || index < 0 || count < 0 || buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException();
			}
			int maxByteCount = this.encoding.GetMaxByteCount(count);
			byte[] byteBuffer = this.GetByteBuffer(maxByteCount);
			int bytes = this.encoding.GetBytes(buffer, index, count, byteBuffer, 0);
			this.output_stream.Write(byteBuffer, 0, bytes);
			if (this.response.buffer)
			{
				return;
			}
			this.response.Flush();
		}

		/// <summary>Sends a carriage return + line feed (CRLF) pair of characters to the HTTP output stream.</summary>
		// Token: 0x06000A8A RID: 2698 RVA: 0x0001926B File Offset: 0x0001746B
		public override void WriteLine()
		{
			this.Write(HttpWriter.newline, 0, 2);
		}

		/// <summary>Sends a string with the specified starting position and number of characters to the HTTP output stream.</summary>
		/// <param name="s">The string to send to the HTTP output stream. </param>
		/// <param name="index">The character position of the first byte to send. </param>
		/// <param name="count">The number of characters to send, beginning at the character position specified by <paramref name="index" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is less than zero.- or - The <paramref name="count" /> is less than zero. - or - The sum of the <paramref name="index" /> and the <paramref name="count" /> are greater than the string length.</exception>
		// Token: 0x06000A8B RID: 2699 RVA: 0x0001927C File Offset: 0x0001747C
		public void WriteString(string s, int index, int count)
		{
			if (s == null)
			{
				return;
			}
			if (index < 0 || count < 0 || index + count > s.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			int maxByteCount = this.encoding.GetMaxByteCount(count);
			byte[] byteBuffer = this.GetByteBuffer(maxByteCount);
			int bytes = this.encoding.GetBytes(s, index, count, byteBuffer, 0);
			this.output_stream.Write(byteBuffer, 0, bytes);
			if (this.response.buffer)
			{
				return;
			}
			this.response.Flush();
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000192F3 File Offset: 0x000174F3
		internal void WriteUTF8Ptr(IntPtr ptr, int length)
		{
			this.output_stream.WritePtr(ptr, length);
		}

		/// <summary>Sends a stream of bytes with the specified starting position and number of bytes to the HTTP output stream.</summary>
		/// <param name="buffer">The memory buffer containing the bytes to send to the HTTP output stream. </param>
		/// <param name="index">The buffer position of the first byte to send. </param>
		/// <param name="count">The number of bytes to send, beginning at the byte position specified by <paramref name="index" />. </param>
		// Token: 0x06000A8D RID: 2701 RVA: 0x00019302 File Offset: 0x00017502
		public void WriteBytes(byte[] buffer, int index, int count)
		{
			this.output_stream.Write(buffer, index, count);
			if (this.response.buffer)
			{
				return;
			}
			this.response.Flush();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HttpWriter()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400105E RID: 4190
		private const long MAX_TOTAL_BUFFERS_SIZE = 4194304L;

		// Token: 0x0400105F RID: 4191
		private const uint SINGLE_BUFFER_SIZE = 131072U;

		// Token: 0x04001060 RID: 4192
		private const uint MIN_SINGLE_BUFFER_SIZE = 32768U;

		// Token: 0x04001061 RID: 4193
		private HttpResponseStream output_stream;

		// Token: 0x04001062 RID: 4194
		private HttpResponse response;

		// Token: 0x04001063 RID: 4195
		private Encoding encoding;

		// Token: 0x04001064 RID: 4196
		[ThreadStatic]
		private static byte[] _bytebuffer;

		// Token: 0x04001065 RID: 4197
		private static readonly uint byteBufferSize;

		// Token: 0x04001066 RID: 4198
		private char[] chars;

		// Token: 0x04001067 RID: 4199
		private static char[] newline = new char[] { '\r', '\n' };
	}
}
