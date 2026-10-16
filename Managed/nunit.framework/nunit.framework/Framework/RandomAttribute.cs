using System;
using System.Collections;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200002B RID: 43
	public class RandomAttribute : DataAttribute, IParameterDataSource
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00004254 File Offset: 0x00002454
		public RandomAttribute(int count)
		{
			this._count = count;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004263 File Offset: 0x00002463
		public RandomAttribute(int min, int max, int count)
		{
			this._source = new RandomAttribute.IntDataSource(min, max, count);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004279 File Offset: 0x00002479
		[CLSCompliant(false)]
		public RandomAttribute(uint min, uint max, int count)
		{
			this._source = new RandomAttribute.UIntDataSource(min, max, count);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000428F File Offset: 0x0000248F
		public RandomAttribute(long min, long max, int count)
		{
			this._source = new RandomAttribute.LongDataSource(min, max, count);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000042A5 File Offset: 0x000024A5
		[CLSCompliant(false)]
		public RandomAttribute(ulong min, ulong max, int count)
		{
			this._source = new RandomAttribute.ULongDataSource(min, max, count);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000042BB File Offset: 0x000024BB
		public RandomAttribute(short min, short max, int count)
		{
			this._source = new RandomAttribute.ShortDataSource(min, max, count);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000042D1 File Offset: 0x000024D1
		[CLSCompliant(false)]
		public RandomAttribute(ushort min, ushort max, int count)
		{
			this._source = new RandomAttribute.UShortDataSource(min, max, count);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000042E7 File Offset: 0x000024E7
		public RandomAttribute(double min, double max, int count)
		{
			this._source = new RandomAttribute.DoubleDataSource(min, max, count);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000042FD File Offset: 0x000024FD
		public RandomAttribute(float min, float max, int count)
		{
			this._source = new RandomAttribute.FloatDataSource(min, max, count);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004313 File Offset: 0x00002513
		public RandomAttribute(byte min, byte max, int count)
		{
			this._source = new RandomAttribute.ByteDataSource(min, max, count);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004329 File Offset: 0x00002529
		[CLSCompliant(false)]
		public RandomAttribute(sbyte min, sbyte max, int count)
		{
			this._source = new RandomAttribute.SByteDataSource(min, max, count);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004340 File Offset: 0x00002540
		public IEnumerable GetData(IParameterInfo parameter)
		{
			Type parameterType = parameter.ParameterType;
			if (this._source == null)
			{
				if (parameterType == typeof(int))
				{
					this._source = new RandomAttribute.IntDataSource(this._count);
				}
				else if (parameterType == typeof(uint))
				{
					this._source = new RandomAttribute.UIntDataSource(this._count);
				}
				else if (parameterType == typeof(long))
				{
					this._source = new RandomAttribute.LongDataSource(this._count);
				}
				else if (parameterType == typeof(ulong))
				{
					this._source = new RandomAttribute.ULongDataSource(this._count);
				}
				else if (parameterType == typeof(short))
				{
					this._source = new RandomAttribute.ShortDataSource(this._count);
				}
				else if (parameterType == typeof(ushort))
				{
					this._source = new RandomAttribute.UShortDataSource(this._count);
				}
				else if (parameterType == typeof(double))
				{
					this._source = new RandomAttribute.DoubleDataSource(this._count);
				}
				else if (parameterType == typeof(float))
				{
					this._source = new RandomAttribute.FloatDataSource(this._count);
				}
				else if (parameterType == typeof(byte))
				{
					this._source = new RandomAttribute.ByteDataSource(this._count);
				}
				else if (parameterType == typeof(sbyte))
				{
					this._source = new RandomAttribute.SByteDataSource(this._count);
				}
				else if (parameterType == typeof(decimal))
				{
					this._source = new RandomAttribute.DecimalDataSource(this._count);
				}
				else if (parameterType.GetTypeInfo().IsEnum)
				{
					this._source = new RandomAttribute.EnumDataSource(this._count);
				}
				else
				{
					this._source = new RandomAttribute.IntDataSource(this._count);
				}
			}
			else if (this._source.DataType != parameterType && this.WeConvert(this._source.DataType, parameterType))
			{
				this._source = new RandomAttribute.RandomDataConverter(this._source);
			}
			return this._source.GetData(parameter);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000458C File Offset: 0x0000278C
		private bool WeConvert(Type sourceType, Type targetType)
		{
			if (targetType == typeof(short) || targetType == typeof(ushort) || targetType == typeof(byte) || targetType == typeof(sbyte))
			{
				return sourceType == typeof(int);
			}
			return targetType == typeof(decimal) && (sourceType == typeof(int) || sourceType == typeof(double));
		}

		// Token: 0x0400002D RID: 45
		private RandomAttribute.RandomDataSource _source;

		// Token: 0x0400002E RID: 46
		private int _count;

		// Token: 0x02000157 RID: 343
		private abstract class RandomDataSource : IParameterDataSource
		{
			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00018C3C File Offset: 0x00016E3C
			// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00018C44 File Offset: 0x00016E44
			public Type DataType { get; protected set; }

			// Token: 0x06000A1A RID: 2586
			public abstract IEnumerable GetData(IParameterInfo parameter);
		}

		// Token: 0x02000158 RID: 344
		private abstract class RandomDataSource<T> : RandomAttribute.RandomDataSource
		{
			// Token: 0x06000A1C RID: 2588 RVA: 0x00018C4D File Offset: 0x00016E4D
			protected RandomDataSource(int count)
			{
				this._count = count;
				this._inRange = false;
				base.DataType = typeof(T);
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x00018C73 File Offset: 0x00016E73
			protected RandomDataSource(T min, T max, int count)
			{
				this._min = min;
				this._max = max;
				this._count = count;
				this._inRange = true;
				base.DataType = typeof(T);
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x00018CA7 File Offset: 0x00016EA7
			public override IEnumerable GetData(IParameterInfo parameter)
			{
				this._randomizer = Randomizer.GetRandomizer(parameter.ParameterInfo);
				int num;
				for (int i = 0; i < this._count; i = num + 1)
				{
					yield return this._inRange ? this.GetNext(this._min, this._max) : this.GetNext();
					num = i;
				}
				yield break;
			}

			// Token: 0x06000A1F RID: 2591
			protected abstract T GetNext();

			// Token: 0x06000A20 RID: 2592
			protected abstract T GetNext(T min, T max);

			// Token: 0x04000226 RID: 550
			private T _min;

			// Token: 0x04000227 RID: 551
			private T _max;

			// Token: 0x04000228 RID: 552
			private int _count;

			// Token: 0x04000229 RID: 553
			private bool _inRange;

			// Token: 0x0400022A RID: 554
			protected Randomizer _randomizer;
		}

		// Token: 0x02000159 RID: 345
		private class RandomDataConverter : RandomAttribute.RandomDataSource
		{
			// Token: 0x06000A21 RID: 2593 RVA: 0x00018CBE File Offset: 0x00016EBE
			public RandomDataConverter(IParameterDataSource source)
			{
				this._source = source;
			}

			// Token: 0x06000A22 RID: 2594 RVA: 0x00018CCD File Offset: 0x00016ECD
			public override IEnumerable GetData(IParameterInfo parameter)
			{
				Type parmType = parameter.ParameterType;
				foreach (object obj in this._source.GetData(parameter))
				{
					if (obj is int)
					{
						int num = (int)obj;
						if (parmType == typeof(short))
						{
							yield return (short)num;
						}
						else if (parmType == typeof(ushort))
						{
							yield return (ushort)num;
						}
						else if (parmType == typeof(byte))
						{
							yield return (byte)num;
						}
						else if (parmType == typeof(sbyte))
						{
							yield return (sbyte)num;
						}
						else if (parmType == typeof(decimal))
						{
							yield return num;
						}
					}
					else if (obj is double)
					{
						double num2 = (double)obj;
						if (parmType == typeof(decimal))
						{
							yield return (decimal)num2;
						}
					}
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0400022B RID: 555
			private IParameterDataSource _source;
		}

		// Token: 0x0200015A RID: 346
		private class IntDataSource : RandomAttribute.RandomDataSource<int>
		{
			// Token: 0x06000A23 RID: 2595 RVA: 0x00018CE4 File Offset: 0x00016EE4
			public IntDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x00018CED File Offset: 0x00016EED
			public IntDataSource(int min, int max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x00018CF8 File Offset: 0x00016EF8
			protected override int GetNext()
			{
				return this._randomizer.Next();
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x00018D05 File Offset: 0x00016F05
			protected override int GetNext(int min, int max)
			{
				return this._randomizer.Next(min, max);
			}
		}

		// Token: 0x0200015B RID: 347
		private class UIntDataSource : RandomAttribute.RandomDataSource<uint>
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x00018D14 File Offset: 0x00016F14
			public UIntDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x00018D1D File Offset: 0x00016F1D
			public UIntDataSource(uint min, uint max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00018D28 File Offset: 0x00016F28
			protected override uint GetNext()
			{
				return this._randomizer.NextUInt();
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x00018D35 File Offset: 0x00016F35
			protected override uint GetNext(uint min, uint max)
			{
				return this._randomizer.NextUInt(min, max);
			}
		}

		// Token: 0x0200015C RID: 348
		private class LongDataSource : RandomAttribute.RandomDataSource<long>
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x00018D44 File Offset: 0x00016F44
			public LongDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x00018D4D File Offset: 0x00016F4D
			public LongDataSource(long min, long max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x00018D58 File Offset: 0x00016F58
			protected override long GetNext()
			{
				return this._randomizer.NextLong();
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x00018D65 File Offset: 0x00016F65
			protected override long GetNext(long min, long max)
			{
				return this._randomizer.NextLong(min, max);
			}
		}

		// Token: 0x0200015D RID: 349
		private class ULongDataSource : RandomAttribute.RandomDataSource<ulong>
		{
			// Token: 0x06000A2F RID: 2607 RVA: 0x00018D74 File Offset: 0x00016F74
			public ULongDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x00018D7D File Offset: 0x00016F7D
			public ULongDataSource(ulong min, ulong max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x00018D88 File Offset: 0x00016F88
			protected override ulong GetNext()
			{
				return this._randomizer.NextULong();
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x00018D95 File Offset: 0x00016F95
			protected override ulong GetNext(ulong min, ulong max)
			{
				return this._randomizer.NextULong(min, max);
			}
		}

		// Token: 0x0200015E RID: 350
		private class ShortDataSource : RandomAttribute.RandomDataSource<short>
		{
			// Token: 0x06000A33 RID: 2611 RVA: 0x00018DA4 File Offset: 0x00016FA4
			public ShortDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x00018DAD File Offset: 0x00016FAD
			public ShortDataSource(short min, short max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x00018DB8 File Offset: 0x00016FB8
			protected override short GetNext()
			{
				return this._randomizer.NextShort();
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x00018DC5 File Offset: 0x00016FC5
			protected override short GetNext(short min, short max)
			{
				return this._randomizer.NextShort(min, max);
			}
		}

		// Token: 0x0200015F RID: 351
		private class UShortDataSource : RandomAttribute.RandomDataSource<ushort>
		{
			// Token: 0x06000A37 RID: 2615 RVA: 0x00018DD4 File Offset: 0x00016FD4
			public UShortDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x00018DDD File Offset: 0x00016FDD
			public UShortDataSource(ushort min, ushort max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x00018DE8 File Offset: 0x00016FE8
			protected override ushort GetNext()
			{
				return this._randomizer.NextUShort();
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x00018DF5 File Offset: 0x00016FF5
			protected override ushort GetNext(ushort min, ushort max)
			{
				return this._randomizer.NextUShort(min, max);
			}
		}

		// Token: 0x02000160 RID: 352
		private class DoubleDataSource : RandomAttribute.RandomDataSource<double>
		{
			// Token: 0x06000A3B RID: 2619 RVA: 0x00018E04 File Offset: 0x00017004
			public DoubleDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A3C RID: 2620 RVA: 0x00018E0D File Offset: 0x0001700D
			public DoubleDataSource(double min, double max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A3D RID: 2621 RVA: 0x00018E18 File Offset: 0x00017018
			protected override double GetNext()
			{
				return this._randomizer.NextDouble();
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x00018E25 File Offset: 0x00017025
			protected override double GetNext(double min, double max)
			{
				return this._randomizer.NextDouble(min, max);
			}
		}

		// Token: 0x02000161 RID: 353
		private class FloatDataSource : RandomAttribute.RandomDataSource<float>
		{
			// Token: 0x06000A3F RID: 2623 RVA: 0x00018E34 File Offset: 0x00017034
			public FloatDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A40 RID: 2624 RVA: 0x00018E3D File Offset: 0x0001703D
			public FloatDataSource(float min, float max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A41 RID: 2625 RVA: 0x00018E48 File Offset: 0x00017048
			protected override float GetNext()
			{
				return this._randomizer.NextFloat();
			}

			// Token: 0x06000A42 RID: 2626 RVA: 0x00018E55 File Offset: 0x00017055
			protected override float GetNext(float min, float max)
			{
				return this._randomizer.NextFloat(min, max);
			}
		}

		// Token: 0x02000162 RID: 354
		private class ByteDataSource : RandomAttribute.RandomDataSource<byte>
		{
			// Token: 0x06000A43 RID: 2627 RVA: 0x00018E64 File Offset: 0x00017064
			public ByteDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x00018E6D File Offset: 0x0001706D
			public ByteDataSource(byte min, byte max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A45 RID: 2629 RVA: 0x00018E78 File Offset: 0x00017078
			protected override byte GetNext()
			{
				return this._randomizer.NextByte();
			}

			// Token: 0x06000A46 RID: 2630 RVA: 0x00018E85 File Offset: 0x00017085
			protected override byte GetNext(byte min, byte max)
			{
				return this._randomizer.NextByte(min, max);
			}
		}

		// Token: 0x02000163 RID: 355
		private class SByteDataSource : RandomAttribute.RandomDataSource<sbyte>
		{
			// Token: 0x06000A47 RID: 2631 RVA: 0x00018E94 File Offset: 0x00017094
			public SByteDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A48 RID: 2632 RVA: 0x00018E9D File Offset: 0x0001709D
			public SByteDataSource(sbyte min, sbyte max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A49 RID: 2633 RVA: 0x00018EA8 File Offset: 0x000170A8
			protected override sbyte GetNext()
			{
				return this._randomizer.NextSByte();
			}

			// Token: 0x06000A4A RID: 2634 RVA: 0x00018EB5 File Offset: 0x000170B5
			protected override sbyte GetNext(sbyte min, sbyte max)
			{
				return this._randomizer.NextSByte(min, max);
			}
		}

		// Token: 0x02000164 RID: 356
		private class EnumDataSource : RandomAttribute.RandomDataSource
		{
			// Token: 0x06000A4B RID: 2635 RVA: 0x00018EC4 File Offset: 0x000170C4
			public EnumDataSource(int count)
			{
				this._count = count;
				base.DataType = typeof(Enum);
			}

			// Token: 0x06000A4C RID: 2636 RVA: 0x00018EE3 File Offset: 0x000170E3
			public override IEnumerable GetData(IParameterInfo parameter)
			{
				Guard.ArgumentValid(parameter.ParameterType.GetTypeInfo().IsEnum, "EnumDataSource requires an enum parameter", "parameter");
				Randomizer randomizer = Randomizer.GetRandomizer(parameter.ParameterInfo);
				base.DataType = parameter.ParameterType;
				int num;
				for (int i = 0; i < this._count; i = num + 1)
				{
					yield return randomizer.NextEnum(parameter.ParameterType);
					num = i;
				}
				yield break;
			}

			// Token: 0x0400022C RID: 556
			private int _count;
		}

		// Token: 0x02000165 RID: 357
		private class DecimalDataSource : RandomAttribute.RandomDataSource<decimal>
		{
			// Token: 0x06000A4D RID: 2637 RVA: 0x00018EFA File Offset: 0x000170FA
			public DecimalDataSource(int count)
				: base(count)
			{
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x00018F03 File Offset: 0x00017103
			public DecimalDataSource(decimal min, decimal max, int count)
				: base(min, max, count)
			{
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x00018F0E File Offset: 0x0001710E
			protected override decimal GetNext()
			{
				return this._randomizer.NextDecimal();
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x00018F1B File Offset: 0x0001711B
			protected override decimal GetNext(decimal min, decimal max)
			{
				return this._randomizer.NextDecimal(min, max);
			}
		}
	}
}
