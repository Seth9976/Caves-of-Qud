using System;
using System.IO;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides access to individual files that have been uploaded by a client.</summary>
	// Token: 0x0200009E RID: 158
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpPostedFile
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x000115EA File Offset: 0x0000F7EA
		internal HttpPostedFile(string name, string content_type, Stream base_stream, long offset, long length)
		{
			this.name = name;
			this.content_type = content_type;
			this.stream = new HttpPostedFile.ReadSubStream(base_stream, offset, length);
		}

		/// <summary>Gets the MIME content type of a file sent by a client.</summary>
		/// <returns>The MIME content type of the uploaded file.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00011610 File Offset: 0x0000F810
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
		}

		/// <summary>Gets the size of an uploaded file, in bytes.</summary>
		/// <returns>The file length, in bytes.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00011618 File Offset: 0x0000F818
		public int ContentLength
		{
			get
			{
				return (int)this.stream.Length;
			}
		}

		/// <summary>Gets the fully qualified name of the file on the client.</summary>
		/// <returns>The name of the client's file, including the directory path.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00011626 File Offset: 0x0000F826
		public string FileName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> pointing to a file.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001162E File Offset: 0x0000F82E
		public Stream InputStream
		{
			get
			{
				return this.stream;
			}
		}

		/// <summary>Saves the contents of an uploaded file.</summary>
		/// <param name="filename">The name of the saved file. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath" /> property of the <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> object is set to true, but <paramref name="filename" /> is not an absolute path.</exception>
		// Token: 0x06000789 RID: 1929 RVA: 0x00011638 File Offset: 0x0000F838
		public void SaveAs(string filename)
		{
			byte[] array = new byte[16384];
			long position = this.stream.Position;
			try
			{
				File.Delete(filename);
				using (FileStream fileStream = File.Create(filename))
				{
					this.stream.Position = 0L;
					int num;
					while ((num = this.stream.Read(array, 0, 16384)) != 0)
					{
						fileStream.Write(array, 0, num);
					}
				}
			}
			finally
			{
				this.stream.Position = position;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HttpPostedFile()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F6F RID: 3951
		private string name;

		// Token: 0x04000F70 RID: 3952
		private string content_type;

		// Token: 0x04000F71 RID: 3953
		private Stream stream;

		// Token: 0x0200009F RID: 159
		private class ReadSubStream : Stream
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x000116CC File Offset: 0x0000F8CC
			public ReadSubStream(Stream s, long offset, long length)
			{
				this.s = s;
				this.offset = offset;
				this.end = offset + length;
				this.position = offset;
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00003946 File Offset: 0x00001B46
			public override void Flush()
			{
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x000116F4 File Offset: 0x0000F8F4
			public override int Read(byte[] buffer, int dest_offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (dest_offset < 0)
				{
					throw new ArgumentOutOfRangeException("dest_offset", "< 0");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "< 0");
				}
				int num = buffer.Length;
				if (dest_offset > num)
				{
					throw new ArgumentException("destination offset is beyond array size");
				}
				if (dest_offset > num - count)
				{
					throw new ArgumentException("Reading would overrun buffer");
				}
				if ((long)count > this.end - this.position)
				{
					count = (int)(this.end - this.position);
				}
				if (count <= 0)
				{
					return 0;
				}
				this.s.Position = this.position;
				int num2 = this.s.Read(buffer, dest_offset, count);
				if (num2 > 0)
				{
					this.position += (long)num2;
				}
				else
				{
					this.position = this.end;
				}
				return num2;
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x000117C4 File Offset: 0x0000F9C4
			public override int ReadByte()
			{
				if (this.position >= this.end)
				{
					return -1;
				}
				this.s.Position = this.position;
				int num = this.s.ReadByte();
				if (num < 0)
				{
					this.position = this.end;
					return num;
				}
				this.position += 1L;
				return num;
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x00011820 File Offset: 0x0000FA20
			public override long Seek(long d, SeekOrigin origin)
			{
				long num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = this.offset + d;
					break;
				case SeekOrigin.Current:
					num = this.position + d;
					break;
				case SeekOrigin.End:
					num = this.end + d;
					break;
				default:
					throw new ArgumentException();
				}
				long num2 = num - this.offset;
				if (num2 < 0L || num2 > this.Length)
				{
					throw new ArgumentException();
				}
				this.position = this.s.Seek(num, SeekOrigin.Begin);
				return this.position;
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x00003A0D File Offset: 0x00001C0D
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000791 RID: 1937 RVA: 0x00003A0D File Offset: 0x00001C0D
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000792 RID: 1938 RVA: 0x00008B4A File Offset: 0x00006D4A
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000793 RID: 1939 RVA: 0x00008B4A File Offset: 0x00006D4A
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000794 RID: 1940 RVA: 0x00008A4D File Offset: 0x00006C4D
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001189E File Offset: 0x0000FA9E
			public override long Length
			{
				get
				{
					return this.end - this.offset;
				}
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000796 RID: 1942 RVA: 0x000118AD File Offset: 0x0000FAAD
			// (set) Token: 0x06000797 RID: 1943 RVA: 0x000118BC File Offset: 0x0000FABC
			public override long Position
			{
				get
				{
					return this.position - this.offset;
				}
				set
				{
					if (value > this.Length)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.position = this.Seek(value, SeekOrigin.Begin);
				}
			}

			// Token: 0x04000F72 RID: 3954
			private Stream s;

			// Token: 0x04000F73 RID: 3955
			private long offset;

			// Token: 0x04000F74 RID: 3956
			private long end;

			// Token: 0x04000F75 RID: 3957
			private long position;
		}
	}
}
