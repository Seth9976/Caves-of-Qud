using System;

namespace NVorbis.Ogg
{
	// Token: 0x0200000F RID: 15
	internal class Packet : DataPacket
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004A2A File Offset: 0x00002C2A
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004A32 File Offset: 0x00002C32
		internal Packet Next
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004A3B File Offset: 0x00002C3B
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004A43 File Offset: 0x00002C43
		internal Packet Prev
		{
			get
			{
				return this._prev;
			}
			set
			{
				this._prev = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004A4C File Offset: 0x00002C4C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004A56 File Offset: 0x00002C56
		internal bool IsContinued
		{
			get
			{
				return base.GetFlag(DataPacket.PacketFlags.User1);
			}
			set
			{
				base.SetFlag(DataPacket.PacketFlags.User1, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A61 File Offset: 0x00002C61
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004A6B File Offset: 0x00002C6B
		internal bool IsContinuation
		{
			get
			{
				return base.GetFlag(DataPacket.PacketFlags.User2);
			}
			set
			{
				base.SetFlag(DataPacket.PacketFlags.User2, value);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004A76 File Offset: 0x00002C76
		internal Packet(ContainerReader containerReader, long streamOffset, int length)
			: base(length)
		{
			this._containerReader = containerReader;
			this._offset = streamOffset;
			this._length = length;
			this._curOfs = 0;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004A9C File Offset: 0x00002C9C
		internal void MergeWith(DataPacket continuation)
		{
			Packet packet = continuation as Packet;
			if (packet == null)
			{
				throw new ArgumentException("Incorrect packet type!");
			}
			base.Length += continuation.Length;
			if (this._mergedPacket == null)
			{
				this._mergedPacket = packet;
			}
			else
			{
				this._mergedPacket.MergeWith(continuation);
			}
			base.PageGranulePosition = continuation.PageGranulePosition;
			base.PageSequenceNumber = continuation.PageSequenceNumber;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B06 File Offset: 0x00002D06
		internal void Reset()
		{
			this._curOfs = 0;
			base.ResetBitReader();
			if (this._mergedPacket != null)
			{
				this._mergedPacket.Reset();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B28 File Offset: 0x00002D28
		protected override int ReadNextByte()
		{
			if (this._curOfs != this._length)
			{
				int num = this._containerReader.PacketReadByte(this._offset + (long)this._curOfs);
				if (num != -1)
				{
					this._curOfs++;
				}
				return num;
			}
			if (this._mergedPacket == null)
			{
				return -1;
			}
			return this._mergedPacket.ReadNextByte();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B86 File Offset: 0x00002D86
		public override void Done()
		{
			if (this._mergedPacket != null)
			{
				this._mergedPacket.Done();
				return;
			}
			this._containerReader.PacketDiscardThrough(this._offset + (long)this._length);
		}

		// Token: 0x04000046 RID: 70
		private long _offset;

		// Token: 0x04000047 RID: 71
		private int _length;

		// Token: 0x04000048 RID: 72
		private int _curOfs;

		// Token: 0x04000049 RID: 73
		private Packet _mergedPacket;

		// Token: 0x0400004A RID: 74
		private Packet _next;

		// Token: 0x0400004B RID: 75
		private Packet _prev;

		// Token: 0x0400004C RID: 76
		private ContainerReader _containerReader;
	}
}
