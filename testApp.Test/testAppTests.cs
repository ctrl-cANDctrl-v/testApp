using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace testApp.Test
{
    [TestClass]
    public class testAppTests
    {
        [TestMethod]
        public void BoxVolume_10and5and5_250returned()
        {
            // arrange
            int width = 10;
            int height = 5;
            int depth = 5;
            int expected = 250;

            // act
            Box box = new Box();
            box.Width = 10;
            box.Height = 5;
            box.Depth = 5;
            double actual = box.Volume;

            // Assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanContain_50and500_true()
        {
            // arrange
            Box box = new Box();
            Pallet pallet = new Pallet();
            box.Width = 10;
            box.Height = 5;
            pallet.Width = 20;
            pallet.Height = 25;
            bool expected = true;

            // act      
            bool actual = pallet.CanContain(box);
           
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
