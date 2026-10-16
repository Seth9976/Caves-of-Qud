using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000535 RID: 1333
	[Obsolete("UxmlRootElementFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlRootElementFactory : UxmlFactory<VisualElement, UxmlRootElementTraits>
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000950B7 File Offset: 0x000932B7
		public override string uxmlName
		{
			get
			{
				return "UXML";
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000950BE File Offset: 0x000932BE
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000950D6 File Offset: 0x000932D6
		public override string substituteForTypeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000950D6 File Offset: 0x000932D6
		public override string substituteForTypeNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000950D6 File Offset: 0x000932D6
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000950E0 File Offset: 0x000932E0
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04001263 RID: 4707
		internal const string k_ElementName = "UXML";
	}
}
