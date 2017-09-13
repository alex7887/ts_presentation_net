using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTestMockOne;
using Moq;


namespace ProjectTestMockOne_Test
{
    [TestClass]
    public class WaterProduceTest
    {
        private const String FANTA = "fanta";
        private const String JIN = "jin";

        private const String ANY = "any";

        [TestMethod]
        public void testCreate_fanta_fanta()
        {
            FantaStore fantaStore = new FantaStore();
            Water waterOne = fantaStore.create(FANTA);


            WaterProducer waterProducer = new WaterProducer();
            waterProducer.createStore(FANTA);
            Water waterTwo = waterProducer.order(FANTA);

            //Debug.
            Assert.AreEqual(waterOne.Name, waterTwo.Name, "not equal water");

        }

        [TestMethod]
        public void testWaterProducerOrder_jin_resiltjin()
        {
            
            Water waterJIN = new AlcoholicBeverage();
            waterJIN.Name = JIN;

            Mock<WaterStore> mockFanta = new Mock<WaterStore>();
            mockFanta.Setup(store => store.create(JIN)).Returns(waterJIN);
          
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockFanta.Object);
            
            Water waterResult = waterProducer.order(JIN);
            
            Assert.AreEqual(JIN, waterResult.Name);

        }

        [TestMethod]
        public void testWaterProducerOrder_JinFanta_resiltJinFanta()
        {

            Water waterFanta = new SoftDrink();
            waterFanta.Name = FANTA;

            Water waterJIN = new AlcoholicBeverage();
            waterJIN.Name = JIN;

            Mock<WaterStore> mockFanta = new Mock<WaterStore>();
            mockFanta.Setup(fantaStore => fantaStore.create(FANTA)).Returns(waterFanta);

            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(jinStore => jinStore.create(JIN)).Returns(waterJIN);

            WaterProducer waterProducer = new WaterProducer();
           
            waterProducer.Stories.Add(FANTA, mockFanta.Object);
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water jinResult = waterProducer.order(JIN);

            Assert.AreEqual(JIN, jinResult.Name);

            Water fantaResult = waterProducer.order(FANTA);

            Assert.AreEqual(FANTA, fantaResult.Name);

        }

        [TestMethod]
        public void testWaterProducerOrder_anyValue_resiltjin()
        {

            Water waterJIN = new AlcoholicBeverage();
            waterJIN.Name = JIN;

            Mock<WaterStore> mockFanta = new Mock<WaterStore>();
            mockFanta.Setup(store => store.create(It.IsAny<String>())).Returns(waterJIN);

            Water waterResult = mockFanta.Object.create(ANY);
            Assert.AreEqual(JIN, waterResult.Name);

        }

        [TestMethod]
        public void testStubWaterProducerOrder_jin_resiltjin()
        {

            Mock<Water> mockWater = new Mock<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);
           
           // mockWater.SetupProperty(water => water.Name, "jin");

            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);


            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockJin.Verify(store => store.create(FANTA), Times.Never());
           
            Assert.AreEqual(1000, mockWater.Object.Price);

        }

        /**
         * verify call method, never, at most ince
         * */
        [TestMethod]
        public void testVerifyCallName_Never()
        {

            Mock<Water> mockWater = new Mock<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);

            // mockWater.SetupProperty(water => water.Name, "jin");

            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);


            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockWater.Verify(w => w.Price, Times.Never());
            
            Assert.AreEqual(1000, mockWater.Object.Price);

        }

        /**
         * verify call method, never, at most ince
         * */
        [TestMethod]
        public void testVerifyCallName_MostOnce()
        {

            Mock<Water> mockWater = new Mock<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);

            // mockWater.SetupProperty(water => water.Name, "jin");

            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);


            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockJin.Verify(store => store.create(JIN), Times.AtMostOnce);

            Assert.AreEqual(1000, mockWater.Object.Price);

            mockWater.

        }

        //verify ALL (store.create(FANTA), water.Price)
        [TestMethod]
        public void testVerifyALLWaterProducerOrder_jin_resiltjin()
        {

            var mockRepository = new MockRepository(MockBehavior.Strict);
            var mockWater = mockRepository.Create<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);
           // mockWater.SetupProperty(water => water.Name, "jin");

            var mockJin = mockRepository.Create<WaterStore>(MockBehavior.Strict);
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);


            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

           // Water waterResult = waterProducer.order(JIN);

            mockJin.Verify(store => store.create(FANTA), Times.Never());

           // Assert.AreEqual(1000, waterResult.Price);
            
            mockRepository.VerifyAll();
            
        }
    }
}
