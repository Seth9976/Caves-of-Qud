using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.Language;
using XRL.World.Capabilities;
using XRL.World.Text.Delegates;

namespace XRL.World.Text
{
	// Token: 0x02001461 RID: 5217
	public class ReplaceBuilder
	{
		// Token: 0x0600CD08 RID: 52488 RVA: 0x004A56D8 File Offset: 0x004A38D8
		public static ReplaceBuilder Get()
		{
			ReplaceBuilder replaceBuilder;
			if (!ReplaceBuilder.Builders.TryPop(out replaceBuilder))
			{
				replaceBuilder = new ReplaceBuilder();
			}
			replaceBuilder.Valid = true;
			return replaceBuilder;
		}

		// Token: 0x0600CD09 RID: 52489 RVA: 0x004A5704 File Offset: 0x004A3904
		public static void Return(ReplaceBuilder Builder)
		{
			if (!ReplaceBuilder.Builders.Contains(Builder))
			{
				Builder.Valid = false;
				Builder.Flags = 0;
				Builder.Target = null;
				Builder.Intrinsic.Clear();
				Builder.Replacers.Clear();
				Builder.Aliases.Clear();
				Builder.Arguments.Clear();
				Builder.DefaultArgument = -1;
				ReplaceBuilder.Builders.Push(Builder);
			}
		}

		// Token: 0x0600CD0A RID: 52490 RVA: 0x004A5774 File Offset: 0x004A3974
		public ReplaceBuilder()
		{
		}

		// Token: 0x0600CD0B RID: 52491 RVA: 0x004A57C4 File Offset: 0x004A39C4
		public ReplaceBuilder(StringBuilder Target)
		{
			this.Target = Target;
		}

		// Token: 0x0600CD0C RID: 52492 RVA: 0x004A5818 File Offset: 0x004A3A18
		private void AssertValid()
		{
			if (!this.Valid)
			{
				throw new InvalidOperationException(ReplaceBuilder.Builders.Contains(this) ? "Builder is finished" : "Builder is invalid");
			}
		}

		// Token: 0x0600CD0D RID: 52493 RVA: 0x004A5844 File Offset: 0x004A3A44
		public ReplaceBuilder Start(string Text)
		{
			StringBuilder intrinsic = this.Intrinsic;
			intrinsic.Clear().Append(Text);
			return this.Start(intrinsic);
		}

		// Token: 0x0600CD0E RID: 52494 RVA: 0x004A586C File Offset: 0x004A3A6C
		public ReplaceBuilder Start(StringBuilder Target)
		{
			this.AssertValid();
			this.Target = Target;
			this.Replacers.Clear();
			this.Aliases.Clear();
			this.Arguments.Clear();
			this.DefaultArgument = -1;
			return this;
		}

		// Token: 0x0600CD0F RID: 52495 RVA: 0x004A58A4 File Offset: 0x004A3AA4
		public ReplaceBuilder SetDefaultArgument(int Index)
		{
			this.AssertValid();
			this.DefaultArgument = Index;
			return this;
		}

