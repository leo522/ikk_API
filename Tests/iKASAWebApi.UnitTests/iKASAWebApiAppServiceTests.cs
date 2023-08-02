using System.Linq;
using NUnit.Framework;
using KMUH.iKASAWebApi.ApplicationLayer.DTO;
using KMUH.iKASAWebApi.ApplicationLayer.Services;

namespace KMUH.iKASAWebApi.UnitTests
{
  [TestFixture]
  public partial class iKASAWebApiAppServiceTests
  {
      private IiKASAWebApiAppService service;

      [TestFixtureSetUp]
      public void CreateService()
      {
          service = new iKASAWebApiAppService ();
      }

      [TestFixtureTearDown]
      public void DisposeService()
      {
          service.Dispose();
      }

      [Test]
      [Ignore()]
      [Category("iKASAWebApi Application Layer")]
      public void YourBusinessMethodTest()
      {

      }
  }
}