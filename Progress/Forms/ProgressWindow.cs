using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Progress
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow(ProgressDialog dialog)
        {
            InitializeComponent();
            MainBar.Maximum = 100;
            MainBar.Minimum = 0;
            SubBar.Maximum = 100;
            SubBar.Minimum = 0;

            this.StartPosition = FormStartPosition.Manual;
            Size screenSize = Screen.PrimaryScreen.WorkingArea.Size;
            Location = new Point(screenSize.Width / 2 - Width / 2, screenSize.Height / 2 - Height / 2);

            BW.WorkerReportsProgress = true;
            BW.WorkerSupportsCancellation = true;
            this.BW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_DoWork);
            this.BW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BW_ProgressChanged);
            this.Shown += new System.EventHandler(this.ShowIvent);

            Check();

            _ProgressDialog = dialog;
            if (_ProgressDialog != null)
            {
                Delay = _ProgressDialog.Delay; 
                Thread thread = new Thread(GetData);
                thread.IsBackground = true;
                thread.Start();
            }
        }
        /// <summary>
        /// запускаеи воркер после включения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowIvent(Object sender, EventArgs e)
        {
            BW.RunWorkerAsync();
        }
        /// <summary>
        /// запускает обновление формы когда воркер объявляет о прогрессе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1) GetData();
        }        
        /// <summary>
        /// запускает процесс обновления данных с выбранной задержкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                worker.ReportProgress(1);
                Thread.Sleep(Delay);
            }
        }   
        /// <summary>
        /// обновляет элементы формы
        /// </summary>
        private void GetData()
        {
            if (_ProgressDialog != null)
            {
                if (_ProgressDialog.SubMessage != SubMessage) SubMessage = _ProgressDialog.SubMessage;
                if (_ProgressDialog.MainMessage != MainMessage) MainMessage = _ProgressDialog.MainMessage;
                if (_ProgressDialog.MainCancelMessage != MainCancelMessage) MainCancelMessage = _ProgressDialog.MainCancelMessage;
                if (_ProgressDialog.UseSubBar != UseSubBar) UseSubBar = _ProgressDialog.UseSubBar;
                if ((int)_ProgressDialog.MainBarValue != MainBarValue) MainBarValue = (int)_ProgressDialog.MainBarValue;
                if ((int)_ProgressDialog.SubBarValue != SubBarValue) SubBarValue = (int)_ProgressDialog.SubBarValue;
                Check();
            }
        }
        /// <summary>
        /// использовать второй бар?
        /// </summary>
        public bool UseSubBar
        {
            get => _useSubBar;
            set 
            { 
                _useSubBar = value;
                Check();
            }            
        } 
        /// <summary>
        /// текст над первым баром
        /// </summary>
        public string MainMessage
        {
            get => _MainMessage;
            set
            {
                _MainMessage = value;
                MainMessageLabel.Text = _MainMessage;
            }           
        }
        /// <summary>
        /// текст над вторым баром
        /// </summary>
        public string SubMessage
        {
            get => _SubMessage;
            set
            {
                _SubMessage = value;
                SubMessageLabel.Text = _SubMessage;
            }
        }
        /// <summary>
        /// текст при отмене
        /// </summary>
        public string MainCancelMessage
        {
            get => _MainCancelMessage;
            set 
            {
                _MainCancelMessage = value;             
            }
        }
        /// <summary>
        /// счетчик первого бара
        /// </summary>
        public int MainBarValue
        {
            get => _MainBarValue;
            set
            {
                if (value > 100) _MainBarValue = 100;
                else if (value < 0) _MainBarValue = 0;
                else _MainBarValue = value;
                MainBar.Value = _MainBarValue;
            }
        }
        /// <summary>
        /// счетчик второго бара
        /// </summary>
        public int SubBarValue
        {
            get => _SubBarValue;
            set
            {                
                if (value > 100) _SubBarValue = 100;
                else if (value < 0) _SubBarValue = 0;
                else _SubBarValue = value;
                SubBar.Value = _SubBarValue;
            }
        }
        /// <summary>
        /// частота обновления
        /// </summary>
        public int Delay
        {
            get => _Delay;
            set
            {
                if (value <= 0) _Delay = 100;
                else _Delay = value;             
            }
        }
        /// <summary>
        /// проверяет и настраивает элементы
        /// </summary>
        private void Check()
        {
            if (_useSubBar)
            {
                if (!this.SubMessageLabel.Visible && !this.SubMessageLabel.InvokeRequired) this.SubMessageLabel.Visible = true;
                if (!this.SubBar.Visible && !this.SubBar.InvokeRequired) this.SubBar.Visible = true;
            }
            else
            {
                if (this.SubMessageLabel.Visible && !this.SubMessageLabel.InvokeRequired) this.SubMessageLabel.Visible = false;
                if (this.SubBar.Visible && !this.SubBar.InvokeRequired) this.SubBar.Visible = false;
            }
        }

        private int _Delay = 100;
        private int _MainBarValue = 0;
        private int _SubBarValue = 0;
        private ProgressDialog _ProgressDialog = null;
        private bool _useSubBar = false;
        private string _MainMessage = string.Empty;
        private string _SubMessage = string.Empty;
        private string _MainCancelMessage = "Идет отмена";

        private void Cancel_Click(object sender, System.EventArgs e)
        {         
            Cancel.Enabled = false;
            _ProgressDialog.IsStopNeed = true;
            MainMessageLabel.Text = MainCancelMessage;            
        }      

    }   
}
