using System;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.  </summary>
	// Token: 0x02000301 RID: 769
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class. </summary>
		// Token: 0x06001887 RID: 6279 RVA: 0x0005F608 File Offset: 0x0005D808
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x06001888 RID: 6280 RVA: 0x0005F610 File Offset: 0x0005D810
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this._fileName = fileName;
			this.ChecksumAlgorithmId = checksumAlgorithmId;
			this.ChecksumData = checksumData;
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005F62D File Offset: 0x0005D82D
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0005F63E File Offset: 0x0005D83E
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005F647 File Offset: 0x0005D847
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0005F64F File Offset: 0x0005D84F
		public Guid ChecksumAlgorithmId { get; set; }

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0005F658 File Offset: 0x0005D858
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0005F660 File Offset: 0x0005D860
		public byte[] ChecksumData { get; set; }

		// Token: 0x04000D92 RID: 3474
		private string _fileName;
	}
}
