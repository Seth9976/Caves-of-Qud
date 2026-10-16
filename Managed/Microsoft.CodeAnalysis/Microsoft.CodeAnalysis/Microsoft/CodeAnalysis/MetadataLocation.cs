using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200017E RID: 382
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MetadataLocation : Location, IEquatable<MetadataLocation>
	{
		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002E8C2 File Offset: 0x0002CAC2
		internal MetadataLocation(IModuleSymbolInternal module)
		{
			this._module = module;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0002E8D1 File Offset: 0x0002CAD1
		public override LocationKind Kind
		{
			get
			{
				return LocationKind.MetadataFile;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		internal override IModuleSymbolInternal MetadataModuleInternal
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002E8DC File Offset: 0x0002CADC
		public override int GetHashCode()
		{
			return this._module.GetHashCode();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002E8E9 File Offset: 0x0002CAE9
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MetadataLocation);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002E8F7 File Offset: 0x0002CAF7
		[NullableContext(2)]
		public bool Equals(MetadataLocation other)
		{
			return other != null && other._module == this._module;
		}

		// Token: 0x0400045B RID: 1115
		private readonly IModuleSymbolInternal _module;
	}
}
