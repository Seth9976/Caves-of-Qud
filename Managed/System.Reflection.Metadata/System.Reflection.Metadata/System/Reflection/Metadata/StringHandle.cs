using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006D RID: 109
	public readonly struct StringHandle : IEquatable<StringHandle>
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x0000B13A File Offset: 0x0000933A
		private StringHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B143 File Offset: 0x00009343
		internal static StringHandle FromOffset(int heapOffset)
		{
			return new StringHandle((uint)(0 | heapOffset));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B14D File Offset: 0x0000934D
		internal static StringHandle FromVirtualIndex(StringHandle.VirtualIndex virtualIndex)
		{
			return new StringHandle((uint)((StringHandle.VirtualIndex)(-2147483648) | virtualIndex));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B15B File Offset: 0x0000935B
		internal static StringHandle FromWriterVirtualIndex(int virtualIndex)
		{
			return new StringHandle((uint)(int.MinValue | virtualIndex));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B169 File Offset: 0x00009369
		internal StringHandle WithWinRTPrefix()
		{
			return new StringHandle(2684354560U | this._value);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000B17C File Offset: 0x0000937C
		internal StringHandle WithDotTermination()
		{
			return new StringHandle(536870912U | this._value);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000B18F File Offset: 0x0000938F
		internal StringHandle SuffixRaw(int prefixByteLength)
		{
			return new StringHandle(0U | (this._value + (uint)prefixByteLength));
		}

		/// <param name="handle" />
		// Token: 0x06000478 RID: 1144 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public static implicit operator Handle(StringHandle handle)
		{
			return new Handle((byte)(((handle._value & 2147483648U) >> 24) | 120U | ((handle._value & 1610612736U) >> 29)), (int)(handle._value & 536870911U));
		}

		/// <param name="handle" />
		// Token: 0x06000479 RID: 1145 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public static explicit operator StringHandle(Handle handle)
		{
			if (((int)handle.VType & -132) != 120)
			{
				Throw.InvalidCast();
			}
			return new StringHandle((uint)(((int)(handle.VType & 128) << 24) | ((int)(handle.VType & 3) << 29) | handle.Offset));
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000B225 File Offset: 0x00009425
		internal uint RawValue
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000B22D File Offset: 0x0000942D
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000B23E File Offset: 0x0000943E
		public bool IsNil
		{
			get
			{
				return (this._value & 2684354559U) == 0U;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000B24F File Offset: 0x0000944F
		internal int GetHeapOffset()
		{
			return (int)(this._value & 536870911U);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000B25D File Offset: 0x0000945D
		internal StringHandle.VirtualIndex GetVirtualIndex()
		{
			return (StringHandle.VirtualIndex)(this._value & 536870911U);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000B26B File Offset: 0x0000946B
		internal int GetWriterVirtualIndex()
		{
			return (int)(this._value & 536870911U);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000B279 File Offset: 0x00009479
		internal StringKind StringKind
		{
			get
			{
				return (StringKind)(this._value >> 29);
			}
		}

		/// <param name="obj" />
		// Token: 0x06000481 RID: 1153 RVA: 0x0000B285 File Offset: 0x00009485
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is StringHandle && this.Equals((StringHandle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000482 RID: 1154 RVA: 0x0000B29D File Offset: 0x0000949D
		public bool Equals(StringHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000B2AD File Offset: 0x000094AD
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000484 RID: 1156 RVA: 0x0000B2B5 File Offset: 0x000094B5
		public static bool operator ==(StringHandle left, StringHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000485 RID: 1157 RVA: 0x0000B2BF File Offset: 0x000094BF
		public static bool operator !=(StringHandle left, StringHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000308 RID: 776
		private readonly uint _value;

		// Token: 0x02000199 RID: 409
		internal enum VirtualIndex
		{
			// Token: 0x040009DA RID: 2522
			System_Runtime_WindowsRuntime,
			// Token: 0x040009DB RID: 2523
			System_Runtime,
			// Token: 0x040009DC RID: 2524
			System_ObjectModel,
			// Token: 0x040009DD RID: 2525
			System_Runtime_WindowsRuntime_UI_Xaml,
			// Token: 0x040009DE RID: 2526
			System_Runtime_InteropServices_WindowsRuntime,
			// Token: 0x040009DF RID: 2527
			System_Numerics_Vectors,
			// Token: 0x040009E0 RID: 2528
			Dispose,
			// Token: 0x040009E1 RID: 2529
			AttributeTargets,
			// Token: 0x040009E2 RID: 2530
			AttributeUsageAttribute,
			// Token: 0x040009E3 RID: 2531
			Color,
			// Token: 0x040009E4 RID: 2532
			CornerRadius,
			// Token: 0x040009E5 RID: 2533
			DateTimeOffset,
			// Token: 0x040009E6 RID: 2534
			Duration,
			// Token: 0x040009E7 RID: 2535
			DurationType,
			// Token: 0x040009E8 RID: 2536
			EventHandler1,
			// Token: 0x040009E9 RID: 2537
			EventRegistrationToken,
			// Token: 0x040009EA RID: 2538
			Exception,
			// Token: 0x040009EB RID: 2539
			GeneratorPosition,
			// Token: 0x040009EC RID: 2540
			GridLength,
			// Token: 0x040009ED RID: 2541
			GridUnitType,
			// Token: 0x040009EE RID: 2542
			ICommand,
			// Token: 0x040009EF RID: 2543
			IDictionary2,
			// Token: 0x040009F0 RID: 2544
			IDisposable,
			// Token: 0x040009F1 RID: 2545
			IEnumerable,
			// Token: 0x040009F2 RID: 2546
			IEnumerable1,
			// Token: 0x040009F3 RID: 2547
			IList,
			// Token: 0x040009F4 RID: 2548
			IList1,
			// Token: 0x040009F5 RID: 2549
			INotifyCollectionChanged,
			// Token: 0x040009F6 RID: 2550
			INotifyPropertyChanged,
			// Token: 0x040009F7 RID: 2551
			IReadOnlyDictionary2,
			// Token: 0x040009F8 RID: 2552
			IReadOnlyList1,
			// Token: 0x040009F9 RID: 2553
			KeyTime,
			// Token: 0x040009FA RID: 2554
			KeyValuePair2,
			// Token: 0x040009FB RID: 2555
			Matrix,
			// Token: 0x040009FC RID: 2556
			Matrix3D,
			// Token: 0x040009FD RID: 2557
			Matrix3x2,
			// Token: 0x040009FE RID: 2558
			Matrix4x4,
			// Token: 0x040009FF RID: 2559
			NotifyCollectionChangedAction,
			// Token: 0x04000A00 RID: 2560
			NotifyCollectionChangedEventArgs,
			// Token: 0x04000A01 RID: 2561
			NotifyCollectionChangedEventHandler,
			// Token: 0x04000A02 RID: 2562
			Nullable1,
			// Token: 0x04000A03 RID: 2563
			Plane,
			// Token: 0x04000A04 RID: 2564
			Point,
			// Token: 0x04000A05 RID: 2565
			PropertyChangedEventArgs,
			// Token: 0x04000A06 RID: 2566
			PropertyChangedEventHandler,
			// Token: 0x04000A07 RID: 2567
			Quaternion,
			// Token: 0x04000A08 RID: 2568
			Rect,
			// Token: 0x04000A09 RID: 2569
			RepeatBehavior,
			// Token: 0x04000A0A RID: 2570
			RepeatBehaviorType,
			// Token: 0x04000A0B RID: 2571
			Size,
			// Token: 0x04000A0C RID: 2572
			System,
			// Token: 0x04000A0D RID: 2573
			System_Collections,
			// Token: 0x04000A0E RID: 2574
			System_Collections_Generic,
			// Token: 0x04000A0F RID: 2575
			System_Collections_Specialized,
			// Token: 0x04000A10 RID: 2576
			System_ComponentModel,
			// Token: 0x04000A11 RID: 2577
			System_Numerics,
			// Token: 0x04000A12 RID: 2578
			System_Windows_Input,
			// Token: 0x04000A13 RID: 2579
			Thickness,
			// Token: 0x04000A14 RID: 2580
			TimeSpan,
			// Token: 0x04000A15 RID: 2581
			Type,
			// Token: 0x04000A16 RID: 2582
			Uri,
			// Token: 0x04000A17 RID: 2583
			Vector2,
			// Token: 0x04000A18 RID: 2584
			Vector3,
			// Token: 0x04000A19 RID: 2585
			Vector4,
			// Token: 0x04000A1A RID: 2586
			Windows_Foundation,
			// Token: 0x04000A1B RID: 2587
			Windows_UI,
			// Token: 0x04000A1C RID: 2588
			Windows_UI_Xaml,
			// Token: 0x04000A1D RID: 2589
			Windows_UI_Xaml_Controls_Primitives,
			// Token: 0x04000A1E RID: 2590
			Windows_UI_Xaml_Media,
			// Token: 0x04000A1F RID: 2591
			Windows_UI_Xaml_Media_Animation,
			// Token: 0x04000A20 RID: 2592
			Windows_UI_Xaml_Media_Media3D,
			// Token: 0x04000A21 RID: 2593
			Count
		}
	}
}
