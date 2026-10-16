using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200023D RID: 573
	public sealed class MethodReturnType : IConstantProvider, IMetadataTokenProvider, ICustomAttributeProvider, IMarshalInfoProvider
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002BB36 File Offset: 0x00029D36
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002BB3E File Offset: 0x00029D3E
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0002BB46 File Offset: 0x00029D46
		public TypeReference ReturnType
		{
			get
			{
				return this.return_type;
			}
			set
			{
				this.return_type = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002BB4F File Offset: 0x00029D4F
		internal ParameterDefinition Parameter
		{
			get
			{
				if (this.parameter == null)
				{
					Interlocked.CompareExchange<ParameterDefinition>(ref this.parameter, new ParameterDefinition(this.return_type, this.method), null);
				}
				return this.parameter;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002BB7D File Offset: 0x00029D7D
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0002BB8A File Offset: 0x00029D8A
		public MetadataToken MetadataToken
		{
			get
			{
				return this.Parameter.MetadataToken;
			}
			set
			{
				this.Parameter.MetadataToken = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002BB98 File Offset: 0x00029D98
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0002BBA5 File Offset: 0x00029DA5
		public ParameterAttributes Attributes
		{
			get
			{
				return this.Parameter.Attributes;
			}
			set
			{
				this.Parameter.Attributes = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002BBB3 File Offset: 0x00029DB3
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		public string Name
		{
			get
			{
				return this.Parameter.Name;
			}
			set
			{
				this.Parameter.Name = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002BBCE File Offset: 0x00029DCE
		public bool HasCustomAttributes
		{
			get
			{
				return this.parameter != null && this.parameter.HasCustomAttributes;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002BBE5 File Offset: 0x00029DE5
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.Parameter.CustomAttributes;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0002BBF2 File Offset: 0x00029DF2
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0002BC09 File Offset: 0x00029E09
		public bool HasDefault
		{
			get
			{
				return this.parameter != null && this.parameter.HasDefault;
			}
			set
			{
				this.Parameter.HasDefault = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0002BC17 File Offset: 0x00029E17
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0002BC2E File Offset: 0x00029E2E
		public bool HasConstant
		{
			get
			{
				return this.parameter != null && this.parameter.HasConstant;
			}
			set
			{
				this.Parameter.HasConstant = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0002BC3C File Offset: 0x00029E3C
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0002BC49 File Offset: 0x00029E49
		public object Constant
		{
			get
			{
				return this.Parameter.Constant;
			}
			set
			{
				this.Parameter.Constant = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0002BC57 File Offset: 0x00029E57
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0002BC6E File Offset: 0x00029E6E
		public bool HasFieldMarshal
		{
			get
			{
				return this.parameter != null && this.parameter.HasFieldMarshal;
			}
			set
			{
				this.Parameter.HasFieldMarshal = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0002BC7C File Offset: 0x00029E7C
		public bool HasMarshalInfo
		{
			get
			{
				return this.parameter != null && this.parameter.HasMarshalInfo;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002BC93 File Offset: 0x00029E93
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0002BCA0 File Offset: 0x00029EA0
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.Parameter.MarshalInfo;
			}
			set
			{
				this.Parameter.MarshalInfo = value;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002BCAE File Offset: 0x00029EAE
		public MethodReturnType(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x040003D9 RID: 985
		internal IMethodSignature method;

		// Token: 0x040003DA RID: 986
		internal ParameterDefinition parameter;

		// Token: 0x040003DB RID: 987
		private TypeReference return_type;
	}
}
