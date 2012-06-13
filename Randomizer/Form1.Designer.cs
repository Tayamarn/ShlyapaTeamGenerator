namespace Randomizer
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MainGV = new System.Windows.Forms.DataGridView();
			this.AddPlayer = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.playersData = new Randomizer.PlayersData();
			this.playersDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnFormGroups = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.MainGV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playersData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playersDataBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// MainGV
			// 
			this.MainGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MainGV.Location = new System.Drawing.Point(12, 12);
			this.MainGV.Name = "MainGV";
			this.MainGV.Size = new System.Drawing.Size(588, 391);
			this.MainGV.TabIndex = 0;
			this.MainGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainGV_CellEndEdit);
			this.MainGV.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.MainGV_CellValidating);
			// 
			// AddPlayer
			// 
			this.AddPlayer.Location = new System.Drawing.Point(12, 409);
			this.AddPlayer.Name = "AddPlayer";
			this.AddPlayer.Size = new System.Drawing.Size(92, 23);
			this.AddPlayer.TabIndex = 1;
			this.AddPlayer.Text = "Новый игрок";
			this.AddPlayer.UseVisualStyleBackColor = true;
			this.AddPlayer.Click += new System.EventHandler(this.AddPlayer_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(110, 409);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(100, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Удалить игрока";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// playersData
			// 
			this.playersData.DataSetName = "PlayersData";
			this.playersData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// playersDataBindingSource
			// 
			this.playersDataBindingSource.DataSource = this.playersData;
			this.playersDataBindingSource.Position = 0;
			// 
			// btnFormGroups
			// 
			this.btnFormGroups.Location = new System.Drawing.Point(216, 409);
			this.btnFormGroups.Name = "btnFormGroups";
			this.btnFormGroups.Size = new System.Drawing.Size(100, 23);
			this.btnFormGroups.TabIndex = 3;
			this.btnFormGroups.Text = "По группам";
			this.btnFormGroups.UseVisualStyleBackColor = true;
			this.btnFormGroups.Click += new System.EventHandler(this.btnFormGroups_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 444);
			this.Controls.Add(this.btnFormGroups);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.AddPlayer);
			this.Controls.Add(this.MainGV);
			this.Name = "Form1";
			this.Text = "Команды для Шляпы";
			((System.ComponentModel.ISupportInitialize)(this.MainGV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playersData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playersDataBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView MainGV;
		private System.Windows.Forms.Button AddPlayer;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.BindingSource playersDataBindingSource;
		private PlayersData playersData;
		private System.Windows.Forms.Button btnFormGroups;
	}
}

