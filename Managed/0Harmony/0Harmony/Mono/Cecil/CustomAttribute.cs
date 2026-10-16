using System;
using System.Diagnostics;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001FA RID: 506
	[DebuggerDisplay("{AttributeType}")]
	public sealed class CustomAttribute : ICustomAttribute
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00026521 File Offset: 0x00024721
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00026529 File Offset: 0x00024729
		public MethodReference Constructor
		{
			get
			{
				return this.constructor;
			}
			set
			{
				this.constructor = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00026532 File Offset: 0x00024732
		public TypeReference AttributeType
		{
			get
			{
				return this.constructor.DeclaringType;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002653F File Offset: 0x0002473F
		public bool IsResolved
		{
			get
			{
				return this.resolved;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00026547 File Offset: 0x00024747
		public bool HasConstructorArguments
		{
			get
			{
				this.Resolve();
				return !this.arguments.IsNullOrEmpty<CustomAttributeArgument>();
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002655D File Offset: 0x0002475D
		public Collection<CustomAttributeArgument> ConstructorArguments
		{
			get
			{
				this.Resolve();
				if (this.arguments == null)
				{
					Interlocked.CompareExchange<Collection<CustomAttributeArgument>>(ref this.arguments, new Collection<CustomAttributeArgument>(), null);
				}
				return this.arguments;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00026585 File Offset: 0x00024785
		public bool HasFields
		{
			get
			{
				this.Resolve();
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002659B File Offset: 0x0002479B
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				this.Resolve();
				if (this.fields == null)
				{
					Interlocked.CompareExchange<Collection<CustomAttributeNamedArgument>>(ref this.fields, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return this.fields;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000265C3 File Offset: 0x000247C3
		public bool HasProperties
		{
			get
			{
				this.Resolve();
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000265D9 File Offset: 0x000247D9
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				this.Resolve();
				if (this.properties == null)
				{
					Interlocked.CompareExchange<Collection<CustomAttributeNamedArgument>>(ref this.properties, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return this.properties;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00026601 File Offset: 0x00024801
		internal bool HasImage
		{
			get
			{
				return this.constructor != null && this.constructor.HasImage;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00026618 File Offset: 0x00024818
		internal ModuleDefinition Module
		{
			get
			{
				return this.constructor.Module;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00026625 File Offset: 0x00024825
		internal CustomAttribute(uint signature, MethodReference constructor)
		{
			this.signature = signature;
			this.constructor = constructor;
			this.resolved = false;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00026642 File Offset: 0x00024842
		public CustomAttribute(MethodReference constructor)
		{
			this.constructor = constructor;
			this.resolved = true;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00026658 File Offset: 0x00024858
		public CustomAttribute(MethodReference constructor, byte[] blob)
		{
			this.constructor = constructor;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00026678 File Offset: 0x00024878
		public byte[] GetBlob()
		{
			if (this.blob != null)
			{
				return this.blob;
			}
			if (!this.HasImage)
			{
				throw new NotSupportedException();
			}
			return this.Module.Read<CustomAttribute, byte[]>(ref this.blob, this, (CustomAttribute attribute, MetadataReader reader) => reader.ReadCustomAttributeBlob(attribute.signature));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000266D4 File Offset: 0x000248D4
		private void Resolve()
		{
			if (this.resolved || !this.HasImage)
			{
				return;
			}
			object syncRoot = this.Module.SyncRoot;
			lock (syncRoot)
			{
				if (!this.resolved)
				{
					this.Module.Read<CustomAttribute>(this, delegate(CustomAttribute attribute, MetadataReader reader)
					{
						try
						{
							reader.ReadCustomAttributeSignature(attribute);
							this.resolved = true;
						}
						catch (ResolutionException)
						{
							if (this.arguments != null)
							{
								this.arguments.Clear();
							}
							if (this.fields != null)
							{
								this.fields.Clear();
							}
							if (this.properties != null)
							{
								this.properties.Clear();
							}
							this.resolved = false;
						}
					});
				}
			}
		}

		// Token: 0x040002E7 RID: 743
		internal CustomAttributeValueProjection projection;

		// Token: 0x040002E8 RID: 744
		internal readonly uint signature;

		// Token: 0x040002E9 RID: 745
		internal bool resolved;

		// Token: 0x040002EA RID: 746
		private MethodReference constructor;

		// Token: 0x040002EB RID: 747
		private byte[] blob;

		// Token: 0x040002EC RID: 748
		internal Collection<CustomAttributeArgument> arguments;

		// Token: 0x040002ED RID: 749
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x040002EE RID: 750
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
