using System;
using System.ComponentModel;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013F RID: 319
	internal class Node : DOMObject, INode
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000086B2 File Offset: 0x000068B2
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x000086BC File Offset: 0x000068BC
		internal nsIDOMNode node
		{
			get
			{
				return this._node;
			}
			set
			{
				this.hashcode = value.GetHashCode();
				this.nodeNoProxy = this._node;
				if (!(value is nsIDOMHTMLDocument) && this.control.platform != this.control.enginePlatform)
				{
					this._node = nsDOMNode.GetProxy(this.control, value);
					return;
				}
				this._node = value;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000871B File Offset: 0x0000691B
		public Node(WebBrowser control, nsIDOMNode domNode)
			: base(control)
		{
			this.control = control;
			this.node = domNode;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00008732 File Offset: 0x00006932
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00008758 File Offset: 0x00006958
		internal virtual nsIDOMNode XPComObject
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00008760 File Offset: 0x00006960
		public virtual IAttributeCollection Attributes
		{
			get
			{
				if (!this.resources.Contains("Attributes"))
				{
					nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
					this.node.getAttributes(out nsIDOMNamedNodeMap);
					if (nsIDOMNamedNodeMap == null)
					{
						return new AttributeCollection(this.control);
					}
					this.resources.Add("Attributes", new AttributeCollection(this.control, nsIDOMNamedNodeMap));
				}
				return this.resources["Attributes"] as IAttributeCollection;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x000087D0 File Offset: 0x000069D0
		public virtual INodeList ChildNodes
		{
			get
			{
				if (!this.resources.Contains("ChildNodes"))
				{
					nsIDOMNodeList nsIDOMNodeList;
					this.node.getChildNodes(out nsIDOMNodeList);
					this.resources.Add("ChildNodes", new NodeList(this.control, nsIDOMNodeList));
				}
				return this.resources["ChildNodes"] as INodeList;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00008830 File Offset: 0x00006A30
		public virtual INode FirstChild
		{
			get
			{
				if (!this.resources.Contains("FirstChild"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getFirstChild(out nsIDOMNode);
					this.resources.Add("FirstChild", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["FirstChild"] as INode;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0000888C File Offset: 0x00006A8C
		public virtual INode LastChild
		{
			get
			{
				if (!this.resources.Contains("LastChild"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getLastChild(out nsIDOMNode);
					this.resources.Add("LastChild", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["LastChild"] as INode;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000088E8 File Offset: 0x00006AE8
		public virtual INode Parent
		{
			get
			{
				if (!this.resources.Contains("Parent"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getParentNode(out nsIDOMNode);
					this.resources.Add("Parent", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Parent"] as INode;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00008944 File Offset: 0x00006B44
		public virtual INode Previous
		{
			get
			{
				if (!this.resources.Contains("Previous"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getPreviousSibling(out nsIDOMNode);
					this.resources.Add("Previous", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Previous"] as INode;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000089A0 File Offset: 0x00006BA0
		public virtual INode Next
		{
			get
			{
				if (!this.resources.Contains("Next"))
				{
					nsIDOMNode nsIDOMNode;
					this.node.getNextSibling(out nsIDOMNode);
					this.resources.Add("Next", base.GetTypedNode(nsIDOMNode));
				}
				return this.resources["Next"] as INode;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000089F9 File Offset: 0x00006BF9
		public virtual string LocalName
		{
			get
			{
				this.node.getLocalName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00008A18 File Offset: 0x00006C18
		public IDocument Owner
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				if (!this.control.documents.ContainsKey(nsIDOMDocument.GetHashCode()))
				{
					this.control.documents.Add(nsIDOMDocument.GetHashCode(), new Document(this.control, nsIDOMDocument as nsIDOMHTMLDocument));
				}
				return this.control.documents[nsIDOMDocument.GetHashCode()] as IDocument;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00008A9C File Offset: 0x00006C9C
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00008B20 File Offset: 0x00006D20
		public string Style
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
				nsIDOMViewCSS nsIDOMViewCSS = (nsIDOMViewCSS)nsIDOMAbstractView;
				Base.StringSet(this.storage, string.Empty);
				AsciiString asciiString = new AsciiString(string.Empty);
				nsIDOMCSSStyleDeclaration nsIDOMCSSStyleDeclaration;
				nsIDOMViewCSS.getComputedStyle(this.node as nsIDOMElement, asciiString.Handle, out nsIDOMCSSStyleDeclaration);
				if (nsIDOMCSSStyleDeclaration == null)
				{
					return "";
				}
				nsIDOMCSSStyleDeclaration.getCssText(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				nsIDOMDocument nsIDOMDocument;
				this.node.getOwnerDocument(out nsIDOMDocument);
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
				nsIDOMViewCSS nsIDOMViewCSS = (nsIDOMViewCSS)nsIDOMAbstractView;
				Base.StringSet(this.storage, string.Empty);
				nsIDOMCSSStyleDeclaration nsIDOMCSSStyleDeclaration;
				nsIDOMViewCSS.getComputedStyle(this.node as nsIDOMElement, this.storage, out nsIDOMCSSStyleDeclaration);
				Base.StringSet(this.storage, value);
				nsIDOMCSSStyleDeclaration.setCssText(this.storage);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008B94 File Offset: 0x00006D94
		public virtual NodeType Type
		{
			get
			{
				ushort num;
				this.node.getNodeType(out num);
				return (NodeType)Enum.ToObject(typeof(NodeType), num);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00008BC4 File Offset: 0x00006DC4
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00008BE3 File Offset: 0x00006DE3
		public virtual string Value
		{
			get
			{
				this.node.getNodeValue(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.node.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00008C04 File Offset: 0x00006E04
		public virtual IntPtr AccessibleObject
		{
			get
			{
				nsIAccessible nsIAccessible = null;
				try
				{
					nsIAccessibilityService accessibilityService = this.control.AccessibilityService;
					nsIDOMDocument nsIDOMDocument;
					this.node.getOwnerDocument(out nsIDOMDocument);
					accessibilityService.getAccessibleFor(nsIDOMDocument, out nsIAccessible);
				}
				catch (Mono.WebBrowser.Exception ex)
				{
					Console.Error.WriteLine(ex.Message);
					goto IL_0063;
				}
				catch (global::System.Exception ex2)
				{
					Console.Error.WriteLine(ex2.Message);
					goto IL_0063;
				}
				if (nsIAccessible != null)
				{
					IntPtr zero = IntPtr.Zero;
					if (nsIAccessible.getNativeInterface(out zero) == 0)
					{
						return zero;
					}
				}
				IL_0063:
				Console.Error.WriteLine("Accessibility not available");
				return IntPtr.Zero;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public virtual void FireEvent(string eventName)
		{
			nsIDOMDocument nsIDOMDocument;
			this.node.getOwnerDocument(out nsIDOMDocument);
			nsIDOMDocumentEvent nsIDOMDocumentEvent = (nsIDOMDocumentEvent)nsIDOMDocument;
			nsIDOMAbstractView nsIDOMAbstractView;
			((nsIDOMDocumentView)nsIDOMDocument).getDefaultView(out nsIDOMAbstractView);
			nsIDOMEventTarget nsIDOMEventTarget = (nsIDOMEventTarget)this.node;
			bool flag = false;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
			if (num <= 1943722471U)
			{
				if (num <= 728389842U)
				{
					if (num <= 563185489U)
					{
						if (num != 297952813U)
						{
							if (num != 337658899U)
							{
								if (num != 563185489U)
								{
									goto IL_0528;
								}
								if (!(eventName == "error"))
								{
									goto IL_0528;
								}
								goto IL_0528;
							}
							else
							{
								if (!(eventName == "focus"))
								{
									goto IL_0528;
								}
								goto IL_0528;
							}
						}
						else
						{
							if (!(eventName == "select"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
					}
					else if (num != 597938448U)
					{
						if (num != 679260337U)
						{
							if (num != 728389842U)
							{
								goto IL_0528;
							}
							if (!(eventName == "unload"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
						else
						{
							if (!(eventName == "keyup"))
							{
								goto IL_0528;
							}
							goto IL_0477;
						}
					}
					else if (!(eventName == "mousedown"))
					{
						goto IL_0528;
					}
				}
				else if (num <= 1628281175U)
				{
					if (num != 1311441080U)
					{
						if (num != 1551804527U)
						{
							if (num != 1628281175U)
							{
								goto IL_0528;
							}
							if (!(eventName == "DOMFocusOut"))
							{
								goto IL_0528;
							}
							goto IL_04D2;
						}
						else if (!(eventName == "click"))
						{
							goto IL_0528;
						}
					}
					else
					{
						if (!(eventName == "keydown"))
						{
							goto IL_0528;
						}
						goto IL_0477;
					}
				}
				else if (num <= 1811665304U)
				{
					if (num != 1695364032U)
					{
						if (num != 1811665304U)
						{
							goto IL_0528;
						}
						if (!(eventName == "blur"))
						{
							goto IL_0528;
						}
						goto IL_0528;
					}
					else
					{
						if (!(eventName == "reset"))
						{
							goto IL_0528;
						}
						goto IL_0528;
					}
				}
				else if (num != 1922892221U)
				{
					if (num != 1943722471U)
					{
						goto IL_0528;
					}
					if (!(eventName == "dblclick"))
					{
						goto IL_0528;
					}
				}
				else
				{
					if (!(eventName == "change"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
			}
			else if (num <= 3642186483U)
			{
				if (num <= 2958779366U)
				{
					if (num != 2035911400U)
					{
						if (num != 2771110649U)
						{
							if (num != 2958779366U)
							{
								goto IL_0528;
							}
							if (!(eventName == "DOMActivate"))
							{
								goto IL_0528;
							}
							goto IL_04D2;
						}
						else
						{
							if (!(eventName == "abort"))
							{
								goto IL_0528;
							}
							goto IL_0528;
						}
					}
					else if (!(eventName == "mouseout"))
					{
						goto IL_0528;
					}
				}
				else if (num != 3447633555U)
				{
					if (num != 3611423958U)
					{
						if (num != 3642186483U)
						{
							goto IL_0528;
						}
						if (!(eventName == "keypress"))
						{
							goto IL_0528;
						}
						goto IL_0477;
					}
					else if (!(eventName == "mouseover"))
					{
						goto IL_0528;
					}
				}
				else if (!(eventName == "contextmenu"))
				{
					goto IL_0528;
				}
			}
			else if (num <= 3923426769U)
			{
				if (num != 3859241449U)
				{
					if (num != 3909063190U)
					{
						if (num != 3923426769U)
						{
							goto IL_0528;
						}
						if (!(eventName == "mousemove"))
						{
							goto IL_0528;
						}
					}
					else
					{
						if (!(eventName == "DOMFocusIn"))
						{
							goto IL_0528;
						}
						goto IL_04D2;
					}
				}
				else
				{
					if (!(eventName == "load"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
			}
			else if (num <= 4118085681U)
			{
				if (num != 4035692073U)
				{
					if (num != 4118085681U)
					{
						goto IL_0528;
					}
					if (!(eventName == "beforeunload"))
					{
						goto IL_0528;
					}
					goto IL_0528;
				}
				else if (!(eventName == "mouseup"))
				{
					goto IL_0528;
				}
			}
			else if (num != 4159373861U)
			{
				if (num != 4191711099U)
				{
					goto IL_0528;
				}
				if (!(eventName == "input"))
				{
					goto IL_0528;
				}
				goto IL_04D2;
			}
			else
			{
				if (!(eventName == "submit"))
				{
					goto IL_0528;
				}
				goto IL_0528;
			}
			string text = "mouseevents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent);
			nsIDOMMouseEvent nsIDOMMouseEvent = nsIDOMEvent as nsIDOMMouseEvent;
			Base.StringSet(this.storage, eventName);
			nsIDOMMouseEvent.initMouseEvent(this.storage, true, true, nsIDOMAbstractView, 1, 0, 0, 0, 0, false, false, false, false, 0, nsIDOMEventTarget);
			nsIDOMEventTarget.dispatchEvent(nsIDOMMouseEvent, out flag);
			return;
			IL_0477:
			text = "keyevents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent2;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent2);
			Base.StringSet(this.storage, eventName);
			nsIDOMKeyEvent nsIDOMKeyEvent = nsIDOMEvent2 as nsIDOMKeyEvent;
			nsIDOMKeyEvent.initKeyEvent(this.storage, true, true, nsIDOMAbstractView, false, false, false, false, 0U, 0U);
			nsIDOMEventTarget.dispatchEvent(nsIDOMKeyEvent, out flag);
			return;
			IL_04D2:
			text = "uievents";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent3;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent3);
			Base.StringSet(this.storage, eventName);
			nsIDOMUIEvent nsIDOMUIEvent = nsIDOMEvent3 as nsIDOMUIEvent;
			nsIDOMUIEvent.initUIEvent(this.storage, true, true, nsIDOMAbstractView, 1);
			nsIDOMEventTarget.dispatchEvent(nsIDOMUIEvent, out flag);
			return;
			IL_0528:
			text = "events";
			Base.StringSet(this.storage, text);
			nsIDOMEvent nsIDOMEvent4;
			nsIDOMDocumentEvent.createEvent(this.storage, out nsIDOMEvent4);
			Base.StringSet(this.storage, eventName);
			nsIDOMEvent4.initEvent(this.storage, true, true);
			nsIDOMEventTarget.dispatchEvent(nsIDOMEvent4, out flag);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00009224 File Offset: 0x00007424
		public virtual INode InsertBefore(INode child, INode refChild)
		{
			nsIDOMNode nsIDOMNode;
			this.node.insertBefore(((Node)child).node, ((Node)refChild).node, out nsIDOMNode);
			return child;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00009258 File Offset: 0x00007458
		public virtual INode ReplaceChild(INode child, INode oldChild)
		{
			nsIDOMNode nsIDOMNode;
			this.node.replaceChild(((Node)child).node, ((Node)oldChild).node, out nsIDOMNode);
			return oldChild;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000928C File Offset: 0x0000748C
		public virtual INode RemoveChild(INode child)
		{
			nsIDOMNode nsIDOMNode;
			this.node.removeChild(((Node)child).node, out nsIDOMNode);
			return child;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000092B4 File Offset: 0x000074B4
		public virtual INode AppendChild(INode child)
		{
			nsIDOMNode nsIDOMNode;
			int num = this.node.appendChild(((Node)child).node, out nsIDOMNode);
			Console.Error.WriteLine(num);
			return child;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000092E6 File Offset: 0x000074E6
		public override bool Equals(object obj)
		{
			return this == obj as Node;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000092F4 File Offset: 0x000074F4
		public static bool operator ==(Node left, Node right)
		{
			return left == right || (left != null && right != null && left.hashcode == right.hashcode);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00009312 File Offset: 0x00007512
		public static bool operator !=(Node left, Node right)
		{
			return !(left == right);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000931E File Offset: 0x0000751E
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00009326 File Offset: 0x00007526
		private EventListener EventListener
		{
			get
			{
				if (this.eventListener == null)
				{
					this.eventListener = new EventListener(this.node as nsIDOMEventTarget, this);
				}
				return this.eventListener;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000934D File Offset: 0x0000754D
		public void AttachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.AddHandler(handler, eventName);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000935C File Offset: 0x0000755C
		public void DetachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.RemoveHandler(handler, eventName);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0000936B File Offset: 0x0000756B
		public new EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00009388 File Offset: 0x00007588
		public void AttachEventHandler(string eventName, Delegate handler)
		{
			string text = string.Intern(this.node.GetHashCode().ToString() + ":" + eventName);
			this.Events.AddHandler(text, handler);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000093C8 File Offset: 0x000075C8
		public void DetachEventHandler(string eventName, Delegate handler)
		{
			string text = string.Intern(this.node.GetHashCode().ToString() + ":" + eventName);
			this.Events.RemoveHandler(text, handler);
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060009E3 RID: 2531 RVA: 0x00009406 File Offset: 0x00007606
		// (remove) Token: 0x060009E4 RID: 2532 RVA: 0x00009419 File Offset: 0x00007619
		public event NodeEventHandler Click
		{
			add
			{
				this.EventListener.AddHandler(value, "click");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "click");
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060009E5 RID: 2533 RVA: 0x0000942C File Offset: 0x0000762C
		// (remove) Token: 0x060009E6 RID: 2534 RVA: 0x0000943F File Offset: 0x0000763F
		public event NodeEventHandler DoubleClick
		{
			add
			{
				this.EventListener.AddHandler(value, "dblclick");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "dblclick");
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060009E7 RID: 2535 RVA: 0x00009452 File Offset: 0x00007652
		// (remove) Token: 0x060009E8 RID: 2536 RVA: 0x00009465 File Offset: 0x00007665
		public event NodeEventHandler KeyDown
		{
			add
			{
				this.EventListener.AddHandler(value, "keydown");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keydown");
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060009E9 RID: 2537 RVA: 0x00009478 File Offset: 0x00007678
		// (remove) Token: 0x060009EA RID: 2538 RVA: 0x0000948B File Offset: 0x0000768B
		public event NodeEventHandler KeyPress
		{
			add
			{
				this.EventListener.AddHandler(value, "keypress");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keypress");
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060009EB RID: 2539 RVA: 0x0000949E File Offset: 0x0000769E
		// (remove) Token: 0x060009EC RID: 2540 RVA: 0x000094B1 File Offset: 0x000076B1
		public event NodeEventHandler KeyUp
		{
			add
			{
				this.EventListener.AddHandler(value, "keyup");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "keyup");
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060009ED RID: 2541 RVA: 0x000094C4 File Offset: 0x000076C4
		// (remove) Token: 0x060009EE RID: 2542 RVA: 0x000094D7 File Offset: 0x000076D7
		public event NodeEventHandler MouseDown
		{
			add
			{
				this.EventListener.AddHandler(value, "mousedown");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mousedown");
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060009EF RID: 2543 RVA: 0x000094EA File Offset: 0x000076EA
		// (remove) Token: 0x060009F0 RID: 2544 RVA: 0x000094FD File Offset: 0x000076FD
		public event NodeEventHandler MouseEnter
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseenter");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseenter");
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060009F1 RID: 2545 RVA: 0x00009510 File Offset: 0x00007710
		// (remove) Token: 0x060009F2 RID: 2546 RVA: 0x00009523 File Offset: 0x00007723
		public event NodeEventHandler MouseLeave
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseout");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseout");
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060009F3 RID: 2547 RVA: 0x00009536 File Offset: 0x00007736
		// (remove) Token: 0x060009F4 RID: 2548 RVA: 0x00009549 File Offset: 0x00007749
		public event NodeEventHandler MouseMove
		{
			add
			{
				this.EventListener.AddHandler(value, "mousemove");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mousemove");
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060009F5 RID: 2549 RVA: 0x0000955C File Offset: 0x0000775C
		// (remove) Token: 0x060009F6 RID: 2550 RVA: 0x0000956F File Offset: 0x0000776F
		public event NodeEventHandler MouseOver
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseover");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseover");
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060009F7 RID: 2551 RVA: 0x00009582 File Offset: 0x00007782
		// (remove) Token: 0x060009F8 RID: 2552 RVA: 0x00009595 File Offset: 0x00007795
		public event NodeEventHandler MouseUp
		{
			add
			{
				this.EventListener.AddHandler(value, "mouseup");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "mouseup");
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060009F9 RID: 2553 RVA: 0x000095A8 File Offset: 0x000077A8
		// (remove) Token: 0x060009FA RID: 2554 RVA: 0x000095BB File Offset: 0x000077BB
		public event NodeEventHandler OnFocus
		{
			add
			{
				this.EventListener.AddHandler(value, "focus");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "focus");
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060009FB RID: 2555 RVA: 0x000095CE File Offset: 0x000077CE
		// (remove) Token: 0x060009FC RID: 2556 RVA: 0x000095E1 File Offset: 0x000077E1
		public event NodeEventHandler OnBlur
		{
			add
			{
				this.EventListener.AddHandler(value, "blur");
			}
			remove
			{
				this.EventListener.RemoveHandler(value, "blur");
			}
		}

		// Token: 0x04000124 RID: 292
		internal nsIDOMNode nodeNoProxy;

		// Token: 0x04000125 RID: 293
		private nsIDOMNode _node;

		// Token: 0x04000126 RID: 294
		protected int hashcode;

		// Token: 0x04000127 RID: 295
		private EventListener eventListener;

		// Token: 0x04000128 RID: 296
		private new WebBrowser control;

		// Token: 0x04000129 RID: 297
		private EventHandlerList events;
	}
}
