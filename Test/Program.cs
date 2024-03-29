// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using Redis.OM;
using StackExchange.Redis;
using Test;

Console.WriteLine("Hello, World!");

string host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
string port = Environment.GetEnvironmentVariable("PORT") ?? "6379";

var muxer = ConnectionMultiplexer.Connect($"{host}:{port}");
var provider = new RedisConnectionProvider(muxer);
provider.Connection.DropIndexAndAssociatedRecords(typeof(TestModel));
provider.Connection.CreateIndex(typeof(TestModel));


var sb = new StringBuilder();

for (var i = 0; i < 250; i++)
{
    sb.Append("test");
}

string str = sb.ToString();

var db = muxer.GetDatabase();
var tasks = new Queue<Task>();
var random = new Random();

var stopwatch = Stopwatch.StartNew();
for (var i = 0; i < 140_000; i++)
{
    tasks.Enqueue(provider.Connection.SetAsync(new TestModel(){Age = random.Next(50)+18, Name = "foobarbaz", MetaData = str}));

    if (tasks.Count > 100)
    {
        await tasks.Dequeue();
    }
}
stopwatch.Stop();
Console.WriteLine($"Insertion time was: {stopwatch.ElapsedMilliseconds}ms");