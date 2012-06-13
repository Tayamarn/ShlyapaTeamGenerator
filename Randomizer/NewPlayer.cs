using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Randomizer
{
	public partial class NewPlayer : Form
	{
		public NewPlayer()
		{
			InitializeComponent();
			ActiveControl = tbName;
			tbName.Focus();
		}

		

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (Check())
			{
				Player player = new Player();
				player.Id = Model.Players.Last().Id + 1;
				player.Name = tbName.Text;
				player.Score = 0;
				Model.AddPlayer(player);
				Close();
			}
		}

		private bool Check()
		{
			if (String.IsNullOrEmpty(tbName.Text.Trim()))
			{
				MessageBox.Show("Укажите имя игрока", "Ошибка");
				return false;
			}
			return true;
		}
	}
}
