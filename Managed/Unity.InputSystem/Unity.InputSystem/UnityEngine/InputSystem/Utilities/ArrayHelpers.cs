using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012C RID: 300
	internal static class ArrayHelpers
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00050F3F File Offset: 0x0004F13F
		public static int LengthSafe<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00050F49 File Offset: 0x0004F149
		public static void Clear<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00050F59 File Offset: 0x0004F159
		public static void Clear<TValue>(this TValue[] array, int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00050F67 File Offset: 0x0004F167
		public static void Clear<TValue>(this TValue[] array, ref int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
			count = 0;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00050F79 File Offset: 0x0004F179
		public static void EnsureCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (capacity == 0)
			{
				return;
			}
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			if (array.Length - count >= capacity)
			{
				return;
			}
			ArrayHelpers.DuplicateWithCapacity<TValue>(ref array, count, capacity, capacityIncrement);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00050FA8 File Offset: 0x0004F1A8
		public static void DuplicateWithCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			TValue[] array2 = new TValue[count + Math.Max(capacity, capacityIncrement)];
			Array.Copy(array, array2, count);
			array = array2;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00050FE4 File Offset: 0x0004F1E4
		public static bool Contains<TValue>(TValue[] array, TValue value)
		{
			if (array == null)
			{
				return false;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < array.Length; i++)
			{
				if (@default.Equals(array[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0005101D File Offset: 0x0004F21D
		public static bool ContainsReference<TValue>(this TValue[] array, TValue value) where TValue : class
		{
			return array != null && array.ContainsReference(array.Length, value);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005102E File Offset: 0x0004F22E
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, count) != -1;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005103E File Offset: 0x0004F23E
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int startIndex, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, startIndex, count) != -1;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00051050 File Offset: 0x0004F250
		public static bool HaveDuplicateReferences<TFirst>(this TFirst[] first, int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				TFirst tfirst = first[i];
				for (int j = i + 1; j < count - i; j++)
				{
					if (tfirst == first[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00051098 File Offset: 0x0004F298
		public static bool HaveEqualElements<TValue>(TValue[] first, TValue[] second, int count = 2147483647)
		{
			if (first == null || second == null)
			{
				return second == first;
			}
			int num = Math.Min(count, first.Length);
			int num2 = Math.Min(count, second.Length);
			if (num != num2)
			{
				return false;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < num; i++)
			{
				if (!@default.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000510F8 File Offset: 0x0004F2F8
		public static int IndexOf<TValue>(TValue[] array, TValue value, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (@default.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0005113C File Offset: 0x0004F33C
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate)
		{
			if (array == null)
			{
				return -1;
			}
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00051170 File Offset: 0x0004F370
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			int num = startIndex + ((count < 0) ? (array.Length - startIndex) : count);
			for (int i = startIndex; i < num; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000511AF File Offset: 0x0004F3AF
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int count = -1) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, 0, count);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000511BC File Offset: 0x0004F3BC
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int startIndex, int count) where TFirst : TSecond where TSecond : class
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00051200 File Offset: 0x0004F400
		public static int IndexOfValue<TValue>(this TValue[] array, TValue value, int startIndex = 0, int count = -1) where TValue : struct, IEquatable<TValue>
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (value.Equals(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00051244 File Offset: 0x0004F444
		public static void Resize<TValue>(ref NativeArray<TValue> array, int newSize, Allocator allocator) where TValue : struct
		{
			int length = array.Length;
			if (length == newSize)
			{
				return;
			}
			if (newSize == 0)
			{
				if (array.IsCreated)
				{
					array.Dispose();
				}
				array = default(NativeArray<TValue>);
				return;
			}
			NativeArray<TValue> nativeArray = new NativeArray<TValue>(newSize, allocator, NativeArrayOptions.ClearMemory);
			if (length != 0)
			{
				UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)(UnsafeUtility.SizeOf<TValue>() * ((newSize < length) ? newSize : length)));
				array.Dispose();
			}
			array = nativeArray;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000512B8 File Offset: 0x0004F4B8
		public static int Append<TValue>(ref TValue[] array, TValue value)
		{
			if (array == null)
			{
				array = new TValue[1];
				array[0] = value;
				return 0;
			}
			int num = array.Length;
			Array.Resize<TValue>(ref array, num + 1);
			array[num] = value;
			return num;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000512F4 File Offset: 0x0004F4F4
		public static int Append<TValue>(ref TValue[] array, IEnumerable<TValue> values)
		{
			if (array == null)
			{
				array = values.ToArray<TValue>();
				return 0;
			}
			int num = array.Length;
			int num2 = values.Count<TValue>();
			Array.Resize<TValue>(ref array, num + num2);
			int num3 = num;
			foreach (TValue tvalue in values)
			{
				array[num3++] = tvalue;
			}
			return num;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0005136C File Offset: 0x0004F56C
		public static int AppendToImmutable<TValue>(ref TValue[] array, TValue[] values)
		{
			if (array == null)
			{
				array = values;
				return 0;
			}
			if (values != null && values.Length != 0)
			{
				int num = array.Length;
				int num2 = values.Length;
				Array.Resize<TValue>(ref array, num + num2);
				Array.Copy(values, 0, array, num, num2);
				return num;
			}
			return array.Length;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000513AC File Offset: 0x0004F5AC
		public static int AppendWithCapacity<TValue>(ref TValue[] array, ref int count, TValue value, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[capacityIncrement];
				array[0] = value;
				count++;
				return 0;
			}
			int num = array.Length;
			if (num == count)
			{
				num += capacityIncrement;
				Array.Resize<TValue>(ref array, num);
			}
			int num2 = count;
			array[num2] = value;
			count++;
			return num2;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00051400 File Offset: 0x0004F600
		public static int AppendListWithCapacity<TValue, TValues>(ref TValue[] array, ref int length, TValues values, int capacityIncrement = 10) where TValues : IReadOnlyList<TValue>
		{
			int count = values.Count;
			if (array == null)
			{
				int num = Math.Max(count, capacityIncrement);
				array = new TValue[num];
				for (int i = 0; i < count; i++)
				{
					array[i] = values[i];
				}
				length += count;
				return 0;
			}
			int num2 = array.Length;
			if (num2 < length + count)
			{
				num2 += Math.Max(length + count, capacityIncrement);
				Array.Resize<TValue>(ref array, num2);
			}
			int num3 = length;
			for (int j = 0; j < count; j++)
			{
				array[num3 + j] = values[j];
			}
			length += count;
			return num3;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000514B4 File Offset: 0x0004F6B4
		public static int AppendWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, TValue value, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length == count)
			{
				ArrayHelpers.GrowBy<TValue>(ref array, (capacityIncrement > 1) ? capacityIncrement : 1, allocator);
			}
			int num = count;
			array[num] = value;
			count++;
			return num;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000514F0 File Offset: 0x0004F6F0
		public static void InsertAt<TValue>(ref TValue[] array, int index, TValue value)
		{
			if (array != null)
			{
				int num = array.Length;
				Array.Resize<TValue>(ref array, num + 1);
				if (index != num)
				{
					Array.Copy(array, index, array, index + 1, num - index);
				}
				array[index] = value;
				return;
			}
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			array = new TValue[1];
			array[0] = value;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0005154C File Offset: 0x0004F74C
		public static void InsertAtWithCapacity<TValue>(ref TValue[] array, ref int count, int index, TValue value, int capacityIncrement = 10)
		{
			ArrayHelpers.EnsureCapacity<TValue>(ref array, count, count + 1, capacityIncrement);
			if (index != count)
			{
				Array.Copy(array, index, array, index + 1, count - index);
			}
			array[index] = value;
			count++;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00051584 File Offset: 0x0004F784
		public static void PutAtIfNotSet<TValue>(ref TValue[] array, int index, Func<TValue> valueFn)
		{
			if (array.LengthSafe<TValue>() < index + 1)
			{
				Array.Resize<TValue>(ref array, index + 1);
			}
			if (EqualityComparer<TValue>.Default.Equals(array[index], default(TValue)))
			{
				array[index] = valueFn();
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000515D4 File Offset: 0x0004F7D4
		public static int GrowBy<TValue>(ref TValue[] array, int count)
		{
			if (array == null)
			{
				array = new TValue[count];
				return 0;
			}
			int num = array.Length;
			Array.Resize<TValue>(ref array, num + count);
			return num;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00051600 File Offset: 0x0004F800
		public static int GrowBy<TValue>(ref NativeArray<TValue> array, int count, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			int length = array.Length;
			if (length == 0)
			{
				array = new NativeArray<TValue>(count, allocator, NativeArrayOptions.ClearMemory);
				return 0;
			}
			NativeArray<TValue> nativeArray = new NativeArray<TValue>(length + count, allocator, NativeArrayOptions.ClearMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)length * (long)UnsafeUtility.SizeOf<TValue>());
			array.Dispose();
			array = nativeArray;
			return length;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00051660 File Offset: 0x0004F860
		public static int GrowWithCapacity<TValue>(ref TValue[] array, ref int count, int growBy, int capacityIncrement = 10)
		{
			if (((array != null) ? array.Length : 0) < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0005168A File Offset: 0x0004F88A
		public static int GrowWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, int growBy, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement, allocator);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000516B4 File Offset: 0x0004F8B4
		public static TValue[] Join<TValue>(TValue value, params TValue[] values)
		{
			int num = 0;
			if (value != null)
			{
				num++;
			}
			if (values != null)
			{
				num += values.Length;
			}
			if (num == 0)
			{
				return null;
			}
			TValue[] array = new TValue[num];
			int num2 = 0;
			if (value != null)
			{
				array[num2++] = value;
			}
			if (values != null)
			{
				Array.Copy(values, 0, array, num2, values.Length);
			}
			return array;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0005170C File Offset: 0x0004F90C
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second) where TValue : IEquatable<TValue>
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return first;
			}
			List<TValue> list = new List<TValue>();
			list.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!list.Exists((TValue x) => x.Equals(secondValue)))
				{
					list.Add(secondValue);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00051778 File Offset: 0x0004F978
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second, IEqualityComparer<TValue> comparer)
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return null;
			}
			List<TValue> list = new List<TValue>();
			list.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!list.Exists((TValue x) => comparer.Equals(secondValue)))
				{
					list.Add(secondValue);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public static void EraseAt<TValue>(ref TValue[] array, int index)
		{
			int num = array.Length;
			if (index == 0 && num == 1)
			{
				array = null;
				return;
			}
			if (index < num - 1)
			{
				Array.Copy(array, index + 1, array, index, num - index - 1);
			}
			Array.Resize<TValue>(ref array, num - 1);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00051838 File Offset: 0x0004FA38
		public static void EraseAtWithCapacity<TValue>(this TValue[] array, ref int count, int index)
		{
			if (index < count - 1)
			{
				Array.Copy(array, index + 1, array, index, count - index - 1);
			}
			array[count - 1] = default(TValue);
			count--;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00051878 File Offset: 0x0004FA78
		public unsafe static void EraseAtWithCapacity<TValue>(NativeArray<TValue> array, ref int count, int index) where TValue : struct
		{
			if (index < count - 1)
			{
				int num = UnsafeUtility.SizeOf<TValue>();
				byte* unsafePtr = (byte*)array.GetUnsafePtr<TValue>();
				UnsafeUtility.MemCpy((void*)(unsafePtr + num * index), (void*)(unsafePtr + num * (index + 1)), (long)((count - index - 1) * num));
			}
			count--;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000518BC File Offset: 0x0004FABC
		public static bool Erase<TValue>(ref TValue[] array, TValue value)
		{
			int num = ArrayHelpers.IndexOf<TValue>(array, value, 0, -1);
			if (num != -1)
			{
				ArrayHelpers.EraseAt<TValue>(ref array, num);
				return true;
			}
			return false;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000518E4 File Offset: 0x0004FAE4
		public static void EraseAtByMovingTail<TValue>(TValue[] array, ref int count, int index)
		{
			if (index != count - 1)
			{
				array[index] = array[count - 1];
			}
			if (count >= 1)
			{
				array[count - 1] = default(TValue);
			}
			count--;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00051928 File Offset: 0x0004FB28
		public static TValue[] Copy<TValue>(TValue[] array)
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TValue[] array2 = new TValue[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00051950 File Offset: 0x0004FB50
		public static TValue[] Clone<TValue>(TValue[] array) where TValue : ICloneable
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TValue[] array2 = new TValue[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (TValue)((object)array[i].Clone());
			}
			return array2;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0005199C File Offset: 0x0004FB9C
		public static TNew[] Select<TOld, TNew>(TOld[] array, Func<TOld, TNew> converter)
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TNew[] array2 = new TNew[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000519DC File Offset: 0x0004FBDC
		private static void Swap<TValue>(ref TValue first, ref TValue second)
		{
			TValue tvalue = first;
			first = second;
			second = tvalue;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00051A04 File Offset: 0x0004FC04
		public static void MoveSlice<TValue>(TValue[] array, int sourceIndex, int destinationIndex, int count)
		{
			if (count <= 0 || sourceIndex == destinationIndex)
			{
				return;
			}
			int num;
			if (destinationIndex > sourceIndex)
			{
				num = destinationIndex + count - sourceIndex;
			}
			else
			{
				num = sourceIndex + count - destinationIndex;
			}
			if (num == count * 2)
			{
				for (int i = 0; i < count; i++)
				{
					ArrayHelpers.Swap<TValue>(ref array[sourceIndex + i], ref array[destinationIndex + i]);
				}
				return;
			}
			int num2 = num - 1;
			int num3 = destinationIndex;
			for (int j = 0; j < num2; j++)
			{
				ArrayHelpers.Swap<TValue>(ref array[num3], ref array[sourceIndex]);
				if (destinationIndex > sourceIndex)
				{
					num3 -= count;
					if (num3 < sourceIndex)
					{
						num3 = destinationIndex + count - Math.Abs(sourceIndex - num3);
					}
				}
				else
				{
					num3 += count;
					if (num3 >= sourceIndex + count)
					{
						num3 = destinationIndex + (num3 - (sourceIndex + count));
					}
				}
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00051AAC File Offset: 0x0004FCAC
		public static void EraseSliceWithCapacity<TValue>(ref TValue[] array, ref int length, int index, int count)
		{
			if (count < length)
			{
				Array.Copy(array, index + count, array, index, length - index - count);
			}
			for (int i = 0; i < count; i++)
			{
				array[length - i - 1] = default(TValue);
			}
			length -= count;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00051AF9 File Offset: 0x0004FCF9
		public static void SwapElements<TValue>(this TValue[] array, int index1, int index2)
		{
			MemoryHelpers.Swap<TValue>(ref array[index1], ref array[index2]);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00051B10 File Offset: 0x0004FD10
		public static void SwapElements<TValue>(this NativeArray<TValue> array, int index1, int index2) where TValue : struct
		{
			TValue tvalue = array[index1];
			array[index1] = array[index2];
			array[index2] = tvalue;
		}
	}
}
