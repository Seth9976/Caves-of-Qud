using System;

namespace Unity.Properties
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class GeneratePropertyBagsForTypesQualifiedWithAttribute : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public Type Type { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002DEE File Offset: 0x00000FEE
		public TypeGenerationOptions Options { get; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public GeneratePropertyBagsForTypesQualifiedWithAttribute(Type type, TypeGenerationOptions options = TypeGenerationOptions.Default)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentException("type is null.");
			}
			bool flag2 = !type.IsInterface;
			if (flag2)
			{
				throw new ArgumentException("GeneratePropertyBagsForTypesQualifiedWithAttribute Type must be an interface type.");
			}
			this.Type = type;
			this.Options = options;
		}
	}
}
