using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Catcher.FileStorageHelper
{
    public class StorageHelper
    {
        async public static Task SaveTextToFile(string fileName, string content)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync
                (fileName, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, content);
        }

        async public static Task<string> ReadTextFromFile(string fileName)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                var result = await Windows.Storage.FileIO.ReadTextAsync(file);
                return result;
            }
            catch
            { return String.Empty; }
        }
    }
}
