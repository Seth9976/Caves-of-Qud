using System;
using System.Threading;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000083 RID: 131
	public sealed class MethodReturnType : IConstantProvider, IMetadataTokenProvider, ICustomAttributeProvider, IMarshalInfoProvider
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00010B86 File Offset: 0x0000ED86
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00010B8E File Offset: 0x0000ED8E
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00010B96 File Offset: 0x0000ED96
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00010B9F File Offset: 0x0000ED9F
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010BCD File Offset: 0x0000EDCD
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010BDA File Offset: 0x0000EDDA
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00010BF5 File Offset: 0x0000EDF5
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00010C03 File Offset: 0x0000EE03
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00010C10 File Offset: 0x0000EE10
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00010C1E File Offset: 0x0000EE1E
		public bool HasCustomAttributes
		{
			get
			{
				return this.parameter != null && this.parameter.HasCustomAttributes;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00010C35 File Offset: 0x0000EE35
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.Parameter.CustomAttributes;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00010C42 File Offset: 0x0000EE42
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00010C59 File Offset: 0x0000EE59
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00010C67 File Offset: 0x0000EE67
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00010C7E File Offset: 0x0000EE7E
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00010C8C File Offset: 0x0000EE8C
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00010C99 File Offset: 0x0000EE99
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00010CA7 File Offset: 0x0000EEA7
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00010CBE File Offset: 0x0000EEBE
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00010CCC File Offset: 0x0000EECC
		public bool HasMarshalInfo
		{
			get
			{
				return this.parameter != null && this.parameter.HasMarshalInfo;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00010CE3 File Offset: 0x0000EEE3
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00010CF0 File Offset: 0x0000EEF0
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

		// Token: 0x06000538 RID: 1336 RVA: 0x00010CFE File Offset: 0x0000EEFE
		public MethodReturnType(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x0400018B RID: 395
		internal IMethodSignature method;

		// Token: 0x0400018C RID: 396
		internal ParameterDefinition parameter;

		// Token: 0x0400018D RID: 397
		private TypeReference return_type;
	}
}
