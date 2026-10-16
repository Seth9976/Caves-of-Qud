using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000578 RID: 1400
	[Obsolete("IUxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public interface IBaseUxmlFactory
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002A21 RID: 10785
		string uxmlName { get; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002A22 RID: 10786
		string uxmlNamespace { get; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002A23 RID: 10787
		string uxmlQualifiedName { get; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002A24 RID: 10788
		Type uxmlType { get; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002A25 RID: 10789
		bool canHaveAnyAttribute { get; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002A26 RID: 10790
		IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription { get; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002A27 RID: 10791
		IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002A28 RID: 10792
		string substituteForTypeName { get; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002A29 RID: 10793
		string substituteForTypeNamespace { get; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002A2A RID: 10794
		string substituteForTypeQualifiedName { get; }

		// Token: 0x06002A2B RID: 10795
		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);
	}
}
