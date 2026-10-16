using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4C RID: 2892
	[GenerateSerializationPartial]
	public class OpinionProselytize : IOpinionSubject
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600749B RID: 29851 RVA: 0x002BE64F File Offset: 0x002BC84F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x002BE652 File Offset: 0x002BC852
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600749D RID: 29853 RVA: 0x002BE66C File Offset: 0x002BC86C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600749E RID: 29854 RVA: 0x002BE686 File Offset: 0x002BC886
		public override int BaseValue
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x002BE68A File Offset: 0x002BC88A
		public override string GetText(GameObject Actor)
		{
			return "Proselytized me.";
		}
	}
}
