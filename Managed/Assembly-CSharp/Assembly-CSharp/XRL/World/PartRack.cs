using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x0200095F RID: 2399
	public class PartRack : Rack<IPart>
	{
		// Token: 0x06006306 RID: 25350 RVA: 0x001E7029 File Offset: 0x001E5229
		public PartRack()
		{
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x001E7031 File Offset: 0x001E5231
		public PartRack(int Capacity)
			: base(Capacity)
		{
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x001E703C File Offset: 0x001E523C
		public void FinalizeRead(GameObject Basis, SerializationReader Reader)
		{
			IPart[] array = this.Items;
			int i = 0;
			int num = this.Length;
			while (i < num)
			{
				IPart part = array[i];
				part.ApplyRegistrar(Basis, false);
				part.FinalizeRead(Reader);
				if (num != this.Length)
				{
					num = this.Length;
					array = this.Items;
					if (i < num && array[i] != part)
					{
						i--;
					}
				}
				i++;
			}
			if (Reader.ExtensionHash != ComponentReflection.ExtensionHash)
			{
				int j = 0;
				int num2 = this.Length;
				while (j < num2)
				{
					IPart part2 = array[j];
					ComponentReflection.ExtensionRecord extensionRecord;
					if (ComponentReflection.PartExtensions.TryGetValue(part2.GetType(), out extensionRecord))
					{
						extensionRecord.FinalizeRead(Basis, part2);
						if (num2 != this.Length)
						{
							num2 = this.Length;
							array = this.Items;
							while (part2 != array[j] && j < num2)
							{
								j++;
							}
						}
					}
					j++;
				}
			}
		}
	}
}
