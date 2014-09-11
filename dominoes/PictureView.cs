/*
 * Created by SharpDevelop.
 * User: Freddie
 * Date: 26/07/2014
 * Time: 17:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace dominoes
{
	/// <summary>
	/// Description of PictureView.
	/// </summary>
	public partial class PictureView : PictureBox
	{
		public PictureView()
		{
			InitializeComponent();
		}
		
//		protected override void OnKeyDown(KeyEventArgs e)
//		{
//			KeyDown(e);
//		}
//		
	    protected override void OnMouseDown(MouseEventArgs e)
	    {
	        this.Focus();
	        base.OnMouseDown(e);
	    }
	    protected override bool IsInputKey(Keys keyData)
	    {
	    	return true;
	    }
	}
}
