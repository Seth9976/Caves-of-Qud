using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F61 RID: 3937
	[Serializable]
	public class ReplaceText : IPart
	{
		// Token: 0x06009CEA RID: 40170 RVA: 0x0038D7D4 File Offset: 0x0038B9D4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009CEB RID: 40171 RVA: 0x0038D7D7 File Offset: 0x0038B9D7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x06009CEC RID: 40172 RVA: 0x0038D7ED File Offset: 0x0038B9ED
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.Replace();
			return base.HandleEvent(E);
		}

		// Token: 0x06009CED RID: 40173 RVA: 0x0038D7FC File Offset: 0x0038B9FC
		public void Replace()
		{
			string[] array = this.Variables.Split(',', StringSplitOptions.None);
			string[] array2 = (this.Replacements.IsNullOrEmpty() ? null : this.Replacements.Split(',', StringSplitOptions.None));
			if (this.ReplaceInDisplayName)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (The.Game.HasStringGameState(text))
					{
						this.ParentObject.Render.DisplayName = this.ParentObject.Render.DisplayName.Replace(text, The.Game.GetStringGameState(text, ""));
					}
					else if (array2 != null && array2.Length >= i)
					{
						this.ParentObject.Render.DisplayName = this.ParentObject.Render.DisplayName.Replace(text, array2[i]);
					}
				}
			}
			Description description;
			if (this.ReplaceInDescription && this.ParentObject.TryGetPart<Description>(out description))
			{
				for (int j = 0; j < array.Length; j++)
				{
					string text2 = array[j];
					if (The.Game.HasStringGameState(text2))
					{
						description._Short = description._Short.Replace(text2, The.Game.GetStringGameState(text2, ""));
					}
					else if (array2 != null && array2.Length >= j)
					{
						description._Short = description._Short.Replace(text2, array2[j]);
					}
				}
			}
		}

		// Token: 0x040040DA RID: 16602
		public string Variables;

		// Token: 0x040040DB RID: 16603
		public string Replacements;

		// Token: 0x040040DC RID: 16604
		public bool ReplaceInDisplayName = true;

		// Token: 0x040040DD RID: 16605
		public bool ReplaceInDescription = true;
	}
}
