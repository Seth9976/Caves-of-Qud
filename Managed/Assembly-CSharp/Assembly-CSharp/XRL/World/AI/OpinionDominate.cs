using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B45 RID: 2885
	[GenerateSerializationPartial]
	public class OpinionDominate : IOpinionSubject
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600746E RID: 29806 RVA: 0x002BE3D0 File Offset: 0x002BC5D0
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x002BE3D3 File Offset: 0x002BC5D3
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x002BE3ED File Offset: 0x002BC5ED
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06007471 RID: 29809 RVA: 0x002BE407 File Offset: 0x002BC607
		public override int BaseValue
		{
			get
			{
				return -20;
			}
		}

		// Token: 0x06007472 RID: 29810 RVA: 0x002BE40B File Offset: 0x002BC60B
		public override string GetText(GameObject Actor)
		{
			return "Did something to my mind.";
		}
	}
}
