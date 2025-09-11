using dotenv.net;
using NUnit.Framework;

namespace Frends.GoogleSheets.CreateSheet.Tests;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        DotEnv.Fluent().WithEnvFiles(".env", ".env.development").Load();
    }
}