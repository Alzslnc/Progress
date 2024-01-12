using System;
using System.Drawing;
using System.Windows.Forms;

namespace Progress
{
    public partial class ProgressWindow : Form 
    {       
        private readonly object _lock = new object();

        public ProgressWindow(ProgressDialog dialog)
        {
            InitializeComponent();

            if (dialog == null) return;

            _ProgressDialog = dialog;

            StartPosition = FormStartPosition.CenterScreen; 
            
            SubMessage = _ProgressDialog.SubMessage;
            MainMessage = _ProgressDialog.MainMessage;           
            UseCancelButton = _ProgressDialog.UseCancelButton;
            MainBarValue = (int)_ProgressDialog.MainBarValue;
            SubBarValue = (int)_ProgressDialog.SubBarValue;
            _MainCancelMessage = _ProgressDialog.MainCancelMessage;
            _UseSubBar = _ProgressDialog.UseSubBar;
            FormChange();

            //подписываемся на изменение данных
            _ProgressDialog.OnChange += this.Event;                       
        }
        private void Event(string name)
        {
            lock (_lock)
            {
                if (name == "CloseWindow")
                {                
                    if (this.InvokeRequired) Invoke(new Action(() => Close()));
                    else Close();
                }
                if (_ProgressDialog == null) return;
                //в зависимости от переданного имени меняем соответствующую переменную
                switch (name)
                {
                    case "SubMessage":
                        {
                            SubMessage = _ProgressDialog.SubMessage;
                            break;
                        }
                    case "MainMessage":
                        {
                            MainMessage = _ProgressDialog.MainMessage;
                            break;
                        }
                    case "MainCancelMessage":
                        {
                            MainCancelMessage = _ProgressDialog.MainCancelMessage;
                            break;
                        }
                    case "UseSubBar":
                        {
                            UseSubBar = _ProgressDialog.UseSubBar;
                            break;
                        }
                    case "UseCancelButton":
                        {
                            UseCancelButton = _ProgressDialog.UseCancelButton;
                            break;
                        }
                    case "MainBarValue":
                        {
                            MainBarValue = (int)_ProgressDialog.MainBarValue;
                            break;
                        }
                    case "SubBarValue":
                        {
                            SubBarValue = (int)_ProgressDialog.SubBarValue;
                            break;
                        }
                                     
                }
            }
        }     
        /// <summary>
        /// использовать второй бар?
        /// </summary>
        public bool UseSubBar
        {
            get => _UseSubBar;
            set 
            {           
                _UseSubBar = value;
                FormChange();
            }            
        }
        public bool UseCancelButton
        {
            get => _UseCancelButton;
            set
            {
                _UseCancelButton = value;
                FormChange();
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
                if (MainMessageLabel.InvokeRequired) MainMessageLabel.Invoke(new Action(() => MainMessageLabel.Text = _MainMessage));
                else MainMessageLabel.Text = _MainMessage;            
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
                if (SubMessageLabel.InvokeRequired) SubMessageLabel.Invoke(new Action(() => SubMessageLabel.Text = _SubMessage));
                else SubMessageLabel.Text = _SubMessage;
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

                if (MainBar.InvokeRequired) MainBar.Invoke(new Action(() => MainBar.Value = _MainBarValue));
                else MainBar.Value = _MainBarValue;       
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
                if (SubBar.InvokeRequired) SubBar.Invoke(new Action(() => SubBar.Value = _SubBarValue));
                else SubBar.Value = _SubBarValue;         
            }
        }
      
        /// <summary>
        /// проверяет и настраивает элементы
        /// </summary>
        private void FormChange()
        {
            int height = 160;

            if (_UseSubBar)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.SubBar.Visible = true));
                    this.Invoke(new Action(() => this.SubMessageLabel.Visible = true));
                    this.Invoke(new Action(() => this.Button_Cancel.Location = new Point(Button_Cancel.Location.X, 283))); 
                }
                else
                {
                    this.SubBar.Visible = true;
                    this.SubMessageLabel.Visible = true;
                    this.Button_Cancel.Location = new Point(Button_Cancel.Location.X, 283);
                }
                height += 130;

            }
            else
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.SubBar.Visible = false));
                    this.Invoke(new Action(() => this.SubMessageLabel.Visible = false));
                    this.Invoke(new Action(() => this.Button_Cancel.Location = new Point(Button_Cancel.Location.X, 153)));
                }
                else
                {
                    this.SubBar.Visible = false;
                    this.SubMessageLabel.Visible = false;
                    this.Button_Cancel.Location = new Point(Button_Cancel.Location.X, 153);
                }
            }

            if (_UseCancelButton)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.Button_Cancel.Visible = true));              
                }
                else
                {
                    this.Button_Cancel.Visible = true;           
                }
                height += 60;
            }
            else
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.Button_Cancel.Visible = false));               
                }
                else
                {
                    this.Button_Cancel.Visible = false;             
                }
            }

            if (this.InvokeRequired) this.Invoke(new Action(() => this.Height = height));
            else this.Height = height;
        }

        private int _MainBarValue = 0;
        private int _SubBarValue = 0;
        private readonly ProgressDialog _ProgressDialog = null;
        private bool _UseSubBar = true;
        private bool _UseCancelButton = true;
        private string _MainMessage = string.Empty;
        private string _SubMessage = string.Empty;
        private string _MainCancelMessage = "Идет отмена";

        private void Cancel_Click(object sender, System.EventArgs e)
        {
            _ProgressDialog.IsStopNeed = true;
            Button_Cancel.Enabled = false;
            Button_Cancel.Text = MainCancelMessage;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, this.Width - 2, this.Height - 2), Color.Black, ButtonBorderStyle.Solid);
        }
    }   
}
