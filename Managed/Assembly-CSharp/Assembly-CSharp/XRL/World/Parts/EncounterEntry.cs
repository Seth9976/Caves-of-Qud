using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DCD RID: 3533
	[GenerateSerializationPartial]
	[Serializable]
	public class EncounterEntry : IComposite
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06008FA1 RID: 36769 RVA: 0x0033CB5A File Offset: 0x0033AD5A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x0033CB60 File Offset: 0x0033AD60
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.Zone);
			Writer.WriteOptimized(this.secretID);
			Writer.WriteOptimized(this.ReplacementText);
			Writer.Write(this.Optional);
			Writer.Write(this.Enabled);
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x0033CBB8 File Offset: 0x0033ADB8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Text = Reader.ReadOptimizedString();
			this.Zone = Reader.ReadOptimizedString();
			this.secretID = Reader.ReadOptimizedString();
			this.ReplacementText = Reader.ReadOptimizedString();
			this.Optional = Reader.ReadBoolean();
			this.Enabled = Reader.ReadBoolean();
		}

		// Token: 0x06008FA4 RID: 36772 RVA: 0x0033CC10 File Offset: 0x0033AE10
		public EncounterEntry()
		{
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x0033CC60 File Offset: 0x0033AE60
		public EncounterEntry(string Text, string Zone, string Replacement, string Secret, bool Optional)
		{
			this.Text = Text;
			this.Zone = Zone;
			this.ReplacementText = Replacement;
			this.Optional = Optional;
			this.secretID = Secret;
		}

		// Token: 0x04003BF6 RID: 15350
		public string Text = "You notice some strange ruins nearby. Do you want to investigate?";

		// Token: 0x04003BF7 RID: 15351
		public string Zone = "";

		// Token: 0x04003BF8 RID: 15352
		public string secretID = "";

		// Token: 0x04003BF9 RID: 15353
		public string ReplacementText = "";

		// Token: 0x04003BFA RID: 15354
		public bool Optional = true;

		// Token: 0x04003BFB RID: 15355
		public bool Enabled = true;
	}
}
