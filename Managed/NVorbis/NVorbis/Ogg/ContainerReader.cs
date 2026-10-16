using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NVorbis.Ogg
{
	// Token: 0x0200000A RID: 10
	public class ContainerReader : IContainerReader, IDisposable
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public int[] StreamSerials
		{
			get
			{
				return Enumerable.ToArray<int>(this._packetReaders.Keys);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00003ABC File Offset: 0x00001CBC
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public event EventHandler<NewStreamEventArgs> NewStream;

		// Token: 0x0600004C RID: 76 RVA: 0x00003B29 File Offset: 0x00001D29
		public ContainerReader(string path)
			: this(File.Open(path, 3, 1, 1), true)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B3C File Offset: 0x00001D3C
		public ContainerReader(Stream stream, bool closeOnDispose)
		{
			this._packetReaders = new Dictionary<int, PacketReader>();
			this._disposedStreamSerials = new List<int>();
			BufferedReadStream bufferedReadStream;
			if ((bufferedReadStream = stream as BufferedReadStream) == null)
			{
				bufferedReadStream = new BufferedReadStream(stream)
				{
					CloseBaseStream = closeOnDispose
				};
			}
			this._stream = bufferedReadStream;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public bool Init()
		{
			this._stream.TakeLock();
			bool flag;
			try
			{
				flag = this.GatherNextPage() != -1;
			}
			finally
			{
				this._stream.ReleaseLock();
			}
			return flag;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public void Dispose()
		{
			foreach (int num in this.StreamSerials)
			{
				this._packetReaders[num].Dispose();
			}
			this._nextPageOffset = 0L;
			this._containerBits = 0L;
			this._wasteBits = 0L;
			this._stream.Dispose();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003C40 File Offset: 0x00001E40
		public IPacketProvider GetStream(int streamSerial)
		{
			PacketReader packetReader;
			if (!this._packetReaders.TryGetValue(streamSerial, ref packetReader))
			{
				throw new ArgumentOutOfRangeException("streamSerial");
			}
			return packetReader;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003C6C File Offset: 0x00001E6C
		public bool FindNextStream()
		{
			if (!this.CanSeek)
			{
				throw new InvalidOperationException();
			}
			int count = this._packetReaders.Count;
			while (this._packetReaders.Count == count)
			{
				this._stream.TakeLock();
				try
				{
					if (this.GatherNextPage() == -1)
					{
						break;
					}
				}
				finally
				{
					this._stream.ReleaseLock();
				}
			}
			return count > this._packetReaders.Count;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public int PagesRead
		{
			get
			{
				return this._pageCount;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public int GetTotalPageCount()
		{
			if (!this.CanSeek)
			{
				throw new InvalidOperationException();
			}
			for (;;)
			{
				this._stream.TakeLock();
				try
				{
					if (this.GatherNextPage() != -1)
					{
						continue;
					}
				}
				finally
				{
					this._stream.ReleaseLock();
				}
				break;
			}
			return this._pageCount;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003D48 File Offset: 0x00001F48
		public bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003D55 File Offset: 0x00001F55
		public long WasteBits
		{
			get
			{
				return this._wasteBits;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003D60 File Offset: 0x00001F60
		private ContainerReader.PageHeader ReadPageHeader(long position)
		{
			this._stream.Seek(position, 0);
			if (this._stream.Read(this._readBuffer, 0, 27) != 27)
			{
				return null;
			}
			if (this._readBuffer[0] != 79 || this._readBuffer[1] != 103 || this._readBuffer[2] != 103 || this._readBuffer[3] != 83)
			{
				return null;
			}
			if (this._readBuffer[4] != 0)
			{
				return null;
			}
			ContainerReader.PageHeader pageHeader = new ContainerReader.PageHeader();
			pageHeader.Flags = (PageFlags)this._readBuffer[5];
			pageHeader.GranulePosition = BitConverter.ToInt64(this._readBuffer, 6);
			pageHeader.StreamSerial = BitConverter.ToInt32(this._readBuffer, 14);
			pageHeader.SequenceNumber = BitConverter.ToInt32(this._readBuffer, 18);
			uint num = BitConverter.ToUInt32(this._readBuffer, 22);
			this._crc.Reset();
			for (int i = 0; i < 22; i++)
			{
				this._crc.Update((int)this._readBuffer[i]);
			}
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update((int)this._readBuffer[26]);
			int num2 = (int)this._readBuffer[26];
			if (this._stream.Read(this._readBuffer, 0, num2) != num2)
			{
				return null;
			}
			List<int> list = new List<int>(num2);
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < num2; j++)
			{
				byte b = this._readBuffer[j];
				this._crc.Update((int)b);
				if (num4 == list.Count)
				{
					list.Add(0);
				}
				List<int> list2;
				int num5;
				(list2 = list)[num5 = num4] = list2[num5] + (int)b;
				if (b < 255)
				{
					num4++;
					pageHeader.LastPacketContinues = false;
				}
				else
				{
					pageHeader.LastPacketContinues = true;
				}
				num3 += (int)b;
			}
			pageHeader.PacketSizes = list.ToArray();
			pageHeader.DataOffset = position + 27L + (long)num2;
			if (this._stream.Read(this._readBuffer, 0, num3) != num3)
			{
				return null;
			}
			for (int k = 0; k < num3; k++)
			{
				this._crc.Update((int)this._readBuffer[k]);
			}
			if (this._crc.Test(num))
			{
				this._containerBits += (long)(8 * (27 + num2));
				this._pageCount++;
				return pageHeader;
			}
			return null;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003FD8 File Offset: 0x000021D8
		private ContainerReader.PageHeader FindNextPageHeader()
		{
			long num = this._nextPageOffset;
			bool flag = false;
			ContainerReader.PageHeader pageHeader;
			while ((pageHeader = this.ReadPageHeader(num)) == null)
			{
				flag = true;
				this._wasteBits += 8L;
				num = (this._stream.Position = num + 1L);
				int num2 = 0;
				do
				{
					int num3 = this._stream.ReadByte();
					if (num3 == 79)
					{
						if (this._stream.ReadByte() == 103 && this._stream.ReadByte() == 103 && this._stream.ReadByte() == 83)
						{
							goto Block_4;
						}
						this._stream.Seek(-3L, 1);
					}
					else if (num3 == -1)
					{
						goto Block_5;
					}
					this._wasteBits += 8L;
				}
				while (++num2 < 65536);
				IL_00AD:
				if (num2 == 65536)
				{
					return null;
				}
				continue;
				Block_4:
				num += (long)num2;
				goto IL_00AD;
				Block_5:
				return null;
			}
			pageHeader.IsResync = flag;
			this._nextPageOffset = pageHeader.DataOffset;
			for (int i = 0; i < pageHeader.PacketSizes.Length; i++)
			{
				this._nextPageOffset += (long)pageHeader.PacketSizes[i];
			}
			return pageHeader;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000040EC File Offset: 0x000022EC
		private bool AddPage(ContainerReader.PageHeader hdr)
		{
			PacketReader packetReader;
			if (!this._packetReaders.TryGetValue(hdr.StreamSerial, ref packetReader))
			{
				packetReader = new PacketReader(this, hdr.StreamSerial);
			}
			packetReader.ContainerBits += this._containerBits;
			this._containerBits = 0L;
			bool flag = hdr.PacketSizes.Length == 1 && hdr.LastPacketContinues;
			bool flag2 = (hdr.Flags & PageFlags.ContinuesPacket) == PageFlags.ContinuesPacket;
			bool flag3 = false;
			bool flag4 = hdr.IsResync;
			long num = hdr.DataOffset;
			int num2 = hdr.PacketSizes.Length;
			foreach (int num3 in hdr.PacketSizes)
			{
				Packet packet = new Packet(this, num, num3)
				{
					PageGranulePosition = hdr.GranulePosition,
					IsEndOfStream = flag3,
					PageSequenceNumber = hdr.SequenceNumber,
					IsContinued = flag,
					IsContinuation = flag2,
					IsResync = flag4
				};
				packetReader.AddPacket(packet);
				num += (long)num3;
				flag2 = false;
				flag4 = false;
				if (--num2 == 1)
				{
					flag = hdr.LastPacketContinues;
					flag3 = (hdr.Flags & PageFlags.EndOfStream) == PageFlags.EndOfStream;
				}
			}
			if (!this._packetReaders.ContainsKey(hdr.StreamSerial))
			{
				this._packetReaders.Add(hdr.StreamSerial, packetReader);
				return true;
			}
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004240 File Offset: 0x00002440
		private int GatherNextPage()
		{
			ContainerReader.PageHeader pageHeader;
			for (;;)
			{
				pageHeader = this.FindNextPageHeader();
				if (pageHeader == null)
				{
					break;
				}
				if (!this._disposedStreamSerials.Contains(pageHeader.StreamSerial))
				{
					if (!this.AddPage(pageHeader))
					{
						goto IL_0071;
					}
					EventHandler<NewStreamEventArgs> newStream = this.NewStream;
					if (newStream == null)
					{
						goto IL_0071;
					}
					NewStreamEventArgs e = new NewStreamEventArgs(this._packetReaders[pageHeader.StreamSerial]);
					newStream.Invoke(this, e);
					if (!e.IgnoreStream)
					{
						goto IL_0071;
					}
					this._packetReaders[pageHeader.StreamSerial].Dispose();
				}
			}
			return -1;
			IL_0071:
			return pageHeader.StreamSerial;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000042C4 File Offset: 0x000024C4
		internal void DisposePacketReader(PacketReader packetReader)
		{
			this._disposedStreamSerials.Add(packetReader.StreamSerial);
			this._packetReaders.Remove(packetReader.StreamSerial);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000042EC File Offset: 0x000024EC
		internal int PacketReadByte(long offset)
		{
			this._stream.TakeLock();
			int num;
			try
			{
				this._stream.Position = offset;
				num = this._stream.ReadByte();
			}
			finally
			{
				this._stream.ReleaseLock();
			}
			return num;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000433C File Offset: 0x0000253C
		internal void PacketDiscardThrough(long offset)
		{
			this._stream.TakeLock();
			try
			{
				this._stream.DiscardThrough(offset);
			}
			finally
			{
				this._stream.ReleaseLock();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004380 File Offset: 0x00002580
		internal void GatherNextPage(int streamSerial)
		{
			if (!this._packetReaders.ContainsKey(streamSerial))
			{
				throw new ArgumentOutOfRangeException("streamSerial");
			}
			int num;
			do
			{
				this._stream.TakeLock();
				try
				{
					if (this._packetReaders[streamSerial].HasEndOfStream)
					{
						break;
					}
					num = this.GatherNextPage();
					if (num == -1)
					{
						foreach (KeyValuePair<int, PacketReader> keyValuePair in this._packetReaders)
						{
							if (!keyValuePair.Value.HasEndOfStream)
							{
								keyValuePair.Value.SetEndOfStream();
							}
						}
						break;
					}
				}
				finally
				{
					this._stream.ReleaseLock();
				}
			}
			while (num != streamSerial);
		}

		// Token: 0x0400001E RID: 30
		private Crc _crc = new Crc();

		// Token: 0x0400001F RID: 31
		private BufferedReadStream _stream;

		// Token: 0x04000020 RID: 32
		private Dictionary<int, PacketReader> _packetReaders;

		// Token: 0x04000021 RID: 33
		private List<int> _disposedStreamSerials;

		// Token: 0x04000022 RID: 34
		private long _nextPageOffset;

		// Token: 0x04000023 RID: 35
		private int _pageCount;

		// Token: 0x04000024 RID: 36
		private byte[] _readBuffer = new byte[65025];

		// Token: 0x04000025 RID: 37
		private long _containerBits;

		// Token: 0x04000026 RID: 38
		private long _wasteBits;

		// Token: 0x0200000B RID: 11
		private class PageHeader
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00004450 File Offset: 0x00002650
			// (set) Token: 0x0600005F RID: 95 RVA: 0x00004458 File Offset: 0x00002658
			public int StreamSerial { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00004461 File Offset: 0x00002661
			// (set) Token: 0x06000061 RID: 97 RVA: 0x00004469 File Offset: 0x00002669
			public PageFlags Flags { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00004472 File Offset: 0x00002672
			// (set) Token: 0x06000063 RID: 99 RVA: 0x0000447A File Offset: 0x0000267A
			public long GranulePosition { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000064 RID: 100 RVA: 0x00004483 File Offset: 0x00002683
			// (set) Token: 0x06000065 RID: 101 RVA: 0x0000448B File Offset: 0x0000268B
			public int SequenceNumber { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000066 RID: 102 RVA: 0x00004494 File Offset: 0x00002694
			// (set) Token: 0x06000067 RID: 103 RVA: 0x0000449C File Offset: 0x0000269C
			public long DataOffset { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000068 RID: 104 RVA: 0x000044A5 File Offset: 0x000026A5
			// (set) Token: 0x06000069 RID: 105 RVA: 0x000044AD File Offset: 0x000026AD
			public int[] PacketSizes { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600006A RID: 106 RVA: 0x000044B6 File Offset: 0x000026B6
			// (set) Token: 0x0600006B RID: 107 RVA: 0x000044BE File Offset: 0x000026BE
			public bool LastPacketContinues { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600006C RID: 108 RVA: 0x000044C7 File Offset: 0x000026C7
			// (set) Token: 0x0600006D RID: 109 RVA: 0x000044CF File Offset: 0x000026CF
			public bool IsResync { get; set; }
		}
	}
}
