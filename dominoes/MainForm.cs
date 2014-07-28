/*
 * Created by SharpDevelop.
 * User: Freddie
 * Date: 26/07/2014
 * Time: 13:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace dominoes
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		public class layout
		{
			private object lck = new object();
			private List<int[,]> old = new List<int[,]>();
			
			public static int p=255,P=0xFF00,m=15,M=240, // might be able to inline a couple of these
			w=1,e=2,d=4,c=8,x=16,z=32,a=64,q=128, // most of these are reusable
			W=131,E=7,D=14,C=28,X=56,Z=X*2,A=Z*2,Q=193, // most (all?) of these are reusable
			Y=w+x,U=a+d,N=c+q,B=z+e; // one of these atleast is pre-evalable
			// recognise this?
			private int t=-1,f=-1,k,u,i=-1,j,J,K,o,O,b;
			
			private int[,]T;
			
			public void store()
			{
				lock (lck)
				{
					if (old.Count > 64)
						old.RemoveAt(0);
						
					var nt = new int[f,t];
					old.Add(nt);
					
					for(i=0;i<f;i++) // values of i and j don't matter, just counters
					{
						for(j=0;j<t;j++) // increment done 3down
						{
							nt[i,j] = T[i,j];
						}
					}
				}
			}
			
			public void revert()
			{
				lock (lck)
				{
					if (old.Count > 0)
					{
						int idx = old.Count - 1;
						T = old[idx];
						old.RemoveAt(idx);
						f=T.GetLength(0);
						t=T.GetLength(1);
					}
				}
			}
			
			public int this[Point p]
			{
				get
				{
					return this[p.X, p.Y];
				}
				set
				{
					this[p.X, p.Y] = value;
				}
			}
			
			// will only make it larger without crashing
			public void resize(int asx, int asy, int aex, int aey)
			{
				lock (lck)
				{
					int[,] nT = new int[asx + aex + f, asy + aey + t];
					
					for (j=Math.Max(0, -asx);j<Math.Min(f, f+aex);j++) // careful!!
					{
						for (k=Math.Max(0, -asy);k<Math.Min(t, t+aey);k++) // careful!!
						{
							nT[j+asx, k+asy] = T[j, k];
						}
					}
					
					f += asx + aex;
					t += asy + aey;
					
					T = nT;
				}
			}
			
			public void trim(out int dx, out int dy)
			{
				lock (lck)
				{
					int sx = -1, sy = -1, ex = -1, ey = -1;
					
					for (j=0;j<f;j++) // careful!!
					{
						for (k=0;k<t;k++) // careful!!
						{
							if (T[j, k] != 0)
							{
								if (sx == -1 || j < sx)
									sx = j;
								if (ex == -1 || j > ex)
									ex = j;
								if (sy == -1 || k < sy)
									sy = k;
								if (ey == -1 || k > ey)
									ey = k;
							}
						}
					}
					
					if (ex < f-1 || sx > 0 || ey < t-1 || sy > 0)
						resize(-sx, -sy, ex - f + 1, ey - t + 1);
					dy = -sx;
					dx = -sy;
				}
			}
			
			public void ensure(int x, int y, out int dx, out int dy)
			{
				lock (lck)
				{
					dx = x < 0 ? -x : 0;
					dy = y < 0 ? -y : 0;
					if (x >= t || x < 0 || y >= f || y < 0)
					{
						resize(y < 0 ? -y : 0, x < 0 ? -x : 0, y >= f ? y - f + 1 : 0, x >= t ? x - t + 1 : 0);
					}
				}
			}
			
			public void ensure(int sx, int sy, int ex, int ey, out int dx, out int dy)
			{
				lock (lck)
				{
					dx = sx < 0 ? -sx : 0;
					dy = sy < 0 ? -sy : 0;
					if (ex >= t || sx < 0 || ey >= f || sy < 0)
					{
						resize(sy < 0 ? -sy : 0, sx < 0 ? -sx : 0, ey >= f ? ey - f + 1 : 0, ex >= t ? ex - t + 1 : 0);
					}
				}
			}
			
			public int this[int x, int y]
			{
				get
				{
					lock (lck)
					{
						if (x >= t || x < 0 || y >= f || y < 0)
							return 0;
						else
							return T[y, x];
					}
				}
				set
				{
					lock (lck)
					{
						if (x >= t || x < 0 || y >= f || y < 0)
						{
						}
						else
							T[y, x] = value;
					}
				}
			}
			
			public int toi(int K)
			{
				return
				// fallen
				(k=='W'?W:k=='E'?E:k=='D'?D:k=='C'?C:k=='X'?X:k=='Z'?Z:k=='A'?A:k=='Q'?Q:
				// dominos
				k==' '?0:k=='-'?Y:k=='/'?N:k=='|'?U:B // ASCII, order for >
				)*(k>64&k<91?1:257);
			}
			
			public void readin(System.IO.TextReader L)
			{
				lock (lck)
				{
					t=-1;f=-1;i=-1;
					
					for(;t<0;)
						for(t=f,f=0;(k=L.Read())>47;)
							f=f*10+k-48;
					
					T=new int[f,t]; // main arr
							
					// input
					for(;++i<f;) // values of i and j don't matter, just counters
					{
						for(j=0;j<t;) // increment done 3down
						{
							k=L.Read();
							T[i,j++]=(toi(k));
						}
					}
				}
			}
			
			public char toc(int K)
			{
				return K<0?'#':K==W?'W':K==E?'E':K==D?'D':K==C?'C':K==X?'X':K==Z?'Z':K==A?'A':K==Q?'Q':(K=K>>8)==0?' ':K==Y?'-':K==N?'/':K==U?'|':K==B?'\\':'!';
			}
			
			public void writeout(System.IO.TextWriter L)
			{
				lock (lck)
				{
					L.Write(t + " " + f + " ");
					
					for (o=0;o<f;o++)
					{
						for (j=0;j<t;j++)
						{
							k=T[o,j];
							L.Write(toc(k)); // order for >
						}
					}
				}
			}
			
			// main
			public bool iter()
			{
				lock (lck)
				{
					// back to important stuff
					i=0; // set me to 1 if we do anything (8 in golfed version due to E being 7)
					
					// move motions
					for (j=0;j<f;j++) // careful!!
					{
						for (k=0;k<t;k++) // careful!!
						{
							O=o=T[j,k];
							if (o>0&o<p) // we are motion
							{
								T[j,k]=-1; // do this so we can't skip it
								
								K=k+(i=((o&d)>1?1:(o&a)>0?-1:0));
								J=j+(u=((o&x)>1?1:(o&w)>0?-1:0));
								
								System.Action v=()=>{
									if(J>=0&J<f&K>=0&K<t&&(b=T[J,K])>p&&((b>>8)&O)>0)
									{
										T[J,K]&=(P|O);
									}
								};
								
								v();
								if (i!=0&u!=0)
								{
									K=i==u?k:K; // k+i == K
									J=i==u?J:j; // j+u == J
									O=(((o&q)>0?w:0)+o*2)&o;
									v();
									
									K=K==k?k+i:k;
									J=J==j?j+u:j;
									O=(((o&w)>0?q:0)+o/2)&o;
									v();
								}
								
								i=1;
							}
						}
					}
					
					// move dominos
					for (j=0;j<f;j++) // careful!!
					{
						for (k=0;k<t;k++) // careful!!
						{
							o=T[j,k];
							if (o>p) // we are domino
							{
								o=o&p;
								if ((o&m)<1!=(o&M)<1)
								{ // we have motion
									T[j,k]=o==w?W:o==q?Q:o+o/2+o*2;
								}
							}
						}
					}
					
					return i > 0;
				}
			}
			
			public void draw(Graphics g)
			{
				lock (lck)
				{
					if (T != null)
					{
						Font font1 = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 0.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
						
						for (j=0;j<f;j++) // careful!!
						{
							for (k=0;k<t;k++) // careful!!
							{
								g.DrawString(""+toc(T[j, k]), font1, Brushes.Black, k, j);
							}
						}
					}
				}
			}
		}
		
		layout lay = new layout();
		
		string mouseMode = "box";
		
		int ox = 0, oy = 0;
		int cx = 8, cy = 14;
		int mouseDown = 0;
		Point lmp;
		bool lmc;
		Point cmp;
		bool cmc;
		Point mdp;
		Point mmp;
		
		bool movewith = true;
		bool twoWayFreeForm = true;
		
		List<Point> prevs = new List<Point>();
		
		int getdir(int x, int y)
		{
			int rx = 0;
			int ry = 0;
			
			if (x == 1)
				rx = layout.D;
			if (x == 0)
				rx = layout.Y;
			if (x == -1)
				rx = layout.A;
			if (y == 1)
				ry = layout.X;
			if (y == 0)
				ry = layout.U;
			if (y == -1)
				ry = layout.W;
			
			return rx & ry;
		}
		
		int getPickup(int dir)
		{
			return dir==layout.w?layout.W:dir==layout.q?layout.Q:dir+dir/2+dir*2;
		}
		
		int geti(int x, int y, bool pickup)
		{
			int r = getdir(x, y);
			if (pickup)
			{
				r = getPickup(r);
			}
			else
			{
				r += (r<<4) + (r>>4);
				r = (r&255) * 257;
			}
			return r;
		}
		
		Point mtrans(int mx, int my, out bool clear)
		{
			mx += ox;
			my += oy;
			
			if (mx < 0)
				mx -= cx + 1;
			if (my < 0)
				my -= cy + 1;
			
			clear = mx % cx >= cx / 4 && mx % cx < cx * 3 / 4
				&& my % cy >= cy / 4 && my % cy < cy * 3 / 4;
			
			int x = mx / cx;
			int y = my / cy;
			
			return new Point(x, y);
		}
		
		void updateSelF()
		{
			if (mouseMode == "point")
			{
				selF.Text = "Cursor at " + cmp.X + ", " + cmp.Y;
			}
			else if (mouseMode == "box")
			{
				selF.Text = "Selection " + Math.Min(cmp.X, lmp.X) + ", " + Math.Min(cmp.Y, lmp.Y) + ", " + (Math.Abs(cmp.X - lmp.X) + 1) + ", " + (Math.Abs(cmp.Y - lmp.Y) + 1);
			}
			selF.Invalidate();
		}
		
		void ViewFMouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = e.Button == MouseButtons.Left ? 1 : e.Button == MouseButtons.Right ? 2 : 3;

			if (mouseDown != 3)
			{
				lmp = (cmp = mtrans(e.X, e.Y, out lmc));
			}
			
			mmp = (mdp = e.Location);
			
			updateSelF();
			viewF.Invalidate();
		}
		
		void ViewFMouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = 0;
			prevs.Clear();
			
			viewF.Invalidate();
		}
		
		void ViewFMouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown == 0)
				return;
			
			if (mouseDown == 3)
			{
				// pan
				ox -= e.X - mmp.X;
				oy -= e.Y - mmp.Y;
			}
			else
			{
				cmp = mtrans(e.X, e.Y, out cmc);
				
				if (mouseMode == "point")
				{
					if (cmp != lmp)
					{
						int r = geti(cmp.X - lmp.X, cmp.Y - lmp.Y, mouseDown == 2);
							
						if (prevs.Count > 0)
						{
							Point p = prevs[prevs.Count - 1];
							int rp = geti(cmp.X - p.X, cmp.Y - p.Y, mouseDown == 2);
							if (rp != 0)
							{
								if (prevs.Count > 1)
								{
									Point cp = prevs[prevs.Count - 2];
									int rpc = geti(p.X - cp.X, p.Y - cp.Y, true);
									
									if ((rpc & rp) > 0 && !twoWayFreeForm)
									{
										r = rp;
										lmp = p;
										goto done;
									}
									else
									{
//										rp = geti(cmp.X - lmp.X, cmp.Y - lmp.Y, mouseDown == 2);
//										rpc = geti(lmp.X - p.X, lmp.Y - p.Y, true);
//										
//										if ((rp & rpc) > 0)
//										{
//											r = rp;
//										}
//										else
//										{
//											rp = geti(cmp.X - lmp.X, cmp.Y - lmp.Y, true);
//											
//											if ((rp & rpc) > 0)
//											{
//												r = (rp & rpc);
//												if (mouseDown == 2)
//												{
//													r = ((r >> 4) + (r << 4)) & 255;
//													r = getPickup(r);
//												}
//												else
//												{
//													r = (r + (r >> 4) + (r << 4)) & 255;
//													r *= 257;
//												}
//											}
//										}
									}
								}
								
								if (twoWayFreeForm)
								{
									rp = geti(cmp.X - lmp.X, cmp.Y - lmp.Y, mouseDown == 2);
									int rpc = geti(lmp.X - p.X, lmp.Y - p.Y, true);
									
									if ((rp & rpc) > 0)
									{
										r = rp;
									}
									else
									{
										rp = geti(cmp.X - lmp.X, cmp.Y - lmp.Y, true);
										
										if ((rp & rpc) > 0)
										{
											r = (rp & rpc);
											if (mouseDown == 2)
											{
												r = ((r >> 4) + (r << 4)) & 255;
												r = getPickup(r);
											}
											else
											{
												r = (r + (r >> 4) + (r << 4)) & 255;
												r *= 257;
											}
										}
									}
								}
								else if (!twoWayFreeForm)
								{
									r = rp;
									lmp = p;
								}
							}
						}
						
					done:
						ensuremp();
						lay[lmp.X, lmp.Y] = r;
						
						if (prevs.Count > 1)
							prevs.RemoveRange(0, prevs.Count - 1);
						prevs.Add(lmp);
			
						lmc = cmc;
						lmp = cmp;
					}
				}
				else if (mouseMode == "box")
				{
				}
			
				updateSelF();
			}
			
			mmp = e.Location;
			
			//viewF.Invalidate(new Region(new Rectangle(e.X - cx * 2, e.Y - cx * 2, cx * 4, cy * 4)));
			viewF.Invalidate();
		}
		
		void ViewFPaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.TranslateTransform(-ox, -oy);
			g.ScaleTransform(cx, cy);
			
			g.Clear(Color.White);
			
			if (mouseMode == "point")
		    {
		    	g.FillRectangle(Brushes.Red, cmp.X, cmp.Y, 1, 1);
				if (mouseDown == 1 || mouseDown == 2)
				{
					g.FillRectangle(Brushes.Maroon, lmp.X, lmp.Y, 1, 1);
				}
		    }
			else if (mouseMode == "box")
			{
				if (cpy != null && cpy.Length > 0)
				{
					g.FillRectangle(Brushes.Beige, Math.Min(cmp.X, lmp.X), Math.Min(cmp.Y, lmp.Y), cpy.GetLength(0), cpy.GetLength(1));
				}
				g.FillRectangle(Brushes.Red, Math.Min(cmp.X, lmp.X), Math.Min(cmp.Y, lmp.Y), Math.Abs(cmp.X - lmp.X) + 1, Math.Abs(cmp.Y - lmp.Y) + 1);
			}
			
			lay.draw(g);
		}
		
		IEnumerable<Point> box
		{
			get
			{
				for (int i = Math.Min(cmp.X, lmp.X); i <= Math.Max(cmp.X, lmp.X); i++)
				{
					for (int j = Math.Min(cmp.Y, lmp.Y); j <= Math.Max(cmp.Y, lmp.Y); j++)
					{
						yield return new Point(i, j);
					}
				}
			}
		}
		
		void setness(int x, int y, bool pickup)
		{
			setness(geti(x, y, pickup));
			if (mouseMode == "point" || cmp == lmp)
			{
				if (movewith)
				{
					cmp.X += x;
					cmp.Y += y;
					lmp = cmp;
				}
			}
		}
		
		void ensuremp()
		{
			int dx, dy;
			lay.ensure(Math.Min(cmp.X, lmp.X), Math.Min(cmp.Y, lmp.Y), Math.Max(cmp.X, lmp.X), Math.Max(cmp.Y, lmp.Y), out dx, out dy);
			ox += dx * cx;
			cmp.X += dx;
			lmp.X += dx;
			oy += dy * cy;
			cmp.Y += dy;
			lmp.Y += dy;
		}
		
		void setness(int r)
		{
			if (mouseMode == "point")
			{
				lay.store();
				ensuremp();
				lay[cmp] = r;
			}
			else if (mouseMode == "box")
			{
				lay.store();
				ensuremp();
				foreach (Point pb in box)
					lay[pb] = r;
			}
		}
		
		int[,] cpy;
		
		void ViewFKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Left:
					cmp.X--;
					lmp.X--;
					break;
				case Keys.Right:
					cmp.X++;
					lmp.X++;
					break;
				case Keys.Up:
					cmp.Y--;
					lmp.Y--;
					break;
				case Keys.Down:
					cmp.Y++;
					lmp.Y++;
					break;
					
				case Keys.W:
					setness(0, -1, e.Shift);
					break;
				case Keys.E:
					setness(1, -1, e.Shift);
					break;
				case Keys.D:
					setness(1, 0, e.Shift);
					break;
				case Keys.C:
					setness(1, 1, e.Shift);
					break;
				case Keys.X:
					setness(0, 1, e.Shift);
					break;
				case Keys.Z:
					setness(-1, 1, e.Shift);
					break;
				case Keys.A:
					setness(-1, 0, e.Shift);
					break;
				case Keys.Q:
					setness(-1, -1, e.Shift);
					break;
				case Keys.S:
					setness(-1);
					break;
				case Keys.Space:
					setness(0);
					break;
					
				case Keys.M:
					movewith = !movewith;
					break;
				case Keys.T:
					twoWayFreeForm = !twoWayFreeForm;
					break;
				case Keys.P:
					mouseMode = "point";
					updateSelF();
					break;
				case Keys.B:
					mouseMode = "box";
					updateSelF();
					break;
				case Keys.Y:
					if (mouseMode == "box")
					{
						cpy = new int[Math.Abs(cmp.X - lmp.X) + 1, Math.Abs(cmp.Y - lmp.Y) + 1];
						int sx = Math.Min(cmp.X, lmp.X);
						int sy = Math.Min(cmp.Y, lmp.Y);
						for (int i = 0; i < Math.Abs(cmp.X - lmp.X) + 1; i++)
						{
							for (int j = 0; j < Math.Abs(cmp.Y - lmp.Y) + 1; j++)
							{
								cpy[i, j] = lay[i + sx, j + sy];
							}
						}
					}
					break;
				case Keys.U:
					if (mouseMode == "box" && cpy != null)
					{
						ensuremp();
						int sx = Math.Min(cmp.X, lmp.X);
						int sy = Math.Min(cmp.Y, lmp.Y);
						for (int i = 0; i < Math.Min(Math.Abs(cmp.X - lmp.X) + 1, cpy.GetLength(0)); i++)
						{
							for (int j = 0; j < Math.Min(Math.Abs(cmp.Y - lmp.Y) + 1, cpy.GetLength(1)); j++)
							{
								lay[i + sx, j + sy] = cpy[i, j];
							}
						}
					}
					break;
				case Keys.Escape:
					cpy = null;
					break;
				case Keys.Back:
					lay.revert();
					break;
					
				case Keys.F5:
					lay.store();
					f5 = !f5;
					break;
				case Keys.F6:
					lay.store();
					lay.iter();
					viewF.Invalidate();
					break;
			}
			
			viewF.Invalidate();
		}
		
		bool runiters(int iters)
		{
			for (int i = 0; i < iters; i++)
			{
				if (lay.iter() == false)
				{
					return false;
				}
			}
			
			return true;
		}
		
		int itrs = 1;
		int slp = 100;
		bool f5 = false;
		void fiver()
		{
		again:
			if (!f5)
				goto wait;
			
			if (runiters(itrs) == false)
				f5 = false;
			
			this.Invoke(new Action(viewF.Invalidate));
			
		wait:
			System.Threading.Thread.Sleep(slp);
			
			goto again;
		}
		
		void ReadBtnClick(object sender, EventArgs e)
		{
			lay.readin(new System.IO.StringReader(txt.Text));
			viewF.Invalidate();
			ox = 0;
			oy = 0;
		}
		
		void WriteBtnClick(object sender, EventArgs e)
		{
			System.IO.StringWriter sw = new System.IO.StringWriter();
			lay.writeout(sw);
			txt.Text = sw.ToString();
		}
		
		void IterBtnClick(object sender, EventArgs e)
		{
			lay.iter();
			viewF.Invalidate();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			lay.readin(new System.IO.StringReader("1 1  "));
			viewF.Invalidate();
			txt.Text = "1 1  ";
			new Action(fiver).BeginInvoke(null, null);
		}
		
		void ClearBtnClick(object sender, EventArgs e)
		{
			lay.readin(new System.IO.StringReader("1 1  "));
			viewF.Invalidate();
			ox = 0;
			oy = 0;
		}
		
		void TrimBtnClick(object sender, EventArgs e)
		{
			
			int dx, dy;
			lay.trim(out dx, out dy);
			ox += dx * cx;
			cmp.X += dx;
			lmp.X += dx;
			oy += dy * cy;
			cmp.Y += dy;
			lmp.Y += dy;
			
			viewF.Invalidate();
		}
	}
}