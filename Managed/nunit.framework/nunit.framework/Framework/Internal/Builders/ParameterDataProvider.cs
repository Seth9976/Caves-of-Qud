using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BE RID: 190
	public class ParameterDataProvider : IParameterDataProvider
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x00010E40 File Offset: 0x0000F040
		public ParameterDataProvider(params IParameterDataProvider[] providers)
		{
			this._providers.AddRange(providers);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00010E60 File Offset: 0x0000F060
		public bool HasDataFor(IParameterInfo parameter)
		{
			using (List<IParameterDataProvider>.Enumerator enumerator = this._providers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasDataFor(parameter))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public IEnumerable GetDataFor(IParameterInfo parameter)
		{
			foreach (IParameterDataProvider parameterDataProvider in this._providers)
			{
				foreach (object obj in parameterDataProvider.GetDataFor(parameter))
				{
					yield return obj;
				}
				IEnumerator enumerator2 = null;
			}
			List<IParameterDataProvider>.Enumerator enumerator = default(List<IParameterDataProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000169 RID: 361
		private List<IParameterDataProvider> _providers = new List<IParameterDataProvider>();
	}
}
