using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B51 RID: 2897
	[GenerateSerializationPartial]
	public class OpinionTrespass : IOpinionSubject
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060074BB RID: 29883 RVA: 0x002BE966 File Offset: 0x002BCB66
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x002BE969 File Offset: 0x002BCB69
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x002BE983 File Offset: 0x002BCB83
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060074BE RID: 29886 RVA: 0x002BE99D File Offset: 0x002BCB9D
		public override int BaseValue
		{
			get
			{
				return -25;
			}
		}

		// Token: 0x060074BF RID: 29887 RVA: 0x002BE9A1 File Offset: 0x002BCBA1
		public override string GetText(GameObject Actor)
		{
			return "Poked around where they shouldn't.";
		}
	}
}
