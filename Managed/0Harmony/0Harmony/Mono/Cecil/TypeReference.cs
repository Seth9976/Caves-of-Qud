using System;
using System.Threading;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000276 RID: 630
	public class TypeReference : MemberReference, IGenericParameterProvider, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002AA35 File Offset: 0x00028C35
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0002FD38 File Offset: 0x0002DF38
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002FD68 File Offset: 0x0002DF68
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0002FD70 File Offset: 0x0002DF70
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0002FDB4 File Offset: 0x0002DFB4
		public override ModuleDefinition Module
		{
			get
			{
				if (this.module != null)
				{
					return this.module;
				}
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Module;
				}
				return null;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0002FDE2 File Offset: 0x0002DFE2
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x0002736E File Offset: 0x0002556E
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00018380 File Offset: 0x00016580
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00027A5D File Offset: 0x00025C5D
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00018318 File Offset: 0x00016518
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Type;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0002FDEF File Offset: 0x0002DFEF
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002FDFF File Offset: 0x0002DFFF
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters == null)
				{
					Interlocked.CompareExchange<Collection<GenericParameter>>(ref this.generic_parameters, new GenericParameterCollection(this), null);
				}
				return this.generic_parameters;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002FE24 File Offset: 0x0002E024
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0002FE48 File Offset: 0x0002E048
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0002FEA6 File Offset: 0x0002E0A6
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002FEB1 File Offset: 0x0002E0B1
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0002FEB9 File Offset: 0x0002E0B9
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
		public override string FullName
		{
			get
			{
				if (this.fullname != null)
				{
					return this.fullname;
				}
				string text = this.TypeFullName();
				if (this.IsNested)
				{
					text = this.DeclaringType.FullName + "/" + text;
				}
				Interlocked.CompareExchange<string>(ref this.fullname, text, null);
				return this.fullname;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsByReference
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsSentinel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsRequiredModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsOptionalModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsFunctionPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0002FF3A File Offset: 0x0002E13A
		public virtual bool IsPrimitive
		{
			get
			{
				return this.etype.IsPrimitive();
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0002FF47 File Offset: 0x0002E147
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

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002FF66 File Offset: 0x0002E166
		protected TypeReference(string @namespace, string name)
			: base(name)
		{
			this.@namespace = @namespace ?? string.Empty;
			this.token = new MetadataToken(TokenType.TypeRef, 0);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002FF90 File Offset: 0x0002E190
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
			: this(@namespace, name)
		{
			this.module = module;
			this.scope = scope;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002FFA9 File Offset: 0x0002E1A9
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope, bool valueType)
			: this(@namespace, name, module, scope)
		{
			this.value_type = valueType;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002FFBE File Offset: 0x0002E1BE
		protected virtual void ClearFullName()
		{
			this.fullname = null;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00018380 File Offset: 0x00016580
		public virtual TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002FFC7 File Offset: 0x0002E1C7
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002FFCF File Offset: 0x0002E1CF
		public new virtual TypeDefinition Resolve()
		{
			ModuleDefinition moduleDefinition = this.Module;
			if (moduleDefinition == null)
			{
				throw new NotSupportedException();
			}
			return moduleDefinition.Resolve(this);
		}

		// Token: 0x04000578 RID: 1400
		private string @namespace;

		// Token: 0x04000579 RID: 1401
		private bool value_type;

		// Token: 0x0400057A RID: 1402
		internal IMetadataScope scope;

		// Token: 0x0400057B RID: 1403
		internal ModuleDefinition module;

		// Token: 0x0400057C RID: 1404
		internal ElementType etype;

		// Token: 0x0400057D RID: 1405
		private string fullname;

		// Token: 0x0400057E RID: 1406
		protected Collection<GenericParameter> generic_parameters;
	}
}
