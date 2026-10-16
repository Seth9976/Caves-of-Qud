using System;
using System.Diagnostics;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200001F RID: 31
	[DebuggerDisplay("{AttributeType}")]
	public sealed class CustomAttribute : ICustomAttribute
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004518 File Offset: 0x00002718
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004520 File Offset: 0x00002720
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004529 File Offset: 0x00002729
		public TypeReference AttributeType
		{
			get
			{
				return this.constructor.DeclaringType;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004536 File Offset: 0x00002736
		public bool IsResolved
		{
			get
			{
				return this.resolved;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000453E File Offset: 0x0000273E
		public bool HasConstructorArguments
		{
			get
			{
				this.Resolve();
				return !this.arguments.IsNullOrEmpty<CustomAttributeArgument>();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004554 File Offset: 0x00002754
		public Collection<CustomAttributeArgument> ConstructorArguments
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeArgument> collection;
				if ((collection = this.arguments) == null)
				{
					collection = (this.arguments = new Collection<CustomAttributeArgument>());
				}
				return collection;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000457F File Offset: 0x0000277F
		public bool HasFields
		{
			get
			{
				this.Resolve();
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004598 File Offset: 0x00002798
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeNamedArgument> collection;
				if ((collection = this.fields) == null)
				{
					collection = (this.fields = new Collection<CustomAttributeNamedArgument>());
				}
				return collection;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000045C3 File Offset: 0x000027C3
		public bool HasProperties
		{
			get
			{
				this.Resolve();
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000045DC File Offset: 0x000027DC
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				this.Resolve();
				Collection<CustomAttributeNamedArgument> collection;
				if ((collection = this.properties) == null)
				{
					collection = (this.properties = new Collection<CustomAttributeNamedArgument>());
				}
				return collection;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004607 File Offset: 0x00002807
		internal bool HasImage
		{
			get
			{
				return this.constructor != null && this.constructor.HasImage;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000461E File Offset: 0x0000281E
		internal ModuleDefinition Module
		{
			get
			{
				return this.constructor.Module;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000462B File Offset: 0x0000282B
		internal CustomAttribute(uint signature, MethodReference constructor)
		{
			this.signature = signature;
			this.constructor = constructor;
			this.resolved = false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004648 File Offset: 0x00002848
		public CustomAttribute(MethodReference constructor)
		{
			this.constructor = constructor;
			this.resolved = true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000465E File Offset: 0x0000285E
		public CustomAttribute(MethodReference constructor, byte[] blob)
		{
			this.constructor = constructor;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000467C File Offset: 0x0000287C
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

		// Token: 0x0600010E RID: 270 RVA: 0x000046D7 File Offset: 0x000028D7
		private void Resolve()
		{
			if (!this.resolved && this.HasImage)
			{
				this.Module.Read<CustomAttribute, CustomAttribute>(this, delegate(CustomAttribute attribute, MetadataReader reader)
				{
					CustomAttribute customAttribute;
					try
					{
						reader.ReadCustomAttributeSignature(attribute);
						this.resolved = true;
						customAttribute = this;
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
						customAttribute = this;
					}
					return customAttribute;
				});
			}
		}

		// Token: 0x04000033 RID: 51
		internal CustomAttributeValueProjection projection;

		// Token: 0x04000034 RID: 52
		internal readonly uint signature;

		// Token: 0x04000035 RID: 53
		internal bool resolved;

		// Token: 0x04000036 RID: 54
		private MethodReference constructor;

		// Token: 0x04000037 RID: 55
		private byte[] blob;

		// Token: 0x04000038 RID: 56
		internal Collection<CustomAttributeArgument> arguments;

		// Token: 0x04000039 RID: 57
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x0400003A RID: 58
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
