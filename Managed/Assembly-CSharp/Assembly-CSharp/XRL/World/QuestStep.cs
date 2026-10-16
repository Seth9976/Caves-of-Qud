using System;

namespace XRL.World
{
	// Token: 0x02000963 RID: 2403
	[Serializable]
	public class QuestStep
	{
		/// <summary>This step is not inherited by the active quest and only exists in the blueprint.</summary>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600633A RID: 25402 RVA: 0x001E7FD5 File Offset: 0x001E61D5
		// (set) Token: 0x0600633B RID: 25403 RVA: 0x001E7FE3 File Offset: 0x001E61E3
		public bool Base
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		/// <summary>The completion state of this quest step, renders with a green checkmark in the UI.</summary>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600633C RID: 25404 RVA: 0x001E7FF2 File Offset: 0x001E61F2
		// (set) Token: 0x0600633D RID: 25405 RVA: 0x001E8000 File Offset: 0x001E6200
		public bool Finished
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		/// <summary>This step has awarded its associated XP and will silently complete without awarding experience if re-completed.</summary>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600633E RID: 25406 RVA: 0x001E800F File Offset: 0x001E620F
		// (set) Token: 0x0600633F RID: 25407 RVA: 0x001E801E File Offset: 0x001E621E
		public bool Awarded
		{
			get
			{
				return this.Flags.HasBit(32);
			}
			set
			{
				(ref this.Flags).SetBit(32, value);
			}
		}

		/// <summary>Whether this quest step has been failed, renders with a red cross in the UI.</summary>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06006340 RID: 25408 RVA: 0x001E802E File Offset: 0x001E622E
		// (set) Token: 0x06006341 RID: 25409 RVA: 0x001E803C File Offset: 0x001E623C
		public bool Failed
		{
			get
			{
				return this.Flags.HasBit(4);
			}
			set
			{
				(ref this.Flags).SetBit(4, value);
			}
		}

		/// <summary>Collapse the text body of this step in the UI when finished.</summary>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06006342 RID: 25410 RVA: 0x001E804B File Offset: 0x001E624B
		// (set) Token: 0x06006343 RID: 25411 RVA: 0x001E8059 File Offset: 0x001E6259
		public bool Collapse
		{
			get
			{
				return this.Flags.HasBit(8);
			}
			set
			{
				(ref this.Flags).SetBit(8, value);
			}
		}

		/// <summary>This step is hidden in the UI until revealed manually.</summary>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06006344 RID: 25412 RVA: 0x001E8068 File Offset: 0x001E6268
		// (set) Token: 0x06006345 RID: 25413 RVA: 0x001E8077 File Offset: 0x001E6277
		public bool Hidden
		{
			get
			{
				return this.Flags.HasBit(16);
			}
			set
			{
				(ref this.Flags).SetBit(16, value);
			}
		}

		/// <summary>This step is optional and won't prevent the quest as a whole from being completing.</summary>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x001E8087 File Offset: 0x001E6287
		// (set) Token: 0x06006347 RID: 25415 RVA: 0x001E8096 File Offset: 0x001E6296
		public bool Optional
		{
			get
			{
				return this.Flags.HasBit(64);
			}
			set
			{
				(ref this.Flags).SetBit(64, value);
			}
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x001E80A6 File Offset: 0x001E62A6
		public QuestStep()
		{
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x001E80B5 File Offset: 0x001E62B5
		public QuestStep(SerializationReader Reader)
			: this()
		{
			this.Load(Reader);
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x001E80C4 File Offset: 0x001E62C4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.ID,
				" n=",
				this.Name,
				" t=",
				this.Text,
				" xp=",
				this.XP.ToString(),
				" finished=",
				this.Finished.ToString()
			});
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x001E8138 File Offset: 0x001E6338
		public void Save(SerializationWriter Writer)
		{
			Writer.Write(this.ID);
			Writer.Write(this.Name);
			Writer.Write(this.Text);
			Writer.Write(this.Value);
			Writer.WriteOptimized(this.XP);
			Writer.WriteOptimized(this.Ordinal);
			Writer.WriteOptimized(this.Flags);
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x001E819C File Offset: 0x001E639C
		public void Load(SerializationReader Reader)
		{
			this.ID = Reader.ReadString();
			this.Name = Reader.ReadString();
			this.Text = Reader.ReadString();
			this.Value = Reader.ReadString();
			this.XP = Reader.ReadOptimizedInt32();
			this.Ordinal = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
		}

		// Token: 0x04002EA9 RID: 11945
		public const int FLAG_BASE = 1;

		// Token: 0x04002EAA RID: 11946
		public const int FLAG_FINISHED = 2;

		// Token: 0x04002EAB RID: 11947
		public const int FLAG_FAILED = 4;

		// Token: 0x04002EAC RID: 11948
		public const int FLAG_COLLAPSE = 8;

		// Token: 0x04002EAD RID: 11949
		public const int FLAG_HIDDEN = 16;

		// Token: 0x04002EAE RID: 11950
		public const int FLAG_AWARDED = 32;

		// Token: 0x04002EAF RID: 11951
		public const int FLAG_OPTIONAL = 64;

		// Token: 0x04002EB0 RID: 11952
		public string ID;

		// Token: 0x04002EB1 RID: 11953
		public string Name;

		// Token: 0x04002EB2 RID: 11954
		public string Text;

		// Token: 0x04002EB3 RID: 11955
		public string Value;

		// Token: 0x04002EB4 RID: 11956
		public int XP;

		/// <summary>The display order in the UI.</summary>
		// Token: 0x04002EB5 RID: 11957
		public int Ordinal;

		// Token: 0x04002EB6 RID: 11958
		public int Flags = 8;
	}
}
