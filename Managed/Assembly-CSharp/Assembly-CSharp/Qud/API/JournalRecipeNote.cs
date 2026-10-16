using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World;
using XRL.World.Skills.Cooking;

namespace Qud.API
{
	// Token: 0x020003FC RID: 1020
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalRecipeNote : IBaseJournalEntry
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x000C5387 File Offset: 0x000C3587
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000C538C File Offset: 0x000C358C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Recipe);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000C5408 File Offset: 0x000C3608
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Recipe = (CookingRecipe)Reader.ReadComposite();
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000C5488 File Offset: 0x000C3688
		public override void Reveal(string LearnedFrom = null, bool Silent = false)
		{
			if (!this.Revealed)
			{
				base.Reveal(LearnedFrom, Silent);
				CookingGameState.instance.knownRecipies.Add(this.Recipe);
				if (!Silent)
				{
					IBaseJournalEntry.DisplayMessage("You learn to cook " + this.Recipe.GetDisplayName() + "!", "sfx_cookingRecipe_learn");
				}
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000C54E1 File Offset: 0x000C36E1
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x000C54E9 File Offset: 0x000C36E9
		[Obsolete]
		public CookingRecipe recipe
		{
			get
			{
				return this.Recipe;
			}
			set
			{
				this.Recipe = value;
			}
		}

		// Token: 0x04001316 RID: 4886
		public CookingRecipe Recipe;
	}
}
