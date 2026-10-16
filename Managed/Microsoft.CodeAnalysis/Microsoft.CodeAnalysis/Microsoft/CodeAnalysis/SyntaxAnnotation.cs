using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000277 RID: 631
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public sealed class SyntaxAnnotation : IObjectWritable, IEquatable<SyntaxAnnotation>
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x00048255 File Offset: 0x00046455
		static SyntaxAnnotation()
		{
			ObjectBinder.RegisterTypeReader(typeof(SyntaxAnnotation), (ObjectReader r) => new SyntaxAnnotation(r));
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00048280 File Offset: 0x00046480
		[Nullable(1)]
		public static SyntaxAnnotation ElasticAnnotation
		{
			[NullableContext(1)]
			get;
		} = new SyntaxAnnotation();

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00048287 File Offset: 0x00046487
		public string Kind { get; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0004828F File Offset: 0x0004648F
		public string Data { get; }

		// Token: 0x06001844 RID: 6212 RVA: 0x00048297 File Offset: 0x00046497
		public SyntaxAnnotation()
		{
			this._id = Interlocked.Increment(ref SyntaxAnnotation.s_nextId);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000482AF File Offset: 0x000464AF
		public SyntaxAnnotation(string kind)
			: this()
		{
			this.Kind = kind;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000482BE File Offset: 0x000464BE
		public SyntaxAnnotation(string kind, string data)
			: this(kind)
		{
			this.Data = data;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000482CE File Offset: 0x000464CE
		[NullableContext(1)]
		private SyntaxAnnotation(ObjectReader reader)
		{
			this._id = reader.ReadInt64();
			this.Kind = reader.ReadString();
			this.Data = reader.ReadString();
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000482FA File Offset: 0x000464FA
		bool IObjectWritable.ShouldReuseInSerialization
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000482FD File Offset: 0x000464FD
		[NullableContext(1)]
		void IObjectWritable.WriteTo(ObjectWriter writer)
		{
			writer.WriteInt64(this._id);
			writer.WriteString(this.Kind);
			writer.WriteString(this.Data);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00048323 File Offset: 0x00046523
		[NullableContext(1)]
		private string GetDebuggerDisplay()
		{
			return string.Format("Annotation: Kind='{0}' Data='{1}'", this.Kind ?? "", this.Data ?? "");
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0004834D File Offset: 0x0004654D
		public bool Equals(SyntaxAnnotation other)
		{
			return other != null && this._id == other._id;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00048362 File Offset: 0x00046562
		public static bool operator ==(SyntaxAnnotation left, SyntaxAnnotation right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00048373 File Offset: 0x00046573
		public static bool operator !=(SyntaxAnnotation left, SyntaxAnnotation right)
		{
			return !(left == right);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0004837F File Offset: 0x0004657F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SyntaxAnnotation);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00048390 File Offset: 0x00046590
		public override int GetHashCode()
		{
			return this._id.GetHashCode();
		}

		// Token: 0x040009FF RID: 2559
		private readonly long _id;

		// Token: 0x04000A00 RID: 2560
		private static long s_nextId;
	}
}
