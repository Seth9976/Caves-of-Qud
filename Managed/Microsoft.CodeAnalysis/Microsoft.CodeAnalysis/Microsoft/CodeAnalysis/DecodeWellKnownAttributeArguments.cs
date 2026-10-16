using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000228 RID: 552
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DecodeWellKnownAttributeArguments<[Nullable(0)] TAttributeSyntax, [Nullable(0)] TAttributeData, [Nullable(2)] TAttributeLocation> where TAttributeSyntax : SyntaxNode where TAttributeData : AttributeData
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x0004455D File Offset: 0x0004275D
		public T GetOrCreateData<[Nullable(0)] T>() where T : WellKnownAttributeData, new()
		{
			if (this._lazyDecodeData == null)
			{
				this._lazyDecodeData = new T();
			}
			return (T)((object)this._lazyDecodeData);
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00044582 File Offset: 0x00042782
		public readonly bool HasDecodedData
		{
			get
			{
				return this._lazyDecodeData != null;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0004458F File Offset: 0x0004278F
		public readonly WellKnownAttributeData DecodedData
		{
			get
			{
				return this._lazyDecodeData;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00044597 File Offset: 0x00042797
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0004459F File Offset: 0x0004279F
		[Nullable(2)]
		public TAttributeSyntax AttributeSyntaxOpt
		{
			[NullableContext(2)]
			readonly get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000445A8 File Offset: 0x000427A8
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x000445B0 File Offset: 0x000427B0
		public TAttributeData Attribute { readonly get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x000445B9 File Offset: 0x000427B9
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x000445C1 File Offset: 0x000427C1
		public int Index { readonly get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000445CA File Offset: 0x000427CA
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x000445D2 File Offset: 0x000427D2
		public int AttributesCount { readonly get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x000445DB File Offset: 0x000427DB
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x000445E3 File Offset: 0x000427E3
		public BindingDiagnosticBag Diagnostics { readonly get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x000445EC File Offset: 0x000427EC
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x000445F4 File Offset: 0x000427F4
		public TAttributeLocation SymbolPart { readonly get; set; }

		// Token: 0x040008EE RID: 2286
		[Nullable(2)]
		private WellKnownAttributeData _lazyDecodeData;
	}
}
