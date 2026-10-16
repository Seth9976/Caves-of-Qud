using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A8 RID: 680
	public struct StylePropertyNameCollection : IEnumerable<StylePropertyName>, IEnumerable
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0004EFA1 File Offset: 0x0004D1A1
		internal StylePropertyNameCollection(List<StylePropertyName> list)
		{
			this.propertiesList = list;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004EFAC File Offset: 0x0004D1AC
		public StylePropertyNameCollection.Enumerator GetEnumerator()
		{
			return new StylePropertyNameCollection.Enumerator(this.propertiesList.GetEnumerator());
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004EFD0 File Offset: 0x0004D1D0
		IEnumerator<StylePropertyName> IEnumerable<StylePropertyName>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004EFF0 File Offset: 0x0004D1F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004F010 File Offset: 0x0004D210
		public bool Contains(StylePropertyName stylePropertyName)
		{
			bool flag2;
			using (List<StylePropertyName>.Enumerator enumerator = this.propertiesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StylePropertyName stylePropertyName2 = enumerator.Current;
					bool flag = stylePropertyName2 == stylePropertyName;
					if (flag)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x04000AB1 RID: 2737
		internal List<StylePropertyName> propertiesList;

		// Token: 0x020002A9 RID: 681
		public struct Enumerator : IEnumerator<StylePropertyName>, IEnumerator, IDisposable
		{
			// Token: 0x0600138D RID: 5005 RVA: 0x0004F074 File Offset: 0x0004D274
			internal Enumerator(List<StylePropertyName>.Enumerator enumerator)
			{
				this.m_Enumerator = enumerator;
			}

			// Token: 0x0600138E RID: 5006 RVA: 0x0004F07E File Offset: 0x0004D27E
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004F08B File Offset: 0x0004D28B
			public StylePropertyName Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004F098 File Offset: 0x0004D298
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x00002152 File Offset: 0x00000352
			public void Reset()
			{
			}

			// Token: 0x06001392 RID: 5010 RVA: 0x0004F0A5 File Offset: 0x0004D2A5
			public void Dispose()
			{
				this.m_Enumerator.Dispose();
			}

			// Token: 0x04000AB2 RID: 2738
			private List<StylePropertyName>.Enumerator m_Enumerator;
		}
	}
}
