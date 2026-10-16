using System;
using System.Collections.Generic;

namespace NVorbis
{
	// Token: 0x0200000C RID: 12
	public abstract class DataPacket
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000044E0 File Offset: 0x000026E0
		protected bool GetFlag(DataPacket.PacketFlags flag)
		{
			return (this._packetFlags & flag) == flag;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000044EE File Offset: 0x000026EE
		protected void SetFlag(DataPacket.PacketFlags flag, bool value)
		{
			if (value)
			{
				this._packetFlags |= flag;
				return;
			}
			this._packetFlags &= ~flag;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004514 File Offset: 0x00002714
		protected DataPacket(int length)
		{
			this.Length = length;
		}

		// Token: 0x06000072 RID: 114
		protected abstract int ReadNextByte();

		// Token: 0x06000073 RID: 115 RVA: 0x00004523 File Offset: 0x00002723
		public virtual void Done()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004528 File Offset: 0x00002728
		public ulong TryPeekBits(int count, out int bitsRead)
		{
			if (count < 0 || count > 64)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				bitsRead = 0;
				return 0UL;
			}
			ulong num2;
			while (this._bitCount < count)
			{
				int num = this.ReadNextByte();
				if (num == -1)
				{
					bitsRead = this._bitCount;
					num2 = this._bitBucket;
					this._bitBucket = 0UL;
					this._bitCount = 0;
					this.IsShort = true;
					return num2;
				}
				this._bitBucket = (ulong)(((long)(num & 255) << this._bitCount) | (long)this._bitBucket);
				this._bitCount += 8;
				if (this._bitCount > 64)
				{
					this._overflowBits = (byte)(num >> 72 - this._bitCount);
				}
			}
			num2 = this._bitBucket;
			if (count < 64)
			{
				num2 &= (1UL << count) - 1UL;
			}
			bitsRead = count;
			return num2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004600 File Offset: 0x00002800
		public void SkipBits(int count)
		{
			if (count == 0)
			{
				return;
			}
			if (this._bitCount > count)
			{
				if (count > 63)
				{
					this._bitBucket = 0UL;
				}
				else
				{
					this._bitBucket >>= count;
				}
				if (this._bitCount > 64)
				{
					int num = this._bitCount - 64;
					this._bitBucket |= (ulong)this._overflowBits << this._bitCount - count - num;
					if (num > count)
					{
						this._overflowBits = (byte)(this._overflowBits >> count);
					}
				}
				this._bitCount -= count;
				this._readBits += count;
				return;
			}
			if (this._bitCount == count)
			{
				this._bitBucket = 0UL;
				this._bitCount = 0;
				this._readBits += count;
				return;
			}
			count -= this._bitCount;
			this._readBits += this._bitCount;
			this._bitCount = 0;
			this._bitBucket = 0UL;
			while (count > 8)
			{
				if (this.ReadNextByte() == -1)
				{
					count = 0;
					this.IsShort = true;
					break;
				}
				count -= 8;
				this._readBits += 8;
			}
			if (count > 0)
			{
				int num2 = this.ReadNextByte();
				if (num2 == -1)
				{
					this.IsShort = true;
					return;
				}
				this._bitBucket = (ulong)((long)(num2 >> count));
				this._bitCount = 8 - count;
				this._readBits += count;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004762 File Offset: 0x00002962
		protected void ResetBitReader()
		{
			this._bitBucket = 0UL;
			this._bitCount = 0;
			this._readBits = 0;
			this.IsShort = false;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004781 File Offset: 0x00002981
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000478A File Offset: 0x0000298A
		public bool IsResync
		{
			get
			{
				return this.GetFlag(DataPacket.PacketFlags.IsResync);
			}
			internal set
			{
				this.SetFlag(DataPacket.PacketFlags.IsResync, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004794 File Offset: 0x00002994
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000479C File Offset: 0x0000299C
		public long GranulePosition
		{
			get
			{
				return this._granulePosition;
			}
			set
			{
				this._granulePosition = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000047A5 File Offset: 0x000029A5
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000047AD File Offset: 0x000029AD
		public long PageGranulePosition
		{
			get
			{
				return this._pageGranulePosition;
			}
			internal set
			{
				this._pageGranulePosition = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000047B6 File Offset: 0x000029B6
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000047BE File Offset: 0x000029BE
		public int Length
		{
			get
			{
				return this._length;
			}
			protected set
			{
				this._length = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000047C7 File Offset: 0x000029C7
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000047D0 File Offset: 0x000029D0
		public bool IsEndOfStream
		{
			get
			{
				return this.GetFlag(DataPacket.PacketFlags.IsEndOfStream);
			}
			internal set
			{
				this.SetFlag(DataPacket.PacketFlags.IsEndOfStream, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000047DA File Offset: 0x000029DA
		public long BitsRead
		{
			get
			{
				return (long)this._readBits;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000047E4 File Offset: 0x000029E4
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000480F File Offset: 0x00002A0F
		public int? GranuleCount
		{
			get
			{
				if (this.GetFlag(DataPacket.PacketFlags.HasGranuleCount))
				{
					return new int?(this._granuleCount);
				}
				return default(int?);
			}
			set
			{
				if (value != null)
				{
					this._granuleCount = value.Value;
					this.SetFlag(DataPacket.PacketFlags.HasGranuleCount, true);
					return;
				}
				this.SetFlag(DataPacket.PacketFlags.HasGranuleCount, false);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004838 File Offset: 0x00002A38
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004840 File Offset: 0x00002A40
		internal int PageSequenceNumber
		{
			get
			{
				return this._pageSequenceNumber;
			}
			set
			{
				this._pageSequenceNumber = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004849 File Offset: 0x00002A49
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004852 File Offset: 0x00002A52
		internal bool IsShort
		{
			get
			{
				return this.GetFlag(DataPacket.PacketFlags.IsShort);
			}
			private set
			{
				this.SetFlag(DataPacket.PacketFlags.IsShort, value);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000485C File Offset: 0x00002A5C
		public ulong ReadBits(int count)
		{
			if (count == 0)
			{
				return 0UL;
			}
			int num2;
			ulong num = this.TryPeekBits(count, out num2);
			this.SkipBits(count);
			return num;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004884 File Offset: 0x00002A84
		public byte PeekByte()
		{
			int num;
			return (byte)this.TryPeekBits(8, out num);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000489B File Offset: 0x00002A9B
		public byte ReadByte()
		{
			return (byte)this.ReadBits(8);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000048A8 File Offset: 0x00002AA8
		public byte[] ReadBytes(int count)
		{
			List<byte> list = new List<byte>(count);
			while (list.Count < count)
			{
				list.Add(this.ReadByte());
			}
			return list.ToArray();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000048DC File Offset: 0x00002ADC
		public int Read(byte[] buffer, int index, int count)
		{
			if (index < 0 || index + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			for (int i = 0; i < count; i++)
			{
				int num;
				byte b = (byte)this.TryPeekBits(8, out num);
				if (num == 0)
				{
					return i;
				}
				buffer[index++] = b;
				this.SkipBits(8);
			}
			return count;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000492D File Offset: 0x00002B2D
		public bool ReadBit()
		{
			return this.ReadBits(1) == 1UL;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000493A File Offset: 0x00002B3A
		public short ReadInt16()
		{
			return (short)this.ReadBits(16);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004945 File Offset: 0x00002B45
		public int ReadInt32()
		{
			return (int)this.ReadBits(32);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004950 File Offset: 0x00002B50
		public long ReadInt64()
		{
			return (long)this.ReadBits(64);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000495A File Offset: 0x00002B5A
		public ushort ReadUInt16()
		{
			return (ushort)this.ReadBits(16);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004965 File Offset: 0x00002B65
		public uint ReadUInt32()
		{
			return (uint)this.ReadBits(32);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004970 File Offset: 0x00002B70
		public ulong ReadUInt64()
		{
			return this.ReadBits(64);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000497A File Offset: 0x00002B7A
		public void SkipBytes(int count)
		{
			this.SkipBits(count * 8);
		}

		// Token: 0x04000030 RID: 48
		private ulong _bitBucket;

		// Token: 0x04000031 RID: 49
		private int _bitCount;

		// Token: 0x04000032 RID: 50
		private int _readBits;

		// Token: 0x04000033 RID: 51
		private byte _overflowBits;

		// Token: 0x04000034 RID: 52
		private DataPacket.PacketFlags _packetFlags;

		// Token: 0x04000035 RID: 53
		private long _granulePosition;

		// Token: 0x04000036 RID: 54
		private long _pageGranulePosition;

		// Token: 0x04000037 RID: 55
		private int _length;

		// Token: 0x04000038 RID: 56
		private int _granuleCount;

		// Token: 0x04000039 RID: 57
		private int _pageSequenceNumber;

		// Token: 0x0200000D RID: 13
		[Flags]
		protected enum PacketFlags : byte
		{
			// Token: 0x0400003B RID: 59
			IsResync = 1,
			// Token: 0x0400003C RID: 60
			IsEndOfStream = 2,
			// Token: 0x0400003D RID: 61
			IsShort = 4,
			// Token: 0x0400003E RID: 62
			HasGranuleCount = 8,
			// Token: 0x0400003F RID: 63
			User1 = 16,
			// Token: 0x04000040 RID: 64
			User2 = 32,
			// Token: 0x04000041 RID: 65
			User3 = 64,
			// Token: 0x04000042 RID: 66
			User4 = 128
		}
	}
}
