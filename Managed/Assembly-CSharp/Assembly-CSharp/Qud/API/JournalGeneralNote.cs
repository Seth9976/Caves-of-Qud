using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003F9 RID: 1017
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalGeneralNote : IBaseJournalEntry
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000C45E6 File Offset: 0x000C27E6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000C45EC File Offset: 0x000C27EC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Time);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000C4668 File Offset: 0x000C2868
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Time = Reader.ReadOptimizedInt64();
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000C46E1 File Offset: 0x000C28E1
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x000C46E9 File Offset: 0x000C28E9
		[Obsolete]
		public long time
		{
			get
			{
				return this.Time;
			}
			set
			{
				this.Time = value;
			}
		}

		// Token: 0x04001306 RID: 4870
		public long Time;
	}
}
