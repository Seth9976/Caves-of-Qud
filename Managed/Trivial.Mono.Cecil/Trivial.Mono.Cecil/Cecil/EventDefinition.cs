using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002D RID: 45
	public sealed class EventDefinition : EventReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004CD0 File Offset: 0x00002ED0
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public EventAttributes Attributes
		{
			get
			{
				return (EventAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004CE1 File Offset: 0x00002EE1
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004CFE File Offset: 0x00002EFE
		public MethodDefinition AddMethod
		{
			get
			{
				if (this.add_method != null)
				{
					return this.add_method;
				}
				this.InitializeMethods();
				return this.add_method;
			}
			set
			{
				this.add_method = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004D07 File Offset: 0x00002F07
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004D24 File Offset: 0x00002F24
		public MethodDefinition InvokeMethod
		{
			get
			{
				if (this.invoke_method != null)
				{
					return this.invoke_method;
				}
				this.InitializeMethods();
				return this.invoke_method;
			}
			set
			{
				this.invoke_method = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004D2D File Offset: 0x00002F2D
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004D4A File Offset: 0x00002F4A
		public MethodDefinition RemoveMethod
		{
			get
			{
				if (this.remove_method != null)
				{
					return this.remove_method;
				}
				this.InitializeMethods();
				return this.remove_method;
			}
			set
			{
				this.remove_method = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004D53 File Offset: 0x00002F53
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004D80 File Offset: 0x00002F80
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004DBF File Offset: 0x00002FBF
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004E02 File Offset: 0x00003002
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00004E14 File Offset: 0x00003014
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004E2D File Offset: 0x0000302D
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00004E3F File Offset: 0x0000303F
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004E65 File Offset: 0x00003065
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004E6E File Offset: 0x0000306E
		public EventDefinition(string name, EventAttributes attributes, TypeReference eventType)
			: base(name, eventType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Event);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E90 File Offset: 0x00003090
		private void InitializeMethods()
		{
			ModuleDefinition module = this.Module;
			if (module != null)
			{
				object syncRoot = module.SyncRoot;
				lock (syncRoot)
				{
					if (this.add_method == null && this.invoke_method == null && this.remove_method == null && module.HasImage())
					{
						module.Read<EventDefinition, EventDefinition>(this, (EventDefinition @event, MetadataReader reader) => reader.ReadMethods(@event));
					}
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00002654 File Offset: 0x00000854
		public override EventDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000054 RID: 84
		private ushort attributes;

		// Token: 0x04000055 RID: 85
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000056 RID: 86
		internal MethodDefinition add_method;

		// Token: 0x04000057 RID: 87
		internal MethodDefinition invoke_method;

		// Token: 0x04000058 RID: 88
		internal MethodDefinition remove_method;

		// Token: 0x04000059 RID: 89
		internal Collection<MethodDefinition> other_methods;
	}
}
