using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Andrii_Mykyta_Lab5_ToP
{
    public class Tasks
    {
        public static IEnumerable<string> TaskA(List<Transportation> transportations, List<Cargo> cargoes, int year, string carNumber)
        {
            var query = from transportation in transportations
                        join cargo in cargoes on transportation.CargoCode equals cargo.Code
                        where transportation.Date.Year == year && transportation.CarNumber == carNumber
                        select cargo.Name;

            if (query.Any())
            {
                return query;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public static string TaskB(List<Transportation> transportations, List<Cargo> cargoes, DateTime fromDate, DateTime toDate)
        {
            var query = from transportation in transportations
                        join cargo in cargoes on transportation.CargoCode equals cargo.Code
                        where transportation.Date >= fromDate && transportation.Date < toDate
                        group transportation by cargo into cargoGroup
                        select new
                        {
                            CargoName = cargoGroup.Key.Name,
                            Bases = from baseGroup in cargoGroup.GroupBy(t => t.BaseNumber)
                                    select new
                                    {
                                        BaseNumber = baseGroup.Key,
                                        TotalCost = baseGroup.Sum(t => t.Cost)
                                    }
                        };

            if (query.Any())
            {
                var xmlDocument = new XElement("cargoes",
                    new XAttribute("from", fromDate.ToShortDateString()),
                    new XAttribute("to", toDate.ToShortDateString()),
                    query.Select(c => new XElement("cargo",
                        new XElement("name", c.CargoName),
                        c.Bases.Select(b => new XElement("base",
                            new XAttribute("number", b.BaseNumber),
                            b.TotalCost
                        ))
                    ))
                );

                return xmlDocument.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static IEnumerable<(string CarNumber, int CountCargos)> TaskC(List<Transportation> transportations)
        {
            var query = from transportation in transportations
                        group transportation.CarNumber by transportation.Date into dateGroup
                        let distinctCars = dateGroup.Distinct()
                        orderby distinctCars.Count() descending
                        select new
                        {
                            Date = dateGroup.Key,
                            CarNumbers = distinctCars
                        };

            foreach (var item in query)
            {
                yield return (item.Date.ToShortDateString(), item.CarNumbers.Count());
            }

            if (!query.Any())
            {
                yield return (string.Empty, 0);
            }
        }

    }
}
