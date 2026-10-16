using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200053E RID: 1342
	[Obsolete("UxmlAttributeOverridesFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000954F3 File Offset: 0x000936F3
		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x000954FA File Offset: 0x000936FA
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x00095236 File Offset: 0x00093436
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x00095247 File Offset: 0x00093447
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x00095261 File Offset: 0x00093461
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00095514 File Offset: 0x00093714
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x0400127A RID: 4730
		internal const string k_ElementName = "AttributeOverrides";
	}
}
