using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x02000571 RID: 1393
	[Obsolete("BaseUxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class BaseUxmlTraits
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x00097B56 File Offset: 0x00095D56
		protected BaseUxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x00097B68 File Offset: 0x00095D68
		// (set) Token: 0x060029FB RID: 10747 RVA: 0x00097B70 File Offset: 0x00095D70
		public bool canHaveAnyAttribute { get; protected set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00097B7C File Offset: 0x00095D7C
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription attributeDescription in this.GetAllAttributeDescriptionForType(base.GetType()))
				{
					yield return attributeDescription;
					attributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x00097B9C File Offset: 0x00095D9C
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00097BBB File Offset: 0x00095DBB
		private IEnumerable<UxmlAttributeDescription> GetAllAttributeDescriptionForType(Type t)
		{
			Type baseType = t.BaseType;
			bool flag = baseType != null;
			if (flag)
			{
				foreach (UxmlAttributeDescription ident in this.GetAllAttributeDescriptionForType(baseType))
				{
					yield return ident;
					ident = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
			}
			foreach (FieldInfo fieldInfo in from f in t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType)
				select f)
			{
				yield return (UxmlAttributeDescription)fieldInfo.GetValue(this);
				fieldInfo = null;
			}
			IEnumerator<FieldInfo> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
