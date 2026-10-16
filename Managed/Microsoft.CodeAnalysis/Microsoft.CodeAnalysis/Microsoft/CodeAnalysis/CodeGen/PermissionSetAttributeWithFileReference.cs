using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D5 RID: 1237
	[NullableContext(1)]
	[Nullable(0)]
	internal class PermissionSetAttributeWithFileReference : ICustomAttribute
	{
		// Token: 0x060031BB RID: 12731 RVA: 0x0008E08E File Offset: 0x0008C28E
		public PermissionSetAttributeWithFileReference(ICustomAttribute sourceAttribute, string resolvedPermissionSetFilePath)
		{
			this._sourceAttribute = sourceAttribute;
			this._resolvedPermissionSetFilePath = resolvedPermissionSetFilePath;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x0008E0A4 File Offset: 0x0008C2A4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IMetadataExpression> GetArguments(EmitContext context)
		{
			return this._sourceAttribute.GetArguments(context);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x0008E0B2 File Offset: 0x0008C2B2
		public IMethodReference Constructor(EmitContext context, bool reportDiagnostics)
		{
			return this._sourceAttribute.Constructor(context, reportDiagnostics);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x0008E0C4 File Offset: 0x0008C2C4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IMetadataNamedArgument> GetNamedArguments(EmitContext context)
		{
			ITypeReference platformType = context.Module.GetPlatformType(PlatformType.SystemString, context);
			XmlReferenceResolver xmlReferenceResolver = context.Module.CommonCompilation.Options.XmlReferenceResolver;
			string text;
			try
			{
				using (Stream stream = xmlReferenceResolver.OpenReadChecked(this._resolvedPermissionSetFilePath))
				{
					text = PermissionSetAttributeWithFileReference.ConvertToHex(stream);
				}
			}
			catch (IOException ex)
			{
				throw new PermissionSetFileReadException(ex.Message, this._resolvedPermissionSetFilePath);
			}
			return ImmutableArray.Create<IMetadataNamedArgument>(new PermissionSetAttributeWithFileReference.HexPropertyMetadataNamedArgument(platformType, new MetadataConstant(platformType, text)));
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x0008E15C File Offset: 0x0008C35C
		internal static string ConvertToHex(Stream stream)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			int num;
			while ((num = stream.ReadByte()) >= 0)
			{
				builder.Append(PermissionSetAttributeWithFileReference.ConvertHexToChar((num >> 4) & 15));
				builder.Append(PermissionSetAttributeWithFileReference.ConvertHexToChar(num & 15));
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x0008E1AB File Offset: 0x0008C3AB
		private static char ConvertHexToChar(int b)
		{
			return (char)((b < 10) ? (48 + b) : (97 + b - 10));
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x0008E1C0 File Offset: 0x0008C3C0
		public int ArgumentCount
		{
			get
			{
				return this._sourceAttribute.ArgumentCount;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x0008E1CD File Offset: 0x0008C3CD
		public ushort NamedArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
		public ITypeReference GetType(EmitContext context)
		{
			return this._sourceAttribute.GetType(context);
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x0008E1DE File Offset: 0x0008C3DE
		public bool AllowMultiple
		{
			get
			{
				return this._sourceAttribute.AllowMultiple;
			}
		}

		// Token: 0x0400133C RID: 4924
		private readonly ICustomAttribute _sourceAttribute;

		// Token: 0x0400133D RID: 4925
		private readonly string _resolvedPermissionSetFilePath;

		// Token: 0x0400133E RID: 4926
		internal const string FilePropertyName = "File";

		// Token: 0x0400133F RID: 4927
		internal const string HexPropertyName = "Hex";

		// Token: 0x0200077C RID: 1916
		[Nullable(0)]
		private struct HexPropertyMetadataNamedArgument : IMetadataNamedArgument, IMetadataExpression
		{
			// Token: 0x06003ECB RID: 16075 RVA: 0x000B0CE9 File Offset: 0x000AEEE9
			public HexPropertyMetadataNamedArgument(ITypeReference type, IMetadataExpression value)
			{
				this._type = type;
				this._value = value;
			}

			// Token: 0x17001159 RID: 4441
			// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000B0CF9 File Offset: 0x000AEEF9
			public string ArgumentName
			{
				get
				{
					return "Hex";
				}
			}

			// Token: 0x1700115A RID: 4442
			// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000B0D00 File Offset: 0x000AEF00
			public IMetadataExpression ArgumentValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x1700115B RID: 4443
			// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000B0D08 File Offset: 0x000AEF08
			public bool IsField
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700115C RID: 4444
			// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000B0D0B File Offset: 0x000AEF0B
			ITypeReference IMetadataExpression.Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06003ED0 RID: 16080 RVA: 0x000B0D13 File Offset: 0x000AEF13
			void IMetadataExpression.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x04001D74 RID: 7540
			private readonly ITypeReference _type;

			// Token: 0x04001D75 RID: 7541
			private readonly IMetadataExpression _value;
		}
	}
}
