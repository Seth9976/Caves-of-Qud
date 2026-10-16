using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056B RID: 1387
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class GeneratedLabelSymbol : LabelSymbol
	{
		// Token: 0x060074EC RID: 29932 RVA: 0x001B3F15 File Offset: 0x001B2115
		public GeneratedLabelSymbol(string name)
		{
			this._name = GeneratedLabelSymbol.LabelName(name);
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x001B3F29 File Offset: 0x001B2129
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060074EE RID: 29934 RVA: 0x001B3F31 File Offset: 0x001B2131
		private static string LabelName(string name)
		{
			return name;
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x001B3F34 File Offset: 0x001B2134
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x060074F0 RID: 29936 RVA: 0x001B3F3B File Offset: 0x001B213B
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001C6E RID: 7278
		private readonly string _name;
	}
}
