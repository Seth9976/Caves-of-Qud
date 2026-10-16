using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E14 RID: 3604
	public class EskhindSonnetBook : IBookContents
	{
		// Token: 0x060092D6 RID: 37590 RVA: 0x0034E522 File Offset: 0x0034C722
		public override string GetTitle()
		{
			return "{{W|a crumpled sheet of paper}}";
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060092D7 RID: 37591 RVA: 0x0034E529 File Offset: 0x0034C729
		// (set) Token: 0x060092D8 RID: 37592 RVA: 0x0034E53F File Offset: 0x0034C73F
		public List<IBookContents.BookPageInfo> pages
		{
			get
			{
				return (List<IBookContents.BookPageInfo>)The.Game.GetObjectGameState("EskhindSonnetBook");
			}
			set
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				game.SetObjectGameState("EskhindSonnetBook", value);
			}
		}

		// Token: 0x060092D9 RID: 37593 RVA: 0x0034E558 File Offset: 0x0034C758
		public override List<IBookContents.BookPageInfo> GetContents()
		{
			if (this.pages != null)
			{
				return this.pages;
			}
			List<List<string>> list = new List<List<string>>
			{
				new List<string> { "all four limbs of mine do long to run", "weary mind cannot conceive of fun" },
				new List<string> { "I can feel my walls are closing in", "Hindriarch says all I do is sin" },
				new List<string> { "evil thoughts accost my blackened soul", "splitting head feels much more like a hole" },
				new List<string> { "spitting anger boils up in my blood", "hardened heart despises all of Qud" },
				new List<string> { "my mind does all grievances rehash", "Hindriarch treats kendren like they're trash" },
				new List<string> { "this cruel world does bring me sad and low", "there's a line I clearly cannot tow" }
			};
			List<List<string>> list2 = new List<List<string>>
			{
				new List<string> { "your sparring axeblade parts the air", "you run your fingers through your hair" },
				new List<string> { "you look at me with those sweet eyes", "you're close enough to tantalize" },
				new List<string> { "you stand, resplendent and refined", "I think perhaps you could be mine" },
				new List<string> { "your armor glints under the sun", "our hearts both beat in time as one" },
				new List<string> { "you take my hand, yours rough but warm", "my eyes, unbidden, trace your form" }
			};
			List<List<string>> list3 = new List<List<string>>
			{
				new List<string> { "my heart ascends the spindle's height", "I know that everything's all right" },
				new List<string> { "a happy smile comes to my face", "I think that I might find my place" },
				new List<string> { "I feel the gentle warmth of love", "I hope that we could be enough" },
				new List<string> { "I feel a tingle on my skin", "my heart scarce knows where to begin" },
				new List<string> { "my breath comes, like my heart, too quick", "I see you and become lovesick" }
			};
			List<List<string>> list4 = new List<List<string>>
			{
				new List<string> { "tended fields of lah", "felling trees by saw" },
				new List<string> { "eating in a yurt", "farming 'til we hurt" },
				new List<string> { "stripping watervine", "claiming to be fine" },
				new List<string> { "putting safety first", "planning for the worst" },
				new List<string> { "gath'ring morning dew", "fearing what is new" }
			};
			List<List<string>> list5 = new List<List<string>>
			{
				new List<string> { "ease us when we hurt", "to the past revert" },
				new List<string> { "treat us like their own", "melt their hearts of stone" },
				new List<string> { "let me be myself", "look out for our health" },
				new List<string> { "see that life needs love", "ever be enough" },
				new List<string> { "offer me your hand", "ever understand" }
			};
			List<List<string>> list6 = new List<List<string>>();
			list6.Add(new List<string> { "However can I tell my aching heart", "that our remaining years will be apart?" });
			list6.Add(new List<string> { "Although I may succumb to nature's fang,", "my heart needs more than one square parasang." });
			list6.Add(new List<string> { "But I must leave you if you won't concede", "that death will find me should I stay unfreed." });
			list6.Add(new List<string> { "Now knowing I must go and you must stay", "it makes my tender heartstrings start to fray." });
			list6.Add(new List<string> { "To know I cannot stay, and you can't leave", "I'm left with no recourse except to grieve." });
			this.pages = new List<IBookContents.BookPageInfo>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			IBookContents.BookPageInfo bookPageInfo = new IBookContents.BookPageInfo();
			List<string> list7 = list.RemoveRandomElement<List<string>>(null);
			List<string> list8 = list.RemoveRandomElement<List<string>>(null);
			stringBuilder.AppendLine("When " + list7[0]);
			stringBuilder.AppendLine("and " + list8[0]);
			stringBuilder.AppendLine("When " + list7[1]);
			stringBuilder.AppendLine("and " + list8[1]);
			stringBuilder.AppendLine();
			List<string> list9 = list2.RemoveRandomElement<List<string>>(null);
			List<string> list10 = list3.RemoveRandomElement<List<string>>(null);
			stringBuilder.AppendLine("But then " + list9[0]);
			stringBuilder.AppendLine("and so " + list10[0]);
			stringBuilder.AppendLine("So as " + list9[1]);
			stringBuilder.AppendLine("That's when " + list10[1]);
			stringBuilder.AppendLine();
			List<string> list11 = list4.RemoveRandomElement<List<string>>(null);
			List<string> list12 = list5.RemoveRandomElement<List<string>>(null);
			stringBuilder.AppendLine("What need have we for " + list11[0]);
			stringBuilder.AppendLine("Our parents will not " + list12[0]);
			stringBuilder.AppendLine("What do we gain from " + list11[1]);
			stringBuilder.AppendLine("Our village shall not " + list12[1]);
			stringBuilder.AppendLine();
			List<string> list13 = list6.RemoveRandomElement<List<string>>(null);
			stringBuilder.AppendLine(list13[0]);
			stringBuilder.AppendLine(list13[1]);
			bookPageInfo.Format = "Auto";
			bookPageInfo.Margins = "1,2,2,2";
			bookPageInfo.Text = stringBuilder.ToString();
			bookPageInfo.Title = this.GetTitle();
			this.pages.Add(bookPageInfo);
			return this.pages;
		}
	}
}
