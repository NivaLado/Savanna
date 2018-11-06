using System;
using Autofac.Extras.Moq;
using Savanna.Entities;
using Savanna.Interfaces;
using Savanna.Services;
using Xunit;

namespace Savanna.UnitTests
{
    public class PredatorTest
    {
        [Fact]
        public void Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISavannaFieldManager>()
                    .Setup(x => x.area)
                    .Returns(GetSampleField());

                var testingClass = mock.Create<Predator>();

                var expected = GetSampleField();

                var actual = testingClass;
            }
            throw new NotImplementedException();
        }

        private ISavannaFieldManager SampleFieldManager =
            new SavannaFieldManager(SavannaField.GetInstance());

        private ISavannaField GetSampleField()
        {
            ISavannaField output = SavannaField.GetInstance();

            output.Field = new CellBase[43, 43];
            for (int x = 0; x < output.Field.GetLength(0); x++)
            {
                for (int y = 0; y < output.Field.GetLength(1); y++)
                {
                    var ground = new Ground(SampleFieldManager);
                    ground.SetPosition(x, y);
                    output.Field[x, y] = ground;
                }
            }
            return output;
        }
    }
}
