using System;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000104 RID: 260
	internal static class SymUnmanagedWriterFactory
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001912C File Offset: 0x0001732C
		public static SymUnmanagedWriter CreateWriter(ISymWriterMetadataProvider metadataProvider, SymUnmanagedWriterCreationOptions options = SymUnmanagedWriterCreationOptions.Default)
		{
			if (metadataProvider == null)
			{
				throw new ArgumentNullException("metadataProvider");
			}
			string text;
			Exception ex;
			object obj = SymUnmanagedFactory.CreateObject(false, (options & SymUnmanagedWriterCreationOptions.UseAlternativeLoadPath) > SymUnmanagedWriterCreationOptions.Default, (options & SymUnmanagedWriterCreationOptions.UseComRegistry) > SymUnmanagedWriterCreationOptions.Default, out text, out ex);
			if (obj == null)
			{
				if (ex is DllNotFoundException)
				{
					throw ex;
				}
				throw new DllNotFoundException(ex.Message, ex);
			}
			else
			{
				ISymUnmanagedWriter5 symUnmanagedWriter = obj as ISymUnmanagedWriter5;
				if (symUnmanagedWriter == null)
				{
					throw new SymUnmanagedWriterException(new NotSupportedException(), text);
				}
				object obj2 = new SymWriterMetadataAdapter(metadataProvider);
				ComMemoryStream comMemoryStream = new ComMemoryStream(32768);
				try
				{
					if ((options & SymUnmanagedWriterCreationOptions.Deterministic) != SymUnmanagedWriterCreationOptions.Default)
					{
						ISymUnmanagedWriter8 symUnmanagedWriter2 = obj as ISymUnmanagedWriter8;
						if (symUnmanagedWriter2 == null)
						{
							throw new NotSupportedException();
						}
						symUnmanagedWriter2.InitializeDeterministic(obj2, comMemoryStream);
					}
					else
					{
						symUnmanagedWriter.Initialize(obj2, "filename.pdb", comMemoryStream, true);
					}
				}
				catch (Exception ex2)
				{
					throw new SymUnmanagedWriterException(ex2, text);
				}
				return new SymUnmanagedWriterImpl(comMemoryStream, symUnmanagedWriter, text);
			}
		}
	}
}
