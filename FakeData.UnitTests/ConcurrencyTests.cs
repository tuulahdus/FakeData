using System.Collections.Generic;
using System.Threading.Tasks;
using FakeData.Model;
using FakeData.Random;
using Xunit;

namespace FakeData.UnitTests
{
    public class ConcurrencyTests
    {
        [Fact]
        [Trait("Category","Concurrency")]
        public void RandomPerson_Is_Thread_Safe()
        {
            List<Task<IPerson>> tasks = new List<Task<IPerson>>();
            var locales = new List<string> {
                "de-DE", "de-CH", "de-AT", "nl-NL", "nl-BE","nb-NO","fi-FI","sv-SE"
            };

            foreach (var locale in locales)
            {
                for (int i = 0; i < 3; i++)
                {
                    tasks.Add(Task.Run(() =>new RandomPerson(locale).GetPerson));
                }
            }
            Task.WhenAll(tasks).Wait();

            var r = tasks.ConvertAll(t => t.IsCompletedSuccessfully);
            Assert.All(r, item => Assert.True(item));
        }

        [Fact]
        [Trait("Category","Concurrency")]
        public void RandomPerson_Is_Thread_Safe2()
        {
            var res = TestHelpers.RunTaskInParallel(() => Task.Run(() => new RandomPerson("de-AT").GetPerson), 4).Result;
            Assert.True(res.IsSuccess);
        }
    }
}