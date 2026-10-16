using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace XRL
{
	// Token: 0x02000526 RID: 1318
	public class ModSettings
	{
		/// <summary>
		/// Compute the hash value for the specified file records' name and size.
		/// </summary>
		/// <param name="Root">
		/// The root mod directory to hash file paths relative to.
		/// I.e. same hash result irrespective of what the mod folder's system location is.
		/// </param>
		/// <returns>A hex string of the computed hash code.</returns>
		// Token: 0x06003884 RID: 14468 RVA: 0x00115990 File Offset: 0x00113B90
		public string CalcFilesHash(IReadOnlyList<ModFile> Files, string Root)
		{
			string text;
			using (SHA1 sha = SHA1.Create())
			{
				int i = 0;
				int count = Files.Count;
				while (i < count)
				{
					ModFile modFile = Files[i];
					if (this.IsHashFile(modFile))
					{
						byte[] array = Encoding.UTF8.GetBytes(modFile.FullName.Replace(Root, ""));
						sha.TransformBlock(array, 0, array.Length, array, 0);
						array = BitConverter.GetBytes(modFile.Size);
						sha.TransformBlock(array, 0, array.Length, array, 0);
					}
					i++;
				}
				sha.TransformFinalBlock(new byte[0], 0, 0);
				text = string.Concat(sha.Hash.Select<byte, string>((byte x) => x.ToString("X2")));
			}
			return text;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x00115A74 File Offset: 0x00113C74
		public bool IsHashFile(ModFile File)
		{
			return File.Type == ModFileType.CSharp || File.Type == ModFileType.XML;
		}

		/// <summary>
		/// Compute the hash value for the specified files' contents.
		/// </summary>
		/// <returns>A hex string of the computed hash code.</returns>
		// Token: 0x06003886 RID: 14470 RVA: 0x00115A8C File Offset: 0x00113C8C
		public string CalcSourceHash(IReadOnlyList<ModFile> Files)
		{
			string text;
			using (SHA1 sha = SHA1.Create())
			{
				byte[] array = new byte[8192];
				int i = 0;
				int count = Files.Count;
				while (i < count)
				{
					ModFile modFile = Files[i];
					if (this.IsHashFile(modFile))
					{
						using (FileStream fileStream = new FileStream(modFile.OriginalName, FileMode.Open))
						{
							int num;
							do
							{
								num = fileStream.Read(array, 0, 8192);
								sha.TransformBlock(array, 0, num, array, 0);
							}
							while (num > 0);
						}
					}
					i++;
				}
				sha.TransformFinalBlock(array, 0, 0);
				text = string.Concat(sha.Hash.Select<byte, string>((byte x) => x.ToString("X2")));
			}
			return text;
		}

		/// <summary>A copy of the mod's display title, for easier reading of ModSettings.json by humans.</summary>
		// Token: 0x04001C76 RID: 7286
		public string Title;

		/// <value><c>false</c> if the mod has been explicitly disabled by the user; otherwise, <c>true</c>.</value>
		// Token: 0x04001C77 RID: 7287
		public bool Enabled = true;

		/// <summary>The last approved hash of all file records within the mod.</summary>
		// Token: 0x04001C78 RID: 7288
		public string FilesHash;

		/// <summary>The last approved hash of all script content within the mod.</summary>
		// Token: 0x04001C79 RID: 7289
		public string SourceHash;

		/// <summary>The last version the user was prompted with, will not prompt again until a new version is available.</summary>
		// Token: 0x04001C7A RID: 7290
		public Version? UpdateVersion;

		/// <value><c>true</c> if the mod has failed to compile; otherwise, <c>false</c>.</value>
		// Token: 0x04001C7B RID: 7291
		[JsonIgnore]
		public bool Failed;

		/// <summary>A list of errors attributed to this mod.</summary>
		// Token: 0x04001C7C RID: 7292
		[JsonIgnore]
		public List<string> Errors = new List<string>();

		/// <summary>A list of warnings attributed to this mod.</summary>
		// Token: 0x04001C7D RID: 7293
		[JsonIgnore]
		public List<string> Warnings = new List<string>();
	}
}
