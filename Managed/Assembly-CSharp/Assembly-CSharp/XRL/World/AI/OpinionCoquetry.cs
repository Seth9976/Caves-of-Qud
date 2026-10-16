using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B44 RID: 2884
	[GenerateSerializationPartial]
	public class OpinionCoquetry : IOpinionSubject
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06007468 RID: 29800 RVA: 0x002BE386 File Offset: 0x002BC586
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x002BE389 File Offset: 0x002BC589
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x002BE3A3 File Offset: 0x002BC5A3
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600746B RID: 29803 RVA: 0x002BE3BD File Offset: 0x002BC5BD
		public override int BaseValue
		{
			get
			{
				return -5;
			}
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x002BE3C1 File Offset: 0x002BC5C1
		public override string GetText(GameObject Actor)
		{
			return "Tried to beguile me.";
		}
	}
}
