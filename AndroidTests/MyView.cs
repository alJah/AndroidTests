using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Linq;

namespace AndroidTests
{
   
    public class MyView : INotifyPropertyChanged
    {
        public event LoadCase GetCase;
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
        /// Событие получения ответа
        /// </summary>
        public event Answered GotAnswer;

        /// <summary>
        /// Отображаемый вопрос со списком ответов
        /// </summary>
        private QuestCase _questCase;

        private byte _taps;
        private bool _swerr;
        private bool _swser;

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
        public byte Errors
        {
            get { return _questCase.Errors; }
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
                OnPropertyChanged("Taps");
                if (_taps == _questCase.Valid)
                {
                    IsAnswered = true;
                    GotAnswer();
                    OnPropertyChanged("IsAnswered");
                }
                else IsAnswered = false;
            }
        }

        internal void NextCase()
        {
            if (Sw_ser) QuestCase = GetCase(Number + 1);
            else QuestCase = GetCase(-1);
        }
        internal void NextCase(int v)
        {
            GetCase(v);
        }
        /// <summary>
        /// Кейс с вопросом, ответами
        /// </summary>
        public QuestCase QuestCase
        {
            get { return _questCase; }
            set
            {
                    _questCase = value;                 
                    QuestCase.Shuffle(_questCase);
                    OnPropertyChanged("Question");
                    OnPropertyChanged("Type");
                    OnPropertyChanged("Errors");
                    for (int x = 0; x < 4; x++)
                    {
                        OnPropertyChanged("Answer" + x.ToString());
                        OnPropertyChanged("Bingo" + x.ToString());
                    }
                    OnPropertyChanged("Number");
                    _taps = 0;
                    OnPropertyChanged("Taps");
                    IsAnswered = false;
                    OnPropertyChanged("IsAnswered");             
            }
        }
        public MyView()
        {
            Sw_err = Preferences.Get(_errors, true);
            Sw_ser = Preferences.Get(_series, false);
        }

        /// <summary>
        /// Вопрос закрыт?
        /// </summary>
        public bool IsAnswered { get; private set; }
        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int Number
        {
            get { return _questCase.Number; }
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

        #region Тексты ответов
        public string Answer0 { get { return _questCase.Answers.Keys.ElementAt(0); } }
        public string Answer1 { get { return _questCase.Answers.Keys.ElementAt(1); } }
        public string Answer2 { get { return _questCase.Answers.Keys.ElementAt(2); } }
        public string Answer3 { get { if (_questCase.Answers.Count < 4) return ""; else return _questCase.Answers.Keys.ElementAt(3); } }
        #endregion

        #region Значения "правильности" ответа.
        public bool Bingo0 { get { return _questCase.Answers.Values.ElementAt(0); } }
        public bool Bingo1 { get { return _questCase.Answers.Values.ElementAt(1); } }
        public bool Bingo2 { get { return _questCase.Answers.Values.ElementAt(2); } }
        public bool Bingo3 { get { if (_questCase.Answers.Count < 4) return false; else return _questCase.Answers.Values.ElementAt(3); } }

        #endregion

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
