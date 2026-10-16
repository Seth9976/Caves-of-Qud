using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x02000947 RID: 2375
	public class EffectRack : Rack<Effect>
	{
		// Token: 0x06005BF0 RID: 23536 RVA: 0x001BCDAF File Offset: 0x001BAFAF
		public EffectRack()
		{
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x001BCDB7 File Offset: 0x001BAFB7
		public EffectRack(int Capacity)
			: base(Capacity)
		{
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x001BCDC0 File Offset: 0x001BAFC0
		public void FinalizeRead(GameObject Basis, SerializationReader Reader)
		{
			Effect[] array = this.Items;
			int i = 0;
			int num = this.Length;
			while (i < num)
			{
				Effect effect = array[i];
				effect.ApplyRegistrar(Basis, false);
				effect.FinalizeRead(Reader);
				if (num != this.Length)
				{
					num = this.Length;
					array = this.Items;
					if (i < num && array[i] != effect)
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
					Effect effect2 = array[j];
					ComponentReflection.ExtensionRecord extensionRecord;
					if (ComponentReflection.EffectExtensions.TryGetValue(effect2.GetType(), out extensionRecord))
					{
						extensionRecord.FinalizeRead(Basis, effect2);
						if (num2 != this.Length)
						{
							num2 = this.Length;
							array = this.Items;
							while (effect2 != array[j] && j < num2)
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
