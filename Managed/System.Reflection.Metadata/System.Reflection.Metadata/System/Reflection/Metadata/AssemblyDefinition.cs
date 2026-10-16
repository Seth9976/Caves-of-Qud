using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct AssemblyDefinition
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000949F File Offset: 0x0000769F
		public AssemblyName GetAssemblyName()
		{
			return this._reader.GetAssemblyName(this.Name, this.Version, this.Culture, this.PublicKey, this.HashAlgorithm, this.Flags);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000094D0 File Offset: 0x000076D0
		internal AssemblyDefinition(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000094D9 File Offset: 0x000076D9
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this._reader.AssemblyTable.GetHashAlgorithm();
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000094EB File Offset: 0x000076EB
		public Version Version
		{
			get
			{
				return this._reader.AssemblyTable.GetVersion();
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000094FD File Offset: 0x000076FD
		public AssemblyFlags Flags
		{
			get
			{
				return this._reader.AssemblyTable.GetFlags();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000950F File Offset: 0x0000770F
		public StringHandle Name
		{
			get
			{
				return this._reader.AssemblyTable.GetName();
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00009521 File Offset: 0x00007721
		public StringHandle Culture
		{
			get
			{
				return this._reader.AssemblyTable.GetCulture();
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00009533 File Offset: 0x00007733
		public BlobHandle PublicKey
		{
			get
			{
				return this._reader.AssemblyTable.GetPublicKey();
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009545 File Offset: 0x00007745
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, EntityHandle.AssemblyDefinition);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000955C File Offset: 0x0000775C
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, EntityHandle.AssemblyDefinition);
		}

		// Token: 0x040002B8 RID: 696
		private readonly MetadataReader _reader;
	}
}
