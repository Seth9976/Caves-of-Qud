using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200053B RID: 1339
	[Obsolete("UxmlTemplateFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlTemplateFactory : UxmlFactory<VisualElement, UxmlTemplateTraits>
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0009539F File Offset: 0x0009359F
		public override string uxmlName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x000953A6 File Offset: 0x000935A6
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x00095236 File Offset: 0x00093436
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x00095247 File Offset: 0x00093447
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x00095261 File Offset: 0x00093461
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000953C0 File Offset: 0x000935C0
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04001272 RID: 4722
		internal const string k_ElementName = "Template";
	}
}
