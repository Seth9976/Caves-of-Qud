using System;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides an extension method to access the TypeDefinitionId column of the ExportedType table.</summary>
	// Token: 0x0200010B RID: 267
	public static class ExportedTypeExtensions
	{
		/// <summary>Gets a hint at the likely row number of the target type in the TypeDef table of its module.
		///  If the namespaces and names do not match, resolution falls back to a full search of the target TypeDef table. Ignored and should be zero if <see cref="P:System.Reflection.Metadata.ExportedType.IsForwarder" /> is <see langword="true" />.</summary>
		/// <param name="exportedType" />
		// Token: 0x060009E7 RID: 2535 RVA: 0x0001C598 File Offset: 0x0001A798
		public static int GetTypeDefinitionId(this ExportedType exportedType)
		{
			return exportedType.reader.ExportedTypeTable.GetTypeDefId(exportedType.rowId);
		}
	}
}
