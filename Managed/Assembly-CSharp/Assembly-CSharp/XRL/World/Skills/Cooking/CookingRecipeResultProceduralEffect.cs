using System;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BBC RID: 3004
	[Serializable]
	public class CookingRecipeResultProceduralEffect : ICookingRecipeResult, IComposite
	{
		// Token: 0x0600775F RID: 30559 RVA: 0x002CF013 File Offset: 0x002CD213
		public CookingRecipeResultProceduralEffect()
		{
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x002CF01B File Offset: 0x002CD21B
		public CookingRecipeResultProceduralEffect(ProceduralCookingEffect Effect)
		{
			this.Effect = Effect;
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06007761 RID: 30561 RVA: 0x002CF02A File Offset: 0x002CD22A
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x002CF02D File Offset: 0x002CD22D
		public void Write(SerializationWriter Writer)
		{
			XRL.World.Effect.Save(this.Effect, Writer);
		}

		// Token: 0x06007763 RID: 30563 RVA: 0x002CF03B File Offset: 0x002CD23B
		public void Read(SerializationReader Reader)
		{
			this.Effect = (ProceduralCookingEffect)XRL.World.Effect.Load(null, Reader);
		}

		// Token: 0x06007764 RID: 30564 RVA: 0x002CF04F File Offset: 0x002CD24F
		public string GetCampfireDescription()
		{
			return Campfire.ProcessEffectDescription(this.Effect.GetTemplatedProceduralEffectDescription(), The.Player);
		}

		// Token: 0x06007765 RID: 30565 RVA: 0x002CF068 File Offset: 0x002CD268
		public string apply(GameObject eater)
		{
			ProceduralCookingEffect proceduralCookingEffect = (ProceduralCookingEffect)this.Effect.DeepCopy(eater);
			proceduralCookingEffect.Init(eater);
			eater.ApplyEffect(proceduralCookingEffect, null);
			return proceduralCookingEffect.GetProceduralEffectDescription();
		}

		// Token: 0x040036D5 RID: 14037
		[NonSerialized]
		public ProceduralCookingEffect Effect;
	}
}
