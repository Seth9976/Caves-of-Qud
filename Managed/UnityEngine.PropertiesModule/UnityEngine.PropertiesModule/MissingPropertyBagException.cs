using System;

namespace Unity.Properties
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class MissingPropertyBagException : Exception
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E93 File Offset: 0x00001093
		public Type Type { get; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002E9B File Offset: 0x0000109B
		public MissingPropertyBagException(Type type)
			: base(MissingPropertyBagException.GetMessageForType(type))
		{
			this.Type = type;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EB2 File Offset: 0x000010B2
		public MissingPropertyBagException(Type type, Exception inner)
			: base(MissingPropertyBagException.GetMessageForType(type), inner)
		{
			this.Type = type;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002ECC File Offset: 0x000010CC
		private static string GetMessageForType(Type type)
		{
			return "No PropertyBag was found for Type=[" + type.FullName + "]. Please make sure all types are declared ahead of time using [GeneratePropertyBagAttribute], [GeneratePropertyBagsForTypeAttribute] or [GeneratePropertyBagsForTypesQualifiedWithAttribute]";
		}
	}
}
