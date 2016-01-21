using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace StoryBoard
{
	partial class SecondController : UIViewController
	{
		public string ParaString {
			get;
			set;
		}
		public SecondController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			this.lblPara.Text=this.ParaString;
		}
	}
}
