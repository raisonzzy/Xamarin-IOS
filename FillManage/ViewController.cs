using System;
using System.IO;
using UIKit;

namespace FillManage
{
	public partial class ViewController : UIViewController
	{
		#region private variable
		UIButton btnCreateFill;
		UILabel lblWriteFill;
		UIButton btnReadFill;
		UIButton btnDeleteFill;
		UIButton btnSave;
		UIButton btnCheck;
		UITextField txtWriteFillContext;
		UILabel lblFillUrl;
		UILabel lblReadFillContext;
		string filePath;
		#endregion
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CreateButton();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
		public void CreateButton()
		{
			this.btnCreateFill = new UIButton (UIButtonType.System);
			this.btnCreateFill.Frame = new CoreGraphics.CGRect (120, 120, 100, 30);
			this.btnCreateFill.SetTitle ("Create Fill", UIControlState.Normal);

			this.lblFillUrl = new UILabel (new CoreGraphics.CGRect (230, 120, 600, 60));
			this.lblFillUrl.BackgroundColor = UIColor.Gray;

			this.lblWriteFill = new UILabel ();
			this.lblWriteFill.Frame = new CoreGraphics.CGRect (120, 200, 100, 30);
			this.lblWriteFill.Text = "Write Fill";

			this.txtWriteFillContext = new UITextField ();
			this.txtWriteFillContext.Frame = new CoreGraphics.CGRect (230, 200, 200, 50);
			this.txtWriteFillContext.BackgroundColor = UIColor.Orange;
			this.txtWriteFillContext.TextColor = UIColor.Black;

			this.btnSave = new UIButton (UIButtonType.System);
			this.btnSave.Frame = new CoreGraphics.CGRect (410, 200, 100, 30);
			this.btnSave.SetTitle ("Save Fill", UIControlState.Normal);

			this.btnReadFill = new UIButton (UIButtonType.System);
			this.btnReadFill.Frame = new CoreGraphics.CGRect (120, 280, 100, 30);
			this.btnReadFill.SetTitle ("Read Fill", UIControlState.Normal);

			this.lblReadFillContext = new UILabel (new CoreGraphics.CGRect (230, 280, 600, 30));
			this.lblReadFillContext.BackgroundColor = UIColor.Gray;

			this.btnDeleteFill = new UIButton (UIButtonType.System);
			this.btnDeleteFill.Frame = new CoreGraphics.CGRect (120, 330, 100, 30);
			this.btnDeleteFill.SetTitle ("Delete Fill", UIControlState.Normal);

			this.btnCheck = new UIButton (UIButtonType.System);
			this.btnCheck.Frame = new CoreGraphics.CGRect (120, 380, 100, 30);
			this.btnCheck.SetTitle ("Check Fill", UIControlState.Normal);

			this.btnCreateFill.TouchUpInside += Button_TouchUpInside;
			this.btnSave.TouchUpInside += Button_TouchUpInside;
			this.btnReadFill.TouchUpInside += Button_TouchUpInside;
			this.btnDeleteFill.TouchUpInside += Button_TouchUpInside;
			this.btnCheck.TouchUpInside += Button_TouchUpInside;

			this.View.AddSubviews (this.btnCreateFill, this.btnSave, this.btnReadFill, this.btnDeleteFill, this.btnCheck);
			this.View.AddSubview (this.txtWriteFillContext);
			this.View.AddSubviews (this.lblFillUrl, this.lblWriteFill, this.lblReadFillContext);
		}

		private void Button_TouchUpInside(object sender,EventArgs e)
		{
			UIAlertView alert = new UIAlertView ();

			UIButton btn = (UIButton)sender;
			Console.WriteLine (btn.Title (UIControlState.Normal));
			switch (btn.Title (UIControlState.Normal)) {
			case"Create Fill":
				{
					filePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "textfill.txt");
					if(!File.Exists(filePath))
						File.Create (filePath);
					this.lblFillUrl.Text = filePath;
					break;
				}
			case"Save Fill":
				{
					if (string.IsNullOrEmpty (filePath)|| (!File.Exists (filePath))) {
						alert.Title = "警告";
						alert.Message = "请创建文件";
						alert.AddButton ("确定");
						alert.Show ();
						break;
					}

					if (string.IsNullOrWhiteSpace (this.txtWriteFillContext.Text)) {
						alert.Title = "警告";
						alert.Message = "请输入文件内容";
						alert.AddButton ("确定");
						alert.Show ();
						break;
					} 

					using (StreamWriter wr = new StreamWriter (filePath)) {
						wr.Write (this.txtWriteFillContext.Text);
						alert.Title = "提醒";
						alert.Message = "文件保存成功";
						alert.AddButton ("确定");
						alert.Show ();
					}
					break;
				}
			case"Read Fill":
				{
					this.lblReadFillContext.Text = "";
					if (string.IsNullOrEmpty (filePath)) {
						alert.Title = "警告";
						alert.Message = "请创建文件";
						alert.AddButton ("确定");
						alert.Show ();
						break;
					}
					using (StreamReader re = new StreamReader (filePath)) {
						this.lblReadFillContext.Text = re.ReadToEnd ();
					}
					break;
				}
			case"Delete Fill":
				{
					if (string.IsNullOrEmpty (filePath) || (!File.Exists (filePath))) {
						alert.Title = "警告";
						alert.Message = "文件不存在";
						alert.AddButton ("确定");
						alert.Show ();
						break;
					}
					File.Delete (filePath);
					break;
				}
			case"Check Fill":
				{
					if ((!File.Exists (filePath))) {
						filePath = string.Empty;
						alert.Title = "提醒";
						alert.Message = "文件不存在";
						alert.AddButton ("确定");
						alert.Show ();
					} else {
						alert.Title = "提醒";
						alert.Message = "文件存在";
						alert.AddButton ("确定");
						alert.Show ();
					}
					break;
				}
			}
		}
	}
}

