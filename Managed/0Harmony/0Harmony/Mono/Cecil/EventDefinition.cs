using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001FF RID: 511
	public sealed class EventDefinition : EventReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00026A50 File Offset: 0x00024C50
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00026A58 File Offset: 0x00024C58
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00026A61 File Offset: 0x00024C61
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00026A7E File Offset: 0x00024C7E
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00026A87 File Offset: 0x00024C87
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00026AA4 File Offset: 0x00024CA4
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00026AAD File Offset: 0x00024CAD
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00026ACA File Offset: 0x00024CCA
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00026AD3 File Offset: 0x00024CD3
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00026B00 File Offset: 0x00024D00
		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				this.InitializeMethods();
				if (this.other_methods == null)
				{
					Interlocked.CompareExchange<Collection<MethodDefinition>>(ref this.other_methods, new Collection<MethodDefinition>(), null);
				}
				return this.other_methods;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00026B37 File Offset: 0x00024D37
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00026B5C File Offset: 0x00024D5C
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00026B7A File Offset: 0x00024D7A
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00026B8C File Offset: 0x00024D8C
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00026BA5 File Offset: 0x00024DA5
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00026BB7 File Offset: 0x00024DB7
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00026BD0 File Offset: 0x00024DD0
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00026BDD File Offset: 0x00024DDD
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00026BE6 File Offset: 0x00024DE6
		public EventDefinition(string name, EventAttributes attributes, TypeReference eventType)
			: base(name, eventType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Event);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00026C08 File Offset: 0x00024E08
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
				if (this.add_method == null && this.invoke_method == null && this.remove_method == null)
				{
					if (module.HasImage())
					{
						module.Read<EventDefinition>(this, delegate(EventDefinition @event, MetadataReader reader)
						{
							reader.ReadMethods(@event);
						});
					}
				}
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00018380 File Offset: 0x00016580
		public override EventDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040002FA RID: 762
		private ushort attributes;

		// Token: 0x040002FB RID: 763
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040002FC RID: 764
		internal MethodDefinition add_method;

		// Token: 0x040002FD RID: 765
		internal MethodDefinition invoke_method;

		// Token: 0x040002FE RID: 766
		internal MethodDefinition remove_method;

		// Token: 0x040002FF RID: 767
		internal Collection<MethodDefinition> other_methods;
	}
}
