using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200056B RID: 1387
	public readonly struct UxmlAttributeNames
	{
		// Token: 0x060029EA RID: 10730 RVA: 0x000971BC File Offset: 0x000953BC
		public UxmlAttributeNames(string fieldName, string uxmlName, Type typeReference = null, params string[] obsoleteNames)
		{
			this.fieldName = fieldName;
			this.uxmlName = uxmlName;
			this.obsoleteNames = obsoleteNames ?? Array.Empty<string>();
			this.typeReference = typeReference;
		}

		// Token: 0x040012CB RID: 4811
		public readonly string fieldName;

		// Token: 0x040012CC RID: 4812
		public readonly string uxmlName;

		// Token: 0x040012CD RID: 4813
		public readonly Type typeReference;

		// Token: 0x040012CE RID: 4814
		public readonly string[] obsoleteNames;
	}
}
