using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B46 RID: 2886
	[GenerateSerializationPartial]
	public class OpinionFriendlyFire : IOpinionSubject
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06007474 RID: 29812 RVA: 0x002BE41A File Offset: 0x002BC61A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x002BE41D File Offset: 0x002BC61D
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007476 RID: 29814 RVA: 0x002BE437 File Offset: 0x002BC637
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06007477 RID: 29815 RVA: 0x002BE451 File Offset: 0x002BC651
		public override int BaseValue
		{
			get
			{
				return -5;
			}
		}

		// Token: 0x06007478 RID: 29816 RVA: 0x002BE455 File Offset: 0x002BC655
		public override string GetText(GameObject Actor)
		{
			return "Shot me by accident.";
		}
	}
}
