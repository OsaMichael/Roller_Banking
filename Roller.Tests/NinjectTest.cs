//using Ninject;
//using System;
//using Xunit;

//namespace Roller.Tests
//{
//    //public class UnitTest1
//    //{
//        [TestClass]
//        public class NinjectTest
//        {
//            [TestMethod]
//            public void TestNinjectBindings()
//            {
//                //Create Kernel
//                var kernel = new StandardKernel();

//                var assembly = Assembly.Load("Cyber_Kitchen");

//                kernel.Load(assembly);
//                kernel.Get<IRestaurantManager>();
//                kernel.Get<IVoterManager>();

//                // best way to do this
//                var interfaces = assembly.GetTypes()
//                                        .Where(t => t.FullName.StartsWith("Cyber_Kitchen.Interface"))
//                                        .Where(t => t.IsInterface).ToList();

//                foreach (var iInterface in interfaces)
//                {
//                    kernel.Get(iInterface);
//                }
//            }

//        private class TestMethodAttribute : Attribute
//        {
//        }
//    }
//    }
//}
