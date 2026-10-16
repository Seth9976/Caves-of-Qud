using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	/// <summary>Contains interlocked exchange mechanisms for immutable collections.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableInterlocked
	{
		/// <summary>Mutates a value in-place with optimistic locking transaction semantics             via a specified transformation function.             The transformation is retried as many times as necessary to win the optimistic locking race.</summary>
		/// <param name="location">The variable or field to be changed, which may be accessed by multiple threads.</param>
		/// <param name="transformer">A function that mutates the value. This function should be side-effect free,              as it may run multiple times when races occur with other threads.</param>
		/// <typeparam name="T">The type of data.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the location's value is changed by applying the result of the <paramref name="transformer" /> function; <see langword="false" /> if the location's value remained the same because the last invocation of <paramref name="transformer" /> returned the existing value.</returns>
		// Token: 0x060001FB RID: 507 RVA: 0x00006590 File Offset: 0x00004790
		public static bool Update<[Nullable(2)] T>(ref T location, Func<T, T> transformer) where T : class
		{
			Requires.NotNull<Func<T, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer.Invoke(t);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Mutates a value in-place with optimistic locking transaction semantics             via a specified transformation function.             The transformation is retried as many times as necessary to win the optimistic locking race.</summary>
		/// <param name="location">The variable or field to be changed, which may be accessed by multiple threads.</param>
		/// <param name="transformer">A function that mutates the value. This function should be side-effect free,              as it may run multiple times when races occur with other threads.</param>
		/// <param name="transformerArgument">The argument to pass to <paramref name="transformer" />.</param>
		/// <typeparam name="T">The type of data.</typeparam>
		/// <typeparam name="TArg">The type of argument passed to the <paramref name="transformer" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the location's value is changed by applying the result of the <paramref name="transformer" /> function; <see langword="false" /> if the location's value remained the same because the last invocation of <paramref name="transformer" /> returned the existing value.</returns>
		// Token: 0x060001FC RID: 508 RVA: 0x000065E8 File Offset: 0x000047E8
		public static bool Update<[Nullable(2)] T, [Nullable(2)] TArg>(ref T location, Func<T, TArg, T> transformer, TArg transformerArgument) where T : class
		{
			Requires.NotNull<Func<T, TArg, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer.Invoke(t, transformerArgument);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Mutates an immutable array in-place with optimistic locking transaction semantics via a specified transformation function.
		///  The transformation is retried as many times as necessary to win the optimistic locking race.</summary>
		/// <param name="location">The immutable array to be changed.</param>
		/// <param name="transformer">A function that produces the new array from the old. This function should be side-effect free, as it may run multiple times when races occur with other threads.</param>
		/// <typeparam name="T">The type of data in the immutable array.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the location's value is changed by applying the result of the <paramref name="transformer" /> function; <see langword="false" /> if the location's value remained the same because the last invocation of <paramref name="transformer" /> returned the existing value.</returns>
		// Token: 0x060001FD RID: 509 RVA: 0x00006640 File Offset: 0x00004840
		[NullableContext(2)]
		public static bool Update<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 1, 0, 1, 0, 1 })] Func<ImmutableArray<T>, ImmutableArray<T>> transformer)
		{
			Requires.NotNull<Func<ImmutableArray<T>, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(ref location.array);
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer.Invoke(new ImmutableArray<T>(array));
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(ref location.array, immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Mutates an immutable array in-place with optimistic locking transaction semantics via a specified transformation function.
		///  The transformation is retried as many times as necessary to win the optimistic locking race.</summary>
		/// <param name="location">The immutable array to be changed.</param>
		/// <param name="transformer">A function that produces the new array from the old. This function should be side-effect free, as it may run multiple times when races occur with other threads.</param>
		/// <param name="transformerArgument">The argument to pass to <paramref name="transformer" />.</param>
		/// <typeparam name="T">The type of data in the immutable array.</typeparam>
		/// <typeparam name="TArg">The type of argument passed to the <paramref name="transformer" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the location's value is changed by applying the result of the <paramref name="transformer" /> function; <see langword="false" /> if the location's value remained the same because the last invocation of <paramref name="transformer" /> returned the existing value.</returns>
		// Token: 0x060001FE RID: 510 RVA: 0x0000669C File Offset: 0x0000489C
		[NullableContext(2)]
		public static bool Update<T, TArg>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 1, 0, 1, 1, 0, 1 })] Func<ImmutableArray<T>, TArg, ImmutableArray<T>> transformer, [Nullable(1)] TArg transformerArgument)
		{
			Requires.NotNull<Func<ImmutableArray<T>, TArg, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(ref location.array);
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer.Invoke(new ImmutableArray<T>(array), transformerArgument);
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(ref location.array, immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Sets an array to the specified array and returns a reference to the original array, as an atomic operation.</summary>
		/// <param name="location">The array to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location" /> parameter is set.</param>
		/// <typeparam name="T">The type of element stored by the array.</typeparam>
		/// <returns>The original value of <paramref name="location" />.</returns>
		// Token: 0x060001FF RID: 511 RVA: 0x000066F7 File Offset: 0x000048F7
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> InterlockedExchange<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value)
		{
			return new ImmutableArray<T>(Interlocked.Exchange<T[]>(ref location.array, value.array));
		}

		/// <summary>Compares two immutable arrays for equality and, if they are equal, replaces one of the arrays.</summary>
		/// <param name="location">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location" />.</param>
		/// <typeparam name="T">The type of element stored by the array.</typeparam>
		/// <returns>The original value in <paramref name="location" />.</returns>
		// Token: 0x06000200 RID: 512 RVA: 0x0000670F File Offset: 0x0000490F
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> InterlockedCompareExchange<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> comparand)
		{
			return new ImmutableArray<T>(Interlocked.CompareExchange<T[]>(ref location.array, value.array, comparand.array));
		}

		/// <summary>Sets an array to the specified array if the array has not been initialized.</summary>
		/// <param name="location">The array to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location" /> parameter is set, if it's not initialized.</param>
		/// <typeparam name="T">The type of element stored by the array.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the array was assigned the specified value;  otherwise, <see langword="false" />.</returns>
		// Token: 0x06000201 RID: 513 RVA: 0x00006730 File Offset: 0x00004930
		[NullableContext(2)]
		public static bool InterlockedInitialize<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value)
		{
			return ImmutableInterlocked.InterlockedCompareExchange<T>(ref location, value, default(ImmutableArray<T>)).IsDefault;
		}

		/// <summary>Gets the value for the specified key from the dictionary, or if the key was not found, adds a new value to the dictionary.</summary>
		/// <param name="location">The variable or field to update if the specified is not in the dictionary.</param>
		/// <param name="key">The key for the value to retrieve or add.</param>
		/// <param name="valueFactory">The function to execute to obtain the value to insert into the dictionary if the key is not found.</param>
		/// <param name="factoryArgument">The argument to pass to the value factory.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <typeparam name="TArg">The type of the argument supplied to the value factory.</typeparam>
		/// <returns>The value at the specified key or <paramref name="valueFactory" /> if the key was not present.</returns>
		// Token: 0x06000202 RID: 514 RVA: 0x00006758 File Offset: 0x00004958
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue, [Nullable(2)] TArg>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory.Invoke(key, factoryArgument);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		/// <summary>Gets the value for the specified key from the dictionary, or if the key was not found, adds a new value to the dictionary.</summary>
		/// <param name="location">The variable or field to atomically update if the specified  is not in the dictionary.</param>
		/// <param name="key">The key for the value to retrieve or add.</param>
		/// <param name="valueFactory">The function to execute to obtain the value to insert into the dictionary if the key is not found. This delegate will not be invoked more than once.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <returns>The value at the specified key or <paramref name="valueFactory" /> if the key was not present.</returns>
		// Token: 0x06000203 RID: 515 RVA: 0x000067A0 File Offset: 0x000049A0
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory.Invoke(key);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		/// <summary>Gets the value for the specified key from the dictionary, or if the key was not found, adds a new value to the dictionary.</summary>
		/// <param name="location">The variable or field to atomically update if the specified key is not in the dictionary.</param>
		/// <param name="key">The key for the value to get or add.</param>
		/// <param name="value">The value to add to the dictionary the key is not found.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <returns>The value at the specified key or <paramref name="valueFactory" /> if the key was not present.</returns>
		// Token: 0x06000204 RID: 516 RVA: 0x000067E8 File Offset: 0x000049E8
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return value;
				}
			}
			return tvalue;
		}

		/// <summary>Obtains the value from a dictionary after having added it or updated an existing entry.</summary>
		/// <param name="location">The variable or field to atomically update if the specified  is not in the dictionary.</param>
		/// <param name="key">The key for the value to add or update.</param>
		/// <param name="addValueFactory">The function that receives the key and returns a new value to add to the dictionary when no value previously exists.</param>
		/// <param name="updateValueFactory">The function that receives the key and prior value and returns the new value with which to update the dictionary.</param>
		/// <typeparam name="TKey">The type of key stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of value stored by the dictionary.</typeparam>
		/// <returns>The added or updated value.</returns>
		// Token: 0x06000205 RID: 517 RVA: 0x00006834 File Offset: 0x00004A34
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(addValueFactory, "addValueFactory");
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue2;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
				}
				else
				{
					tvalue2 = addValueFactory.Invoke(key);
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
			}
			while (!flag);
			return tvalue2;
		}

		/// <summary>Obtains the value from a dictionary after having added it or updated an existing entry.</summary>
		/// <param name="location">The variable or field to atomically update if the specified  is not in the dictionary.</param>
		/// <param name="key">The key for the value to add or update.</param>
		/// <param name="addValue">The value to use if no previous value exists.</param>
		/// <param name="updateValueFactory">The function that receives the key and prior value and returns the new value with which to update the dictionary.</param>
		/// <typeparam name="TKey">The type of key stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of value stored by the dictionary.</typeparam>
		/// <returns>The added or updated value.</returns>
		// Token: 0x06000206 RID: 518 RVA: 0x000068AC File Offset: 0x00004AAC
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue2;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
				}
				else
				{
					tvalue2 = addValue;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
			}
			while (!flag);
			return tvalue2;
		}

		/// <summary>Adds the specified key and value to the dictionary if the key is not in the dictionary.</summary>
		/// <param name="location">The dictionary to update with the specified key and value.</param>
		/// <param name="key">The key to add, if is not already defined in the dictionary.</param>
		/// <param name="value">The value to add.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the key is not in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000207 RID: 519 RVA: 0x00006910 File Offset: 0x00004B10
		public static bool TryAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.ContainsKey(key))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Sets the specified key to the specified value if the specified key already is set to a specific value.</summary>
		/// <param name="location">The dictionary to update.</param>
		/// <param name="key">The key to update.</param>
		/// <param name="newValue">The new value to set.</param>
		/// <param name="comparisonValue">The current value for <paramref name="key" /> in order for the update to succeed.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="key" /> and <paramref name="comparisonValue" /> are present in the dictionary and comparison was updated to <paramref name="newValue" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000208 RID: 520 RVA: 0x00006958 File Offset: 0x00004B58
		public static bool TryUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (!immutableDictionary.TryGetValue(key, out tvalue) || !@default.Equals(tvalue, comparisonValue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, newValue);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Removes the element with the specified key, if the key exists.</summary>
		/// <param name="location">The dictionary to update.</param>
		/// <param name="key">The key to remove.</param>
		/// <param name="value">Receives the value of the removed item, if the dictionary is not empty.</param>
		/// <typeparam name="TKey">The type of the keys contained in the collection.</typeparam>
		/// <typeparam name="TValue">The type of the values contained in the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the key was found and removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000209 RID: 521 RVA: 0x000069B8 File Offset: 0x00004BB8
		public static bool TryRemove<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (!immutableDictionary.TryGetValue(key, out value))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Remove(key);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Removes an element from the top of the stack, if there is an element to remove.</summary>
		/// <param name="location">The stack to update.</param>
		/// <param name="value">Receives the value removed from the stack, if the stack is not empty.</param>
		/// <typeparam name="T">The type of items in the stack.</typeparam>
		/// <returns>
		///   <see langword="true" /> if an element is removed from the stack; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600020A RID: 522 RVA: 0x00006A00 File Offset: 0x00004C00
		public static bool TryPop<[Nullable(2)] T>(ref ImmutableStack<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				if (immutableStack.IsEmpty)
				{
					break;
				}
				ImmutableStack<T> immutableStack2 = immutableStack.Pop(out value);
				ImmutableStack<T> immutableStack3 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack);
				bool flag = immutableStack == immutableStack3;
				immutableStack = immutableStack3;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		/// <summary>Pushes a new element onto the stack.</summary>
		/// <param name="location">The stack to update.</param>
		/// <param name="value">The value to push on the stack.</param>
		/// <typeparam name="T">The type of items in the stack.</typeparam>
		// Token: 0x0600020B RID: 523 RVA: 0x00006A4C File Offset: 0x00004C4C
		public static void Push<[Nullable(2)] T>(ref ImmutableStack<T> location, T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				ImmutableStack<T> immutableStack2 = immutableStack.Push(value);
				ImmutableStack<T> immutableStack3 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack);
				flag = immutableStack == immutableStack3;
				immutableStack = immutableStack3;
			}
			while (!flag);
		}

		/// <summary>Atomically removes and returns the specified element at the head of the queue, if the queue is not empty.</summary>
		/// <param name="location">The variable or field to atomically update.</param>
		/// <param name="value">Set to the value from the head of the queue, if the queue not empty.</param>
		/// <typeparam name="T">The type of items in the queue.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the queue is not empty and the head element is removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600020C RID: 524 RVA: 0x00006A88 File Offset: 0x00004C88
		public static bool TryDequeue<[Nullable(2)] T>(ref ImmutableQueue<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				if (immutableQueue.IsEmpty)
				{
					break;
				}
				ImmutableQueue<T> immutableQueue2 = immutableQueue.Dequeue(out value);
				ImmutableQueue<T> immutableQueue3 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue);
				bool flag = immutableQueue == immutableQueue3;
				immutableQueue = immutableQueue3;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		/// <summary>Atomically enqueues an element to the end of a queue.</summary>
		/// <param name="location">The variable or field to atomically update.</param>
		/// <param name="value">The value to enqueue.</param>
		/// <typeparam name="T">The type of items contained in the collection.</typeparam>
		// Token: 0x0600020D RID: 525 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static void Enqueue<[Nullable(2)] T>(ref ImmutableQueue<T> location, T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				ImmutableQueue<T> immutableQueue2 = immutableQueue.Enqueue(value);
				ImmutableQueue<T> immutableQueue3 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue);
				flag = immutableQueue == immutableQueue3;
				immutableQueue = immutableQueue3;
			}
			while (!flag);
		}
	}
}
