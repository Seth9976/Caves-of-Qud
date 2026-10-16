using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200057C RID: 1404
	[Obsolete("BaseUxmlFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class BaseUxmlFactory<TCreatedType, TTraits> where TCreatedType : new() where TTraits : BaseUxmlTraits, new()
	{
		// Token: 0x06002A2E RID: 10798 RVA: 0x00098118 File Offset: 0x00096318
		protected BaseUxmlFactory()
		{
			this.m_Traits = new TTraits();
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00098130 File Offset: 0x00096330
		public virtual string uxmlName
		{
			get
			{
				return typeof(TCreatedType).Name;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00098154 File Offset: 0x00096354
		public virtual string uxmlNamespace
		{
			get
			{
				return typeof(TCreatedType).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00098180 File Offset: 0x00096380
		public virtual string uxmlQualifiedName
		{
			get
			{
				return typeof(TCreatedType).FullName;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x00095A88 File Offset: 0x00093C88
		public virtual Type uxmlType
		{
			get
			{
				return typeof(TCreatedType);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x000981A4 File Offset: 0x000963A4
		public bool canHaveAnyAttribute
		{
			get
			{
				return this.m_Traits.canHaveAnyAttribute;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000981C8 File Offset: 0x000963C8
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				return this.m_Traits.uxmlAttributesDescription;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000981EC File Offset: 0x000963EC
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				return this.m_Traits.uxmlChildElementsDescription;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x00098210 File Offset: 0x00096410
		public virtual string substituteForTypeName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).Name;
				}
				return text;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x00098258 File Offset: 0x00096458
		public virtual string substituteForTypeNamespace
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).Namespace ?? string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x000982A8 File Offset: 0x000964A8
		public virtual string substituteForTypeQualifiedName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).FullName;
				}
				return text;
			}
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000982F0 File Offset: 0x000964F0
		public virtual bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc)
		{
			return true;
		}

		// Token: 0x040012F5 RID: 4853
		internal TTraits m_Traits;
	}
}
