using System;
using Platform.IO;

namespace XRL
{
	// Token: 0x0200051E RID: 1310
	public class ModFile
	{
		// Token: 0x0600381C RID: 14364 RVA: 0x00111DE4 File Offset: 0x0010FFE4
		public ModFile(ModInfo Mod, FileInfo File)
		{
			this.Mod = Mod;
			this.OriginalName = File.FullName;
			this.RelativeName = Path.GetRelativePath(Mod.Path, this.OriginalName).ToLowerInvariant();
			this.FullName = this.OriginalName.ToLowerInvariant();
			this.Name = Path.GetFileName(this.FullName);
			this.Extension = Path.GetExtension(this.FullName);
			this.Size = File.Length;
			string extension = this.Extension;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(extension);
			ModFileType modFileType;
			if (num <= 1861662575U)
			{
				if (num <= 640503538U)
				{
					if (num != 592705037U)
					{
						if (num == 640503538U)
						{
							if (extension == ".rpm")
							{
								modFileType = ModFileType.Map;
								goto IL_01E5;
							}
						}
					}
					else if (extension == ".mp3")
					{
						modFileType = ModFileType.Audio;
						goto IL_01E5;
					}
				}
				else if (num != 1128223456U)
				{
					if (num != 1426315967U)
					{
						if (num == 1861662575U)
						{
							if (extension == ".json")
							{
								modFileType = ModFileType.JSON;
								goto IL_01E5;
							}
						}
					}
					else if (extension == ".cs")
					{
						modFileType = ModFileType.CSharp;
						goto IL_01E5;
					}
				}
				else if (extension == ".png")
				{
					modFileType = ModFileType.Sprite;
					goto IL_01E5;
				}
			}
			else if (num <= 2561755776U)
			{
				if (num != 2194571213U)
				{
					if (num == 2561755776U)
					{
						if (extension == ".ogg")
						{
							modFileType = ModFileType.Audio;
							goto IL_01E5;
						}
					}
				}
				else if (extension == ".wav")
				{
					modFileType = ModFileType.Audio;
					goto IL_01E5;
				}
			}
			else if (num != 2819025139U)
			{
				if (num != 3534858618U)
				{
					if (num == 3960394863U)
					{
						if (extension == ".dll")
						{
							modFileType = ModFileType.Assembly;
							goto IL_01E5;
						}
					}
				}
				else if (extension == ".xml")
				{
					modFileType = ModFileType.XML;
					goto IL_01E5;
				}
			}
			else if (extension == ".aiff")
			{
				modFileType = ModFileType.Audio;
				goto IL_01E5;
			}
			modFileType = ModFileType.Unknown;
			IL_01E5:
			this.Type = modFileType;
		}

		// Token: 0x04001C22 RID: 7202
		public ModInfo Mod;

		// Token: 0x04001C23 RID: 7203
		public string Name;

		// Token: 0x04001C24 RID: 7204
		public string FullName;

		// Token: 0x04001C25 RID: 7205
		public string RelativeName;

		// Token: 0x04001C26 RID: 7206
		public string OriginalName;

		// Token: 0x04001C27 RID: 7207
		public string Extension;

		// Token: 0x04001C28 RID: 7208
		public long Size;

		// Token: 0x04001C29 RID: 7209
		public ModFileType Type;
	}
}
