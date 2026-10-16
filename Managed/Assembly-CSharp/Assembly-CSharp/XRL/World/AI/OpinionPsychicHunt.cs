using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4D RID: 2893
	[GenerateSerializationPartial]
	public class OpinionPsychicHunt : IOpinionSubject
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060074A1 RID: 29857 RVA: 0x002BE699 File Offset: 0x002BC899
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x002BE69C File Offset: 0x002BC89C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x002BE6B6 File Offset: 0x002BC8B6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060074A4 RID: 29860 RVA: 0x002BE6D0 File Offset: 0x002BC8D0
		public override int BaseValue
		{
			get
			{
				return -500;
			}
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x002BE6D7 File Offset: 0x002BC8D7
		public override string GetText(GameObject Actor)
		{
			return "Glimmering mind.";
		}
	}
}
