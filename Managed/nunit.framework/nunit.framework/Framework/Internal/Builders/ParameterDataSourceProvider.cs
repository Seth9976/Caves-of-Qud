using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BF RID: 191
	public class ParameterDataSourceProvider : IParameterDataProvider
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00010ED3 File Offset: 0x0000F0D3
		public bool HasDataFor(IParameterInfo parameter)
		{
			return parameter.IsDefined<IParameterDataSource>(false);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00010EDC File Offset: 0x0000F0DC
		public IEnumerable GetDataFor(IParameterInfo parameter)
		{
			List<object> list = new List<object>();
			IParameterDataSource[] customAttributes = parameter.GetCustomAttributes<IParameterDataSource>(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				foreach (object obj in customAttributes[i].GetData(parameter))
				{
					list.Add(obj);
				}
			}
			return list;
		}
	}
}