		// Token: 0x0600CD10 RID: 52496 RVA: 0x004A58B4 File Offset: 0x004A3AB4
		public ReplaceBuilder SetDefaultArgument(GameObject Object)
		{
			this.AssertValid();
			int count = this.Arguments.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.Arguments[i].Object == Object)
				{
					this.DefaultArgument = i;
					return this;
				}
			}
			throw new KeyNotFoundException("Object was not present in list of arguments.");
		}

		// Token: 0x0600CD11 RID: 52497 RVA: 0x004A5908 File Offset: 0x004A3B08
		public ReplaceBuilder SetDefaultArgument(string Explicit)
		{
			this.AssertValid();
			int count = this.Arguments.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.Arguments[i].Explicit == Explicit)
				{
					this.DefaultArgument = i;
					return this;
				}
			}
			throw new KeyNotFoundException("Explicit string was not present in list of arguments.");
		}

		// Token: 0x0600CD12 RID: 52498 RVA: 0x004A5960 File Offset: 0x004A3B60
		public ReplaceBuilder AddObject(GameObject Object, string Alias = null)
		{
			this.AssertValid();
			if (Object != null)
			{
				this.Arguments.Add(new TextArgument(Object, null, null));
				if (Alias != null)
				{
					this.Aliases[Alias] = this.Arguments.Count - 1;
				}
				if (this.DefaultArgument == -1)
				{
					this.DefaultArgument = 0;
				}
			}
			return this;
		}

		// Token: 0x0600CD13 RID: 52499 RVA: 0x004A59B8 File Offset: 0x004A3BB8
		public ReplaceBuilder AddExplicit(string Name, string Alias = null, IPronounProvider Pronouns = null)
		{
			this.AssertValid();
			if (!Name.IsNullOrEmpty())
			{
				this.Arguments.Add(new TextArgument(Name, Pronouns));
				if (Alias != null)
				{
					this.Aliases[Alias] = this.Arguments.Count - 1;
				}
				if (this.DefaultArgument == -1)
				{
					this.DefaultArgument = 0;
				}
			}
			return this;
		}

		// Token: 0x0600CD14 RID: 52500 RVA: 0x004A5A12 File Offset: 0x004A3C12
		public ReplaceBuilder AddExplicit(string Name, bool Plural)
		{
			this.AssertValid();
			if (!Name.IsNullOrEmpty())
			{
				this.Arguments.Add(new TextArgument(Name, Plural));
				if (this.DefaultArgument == -1)
				{
					this.DefaultArgument = 0;
				}
			}
			return this;
		}

		// Token: 0x0600CD15 RID: 52501 RVA: 0x004A5A48 File Offset: 0x004A3C48
		public ReplaceBuilder AddArgument(TextArgument Argument, string Alias = null)
		{
			this.AssertValid();
			this.Arguments.Add(Argument);
			if (Alias != null)
			{
				this.Aliases[Alias] = this.Arguments.Count - 1;
			}
			if (this.DefaultArgument == -1)
			{
				this.DefaultArgument = 0;
			}
			return this;
		}

		// Token: 0x0600CD16 RID: 52502 RVA: 0x004A5A94 File Offset: 0x004A3C94
		public ReplaceBuilder AddAlias(string Alias, int Index)
		{
			this.AssertValid();
			this.Aliases[Alias] = Index;
			return this;
		}

		// Token: 0x0600CD17 RID: 52503 RVA: 0x004A5AAC File Offset: 0x004A3CAC
		public ReplaceBuilder AddAlias(string Alias, GameObject Object)
		{
			this.AssertValid();
			int count = this.Arguments.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.Arguments[i].Object == Object)
				{
					this.Aliases[Alias] = i;
					return this;
				}
			}
			throw new KeyNotFoundException("Object was not present in list of arguments.");
		}

		// Token: 0x0600CD18 RID: 52504 RVA: 0x004A5B04 File Offset: 0x004A3D04
		public ReplaceBuilder AddReplacer(string Key, Replacer Delegate, string Default = null, int Flags = 0, bool Capitalization = false)
		{
			this.AssertValid();
			if (!Capitalization)
			{
				this.Replacers.Add(Key, new ReplacerEntry(Delegate, Default, Flags, false));
			}
			else
			{
				this.Replacers.Add(Grammar.InitLower(Key), new ReplacerEntry(Delegate, (Default != null) ? Grammar.InitLower(Default) : null, Flags, false));
				this.Replacers.Add(Grammar.InitCap(Key), new ReplacerEntry(Delegate, (Default != null) ? Grammar.InitCap(Default) : null, Flags, true));
			}
			return this;
		}

		// Token: 0x0600CD19 RID: 52505 RVA: 0x004A5B80 File Offset: 0x004A3D80
		public ReplaceBuilder AddReplacer(string Key, string Value)
		{
			this.AssertValid();
			this.Replacers.Add(Key, new ReplacerEntry(ReplaceBuilder.DefaultReplacer, Value, 0, false));
			return this;
		}

		// Token: 0x0600CD1A RID: 52506 RVA: 0x004A5BA2 File Offset: 0x004A3DA2
		public ReplaceBuilder StripColors()
		{
			return this.StripColors(true);
		}

		// Token: 0x0600CD1B RID: 52507 RVA: 0x004A5BAB File Offset: 0x004A3DAB
		public ReplaceBuilder StripColors(bool Value)
		{
			this.AssertValid();
			(ref this.Flags).SetBit(1, Value);
			return this;
		}

		// Token: 0x0600CD1C RID: 52508 RVA: 0x004A5BC1 File Offset: 0x004A3DC1
		public ReplaceBuilder ForceThirdPerson()
		{
			this.AssertValid();
			(ref this.Flags).SetBit(2, true);
			return this;
		}

		/// <summary>Process the target string builder with current arguments and pool the builder.</summary>
		// Token: 0x0600CD1D RID: 52509 RVA: 0x004A5BD7 File Offset: 0x004A3DD7
		public void Execute()
		{
			this.Process();
			ReplaceBuilder.Return(this);
		}

		// Token: 0x0600CD1E RID: 52510 RVA: 0x004A5BE5 File Offset: 0x004A3DE5
		public override string ToString()
		{
			this.Process();
			string text = this.Target.ToString();
			ReplaceBuilder.Return(this);
			return text;
		}

		// Token: 0x0600CD1F RID: 52511 RVA: 0x004A5C00 File Offset: 0x004A3E00
		public void EmitMessage(char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			GameObject @object = this.Arguments[this.DefaultArgument].Object;
			this.EmitMessage(@object, Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x0600CD20 RID: 52512 RVA: 0x004A5C34 File Offset: 0x004A3E34
		public void EmitMessage(GameObject Source, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			string text = this.ToString();
			Messaging.EmitMessage(Source, text, Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x0600CD21 RID: 52513 RVA: 0x004A5C5C File Offset: 0x004A3E5C
		private void Process()
		{
			this.AssertValid();
			bool flag = this.Flags.HasBit(2) && Grammar.AllowSecondPerson;
			try
			{
				if (flag)
				{
					Grammar.AllowSecondPerson = false;
				}
				GameText.Process(this.Target, this.Replacers, this.Aliases, this.Arguments, this.DefaultArgument, this.Flags.HasBit(1));
			}
			finally
			{
				if (flag)
				{
					Grammar.AllowSecondPerson = true;
				}
			}
		}

		// Token: 0x04004F47 RID: 20295
		private const int FLAG_STRIP_COLORS = 1;

		// Token: 0x04004F48 RID: 20296
		private const int FLAG_THIRD_PERSON = 2;

		// Token: 0x04004F49 RID: 20297
		private static Stack<ReplaceBuilder> Builders = new Stack<ReplaceBuilder>(2);

		// Token: 0x04004F4A RID: 20298
		private StringBuilder Intrinsic = new StringBuilder();

		// Token: 0x04004F4B RID: 20299
		private StringBuilder Target;

		// Token: 0x04004F4C RID: 20300
		private StringMap<ReplacerEntry> Replacers = new StringMap<ReplacerEntry>();

		// Token: 0x04004F4D RID: 20301
		private StringMap<int> Aliases = new StringMap<int>();

		// Token: 0x04004F4E RID: 20302
		private List<TextArgument> Arguments = new List<TextArgument>();

		// Token: 0x04004F4F RID: 20303
		private int DefaultArgument = -1;

		// Token: 0x04004F50 RID: 20304
		private int Flags;

		// Token: 0x04004F51 RID: 20305
		private bool Valid = true;

		// Token: 0x04004F52 RID: 20306
		private static Replacer DefaultReplacer = (DelegateContext Context) => Context.Default;
	}
}
