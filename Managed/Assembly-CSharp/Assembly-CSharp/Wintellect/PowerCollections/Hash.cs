using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F8 RID: 760
	[Serializable]
	internal class Hash<T> : IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x00087B24 File Offset: 0x00085D24
		public Hash(IEqualityComparer<T> equalityComparer)
		{
			this.equalityComparer = equalityComparer;
			this.loadFactor = 0.7f;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00087B3E File Offset: 0x00085D3E
		internal int GetEnumerationStamp()
		{
			return this.changeStamp;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00087B46 File Offset: 0x00085D46
		internal void StopEnumerations()
		{
			this.changeStamp++;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00087B56 File Offset: 0x00085D56
		internal void CheckEnumerationStamp(int startStamp)
		{
			if (startStamp != this.changeStamp)
			{
				throw new InvalidOperationException(Strings.ChangeDuringEnumeration);
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00087B6C File Offset: 0x00085D6C
		private int GetFullHash(T item)
		{
			uint num = (uint)Util.GetHashCode<T>(item, this.equalityComparer);
			num += ~(num << 15);
			num ^= num >> 10;
			num += num << 3;
			num ^= num >> 6;
			num += ~(num << 11);
			num ^= num >> 16;
			num &= 2147483647U;
			if (num == 0U)
			{
				num = 2147483647U;
			}
			return (int)num;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00087BC2 File Offset: 0x00085DC2
		private void GetHashValuesFromFullHash(int hash, out int initialBucket, out int skip)
		{
			initialBucket = hash & this.hashMask;
			skip = ((hash >> this.secondaryShift) & this.hashMask) | 1;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00087BE4 File Offset: 0x00085DE4
		private int GetHashValues(T item, out int initialBucket, out int skip)
		{
			int fullHash = this.GetFullHash(item);
			this.GetHashValuesFromFullHash(fullHash, out initialBucket, out skip);
			return fullHash;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00087C04 File Offset: 0x00085E04
		private void EnsureEnoughSlots(int additionalItems)
		{
			this.StopEnumerations();
			if (this.usedSlots + additionalItems > this.thresholdGrow)
			{
				int num = Math.Max(this.totalSlots, 16);
				while ((int)((float)num * this.loadFactor) < this.usedSlots + additionalItems)
				{
					num *= 2;
					if (num <= 0)
					{
						throw new InvalidOperationException(Strings.CollectionTooLarge);
					}
				}
				this.ResizeTable(num);
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00087C68 File Offset: 0x00085E68
		private void ShrinkIfNeeded()
		{
			if (this.count < this.thresholdShrink)
			{
				int num;
				if (this.count > 0)
				{
					num = 16;
					while ((int)((float)num * this.loadFactor) < this.count)
					{
						num *= 2;
					}
				}
				else
				{
					num = 0;
				}
				this.ResizeTable(num);
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00087CB4 File Offset: 0x00085EB4
		private int GetSecondaryShift(int newSize)
		{
			int num = newSize - 2;
			int num2 = 0;
			while ((num & 1073741824) == 0)
			{
				num <<= 1;
				num2++;
			}
			return num2;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00087CDC File Offset: 0x00085EDC
		private void ResizeTable(int newSize)
		{
			Hash<T>.Slot[] array = this.table;
			this.totalSlots = newSize;
			this.thresholdGrow = (int)((float)this.totalSlots * this.loadFactor);
			this.thresholdShrink = this.thresholdGrow / 3;
			if (this.thresholdShrink <= 16)
			{
				this.thresholdShrink = 1;
			}
			this.hashMask = newSize - 1;
			this.secondaryShift = this.GetSecondaryShift(newSize);
			if (this.totalSlots > 0)
			{
				this.table = new Hash<T>.Slot[this.totalSlots];
			}
			else
			{
				this.table = null;
			}
			if (array != null && this.table != null)
			{
				foreach (Hash<T>.Slot slot in array)
				{
					int hashValue = slot.HashValue;
					int num;
					int num2;
					this.GetHashValuesFromFullHash(hashValue, out num, out num2);
					while (!this.table[num].Empty)
					{
						this.table[num].Collision = true;
						num = (num + num2) & this.hashMask;
					}
					this.table[num].HashValue = hashValue;
					this.table[num].item = slot.item;
				}
			}
			this.usedSlots = this.count;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00087E17 File Offset: 0x00086017
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00087E1F File Offset: 0x0008601F
		internal int SlotCount
		{
			get
			{
				return this.totalSlots;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00087E27 File Offset: 0x00086027
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00087E30 File Offset: 0x00086030
		public float LoadFactor
		{
			get
			{
				return this.loadFactor;
			}
			set
			{
				if ((double)value < 0.25 || (double)value > 0.95)
				{
					throw new ArgumentOutOfRangeException("value", value, Strings.InvalidLoadFactor);
				}
				this.StopEnumerations();
				bool flag = value < this.loadFactor;
				this.loadFactor = value;
				this.thresholdGrow = (int)((float)this.totalSlots * this.loadFactor);
				this.thresholdShrink = this.thresholdGrow / 3;
				if (this.thresholdShrink <= 16)
				{
					this.thresholdShrink = 1;
				}
				if (flag)
				{
					this.EnsureEnoughSlots(0);
					return;
				}
				this.ShrinkIfNeeded();
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00087EC8 File Offset: 0x000860C8
		public bool Insert(T item, bool replaceOnDuplicate, out T previous)
		{
			int num = -1;
			bool flag = true;
			this.EnsureEnoughSlots(1);
			int num2;
			int num3;
			int hashValues = this.GetHashValues(item, out num2, out num3);
			for (;;)
			{
				if (this.table[num2].Empty)
				{
					if (num == -1)
					{
						num = num2;
					}
					if (!flag)
					{
						break;
					}
					if (!this.table[num2].Collision)
					{
						break;
					}
				}
				else
				{
					if (this.table[num2].HashValue == hashValues && this.equalityComparer.Equals(this.table[num2].item, item))
					{
						goto Block_6;
					}
					if (!this.table[num2].Collision)
					{
						if (num >= 0)
						{
							break;
						}
						this.table[num2].Collision = true;
						flag = false;
					}
				}
				num2 = (num2 + num3) & this.hashMask;
			}
			goto IL_00F0;
			Block_6:
			previous = this.table[num2].item;
			if (replaceOnDuplicate)
			{
				this.table[num2].item = item;
			}
			return false;
			IL_00F0:
			this.table[num].HashValue = hashValues;
			this.table[num].item = item;
			this.count++;
			if (!this.table[num].Collision)
			{
				this.usedSlots++;
			}
			previous = default(T);
			return true;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00088020 File Offset: 0x00086220
		public bool Delete(T item, out T itemDeleted)
		{
			this.StopEnumerations();
			if (this.count == 0)
			{
				itemDeleted = default(T);
				return false;
			}
			int num;
			int num2;
			int hashValues = this.GetHashValues(item, out num, out num2);
			while (this.table[num].HashValue != hashValues || !this.equalityComparer.Equals(this.table[num].item, item))
			{
				if (!this.table[num].Collision)
				{
					itemDeleted = default(T);
					return false;
				}
				num = (num + num2) & this.hashMask;
			}
			itemDeleted = this.table[num].item;
			this.table[num].Clear();
			this.count--;
			if (!this.table[num].Collision)
			{
				this.usedSlots--;
			}
			this.ShrinkIfNeeded();
			return true;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00088110 File Offset: 0x00086310
		public bool Find(T find, bool replace, out T item)
		{
			if (this.count == 0)
			{
				item = default(T);
				return false;
			}
			int num;
			int num2;
			int hashValues = this.GetHashValues(find, out num, out num2);
			while (this.table[num].HashValue != hashValues || !this.equalityComparer.Equals(this.table[num].item, find))
			{
				if (!this.table[num].Collision)
				{
					item = default(T);
					return false;
				}
				num = (num + num2) & this.hashMask;
			}
			item = this.table[num].item;
			if (replace)
			{
				this.table[num].item = find;
			}
			return true;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000881C6 File Offset: 0x000863C6
		public IEnumerator<T> GetEnumerator()
		{
			if (this.count > 0)
			{
				int startStamp = this.changeStamp;
				foreach (Hash<T>.Slot slot in this.table)
				{
					if (!slot.Empty)
					{
						yield return slot.item;
						this.CheckEnumerationStamp(startStamp);
					}
				}
				Hash<T>.Slot[] array = null;
			}
			yield break;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000881D5 File Offset: 0x000863D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000881E0 File Offset: 0x000863E0
		public Hash<T> Clone(Converter<T, T> cloneItem)
		{
			Hash<T> hash = new Hash<T>(this.equalityComparer);
			hash.count = this.count;
			hash.usedSlots = this.usedSlots;
			hash.totalSlots = this.totalSlots;
			hash.loadFactor = this.loadFactor;
			hash.thresholdGrow = this.thresholdGrow;
			hash.thresholdShrink = this.thresholdShrink;
			hash.hashMask = this.hashMask;
			hash.secondaryShift = this.secondaryShift;
			if (this.table != null)
			{
				hash.table = (Hash<T>.Slot[])this.table.Clone();
				if (cloneItem != null)
				{
					for (int i = 0; i < this.table.Length; i++)
					{
						if (!this.table[i].Empty)
						{
							this.table[i].item = cloneItem(this.table[i].item);
						}
					}
				}
			}
			return hash;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000882CC File Offset: 0x000864CC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("equalityComparer", this.equalityComparer, typeof(IEqualityComparer<T>));
			info.AddValue("loadFactor", this.loadFactor, typeof(float));
			T[] array = new T[this.count];
			int num = 0;
			foreach (Hash<T>.Slot slot in this.table)
			{
				if (!slot.Empty)
				{
					array[num++] = slot.item;
				}
			}
			info.AddValue("items", array, typeof(T[]));
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0008837F File Offset: 0x0008657F
		protected Hash(SerializationInfo serInfo, StreamingContext context)
		{
			this.serializationInfo = serInfo;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00088390 File Offset: 0x00086590
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (this.serializationInfo == null)
			{
				return;
			}
			this.loadFactor = this.serializationInfo.GetSingle("loadFactor");
			this.equalityComparer = (IEqualityComparer<T>)this.serializationInfo.GetValue("equalityComparer", typeof(IEqualityComparer<T>));
			T[] array = (T[])this.serializationInfo.GetValue("items", typeof(T[]));
			this.EnsureEnoughSlots(array.Length);
			foreach (T t in array)
			{
				T t2;
				this.Insert(t, true, out t2);
			}
			this.serializationInfo = null;
		}

		// Token: 0x04000EFF RID: 3839
		private IEqualityComparer<T> equalityComparer;

		// Token: 0x04000F00 RID: 3840
		private int count;

		// Token: 0x04000F01 RID: 3841
		private int usedSlots;

		// Token: 0x04000F02 RID: 3842
		private int totalSlots;

		// Token: 0x04000F03 RID: 3843
		private float loadFactor;

		// Token: 0x04000F04 RID: 3844
		private int thresholdGrow;

		// Token: 0x04000F05 RID: 3845
		private int thresholdShrink;

		// Token: 0x04000F06 RID: 3846
		private int hashMask;

		// Token: 0x04000F07 RID: 3847
		private int secondaryShift;

		// Token: 0x04000F08 RID: 3848
		private Hash<T>.Slot[] table;

		// Token: 0x04000F09 RID: 3849
		private int changeStamp;

		// Token: 0x04000F0A RID: 3850
		private const int MINSIZE = 16;

		// Token: 0x04000F0B RID: 3851
		private SerializationInfo serializationInfo;

		// Token: 0x020016DE RID: 5854
		private struct Slot
		{
			// Token: 0x17000DAE RID: 3502
			// (get) Token: 0x0600DA93 RID: 55955 RVA: 0x00503D2F File Offset: 0x00501F2F
			// (set) Token: 0x0600DA94 RID: 55956 RVA: 0x00503D3D File Offset: 0x00501F3D
			public int HashValue
			{
				get
				{
					return (int)(this.hash_collision & 2147483647U);
				}
				set
				{
					this.hash_collision = (uint)(value | (int)(this.hash_collision & 2147483648U));
				}
			}

			// Token: 0x17000DAF RID: 3503
			// (get) Token: 0x0600DA95 RID: 55957 RVA: 0x00503D53 File Offset: 0x00501F53
			public bool Empty
			{
				get
				{
					return this.HashValue == 0;
				}
			}

			// Token: 0x0600DA96 RID: 55958 RVA: 0x00503D5E File Offset: 0x00501F5E
			public void Clear()
			{
				this.HashValue = 0;
				this.item = default(T);
			}

			// Token: 0x17000DB0 RID: 3504
			// (get) Token: 0x0600DA97 RID: 55959 RVA: 0x00503D73 File Offset: 0x00501F73
			// (set) Token: 0x0600DA98 RID: 55960 RVA: 0x00503D84 File Offset: 0x00501F84
			public bool Collision
			{
				get
				{
					return (this.hash_collision & 2147483648U) > 0U;
				}
				set
				{
					if (value)
					{
						this.hash_collision |= 2147483648U;
						return;
					}
					this.hash_collision &= 2147483647U;
				}
			}

			// Token: 0x04005C0B RID: 23563
			private uint hash_collision;

			// Token: 0x04005C0C RID: 23564
			public T item;
		}
	}
}
