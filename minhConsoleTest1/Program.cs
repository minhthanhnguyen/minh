using minhCoreFx;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace minhConsoleTest1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //TestReadingFile(@"C:\users\nguyen\desktop\old.txt");

            //TestDownload();

            //await DownloadSiteAsync("http://www.cccis.com");

            LinqTest1();

            Console.ReadLine();
        }

        async static void TestReadingFile(string fileName)
        {
            Console.WriteLine("---- about to read -------");

            await MinhAwesomeUtility.ReadFileNoBlocking(fileName, (contents) =>
            {
                using (var streamReader = new StreamReader(new MemoryStream(contents)))
                {
                    string s = streamReader.ReadToEnd();
                    Console.WriteLine($"file contents: {s}");
                }

                System.Diagnostics.Debug.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine("---- finished reading ------");
        }

        async static void TestDownload()
        {

            await MinhAwesomeUtility.DownloadSite("http://www.cccis.com").
                ContinueWith((bytes) =>
                {
                    System.Diagnostics.Debug.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");

                    using (var streamReader = new StreamReader(new MemoryStream(bytes.Result)))
                    {
                        string s = streamReader.ReadToEnd();
                        Console.WriteLine($"site content: {s}");
                    }
                });

            //System.Diagnostics.Debug.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");

            //byte[] contents = await MinhAwesomeUtility.DownloadSite("http://www.cccis.com");

            //using (var streamReader = new StreamReader(new MemoryStream(contents)))
            //{
            //    string s = streamReader.ReadToEnd();
            //    Console.WriteLine($"site content: {s}");
            //}           
        }

        async static Task DownloadSiteAsync(string site)
        {
            await new HttpClient().GetAsync(site).
                            Bind(async content => await content.Content.ReadAsByteArrayAsync()).
                            Map(bytes => bytes).
                            Tap(async b =>
                            {
                                using (var streamReader = new StreamReader(new MemoryStream(b)))
                                {
                                    string s = streamReader.ReadToEnd();
                                    Console.WriteLine($"site content: {s}");
                                }
                            }
                            );
        }

        async static void LinqTest1()
        {
            var defects = new List<Defect>
            {
                new Defect
                {
                    ID = 1,
                    Name = "first defect",
                    CreatedDateTime = new DateTime(2019, 1, 1),
                },

                new Defect
                {
                    ID = 2,
                    Name = "second defect",
                    CreatedDateTime = new DateTime(2019, 1, 5),
                },

                new Defect
                {
                    ID = 3,
                    Name = "third defect",
                    CreatedDateTime = new DateTime(2019, 1, 9),
                },

                new Defect
                {
                    ID = 4,
                    Name = "fourth defect",
                    CreatedDateTime = new DateTime(2019, 1, 9),
                },
            };

            DateTime start = new DateTime(2019, 1, 1);
            DateTime end = new DateTime(2019, 1, 31);

            var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                                  .Select(offset => start.AddDays(offset));

            var query = from date in dates
                        join defect in defects
                        on date equals defect.CreatedDateTime.Date
                        into joined
                        select new { Date = date, Count = joined.Count() };


            foreach (var grouped in query)
            {
                Console.WriteLine("{0:d}: {1}", grouped.Date, grouped.Count);
            }

            await Task.Delay(1);
        }
    }

    public class Defect
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
