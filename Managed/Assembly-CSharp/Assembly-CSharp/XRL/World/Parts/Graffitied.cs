using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001043 RID: 4163
	[Serializable]
	public class Graffitied : IPart
	{
		// Token: 0x0600A5B9 RID: 42425 RVA: 0x003CA7F3 File Offset: 0x003C89F3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A5BA RID: 42426 RVA: 0x003CA7F6 File Offset: 0x003C89F6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A5BB RID: 42427 RVA: 0x003CA824 File Offset: 0x003C8A24
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{graffitied|graffitied}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5BC RID: 42428 RVA: 0x003CA84E File Offset: 0x003C8A4E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AddGraffiti(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5BD RID: 42429 RVA: 0x003CA85E File Offset: 0x003C8A5E
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			this.AddGraffiti(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5BE RID: 42430 RVA: 0x003CA86E File Offset: 0x003C8A6E
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (E.Context != "Sample")
			{
				this.Graffiti(this.ParentObject);
			}
			else
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5BF RID: 42431 RVA: 0x003CA8A4 File Offset: 0x003C8AA4
		public void Graffiti(GameObject wall)
		{
			wall.Render.SetForegroundColor(Crayons.GetRandomColorExcept((string c) => c == wall.Render.GetForegroundColor(), null));
			string text = "LibraryCorpus.json";
			MarkovBook.EnsureCorpusLoaded(text);
			MarkovChainData markovChainData = MarkovBook.CorpusData[text];
			this.Text = MarkovChain.GenerateShortSentence(markovChainData, null, 18);
			this.Text = Grammar.Obfuscate(this.Text.TrimEnd(' '), 10, 80, 5);
			wall.SetIntProperty("HasGraffiti", 1, false);
		}

		// Token: 0x0600A5C0 RID: 42432 RVA: 0x003CA938 File Offset: 0x003C8B38
		public void AddGraffiti(IShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				if (E.Infix.Length > 0)
				{
					E.Infix.Append("\n");
				}
				E.Infix.Append("\n").Append("Graffiti is scrawled across the surface. It reads: \n\n\"").Append("{{")
					.Append(this.ParentObject.Render.GetForegroundColor())
					.Append("|")
					.Append(this.Text)
					.Append("}}")
					.Append("\"\n");
			}
		}

		// Token: 0x04004473 RID: 17523
		public string Text;
	}
}
