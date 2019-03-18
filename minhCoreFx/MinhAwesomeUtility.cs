using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace minhCoreFx
{
    public static class MinhAwesomeUtility
    {
        public static async Task ReadFileNoBlocking(string filePath, Action<byte[]> process)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x1000, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[fileStream.Length];
                int bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length);

                System.Diagnostics.Debug.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");

                await Task.Run(async () => process(buffer));
            }
        }


        public static Func<string, Task<byte[]>> DownloadSite = async url =>
        {
            System.Diagnostics.Debug.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");

            var response = await new HttpClient().GetAsync(url);
            return await response.Content.ReadAsByteArrayAsync();
        };

        public static Task<T> Return<T>(T task) => Task.FromResult(task);

        public static async Task<R> Bind<T, R>(this Task<T> task, Func<T, Task<R>> cont)
            => await cont(await task.ConfigureAwait(false)).ConfigureAwait(false);

        public static async Task<R> Map<T, R>(this Task<T> task, Func<T, R> map)
            => map(await task.ConfigureAwait(false));

        public static async Task<T> Tap<T>(this Task<T> task, Func<T, Task> action)
        {
            await action(await task);
            return await task;
        }

        //public static async Task<R> SelectMany<T, R>(this Task<T> task, Func<T, Task<R>> then)
        //    => await Bind(await task);
    }
}
