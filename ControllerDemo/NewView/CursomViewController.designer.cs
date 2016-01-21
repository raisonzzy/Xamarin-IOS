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

namespace ControllerDemo
{
	[Register ("CursomViewController")]
	partial class CursomViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblNew { get; set; }

		[Action ("UIButton14_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton14_TouchUpInside (UIButton sender);

		[Action ("UIButton28_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton28_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (lblNew != null) {
				lblNew.Dispose ();
				lblNew = null;
			}
		}
	}
}
