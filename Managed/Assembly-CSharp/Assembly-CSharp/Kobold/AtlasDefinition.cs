using System;
using System.Text;
using UnityEngine;

namespace Kobold
{
	// Token: 0x0200035F RID: 863
	public class AtlasDefinition
	{
		// Token: 0x0600202C RID: 8236 RVA: 0x0009DB60 File Offset: 0x0009BD60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new string[]
			{
				"-atlasdef ",
				this.Prefix,
				" x:",
				this.XSize.ToString(),
				" y:",
				this.YSize.ToString(),
				" compression:",
				this.Compression
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0009DBDC File Offset: 0x0009BDDC
		public exAtlas GenerateAtlas()
		{
			exAtlas exAtlas = ScriptableObject.CreateInstance<exAtlas>();
			exAtlas.width = this.XSize;
			exAtlas.height = this.YSize;
			exAtlas.useContourBleed = true;
			exAtlas.usePaddingBleed = true;
			exAtlas.trimElements = false;
			exAtlas.trimThreshold = 1000;
			exAtlas.customPadding = 3;
			return exAtlas;
		}

		// Token: 0x04001065 RID: 4197
		public string Prefix;

		// Token: 0x04001066 RID: 4198
		public int XSize;

		// Token: 0x04001067 RID: 4199
		public int YSize;

		// Token: 0x04001068 RID: 4200
		public string Compression;

		// Token: 0x04001069 RID: 4201
		public bool Trim;
	}
}
