using System;
using System.Collections.Generic;
using Genkit;

namespace XRL.World.Conversations
{
	// Token: 0x02000BEE RID: 3054
	[Serializable]
	public class FixedHashSet : IComposite
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06007ABA RID: 31418 RVA: 0x002E17CC File Offset: 0x002DF9CC
		public int Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x002E17D4 File Offset: 0x002DF9D4
		public FixedHashSet()
		{
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x002E17FC File Offset: 0x002DF9FC
		public FixedHashSet(ICollection<ulong> Range)
		{
			this.Resize(Range.Count);
			foreach (ulong num in Range)
			{
				this.Add(num);
			}
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x002E1874 File Offset: 0x002DFA74
		public bool Contains(ulong Hash)
		{
			if (this.Length == 0)
			{
				return false;
			}
			ulong num = Hash % (ulong)((long)this.Size);
			for (int i = this.Buckets[(int)(checked((IntPtr)num))] - 1; i >= 0; i = this.Slots[i].Next)
			{
				if (this.Slots[i].Hash == Hash)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x002E18D0 File Offset: 0x002DFAD0
		public bool Add(ulong Hash)
		{
			ulong num = 0UL;
			if (this.Length > 0)
			{
				num = Hash % (ulong)((long)this.Size);
				for (int i = this.Buckets[(int)(checked((IntPtr)num))] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].Hash == Hash)
					{
						return false;
					}
				}
			}
			int num2;
			if (this.Free >= 0)
			{
				num2 = this.Free;
				this.Free = this.Slots[num2].Next;
			}
			else
			{
				if (this.Last == this.Size)
				{
					this.Resize(this.Size * 2);
					num = Hash % (ulong)((long)this.Size);
				}
				num2 = this.Last;
				this.Last++;
			}
			this.Slots[num2].Hash = Hash;
			this.Slots[num2].Next = this.Buckets[(int)(checked((IntPtr)num))] - 1;
			this.Buckets[(int)(checked((IntPtr)num))] = num2 + 1;
			this.Length++;
			return true;
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x002E19DC File Offset: 0x002DFBDC
		public bool Remove(ulong Hash)
		{
			ulong num = Hash % (ulong)((long)this.Size);
			int num2 = -1;
			for (int i = this.Buckets[(int)(checked((IntPtr)num))] - 1; i >= 0; i = this.Slots[i].Next)
			{
				if (this.Slots[i].Hash == Hash)
				{
					if (num2 < 0)
					{
						this.Buckets[(int)(checked((IntPtr)num))] = this.Slots[i].Next + 1;
					}
					else
					{
						this.Slots[num2].Next = this.Slots[i].Next;
					}
					this.Slots[i].Hash = 0UL;
					this.Slots[i].Next = this.Free;
					int num3 = this.Length - 1;
					this.Length = num3;
					if (num3 == 0)
					{
						this.Last = 0;
						this.Free = -1;
					}
					else
					{
						this.Free = i;
					}
					return true;
				}
				num2 = i;
			}
			return false;
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x002E1AD4 File Offset: 0x002DFCD4
		public void Clear()
		{
			if (this.Length > 0)
			{
				Array.Clear(this.Buckets, 0, this.Size);
				Array.Clear(this.Slots, 0, this.Last);
				this.Free = -1;
				this.Length = 0;
				this.Last = 0;
			}
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x002E1B24 File Offset: 0x002DFD24
		protected void Resize(int Capacity)
		{
			Capacity = Hash.GetCapacity(Capacity);
			if (Capacity == this.Size)
			{
				return;
			}
			int[] array = new int[Capacity];
			FixedHashSet.Slot[] array2 = new FixedHashSet.Slot[Capacity];
			for (int i = 0; i < this.Last; i++)
			{
				ulong num = this.Slots[i].Hash % (ulong)((long)Capacity);
				array2[i].Hash = this.Slots[i].Hash;
				array2[i].Next = array[(int)(checked((IntPtr)num))] - 1;
				array[(int)(checked((IntPtr)num))] = i + 1;
			}
			this.Buckets = array;
			this.Slots = array2;
			this.Size = Capacity;
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x002E1BC4 File Offset: 0x002DFDC4
		public ulong[] GetValues()
		{
			ulong[] array = new ulong[this.Length];
			int i = 0;
			int num = 0;
			while (i < this.Last)
			{
				if (this.Slots[i].Hash != 0UL)
				{
					array[num++] = this.Slots[i].Hash;
				}
				i++;
			}
			return array;
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x002E1C1C File Offset: 0x002DFE1C
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Length);
			int num = 0;
			for (int i = 0; i < this.Last; i++)
			{
				if (this.Slots[i].Hash != 0UL)
				{
					Writer.WriteOptimized(this.Slots[i].Hash);
					num++;
				}
			}
			if (num != this.Length)
			{
				MetricsManager.LogError(string.Format("Bad news {0} vs {1}", num, this.Length));
			}
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x002E1CA0 File Offset: 0x002DFEA0
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num > this.Size)
			{
				this.Resize(num);
			}
			for (int i = 0; i < num; i++)
			{
				this.Add(Reader.ReadOptimizedUInt64());
			}
		}

		// Token: 0x040037F4 RID: 14324
		private int[] Buckets = Array.Empty<int>();

		// Token: 0x040037F5 RID: 14325
		private FixedHashSet.Slot[] Slots = Array.Empty<FixedHashSet.Slot>();

		// Token: 0x040037F6 RID: 14326
		private int Size;

		// Token: 0x040037F7 RID: 14327
		private int Last;

		// Token: 0x040037F8 RID: 14328
		private int Free = -1;

		// Token: 0x040037F9 RID: 14329
		private int Length;

		// Token: 0x02001C1B RID: 7195
		private struct Slot
		{
			// Token: 0x04006F3B RID: 28475
			public ulong Hash;

			// Token: 0x04006F3C RID: 28476
			public int Next;
		}
	}
}
