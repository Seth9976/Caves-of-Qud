using System;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C2 RID: 194
	public class TypeReference : MemberReference, IGenericParameterProvider, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0000FBC2 File Offset: 0x0000DDC2
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00019378 File Offset: 0x00017578
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException("Projected type reference name can't be changed.");
				}
				base.Name = value;
				this.ClearFullName();
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000193A8 File Offset: 0x000175A8
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x000193B0 File Offset: 0x000175B0
		public virtual string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != this.@namespace)
				{
					throw new InvalidOperationException("Projected type reference namespace can't be changed.");
				}
				this.@namespace = value;
				this.ClearFullName();
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000193E0 File Offset: 0x000175E0
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000193E8 File Offset: 0x000175E8
		public virtual bool IsValueType
		{
			get
			{
				return this.value_type;
			}
			set
			{
				this.value_type = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000193F1 File Offset: 0x000175F1
		public override ModuleDefinition Module
		{
			get
			{
				if (this.module != null)
				{
					return this.module;
				}
				TypeReference declaringType = this.DeclaringType;
				if (declaringType == null)
				{
					return null;
				}
				return declaringType.Module;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00019413 File Offset: 0x00017613
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x000056A4 File Offset: 0x000038A4
		internal TypeReferenceProjection WindowsRuntimeProjection
		{
			get
			{
				return (TypeReferenceProjection)this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00002654 File Offset: 0x00000854
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00005FCD File Offset: 0x000041CD
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x000025EF File Offset: 0x000007EF
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Type;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00019420 File Offset: 0x00017620
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00019430 File Offset: 0x00017630
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters;
				}
				return this.generic_parameters = new GenericParameterCollection(this);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001945C File Offset: 0x0001765C
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00019480 File Offset: 0x00017680
		public virtual IMetadataScope Scope
		{
			get
			{
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Scope;
				}
				return this.scope;
			}
			set
			{
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					if (base.IsWindowsRuntimeProjection && value != declaringType.Scope)
					{
						throw new InvalidOperationException("Projected type scope can't be changed.");
					}
					declaringType.Scope = value;
					return;
				}
				else
				{
					if (base.IsWindowsRuntimeProjection && value != this.scope)
					{
						throw new InvalidOperationException("Projected type scope can't be changed.");
					}
					this.scope = value;
					return;
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000194DE File Offset: 0x000176DE
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000194E9 File Offset: 0x000176E9
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x000194F1 File Offset: 0x000176F1
		public override TypeReference DeclaringType
		{
			get
			{
				return base.DeclaringType;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.DeclaringType)
				{
					throw new InvalidOperationException("Projected type declaring type can't be changed.");
				}
				base.DeclaringType = value;
				this.ClearFullName();
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001951C File Offset: 0x0001771C
		public override string FullName
		{
			get
			{
				if (this.fullname != null)
				{
					return this.fullname;
				}
				this.fullname = this.TypeFullName();
				if (this.IsNested)
				{
					this.fullname = this.DeclaringType.FullName + "/" + this.fullname;
				}
				return this.fullname;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsByReference
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsSentinel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsRequiredModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsOptionalModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsFunctionPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00019573 File Offset: 0x00017773
		public virtual bool IsPrimitive
		{
			get
			{
				return this.etype.IsPrimitive();
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00019580 File Offset: 0x00017780
		public virtual MetadataType MetadataType
		{
			get
			{
				if (this.etype != ElementType.None)
				{
					return (MetadataType)this.etype;
				}
				if (!this.IsValueType)
				{
					return MetadataType.Class;
				}
				return MetadataType.ValueType;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001959E File Offset: 0x0001779E
		protected TypeReference(string @namespace, string name)
			: base(name)
		{
			this.@namespace = @namespace ?? string.Empty;
			this.token = new MetadataToken(TokenType.TypeRef, 0);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000195C8 File Offset: 0x000177C8
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
			: this(@namespace, name)
		{
			this.module = module;
			this.scope = scope;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000195E1 File Offset: 0x000177E1
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope, bool valueType)
			: this(@namespace, name, module, scope)
		{
			this.value_type = valueType;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000195F6 File Offset: 0x000177F6
		protected virtual void ClearFullName()
		{
			this.fullname = null;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00002654 File Offset: 0x00000854
		public virtual TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000195FF File Offset: 0x000177FF
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00019608 File Offset: 0x00017808
		public new virtual TypeDefinition Resolve()
		{
			ModuleDefinition moduleDefinition = this.Module;
			if (moduleDefinition == null)
			{
				throw new NotSupportedException();
			}
			return moduleDefinition.Resolve(this);
		}

		// Token: 0x040002E8 RID: 744
		private string @namespace;

		// Token: 0x040002E9 RID: 745
		private bool value_type;

		// Token: 0x040002EA RID: 746
		internal IMetadataScope scope;

		// Token: 0x040002EB RID: 747
		internal ModuleDefinition module;

		// Token: 0x040002EC RID: 748
		internal ElementType etype;

		// Token: 0x040002ED RID: 749
		private string fullname;

		// Token: 0x040002EE RID: 750
		protected Collection<GenericParameter> generic_parameters;
	}
}
