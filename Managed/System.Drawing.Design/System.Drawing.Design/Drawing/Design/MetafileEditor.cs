using System;
using System.Drawing.Imaging;
using System.IO;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> that can perform default file searching for metafile (.emf) files.</summary>
	// Token: 0x02000018 RID: 24
	public class MetafileEditor : ImageEditor
	{
		/// <returns>A set of file extensions used to filter the file list.</returns>
		// Token: 0x06000050 RID: 80 RVA: 0x0000359E File Offset: 0x0000179E
		protected override string[] GetExtensions()
		{
			return new string[] { "*.emf", "*.wmf" };
		}

		/// <returns>The description for the default file-list filter.</returns>
		// Token: 0x06000051 RID: 81 RVA: 0x000035B6 File Offset: 0x000017B6
		protected override string GetFileDialogDescription()
		{
			return Locale.GetText("All metafile files");
		}

		/// <returns>The <see cref="T:System.Drawing.Image" /> that has been loaded.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the image to load.</param>
		// Token: 0x06000052 RID: 82 RVA: 0x000035C2 File Offset: 0x000017C2
		protected override Image LoadFromStream(Stream stream)
		{
			return new Metafile(stream);
		}
	}
}
