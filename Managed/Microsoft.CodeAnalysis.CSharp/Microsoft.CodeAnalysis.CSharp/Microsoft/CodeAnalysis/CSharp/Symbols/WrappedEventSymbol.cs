using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B0 RID: 1456
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class WrappedEventSymbol : EventSymbol
	{
		// Token: 0x06007A35 RID: 31285 RVA: 0x001BF387 File Offset: 0x001BD587
		public WrappedEventSymbol(EventSymbol underlyingEvent)
		{
			this._underlyingEvent = underlyingEvent;
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06007A36 RID: 31286 RVA: 0x001BF396 File Offset: 0x001BD596
		public EventSymbol UnderlyingEvent
		{
			get
			{
				return this._underlyingEvent;
			}
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06007A37 RID: 31287 RVA: 0x001BF39E File Offset: 0x001BD59E
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingEvent.IsImplicitlyDeclared;
			}
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x06007A38 RID: 31288 RVA: 0x001BF3AB File Offset: 0x001BD5AB
		internal override bool HasSpecialName
		{
			get
			{
				return this._underlyingEvent.HasSpecialName;
			}
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06007A39 RID: 31289 RVA: 0x001BF3B8 File Offset: 0x001BD5B8
		public override string Name
		{
			get
			{
				return this._underlyingEvent.Name;
			}
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x001BF3C5 File Offset: 0x001BD5C5
		public override string GetDocumentationCommentXml([global::System.Runtime.CompilerServices.Nullable(2)] CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingEvent.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x06007A3B RID: 31291 RVA: 0x001BF3D5 File Offset: 0x001BD5D5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlyingEvent.Locations;
			}
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x001BF3E2 File Offset: 0x001BD5E2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlyingEvent.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x06007A3D RID: 31293 RVA: 0x001BF3EF File Offset: 0x001BD5EF
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._underlyingEvent.DeclaredAccessibility;
			}
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x06007A3E RID: 31294 RVA: 0x001BF3FC File Offset: 0x001BD5FC
		public override bool IsStatic
		{
			get
			{
				return this._underlyingEvent.IsStatic;
			}
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x06007A3F RID: 31295 RVA: 0x001BF409 File Offset: 0x001BD609
		public override bool IsVirtual
		{
			get
			{
				return this._underlyingEvent.IsVirtual;
			}
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x001BF416 File Offset: 0x001BD616
		public override bool IsOverride
		{
			get
			{
				return this._underlyingEvent.IsOverride;
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x06007A41 RID: 31297 RVA: 0x001BF423 File Offset: 0x001BD623
		public override bool IsAbstract
		{
			get
			{
				return this._underlyingEvent.IsAbstract;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x001BF430 File Offset: 0x001BD630
		public override bool IsSealed
		{
			get
			{
				return this._underlyingEvent.IsSealed;
			}
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06007A43 RID: 31299 RVA: 0x001BF43D File Offset: 0x001BD63D
		public override bool IsExtern
		{
			get
			{
				return this._underlyingEvent.IsExtern;
			}
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x06007A44 RID: 31300 RVA: 0x001BF44A File Offset: 0x001BD64A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._underlyingEvent.ObsoleteAttributeData;
			}
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x06007A45 RID: 31301 RVA: 0x001BF457 File Offset: 0x001BD657
		public override bool IsWindowsRuntimeEvent
		{
			get
			{
				return this._underlyingEvent.IsWindowsRuntimeEvent;
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x001BF464 File Offset: 0x001BD664
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this._underlyingEvent.HasRuntimeSpecialName;
			}
		}

		// Token: 0x04001D2D RID: 7469
		protected readonly EventSymbol _underlyingEvent;
	}
}
