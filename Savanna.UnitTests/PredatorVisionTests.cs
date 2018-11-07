using Autofac.Extras.Moq;
using Savanna.Entities;
using Savanna.Interfaces;
using Savanna.Services;
using Xunit;

namespace Savanna.UnitTests
{
    public class PredatorVisionTests
    {
        [Fact]
        public void LookAroundForAVictim_ShouldNotFindAny_ReturnsNull()
        {
            using (var mock = AutoMock.GetLoose())
            {
                ICellBase expected = null;
                var area = new SavannaField();
                var dependency = new SavannaFieldManager(area);
                var pathDep = new AStarPathfinding(null, dependency);

                var field = mock.Provide<ISavannaFieldManager>(dependency);
                field.GenerateEmptyField();

                var pathfinder = mock.Provide<IPathfinder>(pathDep);
                var testingClass = mock.Create<Predator>();

                var actual = testingClass.LookAroundFor<GrassEater>();
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void LookAroundForAVictim_ShouldFind_ReturnsGrasseater()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var area = new SavannaField();
                var dependency = new SavannaFieldManager(area);
                var pathDep = new AStarPathfinding(null, dependency);
                mock.Provide<IPathfinder>(pathDep);
                var field = mock.Provide<ISavannaFieldManager>(dependency);
                field.GenerateEmptyField();

                var victim = mock.Create<GrassEater>();
                field.Area.Field[1, 1] = victim;
                var testingClass = mock.Create<Predator>();

                var actual = testingClass.LookAroundFor<GrassEater>();
                Assert.IsType<GrassEater>(actual);
            }
        }
    }
}
