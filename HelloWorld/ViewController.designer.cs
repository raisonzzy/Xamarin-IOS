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

namespace HelloWorld
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LblHello { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLoginResult { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPosition { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LblTest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView myview { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPosition { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
			if (LblHello != null) {
				LblHello.Dispose ();
				LblHello = null;
			}
			if (lblLoginResult != null) {
				lblLoginResult.Dispose ();
				lblLoginResult = null;
			}
			if (lblPosition != null) {
				lblPosition.Dispose ();
				lblPosition = null;
			}
			if (LblTest != null) {
				LblTest.Dispose ();
				LblTest = null;
			}
			if (myview != null) {
				myview.Dispose ();
				myview = null;
			}
			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
			if (txtPosition != null) {
				txtPosition.Dispose ();
				txtPosition = null;
			}
			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}
		}
	}
}
