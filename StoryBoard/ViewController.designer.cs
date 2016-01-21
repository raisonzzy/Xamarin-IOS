// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace StoryBoard
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btngo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPara { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btngo != null) {
				btngo.Dispose ();
				btngo = null;
			}
			if (txtPara != null) {
				txtPara.Dispose ();
				txtPara = null;
			}
		}
	}
}
