using dotenv.net;
using NUnit.Framework;

namespace Frends.GoogleSheets.ReadSheet.Tests;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        DotEnv.Fluent().WithEnvFiles(".env", ".env.development").Load();
    }
}