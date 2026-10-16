using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a set of options used by a code generator.</summary>
	// Token: 0x02000349 RID: 841
	public class CodeGeneratorOptions
	{
		/// <summary>Gets or sets the object at the specified index.</summary>
		/// <returns>The object associated with the specified name. If no object associated with the specified name exists in the collection, null.</returns>
		/// <param name="index">The name associated with the object to retrieve. </param>
		// Token: 0x17000574 RID: 1396
		public object this[string index]
		{
			get
			{
				return this._options[index];
			}
			set
			{
				this._options[index] = value;
			}
		}

		/// <summary>Gets or sets the string to use for indentations.</summary>
		/// <returns>A string containing the characters to use for indentations.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00064834 File Offset: 0x00062A34
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x00064861 File Offset: 0x00062A61
		public string IndentString
		{
			get
			{
				object obj = this._options["IndentString"];
				if (obj == null)
				{
					return "    ";
				}
				return (string)obj;
			}
			set
			{
				this._options["IndentString"] = value;
			}
		}

		/// <summary>Gets or sets the style to use for bracing.</summary>
		/// <returns>A string containing the bracing style to use.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00064874 File Offset: 0x00062A74
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x000648A1 File Offset: 0x00062AA1
		public string BracingStyle
		{
			get
			{
				object obj = this._options["BracingStyle"];
				if (obj == null)
				{
					return "Block";
				}
				return (string)obj;
			}
			set
			{
				this._options["BracingStyle"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to append an else, catch, or finally block, including brackets, at the closing line of each previous if or try block.</summary>
		/// <returns>true if an else should be appended; otherwise, false. The default value of this property is false.</returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000648B4 File Offset: 0x00062AB4
		// (set) Token: 0x06001B54 RID: 6996 RVA: 0x000648DD File Offset: 0x00062ADD
		public bool ElseOnClosing
		{
			get
			{
				object obj = this._options["ElseOnClosing"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._options["ElseOnClosing"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to insert blank lines between members.</summary>
		/// <returns>true if blank lines should be inserted; otherwise, false. By default, the value of this property is true.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000648F8 File Offset: 0x00062AF8
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x00064921 File Offset: 0x00062B21
		public bool BlankLinesBetweenMembers
		{
			get
			{
				object obj = this._options["BlankLinesBetweenMembers"];
				return obj == null || (bool)obj;
			}
			set
			{
				this._options["BlankLinesBetweenMembers"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate members in the order in which they occur in member collections.</summary>
		/// <returns>true to generate the members in the order in which they occur in the member collection; otherwise, false. The default value of this property is false.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0006493C File Offset: 0x00062B3C
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x00064965 File Offset: 0x00062B65
		public bool VerbatimOrder
		{
			get
			{
				object obj = this._options["VerbatimOrder"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._options["VerbatimOrder"] = value;
			}
		}

		// Token: 0x04000E48 RID: 3656
		private readonly IDictionary _options = new ListDictionary();
	}
}
