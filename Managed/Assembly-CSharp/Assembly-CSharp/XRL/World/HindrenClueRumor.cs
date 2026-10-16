using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using Qud.API;

namespace XRL.World
{
	// Token: 0x02000851 RID: 2129
	[GenerateSerializationPartial]
	[Serializable]
	public sealed class HindrenClueRumor : IComposite
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x00195F31 File Offset: 0x00194131
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00195F34 File Offset: 0x00194134
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.villagerCategory);
			Writer.WriteOptimized(this.text);
			Writer.WriteOptimized(this.secret);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x00195F5A File Offset: 0x0019415A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Read(SerializationReader Reader)
		{
			this.villagerCategory = Reader.ReadOptimizedString();
			this.text = Reader.ReadOptimizedString();
			this.secret = Reader.ReadOptimizedString();
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00195F80 File Offset: 0x00194180
		public void trigger()
		{
			JournalAPI.RevealObservation(this.secret, true);
		}

		// Token: 0x04002AB8 RID: 10936
		public string villagerCategory;

		// Token: 0x04002AB9 RID: 10937
		public string text;

		// Token: 0x04002ABA RID: 10938
		public string secret;
	}
}
