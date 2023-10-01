using System;
using System.Threading;

namespace Progress
{
    public class ProgressDialog : IDisposable
    {
        //объект для блокировки объектов при обращении из разных потоков
        private readonly object Lock = new object();
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
            Thread?.Abort();
            IsRunning = false;
            Thread = null;        
        }
        /// <summary>
        /// переход к следующему шагу основного бара
        /// </summary>
        public void StepMainBar()
        {
            _SubBarValue = 0;
            _CurrentSubStep = 0;
            if (_CurrentMainStep < _MainBarCount) _CurrentMainStep += 1;
            _MainBarValue = _CurrentMainStep / _MainBarCount * 100;
        }
        /// <summary>
        /// переход к следующему шагу дополнительнго бара
        /// </summary>
        public void StepSubBar()
        {
            if (_CurrentSubStep < _SubBarCount) _CurrentSubStep += 1;
            _SubBarValue = _CurrentSubStep / _SubBarCount * 100;
            _MainBarValue += 1 / _SubBarCount / _MainBarCount * 100;
        }

        /// <summary>
        /// число в основном счетчике
        /// </summary>
        public double SetMainBarCount
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
                    _MainBarCount = value;
                }
            }
        }
        /// <summary>
        /// число в дополнительном счетчике
        /// </summary>
        public double SetSubBarCount
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
                    _SubBarCount = value;
                }
            }
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
                    _UseSubBar = value;               
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
                    _MainMessage = value;  
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
                    _SubMessage = value;                  
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
                    _MainCancelMessage = value;
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
                    _MainBarValue = value;
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
                    _SubBarValue = value;
                }
            }
        }
        /// <summary>
        /// частота обновления в микросекундах
        /// </summary>
        public int Delay
        {
            get
            {
                lock (Lock)
                {
                    return _Delay;
                }
            }
            set
            {
                lock (Lock)
                {
                    _Delay = value;
                }
            }
        }

        private double _MainBarCount = 1;
        private double _SubBarCount = 1;
        private double _CurrentMainStep = 0;
        private double _CurrentSubStep = 0;

        private int _Delay = 100;
        private double _MainBarValue = 0;
        private double _SubBarValue = 0;
        private bool _IsStopNeed = false;
        private bool _UseSubBar = false;
        private string _MainMessage = string.Empty;
        private string _SubMessage = string.Empty;
        private string _MainCancelMessage = string.Empty;
        public bool IsDisposed { get; private set; } = false;       
        public void Dispose()
        {
            if (IsDisposed) return;           
            Thread?.Abort();
            Thread = null;
            IsDisposed = true;          
        }
    }
}
