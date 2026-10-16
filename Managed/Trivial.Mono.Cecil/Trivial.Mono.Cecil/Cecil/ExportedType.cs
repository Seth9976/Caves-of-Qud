using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000031 RID: 49
	public sealed class ExportedType : IMetadataTokenProvider
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005028 File Offset: 0x00003228
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005030 File Offset: 0x00003230
		public string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005039 File Offset: 0x00003239
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005041 File Offset: 0x00003241
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000504A File Offset: 0x0000324A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005052 File Offset: 0x00003252
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000505B File Offset: 0x0000325B
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005077 File Offset: 0x00003277
		public IMetadataScope Scope
		{
			get
			{
				if (this.declaring_type != null)
				{
					return this.declaring_type.Scope;
				}
				return this.scope;
			}
			set
			{
				if (this.declaring_type != null)
				{
					this.declaring_type.Scope = value;
					return;
				}
				this.scope = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005095 File Offset: 0x00003295
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000509D File Offset: 0x0000329D
		public ExportedType DeclaringType
		{
			get
			{
				return this.declaring_type;
			}
			set
			{
				this.declaring_type = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000050A6 File Offset: 0x000032A6
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000050AE File Offset: 0x000032AE
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000050B7 File Offset: 0x000032B7
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000050BF File Offset: 0x000032BF
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000050C8 File Offset: 0x000032C8
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000050D7 File Offset: 0x000032D7
		public bool IsNotPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 0U, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000050ED File Offset: 0x000032ED
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000050FC File Offset: 0x000032FC
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005112 File Offset: 0x00003312
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005121 File Offset: 0x00003321
		public bool IsNestedPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005137 File Offset: 0x00003337
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005146 File Offset: 0x00003346
		public bool IsNestedPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 3U, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000515C File Offset: 0x0000335C
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000516B File Offset: 0x0000336B
		public bool IsNestedFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 4U, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005181 File Offset: 0x00003381
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005190 File Offset: 0x00003390
		public bool IsNestedAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 5U, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000051A6 File Offset: 0x000033A6
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000051B5 File Offset: 0x000033B5
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 6U, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000051CB File Offset: 0x000033CB
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000051DA File Offset: 0x000033DA
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 7U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 7U, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000051F0 File Offset: 0x000033F0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005200 File Offset: 0x00003400
		public bool IsAutoLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 0U, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005217 File Offset: 0x00003417
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005227 File Offset: 0x00003427
		public bool IsSequentialLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 8U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 8U, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000523E File Offset: 0x0000343E
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000524F File Offset: 0x0000344F
		public bool IsExplicitLayout
		{
			get
			{
				return this.attributes.GetMaskedAttributes(24U, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(24U, 16U, value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005267 File Offset: 0x00003467
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005277 File Offset: 0x00003477
		public bool IsClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 0U, value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000528E File Offset: 0x0000348E
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000529F File Offset: 0x0000349F
		public bool IsInterface
		{
			get
			{
				return this.attributes.GetMaskedAttributes(32U, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(32U, 32U, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000052B7 File Offset: 0x000034B7
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000052C9 File Offset: 0x000034C9
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(128U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128U, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000052E2 File Offset: 0x000034E2
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000052F4 File Offset: 0x000034F4
		public bool IsSealed
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000530D File Offset: 0x0000350D
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000531F File Offset: 0x0000351F
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024U, value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005338 File Offset: 0x00003538
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000534A File Offset: 0x0000354A
		public bool IsImport
		{
			get
			{
				return this.attributes.GetAttributes(4096U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096U, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005363 File Offset: 0x00003563
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005375 File Offset: 0x00003575
		public bool IsSerializable
		{
			get
			{
				return this.attributes.GetAttributes(8192U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192U, value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000538E File Offset: 0x0000358E
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000053A1 File Offset: 0x000035A1
		public bool IsAnsiClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 0U, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000053BB File Offset: 0x000035BB
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000053D2 File Offset: 0x000035D2
		public bool IsUnicodeClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 65536U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 65536U, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000053F0 File Offset: 0x000035F0
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00005407 File Offset: 0x00003607
		public bool IsAutoClass
		{
			get
			{
				return this.attributes.GetMaskedAttributes(196608U, 131072U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(196608U, 131072U, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005425 File Offset: 0x00003625
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00005437 File Offset: 0x00003637
		public bool IsBeforeFieldInit
		{
			get
			{
				return this.attributes.GetAttributes(1048576U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1048576U, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005450 File Offset: 0x00003650
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00005462 File Offset: 0x00003662
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048U, value);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000547B File Offset: 0x0000367B
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000548D File Offset: 0x0000368D
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(262144U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(262144U, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000054A6 File Offset: 0x000036A6
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000054B8 File Offset: 0x000036B8
		public bool IsForwarder
		{
			get
			{
				return this.attributes.GetAttributes(2097152U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2097152U, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000054D4 File Offset: 0x000036D4
		public string FullName
		{
			get
			{
				string text = (string.IsNullOrEmpty(this.@namespace) ? this.name : (this.@namespace + "." + this.name));
				if (this.declaring_type != null)
				{
					return this.declaring_type.FullName + "/" + text;
				}
				return text;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000552D File Offset: 0x0000372D
		public ExportedType(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
		{
			this.@namespace = @namespace;
			this.name = name;
			this.scope = scope;
			this.module = module;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005552 File Offset: 0x00003752
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000555A File Offset: 0x0000375A
		public TypeDefinition Resolve()
		{
			return this.module.Resolve(this.CreateReference());
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000556D File Offset: 0x0000376D
		internal TypeReference CreateReference()
		{
			return new TypeReference(this.@namespace, this.name, this.module, this.scope)
			{
				DeclaringType = ((this.declaring_type != null) ? this.declaring_type.CreateReference() : null)
			};
		}

		// Token: 0x0400005B RID: 91
		private string @namespace;

		// Token: 0x0400005C RID: 92
		private string name;

		// Token: 0x0400005D RID: 93
		private uint attributes;

		// Token: 0x0400005E RID: 94
		private IMetadataScope scope;

		// Token: 0x0400005F RID: 95
		private ModuleDefinition module;

		// Token: 0x04000060 RID: 96
		private int identifier;

		// Token: 0x04000061 RID: 97
		private ExportedType declaring_type;

		// Token: 0x04000062 RID: 98
		internal MetadataToken token;
	}
}
