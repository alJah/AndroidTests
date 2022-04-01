using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;

namespace AndroidTests
{

    public class MyView : INotifyPropertyChanged
    {
        public event Errored SaveError;
        public event LoadCase GetCase;
        public event ClearErrorList ClearErrors;
        /// <summary>
        /// Название ключа настройки. Вопросы по порядку.
        /// </summary>
        private const string _series = "isSeries";

        /// <summary>
        /// Название ключа настройки. Показывать ошибки чаще.
        /// </summary>
        private const string _errors = "showErrors";

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Отображаемый вопрос со списком ответов
        /// </summary>
        private QuestCase _questCase;

        private byte _taps;
        private bool _swerr;
        private bool _swser;
        private ICommand tapCommand;
        private ICommand changeCommand;
        private ICommand deleteErrorsCommand;

        private bool gettingError = false;
        public bool GettingError
        {
            private get
            {
                return gettingError;
            }
            set
            {
                gettingError = value;
                if (gettingError) {  Errors = 4; }
                OnPropertyChanged(nameof(GettingError));
                OnPropertyChanged(nameof(Errors));
            }
        }

        /// <summary>
        /// Настройка. Показывать ошибки чаще?
        /// </summary>
        public bool Sw_err
        {
            get
            {
                return _swerr;
            }
            set
            {
                _swerr = value;
                OnPropertyChanged("Sw_err");
                _swser = !value;
                OnPropertyChanged("Sw_ser");
                SaveSettings();
            }
        }

        /// <summary>
        /// Настройка. Вопросы по порядку?
        /// </summary>
        public bool Sw_ser
        {
            get
            {
                return _swser;
            }
            set
            {
                _swser = value;
                OnPropertyChanged("Sw_ser");
                _swerr = !value;
                OnPropertyChanged("Sw_err");
                SaveSettings();
            }
        }
        /// <summary>
        /// Счётчик до выхода из списка чаще показываемых
        /// </summary>
        public sbyte Errors
        {
            get { return QuestCase.Errors; }
            set { QuestCase.Errors = value; }
        }
        /// <summary>
        /// Количество сделанных тапов
        /// </summary>
        public byte Taps
        {
            get { return _taps; }
            set
            {
                _taps = value;
                OnPropertyChanged(nameof(Taps));
                OnPropertyChanged(nameof(IsAnswered));
            }
        }
        private void ChangeCase()
        {
            if (IsAnswered)
            {
                if (!GettingError && QuestCase.Errors > 0) { QuestCase.Errors -= 1; }
                SaveError();
            }
            if (Sw_ser) QuestCase = GetCase(Number);
            else QuestCase = GetCase(-1);
        }
        internal void ChangeCase(int v)
        {
            QuestCase = GetCase(v);
        }
        /// <summary>
        /// Кейс с вопросом, ответами
        /// </summary>
        public  QuestCase QuestCase
        {
            get { return _questCase; }
            set
            {
                _questCase = value;
                QuestCase.Shuffle(QuestCase);
                GettingError = false;
                Taps = 0;
                OnPropertyChanged("");
            }
        }
        public MyView()
        {
            TapCommand = new Command<MyLabel>(MyLabelTapped);
            CmdChangeCase = new Command(ChangeCase);
            deleteErrorsCommand = new Command(Clear);
            Sw_err = Preferences.Get(_errors, true);
            Sw_ser = Preferences.Get(_series, false);
        }
        private void Clear(object obj)
        {
            ClearErrors();
        }
        private void MyLabelTapped(MyLabel label)
        {
            if (IsAnswered) return;
            if (label.IsClicked)
            {
                label.IsClicked = false;
                Taps -= 1;
                return;
            }
            label.IsClicked = true;
            Taps += 1;
        }

        /// <summary>
        /// Answer recived?
        /// </summary>
        public bool IsAnswered { get => Taps == QuestCase.Valid; }

        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int Number
        {
            get { return _questCase.Number + 1; }
        }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Question
        {
            get { return _questCase.Question; }
        }
        /// <summary>
        /// Текст темы вопроса
        /// </summary>
        public string Type
        {
            get { return _questCase.Type; }
        }
        public ICommand CmdChangeCase { get => changeCommand; set => changeCommand = value; }
        public ICommand TapCommand { get => tapCommand; set => tapCommand = value; }
        public ICommand ClearErrorsCommand { get => deleteErrorsCommand; set => deleteErrorsCommand = value; }
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        void SaveSettings()
        {
            Preferences.Set(_errors, _swerr);
            Preferences.Set(_series, _swser);
        }
    }
}
