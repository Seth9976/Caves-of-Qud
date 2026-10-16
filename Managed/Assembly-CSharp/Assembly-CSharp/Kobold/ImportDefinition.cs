using System;
using System.Text;

namespace Kobold
{
	// Token: 0x02000360 RID: 864
	public class ImportDefinition
	{
		// Token: 0x0600202F RID: 8239 RVA: 0x0009DC38 File Offset: 0x0009BE38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Spec = " + this.Spec);
			if (this.DiffuseAtlas != null)
			{
				stringBuilder.Append("\ndiffuse: " + this.DiffuseAtlas.ToString());
			}
			if (this.NormalAtlas != null)
			{
				stringBuilder.Append("\nnormal: " + this.NormalAtlas.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400106A RID: 4202
		public string Spec;

		// Token: 0x0400106B RID: 4203
		public AtlasDefinition DiffuseAtlas;

		// Token: 0x0400106C RID: 4204
		public AtlasDefinition NormalAtlas;
	}
}
