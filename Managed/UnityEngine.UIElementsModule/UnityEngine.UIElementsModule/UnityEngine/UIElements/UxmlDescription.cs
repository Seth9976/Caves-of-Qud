using System;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x0200056E RID: 1390
	internal readonly struct UxmlDescription
	{
		// Token: 0x060029EF RID: 10735 RVA: 0x00097224 File Offset: 0x00095424
		public UxmlDescription(string uxmlName, string cSharpName, string overriddenCSharpName, FieldInfo serializedField, string[] obsoleteNames = null)
		{
			this.uxmlName = uxmlName;
			this.cSharpName = cSharpName;
			this.overriddenCSharpName = overriddenCSharpName;
			this.serializedField = serializedField;
			this.serializedFieldAttributeFlags = serializedField.DeclaringType.GetField(serializedField.Name + "_UxmlAttributeFlags", BindingFlags.Instance | BindingFlags.NonPublic);
			this.fieldType = ((serializedField.GetCustomAttribute<UxmlTypeReferenceAttribute>() != null) ? typeof(Type) : serializedField.FieldType);
			this.obsoleteNames = obsoleteNames;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000972A0 File Offset: 0x000954A0
		public UxmlDescription(FieldInfo serializedField, UxmlAttributeNames names, string overriddenCSharpName)
		{
			this.uxmlName = names.uxmlName;
			this.cSharpName = names.fieldName;
			this.overriddenCSharpName = overriddenCSharpName;
			this.serializedField = serializedField;
			this.serializedFieldAttributeFlags = serializedField.DeclaringType.GetField(serializedField.Name + "_UxmlAttributeFlags", BindingFlags.Instance | BindingFlags.NonPublic);
			this.fieldType = ((null != names.typeReference) ? typeof(Type) : serializedField.FieldType);
			this.obsoleteNames = names.obsoleteNames;
		}

		// Token: 0x040012D0 RID: 4816
		public readonly string uxmlName;

		// Token: 0x040012D1 RID: 4817
		public readonly string cSharpName;

		// Token: 0x040012D2 RID: 4818
		public readonly string overriddenCSharpName;

		// Token: 0x040012D3 RID: 4819
		public readonly FieldInfo serializedField;

		// Token: 0x040012D4 RID: 4820
		public readonly FieldInfo serializedFieldAttributeFlags;

		// Token: 0x040012D5 RID: 4821
		public readonly Type fieldType;

		// Token: 0x040012D6 RID: 4822
		public readonly string[] obsoleteNames;
	}
}
