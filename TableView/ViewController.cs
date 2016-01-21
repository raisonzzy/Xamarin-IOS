using System;
using System.Collections.Generic;
using UIKit;

namespace TableView
{
	public partial class ViewController : UIViewController
	{

		public UITableView customerStyleTableView;
		public UITableView customerAccessory;
		public UISwitch usw;
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			List<string> DataSourceList = new List<string> () {"itme1", "itme2", "itme3",
				"itme4", "itme5", "itme6", "itme7","itme8", "itme9", "itme10", "itme11"
			};

			this.myTableView.Source = new CustomerDataSource (DataSourceList);

			List<string> titleList = new List<string> (){ "A-Z", "a-z", "0-9","I-VVI" };
			List<string> Alist = new List<string> (){ "A", "B", "C", "D", "E" };
			List<string> NumList = new List<string> (){ "0", "1", "2", "3", "4" };
			List<string> MIList = new List<string> (){ "I", "II", "III", "VI", "VII" };

			this.customerStyleTableView = new UITableView (new CoreGraphics.CGRect (300, 50, 200, 600), UITableViewStyle.Grouped);
			this.customerStyleTableView.Source = new CustomerGroupSource (titleList, Alist, NumList,MIList);

			this.View.AddSubview(this.customerStyleTableView);

			this.customerAccessory = new UITableView (new CoreGraphics.CGRect (600, 50, 400, 600), UITableViewStyle.Grouped);
			this.customerAccessory.Source = new CustomerGroupSource (titleList, Alist, NumList,MIList);
			this.View.AddSubview (this.customerAccessory);

			usw = new UISwitch (new CoreGraphics.CGRect (820, 50, 50, 20));

			usw.ValueChanged += CSw_ValueChange;
			this.View.AddSubview (usw);
		}

