using System;
using System.Collections.Generic;


namespace AndroidTests
{
    [Serializable]
    public class QuestCase
    {
        private readonly Dictionary<string, bool> _answers;
        private sbyte errors;
        public int Number { get;}
        public sbyte Errors { get { return errors; } set { errors = value < 0 ? (sbyte)0 : value; } }
        /// <summary>
        /// Тема вопроса
        /// </summary>
        public string Type { get; }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Question { get; }
        /// <summary>
        /// Количество правильных ответов 
        /// </summary>
        public byte Valid { get; }
        /// <summary>
        /// Ответы правильные и неправильные
        /// </summary>
        public Dictionary<string, bool> Answers { get { return _answers; } }

        public QuestCase()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Тема вопроса</param>
        /// <param name="quest">Вопрос</param>
        /// <param name="answers">Ответы и метки</param>
        /// <param name="bingos">Количество правильных ответов</param>
        /// <param name="number">Номер вопроса</param>
        public QuestCase(string type, string quest, Dictionary<string, bool> answers, byte bingos, int number)
        {
            Type = type;
            Question = quest;
            _answers = answers;
            Valid = bingos;
            Number = number;
        }
        /// <summary>
        /// Перемешать порядок ответов.
        /// </summary>
        public static void Shuffle(QuestCase quest)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            KeyValuePair<string, bool>[] list = new KeyValuePair<string, bool>[quest.Answers.Count];

            int x = 0;
            foreach (KeyValuePair<string, bool> v in quest.Answers)
            {
                list[x++] = new KeyValuePair<string, bool>(v.Key, v.Value);
            }
            int n = list.Length - 1;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                KeyValuePair<string, bool> value = new KeyValuePair<string, bool>(list[k].Key, list[k].Value);
                list[k] = list[n];
                list[n] = value;
                n--;
            }
            quest.Answers.Clear();
            foreach (var kvp in list)
            {
                quest.Answers.Add(kvp.Key, kvp.Value);
            }
            provider.Dispose();
        }
    }
}
