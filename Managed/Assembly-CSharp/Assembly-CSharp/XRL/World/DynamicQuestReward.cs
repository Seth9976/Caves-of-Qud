using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Occult.Engine.CodeGeneration;

namespace XRL.World
{
	// Token: 0x0200084D RID: 2125
	[GenerateSerializationPartial]
	[Serializable]
	public class DynamicQuestReward : IComposite
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600543E RID: 21566 RVA: 0x001917F4 File Offset: 0x0018F9F4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x001917F7 File Offset: 0x0018F9F7
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.special);
			Writer.WriteOptimized(this.StepXP);
			Writer.WriteComposite<DynamicQuestRewardElement>(this.rewards);
			Writer.WriteComposite<DynamicQuestRewardElement>(this.postrewards);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00191829 File Offset: 0x0018FA29
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.special = Reader.ReadOptimizedString();
			this.StepXP = Reader.ReadOptimizedInt32();
			this.rewards = Reader.ReadCompositeList<DynamicQuestRewardElement>();
			this.postrewards = Reader.ReadCompositeList<DynamicQuestRewardElement>();
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0019185C File Offset: 0x0018FA5C
		public void award()
		{
			foreach (DynamicQuestRewardElement dynamicQuestRewardElement in this.rewards)
			{
				dynamicQuestRewardElement.award();
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x001918AC File Offset: 0x0018FAAC
		public void postaward()
		{
			foreach (DynamicQuestRewardElement dynamicQuestRewardElement in this.postrewards)
			{
				dynamicQuestRewardElement.award();
			}
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x001918FC File Offset: 0x0018FAFC
		public string getRewardConversationType()
		{
			foreach (DynamicQuestRewardElement dynamicQuestRewardElement in this.rewards)
			{
				string rewardConversationType = dynamicQuestRewardElement.getRewardConversationType();
				if (!string.IsNullOrEmpty(rewardConversationType))
				{
					return rewardConversationType;
				}
			}
			return "Choice";
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00191960 File Offset: 0x0018FB60
		public string getRewardAcceptQuestText()
		{
			foreach (DynamicQuestRewardElement dynamicQuestRewardElement in this.rewards)
			{
				string rewardAcceptQuestText = dynamicQuestRewardElement.getRewardAcceptQuestText();
				if (!string.IsNullOrEmpty(rewardAcceptQuestText))
				{
					return rewardAcceptQuestText;
				}
			}
			return null;
		}

		// Token: 0x04002A56 RID: 10838
		public string special;

		// Token: 0x04002A57 RID: 10839
		public int StepXP;

		// Token: 0x04002A58 RID: 10840
		public List<DynamicQuestRewardElement> rewards = new List<DynamicQuestRewardElement>();

		// Token: 0x04002A59 RID: 10841
		public List<DynamicQuestRewardElement> postrewards = new List<DynamicQuestRewardElement>();
	}
}
