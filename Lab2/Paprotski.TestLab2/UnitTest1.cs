using System;
using Paprotski.Lab2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paprotski.TestLab2
{
    using System.Linq;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateObject()
        {
            var coupes = new Coupe[6]
                             {
                                 new Coupe(true, false, 4, 3), new Coupe(false, false, 4, 4), new Coupe(true, false, 4, 2),
                                 new Coupe(false, false, 4, 1), new Coupe(true, true, 4, 2), new Coupe(true, false, 4, 3)
                             };

            var wagons = new Wagon[6]
                             {
                                 new Wagon(coupes, 2008, 25, 5, 130), new Wagon(coupes, 2009, 25, 15, 130),
                                 new Wagon(coupes, 2008, 25, 15, 130), new Wagon(coupes, 2010, 25, 15, 130),
                                 new Wagon(coupes, 2008, 25, 15, 130), new Wagon(coupes, 2012, 25, 15, 130)
                             };

            var train = new Train(
                wagons,
                2014,
                TrainType.Local,
                "Gomel",
                "Minsk",
                DateTime.Parse("8:00"),
                DateTime.Parse("13:00"));
        }

        [TestMethod]
        public void TestComparisonOperationsObject()
        {
            var coupe1 = new Coupe(false, true, 4, 3);
            var coupe2 = new Coupe(false, true, 4, 3);

            Assert.AreEqual(coupe1.Equals(coupe1), true);
            Assert.AreEqual(coupe1.Equals(null), false);
            Assert.AreEqual(coupe1.Equals(coupe2) == coupe2.Equals(coupe1), true);

            var wagon1 = new Wagon(2014, 30, 4, 130);
            var wagon2 = new Wagon(2014, 30, 4, 130);

            Assert.AreEqual(wagon1.Equals(wagon1), true);
            Assert.AreEqual(wagon1.Equals(null), false);
            Assert.AreEqual(wagon1.Equals(wagon2) == wagon2.Equals(wagon1), true);

            var train1 = new Train(
                2014,
                TrainType.Internationl,
                "Minsk",
                "Brest",
                DateTime.Parse("7:45"),
                DateTime.Parse("12:50"));

            var train2 = new Train(
                2014,
                TrainType.Internationl,
                "Minsk",
                "Brest",
                DateTime.Parse("7:45"),
                DateTime.Parse("12:50"));

            Assert.AreEqual(train1.Equals(train1), true);
            Assert.AreEqual(train1.Equals(null), false);
            Assert.AreEqual(train1.Equals(train2) == train2.Equals(train1), true);
        }

        [TestMethod]
        public void TestCorrectDataObject()
        {
            var coupes = new Coupe[6]
                             {
                                 new Coupe(true), new Coupe(false, false, 4, 4), new Coupe(true, false, 4, 2),
                                 new Coupe(false, false, 4, 1), new Coupe(true, true, 4, 2), new Coupe(true, false, 4, 3)
                             };

            var wagons = new Wagon(coupes, 2008, 25, 5, 100);

            wagons.Coupes.Remove(coupes[0]);
            Assert.AreEqual(wagons.Coupes.Contains(coupes[0]), false);

            wagons.Coupes.RemoveAt(0);
            Assert.AreEqual(wagons.Coupes[0], coupes[2]);

            wagons.Coupes.Add(new Coupe(true, true, 4, 4));
            Assert.AreEqual(wagons.Coupes.Contains(new Coupe(true, true, 4, 4)), true);
        }

        [TestMethod]
        public void TestGroupObject()
        {
            var coupes = new Coupe[6]
                             {
                                 new Coupe(false, false, 4, 2), new Coupe(false, false, 4, 4), new Coupe(true, false, 4, 2),
                                 new Coupe(false, false, 4, 1), new Coupe(true, true, 4, 2), new Coupe(true, false, 4, 3)
                             };

            var wagons1 = new Wagon(coupes, 2008, 25, 5, 100);
            var wagons2 = new Wagon(coupes, 2003, 25, 10, 150);
            var wagons4 = new Wagon(2014, 30, 4, 120); 
            var wagons3 = wagons1.Where(coupe => coupe.IsVipStatus);


            Assert.AreEqual(wagons3.Count(), 3); 
            Assert.AreEqual(wagons1.Min(coupe => coupe.SeatsNumberOccupied), 1);
            Assert.AreEqual(wagons2.OrderBy(coupes1 => coupes1.SeatsNumberOccupied).ElementAt(5), coupes[1]);
            Assert.AreEqual(coupes.All(coupe => coupe.SeatsNumber == 4), true);
        }
    }
}
