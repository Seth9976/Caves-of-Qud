using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012A RID: 298
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Reference,nq}")]
	public struct CommandLineReference : IEquatable<CommandLineReference>
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		public CommandLineReference(string reference, MetadataReferenceProperties properties)
		{
			this._reference = reference;
			this._properties = properties;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001FFE0 File Offset: 0x0001E1E0
		public string Reference
		{
			get
			{
				return this._reference;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		public MetadataReferenceProperties Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CommandLineReference && base.Equals((CommandLineReference)obj);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00020018 File Offset: 0x0001E218
		public bool Equals(CommandLineReference other)
		{
			return this._reference == other._reference && this._properties.Equals(other._properties);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00020050 File Offset: 0x0001E250
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this._reference, this._properties.GetHashCode());
		}

		// Token: 0x04000320 RID: 800
		private readonly string _reference;

		// Token: 0x04000321 RID: 801
		private readonly MetadataReferenceProperties _properties;
	}
}
