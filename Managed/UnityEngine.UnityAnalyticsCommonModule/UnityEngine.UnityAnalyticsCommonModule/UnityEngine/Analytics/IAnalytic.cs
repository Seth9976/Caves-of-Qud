using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Internal;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000F RID: 15
	[ExcludeFromDocs]
	public interface IAnalytic
	{
		// Token: 0x0600001C RID: 28
		bool TryGatherData(out IAnalytic.IData data, [NotNullWhen(false)] out Exception error);

		// Token: 0x02000010 RID: 16
		public interface IData
		{
		}

		// Token: 0x02000011 RID: 17
		public struct DataList<T> : IEnumerable, IAnalytic.IData where T : struct
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000021DE File Offset: 0x000003DE
			public DataList(T[] datas)
			{
				this.m_UsageData = datas;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000021E8 File Offset: 0x000003E8
			public IEnumerator GetEnumerator()
			{
				return this.m_UsageData.GetEnumerator();
			}

			// Token: 0x04000018 RID: 24
			private readonly T[] m_UsageData;
		}
	}
}
