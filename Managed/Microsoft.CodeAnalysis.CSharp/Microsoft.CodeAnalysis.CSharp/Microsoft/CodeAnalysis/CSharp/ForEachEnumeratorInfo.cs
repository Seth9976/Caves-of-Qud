using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000026 RID: 38
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ForEachEnumeratorInfo
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00036455 File Offset: 0x00034655
		public TypeSymbol ElementType
		{
			get
			{
				return this.ElementTypeWithAnnotations.Type;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00036464 File Offset: 0x00034664
		private ForEachEnumeratorInfo(TypeSymbol collectionType, TypeWithAnnotations elementType, MethodArgumentInfo getEnumeratorInfo, MethodSymbol currentPropertyGetter, MethodArgumentInfo moveNextInfo, bool isAsync, bool needsDisposal, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo disposeAwaitableInfo, [global::System.Runtime.CompilerServices.Nullable(2)] MethodArgumentInfo patternDisposeInfo, Conversion collectionConversion, Conversion currentConversion, Conversion enumeratorConversion, BinderFlags location)
		{
			this.CollectionType = collectionType;
			this.ElementTypeWithAnnotations = elementType;
			this.GetEnumeratorInfo = getEnumeratorInfo;
			this.CurrentPropertyGetter = currentPropertyGetter;
			this.MoveNextInfo = moveNextInfo;
			this.IsAsync = isAsync;
			this.NeedsDisposal = needsDisposal;
			this.DisposeAwaitableInfo = disposeAwaitableInfo;
			this.PatternDisposeInfo = patternDisposeInfo;
			this.CollectionConversion = collectionConversion;
			this.CurrentConversion = currentConversion;
			this.EnumeratorConversion = enumeratorConversion;
			this.Location = location;
		}

		// Token: 0x04000065 RID: 101
		public readonly TypeSymbol CollectionType;

		// Token: 0x04000066 RID: 102
		public readonly TypeWithAnnotations ElementTypeWithAnnotations;

		// Token: 0x04000067 RID: 103
		public readonly MethodArgumentInfo GetEnumeratorInfo;

		// Token: 0x04000068 RID: 104
		public readonly MethodSymbol CurrentPropertyGetter;

		// Token: 0x04000069 RID: 105
		public readonly MethodArgumentInfo MoveNextInfo;

		// Token: 0x0400006A RID: 106
		public readonly bool NeedsDisposal;

		// Token: 0x0400006B RID: 107
		public readonly bool IsAsync;

		// Token: 0x0400006C RID: 108
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public readonly BoundAwaitableInfo DisposeAwaitableInfo;

		// Token: 0x0400006D RID: 109
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public readonly MethodArgumentInfo PatternDisposeInfo;

		// Token: 0x0400006E RID: 110
		public readonly Conversion CollectionConversion;

		// Token: 0x0400006F RID: 111
		public readonly Conversion CurrentConversion;

		// Token: 0x04000070 RID: 112
		public readonly Conversion EnumeratorConversion;

		// Token: 0x04000071 RID: 113
		public readonly BinderFlags Location;

		// Token: 0x0200063D RID: 1597
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal struct Builder
		{
			// Token: 0x1700267D RID: 9853
			// (get) Token: 0x06008257 RID: 33367 RVA: 0x001DA18B File Offset: 0x001D838B
			public TypeSymbol ElementType
			{
				get
				{
					return this.ElementTypeWithAnnotations.Type;
				}
			}

			// Token: 0x06008258 RID: 33368 RVA: 0x001DA198 File Offset: 0x001D8398
			public ForEachEnumeratorInfo Build(BinderFlags location)
			{
				return new ForEachEnumeratorInfo(this.CollectionType, this.ElementTypeWithAnnotations, this.GetEnumeratorInfo, this.CurrentPropertyGetter, this.MoveNextInfo, this.IsAsync, this.NeedsDisposal, this.DisposeAwaitableInfo, this.PatternDisposeInfo, this.CollectionConversion, this.CurrentConversion, this.EnumeratorConversion, location);
			}

			// Token: 0x1700267E RID: 9854
			// (get) Token: 0x06008259 RID: 33369 RVA: 0x001DA1F3 File Offset: 0x001D83F3
			public bool IsIncomplete
			{
				get
				{
					return this.GetEnumeratorInfo == null || this.MoveNextInfo == null || this.CurrentPropertyGetter == null;
				}
			}

			// Token: 0x04001F71 RID: 8049
			public TypeSymbol CollectionType;

			// Token: 0x04001F72 RID: 8050
			public TypeWithAnnotations ElementTypeWithAnnotations;

			// Token: 0x04001F73 RID: 8051
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public MethodArgumentInfo GetEnumeratorInfo;

			// Token: 0x04001F74 RID: 8052
			public MethodSymbol CurrentPropertyGetter;

			// Token: 0x04001F75 RID: 8053
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public MethodArgumentInfo MoveNextInfo;

			// Token: 0x04001F76 RID: 8054
			public bool IsAsync;

			// Token: 0x04001F77 RID: 8055
			public bool NeedsDisposal;

			// Token: 0x04001F78 RID: 8056
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public BoundAwaitableInfo DisposeAwaitableInfo;

			// Token: 0x04001F79 RID: 8057
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public MethodArgumentInfo PatternDisposeInfo;

			// Token: 0x04001F7A RID: 8058
			public Conversion CollectionConversion;

			// Token: 0x04001F7B RID: 8059
			public Conversion CurrentConversion;

			// Token: 0x04001F7C RID: 8060
			public Conversion EnumeratorConversion;
		}
	}
}
