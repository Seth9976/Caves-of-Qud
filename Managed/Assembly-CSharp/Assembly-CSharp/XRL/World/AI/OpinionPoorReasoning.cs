using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4B RID: 2891
	[GenerateSerializationPartial]
	public class OpinionPoorReasoning : IOpinionSubject
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06007495 RID: 29845 RVA: 0x002BE605 File Offset: 0x002BC805
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007496 RID: 29846 RVA: 0x002BE608 File Offset: 0x002BC808
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x002BE622 File Offset: 0x002BC822
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06007498 RID: 29848 RVA: 0x002BE63C File Offset: 0x002BC83C
		public override int BaseValue
		{
			get
			{
				return -10;
			}
		}

		// Token: 0x06007499 RID: 29849 RVA: 0x002BE640 File Offset: 0x002BC840
		public override string GetText(GameObject Actor)
		{
			return "Poor reasoning.";
		}
	}
}
