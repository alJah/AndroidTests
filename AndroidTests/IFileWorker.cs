using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidTests
{
    public interface IFileWorker
    {
        /// <summary>
        /// Файл существует?
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(string fname);
        /// <summary>
        /// Сохранить список в файл
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="cases"></param>
        void SaveBase(string fname, List<QuestCase> cases);
        /// <summary>
        /// Открыть список из файла
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        Task<List<QuestCase>> OpenBase(string fname);
        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        Task DeleteAsync(string fname);
    }
}
