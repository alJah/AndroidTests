using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace AndroidTests
{
    /// <summary>
    /// Событие запроса нового вопроса. 
    /// </summary>
    /// <param name="number">Номер запрашиваемого вопроса или -1, если нужен случайный вопрос.</param>
    /// <returns></returns>
    public delegate QuestCase LoadCase(int number);
    public delegate void Answered();
    public partial class App : Application
    {
        readonly string basename;
        List<QuestCase> SessionBase;
        readonly IEnumerable<QuestCase> Errors;

        public App(string bName, string xmlString)
        {
            InitializeComponent();
            basename = bName;
            LoadBase(xmlString);

            Errors = from e in SessionBase where e.Errors > 0 select e;
            MyView myView = new MyView() { QuestCase = SessionBase[Utils.RandomInt(0,SessionBase.Count-1)] };
            myView.GetCase += MyView_Need;
            MainPage = new NavigationPage(new MainPage(myView));
        }

        private void LoadBase(string xml)
        {
            IFileWorker worker = DependencyService.Get<IFileWorker>();
            List<QuestCase> myBase = new List<QuestCase>();
            if (!worker.ExistAsync(basename).Result)
            {
                myBase = Parse(xml);
            }
            else
            {
                try
                {
                   myBase = worker.OpenBase(basename).Result;
                }
                catch (Exception)
                {
                    worker.DeleteAsync(basename);
                    myBase = Parse(xml);
                }
            }
            SessionBase = myBase;
        }

        private List<QuestCase> Parse(string xml)
        {
            XDocument document = XDocument.Parse(xml);
            List<QuestCase> qBase = new List<QuestCase>();
            int x = 0;//номер вопроса
            foreach (XElement element in document.Element("Example_Test").Elements("Theme"))
            {
                string themeName = element.Element("Name").Value;
                foreach (XElement qsts in element.Element("ThemeQuests").Elements("Question"))
                {
                    //техт вопроса
                    string qtext = qsts.Element("QText").Value;

                    //количество правильных ответов
                    byte qnum = byte.Parse(qsts.Attribute("numAnswers").Value);
                    Dictionary<string, bool> answers = new Dictionary<string, bool>();
                    foreach (XElement answ in qsts.Elements("AText"))
                    {
                        if (answ.Attribute("bingo").Value == "true")
                        {
                            answers.Add(answ.Value, true);
                        }
                        else
                        {
                            answers.Add(answ.Value, false);
                        }
                    }
                    qBase.Add(new QuestCase(themeName, qtext, answers, qnum, x));
                    x++;
                }
            }
            return qBase;
        }

        /// <summary>
        /// Если входной параметр меньше нуля, вернуть случайный вопрос. Иначе вернуть запрашиваемый.
        /// </summary>
        /// <param name="v">Запрашиваемый номер вопроса</param>
        /// <returns></returns>
        private QuestCase MyView_Need(int v)
        {
            if (v < 0)
                return RandomCase();
            else
                return v == SessionBase.Count ? SessionBase[0] : SessionBase[v];
        }
        /// <summary>
        /// Вернёт случайный вопрос. Вопросы из списка ошибок ворачиваются чаще.
        /// </summary>
        /// <returns></returns>
        private QuestCase RandomCase()
        {
            if (Utils.RandomInt(0, 100) % 4 == 0 && Errors.Any())
                return Errors.ElementAt(Utils.RandomInt(0, Errors.Count() - 1));
            return SessionBase[Utils.RandomInt(0, SessionBase.Count - 1)];
        }

        protected override void OnStart()
        {
            Open();
        }
        protected override void OnSleep()
        {
            Save();
        }
        protected override void OnResume()
        {
            Open();
        }

        private void Open()
        {
            IFileWorker worker = DependencyService.Get<IFileWorker>();
            if( worker.ExistAsync(basename).Result )
            SessionBase = worker.OpenBase(basename).Result;
        }

        private void Save()
        {           
            if (String.IsNullOrEmpty(basename)) return;
            // перезаписываем файл
            IFileWorker service = DependencyService.Get<IFileWorker>();
            service.SaveBase(basename, SessionBase);
        }
    }
}
