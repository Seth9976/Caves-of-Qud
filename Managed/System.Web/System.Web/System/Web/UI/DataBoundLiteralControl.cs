using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI
{
	/// <summary>Retains data-binding expressions and static literal text. This class cannot be inherited.</summary>
	// Token: 0x020001BD RID: 445
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBoundLiteralControl : Control, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> class. </summary>
		/// <param name="staticLiteralsCount">Defines the size of the array to create for storing static literal strings.</param>
		/// <param name="dataBoundLiteralCount">Defines the size of the array to create for storing data-bound literal strings.</param>
		// Token: 0x06001205 RID: 4613 RVA: 0x0002FD5E File Offset: 0x0002DF5E
		public DataBoundLiteralControl(int staticLiteralsCount, int dataBoundLiteralCount)
		{
			this.staticLiteralsCount = staticLiteralsCount;
			this.dataBoundLiterals = new string[dataBoundLiteralCount];
			base.AutoID = false;
		}

		/// <summary>Gets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object. </summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0002FD80 File Offset: 0x0002DF80
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = ((this.staticLiterals == null) ? 0 : this.staticLiterals.Length);
				int num2 = this.dataBoundLiterals.Length;
				int num3 = ((num > num2) ? num : num2);
				for (int i = 0; i < num3; i++)
				{
					if (i < num)
					{
						stringBuilder.Append(this.staticLiterals[i]);
					}
					if (i < num2)
					{
						stringBuilder.Append(this.dataBoundLiterals[i]);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0002FE04 File Offset: 0x0002E004
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Array array = (Array)savedState;
				if (array.Length == this.dataBoundLiterals.Length)
				{
					array.CopyTo(this.dataBoundLiterals, 0);
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0002FE38 File Offset: 0x0002E038
		protected internal override void Render(HtmlTextWriter output)
		{
			int num = ((this.staticLiterals == null) ? 0 : this.staticLiterals.Length);
			int num2 = this.dataBoundLiterals.Length;
			int num3 = ((num > num2) ? num : num2);
			for (int i = 0; i < num3; i++)
			{
				if (i < num)
				{
					output.Write(this.staticLiterals[i]);
				}
				if (i < num2)
				{
					output.Write(this.dataBoundLiterals[i]);
				}
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0002FE9B File Offset: 0x0002E09B
		protected override object SaveViewState()
		{
			if (this.dataBoundLiterals.Length == 0)
			{
				return null;
			}
			return this.dataBoundLiterals;
		}

		/// <summary>Assigns a string value to an array containing data-bound values.</summary>
		/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value. </param>
		/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
		// Token: 0x0600120B RID: 4619 RVA: 0x0002FEAE File Offset: 0x0002E0AE
		public void SetDataBoundString(int index, string s)
		{
			this.dataBoundLiterals[index] = s;
		}

		/// <summary>Assigns a string value to an array containing static values.</summary>
		/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value.</param>
		/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
		// Token: 0x0600120C RID: 4620 RVA: 0x0002FEB9 File Offset: 0x0002E0B9
		public void SetStaticString(int index, string s)
		{
			if (this.staticLiterals == null)
			{
				this.staticLiterals = new string[this.staticLiteralsCount];
			}
			this.staticLiterals[index] = s;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt to set the value is made.</exception>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0002FEDD File Offset: 0x0002E0DD
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00003A0D File Offset: 0x00001C0D
		string ITextControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001412 RID: 5138
		private int staticLiteralsCount;

		// Token: 0x04001413 RID: 5139
		private string[] staticLiterals;

		// Token: 0x04001414 RID: 5140
		private string[] dataBoundLiterals;
	}
}
