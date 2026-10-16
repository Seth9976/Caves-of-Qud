using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000B6 RID: 182
	public class CombinatorialStrategy : ICombiningStrategy
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public IEnumerable<ITestCaseData> GetTestCases(IEnumerable[] sources)
		{
			List<ITestCaseData> list = new List<ITestCaseData>();
			IEnumerator[] array = new IEnumerator[sources.Length];
			int num = -1;
			for (;;)
			{
				if (++num >= sources.Length)
				{
					object[] array2 = new object[sources.Length];
					for (int i = 0; i < sources.Length; i++)
					{
						array2[i] = array[i].Current;
					}
					TestCaseParameters testCaseParameters = new TestCaseParameters(array2);
					list.Add(testCaseParameters);
					num = sources.Length;
					while (--num >= 0 && !array[num].MoveNext())
					{
					}
					if (num < 0)
					{
						return list;
					}
				}
				else
				{
					array[num] = sources[num].GetEnumerator();
					if (!array[num].MoveNext())
					{
						break;
					}
				}
			}
			return list;
		}
	}
}
