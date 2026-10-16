using System;
using System.Text;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200025A RID: 602
	public sealed class PropertyDefinition : PropertyReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public PropertyAttributes Attributes
		{
			get
			{
				return (PropertyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0002DC27 File Offset: 0x0002BE27
		public bool HasThis
		{
			get
			{
				if (this.has_this != null)
				{
					return this.has_this.Value;
				}
				if (this.GetMethod != null)
				{
					return this.get_method.HasThis;
				}
				return this.SetMethod != null && this.set_method.HasThis;
			}
			set
			{
				this.has_this = new bool?(value);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0002DC35 File Offset: 0x0002BE35
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0002DC5A File Offset: 0x0002BE5A
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0002DC78 File Offset: 0x0002BE78
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0002DC95 File Offset: 0x0002BE95
		public MethodDefinition GetMethod
		{
			get
			{
				if (this.get_method != null)
				{
					return this.get_method;
				}
				this.InitializeMethods();
				return this.get_method;
			}
			set
			{
				this.get_method = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002DC9E File Offset: 0x0002BE9E
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0002DCBB File Offset: 0x0002BEBB
		public MethodDefinition SetMethod
		{
			get
			{
				if (this.set_method != null)
				{
					return this.set_method;
				}
				this.InitializeMethods();
				return this.set_method;
			}
			set
			{
				this.set_method = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0002DCC4 File Offset: 0x0002BEC4
		public bool HasOtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods.Count > 0;
				}
				this.InitializeMethods();
				return !this.other_methods.IsNullOrEmpty<MethodDefinition>();
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002DCF1 File Offset: 0x0002BEF1
		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				this.InitializeMethods();
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				Interlocked.CompareExchange<Collection<MethodDefinition>>(ref this.other_methods, new Collection<MethodDefinition>(), null);
				return this.other_methods;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0002DD30 File Offset: 0x0002BF30
		public bool HasParameters
		{
			get
			{
				this.InitializeMethods();
				if (this.get_method != null)
				{
					return this.get_method.HasParameters;
				}
				return this.set_method != null && this.set_method.HasParameters && this.set_method.Parameters.Count > 1;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002DD83 File Offset: 0x0002BF83
		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				this.InitializeMethods();
				if (this.get_method != null)
				{
					return PropertyDefinition.MirrorParameters(this.get_method, 0);
				}
				if (this.set_method != null)
				{
					return PropertyDefinition.MirrorParameters(this.set_method, 1);
				}
				return new Collection<ParameterDefinition>();
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002DDBC File Offset: 0x0002BFBC
		private static Collection<ParameterDefinition> MirrorParameters(MethodDefinition method, int bound)
		{
			Collection<ParameterDefinition> collection = new Collection<ParameterDefinition>();
			if (!method.HasParameters)
			{
				return collection;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			int num = parameters.Count - bound;
			for (int i = 0; i < num; i++)
			{
				collection.Add(parameters[i]);
			}
			return collection;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002DE03 File Offset: 0x0002C003
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x0002DE27 File Offset: 0x0002C027
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0002DE37 File Offset: 0x0002C037
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x0002DE49 File Offset: 0x0002C049
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0002DE52 File Offset: 0x0002C052
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0002DE64 File Offset: 0x0002C064
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0002DE7D File Offset: 0x0002C07D
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x0002DE8F File Offset: 0x0002C08F
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0002DEBA File Offset: 0x0002C0BA
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00026BD0 File Offset: 0x00024DD0
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00026BDD File Offset: 0x00024DDD
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002DED4 File Offset: 0x0002C0D4
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.PropertyType.ToString());
				stringBuilder.Append(' ');
				stringBuilder.Append(base.MemberFullName());
				stringBuilder.Append('(');
				if (this.HasParameters)
				{
					Collection<ParameterDefinition> parameters = this.Parameters;
					for (int i = 0; i < parameters.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(parameters[i].ParameterType.FullName);
					}
				}
				stringBuilder.Append(')');
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002DF6C File Offset: 0x0002C16C
		public PropertyDefinition(string name, PropertyAttributes attributes, TypeReference propertyType)
			: base(name, propertyType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Property);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002DF98 File Offset: 0x0002C198
		private void InitializeMethods()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				return;
			}
			object syncRoot = module.SyncRoot;
			lock (syncRoot)
			{
				if (this.get_method == null && this.set_method == null)
				{
					if (module.HasImage())
					{
						module.Read<PropertyDefinition>(this, delegate(PropertyDefinition property, MetadataReader reader)
						{
							reader.ReadMethods(property);
						});
					}
				}
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00018380 File Offset: 0x00016580
		public override PropertyDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040004B6 RID: 1206
		private bool? has_this;

		// Token: 0x040004B7 RID: 1207
		private ushort attributes;

		// Token: 0x040004B8 RID: 1208
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040004B9 RID: 1209
		internal MethodDefinition get_method;

		// Token: 0x040004BA RID: 1210
		internal MethodDefinition set_method;

		// Token: 0x040004BB RID: 1211
		internal Collection<MethodDefinition> other_methods;

		// Token: 0x040004BC RID: 1212
		private object constant = Mixin.NotResolved;
	}
}
