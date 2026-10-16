using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000538 RID: 1336
	[Obsolete("UxmlStyleFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlStyleFactory : UxmlFactory<VisualElement, UxmlStyleTraits>
	{
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x00095217 File Offset: 0x00093417
		public override string uxmlName
		{
			get
			{
				return "Style";
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x0009521E File Offset: 0x0009341E
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x00095236 File Offset: 0x00093436
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x00095247 File Offset: 0x00093447
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x00095261 File Offset: 0x00093461
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00095274 File Offset: 0x00093474
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x0400126A RID: 4714
		internal const string k_ElementName = "Style";
	}
}
