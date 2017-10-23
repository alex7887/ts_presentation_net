using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTestMockOne;
using Moq;


namespace ProjectTestMockOne_Test
{
    [TestClass]
    public class WaterProduceTes_Test
    {
        private const String FANTA = "fanta";
        private const String JIN = "jin";
        private const String ANY = "any";

        //Stub, compare two objects
        [TestMethod]
        public void testCreate_fanta_resultfanta()
        {
            FantaStore fantaStore = new FantaStore();
            Water waterOne = fantaStore.create(FANTA);
            
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.createStore(FANTA);
            Water waterTwo = waterProducer.order(FANTA);

            //сравниваются два объекта, если значения их не равны, то выкидывается сообщение "not equal fanta"
            Assert.AreEqual(waterOne.Name, waterTwo.Name, "not equal fanta");

        }

        //Stub, compare two objects
        [TestMethod]
        public void testWaterProducerOrder_jin_resultjin()
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

        //Stub, compare two objects
        [TestMethod]
        public void testWaterProducerOrder_JinFanta_resultJin()
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


        }

        //Stub, compare two objects
        [TestMethod]
        public void testWaterProducerOrder_anyValue_resultjin()
        {

            Water waterJIN = new AlcoholicBeverage();
            waterJIN.Name = JIN;

            Mock<WaterStore> mockFanta = new Mock<WaterStore>();
            mockFanta.Setup(store => store.create(It.IsAny<String>())).Returns(waterJIN);

            Water waterResult = mockFanta.Object.create(ANY);
            Assert.AreEqual(JIN, waterResult.Name);

        }

        //Mock,verify call method (create(FANTA)) never
        [TestMethod]
        public void testVerifyCallNameCreate_fanta_resultNever()
        {

            Mock<Water> mockWater = new Mock<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);
           
            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);
            
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockJin.Verify(store => store.create(FANTA), Times.Never());
           

            //это пример того, как не следует осуществлять несколько проверок в один модульный тест.
            //Эта проверка лишняя для модульного теста
            Assert.AreEqual(1000, mockWater.Object.Price);

        }

        /**
         * Mock, verify call method (w.Price), never 
         * */
        [TestMethod]
        public void testVerifyCallNamePrice_Never()
        {

            Mock<Water> mockWater = new Mock<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);
            
            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);
            
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockWater.Verify(w => w.Price, Times.Never());


        }

        
        //Mock, verify call method, never, at most ince (store.create(JIN))
        //т.е. хотя бы раз была вызвана функция store.create(JIN)         
        [TestMethod]
        public void testVerifyCallNameCreate_JIN_MostOnce()
        {

            Mock<Water> mockWater = new Mock<Water>();

            //эта строка не играет никакой роли в тесте, т.к. не явлется ее целью 
            mockWater.Setup(water => water.Price).Returns(1000);

            Mock<WaterStore> mockJin = new Mock<WaterStore>();
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);
            
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);

            Water waterResult = waterProducer.order(JIN);

            mockJin.Verify(store => store.create(JIN), Times.AtMostOnce);


        }

        //Mock, verify ALL registration functions  in Mock  (store.create(JIN), water.Price)
        //Это не модульный тест, пример показывает возможности Mock
        [TestMethod]
        public void testVerifyALLWaterProducerOrder_jin_resultTrue()
        {

            var mockRepository = new MockRepository(MockBehavior.Strict);
            var mockWater = mockRepository.Create<Water>();

            mockWater.Setup(water => water.Price).Returns(1000);
          
            var mockJin = mockRepository.Create<WaterStore>(MockBehavior.Strict);
            mockJin.Setup(store => store.create(JIN)).Returns(mockWater.Object);
            
            WaterProducer waterProducer = new WaterProducer();
            waterProducer.Stories.Add(JIN, mockJin.Object);
                        
            Water waterFanta = waterProducer.order(JIN);

            //mockRepository.VerifyAll(); 
            
        }
    }
  }
