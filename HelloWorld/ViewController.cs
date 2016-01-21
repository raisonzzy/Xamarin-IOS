using System;
using System.Drawing;
using Foundation;
using UIKit;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld
{
	public partial class ViewController : UIViewController
	{
		
		#region 私有变量 
		//开关灯按钮
		UIButton mybutton;
		bool isOpen=true;
		//图片样式标志
		UIButton btnImgStyle;
		UIImageView myImgView;
		int imgStyle;
		//旋转图片
		UIButton btnTrans;
		//缩放图片
		UIButton btnMakeScale;
		//键盘位置变量
		private NSObject kbdShow, kbdHide;
		//进度条
		UIButton btnLoad;
		UIProgressView progView;
		float vIncrementBy=0f;
		//滚动视图
		UIButton btnScrollView;
		UIScrollView imgScrollView;
		//分页实现
		UIButton btnPageControl;
		UIPageControl pageControl;
		UIScrollView pageScrollView;
		UIImageView imgva;
		UIImageView imgvb;
		//提醒视图
		UIButton btnAlert;
		//整体风格更改
		UIButton btnAllStyle;
		#endregion

		#region 视图初始化
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			this.LblHello.Text="My Label Hello";
			this.myview.BackgroundColor = UIColor.Black;
			CreateButton ();
			CreateButtonImgStyle ();
			CreateButtonTransfrom ();
			CreateBtnMakeScale ();

			//login
			this.btnLogin.TouchUpInside+=this.BtnLogin_TouchInside;
			//设置键盘样式
			this.txtUserName.ReturnKeyType=UIReturnKeyType.Next;

			this.txtUserName.KeyboardAppearance = UIKeyboardAppearance.Dark;
			this.txtPassword.KeyboardType = UIKeyboardType.NumberPad;
			this.txtPassword.ReturnKeyType = UIReturnKeyType.Go;

			//this.txtUserName.Delegate
			//键盘位置实现
			//键盘显示时
			kbdShow = UIKeyboard.Notifications.ObserveWillShow ((s, e) => {
				//if (this.txtPosition.Focused) {
					CoreGraphics.CGRect kbdFrame = e.FrameEnd;
					CoreGraphics.CGRect txtFrame = this.txtPosition.Frame;
					CoreGraphics.CGRect lblFrame = this.lblPosition.Frame;
					txtFrame.Y -= kbdFrame.Height;
					lblFrame.Y -= kbdFrame.Height;
					this.txtPosition.Frame = txtFrame;
					this.lblPosition.Frame = lblFrame;
				//}
			});
			//键盘隐藏时
			kbdHide = UIKeyboard.Notifications.ObserveDidHide ((s, e) => {
				//if (this.txtPosition.Focused) {
					CoreGraphics.CGRect kbdFrame = e.FrameEnd;
					CoreGraphics.CGRect txtFrame = this.txtPosition.Frame;
					CoreGraphics.CGRect lblFrame = this.lblPosition.Frame;
					txtFrame.Y += kbdFrame.Height;
					lblFrame.Y += kbdFrame.Height;
					this.txtPosition.Frame = txtFrame;
					this.lblPosition.Frame = lblFrame;
				//}
			});
			bool f = kbdShow.IsProxy;
			bool g = kbdHide.IsProxy;
			//加载按钮
			CreateBtnLoad ();
			//滚动视图
			CreateBtnScrollView();
			//分页
			CreateBtnPageControl();
			//提醒视图
			CreateBtnAlert();
			//修改全部控件风格
			//CreateBtnAllStyle();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
		#endregion

		#region 登陆处理
		private void BtnLogin_TouchInside(object sender,EventArgs e)
		{
			//关闭键盘
			this.txtUserName.ResignFirstResponder ();
			this.txtPassword.ResignFirstResponder ();

			if (string.IsNullOrWhiteSpace (this.txtUserName.Text)) {
				this.lblLoginResult.Text = "用户名不能为空，请输入";
				this.lblLoginResult.TextAlignment = UITextAlignment.Center;
				this.lblLoginResult.TextColor = UIColor.Red;
				return;
			}
			if (string.IsNullOrWhiteSpace (this.txtPassword.Text)) {
				this.lblLoginResult.Text = "密码不能为空，请输入";
				this.lblLoginResult.TextAlignment = UITextAlignment.Center;
				this.lblLoginResult.TextColor = UIColor.Red;
				return;
			}
			if (this.txtUserName.Text == "admin" && this.txtPassword.Text == "123456") {
				this.lblLoginResult.Text = "欢迎管理员回来";
				this.lblLoginResult.TextAlignment = UITextAlignment.Center;
				this.lblLoginResult.TextColor = UIColor.Yellow;
			} else {
				this.lblLoginResult.Text = "用户名或密码不正确";
				this.lblLoginResult.TextAlignment = UITextAlignment.Center;
				this.lblLoginResult.TextColor = UIColor.Red;
			}
		}
		#endregion

		#region 开关灯
		/// <summary>
		/// Creates the button.
		/// </summary>
		public void CreateButton()
		{
			mybutton = new UIButton (UIButtonType.System);
			mybutton.BackgroundColor = UIColor.Black;
			mybutton.Frame = new CoreGraphics.CGRect (250, 50, 100, 40);
			this.mybutton.SetTitle ("Open Light", UIControlState.Normal);
			//this.mybutton.SetTitleColor (UIColor.Blue, UIControlState.Highlighted);
			this.mybutton.TouchUpInside += this.MybuttonTouchUp;
			this.View.AddSubview(this.mybutton);

		}
		/// <summary>
		/// Mybuttons the touch up.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void MybuttonTouchUp(object sender,EventArgs e)
		{
			if (isOpen) {
				this.myview.BackgroundColor = UIColor.Green;
				isOpen = false;
				this.mybutton.ShowsTouchWhenHighlighted = true;
				this.mybutton.BackgroundColor = UIColor.Green;
				this.mybutton.SetTitle ("Close Light", UIControlState.Normal);
			}
			else
			{
				this.myview.BackgroundColor = UIColor.Black;
				isOpen = true;
				this.mybutton.BackgroundColor = UIColor.Black;
				this.mybutton.SetTitle ("Open Light", UIControlState.Normal);
			}
		}
		#endregion

		#region 创建图片
		/// <summary>
		/// Creates the image.
		/// </summary>
		public void CreateImg()
		{
			this.imgStyle = 0;
			this.myImgView = new UIImageView ();
			this.myImgView.Frame=new CoreGraphics.CGRect(380, 100, 320, 580);
			this.myImgView.ContentMode = UIViewContentMode.ScaleAspectFit;
			this.myImgView.Image = UIImage.FromFile ("imgstyle.png");
			//this.myImgView.Image = UIImage.FromFile ("556a69d05ddd1.jpg");
			this.View.AddSubview (this.myImgView);
		}
		#endregion

		#region 切换图片样式
		/// <summary>
		/// Creates the button image style.
		/// </summary>
		public void CreateButtonImgStyle()
		{
			this.btnImgStyle = new UIButton (UIButtonType.System);
			this.btnImgStyle.Frame = new CoreGraphics.CGRect (380, 50, 180, 30);
			this.btnImgStyle.BackgroundColor = UIColor.Gray;
			this.btnImgStyle.SetTitle ("更换图片样式-ScaleAspectFit", UIControlState.Normal);
			this.btnImgStyle.TouchUpInside += this.BtnTrans_TouchUpInside;
			//create UIImageView
			this.CreateImg();
			this.View.AddSubview (this.btnImgStyle);
		}
		#endregion

		#region 旋转图片
		/// <summary>
		/// Creates the button transfrom.
		/// </summary>
		public void CreateButtonTransfrom()
		{
			this.btnTrans = new UIButton (UIButtonType.System);
			this.btnTrans.Frame = new CoreGraphics.CGRect (580, 50, 100, 30);
			this.btnTrans.BackgroundColor = UIColor.Gray;
			this.btnTrans.SetTitle ("旋转图片", UIControlState.Normal);
			this.btnTrans.TouchUpInside += this.BtnTrans_TouchUpInside;

			this.View.AddSubview (this.btnTrans);
		}
		#endregion

		#region 缩放图片
		public void CreateBtnMakeScale()
		{
			this.btnMakeScale = new UIButton (UIButtonType.System);
			this.btnMakeScale.Frame = new CoreGraphics.CGRect (690, 50, 100, 30);
			this.btnMakeScale.BackgroundColor = UIColor.Gray;
			this.btnMakeScale.SetTitle ("缩放图片", UIControlState.Normal);
			this.btnMakeScale.TouchUpInside += this.BtnTrans_TouchUpInside;

			this.View.AddSubview (this.btnMakeScale);
		}
		#endregion

		#region 图片样式控制
		/// <summary>
		/// Buttons the trans touch up inside.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void BtnTrans_TouchUpInside(object sender,EventArgs e)
		{
			UIButton btnTemp = (UIButton)sender;

			if (btnTemp.Title (UIControlState.Normal).ToString () == "旋转图片") {
				this.myImgView.Image = UIImage.FromFile ("556a69d05ddd1.jpg");
				this.myImgView.Transform = CoreGraphics.CGAffineTransform.MakeRotation ((float)(3.14 / 10));
			} else if (btnTemp.Title (UIControlState.Normal).ToString () == "缩放图片") {
				this.myImgView.Image = UIImage.FromFile ("556a69d05ddd1.jpg");
				this.myImgView.Transform = CoreGraphics.CGAffineTransform.MakeScale ((float)(0.5), (float)(0.5));
			} else if (btnTemp.Title (UIControlState.Normal).IndexOf ("-") > -1) {
				imgStyle++;
				this.myImgView.Image = UIImage.FromFile ("imgstyle.png");
				switch (imgStyle) {
				case 1:
					{
						this.myImgView.ContentMode = UIViewContentMode.ScaleToFill;
						this.btnImgStyle.SetTitle ("更换图片样式-ScaleToFill", UIControlState.Normal);
						break;
					}
				case 2:
					{
						this.myImgView.ContentMode = UIViewContentMode.ScaleAspectFill;
						this.btnImgStyle.SetTitle ("更换图片样式-ScaleAspectFill", UIControlState.Normal);
						break;
					}
				case 3:
					{
						this.myImgView.ContentMode = UIViewContentMode.Redraw;
						this.btnImgStyle.SetTitle ("更换图片样式-Redraw", UIControlState.Normal);
						break;
					}
				case 4:
					{
						this.myImgView.ContentMode = UIViewContentMode.Center;
						this.btnImgStyle.SetTitle ("更换图片样式-Center", UIControlState.Normal);
						break;
					}
				case 5:
					{
						this.imgStyle = 1;
						this.myImgView.ContentMode = UIViewContentMode.ScaleAspectFit;
						this.btnImgStyle.SetTitle ("更换图片样式-ScaleAspectFit", UIControlState.Normal);
						break;
					}
				}
			}
		}
		#endregion

		#region 进度条加载
		public void CreateBtnLoad()
		{
			this.btnLoad = new UIButton (UIButtonType.System);
			this.btnLoad.Frame = new CoreGraphics.CGRect (380, 700, 80, 30);
			this.btnLoad.SetTitle ("加载", UIControlState.Normal);
			this.btnLoad.TouchUpInside += this.BtnLoad_TouchUpInside;
			CreateProgView ();
			this.View.AddSubview (this.btnLoad);
		}
		public void CreateProgView()
		{
			this.progView = new UIProgressView (new RectangleF (500, 700, 100, 50));
			this.progView.Progress = 0f;
			this.vIncrementBy = (float)(0.2);
			this.View.AddSubview (this.progView);
		}
		private void BtnLoad_TouchUpInside(object sender,EventArgs e)
		{
			this.btnLoad.Enabled = false;
			this.progView.Progress = 0f;
			Task.Factory.StartNew (this.StartProgress);
		}

		public void StartProgress()
		{
			float currentProgress = 0f;
			while (currentProgress < 1f) {
				Thread.Sleep (500);
				this.InvokeOnMainThread (delegate {
					this.progView.Progress += this.vIncrementBy;
					currentProgress = this.progView.Progress;
					if (currentProgress >= 1f) {
						this.btnLoad.Enabled = true;
					}
				});
			}
		}
		#endregion

		#region 滚动视图
		public void CreateBtnScrollView()
		{
			this.btnScrollView = new UIButton (UIButtonType.System);
			this.btnScrollView.Frame = new CoreGraphics.CGRect (800, 50, 100, 30);
			this.btnScrollView.SetTitle ("滚动视图", UIControlState.Normal);
			this.btnScrollView.TouchUpInside += this.BtnScrollView_TouchUpInside;
			this.View.AddSubview(this.btnScrollView);
			this.CreateScrollView ();
		}
		public void CreateScrollView()
		{
			this.imgScrollView = new UIScrollView ();
			this.imgScrollView.Frame = new CoreGraphics.CGRect (750, 80, 260, 500);
			this.imgScrollView.ContentSize = new CoreGraphics.CGSize (260, 1000);
			//this.imgScrollView.BackgroundColor = UIColor.Black;
			this.View.AddSubview (this.imgScrollView);
			this.imgScrollView.Scrolled += delegate {
				Console.WriteLine ("开始滚动");
			};
			this.imgScrollView.DecelerationEnded += delegate {
				Console.WriteLine ("结束滚动");
			};
		}
		private void BtnScrollView_TouchUpInside(object sender,EventArgs e)
		{
			float y = 10;
			for (float i = 1; i < 21; i++) {
				UILabel lblrow = new UILabel ();
				lblrow.Frame = new CoreGraphics.CGRect (0, y, 260, 30);
				lblrow.BackgroundColor = UIColor.Cyan;
				lblrow.Text = string.Format ("NO {0}", i);
				this.imgScrollView.AddSubview (lblrow);
				y += 50;
			}
		}
		#endregion

		#region 分页实现
		public void CreateBtnPageControl()
		{
			this.btnPageControl = new UIButton (UIButtonType.System);
			this.btnPageControl.Frame = new CoreGraphics.CGRect (380, 100, 100, 30);
			this.btnPageControl.SetTitle ("我要分页", UIControlState.Normal);
			this.btnPageControl.TouchUpInside += BtnPageControl_TouchUpInside;
			this.View.AddSubview (this.btnPageControl);
		}
		private void BtnPageControl_TouchUpInside(object sender,EventArgs e)
		{
			//UIScrollView
			this.pageScrollView = new UIScrollView ();
			this.pageScrollView.Frame = new CoreGraphics.CGRect (750, 610, 260, 100);
			this.pageScrollView.PagingEnabled = true;
			CoreGraphics.CGRect pageFrame = this.pageScrollView.Frame;
			this.pageScrollView.ContentSize = new CoreGraphics.CGSize (pageFrame.Width * 2, pageFrame.Height);
			this.pageScrollView.DecelerationEnded += PageScrollView_DecelerationEnded;
			//UIPageControl
			this.pageControl = new UIPageControl ();
			this.pageControl.Frame = new CoreGraphics.CGRect (750, 740, 260, 20);
			this.pageControl.Pages = 2;
			this.pageControl.BackgroundColor = UIColor.Gray;
			this.pageControl.ValueChanged += PageControl_ValueChanged;
			imgva = new UIImageView (new CoreGraphics.CGRect (0, 0, 260, 100));
			imgva.Image = UIImage.FromFile ("imgstyle.png");
			imgva.ContentMode = UIViewContentMode.ScaleAspectFit;
			pageFrame.X += this.pageScrollView.Frame.Width;
			imgvb=new UIImageView(new CoreGraphics.CGRect (260, 0, 260, 100));
			imgvb.Image = UIImage.FromFile ("556a69d05ddd1.jpg");
			imgvb.ContentMode = UIViewContentMode.ScaleAspectFit;

			this.pageScrollView.AddSubview (imgva);
			this.pageScrollView.AddSubview (imgvb);
			this.View.AddSubview (this.pageScrollView);
			this.View.AddSubview(this.pageControl);
		}
		private void PageScrollView_DecelerationEnded(object sender,EventArgs e)
		{
			nfloat x1 = this.imgva.Frame.X;
			nfloat x2 = this.imgvb.Frame.X;
			nfloat x = this.pageScrollView.ContentOffset.X;
			if (x == x1) {
				this.pageControl.CurrentPage = 0;
			} else {
				this.pageControl.CurrentPage = 1;
			}
		}
		private void PageControl_ValueChanged(object sender,EventArgs e)
		{
			CoreGraphics.CGPoint pagePoint = this.pageScrollView.ContentOffset;
			switch (this.pageControl.CurrentPage) {
			case 0:
				pagePoint.X = this.imgva.Frame.X;
				this.pageScrollView.SetContentOffset (pagePoint, true);
				break;
			case 1:
				pagePoint.X = this.imgvb.Frame.X;
				this.pageScrollView.SetContentOffset (pagePoint, true);
				break;
			}
		}
		#endregion

		#region 提醒视图
		public void CreateBtnAlert()
		{
			this.btnAlert = new UIButton (UIButtonType.System);
			this.btnAlert.Frame = new CoreGraphics.CGRect (500, 100, 100, 30);
			this.btnAlert.SetTitle ("Show Alert", UIControlState.Normal);
			this.btnAlert.TouchUpInside += BtnAlert_TouchUpInside;
			this.View.AddSubview (this.btnAlert);
		}
		private void BtnAlert_TouchUpInside(object sender,EventArgs e)
		{
			UIAlertView alert = new UIAlertView ();
			alert.Title = "登陆";
			alert.AlertViewStyle = UIAlertViewStyle.LoginAndPasswordInput;
			alert.Message = "请登录系统!";
			alert.AddButton ("确定");
			alert.AddButton ("取消");
			alert.AddButton ("关闭");
			alert.Dismissed += Alert_Dismissed;
			alert.Show ();
		}
		private void Alert_Dismissed(object sender,UIButtonEventArgs e)
		{
			nint btnIndex = e.ButtonIndex;

			this.btnAlert.SetTitle (btnIndex.ToString(), UIControlState.Normal);
		}
		#endregion

		#region 修改全部控件风格
		public void CreateBtnAllStyle()
		{
			this.btnAllStyle = new UIButton (UIButtonType.System);
			this.btnAllStyle.Frame = new CoreGraphics.CGRect (610, 100, 130, 30);
			this.btnAllStyle.SetTitle ("Change All Style", UIControlState.Normal);
			this.btnAllStyle.TouchUpInside += BtnAllStyle_TouchUpInside;
			this.View.AddSubview (this.btnAllStyle);
		}
		private void BtnAllStyle_TouchUpInside(object sender,EventArgs e)
		{
			UILabel.Appearance.BackgroundColor = UIColor.Black;
		}
		#endregion

	}
}

