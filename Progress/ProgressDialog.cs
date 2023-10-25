using System;
using System.Threading;

namespace Progress
{
    public class ProgressDialog : ViewModel, IDisposable
    {        
        public ProgressDialog()
        {              
        }       
        /// <summary>
        /// запуск бара
        /// </summary>
        public void Start()
        { 
            if (IsRunning) return;
            IsRunning = true;
            //создаем новый поток и запускаем его
            Thread = new Thread(StartNewThread)
            {
                IsBackground = true
            };
            Thread.Start();
        }
        /// <summary>
        /// создаем форму во втором потоке
        /// </summary>
        private void StartNewThread()
        {
            Window = new ProgressWindow(this);
            Window.ShowDialog();            
        }
        /// <summary>
        /// при стопе останавливаем поток, форма умрет вместе с потоком
        /// </summary>
        public void Stop()
        { 
            Window?.Close();
            Thread?.Abort();
            IsRunning = false;
            Thread = null;        
        }
        /// <summary>
        /// переход к следующему шагу основного бара
        /// </summary>
        public void StepMainBar()
        {
            if (BarLink)
            {
                SubBarValue = 0;
                _CurrentSubStep = 0;              
            }
            if (_CurrentMainStep < _MainBarCount) _CurrentMainStep += 1;
            MainBarValue = _CurrentMainStep / _MainBarCount * 100;
        }
        /// <summary>
        /// переход к следующему шагу дополнительнго бара
        /// </summary>
        public void StepSubBar()
        {
            if (_CurrentSubStep < _SubBarCount) _CurrentSubStep += 1;
            SubBarValue = _CurrentSubStep / _SubBarCount * 100;
            MainBarValue += 1 / _SubBarCount / _MainBarCount * 100;
        }
        /// <summary>
        /// сохраняет текущее состояние дополнительного бара
        /// </summary>
        public void GetSubSnapShot()
        {           
            _PictureSubBarValue = SubBarValue;  
            _PictureSubBarCount = SubBarCount;      
            _PictureSubMessage = SubMessage;     
            _PictureCurrentSubStep = CurrentSubStep;
        }
        /// <summary>
        /// сохраняет текущее состояние основного бара
        /// </summary>
        public void GetMainSnapShot()
        {
            _PictureMainBarValue = MainBarValue;
            _PictureMainBarCount = MainBarCount;
            _PictureMainMessage = MainMessage;
            _PictureCurrentMainStep = CurrentMainStep;
        } 
        /// <summary>
        /// восстанаваливает сохраненное состояние дополнительного бара
        /// </summary>
        public void SetSubSnapShot()
        {
            SubBarValue = _PictureSubBarValue;
            SubBarCount = _PictureSubBarCount;
            SubMessage = _PictureSubMessage;
            CurrentSubStep = _PictureCurrentSubStep;
        }
        /// <summary>
        /// восстанаваливает сохраненное состояние основного бара
        /// </summary>
        public void SetMainSnapShot()
        {
            MainBarValue = _PictureMainBarValue;
            MainBarCount = _PictureMainBarCount;
            MainMessage = _PictureMainMessage;
            CurrentMainStep = _PictureCurrentMainStep;
        }
        /// <summary>
        /// форма
        /// </summary>
        private ProgressWindow Window { get; set; } = null;
        /// <summary>
        /// второй поток
        /// </summary>
        public Thread Thread { get; private set; }
        /// <summary>
        /// маркер что второй поток с формой уже запущены
        /// </summary>
        public bool IsRunning { get; private set; } = false;
        /// <summary>
        /// маркер нажатой кнопки отмены на форме
        /// </summary>
        public bool IsStopNeed 
        {
            get 
            {
                lock (Lock)
                {
                    return _IsStopNeed;
                }               
            }
            set             
            {
                lock (Lock)
                {
                    _IsStopNeed = value;
                }
            }        
        }
        /// <summary>
        /// маркер что надо использовать дополнительный бар
        /// </summary>
        public bool UseSubBar
        {
            get
            {
                lock (Lock)
                {
                    return _UseSubBar;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _UseSubBar, value);               
                }
            }
        }
        /// <summary>
        /// текст основного бара
        /// </summary>
        public string MainMessage
        {
            get
            {
                lock (Lock)
                {
                    return _MainMessage;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _MainMessage, value);  
                }
            }
        }
        /// <summary>
        /// текст дополнительного бара
        /// </summary>
        public string SubMessage
        {
            get
            {
                lock (Lock)
                {
                    return _SubMessage;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _SubMessage, value);                               
                }
            }
        }
        /// <summary>
        /// текст после нажатия отмены
        /// </summary>
        public string MainCancelMessage
        {
            get
            {
                lock (Lock)
                {
                    return _MainCancelMessage;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _MainCancelMessage, value);
                }
            }
        }
        /// <summary>
        /// счетчик основного бара
        /// </summary>
        public double MainBarValue
        {
            get
            {
                lock (Lock)
                {
                    return _MainBarValue;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _MainBarValue, value);                 
                }
            }
        }
        /// <summary>
        /// счетчик дополнительного бара
        /// </summary>
        public double SubBarValue
        {
            get
            {
                lock (Lock)
                {
                    return _SubBarValue;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _SubBarValue, value);  
                }
            }
        }
        public double MainBarCount
        {
            get
            {
                lock (Lock)
                {
                    return _MainBarCount;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _MainBarCount, value);
                }
            }
        }
        public double SubBarCount
        {
            get
            {
                lock (Lock)
                {
                    return _SubBarCount;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _SubBarCount, value);
                }
            }
        }
        public double CurrentMainStep
        {
            get
            {
                lock (Lock)
                {
                    return _CurrentMainStep;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _CurrentMainStep, value);
                }
            }
        }
        public double CurrentSubStep
        {
            get
            {
                lock (Lock)
                {
                    return _CurrentSubStep;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _CurrentSubStep, value);
                }
            }
        }
        public bool BarLink
        {
            get
            {
                lock (Lock)
                {
                    return _BarLink;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _BarLink, value);
                }
            }
        }
        private double _MainBarCount = 1;
        private double _SubBarCount = 1;
        private double _CurrentMainStep = 0;
        private double _CurrentSubStep = 0;
     
        private double _MainBarValue = 0;
        private double _SubBarValue = 0;
        private bool _BarLink = false;
        private bool _IsStopNeed = false;
        private bool _UseSubBar = false;
        private string _MainMessage = string.Empty;
        private string _SubMessage = string.Empty;
        private string _MainCancelMessage = string.Empty;

        private double _PictureMainBarValue = 0;
        private double _PictureSubBarValue = 0;
        private double _PictureMainBarCount = 0;
        private double _PictureSubBarCount = 0;
        private string _PictureMainMessage = string.Empty;
        private string _PictureSubMessage = string.Empty;
        private double _PictureCurrentMainStep = 0;
        private double _PictureCurrentSubStep = 0;

        public bool IsDisposed { get; private set; } = false;       
        public void Dispose()
        {
            if (IsDisposed) return;
            if (Window != null)
            {
                if (Window.InvokeRequired) Window.Invoke(new Action(() => Window.Close()));
                else Window.Close();
            }                
            Thread?.Abort();
            Thread = null;
            IsDisposed = true;          
        }
    }
}
