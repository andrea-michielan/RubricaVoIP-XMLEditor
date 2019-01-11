namespace XmlEditor {
	partial class Form1 {
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Liberare le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.numberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.phone1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.phone2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.phone3Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.searchGroupBox = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.previousButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.searchItemGroupBox = new System.Windows.Forms.GroupBox();
			this.searchButton = new System.Windows.Forms.Button();
			this.searchTextBox = new System.Windows.Forms.TextBox();
			this.phoneNumberRadioButton = new System.Windows.Forms.RadioButton();
			this.nameRadioButton = new System.Windows.Forms.RadioButton();
			this.menuGroupBox = new System.Windows.Forms.GroupBox();
			this.deleteMenuButton = new System.Windows.Forms.Button();
			this.menuComboBox = new System.Windows.Forms.ComboBox();
			this.newMenuGroupBox = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.newMenuNameTextBox = new System.Windows.Forms.TextBox();
			this.addMenuButton = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.searchGroupBox.SuspendLayout();
			this.searchItemGroupBox.SuspendLayout();
			this.menuGroupBox.SuspendLayout();
			this.newMenuGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveAsToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(1051, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "toolStripButton1";
			this.newToolStripButton.ToolTipText = "Nuovo (CTRL + N)";
			this.newToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Apri (CTRL - O)";
			this.openToolStripButton.ToolTipText = "Apri (CTRL + O)";
			this.openToolStripButton.Click += new System.EventHandler(this.apriToolStripButton_Click);
			// 
			// saveAsToolStripButton
			// 
			this.saveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveAsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripButton.Image")));
			this.saveAsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveAsToolStripButton.Name = "saveAsToolStripButton";
			this.saveAsToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveAsToolStripButton.Text = "toolStripButton2";
			this.saveAsToolStripButton.ToolTipText = "Salva con nome (CTRL + F12)";
			this.saveAsToolStripButton.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Salva (CTRL - S)";
			this.saveToolStripButton.ToolTipText = "Salva (CTRL + S)";
			this.saveToolStripButton.Click += new System.EventHandler(this.salvaToolStripButton_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Title = "Apertura file XML";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Title = "Salvataggio file XML";
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numberColumn,
            this.nameColumn,
            this.phone1Column,
            this.phone2Column,
            this.phone3Column});
			this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.Size = new System.Drawing.Size(826, 552);
			this.dataGridView1.TabIndex = 10;
			// 
			// numberColumn
			// 
			this.numberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.numberColumn.Frozen = true;
			this.numberColumn.HeaderText = "N";
			this.numberColumn.Name = "numberColumn";
			this.numberColumn.ReadOnly = true;
			this.numberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.numberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.numberColumn.Width = 24;
			// 
			// nameColumn
			// 
			this.nameColumn.HeaderText = "Nome";
			this.nameColumn.Name = "nameColumn";
			// 
			// phone1Column
			// 
			this.phone1Column.HeaderText = "Telefono 1";
			this.phone1Column.Name = "phone1Column";
			// 
			// phone2Column
			// 
			this.phone2Column.HeaderText = "Telefono 2";
			this.phone2Column.Name = "phone2Column";
			// 
			// phone3Column
			// 
			this.phone3Column.HeaderText = "Telefono 3";
			this.phone3Column.Name = "phone3Column";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.searchGroupBox);
			this.splitContainer1.Panel1.Controls.Add(this.menuGroupBox);
			this.splitContainer1.Panel1.Controls.Add(this.newMenuGroupBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainer1.Size = new System.Drawing.Size(1051, 556);
			this.splitContainer1.SplitterDistance = 217;
			this.splitContainer1.TabIndex = 2;
			// 
			// searchGroupBox
			// 
			this.searchGroupBox.Controls.Add(this.label2);
			this.searchGroupBox.Controls.Add(this.previousButton);
			this.searchGroupBox.Controls.Add(this.nextButton);
			this.searchGroupBox.Controls.Add(this.searchItemGroupBox);
			this.searchGroupBox.Controls.Add(this.phoneNumberRadioButton);
			this.searchGroupBox.Controls.Add(this.nameRadioButton);
			this.searchGroupBox.Location = new System.Drawing.Point(12, 259);
			this.searchGroupBox.Name = "searchGroupBox";
			this.searchGroupBox.Size = new System.Drawing.Size(200, 283);
			this.searchGroupBox.TabIndex = 8;
			this.searchGroupBox.TabStop = false;
			this.searchGroupBox.Text = "Ricerca contatti per";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 181);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 8;
			// 
			// previousButton
			// 
			this.previousButton.Location = new System.Drawing.Point(27, 245);
			this.previousButton.Name = "previousButton";
			this.previousButton.Size = new System.Drawing.Size(138, 23);
			this.previousButton.TabIndex = 9;
			this.previousButton.Text = "Precedente";
			this.previousButton.UseVisualStyleBackColor = true;
			this.previousButton.Click += new System.EventHandler(this.button5_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(27, 207);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(138, 23);
			this.nextButton.TabIndex = 8;
			this.nextButton.Text = "Successivo";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.button4_Click);
			// 
			// searchItemGroupBox
			// 
			this.searchItemGroupBox.Controls.Add(this.searchButton);
			this.searchItemGroupBox.Controls.Add(this.searchTextBox);
			this.searchItemGroupBox.Location = new System.Drawing.Point(6, 84);
			this.searchItemGroupBox.Name = "searchItemGroupBox";
			this.searchItemGroupBox.Size = new System.Drawing.Size(188, 85);
			this.searchItemGroupBox.TabIndex = 5;
			this.searchItemGroupBox.TabStop = false;
			this.searchItemGroupBox.Text = "Elemento da ricercare";
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(21, 54);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(138, 23);
			this.searchButton.TabIndex = 7;
			this.searchButton.Text = "Ricerca";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.button3_Click);
			// 
			// searchTextBox
			// 
			this.searchTextBox.Location = new System.Drawing.Point(21, 28);
			this.searchTextBox.Name = "searchTextBox";
			this.searchTextBox.Size = new System.Drawing.Size(138, 20);
			this.searchTextBox.TabIndex = 6;
			this.searchTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// phoneNumberRadioButton
			// 
			this.phoneNumberRadioButton.AutoSize = true;
			this.phoneNumberRadioButton.Location = new System.Drawing.Point(6, 53);
			this.phoneNumberRadioButton.Name = "phoneNumberRadioButton";
			this.phoneNumberRadioButton.Size = new System.Drawing.Size(114, 17);
			this.phoneNumberRadioButton.TabIndex = 5;
			this.phoneNumberRadioButton.TabStop = true;
			this.phoneNumberRadioButton.Text = "Numero di telefono";
			this.phoneNumberRadioButton.UseVisualStyleBackColor = true;
			// 
			// nameRadioButton
			// 
			this.nameRadioButton.AutoSize = true;
			this.nameRadioButton.Location = new System.Drawing.Point(6, 29);
			this.nameRadioButton.Name = "nameRadioButton";
			this.nameRadioButton.Size = new System.Drawing.Size(53, 17);
			this.nameRadioButton.TabIndex = 4;
			this.nameRadioButton.TabStop = true;
			this.nameRadioButton.Text = "Nome";
			this.nameRadioButton.UseVisualStyleBackColor = true;
			// 
			// menuGroupBox
			// 
			this.menuGroupBox.Controls.Add(this.deleteMenuButton);
			this.menuGroupBox.Controls.Add(this.menuComboBox);
			this.menuGroupBox.Location = new System.Drawing.Point(12, 3);
			this.menuGroupBox.Name = "menuGroupBox";
			this.menuGroupBox.Size = new System.Drawing.Size(200, 118);
			this.menuGroupBox.TabIndex = 7;
			this.menuGroupBox.TabStop = false;
			this.menuGroupBox.Text = "Menu";
			// 
			// deleteMenuButton
			// 
			this.deleteMenuButton.Location = new System.Drawing.Point(28, 73);
			this.deleteMenuButton.Name = "deleteMenuButton";
			this.deleteMenuButton.Size = new System.Drawing.Size(138, 23);
			this.deleteMenuButton.TabIndex = 1;
			this.deleteMenuButton.Text = "Elimina";
			this.deleteMenuButton.UseVisualStyleBackColor = true;
			this.deleteMenuButton.Click += new System.EventHandler(this.button2_Click);
			// 
			// menuComboBox
			// 
			this.menuComboBox.FormattingEnabled = true;
			this.menuComboBox.Location = new System.Drawing.Point(27, 31);
			this.menuComboBox.MaxDropDownItems = 10;
			this.menuComboBox.Name = "menuComboBox";
			this.menuComboBox.Size = new System.Drawing.Size(138, 21);
			this.menuComboBox.TabIndex = 0;
			this.menuComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// newMenuGroupBox
			// 
			this.newMenuGroupBox.Controls.Add(this.label1);
			this.newMenuGroupBox.Controls.Add(this.newMenuNameTextBox);
			this.newMenuGroupBox.Controls.Add(this.addMenuButton);
			this.newMenuGroupBox.Location = new System.Drawing.Point(12, 127);
			this.newMenuGroupBox.Name = "newMenuGroupBox";
			this.newMenuGroupBox.Size = new System.Drawing.Size(200, 126);
			this.newMenuGroupBox.TabIndex = 6;
			this.newMenuGroupBox.TabStop = false;
			this.newMenuGroupBox.Text = "Gruppo rubrica";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nome gruppo:";
			// 
			// newMenuNameTextBox
			// 
			this.newMenuNameTextBox.Location = new System.Drawing.Point(27, 51);
			this.newMenuNameTextBox.Name = "newMenuNameTextBox";
			this.newMenuNameTextBox.Size = new System.Drawing.Size(138, 20);
			this.newMenuNameTextBox.TabIndex = 2;
			this.newMenuNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// addMenuButton
			// 
			this.addMenuButton.AutoSize = true;
			this.addMenuButton.ForeColor = System.Drawing.Color.Black;
			this.addMenuButton.Location = new System.Drawing.Point(27, 77);
			this.addMenuButton.Name = "addMenuButton";
			this.addMenuButton.Size = new System.Drawing.Size(138, 25);
			this.addMenuButton.TabIndex = 3;
			this.addMenuButton.Text = "Aggiungi";
			this.addMenuButton.UseVisualStyleBackColor = true;
			this.addMenuButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
			this.progressBar1.Location = new System.Drawing.Point(883, 3);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(156, 18);
			this.progressBar1.Step = 1;
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(1051, 581);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Editor Rubrica";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.searchGroupBox.ResumeLayout(false);
			this.searchGroupBox.PerformLayout();
			this.searchItemGroupBox.ResumeLayout(false);
			this.searchItemGroupBox.PerformLayout();
			this.menuGroupBox.ResumeLayout(false);
			this.newMenuGroupBox.ResumeLayout(false);
			this.newMenuGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ComboBox menuComboBox;
		private System.Windows.Forms.Button addMenuButton;
		private System.Windows.Forms.TextBox newMenuNameTextBox;
		private System.Windows.Forms.GroupBox newMenuGroupBox;
		private System.Windows.Forms.GroupBox menuGroupBox;
		private System.Windows.Forms.Button deleteMenuButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn numberColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn phone1Column;
		private System.Windows.Forms.DataGridViewTextBoxColumn phone2Column;
		private System.Windows.Forms.DataGridViewTextBoxColumn phone3Column;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox searchGroupBox;
		private System.Windows.Forms.GroupBox searchItemGroupBox;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.RadioButton phoneNumberRadioButton;
		private System.Windows.Forms.RadioButton nameRadioButton;
		private System.Windows.Forms.Button previousButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar progressBar1;
	}
}

