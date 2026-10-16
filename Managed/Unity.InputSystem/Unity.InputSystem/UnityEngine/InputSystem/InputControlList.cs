using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000033 RID: 51
	[DebuggerDisplay("Count = {Count}")]
	public struct InputControlList<TControl> : IList<TControl>, ICollection<TControl>, IEnumerable<TControl>, IEnumerable, IReadOnlyList<TControl>, IReadOnlyCollection<TControl>, IDisposable where TControl : InputControl
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00012051 File Offset: 0x00010251
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00012059 File Offset: 0x00010259
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00012078 File Offset: 0x00010278
		public int Capacity
		{
			get
			{
				if (!this.m_Indices.IsCreated)
				{
					return 0;
				}
				return this.m_Indices.Length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Capacity cannot be negative", "value");
				}
				if (value == 0)
				{
					if (this.m_Count != 0)
					{
						this.m_Indices.Dispose();
					}
					this.m_Count = 0;
					return;
				}
				Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
				ArrayHelpers.Resize<ulong>(ref this.m_Indices, value, allocator);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000120D8 File Offset: 0x000102D8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000120 RID: 288
		public TControl this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				return InputControlList<TControl>.FromIndex(this.m_Indices[index]);
			}
			set
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				this.m_Indices[index] = InputControlList<TControl>.ToIndex(value);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00012182 File Offset: 0x00010382
		public InputControlList(Allocator allocator, int initialCapacity = 0)
		{
			this.m_Allocator = allocator;
			this.m_Indices = default(NativeArray<ulong>);
			this.m_Count = 0;
			if (initialCapacity != 0)
			{
				this.Capacity = initialCapacity;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000121A8 File Offset: 0x000103A8
		public InputControlList(IEnumerable<TControl> values, Allocator allocator = Allocator.Persistent)
		{
			this = new InputControlList<TControl>(allocator, 0);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (TControl tcontrol in values)
			{
				this.Add(tcontrol);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00012208 File Offset: 0x00010408
		public InputControlList(params TControl[] values)
		{
			this = default(InputControlList<TControl>);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = values.Length;
			this.Capacity = Mathf.Max(num, 10);
			for (int i = 0; i < num; i++)
			{
				this.Add(values[i]);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00012258 File Offset: 0x00010458
		public unsafe void Resize(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "Size cannot be negative");
			}
			if (this.Capacity < size)
			{
				this.Capacity = size;
			}
			if (size > this.Count)
			{
				UnsafeUtility.MemSet((void*)((byte*)this.m_Indices.GetUnsafePtr<ulong>() + this.Count * 8), byte.MaxValue, (long)(size - this.Count));
			}
			this.m_Count = size;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000122C0 File Offset: 0x000104C0
		public void Add(TControl item)
		{
			ulong num = InputControlList<TControl>.ToIndex(item);
			Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
			ArrayHelpers.AppendWithCapacity<ulong>(ref this.m_Indices, ref this.m_Count, num, 10, allocator);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000122FC File Offset: 0x000104FC
		public void AddSlice<TList>(TList list, int count = -1, int destinationIndex = -1, int sourceIndex = 0) where TList : IReadOnlyList<TControl>
		{
			if (count < 0)
			{
				count = list.Count;
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (sourceIndex + count > list.Count)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count of {0} elements starting at index {1} exceeds length of list of {2}", count, sourceIndex, list.Count));
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			for (int i = 0; i < count; i++)
			{
				this.m_Indices[destinationIndex + i] = InputControlList<TControl>.ToIndex(list[sourceIndex + i]);
			}
			this.m_Count += count;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000123FC File Offset: 0x000105FC
		public void AddRange(IEnumerable<TControl> list, int count = -1, int destinationIndex = -1)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (count < 0)
			{
				count = list.Count<TControl>();
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			foreach (TControl tcontrol in list)
			{
				this.m_Indices[destinationIndex++] = InputControlList<TControl>.ToIndex(tcontrol);
				this.m_Count++;
				count--;
				if (count == 0)
				{
					break;
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000124E4 File Offset: 0x000106E4
		public bool Remove(TControl item)
		{
			if (this.m_Count == 0)
			{
				return false;
			}
			ulong num = InputControlList<TControl>.ToIndex(item);
			for (int i = 0; i < this.m_Count; i++)
			{
				if (this.m_Indices[i] == num)
				{
					ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012538 File Offset: 0x00010738
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} elements", index, this.m_Count));
			}
			ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, index);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001258A File Offset: 0x0001078A
		public void CopyTo(TControl[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012591 File Offset: 0x00010791
		public int IndexOf(TControl item)
		{
			return this.IndexOf(item, 0, -1);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001259C File Offset: 0x0001079C
		public unsafe int IndexOf(TControl item, int startIndex, int count = -1)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be negative");
			}
			if (this.m_Count == 0)
			{
				return -1;
			}
			if (count < 0)
			{
				count = Mathf.Max(this.m_Count - startIndex, 0);
			}
			if (startIndex + count > this.m_Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			ulong num = InputControlList<TControl>.ToIndex(item);
			ulong* unsafeReadOnlyPtr = (ulong*)this.m_Indices.GetUnsafeReadOnlyPtr<ulong>();
			for (int i = 0; i < count; i++)
			{
				if (unsafeReadOnlyPtr[startIndex + i] == num)
				{
					return startIndex + i;
				}
			}
			return -1;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012621 File Offset: 0x00010821
		public void Insert(int index, TControl item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00012628 File Offset: 0x00010828
		public void Clear()
		{
			this.m_Count = 0;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012631 File Offset: 0x00010831
		public bool Contains(TControl item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00012640 File Offset: 0x00010840
		public bool Contains(TControl item, int startIndex, int count = -1)
		{
			return this.IndexOf(item, startIndex, count) != -1;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012654 File Offset: 0x00010854
		public void SwapElements(int index1, int index2)
		{
			if (index1 < 0 || index1 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index1");
			}
			if (index2 < 0 || index2 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index2");
			}
			if (index1 != index2)
			{
				this.m_Indices.SwapElements<ulong>(index1, index2);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000126A4 File Offset: 0x000108A4
		public void Sort<TCompare>(int startIndex, int count, TCompare comparer) where TCompare : IComparer<TControl>
		{
			if (startIndex < 0 || startIndex >= this.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (startIndex + count >= this.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (int i = 1; i < count; i++)
			{
				int num = i;
				while (num > 0 && comparer.Compare(this[num - 1], this[num]) < 0)
				{
					this.SwapElements(num, num - 1);
					num--;
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012724 File Offset: 0x00010924
		public TControl[] ToArray(bool dispose = false)
		{
			TControl[] array = new TControl[this.m_Count];
			for (int i = 0; i < this.m_Count; i++)
			{
				array[i] = this[i];
			}
			if (dispose)
			{
				this.Dispose();
			}
			return array;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00012768 File Offset: 0x00010968
		internal void AppendTo(ref TControl[] array, ref int count)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				ArrayHelpers.AppendWithCapacity<TControl>(ref array, ref count, this[i], 10);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012797 File Offset: 0x00010997
		public void Dispose()
		{
			if (this.m_Indices.IsCreated)
			{
				this.m_Indices.Dispose();
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000127B1 File Offset: 0x000109B1
		public IEnumerator<TControl> GetEnumerator()
		{
			return new InputControlList<TControl>.Enumerator(this);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000127C3 File Offset: 0x000109C3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000127CC File Offset: 0x000109CC
		public override string ToString()
		{
			if (this.Count == 0)
			{
				return "()";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this[i]);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012838 File Offset: 0x00010A38
		private static ulong ToIndex(TControl control)
		{
			if (control == null)
			{
				return ulong.MaxValue;
			}
			InputDevice device = control.device;
			int deviceId = device.m_DeviceId;
			int num = ((device != control) ? (device.m_ChildrenForEachControl.IndexOfReference(control, -1) + 1) : 0);
			ulong num2 = (ulong)((ulong)((long)deviceId) << 32);
			ulong num3 = (ulong)((long)num);
			return num2 | num3;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00012890 File Offset: 0x00010A90
		private static TControl FromIndex(ulong index)
		{
			if (index == 18446744073709551615UL)
			{
				return default(TControl);
			}
			int num = (int)(index >> 32);
			int num2 = (int)(index & (ulong)(-1));
			InputDevice deviceById = InputSystem.GetDeviceById(num);
			if (deviceById == null)
			{
				return default(TControl);
			}
			if (num2 == 0)
			{
				return (TControl)((object)deviceById);
			}
			return (TControl)((object)deviceById.m_ChildrenForEachControl[num2 - 1]);
		}

		// Token: 0x04000145 RID: 325
		private int m_Count;

		// Token: 0x04000146 RID: 326
		private NativeArray<ulong> m_Indices;

		// Token: 0x04000147 RID: 327
		private readonly Allocator m_Allocator;

		// Token: 0x04000148 RID: 328
		private const ulong kInvalidIndex = 18446744073709551615UL;

		// Token: 0x02000194 RID: 404
		private struct Enumerator : IEnumerator<TControl>, IEnumerator, IDisposable
		{
			// Token: 0x06001420 RID: 5152 RVA: 0x0005D813 File Offset: 0x0005BA13
			public unsafe Enumerator(InputControlList<TControl> list)
			{
				this.m_Count = list.m_Count;
				this.m_Current = -1;
				this.m_Indices = (ulong*)((this.m_Count > 0) ? list.m_Indices.GetUnsafeReadOnlyPtr<ulong>() : null);
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x0005D846 File Offset: 0x0005BA46
			public bool MoveNext()
			{
				if (this.m_Current >= this.m_Count)
				{
					return false;
				}
				this.m_Current++;
				return this.m_Current != this.m_Count;
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x0005D877 File Offset: 0x0005BA77
			public void Reset()
			{
				this.m_Current = -1;
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001423 RID: 5155 RVA: 0x0005D880 File Offset: 0x0005BA80
			public unsafe TControl Current
			{
				get
				{
					if (this.m_Indices == null)
					{
						throw new InvalidOperationException("Enumerator is not valid");
					}
					return InputControlList<TControl>.FromIndex(this.m_Indices[this.m_Current]);
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001424 RID: 5156 RVA: 0x0005D8AD File Offset: 0x0005BAAD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x0005D8BA File Offset: 0x0005BABA
			public void Dispose()
			{
			}

			// Token: 0x040008DD RID: 2269
			private unsafe readonly ulong* m_Indices;

			// Token: 0x040008DE RID: 2270
			private readonly int m_Count;

			// Token: 0x040008DF RID: 2271
			private int m_Current;
		}
	}
}
