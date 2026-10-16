using System;
using System.Collections;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000651 RID: 1617
	internal class GlobalAsaxCompiler : BaseCompiler
	{
		// Token: 0x06004567 RID: 17767 RVA: 0x000BC295 File Offset: 0x000BA495
		public GlobalAsaxCompiler(ApplicationFileParser parser)
			: base(parser)
		{
			GlobalAsaxCompiler.applicationObjectTags.Clear();
			GlobalAsaxCompiler.sessionObjectTags.Clear();
			this.parser = parser;
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x00038745 File Offset: 0x00036945
		public static Type CompileApplicationType(ApplicationFileParser parser)
		{
			return new AspGenerator(parser).GetCompiledType();
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x000BC2B9 File Offset: 0x000BA4B9
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			base.CreateProfileProperty();
			this.ProcessObjects(this.parser.RootBuilder);
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x000BC2D8 File Offset: 0x000BA4D8
		private void ProcessObjects(ControlBuilder builder)
		{
			if (builder.Children == null)
			{
				return;
			}
			foreach (object obj in builder.Children)
			{
				if (obj is ObjectTagBuilder)
				{
					ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)obj;
					if (objectTagBuilder.Scope == null)
					{
						string text = base.CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
						base.CreatePropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, text, true);
					}
					else if (string.Compare(objectTagBuilder.Scope, "session", true, Helpers.InvariantCulture) == 0)
					{
						GlobalAsaxCompiler.sessionObjectTags.Add(objectTagBuilder);
						base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, false, false);
					}
					else
					{
						if (string.Compare(objectTagBuilder.Scope, "application", true, Helpers.InvariantCulture) != 0)
						{
							throw new ParseException(objectTagBuilder.Location, "Invalid scope: " + objectTagBuilder.Scope);
						}
						GlobalAsaxCompiler.applicationObjectTags.Add(objectTagBuilder);
						base.CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
						base.CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, true, false);
					}
				}
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x000BC42C File Offset: 0x000BA62C
		internal static ArrayList ApplicationObjects
		{
			get
			{
				return GlobalAsaxCompiler.applicationObjectTags;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000BC433 File Offset: 0x000BA633
		internal static ArrayList SessionObjects
		{
			get
			{
				return GlobalAsaxCompiler.sessionObjectTags;
			}
		}

		// Token: 0x040024F6 RID: 9462
		private ApplicationFileParser parser;

		// Token: 0x040024F7 RID: 9463
		private static ArrayList applicationObjectTags = new ArrayList(1);

		// Token: 0x040024F8 RID: 9464
		private static ArrayList sessionObjectTags = new ArrayList(1);
	}
}
