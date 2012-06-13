using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Randomizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			
			InitializeComponent();
			UpdateMainGV();
		}

		public void UpdateMainGV()
		{
			Model.Select();
			MainGV.DataSource = null;
			MainGV.DataSource = Model.Players;
            MainGV.AllowUserToAddRows = false;
            
			MainGV.Columns["Id"].ReadOnly = true;
			MainGV.Columns["Name"].ReadOnly = true;
			//MainGV.Columns["AssignedGroup"].ReadOnly = true;
			MainGV.Refresh();
			
		}

		private void AddPlayer_Click(object sender, EventArgs e)
		{
			NewPlayer newPlayer = new NewPlayer();
			newPlayer.Closing += NewPlayerOnClosing;
			newPlayer.Show();
		}

		private void NewPlayerOnClosing(object sender, CancelEventArgs cancelEventArgs)
		{
			UpdateMainGV();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (MainGV.SelectedCells.Count == 1)
			{
				Model.DeleteByRow(MainGV.SelectedCells[0].OwningRow);
			}
			else if (MainGV.SelectedRows.Count == 1)
			{
				Model.DeleteByRow(MainGV.SelectedRows[0]);
			}
			else
			{
				MessageBox.Show("Выберите одного игрока", "Ошибка");
			}
			UpdateMainGV();
			
		}

		private void MainGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (MainGV.Columns[e.ColumnIndex].Name == "Score")
			{
				Model.UpdateScore((int)MainGV.Rows[e.RowIndex].Cells["Id"].Value,
				(int)MainGV.Rows[e.RowIndex].Cells["Score"].Value); 
				
			}
			UpdateMainGV();
		}

		private void MainGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (MainGV.Columns[e.ColumnIndex].Name == "Score")
			{
				int score;
				if (!Int32.TryParse(e.FormattedValue.ToString(),out score))
				{
					MessageBox.Show("Введите целое число", "Ошибка");
					e.Cancel = true;
				}
			}
		}

		private bool CheckNumOfPlayers()
		{
			if (MainGV.Rows.Count < 12)
			{
				MessageBox.Show("Нужно хотя бы 12 игроков", "Ошибка");
				return false;
			}
			if (MainGV.Rows.Count%2 != 0)
			{
				MessageBox.Show("Введите четное число игроков", "Ошибка");
				return false;
			}
			return true;
		}

		private void btnFormGroups_Click(object sender, EventArgs e)
		{
			if(!CheckNumOfPlayers())
			{return;}
			Random random = new Random();

			GenerateGroups(FormNumberOfGroups(Model.Players.Count));
		

			foreach (Player player in Model.Players)
			{
				player.random = random.NextDouble();
				player.AssignedGroup = null;
			}

			int i = -1;

            while ((++i)*(Model.Groups.Count) < Model.Players.Count)
            {
                int groupNo = i % (Model.Groups.Count);
                Model.Groups[groupNo].Players.Add(Model.Players[i * 2]);
                Model.Groups[groupNo].Players.Add(Model.Players[(i * 2) + 1]);
                Model.Players[i * 2].AssignedGroup = Model.Groups[groupNo];
                Model.Players[(i * 2) + 1].AssignedGroup = Model.Groups[groupNo];
            }


            //foreach (Player player in Model.Players)
            //{
            //    int groupNo = (++i)%(Model.Groups.Count);
            //    Model.Groups[groupNo].Players.Add(player);
            //    player.AssignedGroup = Model.Groups[i%(Model.Groups.Count - 1)];
            //}

            //Debug.WriteLine(Model.Groups.Count);
            //Debug.WriteLine(Model.Groups[0].Players.Count);
            //Debug.WriteLine(Model.Groups[1].Players.Count);

		}

		private int FormNumberOfGroups(int p)
		{
			if (p == 24)
				return 4; // чтобы было хорошее число людей на финале
			return ((p-1)/8)+1;
		}

		private void GenerateGroups(int p)
		{
            Model.Groups.Clear();
			for (int i = 1; i <= p; i++)
			{
				Model.Groups.Add(new Group{Id = i, Players = new List<Player>()});
			}
		}

        private void MainGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Debug.WriteLine(MainGV.Columns[e.ColumnIndex]);
            //MainGV.Sort(MainGV.Columns[e.ColumnIndex],ListSortDirection.Ascending);
        }

        
	}
}
