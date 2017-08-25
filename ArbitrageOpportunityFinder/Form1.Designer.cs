namespace ArbitrageOpportunityFinder
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_getExchangeData = new System.Windows.Forms.Button();
            this.button_crossArbOpportunities = new System.Windows.Forms.Button();
            this.button_LoadData = new System.Windows.Forms.Button();
            this.button_SaveData = new System.Windows.Forms.Button();
            this.button_triArbOpportunities = new System.Windows.Forms.Button();
            this.button_UpdateTransactionData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Messages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_QuadArbOpportunities = new System.Windows.Forms.Button();
            this.button_PentArbOpportunities = new System.Windows.Forms.Button();
            this.button_GetTriArbTransactions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.CausesValidation = false;
            this.textBox1.Location = new System.Drawing.Point(1028, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(497, 782);
            this.textBox1.TabIndex = 0;
            // 
            // button_getExchangeData
            // 
            this.button_getExchangeData.Location = new System.Drawing.Point(26, 39);
            this.button_getExchangeData.Name = "button_getExchangeData";
            this.button_getExchangeData.Size = new System.Drawing.Size(161, 23);
            this.button_getExchangeData.TabIndex = 1;
            this.button_getExchangeData.Text = "Get Exchange Data";
            this.button_getExchangeData.UseVisualStyleBackColor = true;
            this.button_getExchangeData.Click += new System.EventHandler(this.button_getExchangeData_Click);
            // 
            // button_crossArbOpportunities
            // 
            this.button_crossArbOpportunities.Location = new System.Drawing.Point(26, 486);
            this.button_crossArbOpportunities.Name = "button_crossArbOpportunities";
            this.button_crossArbOpportunities.Size = new System.Drawing.Size(269, 30);
            this.button_crossArbOpportunities.TabIndex = 3;
            this.button_crossArbOpportunities.Text = "Cross Exchange Arbitrage Opportunities";
            this.button_crossArbOpportunities.UseVisualStyleBackColor = true;
            // 
            // button_LoadData
            // 
            this.button_LoadData.Location = new System.Drawing.Point(26, 190);
            this.button_LoadData.Name = "button_LoadData";
            this.button_LoadData.Size = new System.Drawing.Size(161, 23);
            this.button_LoadData.TabIndex = 4;
            this.button_LoadData.Text = "Load Arbitrage Data";
            this.button_LoadData.UseVisualStyleBackColor = true;
            // 
            // button_SaveData
            // 
            this.button_SaveData.Location = new System.Drawing.Point(26, 161);
            this.button_SaveData.Name = "button_SaveData";
            this.button_SaveData.Size = new System.Drawing.Size(161, 23);
            this.button_SaveData.TabIndex = 5;
            this.button_SaveData.Text = "Save Arbitrage Data";
            this.button_SaveData.UseVisualStyleBackColor = true;
            // 
            // button_triArbOpportunities
            // 
            this.button_triArbOpportunities.Location = new System.Drawing.Point(26, 450);
            this.button_triArbOpportunities.Name = "button_triArbOpportunities";
            this.button_triArbOpportunities.Size = new System.Drawing.Size(269, 30);
            this.button_triArbOpportunities.TabIndex = 6;
            this.button_triArbOpportunities.Text = "Triangular Arbitrage Opportunities";
            this.button_triArbOpportunities.UseVisualStyleBackColor = true;
            this.button_triArbOpportunities.Click += new System.EventHandler(this.button_triArbOpportunities_Click);
            // 
            // button_UpdateTransactionData
            // 
            this.button_UpdateTransactionData.Location = new System.Drawing.Point(26, 248);
            this.button_UpdateTransactionData.Name = "button_UpdateTransactionData";
            this.button_UpdateTransactionData.Size = new System.Drawing.Size(161, 23);
            this.button_UpdateTransactionData.TabIndex = 7;
            this.button_UpdateTransactionData.Text = "Refresh Transaction Data";
            this.button_UpdateTransactionData.UseVisualStyleBackColor = true;
            this.button_UpdateTransactionData.Click += new System.EventHandler(this.button_UpdateTransactionData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(707, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "This is used to get the blacklists, translation lists, fees, max/min volumes, tra" +
    "nsactions, and other essential data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(543, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Used to update the bid/ask data to current then recalculate the arbitage opportun" +
    "ites";
            // 
            // textBox_Messages
            // 
            this.textBox_Messages.Location = new System.Drawing.Point(652, 457);
            this.textBox_Messages.Multiline = true;
            this.textBox_Messages.Name = "textBox_Messages";
            this.textBox_Messages.Size = new System.Drawing.Size(370, 337);
            this.textBox_Messages.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(649, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Messages";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button_QuadArbOpportunities
            // 
            this.button_QuadArbOpportunities.Location = new System.Drawing.Point(301, 450);
            this.button_QuadArbOpportunities.Name = "button_QuadArbOpportunities";
            this.button_QuadArbOpportunities.Size = new System.Drawing.Size(75, 30);
            this.button_QuadArbOpportunities.TabIndex = 14;
            this.button_QuadArbOpportunities.Text = "Quad";
            this.button_QuadArbOpportunities.UseVisualStyleBackColor = true;
            // 
            // button_PentArbOpportunities
            // 
            this.button_PentArbOpportunities.Location = new System.Drawing.Point(382, 450);
            this.button_PentArbOpportunities.Name = "button_PentArbOpportunities";
            this.button_PentArbOpportunities.Size = new System.Drawing.Size(147, 30);
            this.button_PentArbOpportunities.TabIndex = 15;
            this.button_PentArbOpportunities.Text = "PentArb - Caution";
            this.button_PentArbOpportunities.UseVisualStyleBackColor = true;
            // 
            // button_GetTriArbTransactions
            // 
            this.button_GetTriArbTransactions.Location = new System.Drawing.Point(26, 413);
            this.button_GetTriArbTransactions.Name = "button_GetTriArbTransactions";
            this.button_GetTriArbTransactions.Size = new System.Drawing.Size(503, 31);
            this.button_GetTriArbTransactions.TabIndex = 16;
            this.button_GetTriArbTransactions.Text = "Generate Triangular Arbitrage Transactions";
            this.button_GetTriArbTransactions.UseVisualStyleBackColor = true;
            this.button_GetTriArbTransactions.Click += new System.EventHandler(this.button_GetTriArbTransactions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 806);
            this.Controls.Add(this.button_GetTriArbTransactions);
            this.Controls.Add(this.button_PentArbOpportunities);
            this.Controls.Add(this.button_QuadArbOpportunities);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Messages);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_UpdateTransactionData);
            this.Controls.Add(this.button_triArbOpportunities);
            this.Controls.Add(this.button_SaveData);
            this.Controls.Add(this.button_LoadData);
            this.Controls.Add(this.button_crossArbOpportunities);
            this.Controls.Add(this.button_getExchangeData);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_getExchangeData;
        private System.Windows.Forms.Button button_crossArbOpportunities;
        private System.Windows.Forms.Button button_LoadData;
        private System.Windows.Forms.Button button_SaveData;
        private System.Windows.Forms.Button button_triArbOpportunities;
        private System.Windows.Forms.Button button_UpdateTransactionData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Messages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_QuadArbOpportunities;
        private System.Windows.Forms.Button button_PentArbOpportunities;
        private System.Windows.Forms.Button button_GetTriArbTransactions;
    }
}

