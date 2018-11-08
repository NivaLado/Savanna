using Autofac.Extras.Moq;
using Savanna.Constants;
using Savanna.Entities.Interfaces;
using Savanna.Services;
using Xunit;

namespace Savanna.UnitTests
{
    public class SavannaFieldManagerTest
    {
        [Fact]
        public void GenerateEmptyField_ShouldFillArrayWithGrounds()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISavannaField>()
                    .Setup(x => x.Field)
                    .Returns(GetSampleField());

                var testingClass = mock.Create<SavannaFieldManager>();
                testingClass.GenerateEmptyField();

                var expected = GetSampleField();
                var actual = testingClass.Area.Field;

                Assert.True(actual != null);
                Assert.Equal(expected.Length, actual.Length);
            }
        }

        /*Mocking*/
        private ICellBase[,] GetSampleField()
        {
            ICellBase[,] output = new ICellBase[Globals.Width, Globals.Height];
            return output;
        }
    }
}
