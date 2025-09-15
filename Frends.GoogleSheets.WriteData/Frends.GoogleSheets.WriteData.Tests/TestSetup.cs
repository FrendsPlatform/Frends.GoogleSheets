using dotenv.net;
using NUnit.Framework;

namespace Frends.GoogleSheets.WriteData.Tests;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        DotEnv.Fluent().WithEnvFiles(".env", ".env.development").Load();
    }
}