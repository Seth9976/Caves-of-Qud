using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BD RID: 189
	public class PairwiseStrategy : ICombiningStrategy
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x00010CD4 File Offset: 0x0000EED4
		public IEnumerable<ITestCaseData> GetTestCases(IEnumerable[] sources)
		{
			List<ITestCaseData> list = new List<ITestCaseData>();
			List<object>[] array = this.CreateValueSet(sources);
			int[] array2 = this.CreateDimensions(array);
			foreach (object obj in new PairwiseStrategy.PairwiseTestCaseGenerator().GetTestCases(array2))
			{
				PairwiseStrategy.TestCaseInfo testCaseInfo = (PairwiseStrategy.TestCaseInfo)obj;
				object[] array3 = new object[testCaseInfo.Features.Length];
				for (int i = 0; i < testCaseInfo.Features.Length; i++)
				{
					array3[i] = array[i][testCaseInfo.Features[i]];
				}
				TestCaseParameters testCaseParameters = new TestCaseParameters(array3);
				list.Add(testCaseParameters);
			}
			return list;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00010D98 File Offset: 0x0000EF98
		private List<object>[] CreateValueSet(IEnumerable[] sources)
		{
			List<object>[] array = new List<object>[sources.Length];
			for (int i = 0; i < array.Length; i++)
			{
				List<object> list = new List<object>();
				foreach (object obj in sources[i])
				{
					list.Add(obj);
				}
				array[i] = list;
			}
			return array;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00010E10 File Offset: 0x0000F010
		private int[] CreateDimensions(List<object>[] valueSet)
		{
			int[] array = new int[valueSet.Length];
			for (int i = 0; i < valueSet.Length; i++)
			{
				array[i] = valueSet[i].Count;
			}
			return array;
		}

		// Token: 0x0200018C RID: 396
		internal class FleaRand
		{
			// Token: 0x06000AC9 RID: 2761 RVA: 0x0001A314 File Offset: 0x00018514
			public FleaRand(uint seed)
			{
				this._b = seed;
				this._c = seed;
				this._d = seed;
				this._z = seed;
				this._m = new uint[256];
				this._r = new uint[256];
				for (int i = 0; i < this._m.Length; i++)
				{
					this._m[i] = seed;
				}
				for (int j = 0; j < 10; j++)
				{
					this.Batch();
				}
				this._q = 0U;
			}

			// Token: 0x06000ACA RID: 2762 RVA: 0x0001A399 File Offset: 0x00018599
			public uint Next()
			{
				if (this._q == 0U)
				{
					this.Batch();
					this._q = (uint)(this._r.Length - 1);
				}
				else
				{
					this._q -= 1U;
				}
				return this._r[(int)this._q];
			}

			// Token: 0x06000ACB RID: 2763 RVA: 0x0001A3D8 File Offset: 0x000185D8
			private void Batch()
			{
				uint num = this._b;
				uint c = this._c;
				uint num2 = this._z + 1U;
				this._z = num2;
				uint num3 = c + num2;
				uint num4 = this._d;
				for (int i = 0; i < this._r.Length; i++)
				{
					uint num5;
					checked
					{
						num5 = this._m[(int)((IntPtr)(unchecked((ulong)num % (ulong)((long)this._m.Length))))];
						this._m[(int)((IntPtr)(unchecked((ulong)num % (ulong)((long)this._m.Length))))] = num4;
					}
					num4 = (num3 << 19) + (num3 >> 13) + num;
					num3 = num ^ this._m[i];
					num = num5 + num4;
					this._r[i] = num3;
				}
				this._b = num;
				this._c = num3;
				this._d = num4;
			}

			// Token: 0x04000282 RID: 642
			private uint _b;

			// Token: 0x04000283 RID: 643
			private uint _c;

			// Token: 0x04000284 RID: 644
			private uint _d;

			// Token: 0x04000285 RID: 645
			private uint _z;

			// Token: 0x04000286 RID: 646
			private uint[] _m;

			// Token: 0x04000287 RID: 647
			private uint[] _r;

			// Token: 0x04000288 RID: 648
			private uint _q;
		}

		// Token: 0x0200018D RID: 397
		internal class FeatureInfo
		{
			// Token: 0x06000ACC RID: 2764 RVA: 0x0001A486 File Offset: 0x00018686
			public FeatureInfo(int dimension, int feature)
			{
				this.Dimension = dimension;
				this.Feature = feature;
			}

			// Token: 0x04000289 RID: 649
			public readonly int Dimension;

			// Token: 0x0400028A RID: 650
			public readonly int Feature;
		}

		// Token: 0x0200018E RID: 398
		internal class FeatureTuple
		{
			// Token: 0x06000ACD RID: 2765 RVA: 0x0001A49C File Offset: 0x0001869C
			public FeatureTuple(PairwiseStrategy.FeatureInfo feature1)
			{
				this._features = new PairwiseStrategy.FeatureInfo[] { feature1 };
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x0001A4B4 File Offset: 0x000186B4
			public FeatureTuple(PairwiseStrategy.FeatureInfo feature1, PairwiseStrategy.FeatureInfo feature2)
			{
				this._features = new PairwiseStrategy.FeatureInfo[] { feature1, feature2 };
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001A4D0 File Offset: 0x000186D0
			public int Length
			{
				get
				{
					return this._features.Length;
				}
			}

			// Token: 0x17000271 RID: 625
			public PairwiseStrategy.FeatureInfo this[int index]
			{
				get
				{
					return this._features[index];
				}
			}

			// Token: 0x0400028B RID: 651
			private readonly PairwiseStrategy.FeatureInfo[] _features;
		}

		// Token: 0x0200018F RID: 399
		internal class TestCaseInfo
		{
			// Token: 0x06000AD1 RID: 2769 RVA: 0x0001A4E4 File Offset: 0x000186E4
			public TestCaseInfo(int length)
			{
				this.Features = new int[length];
			}

			// Token: 0x06000AD2 RID: 2770 RVA: 0x0001A4F8 File Offset: 0x000186F8
			public bool IsTupleCovered(PairwiseStrategy.FeatureTuple tuple)
			{
				for (int i = 0; i < tuple.Length; i++)
				{
					if (this.Features[tuple[i].Dimension] != tuple[i].Feature)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0400028C RID: 652
			public readonly int[] Features;
		}

		// Token: 0x02000190 RID: 400
		internal class PairwiseTestCaseGenerator
		{
			// Token: 0x06000AD3 RID: 2771 RVA: 0x0001A53C File Offset: 0x0001873C
			public IEnumerable GetTestCases(int[] dimensions)
			{
				this._prng = new PairwiseStrategy.FleaRand(15485863U);
				this._dimensions = dimensions;
				this.CreateAllTuples();
				List<PairwiseStrategy.TestCaseInfo> list = new List<PairwiseStrategy.TestCaseInfo>();
				for (;;)
				{
					PairwiseStrategy.FeatureTuple nextTuple = this.GetNextTuple();
					if (nextTuple == null)
					{
						break;
					}
					PairwiseStrategy.TestCaseInfo testCaseInfo = this.CreateTestCase(nextTuple);
					this.RemoveTuplesCoveredByTest(testCaseInfo);
					list.Add(testCaseInfo);
				}
				return list;
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x0001A58F File Offset: 0x0001878F
			private int GetNextRandomNumber()
			{
				return (int)(this._prng.Next() >> 1);
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x0001A5A0 File Offset: 0x000187A0
			private void CreateAllTuples()
			{
				this._uncoveredTuples = new List<PairwiseStrategy.FeatureTuple>[this._dimensions.Length][];
				for (int i = 0; i < this._dimensions.Length; i++)
				{
					this._uncoveredTuples[i] = new List<PairwiseStrategy.FeatureTuple>[this._dimensions[i]];
					for (int j = 0; j < this._dimensions[i]; j++)
					{
						this._uncoveredTuples[i][j] = this.CreateTuples(i, j);
					}
				}
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x0001A610 File Offset: 0x00018810
			private List<PairwiseStrategy.FeatureTuple> CreateTuples(int dimension, int feature)
			{
				List<PairwiseStrategy.FeatureTuple> list = new List<PairwiseStrategy.FeatureTuple>();
				list.Add(new PairwiseStrategy.FeatureTuple(new PairwiseStrategy.FeatureInfo(dimension, feature)));
				for (int i = 0; i < this._dimensions.Length; i++)
				{
					if (i != dimension)
					{
						for (int j = 0; j < this._dimensions[i]; j++)
						{
							list.Add(new PairwiseStrategy.FeatureTuple(new PairwiseStrategy.FeatureInfo(dimension, feature), new PairwiseStrategy.FeatureInfo(i, j)));
						}
					}
				}
				return list;
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x0001A67C File Offset: 0x0001887C
			private PairwiseStrategy.FeatureTuple GetNextTuple()
			{
				for (int i = 0; i < this._uncoveredTuples.Length; i++)
				{
					for (int j = 0; j < this._uncoveredTuples[i].Length; j++)
					{
						List<PairwiseStrategy.FeatureTuple> list = this._uncoveredTuples[i][j];
						if (list.Count > 0)
						{
							PairwiseStrategy.FeatureTuple featureTuple = list[0];
							list.RemoveAt(0);
							return featureTuple;
						}
					}
				}
				return null;
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0001A6D8 File Offset: 0x000188D8
			private PairwiseStrategy.TestCaseInfo CreateTestCase(PairwiseStrategy.FeatureTuple tuple)
			{
				PairwiseStrategy.TestCaseInfo testCaseInfo = null;
				int num = -1;
				for (int i = 0; i < 7; i++)
				{
					PairwiseStrategy.TestCaseInfo testCaseInfo2 = this.CreateRandomTestCase(tuple);
					int num2 = this.MaximizeCoverage(testCaseInfo2, tuple);
					if (num2 > num)
					{
						testCaseInfo = testCaseInfo2;
						num = num2;
					}
				}
				return testCaseInfo;
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x0001A714 File Offset: 0x00018914
			private PairwiseStrategy.TestCaseInfo CreateRandomTestCase(PairwiseStrategy.FeatureTuple tuple)
			{
				PairwiseStrategy.TestCaseInfo testCaseInfo = new PairwiseStrategy.TestCaseInfo(this._dimensions.Length);
				for (int i = 0; i < this._dimensions.Length; i++)
				{
					testCaseInfo.Features[i] = this.GetNextRandomNumber() % this._dimensions[i];
				}
				for (int j = 0; j < tuple.Length; j++)
				{
					testCaseInfo.Features[tuple[j].Dimension] = tuple[j].Feature;
				}
				return testCaseInfo;
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x0001A78C File Offset: 0x0001898C
			private int MaximizeCoverage(PairwiseStrategy.TestCaseInfo testCase, PairwiseStrategy.FeatureTuple tuple)
			{
				int num = 1;
				int[] mutableDimensions = this.GetMutableDimensions(tuple);
				bool flag;
				do
				{
					flag = false;
					this.ScrambleDimensions(mutableDimensions);
					foreach (int num2 in mutableDimensions)
					{
						int num3 = this.CountTuplesCoveredByTest(testCase, num2, testCase.Features[num2]);
						int num4 = this.MaximizeCoverageForDimension(testCase, num2, num3);
						num += num4;
						if (num4 > num3)
						{
							flag = true;
						}
					}
				}
				while (flag);
				return num;
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x0001A7F4 File Offset: 0x000189F4
			private int[] GetMutableDimensions(PairwiseStrategy.FeatureTuple tuple)
			{
				List<int> list = new List<int>();
				bool[] array = new bool[this._dimensions.Length];
				for (int i = 0; i < tuple.Length; i++)
				{
					array[tuple[i].Dimension] = true;
				}
				for (int j = 0; j < this._dimensions.Length; j++)
				{
					if (!array[j])
					{
						list.Add(j);
					}
				}
				return list.ToArray();
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0001A85C File Offset: 0x00018A5C
			private void ScrambleDimensions(int[] dimensions)
			{
				for (int i = 0; i < dimensions.Length; i++)
				{
					int num = this.GetNextRandomNumber() % dimensions.Length;
					int num2 = dimensions[i];
					dimensions[i] = dimensions[num];
					dimensions[num] = num2;
				}
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x0001A890 File Offset: 0x00018A90
			private int MaximizeCoverageForDimension(PairwiseStrategy.TestCaseInfo testCase, int dimension, int bestCoverage)
			{
				List<int> list = new List<int>(this._dimensions[dimension]);
				for (int i = 0; i < this._dimensions[dimension]; i++)
				{
					testCase.Features[dimension] = i;
					int num = this.CountTuplesCoveredByTest(testCase, dimension, i);
					if (num >= bestCoverage)
					{
						if (num > bestCoverage)
						{
							bestCoverage = num;
							list.Clear();
						}
						list.Add(i);
					}
				}
				testCase.Features[dimension] = list[this.GetNextRandomNumber() % list.Count];
				return bestCoverage;
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x0001A908 File Offset: 0x00018B08
			private int CountTuplesCoveredByTest(PairwiseStrategy.TestCaseInfo testCase, int dimension, int feature)
			{
				int num = 0;
				List<PairwiseStrategy.FeatureTuple> list = this._uncoveredTuples[dimension][feature];
				for (int i = 0; i < list.Count; i++)
				{
					if (testCase.IsTupleCovered(list[i]))
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x0001A948 File Offset: 0x00018B48
			private void RemoveTuplesCoveredByTest(PairwiseStrategy.TestCaseInfo testCase)
			{
				for (int i = 0; i < this._uncoveredTuples.Length; i++)
				{
					for (int j = 0; j < this._uncoveredTuples[i].Length; j++)
					{
						List<PairwiseStrategy.FeatureTuple> list = this._uncoveredTuples[i][j];
						for (int k = list.Count - 1; k >= 0; k--)
						{
							if (testCase.IsTupleCovered(list[k]))
							{
								list.RemoveAt(k);
							}
						}
					}
				}
			}

			// Token: 0x0400028D RID: 653
			private PairwiseStrategy.FleaRand _prng;

			// Token: 0x0400028E RID: 654
			private int[] _dimensions;

			// Token: 0x0400028F RID: 655
			private List<PairwiseStrategy.FeatureTuple>[][] _uncoveredTuples;
		}
	}
}
