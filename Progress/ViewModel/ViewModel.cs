using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Progress
{
    public abstract class ViewModel
    {
        /// <summary>
        /// создаем делегат хранящий имя
        /// </summary>
        /// <param name="name"></param>
        public delegate void Methods(string name);
        /// <summary>
        /// создаем событие выбываемое при изменении переменных
        /// </summary>
        public event Methods OnChange;
        /// <summary>
        /// метод для изменения переменной который так же получаем имя изменяемой переменной и сообщает о изменении
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected void SetData<T>(ref T data, T value, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<T>.Default.Equals(data, value)) return;
            data = value;
            OnChange?.Invoke(name);
        }
        //объект для блокировки объектов при обращении из разных потоков
        protected readonly object Lock = new object();
    }
}
