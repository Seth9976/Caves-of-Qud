using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000C1 RID: 193
	public class SequentialStrategy : ICombiningStrategy
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00011030 File Offset: 0x0000F230
		public IEnumerable<ITestCaseData> GetTestCases(IEnumerable[] sources)
		{
			List<ITestCaseData> list = new List<ITestCaseData>();
			IEnumerator[] array = new IEnumerator[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				array[i] = sources[i].GetEnumerator();
			}
			for (;;)
			{
				bool flag = false;
				object[] array2 = new object[sources.Length];
				for (int j = 0; j < sources.Length; j++)
				{
					if (array[j].MoveNext())
					{
						array2[j] = array[j].Current;
						flag = true;
					}
					else
					{
						array2[j] = null;
					}
				}
				if (!flag)
				{
					break;
				}
				TestCaseParameters testCaseParameters = new TestCaseParameters(array2);
				list.Add(testCaseParameters);
			}
			return list;
		}
	}
}
