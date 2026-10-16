using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B47 RID: 2887
	[GenerateSerializationPartial]
	public class OpinionGoad : IOpinionSubject
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x002BE464 File Offset: 0x002BC664
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x002BE467 File Offset: 0x002BC667
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x002BE481 File Offset: 0x002BC681
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600747D RID: 29821 RVA: 0x002BE49B File Offset: 0x002BC69B
		public override int BaseValue
		{
			get
			{
				return -500;
			}
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x002BE4A2 File Offset: 0x002BC6A2
		public override string GetText(GameObject Actor)
		{
			return "Goaded me in conversation.";
		}
	}
}