		public void CSw_ValueChange(object sender,EventArgs e)
		{
			UIAlertView alert = new UIAlertView ();
			alert.Title="Message";
			alert.Message = ((((UISwitch)sender).Selected == true) ? "true" : "false");
			Console.WriteLine (sender);
			alert.AddButton ("OK");
			alert.Show ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		private class CustomerGroupSource:UITableViewSource
		{
			List<string> _title;
			List<string>_AList;
			List<string>_numList;
			List<string>_miList;
			public CustomerGroupSource(List<string> paraTitle,List<string> paraAlist,List<string>paraNumList,List<string>paraMIList)
			{
				_title=paraTitle;
				_AList=paraAlist;
				_numList=paraNumList;
				_miList=paraMIList;
			}
			public override nint NumberOfSections (UITableView tableView)
			{
				return _title.Count;
			}
			public override nint RowsInSection (UITableView tableview, nint section)
			{
				switch (section) {
				case 0:
				case 1:
					{
						return _AList.Count;
					}
				case 2:
					{
						return _numList.Count;
					}
				case 3:
				default:
					{
						return _miList.Count;
					}
				}
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				Console.WriteLine (DateTime.Now.ToString ());
				if (tableView.Frame.X != 600) {
					switch (indexPath.Section) {
					case 0:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Subtitle, null);
							cell.TextLabel.Text = _AList [indexPath.Row];
							cell.DetailTextLabel.Text = _AList [indexPath.Row];
							switch (indexPath.Row) {
							case 0:
								{
									cell.Accessory = UITableViewCellAccessory.None;
									break;
								}
							case 1:
								{
									cell.Accessory = UITableViewCellAccessory.Checkmark;
									break;
								}
							case 2:
								{
									cell.Accessory = UITableViewCellAccessory.DetailButton;

									break;
								}
							case 3:
								{
									cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
									break;
								}
							case 4:
								{
									cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
									break;
								}
							}
							return cell;
						}
					case 1:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value1, null);
							cell.TextLabel.Text = _AList [indexPath.Row].ToLower ();
							cell.DetailTextLabel.Text = _AList [indexPath.Row].ToLower ();
							return cell;
						}
					case 2:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value2, null);
							cell.TextLabel.Text = _numList [indexPath.Row];
							cell.DetailTextLabel.Text = _numList [indexPath.Row];
							return cell;
						}
					case 3:
					default:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Default, null);
							cell.TextLabel.Text = _miList [indexPath.Row];
							cell.ImageView.Image = new UIImage ("1_130124110644_3.png");
							return cell;
						}
					}
				} else {
					switch (indexPath.Section) {
					case 0:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Subtitle, null);
							cell.TextLabel.Text = _AList [indexPath.Row];
							cell.DetailTextLabel.Text = _AList [indexPath.Row];
							switch (indexPath.Row) {
							case 0:
								{
									//cell.Accessory = UITableViewCellAccessory.None;
									UISwitch sw = new UISwitch (new CoreGraphics.CGRect (0, 0, 50, 20));
									sw.Selected = true;
									sw.ValueChanged += this.Sw_ValueChange;
									//sw.AddTarget(Sw_ValueChange);
									cell.AccessoryView = sw;

									break;
								}
							case 1:
								{
									//cell.Accessory = UITableViewCellAccessory.Checkmark;
									UIView vi=new UIView(new CoreGraphics.CGRect(0,0,350,50));
									UIImageView imgv = new UIImageView (new CoreGraphics.CGRect(0, 0, 100, 50));
									imgv.Image=new UIImage("1_130124110644_3.png");

									UILabel lbl = new UILabel (new CoreGraphics.CGRect (110, 0, 100, 30));
									lbl.BackgroundColor = UIColor.Gray;
									lbl.Text="My Label";

									UIButton btn = new UIButton (UIButtonType.System);
									btn.Frame = new CoreGraphics.CGRect (220, 0, 40, 30);
									btn.SetTitle ("OK", UIControlState.Normal);
									btn.TouchUpInside += CellBtn_TouchUpInside;
									vi.AddSubviews (imgv, lbl, btn);
									cell.AccessoryView = vi;

									break;
								}
							}
							return cell;
						}
					case 1:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value1, null);
							cell.TextLabel.Text = _AList [indexPath.Row].ToLower ();
							cell.DetailTextLabel.Text = _AList [indexPath.Row].ToLower ();
							return cell;
						}
					case 2:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value2, null);
							cell.TextLabel.Text = _numList [indexPath.Row];
							cell.DetailTextLabel.Text = _numList [indexPath.Row];
							return cell;
						}
					case 3:
					default:
						{
							UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Default, null);
							cell.TextLabel.Text = _miList [indexPath.Row];
							cell.ImageView.Image = new UIImage ("1_130124110644_3.png");
							return cell;
						}
					}
				}
			}

			public void CellBtn_TouchUpInside(object sender,EventArgs e)
			{
				UIAlertView alert = new UIAlertView ();
				alert.Title="Message";
				alert.Message = (((UIButton)sender).Title (UIControlState.Normal));
				Console.WriteLine (sender);
				alert.AddButton ("OK");
				alert.Show ();
			}

			public void Sw_ValueChange(object sender,EventArgs e)
			{
				UIAlertView alert = new UIAlertView ();
				alert.Title="Message";
				alert.Message = ((((UISwitch)sender).On == true) ? "true" : "false");
				Console.WriteLine (sender);
				alert.AddButton ("OK");
				alert.Show ();
			}

			public override string TitleForHeader (UITableView tableView, nint section)
			{
				return _title [(int)section];
			}

			public override nfloat GetHeightForHeader (UITableView tableView, nint section)
			{
				return 30;
			}
			public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				if (indexPath.Section == 1) {
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.Checkmark;
				} else {
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
			}

		}

		private class CustomerDataSource : UITableViewSource
		{
			private List<string> datas;
			public CustomerDataSource(List<string> para)
			{
				datas=para;
			}
			//setting row
			public override nint RowsInSection (UITableView tableView, nint section)
			{
				return datas.Count;
			}
			//every cell show context
			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value1, null);
				cell.TextLabel.Text = datas [indexPath.Row];
				cell.ImageView.Image = new UIImage ("1_130124110644_3.png");
				return cell;
			}

			public override string TitleForHeader (UITableView tableView, nint section)
			{
				return "Test Header";
			}

			public override string TitleForFooter (UITableView tableView, nint section)
			{
				return "Test Footer";
			}

			public override nfloat GetHeightForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return 60;
			}

		}
	}


}

