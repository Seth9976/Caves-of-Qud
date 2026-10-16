using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct PermissionSetEncoder
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001A81E File Offset: 0x00018A1E
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600094D RID: 2381 RVA: 0x0001A826 File Offset: 0x00018A26
		public PermissionSetEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <param name="typeName" />
		/// <param name="encodedArguments" />
		// Token: 0x0600094E RID: 2382 RVA: 0x0001A830 File Offset: 0x00018A30
		[NullableContext(0)]
		public PermissionSetEncoder AddPermission([Nullable(1)] string typeName, ImmutableArray<byte> encodedArguments)
		{
			if (typeName == null)
			{
				Throw.ArgumentNull("typeName");
			}
			if (encodedArguments.IsDefault)
			{
				Throw.ArgumentNull("encodedArguments");
			}
			if (encodedArguments.Length > 536870911)
			{
				Throw.BlobTooLarge("encodedArguments");
			}
			this.Builder.WriteSerializedString(typeName);
			this.Builder.WriteCompressedInteger(encodedArguments.Length);
			this.Builder.WriteBytes(encodedArguments);
			return this;
		}

		/// <param name="typeName" />
		/// <param name="encodedArguments" />
		// Token: 0x0600094F RID: 2383 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		public PermissionSetEncoder AddPermission(string typeName, BlobBuilder encodedArguments)
		{
			if (typeName == null)
			{
				Throw.ArgumentNull("typeName");
			}
			if (encodedArguments == null)
			{
				Throw.ArgumentNull("encodedArguments");
			}
			if (encodedArguments.Count > 536870911)
			{
				Throw.BlobTooLarge("encodedArguments");
			}
			this.Builder.WriteSerializedString(typeName);
			this.Builder.WriteCompressedInteger(encodedArguments.Count);
			encodedArguments.WriteContentTo(this.Builder);
			return this;
		}
	}
}
