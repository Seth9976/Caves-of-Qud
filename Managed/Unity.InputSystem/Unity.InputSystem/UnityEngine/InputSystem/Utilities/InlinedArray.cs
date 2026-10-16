using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000135 RID: 309
	internal struct InlinedArray<TValue> : IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00052495 File Offset: 0x00050695
		public int Capacity
		{
			get
			{
				TValue[] array = this.additionalValues;
				if (array == null)
				{
					return 1;
				}
				return array.Length + 1;
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000524A7 File Offset: 0x000506A7
		public InlinedArray(TValue value)
		{
			this.length = 1;
			this.firstValue = value;
			this.additionalValues = null;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000524BE File Offset: 0x000506BE
		public InlinedArray(TValue firstValue, params TValue[] additionalValues)
		{
			this.length = 1 + additionalValues.Length;
			this.firstValue = firstValue;
			this.additionalValues = additionalValues;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000524DC File Offset: 0x000506DC
		public InlinedArray(IEnumerable<TValue> values)
		{
			this = default(InlinedArray<TValue>);
			this.length = values.Count<TValue>();
			if (this.length > 1)
			{
				this.additionalValues = new TValue[this.length - 1];
			}
			else
			{
				this.additionalValues = null;
			}
			int num = 0;
			foreach (TValue tvalue in values)
			{
				if (num == 0)
				{
					this.firstValue = tvalue;
				}
				else
				{
					this.additionalValues[num - 1] = tvalue;
				}
				num++;
			}
		}

		// Token: 0x170004E0 RID: 1248
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					return this.firstValue;
				}
				return this.additionalValues[index - 1];
			}
			set
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					this.firstValue = value;
					return;
				}
				this.additionalValues[index - 1] = value;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000525DE File Offset: 0x000507DE
		public void Clear()
		{
			this.length = 0;
			this.firstValue = default(TValue);
			this.additionalValues = null;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000525FC File Offset: 0x000507FC
		public void ClearWithCapacity()
		{
			this.firstValue = default(TValue);
			for (int i = 0; i < this.length - 1; i++)
			{
				this.additionalValues[i] = default(TValue);
			}
			this.length = 0;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00052644 File Offset: 0x00050844
		public InlinedArray<TValue> Clone()
		{
			return new InlinedArray<TValue>
			{
				length = this.length,
				firstValue = this.firstValue,
				additionalValues = ((this.additionalValues != null) ? ArrayHelpers.Copy<TValue>(this.additionalValues) : null)
			};
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00052694 File Offset: 0x00050894
		public void SetLength(int size)
		{
			if (size < this.length)
			{
				for (int i = size; i < this.length; i++)
				{
					this[i] = default(TValue);
				}
			}
			this.length = size;
			if (size > 1 && (this.additionalValues == null || this.additionalValues.Length < size - 1))
			{
				Array.Resize<TValue>(ref this.additionalValues, size - 1);
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000526F9 File Offset: 0x000508F9
		public TValue[] ToArray()
		{
			return ArrayHelpers.Join<TValue>(this.firstValue, this.additionalValues);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0005270C File Offset: 0x0005090C
		public TOther[] ToArray<TOther>(Func<TValue, TOther> mapFunction)
		{
			if (this.length == 0)
			{
				return null;
			}
			TOther[] array = new TOther[this.length];
			for (int i = 0; i < this.length; i++)
			{
				array[i] = mapFunction(this[i]);
			}
			return array;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00052758 File Offset: 0x00050958
		public int IndexOf(TValue value)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			if (this.length > 0)
			{
				if (@default.Equals(this.firstValue, value))
				{
					return 0;
				}
				if (this.additionalValues != null)
				{
					for (int i = 0; i < this.length - 1; i++)
					{
						if (@default.Equals(this.additionalValues[i], value))
						{
							return i + 1;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000527BC File Offset: 0x000509BC
		public int Append(TValue value)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else if (this.additionalValues == null)
			{
				this.additionalValues = new TValue[1];
				this.additionalValues[0] = value;
			}
			else
			{
				Array.Resize<TValue>(ref this.additionalValues, this.length);
				this.additionalValues[this.length - 1] = value;
			}
			int num = this.length;
			this.length++;
			return num;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00052838 File Offset: 0x00050A38
		public int AppendWithCapacity(TValue value, int capacityIncrement = 10)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else
			{
				int num = this.length - 1;
				ArrayHelpers.AppendWithCapacity<TValue>(ref this.additionalValues, ref num, value, capacityIncrement);
			}
			int num2 = this.length;
			this.length++;
			return num2;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00052884 File Offset: 0x00050A84
		public void AssignWithCapacity(InlinedArray<TValue> values)
		{
			if (this.Capacity < values.length && values.length > 1)
			{
				this.additionalValues = new TValue[values.length - 1];
			}
			this.length = values.length;
			if (this.length > 0)
			{
				this.firstValue = values.firstValue;
			}
			if (this.length > 1)
			{
				Array.Copy(values.additionalValues, this.additionalValues, this.length - 1);
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00052900 File Offset: 0x00050B00
		public void Append(IEnumerable<TValue> values)
		{
			foreach (TValue tvalue in values)
			{
				this.Append(tvalue);
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0005294C File Offset: 0x00050B4C
		public void Remove(TValue value)
		{
			if (this.length < 1)
			{
				return;
			}
			if (EqualityComparer<TValue>.Default.Equals(this.firstValue, value))
			{
				this.RemoveAt(0);
				return;
			}
			if (this.additionalValues != null)
			{
				for (int i = 0; i < this.length - 1; i++)
				{
					if (EqualityComparer<TValue>.Default.Equals(this.additionalValues[i], value))
					{
						this.RemoveAt(i + 1);
						return;
					}
				}
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000529BC File Offset: 0x00050BBC
		public void RemoveAtWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.length == 1)
				{
					this.firstValue = default(TValue);
				}
				else if (this.length == 2)
				{
					this.firstValue = this.additionalValues[0];
					this.additionalValues[0] = default(TValue);
				}
				else
				{
					this.firstValue = this.additionalValues[0];
					int num = this.length - 1;
					this.additionalValues.EraseAtWithCapacity<TValue>(ref num, 0);
				}
			}
			else
			{
				int num2 = this.length - 1;
				this.additionalValues.EraseAtWithCapacity<TValue>(ref num2, index - 1);
			}
			this.length--;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00052A80 File Offset: 0x00050C80
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.additionalValues != null)
				{
					this.firstValue = this.additionalValues[0];
					if (this.additionalValues.Length == 1)
					{
						this.additionalValues = null;
					}
					else
					{
						Array.Copy(this.additionalValues, 1, this.additionalValues, 0, this.additionalValues.Length - 1);
						Array.Resize<TValue>(ref this.additionalValues, this.additionalValues.Length - 1);
					}
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				int num = this.length - 1;
				if (num == 1)
				{
					this.additionalValues = null;
				}
				else if (index == this.length - 1)
				{
					Array.Resize<TValue>(ref this.additionalValues, num - 1);
				}
				else
				{
					TValue[] array = new TValue[num - 1];
					if (index >= 2)
					{
						Array.Copy(this.additionalValues, 0, array, 0, index - 1);
					}
					Array.Copy(this.additionalValues, index + 1 - 1, array, index - 1, this.length - index - 1);
					this.additionalValues = array;
				}
			}
			this.length--;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00052BA0 File Offset: 0x00050DA0
		public void RemoveAtByMovingTailWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = this.length - 1;
			if (index == 0)
			{
				if (this.length > 1)
				{
					this.firstValue = this.additionalValues[num - 1];
					this.additionalValues[num - 1] = default(TValue);
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				ArrayHelpers.EraseAtByMovingTail<TValue>(this.additionalValues, ref num, index - 1);
			}
			this.length--;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00052C34 File Offset: 0x00050E34
		public bool RemoveByMovingTailWithCapacity(TValue value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return false;
			}
			this.RemoveAtByMovingTailWithCapacity(num);
			return true;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00052C58 File Offset: 0x00050E58
		public bool Contains(TValue value, IEqualityComparer<TValue> comparer)
		{
			for (int i = 0; i < this.length; i++)
			{
				if (comparer.Equals(this[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00052C8C File Offset: 0x00050E8C
		public void Merge(InlinedArray<TValue> other)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < other.length; i++)
			{
				TValue tvalue = other[i];
				if (!this.Contains(tvalue, @default))
				{
					this.Append(tvalue);
				}
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00052CCC File Offset: 0x00050ECC
		public IEnumerator<TValue> GetEnumerator()
		{
			return new InlinedArray<TValue>.Enumerator
			{
				array = this,
				index = -1
			};
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00052CFC File Offset: 0x00050EFC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000717 RID: 1815
		public int length;

		// Token: 0x04000718 RID: 1816
		public TValue firstValue;

		// Token: 0x04000719 RID: 1817
		public TValue[] additionalValues;

		// Token: 0x02000245 RID: 581
		private struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06001630 RID: 5680 RVA: 0x0006649A File Offset: 0x0006469A
			public bool MoveNext()
			{
				if (this.index >= this.array.length)
				{
					return false;
				}
				this.index++;
				return this.index < this.array.length;
			}

			// Token: 0x06001631 RID: 5681 RVA: 0x000664D2 File Offset: 0x000646D2
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001632 RID: 5682 RVA: 0x000664DB File Offset: 0x000646DB
			public TValue Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001633 RID: 5683 RVA: 0x000664EE File Offset: 0x000646EE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001634 RID: 5684 RVA: 0x000664FB File Offset: 0x000646FB
			public void Dispose()
			{
			}

			// Token: 0x04000C78 RID: 3192
			public InlinedArray<TValue> array;

			// Token: 0x04000C79 RID: 3193
			public int index;
		}
	}
}
