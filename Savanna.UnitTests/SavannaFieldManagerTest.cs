using Autofac.Extras.Moq;
using Savanna.Entities;
using Savanna.Interfaces;
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
                for (int x = 0; x < testingClass.Area.Width; x++)
                {
                    for (int y = 0; y < testingClass.Area.Height; y++)
                    {
                        Assert.Equal(expected[x, y], actual[x, y]);
                    }
                }
            }
        }


        /*Mocking*/
        private ISavannaField SampleArea;
        private ISavannaFieldManager SampleFieldManager;

        private ICellBase[,] GetSampleField()
        {
            SampleArea = new SavannaField();
            SampleFieldManager = new SavannaFieldManager(SampleArea);

            SampleArea.Field = new CellBase[43, 43];
            for (int x = 0; x < 43; x++)
            {
                for (int y = 0; y < 43; y++)
                {
                    var ground = new Ground(SampleFieldManager);
                    ground.SetPosition(x, y);
                    SampleArea.Field[x, y] = ground;
                }
            }

            return SampleArea.Field;
        }
    }
}
