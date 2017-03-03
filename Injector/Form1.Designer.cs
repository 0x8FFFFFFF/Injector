namespace Injector
{
    partial class Form_Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.openFileDialogDll = new System.Windows.Forms.OpenFileDialog();
            this.listViewDll = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddDll = new System.Windows.Forms.Button();
            this.buttonDelDll = new System.Windows.Forms.Button();
            this.buttonDelAllDll = new System.Windows.Forms.Button();
            this.buttonLoadDll = new System.Windows.Forms.Button();
            this.buttonUnloadDll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(12, 25);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(267, 21);
            this.comboBoxProcess.Sorted = true;
            this.comboBoxProcess.TabIndex = 0;
            this.comboBoxProcess.DropDown += new System.EventHandler(this.comboBoxProcess_DropDown);
            this.comboBoxProcess.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcess_SelectedIndexChanged);
            // 
            // listBoxLog
            // 
            this.listBoxLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 239);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(267, 108);
            this.listBoxLog.TabIndex = 1;
            this.listBoxLog.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxLog_DrawItem);
            // 
            // openFileDialogDll
            // 
            this.openFileDialogDll.Filter = "(*.dll)|*.dll";
            this.openFileDialogDll.Multiselect = true;
            // 
            // listViewDll
            // 
            this.listViewDll.Location = new System.Drawing.Point(12, 69);
            this.listViewDll.Name = "listViewDll";
            this.listViewDll.Size = new System.Drawing.Size(267, 62);
            this.listViewDll.TabIndex = 2;
            this.listViewDll.UseCompatibleStateImageBehavior = false;
            this.listViewDll.View = System.Windows.Forms.View.SmallIcon;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Процесс:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dll для загрузки:";
            // 
            // buttonAddDll
            // 
            this.buttonAddDll.Location = new System.Drawing.Point(12, 137);
            this.buttonAddDll.Name = "buttonAddDll";
            this.buttonAddDll.Size = new System.Drawing.Size(85, 23);
            this.buttonAddDll.TabIndex = 5;
            this.buttonAddDll.Text = "Добавить";
            this.buttonAddDll.UseVisualStyleBackColor = true;
            this.buttonAddDll.Click += new System.EventHandler(this.buttonAddDll_Click);
            // 
            // buttonDelDll
            // 
            this.buttonDelDll.Location = new System.Drawing.Point(103, 137);
            this.buttonDelDll.Name = "buttonDelDll";
            this.buttonDelDll.Size = new System.Drawing.Size(85, 23);
            this.buttonDelDll.TabIndex = 6;
            this.buttonDelDll.Text = "Удалить";
            this.buttonDelDll.UseVisualStyleBackColor = true;
            this.buttonDelDll.Click += new System.EventHandler(this.buttonDelDll_Click);
            // 
            // buttonDelAllDll
            // 
            this.buttonDelAllDll.Location = new System.Drawing.Point(194, 137);
            this.buttonDelAllDll.Name = "buttonDelAllDll";
            this.buttonDelAllDll.Size = new System.Drawing.Size(85, 23);
            this.buttonDelAllDll.TabIndex = 7;
            this.buttonDelAllDll.Text = "Удалить все";
            this.buttonDelAllDll.UseVisualStyleBackColor = true;
            this.buttonDelAllDll.Click += new System.EventHandler(this.buttonDelAllDll_Click);
            // 
            // buttonLoadDll
            // 
            this.buttonLoadDll.Location = new System.Drawing.Point(12, 167);
            this.buttonLoadDll.Name = "buttonLoadDll";
            this.buttonLoadDll.Size = new System.Drawing.Size(267, 23);
            this.buttonLoadDll.TabIndex = 8;
            this.buttonLoadDll.Text = "Загрузить в процесс dll";
            this.buttonLoadDll.UseVisualStyleBackColor = true;
            this.buttonLoadDll.Click += new System.EventHandler(this.buttonLoadDll_Click);
            // 
            // buttonUnloadDll
            // 
            this.buttonUnloadDll.Location = new System.Drawing.Point(12, 197);
            this.buttonUnloadDll.Name = "buttonUnloadDll";
            this.buttonUnloadDll.Size = new System.Drawing.Size(267, 23);
            this.buttonUnloadDll.TabIndex = 9;
            this.buttonUnloadDll.Text = "Выгрузить из процесса наши dll";
            this.buttonUnloadDll.UseVisualStyleBackColor = true;
            this.buttonUnloadDll.Click += new System.EventHandler(this.buttonUnloadDll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Инфо:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(191, 357);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(88, 13);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.CheatOn.ru";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(12, 357);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(93, 13);
            this.labelCopyright.TabIndex = 12;
            this.labelCopyright.Text = "Copyright ©  2017";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 379);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonUnloadDll);
            this.Controls.Add(this.buttonLoadDll);
            this.Controls.Add(this.buttonDelAllDll);
            this.Controls.Add(this.buttonDelDll);
            this.Controls.Add(this.buttonAddDll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewDll);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.comboBoxProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.Text = "Инжектор x64";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProcess;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.OpenFileDialog openFileDialogDll;
        private System.Windows.Forms.ListView listViewDll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddDll;
        private System.Windows.Forms.Button buttonDelDll;
        private System.Windows.Forms.Button buttonDelAllDll;
        private System.Windows.Forms.Button buttonLoadDll;
        private System.Windows.Forms.Button buttonUnloadDll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label labelCopyright;
    }
}

