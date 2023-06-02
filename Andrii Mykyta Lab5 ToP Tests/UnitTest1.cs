using Andrii_Mykyta_Lab5_ToP;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Andrii_Mykyta_Lab5_ToP_Tests
{
    public class Tests
    {
        private List<Transportation> GetTransportations()
        {
            var transportations = new List<Transportation>();

            for (int i = 0; i < 25; i++)
            {
                transportations.Add(new Transportation
                {
                    BaseNumber = $"Base{i}",
                    CarNumber = $"Car{i}",
                    Date = DateTime.Now.AddDays(i),
                    CargoCode = $"Cargo{i}",
                    Cost = i * 100
                });
            }

            return transportations;
        }

        private List<Cargo> GetCargoes()
        {
            var cargoes = new List<Cargo>();

            for (int i = 0; i < 25; i++)
            {
                cargoes.Add(new Cargo($"Cargo{i}", $"CargoName{i}"));
            }

            return cargoes;
        }

        [Test]
        public void TaskA_ReturnsCargoNames_WhenValidDataExists()
        {
            var transportations = GetTransportations();
            var cargoes = GetCargoes();
            int year = DateTime.Now.Year;
            string carNumber = "Car0";

            var result = Tasks.TaskA(transportations, cargoes, year, carNumber);

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void TaskA_ReturnsEmptyResult_WhenNoValidDataExists()
        {
            var transportations = GetTransportations();
            var cargoes = GetCargoes();
            int year = DateTime.Now.Year + 1;
            string carNumber = "InvalidCar";

            var result = Tasks.TaskA(transportations, cargoes, year, carNumber);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void TaskB_ReturnsXmlString_WhenValidDataExists()
        {
            var transportations = GetTransportations();
            var cargoes = GetCargoes();
            DateTime fromDate = DateTime.Now.AddDays(-10);
            DateTime toDate = DateTime.Now;

            var result = Tasks.TaskB(transportations, cargoes, fromDate, toDate);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Does.StartWith("<cargoes"));
            Assert.That(result, Does.EndWith("</cargoes>"));
        }

        [Test]
        public void TaskB_ReturnsEmptyResult_WhenNoValidDataExists()
        {
            var transportations = GetTransportations();
            var cargoes = GetCargoes();
            DateTime fromDate = DateTime.Now.AddDays(30);
            DateTime toDate = DateTime.Now.AddDays(40);

            var result = Tasks.TaskB(transportations, cargoes, fromDate, toDate);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void TaskC_ReturnsCarNumberCountPairs_WhenValidDataExists()
        {
            var transportations = GetTransportations();

            var result = Tasks.TaskC(transportations);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TaskC_ReturnsEmptyResult_WhenNoValidDataExists()
        {
            var transportations = new List<Transportation>();

            var result = Tasks.TaskC(transportations);
            var expectation = new List<(string CarNumber, int CountCargos)>()
            {
                (string.Empty, 0)
            };

            Assert.AreEqual(expectation, result);
        }

    }
}