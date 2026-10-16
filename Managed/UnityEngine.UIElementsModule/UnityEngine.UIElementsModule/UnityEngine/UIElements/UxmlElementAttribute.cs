using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000563 RID: 1379
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class UxmlElementAttribute : Attribute
	{
		// Token: 0x060029D5 RID: 10709 RVA: 0x00097055 File Offset: 0x00095255
		public UxmlElementAttribute()
			: this(null)
		{
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00097060 File Offset: 0x00095260
		public UxmlElementAttribute(string uxmlName)
			: this(uxmlName, null)
		{
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0009706C File Offset: 0x0009526C
		public UxmlElementAttribute(string uxmlName, params Type[] supportedTypes)
		{
			this.name = uxmlName;
			this.supportedChildTypes = supportedTypes;
		}

		// Token: 0x040012BF RID: 4799
		public readonly string name;

		// Token: 0x040012C0 RID: 4800
		public LibraryVisibility visibility = LibraryVisibility.Default;

		// Token: 0x040012C1 RID: 4801
		public string libraryPath;

		// Token: 0x040012C2 RID: 4802
		internal readonly Type[] supportedChildTypes;
	}
}
