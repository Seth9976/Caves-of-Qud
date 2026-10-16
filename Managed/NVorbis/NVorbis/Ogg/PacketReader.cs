using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NVorbis.Ogg
{
	// Token: 0x02000010 RID: 16
	[DebuggerTypeProxy(typeof(PacketReader.DebugView))]
	internal class PacketReader : IPacketProvider, IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x00004BB8 File Offset: 0x00002DB8
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public event EventHandler<ParameterChangeEventArgs> ParameterChange;

		// Token: 0x060000A9 RID: 169 RVA: 0x00004C25 File Offset: 0x00002E25
		internal PacketReader(ContainerReader container, int streamSerial)
		{
			this._container = container;
			this._streamSerial = streamSerial;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004C48 File Offset: 0x00002E48
		public void Dispose()
		{
			this._eosFound = true;
			this._container.DisposePacketReader(this);
			this._container = null;
			this._current = null;
			if (this._first != null)
			{
				Packet packet = this._first;
				this._first = null;
				while (packet.Next != null)
				{
					Packet next = packet.Next;
					packet.Next = null;
					packet = next;
					packet.Prev = null;
				}
			}
			this._last = null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004CB8 File Offset: 0x00002EB8
		internal void AddPacket(Packet packet)
		{
			lock (this._packetLock)
			{
				if (!this._eosFound)
				{
					if (packet.IsResync)
					{
						packet.IsContinuation = false;
						if (this._last != null)
						{
							this._last.IsContinued = false;
						}
					}
					if (packet.IsContinuation)
					{
						if (this._last == null)
						{
							throw new InvalidDataException();
						}
						if (!this._last.IsContinued)
						{
							throw new InvalidDataException();
						}
						this._last.MergeWith(packet);
						this._last.IsContinued = packet.IsContinued;
					}
					else
					{
						if (packet == null)
						{
							throw new ArgumentException("Wrong packet datatype", "packet");
						}
						if (this._first == null)
						{
							this._first = packet;
							this._last = packet;
						}
						else
						{
							(packet.Prev = this._last).Next = packet;
							this._last = packet;
						}
					}
					if (packet.IsEndOfStream)
					{
						this.SetEndOfStream();
					}
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004DBC File Offset: 0x00002FBC
		internal bool HasEndOfStream
		{
			get
			{
				return this._eosFound;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004DC4 File Offset: 0x00002FC4
		internal void SetEndOfStream()
		{
			lock (this._packetLock)
			{
				this._eosFound = true;
				if (this._last.IsContinued)
				{
					this._last = this._last.Prev;
					this._last.Next.Prev = null;
					this._last.Next = null;
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004E3C File Offset: 0x0000303C
		public int StreamSerial
		{
			get
			{
				return this._streamSerial;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004E44 File Offset: 0x00003044
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004E4C File Offset: 0x0000304C
		public long ContainerBits { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004E55 File Offset: 0x00003055
		public bool CanSeek
		{
			get
			{
				return this._container.CanSeek;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004E64 File Offset: 0x00003064
		public DataPacket GetNextPacket()
		{
			return this._current = this.PeekNextPacketInternal();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004E80 File Offset: 0x00003080
		public DataPacket PeekNextPacket()
		{
			return this.PeekNextPacketInternal();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E88 File Offset: 0x00003088
		private Packet PeekNextPacketInternal()
		{
			Packet packet;
			if (this._current == null)
			{
				packet = this._first;
			}
			else
			{
				for (;;)
				{
					lock (this._packetLock)
					{
						packet = this._current.Next;
						if ((packet != null && !packet.IsContinued) || this._eosFound)
						{
							break;
						}
					}
					this._container.GatherNextPage(this._streamSerial);
				}
			}
			if (packet != null)
			{
				if (packet.IsContinued)
				{
					throw new InvalidDataException("Packet is incomplete!");
				}
				packet.Reset();
			}
			return packet;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004F20 File Offset: 0x00003120
		internal void ReadAllPages()
		{
			if (!this.CanSeek)
			{
				throw new InvalidOperationException();
			}
			while (!this._eosFound)
			{
				this._container.GatherNextPage(this._streamSerial);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004F49 File Offset: 0x00003149
		internal DataPacket GetLastPacket()
		{
			this.ReadAllPages();
			return this._last;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004F58 File Offset: 0x00003158
		public int GetTotalPageCount()
		{
			this.ReadAllPages();
			int num = 0;
			int num2 = 0;
			for (Packet packet = this._first; packet != null; packet = packet.Next)
			{
				if (packet.PageSequenceNumber != num2)
				{
					num++;
					num2 = packet.PageSequenceNumber;
				}
			}
			return num;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004F98 File Offset: 0x00003198
		public DataPacket GetPacket(int packetIndex)
		{
			if (!this.CanSeek)
			{
				throw new InvalidOperationException();
			}
			if (packetIndex < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._first == null)
			{
				throw new InvalidOperationException("Packet reader has no packets!");
			}
			Packet packet = this._first;
			while (--packetIndex >= 0)
			{
				while (packet.Next == null)
				{
					if (this._eosFound)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this._container.GatherNextPage(this._streamSerial);
				}
				packet = packet.Next;
			}
			packet.Reset();
			return packet;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005024 File Offset: 0x00003224
		private Packet GetLastPacketInPage(Packet packet)
		{
			if (packet != null)
			{
				int pageSequenceNumber = packet.PageSequenceNumber;
				while (packet.Next != null && packet.Next.PageSequenceNumber == pageSequenceNumber)
				{
					packet = packet.Next;
				}
				if (packet != null && packet.IsContinued)
				{
					packet = packet.Prev;
				}
			}
			return packet;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005070 File Offset: 0x00003270
		private Packet FindPacketInPage(Packet pagePacket, long targetGranulePos, Func<DataPacket, DataPacket, int> packetGranuleCountCallback)
		{
			Packet lastPacketInPage = this.GetLastPacketInPage(pagePacket);
			if (lastPacketInPage == null)
			{
				return null;
			}
			Packet packet = lastPacketInPage;
			for (;;)
			{
				if (packet.GranuleCount == null)
				{
					if (packet == lastPacketInPage)
					{
						packet.GranulePosition = packet.PageGranulePosition;
					}
					else
					{
						packet.GranulePosition = packet.Next.GranulePosition - (long)packet.Next.GranuleCount.Value;
					}
					if (packet == this._last && this._eosFound && packet.Prev.PageSequenceNumber < packet.PageSequenceNumber)
					{
						packet.GranuleCount = new int?((int)(packet.GranulePosition - packet.Prev.PageGranulePosition));
					}
					else if (packet.Prev != null)
					{
						packet.Prev.Reset();
						packet.Reset();
						packet.GranuleCount = new int?(packetGranuleCountCallback.Invoke(packet, packet.Prev));
					}
					else
					{
						long granulePosition = packet.GranulePosition;
						long granulePosition2 = packet.Next.GranulePosition;
						int? granuleCount = packet.Next.GranuleCount;
						if (granulePosition > ((granuleCount != null) ? new long?(granulePosition2 - (long)granuleCount.GetValueOrDefault()) : default(long?)))
						{
							break;
						}
						packet.GranuleCount = new int?((int)packet.GranulePosition);
					}
				}
				if (targetGranulePos <= packet.GranulePosition)
				{
					long granulePosition3 = packet.GranulePosition;
					int? granuleCount2 = packet.GranuleCount;
					if (targetGranulePos > ((granuleCount2 != null) ? new long?(granulePosition3 - (long)granuleCount2.GetValueOrDefault()) : default(long?)))
					{
						goto Block_14;
					}
				}
				packet = packet.Prev;
				if (packet == null || packet.PageSequenceNumber != lastPacketInPage.PageSequenceNumber)
				{
					goto IL_020B;
				}
			}
			throw new InvalidOperationException("First data packet size mismatch");
			Block_14:
			if (packet.Prev != null && packet.Prev.GranuleCount == null)
			{
				packet.Prev.GranulePosition = packet.GranulePosition - (long)packet.GranuleCount.Value;
			}
			return packet;
			IL_020B:
			if (packet != null && packet.PageGranulePosition < targetGranulePos)
			{
				packet.GranulePosition = packet.PageGranulePosition;
				return packet.Next;
			}
			return null;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000052A8 File Offset: 0x000034A8
		public DataPacket FindPacket(long granulePos, Func<DataPacket, DataPacket, int> packetGranuleCountCallback)
		{
			if (granulePos < 0L)
			{
				throw new ArgumentOutOfRangeException("granulePos");
			}
			Packet packet = this._current ?? this._first;
			Packet packet2;
			if (granulePos > packet.PageGranulePosition)
			{
				while (granulePos > packet.PageGranulePosition)
				{
					if ((packet.Next == null || packet.IsContinued) && !this._eosFound)
					{
						this._container.GatherNextPage(this._streamSerial);
						if (this._eosFound)
						{
							packet = null;
							break;
						}
					}
					packet = packet.Next;
				}
				packet2 = this.FindPacketInPage(packet, granulePos, packetGranuleCountCallback);
			}
			else
			{
				while (packet.Prev != null && (granulePos < packet.Prev.PageGranulePosition || packet.Prev.PageGranulePosition == -1L))
				{
					packet = packet.Prev;
				}
				packet2 = this.FindPacketInPage(packet, granulePos, packetGranuleCountCallback);
			}
			return packet2;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000536C File Offset: 0x0000356C
		public void SeekToPacket(DataPacket packet, int preRoll)
		{
			if (preRoll < 0)
			{
				throw new ArgumentOutOfRangeException("preRoll");
			}
			if (packet == null)
			{
				throw new ArgumentNullException("granulePos");
			}
			Packet packet2 = packet as Packet;
			if (packet2 == null)
			{
				throw new ArgumentException("Incorrect packet type!", "packet");
			}
			while (--preRoll >= 0)
			{
				packet2 = packet2.Prev;
				if (packet2 == null)
				{
					throw new ArgumentOutOfRangeException("preRoll");
				}
			}
			this._current = packet2.Prev;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000053DA File Offset: 0x000035DA
		public long GetGranuleCount()
		{
			return this.GetLastPacket().PageGranulePosition;
		}

		// Token: 0x0400004E RID: 78
		private ContainerReader _container;

		// Token: 0x0400004F RID: 79
		private int _streamSerial;

		// Token: 0x04000050 RID: 80
		private bool _eosFound;

		// Token: 0x04000051 RID: 81
		private Packet _first;

		// Token: 0x04000052 RID: 82
		private Packet _current;

		// Token: 0x04000053 RID: 83
		private Packet _last;

		// Token: 0x04000054 RID: 84
		private object _packetLock = new object();

		// Token: 0x02000011 RID: 17
		private class DebugView
		{
			// Token: 0x060000BE RID: 190 RVA: 0x000053E7 File Offset: 0x000035E7
			public DebugView(PacketReader reader)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this._reader = reader;
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00005410 File Offset: 0x00003610
			public ContainerReader Container
			{
				get
				{
					return this._reader._container;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000541D File Offset: 0x0000361D
			public int StreamSerial
			{
				get
				{
					return this._reader._streamSerial;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000542A File Offset: 0x0000362A
			public bool EndOfStreamFound
			{
				get
				{
					return this._reader._eosFound;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005437 File Offset: 0x00003637
			public int CurrentPacketIndex
			{
				get
				{
					if (this._reader._current == null)
					{
						return -1;
					}
					return Array.IndexOf<Packet>(this.Packets, this._reader._current);
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005460 File Offset: 0x00003660
			public Packet[] Packets
			{
				get
				{
					if (this._reader._last == this._last && this._reader._first == this._first)
					{
						return this._packetList;
					}
					this._last = this._reader._last;
					this._first = this._reader._first;
					List<Packet> list = new List<Packet>();
					for (Packet packet = this._first; packet != null; packet = packet.Next)
					{
						list.Add(packet);
					}
					this._packetList = list.ToArray();
					return this._packetList;
				}
			}

			// Token: 0x04000056 RID: 86
			private PacketReader _reader;

			// Token: 0x04000057 RID: 87
			private Packet _last;

			// Token: 0x04000058 RID: 88
			private Packet _first;

			// Token: 0x04000059 RID: 89
			private Packet[] _packetList = new Packet[0];
		}
	}
}
