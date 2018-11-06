using System;
using Autofac.Extras.Moq;
using Savanna.Entities;
using Savanna.Interfaces;
using Savanna.Services;
using Xunit;

namespace Savanna.UnitTests
{
    public class SavannaManagerTests
    {
        [Fact]
        public void Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISavannaFieldManager>()
                    .Setup(x => x.Area)
                    .Returns(GetSampleField());

                var testingClass = mock.Create<Predator>();

                var expected = GetSampleField();

                //var actual = testingClass.;
            }
            throw new NotImplementedException();
        }

        /*Mocking*/
        private ISavannaFieldManager SampleFieldManager =
                         SavannaFieldManager.GetInstance();

        private ISavannaField GetSampleField()
        {
            ISavannaField output;

            SampleFieldManager.GenerateEmptyField();

            output = SampleFieldManager.Area;

            return output;
        }
    }
}
