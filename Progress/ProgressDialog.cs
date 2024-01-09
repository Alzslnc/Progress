using System;
using System.Threading;

namespace Progress
{
    public class ProgressDialog : Base, IDisposable
    {
        private readonly object stopLock = new object();

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
            Thread.CurrentThread.Interrupt();
        }
        /// <summary>
        /// при стопе останавливаем поток, форма умрет вместе с потоком
        /// </summary>
        public void Stop()
        {
            CloseWindow = true;
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
            if (_CurrentMainStep < _MainBarCount) _CurrentMainStep += MainStep;
            MainBarValue = _CurrentMainStep / _MainBarCount * 100;

            if (_AddCurrentValueToMessage) MainMessage = _MainMessageValue;
        }
        /// <summary>
        /// переход к следующему шагу дополнительнго бара
        /// </summary>
        public void StepSubBar()
        {
            if (_CurrentSubStep < _SubBarCount) _CurrentSubStep += SubStep;
            SubBarValue = _CurrentSubStep / _SubBarCount * 100;
            if (BarLink) MainBarValue += 1 / _SubBarCount / _MainBarCount * 100;

            if (_AddCurrentValueToMessage) SubMessage = _SubMessageValue;
        }
        /// <summary>
        /// сохраняет текущее состояние дополнительного бара
        /// </summary>
        public void GetSubSnapShot()
        {
            _SnapShotSubBarValue = SubBarValue;
            _SnapShotSubBarCount = SubBarCount;
            _SnapShotSubMessage = SubMessage;
            _SnapShotCurrentSubStep = CurrentSubStep;
        }
        /// <summary>
        /// сохраняет текущее состояние основного бара
        /// </summary>
        public void GetMainSnapShot()
        {
            _SnapShotMainBarValue = MainBarValue;
            _SnapShotMainBarCount = MainBarCount;
            _SnapShotMainMessage = MainMessage;
            _SnapShotCurrentMainStep = CurrentMainStep;
        }
        /// <summary>
        /// восстанаваливает сохраненное состояние дополнительного бара
        /// </summary>
        public void SetSubSnapShot()
        {
            SubBarValue = _SnapShotSubBarValue;
            SubBarCount = _SnapShotSubBarCount;
            SubMessage = _SnapShotSubMessage;
            CurrentSubStep = _SnapShotCurrentSubStep;
        }
        /// <summary>
        /// восстанаваливает сохраненное состояние основного бара
        /// </summary>
        public void SetMainSnapShot()
        {
            MainBarValue = _SnapShotMainBarValue;
            MainBarCount = _SnapShotMainBarCount;
            MainMessage = _SnapShotMainMessage;
            CurrentMainStep = _SnapShotCurrentMainStep;
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
                lock (stopLock)
                {
                    return _IsStopNeed;
                }
            }
            set
            {
                lock (stopLock)
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
                    _MainMessageValue = value;
                    string message = value;
                    if (_AddCurrentValueToMessage) message += "\n (" + _CurrentMainStep + " из " + _MainBarCount + ")";
                    SetData(ref _MainMessage, message);
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
                    _SubMessageValue = value;
                    string message = value;
                    if (_AddCurrentValueToMessage) message += "\n (" + _CurrentSubStep + " из " + _SubBarCount + ")";
                    SetData(ref _SubMessage, message);
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
        /// позиция счетчика основного бара
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
                    if (_MainBarValue == 0)
                    {
                        _CurrentMainStep = 0;
                    }
                }
            }
        }
        /// <summary>
        /// позиция счетчика дополнительного бара
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
                    if (_SubBarValue == 0)
                    {
                        _CurrentSubStep = 0;
                    }
                }
            }
        }
        /// <summary>
        /// всего считающихся объектов основного счетчика
        /// </summary>
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
        /// <summary>
        /// всего считающихся объектов дополнительного счетчика
        /// </summary>
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
        /// <summary>
        /// текущий посчитанный объект основного счетчика
        /// </summary>
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
        /// <summary>
        /// текущий посчитанный объект дополнительного счетчика
        /// </summary>
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
        /// <summary>
        /// число объектов прибавляющихся к дополнительному счетчику при одном шаге
        /// </summary>
        public double SubStep
        {
            get
            {
                lock (Lock)
                {
                    return _SubStep;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _SubStep, value);
                }
            }
        }
        /// <summary>
        /// число объектов прибавляющихся к основному счетчику при одном шаге
        /// </summary>
        public double MainStep
        {
            get
            {
                lock (Lock)
                {
                    return _MainStep;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _MainStep, value);
                }
            }
        }
        /// <summary>
        /// зависит ли основной счетчик от дополнительного?
        /// </summary>
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
        /// <summary>
        /// число объектов прибавляющихся к дополнительному счетчику при одном шаге
        /// </summary>
        public bool CloseWindow
        {
            get
            {
                lock (Lock)
                {
                    return _CloseWindow;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _CloseWindow, value);
                }
            }
        }
        /// <summary>
        /// Включить кнопку отмены?
        /// </summary>
        public bool UseCancelButton
        {
            get
            {
                lock (Lock)
                {
                    return _UseCancelButton;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _UseCancelButton, value);
                }
            }
        }
        /// <summary>
        /// Показывать число объектов цифрами в сообщении?
        /// </summary>
        public bool AddCurrentValueToMessage
        {
            get
            {
                lock (Lock)
                {
                    return _AddCurrentValueToMessage;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetData(ref _AddCurrentValueToMessage, value);
                }
            }
        }

        private double _MainBarCount = 1;
        private double _SubBarCount = 1;
        private double _CurrentMainStep = 0;
        private double _CurrentSubStep = 0;
        private double _MainStep = 1;
        private double _SubStep = 1;

        private double _MainBarValue = 0;
        private double _SubBarValue = 0;
        private bool _BarLink = false;
        private bool _IsStopNeed = false;
        private bool _CloseWindow = false;
        private bool _UseSubBar = false;
        private string _MainMessage = string.Empty;
        private string _SubMessage = string.Empty;
        private string _MainMessageValue = string.Empty;
        private string _SubMessageValue = string.Empty;
        private string _MainCancelMessage = string.Empty;
        private bool _UseCancelButton = true;
        private bool _AddCurrentValueToMessage = true;


        private double _SnapShotMainBarValue = 0;
        private double _SnapShotSubBarValue = 0;
        private double _SnapShotMainBarCount = 0;
        private double _SnapShotSubBarCount = 0;
        private string _SnapShotMainMessage = string.Empty;
        private string _SnapShotSubMessage = string.Empty;
        private double _SnapShotCurrentMainStep = 0;
        private double _SnapShotCurrentSubStep = 0;

        public bool IsDisposed { get; private set; } = false;
        public void Dispose()
        {
            if (IsDisposed) return;
            Thread.Sleep(100);
            Thread?.Abort();
            Thread = null;
            IsDisposed = true;
        }
    }
}
