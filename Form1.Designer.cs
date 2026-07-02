namespace TransportSolver
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем все элементы управления
        private System.Windows.Forms.TextBox txtSupply;
        private System.Windows.Forms.TextBox txtDemand;
        private System.Windows.Forms.Label lblSupply;
        private System.Windows.Forms.Label lblDemand;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvCosts;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtSupply = new System.Windows.Forms.TextBox();
            this.txtDemand = new System.Windows.Forms.TextBox();
            this.lblSupply = new System.Windows.Forms.Label();
            this.lblDemand = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvCosts = new System.Windows.Forms.DataGridView();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCosts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();

            // 
            // lblSupply
            // 
            this.lblSupply.AutoSize = true;
            this.lblSupply.Location = new System.Drawing.Point(12, 15);
            this.lblSupply.Name = "lblSupply";
            this.lblSupply.Size = new System.Drawing.Size(77, 20);
            this.lblSupply.Text = "Запасы (A):";

            // 
            // txtSupply
            // 
            this.txtSupply.Location = new System.Drawing.Point(95, 12);
            this.txtSupply.Name = "txtSupply";
            this.txtSupply.Size = new System.Drawing.Size(250, 26);
            this.txtSupply.Text = "100 150 120";
            this.txtSupply.Font = new System.Drawing.Font("Consolas", 12F);

            // 
            // lblDemand
            // 
            this.lblDemand.AutoSize = true;
            this.lblDemand.Location = new System.Drawing.Point(370, 15);
            this.lblDemand.Name = "lblDemand";
            this.lblDemand.Size = new System.Drawing.Size(96, 20);
            this.lblDemand.Text = "Потребности (B):";

            // 
            // txtDemand
            // 
            this.txtDemand.Location = new System.Drawing.Point(472, 12);
            this.txtDemand.Name = "txtDemand";
            this.txtDemand.Size = new System.Drawing.Size(250, 26);
            this.txtDemand.Text = "80 120 170";
            this.txtDemand.Font = new System.Drawing.Font("Consolas", 12F);

            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(740, 8);
            this.btnLoad.Size = new System.Drawing.Size(120, 30);
            this.btnLoad.Text = "📂 Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(740, 44);
            this.btnCalculate.Size = new System.Drawing.Size(120, 30);
            this.btnCalculate.Text = "✅ Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(740, 80);
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Text = "💾 Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(740, 116);
            this.btnClear.Size = new System.Drawing.Size(120, 30);
            this.btnClear.Text = "🗑️ Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // dgvCosts (Тарифы)
            // 
            this.dgvCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCosts.Location = new System.Drawing.Point(12, 50);
            this.dgvCosts.Name = "dgvCosts";
            this.dgvCosts.Size = new System.Drawing.Size(350, 400);
            this.dgvCosts.TabIndex = 0;
            this.dgvCosts.AllowUserToAddRows = true;
            this.dgvCosts.AllowUserToDeleteRows = true;
            this.dgvCosts.Font = new System.Drawing.Font("Consolas", 12F);

            // 
            // dgvResult (Результат)
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(380, 50);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.Size = new System.Drawing.Size(350, 400);
            this.dgvResult.TabIndex = 1;
            this.dgvResult.ReadOnly = true;
            this.dgvResult.Font = new System.Drawing.Font("Consolas", 12F);
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;

            // 
            // lblStatus (Строка состояния)
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(12, 465);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 23);
            this.lblStatus.Text = "Статус: Ожидание";
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;

            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 500);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.dgvCosts);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtDemand);
            this.Controls.Add(this.lblDemand);
            this.Controls.Add(this.txtSupply);
            this.Controls.Add(this.lblSupply);
            this.Name = "Form1";
            this.Text = "Решение транспортной задачи (Метод С-З угла)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCosts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}


