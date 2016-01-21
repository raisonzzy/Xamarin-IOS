using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using System.Linq;

namespace DataTableCore
{
	public partial class ViewController : UIViewController
	{
		UITableView dataTable;
		CustomerDataSource dataViewSource;
		UITextView txt;
		UILabel lblConsole;
		List<string> DataSourceList;
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			DataSourceList = new List<string> () {"itme1", "itme2", "itme3",
				"itme4", "itme5", "itme6", "itme7","itme8", "itme9", "itme10", "itme11", 
				"aaa", "bbb", "ccc", "dddd", "eeee", "fffff"
			};

			dataTable=new UITableView(new CoreGraphics.CGRect(50,100,500,600));
			dataTable.Source = new CustomerDataSource (DataSourceList);


			UIButton btnAdd=new UIButton(UIButtonType.System);
			btnAdd.Frame = new CoreGraphics.CGRect (600, 50, 100, 30);
			btnAdd.SetTitle ("Add New Row", UIControlState.Normal);
			btnAdd.TouchUpInside += BtnAdd_TouchUpInside;

			txt = new UITextView (new CoreGraphics.CGRect (600, 100, 200, 30));
			txt.BackgroundColor = UIColor.Gray;
			txt.KeyboardType = UIKeyboardType.NumberPad;

			UIButton btnDelete=new UIButton(UIButtonType.System);
			btnDelete.Frame = new CoreGraphics.CGRect (710, 50, 150, 30);
			btnDelete.SetTitle ("Delete Select Rows", UIControlState.Normal);
			btnDelete.TouchUpInside += BtnDelete_TouchUpInside;

			UIButton btnMove = new UIButton (UIButtonType.System);
			btnMove.Frame = new CoreGraphics.CGRect (600, 380, 200, 30);
			btnMove.SetTitle ("I want to move row", UIControlState.Normal);
			btnMove.TouchUpInside += BtnMove_TouchUpInside;

			this.View.AddSubviews (btnAdd, txt, btnDelete, btnMove);
			this.View.AddSubview (dataTable);
		}

		public void BtnMove_TouchUpInside(object sender,EventArgs e)
		{
			lblConsole = new UILabel (new CoreGraphics.CGRect (600, 300, 200, 30));
			lblConsole.BackgroundColor = UIColor.Gray;
			dataTable.Source = new MoveRowDataSource (DataSourceList, lblConsole);
			//开启行编辑状态，如果使用滑动删除则关闭此状态
			dataTable.SetEditing (true, true);
			this.View.AddSubview (lblConsole);
		}

		public void BtnDelete_TouchUpInside(object sender,EventArgs e)
		{
			dataViewSource = (CustomerDataSource)this.dataTable.Source;
			foreach (int i in dataViewSource.selectRows) {
				dataViewSource.datas.RemoveAt (i);

			}
			dataViewSource.selectRows = new List<int> ();
			dataTable.ReloadData();
		}

		public void BtnAdd_TouchUpInside(object sender,EventArgs e)
		{
			string txtValue = this.txt.Text;
			if (!string.IsNullOrEmpty (txtValue)) {
				dataViewSource = (CustomerDataSource)this.dataTable.Source;
				dataViewSource.datas.Insert (0, txtValue);
				//this.dataTable.InsertRows (new NSIndexPath[]{ NSIndexPath.FromRowSection (0, 0) }, UITableViewRowAnimation.Bottom);
				this.dataTable.ReloadData();
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		private class CustomerDataSource : UITableViewSource
		{
			public List<string> datas {
				get;
				set;
			}
			public List<int> selectRows {
				get;
				set;
			}

			public CustomerDataSource(List<string> para)
			{
				datas=para;
				selectRows=new List<int>();
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
				cell.DetailTextLabel.Text = DateTime.Now.ToString ();
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

			public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.CellAt (indexPath);
				if (cell.Accessory == UITableViewCellAccessory.None) {
					cell.Accessory = UITableViewCellAccessory.Checkmark;
					selectRows.Add (indexPath.Row);
				} else {
					cell.Accessory = UITableViewCellAccessory.None;
					tableView.DeselectRow (indexPath, true);
					selectRows.Remove (indexPath.Row);
				}
			}

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					datas.RemoveAt (indexPath.Row);
					tableView.DeleteRows (new Foundation.NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Right);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					datas.Insert (indexPath.Row, "new row " + indexPath.Row.ToString ());
					tableView.InsertRows(new Foundation.NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Left);
				}
			}
			//一般不对此方法重写，默认delete
			//此处为了实现移动行
			public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return UITableViewCellEditingStyle.None;
			}

			//缩进
			public override nint IndentationLevel (UITableView tableView, NSIndexPath indexPath)
			{
				return indexPath.Row % 5;
			}
			//
			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return datas.Select (m => Convert.ToString (m [0])).Distinct ().ToArray ();
			}
//			public override nint SectionFor (UITableView tableView, string title, nint atIndex)
//			{
//				tableView.RectForHeaderInSection (atIndex);
//				return atIndex;
//			}

		}

		private class MoveRowDataSource : UITableViewSource
		{
			public List<string> datas {
				get;
				set;
			}

			public UILabel _lbl {
				get;
				set;
			}

			public MoveRowDataSource(List<string> para,UILabel concl)
			{
				datas=para;
				_lbl=concl;
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
				cell.DetailTextLabel.Text = DateTime.Now.ToString ();
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

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					datas.RemoveAt (indexPath.Row);
					tableView.DeleteRows (new Foundation.NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Right);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					datas.Insert (indexPath.Row, "new row " + indexPath.Row.ToString ());
					tableView.InsertRows(new Foundation.NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Left);
				}
			}
			//一般不对此方法重写，默认delete
			//此处为了实现移动行
			public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return UITableViewCellEditingStyle.None;
			}
			//行移动
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
				_lbl.Text="From["+sourceIndexPath.Row.ToString()+"]To["+destinationIndexPath.Row.ToString()+"]";
				string cellValue = datas [sourceIndexPath.Row];
				datas.RemoveAt (sourceIndexPath.Row);
				datas.Insert (destinationIndexPath.Row, cellValue);
				tableView.ReloadData ();
			}
			//缩进
			public override nint IndentationLevel (UITableView tableView, NSIndexPath indexPath)
			{
				return indexPath.Row % 5;
			}
			//
			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return datas.Select (m => Convert.ToString (m [0])).Distinct ().ToArray ();
			}
		}
	}
}

