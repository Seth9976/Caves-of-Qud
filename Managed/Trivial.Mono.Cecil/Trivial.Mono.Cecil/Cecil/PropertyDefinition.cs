using System;
using System.Text;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A2 RID: 162
	public sealed class PropertyDefinition : PropertyReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00014BE6 File Offset: 0x00012DE6
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00014BEE File Offset: 0x00012DEE
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00014BF8 File Offset: 0x00012DF8
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00014C47 File Offset: 0x00012E47
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00014C55 File Offset: 0x00012E55
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00014C7A File Offset: 0x00012E7A
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00014C98 File Offset: 0x00012E98
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00014CB5 File Offset: 0x00012EB5
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

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00014CBE File Offset: 0x00012EBE
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00014CDB File Offset: 0x00012EDB
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00014CE4 File Offset: 0x00012EE4
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00014D14 File Offset: 0x00012F14
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
				return this.other_methods = new Collection<MethodDefinition>();
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00014D54 File Offset: 0x00012F54
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

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00014DA7 File Offset: 0x00012FA7
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00014DDE File Offset: 0x00012FDE
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00014E02 File Offset: 0x00013002
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00014E12 File Offset: 0x00013012
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00014E24 File Offset: 0x00013024
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00014E2D File Offset: 0x0001302D
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00014E3F File Offset: 0x0001303F
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00014E58 File Offset: 0x00013058
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00014E6A File Offset: 0x0001306A
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00014E83 File Offset: 0x00013083
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00014E95 File Offset: 0x00013095
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00004E65 File Offset: 0x00003065
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00014EB0 File Offset: 0x000130B0
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

		// Token: 0x060006D2 RID: 1746 RVA: 0x00014F48 File Offset: 0x00013148
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

		// Token: 0x060006D3 RID: 1747 RVA: 0x00014F8F File Offset: 0x0001318F
		public PropertyDefinition(string name, PropertyAttributes attributes, TypeReference propertyType)
			: base(name, propertyType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Property);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00014FBC File Offset: 0x000131BC
		private void InitializeMethods()
		{
			ModuleDefinition module = this.Module;
			if (module != null)
			{
				object syncRoot = module.SyncRoot;
				lock (syncRoot)
				{
					if (this.get_method == null && this.set_method == null && module.HasImage())
					{
						module.Read<PropertyDefinition, PropertyDefinition>(this, (PropertyDefinition property, MetadataReader reader) => reader.ReadMethods(property));
					}
				}
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00002654 File Offset: 0x00000854
		public override PropertyDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000238 RID: 568
		private bool? has_this;

		// Token: 0x04000239 RID: 569
		private ushort attributes;

		// Token: 0x0400023A RID: 570
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x0400023B RID: 571
		internal MethodDefinition get_method;

		// Token: 0x0400023C RID: 572
		internal MethodDefinition set_method;

		// Token: 0x0400023D RID: 573
		internal Collection<MethodDefinition> other_methods;

		// Token: 0x0400023E RID: 574
		private object constant = Mixin.NotResolved;
	}
}
