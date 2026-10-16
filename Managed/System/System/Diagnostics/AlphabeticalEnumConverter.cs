using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200024E RID: 590
	internal sealed class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0004E26C File Offset: 0x0004C46C
		public AlphabeticalEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004E275 File Offset: 0x0004C475
		[MonoTODO("Create sorted standart values")]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}
