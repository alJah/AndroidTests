using AndroidTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(AndroidTests.Droid.FileWorker))]
namespace AndroidTests.Droid
{
    public class FileWorker : IFileWorker
    {
        public Task<bool> ExistAsync(string fname)
        {
            string fpath = GetFilePath(fname);
            bool exist = File.Exists(fpath);
            return Task<bool>.FromResult(exist);
        }
        public void SaveBase(string fname, List<QuestCase> cases)
        {
            string file = GetFilePath(fname);
            OpenSaveObject.Serialize(file, cases);
        }
        public async Task<List<QuestCase>> OpenBase(string fname)
        {
            string file = GetFilePath(fname);
            return await Task.FromResult((List<QuestCase>)OpenSaveObject.Deserializea(file));
        }
        string GetFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename);
        }
        public Task DeleteAsync(string fname)
        {
            File.Delete(GetFilePath(fname));
            return Task.FromResult(true);
        }
    }
}